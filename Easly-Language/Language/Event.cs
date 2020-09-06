﻿namespace Easly
{
    using System.Diagnostics;

    public interface IEvent
    {
        bool IsSignaled { get; }
        void Raise();
        bool Wait();
    }

    public class Event : EventBase, IEvent
    {
        #region Init
        public Event(bool isAutoReset, bool isSignaled = false)
            : base(isAutoReset, isSignaled)
        {
        }
        #endregion

        #region Client Interface
        public void Raise()
        {
            Debug.Assert(HandleList.Count == 1, "Can wait on multiple events, but can only raise one");

            IsSignaled = true;
            HandleList[0].Set();
        }
        #endregion
    }
}
