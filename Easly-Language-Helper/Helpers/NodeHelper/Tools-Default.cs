#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static partial class NodeHelper
    {
        public static bool IsOptionalAssignedToDefault(IOptionalReference optional)
        {
            if (optional == null) throw new ArgumentNullException(nameof(optional));

            if (!optional.IsAssigned)
                return false;

            Node Node = (Node)optional.Item;
            Debug.Assert(Node != null, $"The optional item is always a {nameof(Node)}");

            if (Node == null)
                return false;

            return IsDefaultNode(Node);
        }

        public static bool IsDefaultNode(Node node)
        {
            switch (node)
            {
                case Name AsName:
                    return IsDefaultName(AsName);

                case Identifier AsIdentifier:
                    return IsDefaultIdentifier(AsIdentifier);

                case Scope AsScope:
                    return IsDefaultScope(AsScope);

                case QualifiedName AsQualifiedName:
                    return IsDefaultQualifiedName(AsQualifiedName);

                case ObjectType AsObjectType:
                    return IsDefaultObjectType(AsObjectType);

                case Expression AsExpression:
                    return IsDefaultExpression(AsExpression);

                case Body AsBody:
                    return IsDefaultBody(AsBody);

                case Argument AsArgument:
                    return IsDefaultArgument(AsArgument);

                default:
                    return IsEmptyNode(node);
            }
        }

        public static bool IsDefaultName(Name nodeName)
        {
            return nodeName.Text.Length == 0;
        }

        public static bool IsDefaultIdentifier(Identifier nodeIdentifier)
        {
            return nodeIdentifier.Text.Length == 0;
        }

        public static bool IsDefaultScope(Scope nodeScope)
        {
            return nodeScope.EntityDeclarationBlocks.NodeBlockList.Count == 0 && nodeScope.InstructionBlocks.NodeBlockList.Count == 0;
        }

        public static bool IsDefaultQualifiedName(QualifiedName nodeQualifiedName)
        {
            IList<Identifier> Path = nodeQualifiedName.Path; // Debug.Assert(Path.Count > 0);
            return Path.Count == 1 && Path[0].Text.Length == 0;
        }

        public static bool IsDefaultObjectType(ObjectType nodeObjectType)
        {
            switch (nodeObjectType)
            {
                case SimpleType AsSimpleType:
                    return IsDefaultSimpleType(AsSimpleType);

                default:
                    return false;
            }
        }

        public static bool IsDefaultSimpleType(SimpleType nodeSimpleType)
        {
            return nodeSimpleType.Sharing == SharingType.NotShared && nodeSimpleType.ClassIdentifier.Text.Length == 0;
        }

        public static bool IsDefaultBody(Body nodeBody)
        {
            switch (nodeBody)
            {
                case EffectiveBody AsEffectiveBody:
                    return IsDefaultEffectiveBody(AsEffectiveBody);

                default:
                    return false;
            }
        }

        public static bool IsDefaultEffectiveBody(EffectiveBody nodeEffectiveBody)
        {
            return nodeEffectiveBody.RequireBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.EnsureBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.ExceptionIdentifierBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.EntityDeclarationBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.BodyInstructionBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.ExceptionHandlerBlocks.NodeBlockList.Count == 0;
        }

        public static bool IsDefaultExpression(Expression nodeExpression)
        {
            switch (nodeExpression)
            {
                case QueryExpression AsQueryExpression:
                    return IsDefaultQueryExpression(AsQueryExpression);

                case ManifestCharacterExpression AsManifestCharacterExpression:
                    return IsDefaultManifestCharacterExpression(AsManifestCharacterExpression);

                case ManifestNumberExpression AsManifestNumberExpression:
                    return IsDefaultManifestNumberExpression(AsManifestNumberExpression);

                case ManifestStringExpression AsManifestStringExpression:
                    return IsDefaultManifestStringExpression(AsManifestStringExpression);

                default:
                    return false;
            }
        }

        public static bool IsDefaultQueryExpression(QueryExpression nodeQueryExpression)
        {
            IList<Identifier> Path = nodeQueryExpression.Query.Path; // Debug.Assert(Path.Count > 0);
            return nodeQueryExpression.ArgumentBlocks.NodeBlockList.Count == 0 && Path.Count == 1 && Path[0].Text.Length == 0;
        }

        public static bool IsDefaultManifestCharacterExpression(ManifestCharacterExpression nodeManifestCharacterExpression)
        {
            return nodeManifestCharacterExpression.Text.Length == 0;
        }

        public static bool IsDefaultManifestNumberExpression(ManifestNumberExpression nodeManifestNumberExpression)
        {
            return nodeManifestNumberExpression.Text.Length == 0;
        }

        public static bool IsDefaultManifestStringExpression(ManifestStringExpression nodeManifestStringExpression)
        {
            return nodeManifestStringExpression.Text.Length == 0;
        }

        public static bool IsDefaultArgument(Argument nodeArgument)
        {
            if (nodeArgument is PositionalArgument AsPositional)
                if (AsPositional.Source is QueryExpression AsQueryExpression)
                {
                    IList<Identifier> Path = AsQueryExpression.Query.Path;
                    if (Path.Count == 1 && Path[0].Text.Length == 0)
                    {
                        IBlockList ArgumentBlocks = (IBlockList)AsQueryExpression.ArgumentBlocks;
                        Debug.Assert(ArgumentBlocks != null, $"ArgumentBlocks is always a {nameof(IBlockList)}");

                        if (ArgumentBlocks == null)
                            return false;

                        if (NodeTreeHelperBlockList.IsBlockListEmpty(ArgumentBlocks))
                            return true;
                    }
                }

            return false;
        }
    }
}
