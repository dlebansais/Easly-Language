namespace Easly
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Threading;

    /// <summary>
    /// Represents an event.
    /// </summary>
    public class EventBase
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="EventBase"/> class.
        /// </summary>
        /// <param name="isAutoReset">True if the event is reset when a wait is completed; false if the event remains signaled forever.</param>
        /// <param name="isSignaled">True if the event starts in signaled state.</param>
        public EventBase(bool isAutoReset, bool isSignaled = false)
        {
            IsSignaled = isSignaled;
            Signaler = () => { return IsSignaled; };

            if (isAutoReset)
            {
                HandleList = new List<EventWaitHandle>() { new AutoResetEvent(IsSignaled) };
                Updater = () => { IsSignaled = false; };
            }
            else
            {
                HandleList = new List<EventWaitHandle>() { new ManualResetEvent(IsSignaled) };
                Updater = () => { };
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBase"/> class.
        /// </summary>
        /// <param name="event1">First nested event.</param>
        /// <param name="event2">Second nested event.</param>
        /// <param name="signaler">The predicate for the signaled state.</param>
        /// <param name="updater">The action to take for the signaled state.</param>
        protected EventBase(EventBase event1, EventBase event2, Func<bool> signaler, Action updater)
        {
            Contract.Requires(event1 != null);
            Contract.Requires(event2 != null);

            HandleList = new List<EventWaitHandle>();
            HandleList.AddRange(event1!.HandleList);
            HandleList.AddRange(event2!.HandleList);

            Signaler = signaler;
            Updater = updater;
            Evaluate();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the predicate for the signaled state.
        /// </summary>
        protected Func<bool> Signaler { get; private set; }

        /// <summary>
        /// Gets the action to take for the signaled state.
        /// </summary>
        protected Action Updater { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether event is signaled.
        /// </summary>
        public bool IsSignaled { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether event is always signaled.
        /// </summary>
        public bool IsTrue { get { return false; } }

        /// <summary>
        /// Gets a value indicating whether event is never signaled.
        /// </summary>
        public bool IsFalse { get { return false; } }

        /// <summary>
        /// Gets the list of event wait handles.
        /// </summary>
        protected List<EventWaitHandle> HandleList { get; private set; }
        #endregion

        #region Operators
        /// <summary>
        /// Combines two events that must be signaled together.
        /// </summary>
        /// <param name="event1">The first event.</param>
        /// <param name="event2">The second event.</param>
        /// <returns>True if both events are signaled.</returns>
        public static EventBase operator &(EventBase event1, EventBase event2)
        {
            return BitwiseAnd(event1, event2);
        }

        /// <summary>
        /// Combines two events that must be signaled together.
        /// </summary>
        /// <param name="event1">The first event.</param>
        /// <param name="event2">The second event.</param>
        /// <returns>The combination of the two events.</returns>
        public static EventBase BitwiseAnd(EventBase event1, EventBase event2)
        {
            Func<bool> Signaler = () =>
            {
                event1.Evaluate();
                event2.Evaluate();
                return event1.IsSignaled && event2.IsSignaled;
            };
            Action Updater = () =>
            {
                event1.Updater();
                event2.Updater();
            };

            return new EventBase(event1, event2, Signaler, Updater);
        }

        /// <summary>
        /// Combines two events that can be signaled separately.
        /// </summary>
        /// <param name="event1">The first event.</param>
        /// <param name="event2">The second event.</param>
        /// <returns>True if either events is signaled.</returns>
        public static EventBase operator |(EventBase event1, EventBase event2)
        {
            return BitwiseOr(event1, event2);
        }

        /// <summary>
        /// Combines two events that can be signaled separately.
        /// </summary>
        /// <param name="event1">The first event.</param>
        /// <param name="event2">The second event.</param>
        /// <returns>The combination of the two events.</returns>
        public static EventBase BitwiseOr(EventBase event1, EventBase event2)
        {
            Func<bool> Signaler = () =>
            {
                event1.Evaluate();
                event2.Evaluate();
                return event1.IsSignaled || event2.IsSignaled;
            };
            Action Updater = () =>
            {
                event1.Updater();
                event2.Updater();
            };

            return new EventBase(event1, event2, Signaler, Updater);
        }

        /// <summary>
        /// Combines two events that can be signaled separately but not together.
        /// </summary>
        /// <param name="event1">The first event.</param>
        /// <param name="event2">The second event.</param>
        /// <returns>True if either events is signaled.</returns>
        public static EventBase operator ^(EventBase event1, EventBase event2)
        {
            return Xor(event1, event2);
        }

        /// <summary>
        /// Combines two events that can be signaled separately but not together.
        /// </summary>
        /// <param name="event1">The first event.</param>
        /// <param name="event2">The second event.</param>
        /// <returns>The combination of the two events.</returns>
        public static EventBase Xor(EventBase event1, EventBase event2)
        {
            Func<bool> Signaler = () =>
            {
                event1.Evaluate();
                event2.Evaluate();
                return event1.IsSignaled ^ event2.IsSignaled;
            };
            Action Updater = () =>
            {
                event1.Updater();
                event2.Updater();
            };

            return new EventBase(event1, event2, Signaler, Updater);
        }

        /// <summary>
        /// Combines two events such that one implies the other.
        /// </summary>
        /// <param name="event1">The first event.</param>
        /// <param name="event2">The second event.</param>
        /// <returns>True if <paramref name="event1"/> is not signaled, or <paramref name="event2"/> is.</returns>
        public static EventBase operator /(EventBase event1, EventBase event2)
        {
            return Implies(event1, event2);
        }

        /// <summary>
        /// Combines two events such that one implies the other.
        /// </summary>
        /// <param name="event1">The first event.</param>
        /// <param name="event2">The second event.</param>
        /// <returns>The combination of the two events.</returns>
        public static EventBase Implies(EventBase event1, EventBase event2)
        {
            Func<bool> Signaler = () =>
            {
                event1.Evaluate();
                event2.Evaluate();
                return !event1.IsSignaled || event2.IsSignaled;
            };
            Action Updater = () =>
            {
                event1.Updater();
                event2.Updater();
            };

            return new EventBase(event1, event2, Signaler, Updater);
        }

        /// <summary>
        /// Returns the signaled state of <paramref name="event1"/>.
        /// </summary>
        /// <param name="event1">The event.</param>
        /// <returns>The signaled state.</returns>
        public static bool operator true(EventBase event1)
        {
            Contract.Requires(event1 != null);

            return event1!.IsTrue;
        }

        /// <summary>
        /// Returns the unsignaled state of <paramref name="event1"/>.
        /// </summary>
        /// <param name="event1">The event.</param>
        /// <returns>The unsignaled state.</returns>
        public static bool operator false(EventBase event1)
        {
            Contract.Requires(event1 != null);

            return event1!.IsFalse;
        }
        #endregion

        #region Client Interface
        /// <summary>
        /// Checks if the event is in signaled state.
        /// </summary>
        public void Evaluate()
        {
            IsSignaled = Signaler();
        }

        /// <summary>
        /// Waits for the event to be in signaled state.
        /// </summary>
        /// <returns>True if the event is in signaled state upon return.</returns>
        public bool Wait()
        {
            do
            {
                WaitHandle.WaitAny(HandleList.ToArray());
                Evaluate();
            }
            while (!IsSignaled);

            Updater();

            return true;
        }
        #endregion
    }
}
