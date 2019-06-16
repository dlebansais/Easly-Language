namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        #region General
        public static bool GetComplexifiedNode(INode node, out List<INode> complexifiedNodeList)
        {
            complexifiedNodeList = new List<INode>();
            GetComplexifiedNodeRecursive(node, complexifiedNodeList);

            return complexifiedNodeList.Count > 0;
        }

        private static void GetComplexifiedNodeRecursive(INode node, List<INode> complexifiedNodeList)
        {
            int OldCount = complexifiedNodeList.Count;

            switch (node)
            {
                case IArgument AsArgument:
                    GetComplexifiedArgument(AsArgument, complexifiedNodeList);
                    break;

                case IExpression AsExpression:
                    GetComplexifiedExpression(AsExpression, complexifiedNodeList);
                    break;

                case IInstruction AsInstruction:
                    GetComplexifiedInstruction(AsInstruction, complexifiedNodeList);
                    break;

                case IObjectType AsObjectType:
                    GetComplexifiedObjectType(AsObjectType, complexifiedNodeList);
                    break;

                case ITypeArgument AsTypeArgument:
                    GetComplexifiedTypeArgument(AsTypeArgument, complexifiedNodeList);
                    break;

                case IQualifiedName AsQualifiedName:
                    GetComplexifiedQualifiedName(AsQualifiedName, complexifiedNodeList);
                    break;
            }

            int NewCount = complexifiedNodeList.Count;

            for (int i = OldCount; i < NewCount; i++)
                GetComplexifiedNodeRecursive(complexifiedNodeList[i], complexifiedNodeList);
        }
        #endregion

        #region Others
        private static void GetComplexifiedQualifiedName(IQualifiedName node, IList<INode> complexifiedNodeList)
        {
            if (ComplexifyQualifiedName(node, out IQualifiedName ComplexifiedNode))
                complexifiedNodeList.Add(ComplexifiedNode);
        }

        private static bool ComplexifyQualifiedName(IQualifiedName node, out IQualifiedName complexifiedNode)
        {
            Debug.Assert(node.Path.Count > 0);

            complexifiedNode = null;
            bool IsSplit = false;

            IList<IIdentifier> Path = new List<IIdentifier>();

            foreach (IIdentifier Item in node.Path)
            {
                string[] SplitText = Item.Text.Split('.');
                IsSplit |= SplitText.Length > 1;

                for (int i = 0; i < SplitText.Length; i++)
                {
                    IIdentifier Identifier = CreateSimpleIdentifier(SplitText[i]);
                    Path.Add(Identifier);
                }
            }

            Debug.Assert((IsSplit && Path.Count >= node.Path.Count) || (!IsSplit && Path.Count == node.Path.Count));

            if (IsSplit)
                complexifiedNode = CreateQualifiedName(Path);

            return complexifiedNode != null;
        }

        private static bool ComplexifyConditional(IConditional node, out IList<IConditional> complexifiedNodeList)
        {
            if (GetComplexifiedNode(node.BooleanExpression, out List<INode> ComplexifiedBooleanExpressionList) && IsNodeListSameType(ComplexifiedBooleanExpressionList, out IList<IExpression> ComplexifiedList))
            {
                complexifiedNodeList = new List<IConditional>();
                foreach (IExpression ComplexifiedBooleanExpression in ComplexifiedList)
                {
                    IScope ClonedInstructions = (IScope)DeepCloneNode(node.Instructions, cloneCommentGuid: false);
                    IConditional ComplexifiedNode = CreateConditional(ComplexifiedBooleanExpression, ClonedInstructions);
                    complexifiedNodeList.Add(ComplexifiedNode);
                }

                return true;
            }

            complexifiedNodeList = null;
            return false;
        }
        #endregion

        #region Tools
        private static bool GetComplexifiedIdentifierBlockList(IBlockList<IIdentifier, Identifier> identifierBlockList, out IBlockList<IIdentifier, Identifier> newBlockList)
        {
            for (int BlockIndex = 0; BlockIndex < identifierBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IIdentifier, Identifier> Block = identifierBlockList.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IIdentifier Identifier = Block.NodeList[NodeIndex];
                    if (SplitIdentifier(Identifier, out IList<IIdentifier> Split))
                    {
                        newBlockList = (IBlockList<IIdentifier, Identifier>)DeepCloneBlockList((IBlockList)identifierBlockList, cloneCommentGuid: false);

                        newBlockList.NodeBlockList[BlockIndex].NodeList.RemoveAt(NodeIndex);
                        for (int i = 0; i < Split.Count; i++)
                            newBlockList.NodeBlockList[BlockIndex].NodeList.Insert(NodeIndex + i, Split[i]);

                        return true;
                    }
                }
            }

            newBlockList = null;
            return false;
        }

        private static bool SplitIdentifier(IIdentifier identifier, out IList<IIdentifier> split)
        {
            string[] SplitText = identifier.Text.Split(',');

            if (SplitText.Length > 1)
            {
                split = new List<IIdentifier>();

                for (int i = 0; i < SplitText.Length; i++)
                {
                    IIdentifier NewIdentifier = CreateSimpleIdentifier(SplitText[i].Trim());
                    split.Add(NewIdentifier);
                }

                return true;
            }

            split = null;
            return false;
        }

        private static bool GetComplexifiedArgumentBlockList(IBlockList<IArgument, Argument> argumentBlocks, out IBlockList<IArgument, Argument> newArgumentBlocks)
        {
            for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IArgument, Argument> Block = argumentBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IArgument Argument = Block.NodeList[NodeIndex];

                    if (GetComplexifiedNode(Argument, out List<INode> ComplexifiedArgumentList) && ComplexifiedArgumentList[0] is IArgument AsComplexifiedArgument)
                    {
                        newArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = AsComplexifiedArgument;
                        return true;
                    }
                }
            }

            newArgumentBlocks = null;
            return false;
        }

        private static bool GetComplexifiedAssignmentArgumentBlockList(IBlockList<IAssignmentArgument, AssignmentArgument> argumentBlocks, out IBlockList<IAssignmentArgument, AssignmentArgument> newAssignmentArgumentBlocks)
        {
            for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IAssignmentArgument, AssignmentArgument> Block = argumentBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IAssignmentArgument AssignmentArgument = Block.NodeList[NodeIndex];

                    if (GetComplexifiedNode(AssignmentArgument, out List<INode> ComplexifiedAssignmentArgumentList) && ComplexifiedAssignmentArgumentList[0] is IAssignmentArgument AsComplexifiedAssignmentArgument)
                    {
                        newAssignmentArgumentBlocks = (IBlockList<IAssignmentArgument, AssignmentArgument>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newAssignmentArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = AsComplexifiedAssignmentArgument;
                        return true;
                    }
                }
            }

            newAssignmentArgumentBlocks = null;
            return false;
        }

        private static bool GetComplexifiedQualifiedNameBlockList(IBlockList<IQualifiedName, QualifiedName> argumentBlocks, out IBlockList<IQualifiedName, QualifiedName> newQualifiedNameBlocks)
        {
            for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IQualifiedName, QualifiedName> Block = argumentBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IQualifiedName QualifiedName = Block.NodeList[NodeIndex];

                    if (GetComplexifiedNode(QualifiedName, out List<INode> ComplexifiedQualifiedNameList) && ComplexifiedQualifiedNameList[0] is IQualifiedName AsComplexifiedQualifiedName)
                    {
                        newQualifiedNameBlocks = (IBlockList<IQualifiedName, QualifiedName>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newQualifiedNameBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = AsComplexifiedQualifiedName;
                        return true;
                    }
                }
            }

            newQualifiedNameBlocks = null;
            return false;
        }

        private static bool ComplexifyWithArguments(IQualifiedName qualifiedName, out IQualifiedName newQualifiedName, out List<IArgument> argumentList)
        {
            return ComplexifyWithArguments(qualifiedName, '(', ')', out newQualifiedName, out argumentList);
        }

        private static bool ComplexifyWithArguments(IQualifiedName qualifiedName, char leftSymbol, char rightSymbol, out IQualifiedName newQualifiedName, out List<IArgument> argumentList)
        {
            newQualifiedName = null;
            argumentList = null;

            int BreakPathIndex = -1;
            string BeforeText = null;
            string AfterText = null;

            for (int i = 0; i < qualifiedName.Path.Count; i++)
            {
                int Index = qualifiedName.Path[i].Text.IndexOf(leftSymbol);
                if (Index >= 0)
                {
                    string Text = qualifiedName.Path[i].Text;
                    BreakPathIndex = i;
                    BeforeText = Text.Substring(0, Index);
                    AfterText = Text.Substring(Index + 1);
                    break;
                }
            }

            if (BreakPathIndex >= 0 && BeforeText != null && AfterText != null)
            {
                string Text = qualifiedName.Path[qualifiedName.Path.Count - 1].Text;

                if (Text.EndsWith(rightSymbol.ToString()))
                {
                    List<IIdentifier> CommandIdentifierList = new List<IIdentifier>();
                    for (int i = 0; i < BreakPathIndex; i++)
                        CommandIdentifierList.Add(CreateSimpleIdentifier(qualifiedName.Path[i].Text));
                    CommandIdentifierList.Add(CreateSimpleIdentifier(BeforeText));

                    newQualifiedName = CreateQualifiedName(CommandIdentifierList);

                    List<IIdentifier> ArgumentIdentifierList = new List<IIdentifier>();

                    if (BreakPathIndex + 1 < qualifiedName.Path.Count)
                    {
                        ArgumentIdentifierList.Add(CreateSimpleIdentifier(AfterText));
                        for (int i = BreakPathIndex + 1; i + 1 < qualifiedName.Path.Count; i++)
                            ArgumentIdentifierList.Add(CreateSimpleIdentifier(qualifiedName.Path[i].Text));
                        ArgumentIdentifierList.Add(CreateSimpleIdentifier(Text.Substring(0, Text.Length - 1)));
                    }
                    else
                        ArgumentIdentifierList.Add(CreateSimpleIdentifier(AfterText.Substring(0, AfterText.Length - 1)));

                    IQualifiedName ArgumentQuery = CreateQualifiedName(ArgumentIdentifierList);
                    IExpression ArgumentExpression = CreateQueryExpression(ArgumentQuery, new List<IArgument>());

                    argumentList = new List<IArgument>();
                    IPositionalArgument FirstArgument = CreatePositionalArgument(ArgumentExpression);
                    argumentList.Add(FirstArgument);

                    return true;
                }
            }

            return false;
        }

        private static bool GetComplexifiedEntityDeclarationBlockList(IBlockList<IEntityDeclaration, EntityDeclaration> identifierBlockList, out IBlockList<IEntityDeclaration, EntityDeclaration> newBlockList)
        {
            for (int BlockIndex = 0; BlockIndex < identifierBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IEntityDeclaration, EntityDeclaration> Block = identifierBlockList.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IEntityDeclaration EntityDeclaration = Block.NodeList[NodeIndex];
                    if (SplitEntityDeclaration(EntityDeclaration, out IList<IEntityDeclaration> Split))
                    {
                        newBlockList = (IBlockList<IEntityDeclaration, EntityDeclaration>)DeepCloneBlockList((IBlockList)identifierBlockList, cloneCommentGuid: false);

                        newBlockList.NodeBlockList[BlockIndex].NodeList.RemoveAt(NodeIndex);
                        for (int i = 0; i < Split.Count; i++)
                            newBlockList.NodeBlockList[BlockIndex].NodeList.Insert(NodeIndex + i, Split[i]);

                        return true;
                    }
                }
            }

            newBlockList = null;
            return false;
        }

        private static bool SplitEntityDeclaration(IEntityDeclaration entityDeclaration, out IList<IEntityDeclaration> split)
        {
            if (entityDeclaration.EntityType is ISimpleType AsSimpleType && AsSimpleType.Sharing == SharingType.NotShared && !entityDeclaration.DefaultValue.IsAssigned)
            {
                int ColonIndex;
                int CommaIndex;
                IEntityDeclaration FirstEntityDeclaration = null;
                IEntityDeclaration SecondEntityDeclaration = null;

                if ((ColonIndex = entityDeclaration.EntityName.Text.IndexOf(':')) >= 0)
                {
                    string FirstName = entityDeclaration.EntityName.Text.Substring(0, ColonIndex);
                    string FirstType = entityDeclaration.EntityName.Text.Substring(ColonIndex + 1);

                    FirstEntityDeclaration = CreateEntityDeclaration(CreateSimpleName(FirstName), CreateSimpleType(SharingType.NotShared, CreateSimpleIdentifier(FirstType)));

                    IName SecondName = CreateEmptyName();
                    IObjectType SecondType = (IObjectType)DeepCloneNode(AsSimpleType, cloneCommentGuid: false);

                    SecondEntityDeclaration = CreateEntityDeclaration(SecondName, SecondType);
                }
                else if ((CommaIndex = AsSimpleType.ClassIdentifier.Text.IndexOf(',')) >= 0)
                {
                    IName FirstName = (IName)DeepCloneNode(entityDeclaration.EntityName, cloneCommentGuid: false);
                    string FirstType = AsSimpleType.ClassIdentifier.Text.Substring(0, CommaIndex);

                    FirstEntityDeclaration = CreateEntityDeclaration(FirstName, CreateSimpleType(SharingType.NotShared, CreateSimpleIdentifier(FirstType)));

                    string SecondName = AsSimpleType.ClassIdentifier.Text.Substring(CommaIndex + 1);
                    IObjectType SecondType = CreateEmptySimpleType();

                    SecondEntityDeclaration = CreateEntityDeclaration(CreateSimpleName(SecondName), SecondType);
                }

                if (FirstEntityDeclaration != null && SecondEntityDeclaration != null)
                {
                    split = new List<IEntityDeclaration>();
                    split.Add(FirstEntityDeclaration);
                    split.Add(SecondEntityDeclaration);
                    return true;
                }
            }

            split = null;
            return false;
        }

        private static bool GetComplexifiedTypeArgumentBlockList(IBlockList<ITypeArgument, TypeArgument> argumentBlocks, out IBlockList<ITypeArgument, TypeArgument> newTypeArgumentBlocks)
        {
            for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<ITypeArgument, TypeArgument> Block = argumentBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    ITypeArgument TypeArgument = Block.NodeList[NodeIndex];

                    if (GetComplexifiedNode(TypeArgument, out List<INode> ComplexifiedTypeArgumentList) && ComplexifiedTypeArgumentList[0] is ITypeArgument AsComplexifiedTypeArgument)
                    {
                        newTypeArgumentBlocks = (IBlockList<ITypeArgument, TypeArgument>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newTypeArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = AsComplexifiedTypeArgument;
                        return true;
                    }
                }
            }

            newTypeArgumentBlocks = null;
            return false;
        }

        private static bool IsQuerySimple(IQueryExpression node)
        {
            return node.Query.Path.Count == 1 && node.ArgumentBlocks.NodeBlockList.Count == 0;
        }

        private static bool ParsePattern(IQueryExpression node, string patternText, out string beforeText, out string afterText)
        {
            Debug.Assert(node.Query.Path.Count > 0);

            string Text = node.Query.Path[0].Text;
            return ParsePattern(Text, patternText, out beforeText, out afterText);
        }

        private static bool ParsePattern(ICommandInstruction node, string patternText, out string beforeText, out string afterText)
        {
            Debug.Assert(node.Command.Path.Count > 0);

            string Text = node.Command.Path[0].Text;
            return ParsePattern(Text, patternText, out beforeText, out afterText);
        }

        private static bool ParsePattern(string text, string patternText, out string beforeText, out string afterText)
        {
            beforeText = null;
            afterText = null;

            int PatternIndex = text.IndexOf(patternText);
            if (PatternIndex >= 0)
            {
                beforeText = text.Substring(0, PatternIndex);
                afterText = text.Substring(PatternIndex + patternText.Length);
            }

            return beforeText != null || afterText != null;
        }

        private static void CloneComplexifiedExpression(IQueryExpression node, string afterText, out IExpression rightExpression)
        {
            IQueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as IQueryExpression;
            Debug.Assert(ClonedQuery.Query != null);
            Debug.Assert(ClonedQuery.Query.Path.Count > 0);

            NodeTreeHelper.SetString(ClonedQuery.Query.Path[0], "Text", afterText);

            rightExpression = ClonedQuery;
        }

        private static void CloneComplexifiedExpression(IQueryExpression node, string beforeText, string afterText, out IExpression leftExpression, out IExpression rightExpression)
        {
            leftExpression = CreateSimpleQueryExpression(beforeText);

            IQueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as IQueryExpression;
            Debug.Assert(ClonedQuery.Query != null);
            Debug.Assert(ClonedQuery.Query.Path.Count > 0);

            NodeTreeHelper.SetString(ClonedQuery.Query.Path[0], "Text", afterText);

            rightExpression = ClonedQuery;
        }

        private static void CloneComplexifiedCommand(ICommandInstruction node, string afterText, out IExpression rightExpression)
        {
            ICommandInstruction ClonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as ICommandInstruction;
            Debug.Assert(ClonedCommand.Command != null);
            Debug.Assert(ClonedCommand.Command.Path.Count > 0);

            NodeTreeHelper.SetString(ClonedCommand.Command.Path[0], "Text", afterText);

            rightExpression = CreateQueryExpression(ClonedCommand.Command, ClonedCommand.ArgumentBlocks);
        }

        private static void CloneComplexifiedCommand(ICommandInstruction node, string beforeText, string afterText, out IExpression leftExpression, out IExpression rightExpression)
        {
            leftExpression = CreateSimpleQueryExpression(beforeText);

            ICommandInstruction ClonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as ICommandInstruction;
            Debug.Assert(ClonedCommand.Command != null);
            Debug.Assert(ClonedCommand.Command.Path.Count > 0);

            NodeTreeHelper.SetString(ClonedCommand.Command.Path[0], "Text", afterText);

            rightExpression = CreateQueryExpression(ClonedCommand.Command, ClonedCommand.ArgumentBlocks);
        }

        private static void CloneComplexifiedCommand(ICommandInstruction node, string pattern, out ICommandInstruction clonedCommand)
        {
            clonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as ICommandInstruction;
            Debug.Assert(clonedCommand.Command.Path.Count > 0);
            IIdentifier FirstIdentifier = clonedCommand.Command.Path[0];
            string Text = FirstIdentifier.Text;
            Debug.Assert(Text.StartsWith(pattern));

            if (Text.Length > pattern.Length || clonedCommand.Command.Path.Count == 1)
                NodeTreeHelper.SetString(FirstIdentifier, nameof(IIdentifier.Text), Text.Substring(pattern.Length));
            else
                clonedCommand.Command.Path.RemoveAt(0);
        }

        private static bool StringToKeyword(string text, out Keyword value)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Substring(0, 1).ToUpper() + text.Substring(1);

                string[] Names = typeof(Keyword).GetEnumNames();

                for (int i = 0; i < Names.Length; i++)
                {
                    if (text == Names[i])
                    {
                        value = (Keyword)i;
                        return true;
                    }
                }
            }

            value = (Keyword)(-1);
            return false;
        }

        private static bool IsNodeListSameType<T>(List<INode> nodeList, out IList<T> result)
            where T: INode
        {
            result = new List<T>();

            foreach (INode Node in nodeList)
                if (Node is T AsT)
                    result.Add(AsT);
                else
                    return false;

            return true;
        }
        #endregion
    }
}
