namespace Easly
{
    using System.Diagnostics;

    /// <summary>
    /// Represents an event.
    /// </summary>
    public class Event : EventBase
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="isAutoReset">True if the event is reset when a wait is completed; false if the event remains signaled forever.</param>
        /// <param name="isSignaled">True if the event starts in signaled state.</param>
        public Event(bool isAutoReset, bool isSignaled = false)
            : base(isAutoReset, isSignaled)
        {
        }
        #endregion

        #region Client Interface
        /// <summary>
        /// Raises the event, setting its state to signaled.
        /// </summary>
        public void Raise()
        {
            Debug.Assert(HandleList.Count == 1, "Can wait on multiple events, but can only raise one");

            IsSignaled = true;
            HandleList[0].Set();
        }
        #endregion
    }
}
