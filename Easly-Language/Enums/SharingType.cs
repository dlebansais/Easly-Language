namespace BaseNode
{
    /// <summary>
    /// Sharing type.
    /// </summary>
    public enum SharingType
    {
        /// <summary>
        /// Not shared.
        /// </summary>
        NotShared,

        /// <summary>
        /// Shared, read and write access.
        /// </summary>
        ReadWrite,

        /// <summary>
        /// Shared, read only access.
        /// </summary>
        ReadOnly,

        /// <summary>
        /// Shared, write only access.
        /// </summary>
        WriteOnly,
    }
}
