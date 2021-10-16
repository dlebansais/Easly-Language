namespace BaseNodeHelper
{
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
            if (!optional.IsAssigned)
                return false;

            Debug.Assert(optional.HasItem);
            Node Node = (Node)optional.Item;

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

                case Feature AsFeature:
                    return IsDefaultFeature(AsFeature);

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
            return nodeScope.EntityDeclarationBlocks.NodeBlockList.Count == 0 &&
                   nodeScope.InstructionBlocks.NodeBlockList.Count == 0;
        }

        private static bool IsDefaultQualifiedName(QualifiedName nodeQualifiedName)
        {
            IList<Identifier> Path = nodeQualifiedName.Path;
            Debug.Assert(Path.Count > 0);

            return Path.Count == 1 &&
                   Path[0].Text.Length == 0;
        }

        private static bool IsDefaultObjectType(ObjectType nodeObjectType)
        {
            if (nodeObjectType is not SimpleType AsSimpleType)
                return false;

            return IsDefaultSimpleType(AsSimpleType);
        }

        private static bool IsDefaultSimpleType(SimpleType nodeSimpleType)
        {
            return nodeSimpleType.Sharing == SharingType.NotShared &&
                   nodeSimpleType.ClassIdentifier.Text.Length == 0;
        }

        private static bool IsDefaultBody(Body nodeBody)
        {
            if (nodeBody is not EffectiveBody AsEffectiveBody)
                return false;

            return IsDefaultEffectiveBody(AsEffectiveBody);
        }

        private static bool IsDefaultEffectiveBody(EffectiveBody nodeEffectiveBody)
        {
            return NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)nodeEffectiveBody.RequireBlocks) &&
                   NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)nodeEffectiveBody.EnsureBlocks) &&
                   NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)nodeEffectiveBody.ExceptionIdentifierBlocks) &&
                   NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)nodeEffectiveBody.EntityDeclarationBlocks) &&
                   NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)nodeEffectiveBody.BodyInstructionBlocks) &&
                   NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)nodeEffectiveBody.ExceptionHandlerBlocks);
        }

        private static bool IsDefaultFeature(Feature nodeFeature)
        {
            if (nodeFeature is not AttributeFeature AsAttributeFeature)
                return false;

            return IsDefaultAttributeFeature(AsAttributeFeature);
        }

        private static bool IsDefaultAttributeFeature(AttributeFeature nodeAttributeFeature)
        {
            return nodeAttributeFeature.ExportIdentifier.Text == "All" &&
                   nodeAttributeFeature.Export == ExportStatus.Exported &&
                   IsDefaultName(nodeAttributeFeature.EntityName) &&
                   IsDefaultObjectType(nodeAttributeFeature.EntityType) &&
                   NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)nodeAttributeFeature.EnsureBlocks);
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
            IList<Identifier> Path = nodeQueryExpression.Query.Path;
            Debug.Assert(Path.Count > 0);

            return nodeQueryExpression.ArgumentBlocks.NodeBlockList.Count == 0 &&
                   Path.Count == 1 &&
                   Path[0].Text.Length == 0;
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
                    Debug.Assert(Path.Count > 0);

                    bool DefaultQueryPath = Path.Count == 1 && Path[0].Text.Length == 0;
                    bool DefaultQueryArguments = NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)AsQueryExpression.ArgumentBlocks);

                    if (DefaultQueryPath && DefaultQueryArguments)
                        return true;
                }

            return false;
        }
    }
}
