﻿using System.Reflection;

namespace Easly
{
    public class ProcedureEntity : NamedFeatureEntity
    {
        #region Init
        public ProcedureEntity(MemberInfo FeatureInfo)
            : base(FeatureInfo)
        {
        }
        #endregion
    }
}