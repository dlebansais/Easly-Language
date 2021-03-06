﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface IDocument
    {
        string Comment { get; }
        System.Guid Uuid { get; }
    }

    [System.Serializable]
    public class Document : IDocument
    {
        public virtual string Comment { get; set; }
        public virtual System.Guid Uuid { get; set; }
    }
}
