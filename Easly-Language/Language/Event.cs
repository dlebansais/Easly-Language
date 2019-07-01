namespace Easly
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    public interface IEvent
    {
        bool IsSignaled { get; }
        void Raise();
        bool Wait();
    }

    public class EventBase
    {
        public EventBase(bool isAutoReset, bool isSignaled = false)
        {
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

        public static EventBase operator |(EventBase event1, EventBase event2)
        {
            return new EventBase(event1, event2, () => { event1.Evaluate(); event2.Evaluate(); return event1.IsSignaled || event2.IsSignaled; });
        }

        public static EventBase operator &(EventBase event1, EventBase event2)
        {
            return new EventBase(event1, event2, () => { event1.Evaluate(); event2.Evaluate(); return event1.IsSignaled && event2.IsSignaled; });
        }

        public static bool operator true(EventBase f)
        {
            return false;
        }

        public static bool operator false(EventBase f)
        {
            return false;
        }

        protected List<EventWaitHandle> HandleList;
    }

    public class Event : EventBase, IEvent
    {
        public Event(bool isAutoReset, bool isSignaled = false)
            : base(isAutoReset, isSignaled)
        {
        }

        public void Raise()
        {
            Debug.Assert(HandleList.Count == 1);

            IsSignaled = true;
            HandleList[0].Set();
        }
    }
}
