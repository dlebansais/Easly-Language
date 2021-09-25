namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;
    using Easly;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        /// <summary>
        /// Checks whether an optional reference is assigned to a node with the default value for its type.
        /// </summary>
        /// <param name="optional">The optional reference.</param>
        /// <returns>True if the optional reference is assigned to a node with the default value for its type; otherwise, false.</returns>
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

        /// <summary>
        /// Checks whether a node has the default value for its type.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>True if the node has the default value for its type.; otherwise, false.</returns>
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

        private static bool IsDefaultName(Name nodeName)
        {
            return nodeName.Text.Length == 0;
        }

        private static bool IsDefaultIdentifier(Identifier nodeIdentifier)
        {
            return nodeIdentifier.Text.Length == 0;
        }

        private static bool IsDefaultScope(Scope nodeScope)
        {
            return nodeScope.EntityDeclarationBlocks.NodeBlockList.Count == 0 && nodeScope.InstructionBlocks.NodeBlockList.Count == 0;
        }

        private static bool IsDefaultQualifiedName(QualifiedName nodeQualifiedName)
        {
            IList<Identifier> Path = nodeQualifiedName.Path; // Debug.Assert(Path.Count > 0);
            return Path.Count == 1 && Path[0].Text.Length == 0;
        }

        private static bool IsDefaultObjectType(ObjectType nodeObjectType)
        {
            switch (nodeObjectType)
            {
                case SimpleType AsSimpleType:
                    return IsDefaultSimpleType(AsSimpleType);

                default:
                    return false;
            }
        }

        private static bool IsDefaultSimpleType(SimpleType nodeSimpleType)
        {
            return nodeSimpleType.Sharing == SharingType.NotShared && nodeSimpleType.ClassIdentifier.Text.Length == 0;
        }

        private static bool IsDefaultBody(Body nodeBody)
        {
            switch (nodeBody)
            {
                case EffectiveBody AsEffectiveBody:
                    return IsDefaultEffectiveBody(AsEffectiveBody);

                default:
                    return false;
            }
        }

        private static bool IsDefaultEffectiveBody(EffectiveBody nodeEffectiveBody)
        {
            return nodeEffectiveBody.RequireBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.EnsureBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.ExceptionIdentifierBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.EntityDeclarationBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.BodyInstructionBlocks.NodeBlockList.Count == 0 &&
                   nodeEffectiveBody.ExceptionHandlerBlocks.NodeBlockList.Count == 0;
        }

        private static bool IsDefaultExpression(Expression nodeExpression)
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

        private static bool IsDefaultQueryExpression(QueryExpression nodeQueryExpression)
        {
            IList<Identifier> Path = nodeQueryExpression.Query.Path; // Debug.Assert(Path.Count > 0);
            return nodeQueryExpression.ArgumentBlocks.NodeBlockList.Count == 0 && Path.Count == 1 && Path[0].Text.Length == 0;
        }

        private static bool IsDefaultManifestCharacterExpression(ManifestCharacterExpression nodeManifestCharacterExpression)
        {
            return nodeManifestCharacterExpression.Text.Length == 0;
        }

        private static bool IsDefaultManifestNumberExpression(ManifestNumberExpression nodeManifestNumberExpression)
        {
            return nodeManifestNumberExpression.Text.Length == 0;
        }

        private static bool IsDefaultManifestStringExpression(ManifestStringExpression nodeManifestStringExpression)
        {
            return nodeManifestStringExpression.Text.Length == 0;
        }

        private static bool IsDefaultArgument(Argument nodeArgument)
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
