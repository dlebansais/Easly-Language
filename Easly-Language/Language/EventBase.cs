namespace Easly
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class EventBase
    {
        public EventBase(bool isAutoReset, bool isSignaled = false)
        {
            IsSignaled = isSignaled;

            EventWaitHandle Handle;

            if (isAutoReset)
                Handle = new AutoResetEvent(IsSignaled);
            else
                Handle = new ManualResetEvent(IsSignaled);

            HandleList = new List<EventWaitHandle>() { Handle };

            Signaler = () => { return IsSignaled; };
            Evaluate();
        }

        public EventBase(EventBase event1, EventBase event2, Func<bool> signaler)
        {
            HandleList = new List<EventWaitHandle>();
            HandleList.AddRange(event1.HandleList);
            HandleList.AddRange(event2.HandleList);

            Signaler = signaler;
            Evaluate();
        }

        public Func<bool> Signaler { get; private set; }
        public bool IsSignaled { get; protected set; }
        public bool IsTrue { get { return false; } }
        public bool IsFalse { get { return false; } }

        public void Evaluate()
        {
            IsSignaled = Signaler();
        }

        public bool Wait()
        {
            while (!IsSignaled)
            {
                WaitHandle.WaitAny(HandleList.ToArray());
                Evaluate();
            }

            return IsSignaled;
        }

        public static EventBase operator &(EventBase event1, EventBase event2)
        {
            return BitwiseAnd(event1, event2);
        }

        public static EventBase BitwiseAnd(EventBase event1, EventBase event2)
        {
            return new EventBase(event1, event2, () => { event1.Evaluate(); event2.Evaluate(); return event1.IsSignaled && event2.IsSignaled; });
        }

        public static EventBase operator |(EventBase event1, EventBase event2)
        {
            return BitwiseOr(event1, event2);
        }

        public static EventBase BitwiseOr(EventBase event1, EventBase event2)
        {
            return new EventBase(event1, event2, () => { event1.Evaluate(); event2.Evaluate(); return event1.IsSignaled || event2.IsSignaled; });
        }

        public static EventBase operator ^(EventBase event1, EventBase event2)
        {
            return Xor(event1, event2);
        }

        public static EventBase Xor(EventBase event1, EventBase event2)
        {
            return new EventBase(event1, event2, () => { event1.Evaluate(); event2.Evaluate(); return event1.IsSignaled ^ event2.IsSignaled; });
        }

        public static EventBase operator /(EventBase event1, EventBase event2)
        {
            return Implies(event1, event2);
        }

        public static EventBase Implies(EventBase event1, EventBase event2)
        {
            return new EventBase(event1, event2, () => { event1.Evaluate(); event2.Evaluate(); return !event1.IsSignaled || event2.IsSignaled; });
        }

        public static bool operator true(EventBase event1)
        {
            return event1.IsTrue;
        }

        public static bool operator false(EventBase event1)
        {
            return event1.IsFalse;
        }

        protected List<EventWaitHandle> HandleList { get; private set; }
    }
}
