namespace Easly
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Threading;

    public class EventBase
    {
        #region Init
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
            Contract.Requires(event1 != null);
            Contract.Requires(event2 != null);

            HandleList = new List<EventWaitHandle>();
            HandleList.AddRange(event1!.HandleList);
            HandleList.AddRange(event2!.HandleList);

            Signaler = signaler;
            Evaluate();
        }
        #endregion

        #region Properties
        public Func<bool> Signaler { get; private set; }
        public bool IsSignaled { get; protected set; }
        public bool IsTrue { get { return false; } }
        public bool IsFalse { get { return false; } }
        protected List<EventWaitHandle> HandleList { get; private set; }
        #endregion

        #region Operators
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
            Contract.Requires(event1 != null);

            return event1!.IsTrue;
        }

        public static bool operator false(EventBase event1)
        {
            Contract.Requires(event1 != null);

            return event1!.IsFalse;
        }
        #endregion

        #region Client Interface
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
        #endregion
    }
}
