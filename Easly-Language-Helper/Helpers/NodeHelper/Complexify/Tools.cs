namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static partial class NodeHelper
    {
        public static bool GetRenamedBinarySymbol(string symbol, out string renamedSymbol)
        {
            renamedSymbol = null;

            switch (symbol)
            {
                case ">=":
                    renamedSymbol = "≥";
                    break;

                case "<=":
                    renamedSymbol = "≤";
                    break;

                case "=>":
                    renamedSymbol = "⇒";
                    break;
            }

            return renamedSymbol != null;
        }

        public static bool GetInverseRenamedBinarySymbol(string symbol, out string renamedSymbol)
        {
            renamedSymbol = null;

            switch (symbol)
            {
                case "≥":
                    renamedSymbol = ">=";
                    break;

                case "≤":
                    renamedSymbol = "<=";
                    break;

                case "⇒":
                    renamedSymbol = "=>";
                    break;
            }

            return renamedSymbol != null;
        }

        public static bool GetRenamedUnarySymbol(string symbol, out string renamedSymbol)
        {
            renamedSymbol = null;

            switch (symbol)
            {
                case "sqrt":
                    renamedSymbol = "√";
                    break;
            }

            return renamedSymbol != null;
        }

        public static bool GetInverseRenamedUnarySymbol(string symbol, out string renamedSymbol)
        {
            renamedSymbol = null;

            switch (symbol)
            {
                case "√":
                    renamedSymbol = "sqrt";
                    break;
            }

            return renamedSymbol != null;
        }

        private static bool GetComplexifiedIdentifierBlockList(IBlockList<IIdentifier, Identifier> identifierBlockList, out IBlockList<IIdentifier, Identifier> newBlockList)
        {
            for (int BlockIndex = 0; BlockIndex < identifierBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IIdentifier, Identifier> Block = identifierBlockList.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IIdentifier Identifier = Block.NodeList[NodeIndex];
                    if (SplitIdentifier(Identifier, ',', ',', out IList<IIdentifier> Split))
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

        private static bool SplitIdentifier(IIdentifier identifier, char startTag, char endTag, out IList<IIdentifier> split)
        {
            IList<string> SplitList = SplitString(identifier.Text, startTag, endTag);

            if (SplitList.Count > 1)
            {
                split = new List<IIdentifier>();

                foreach (string Item in SplitList)
                {
                    IIdentifier NewIdentifier = CreateSimpleIdentifier(Item.Trim());
                    split.Add(NewIdentifier);
                }

                return true;
            }

            split = null;
            return false;
        }

        private static bool GetComplexifiedNameBlockList(IBlockList<IName, Name> nameBlockList, out IBlockList<IName, Name> newBlockList)
        {
            for (int BlockIndex = 0; BlockIndex < nameBlockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IName, Name> Block = nameBlockList.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IName Name = Block.NodeList[NodeIndex];
                    if (SplitName(Name, ',', ',', out IList<IName> Split))
                    {
                        newBlockList = (IBlockList<IName, Name>)DeepCloneBlockList((IBlockList)nameBlockList, cloneCommentGuid: false);

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

        private static bool SplitName(IName name, char startTag, char endTag, out IList<IName> split)
        {
            IList<string> SplitList = SplitString(name.Text, startTag, endTag);

            if (SplitList.Count > 1)
            {
                split = new List<IName>();

                foreach (string Item in SplitList)
                {
                    IName NewName = CreateSimpleName(Item.Trim());
                    split.Add(NewName);
                }

                return true;
            }

            split = null;
            return false;
        }

        private static IList<string> SplitString(string text, char startTag, char endTag)
        {
            IList<string> SplitList = new List<string>();

            int StartIndex = 0;
            int AtomCount = 0;
            int i = 0;

            while (i < text.Length)
            {
                char c = text[i];

                if (c == ',')
                {
                    if (AtomCount == 0)
                    {
                        SplitList.Add(text.Substring(StartIndex, i - StartIndex));
                        StartIndex = i + 1;
                    }
                }
                else if (c == startTag)
                    AtomCount++;
                else if (c == endTag && AtomCount > 0)
                    AtomCount--;

                i++;
            }

            if (StartIndex < i || text.Length == 0)
                SplitList.Add(text.Substring(StartIndex, i - StartIndex));

            return SplitList;
        }

        private static bool GetComplexifiedArgumentBlockList(IBlockList<IArgument, Argument> argumentBlocks, out IBlockList<IArgument, Argument> newArgumentBlocks)
        {
            for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IArgument, Argument> Block = argumentBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IArgument Argument = Block.NodeList[NodeIndex];

                    if (SplitArgument(Argument, out IList<IArgument> SplitArgumentList))
                    {
                        newArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList.RemoveAt(NodeIndex);

                        for (int i = 0; i < SplitArgumentList.Count; i++)
                            Block.NodeList.Insert(NodeIndex + i, SplitArgumentList[i]);
                        return true;
                    }
                }
            }

            for (int BlockIndex = 0; BlockIndex < argumentBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IArgument, Argument> Block = argumentBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IArgument Argument = Block.NodeList[NodeIndex];

                    if (GetComplexifiedArgument(Argument, out IList<IArgument> ComplexifiedArgumentList))
                    {
                        IArgument ComplexifiedArgument = ComplexifiedArgumentList[0];
                        newArgumentBlocks = (IBlockList<IArgument, Argument>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = ComplexifiedArgument;
                        return true;
                    }
                }
            }

            newArgumentBlocks = null;
            return false;
        }

        private static bool SplitArgument(IArgument argument, out IList<IArgument> split)
        {
            if (argument is IPositionalArgument AsPositionalArgument && AsPositionalArgument.Source is IQueryExpression AsQueryExpression && IsQuerySimple(AsQueryExpression))
            {
                IIdentifier QueryIdentifier = AsQueryExpression.Query.Path[0];
                if (SplitIdentifier(QueryIdentifier, '(', ')', out IList<IIdentifier> SplitIdentifierList))
                {
                    split = new List<IArgument>();

                    foreach (IIdentifier Item in SplitIdentifierList)
                    {
                        IArgument NewArgument = CreateSimplePositionalArgument(Item.Text);
                        split.Add(NewArgument);
                    }

                    return true;
                }
            }

            split = null;
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

                    if (GetComplexifiedAssignmentArgument(AssignmentArgument, out IList<IArgument> ComplexifiedAssignmentArgumentList))
                    {
                        IAssignmentArgument ComplexifiedAssignmentArgument = ComplexifiedAssignmentArgumentList[0] as IAssignmentArgument;
                        Debug.Assert(ComplexifiedAssignmentArgument != null, $"The list can't contain anything else than {nameof(IAssignmentArgument)} elements");

                        newAssignmentArgumentBlocks = (IBlockList<IAssignmentArgument, AssignmentArgument>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newAssignmentArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = ComplexifiedAssignmentArgument;
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

                    if (GetComplexifiedQualifiedName(QualifiedName, out IList<IQualifiedName> ComplexifiedQualifiedNameList))
                    {
                        IQualifiedName ComplexifiedQualifiedName = ComplexifiedQualifiedNameList[0];
                        newQualifiedNameBlocks = (IBlockList<IQualifiedName, QualifiedName>)DeepCloneBlockList((IBlockList)argumentBlocks, cloneCommentGuid: false);

                        Block = newQualifiedNameBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = ComplexifiedQualifiedName;
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
                int Index = qualifiedName.Path[i].Text.IndexOf(leftSymbol.ToString(), StringComparison.InvariantCulture);
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

                if (Text.EndsWith(rightSymbol.ToString(), StringComparison.InvariantCulture))
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

                if ((ColonIndex = entityDeclaration.EntityName.Text.IndexOf(":", StringComparison.InvariantCulture)) >= 0)
                {
                    string FirstName = entityDeclaration.EntityName.Text.Substring(0, ColonIndex);
                    string FirstType = entityDeclaration.EntityName.Text.Substring(ColonIndex + 1);

                    FirstEntityDeclaration = CreateEntityDeclaration(CreateSimpleName(FirstName), CreateSimpleType(SharingType.NotShared, CreateSimpleIdentifier(FirstType)));

                    IName SecondName = CreateEmptyName();
                    IObjectType SecondType = (IObjectType)DeepCloneNode(AsSimpleType, cloneCommentGuid: false);

                    SecondEntityDeclaration = CreateEntityDeclaration(SecondName, SecondType);
                }
                else if ((CommaIndex = AsSimpleType.ClassIdentifier.Text.IndexOf(",", StringComparison.InvariantCulture)) >= 0)
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

        private static bool GetComplexifiedTypeArgumentBlockList(IBlockList<ITypeArgument, TypeArgument> typeArgumentBlocks, out IBlockList<ITypeArgument, TypeArgument> newTypeArgumentBlocks)
        {
            for (int BlockIndex = 0; BlockIndex < typeArgumentBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<ITypeArgument, TypeArgument> Block = typeArgumentBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    ITypeArgument TypeArgument = Block.NodeList[NodeIndex];

                    if (SplitTypeArgument(TypeArgument, out IList<ITypeArgument> SplitTypeArgumentList))
                    {
                        newTypeArgumentBlocks = (IBlockList<ITypeArgument, TypeArgument>)DeepCloneBlockList((IBlockList)typeArgumentBlocks, cloneCommentGuid: false);

                        Block = newTypeArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList.RemoveAt(NodeIndex);

                        for (int i = 0; i < SplitTypeArgumentList.Count; i++)
                            Block.NodeList.Insert(NodeIndex + i, SplitTypeArgumentList[i]);
                        return true;
                    }
                }
            }

            for (int BlockIndex = 0; BlockIndex < typeArgumentBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<ITypeArgument, TypeArgument> Block = typeArgumentBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    ITypeArgument TypeArgument = Block.NodeList[NodeIndex];

                    if (GetComplexifiedTypeArgument(TypeArgument, out IList<ITypeArgument> ComplexifiedTypeArgumentList))
                    {
                        ITypeArgument ComplexifiedTypeArgument = ComplexifiedTypeArgumentList[0];
                        newTypeArgumentBlocks = (IBlockList<ITypeArgument, TypeArgument>)DeepCloneBlockList((IBlockList)typeArgumentBlocks, cloneCommentGuid: false);

                        Block = newTypeArgumentBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = ComplexifiedTypeArgument;
                        return true;
                    }
                }
            }

            newTypeArgumentBlocks = null;
            return false;
        }

        private static bool SplitTypeArgument(ITypeArgument typeArgument, out IList<ITypeArgument> split)
        {
            if (typeArgument is IPositionalTypeArgument AsPositionalTypeArgument && AsPositionalTypeArgument.Source is ISimpleType AsSimpleType)
            {
                IIdentifier ClassIdentifier = AsSimpleType.ClassIdentifier;
                if (SplitIdentifier(ClassIdentifier, '[', ']', out IList<IIdentifier> SplitIdentifierList))
                {
                    split = new List<ITypeArgument>();

                    foreach (IIdentifier Item in SplitIdentifierList)
                    {
                        ITypeArgument NewTypeArgument = CreateSimplePositionalTypeArgument(Item.Text);
                        split.Add(NewTypeArgument);
                    }

                    return true;
                }
            }

            split = null;
            return false;
        }

        private static bool GetComplexifiedObjectTypeBlockList(IBlockList<IObjectType, ObjectType> objectTypeBlocks, out IBlockList<IObjectType, ObjectType> newObjectTypeBlocks)
        {
            for (int BlockIndex = 0; BlockIndex < objectTypeBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IObjectType, ObjectType> Block = objectTypeBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IObjectType ObjectType = Block.NodeList[NodeIndex];

                    if (SplitObjectType(ObjectType, out IList<IObjectType> SplitObjectTypeList))
                    {
                        newObjectTypeBlocks = (IBlockList<IObjectType, ObjectType>)DeepCloneBlockList((IBlockList)objectTypeBlocks, cloneCommentGuid: false);

                        Block = newObjectTypeBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList.RemoveAt(NodeIndex);

                        for (int i = 0; i < SplitObjectTypeList.Count; i++)
                            Block.NodeList.Insert(NodeIndex + i, SplitObjectTypeList[i]);
                        return true;
                    }
                }
            }

            for (int BlockIndex = 0; BlockIndex < objectTypeBlocks.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IObjectType, ObjectType> Block = objectTypeBlocks.NodeBlockList[BlockIndex];

                for (int NodeIndex = 0; NodeIndex < Block.NodeList.Count; NodeIndex++)
                {
                    IObjectType ObjectType = Block.NodeList[NodeIndex];

                    if (GetComplexifiedObjectType(ObjectType, out IList<IObjectType> ComplexifiedObjectTypeList))
                    {
                        IObjectType ComplexifiedObjectType = ComplexifiedObjectTypeList[0];
                        newObjectTypeBlocks = (IBlockList<IObjectType, ObjectType>)DeepCloneBlockList((IBlockList)objectTypeBlocks, cloneCommentGuid: false);

                        Block = newObjectTypeBlocks.NodeBlockList[BlockIndex];
                        Block.NodeList[NodeIndex] = ComplexifiedObjectType;
                        return true;
                    }
                }
            }

            newObjectTypeBlocks = null;
            return false;
        }

        private static bool SplitObjectType(IObjectType objectType, out IList<IObjectType> split)
        {
            if (objectType is ISimpleType AsSimpleType)
            {
                IIdentifier ClassIdentifier = AsSimpleType.ClassIdentifier;
                if (SplitIdentifier(ClassIdentifier, ',', ',', out IList<IIdentifier> SplitIdentifierList))
                {
                    split = new List<IObjectType>();

                    foreach (IIdentifier Item in SplitIdentifierList)
                    {
                        IObjectType NewObjectType = CreateSimpleSimpleType(Item.Text);
                        split.Add(NewObjectType);
                    }

                    return true;
                }
            }

            split = null;
            return false;
        }

        private static bool IsQuerySimple(IQueryExpression node)
        {
            return node.Query.Path.Count == 1 && node.ArgumentBlocks.NodeBlockList.Count == 0;
        }

        private static bool ParsePattern(IQueryExpression node, string patternText, out string beforeText, out string afterText)
        {
            Debug.Assert(node.Query.Path.Count > 0, "The parsed node has a valid path");

            string Text = node.Query.Path[0].Text;
            return ParsePattern(Text, patternText, out beforeText, out afterText);
        }

        private static bool ParsePattern(ICommandInstruction node, string patternText, out string beforeText, out string afterText)
        {
            Debug.Assert(node.Command.Path.Count > 0, "The parsed node has a valid path");

            string Text = node.Command.Path[0].Text;
            return ParsePattern(Text, patternText, out beforeText, out afterText);
        }

        private static bool ParsePattern(string text, string patternText, out string beforeText, out string afterText)
        {
            beforeText = null;
            afterText = null;

            int PatternIndex = text.IndexOf(patternText, StringComparison.InvariantCulture);
            if (PatternIndex >= 0)
            {
                beforeText = text.Substring(0, PatternIndex).Trim();
                afterText = text.Substring(PatternIndex + patternText.Length).Trim();
            }

            return beforeText != null || afterText != null;
        }

        private static void CloneComplexifiedExpression(IQueryExpression node, string afterText, out IExpression rightExpression)
        {
            IQueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as IQueryExpression;
            Debug.Assert(ClonedQuery.Query != null, $"The clone always contains a {nameof(IQueryExpression.Query)}");
            Debug.Assert(ClonedQuery.Query.Path.Count > 0, "The clone query path is always valid");

            NodeTreeHelper.SetString(ClonedQuery.Query.Path[0], "Text", afterText);

            rightExpression = ClonedQuery;
        }

        private static void CloneComplexifiedExpression(IQueryExpression node, string beforeText, string afterText, out IExpression leftExpression, out IExpression rightExpression)
        {
            leftExpression = CreateSimpleQueryExpression(beforeText);

            IQueryExpression ClonedQuery = DeepCloneNode(node, cloneCommentGuid: false) as IQueryExpression;
            Debug.Assert(ClonedQuery.Query != null, $"The clone always contains a {nameof(IQueryExpression.Query)}");
            Debug.Assert(ClonedQuery.Query.Path.Count > 0, "The clone query path is always valid");

            NodeTreeHelper.SetString(ClonedQuery.Query.Path[0], "Text", afterText);

            rightExpression = ClonedQuery;
        }

        private static void CloneComplexifiedCommand(ICommandInstruction node, string afterText, out IExpression rightExpression)
        {
            ICommandInstruction ClonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as ICommandInstruction;
            Debug.Assert(ClonedCommand.Command != null, $"The clone always contains a {nameof(ICommandInstruction.Command)}");
            Debug.Assert(ClonedCommand.Command.Path.Count > 0, "The clone command path is always valid");

            NodeTreeHelper.SetString(ClonedCommand.Command.Path[0], "Text", afterText);

            rightExpression = CreateQueryExpression(ClonedCommand.Command, ClonedCommand.ArgumentBlocks);
        }

        private static void CloneComplexifiedCommand(ICommandInstruction node, string beforeText, string afterText, out IExpression leftExpression, out IExpression rightExpression)
        {
            leftExpression = CreateSimpleQueryExpression(beforeText);

            ICommandInstruction ClonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as ICommandInstruction;
            Debug.Assert(ClonedCommand.Command != null, $"The clone always contains a {nameof(ICommandInstruction.Command)}");
            Debug.Assert(ClonedCommand.Command.Path.Count > 0, "The clone command path is always valid");

            NodeTreeHelper.SetString(ClonedCommand.Command.Path[0], "Text", afterText);

            rightExpression = CreateQueryExpression(ClonedCommand.Command, ClonedCommand.ArgumentBlocks);
        }

        private static void CloneComplexifiedCommand(ICommandInstruction node, string pattern, out ICommandInstruction clonedCommand)
        {
            clonedCommand = DeepCloneNode(node, cloneCommentGuid: false) as ICommandInstruction;
            Debug.Assert(clonedCommand.Command.Path.Count > 0, "The clone command path is always valid");
            IIdentifier FirstIdentifier = clonedCommand.Command.Path[0];
            string Text = FirstIdentifier.Text;
            Debug.Assert(Text.StartsWith(pattern, StringComparison.InvariantCulture), "The first element in the clone command path is always unchanged");

            if (Text.Length > pattern.Length || clonedCommand.Command.Path.Count == 1)
                NodeTreeHelper.SetString(FirstIdentifier, nameof(IIdentifier.Text), Text.Substring(pattern.Length));
            else
                clonedCommand.Command.Path.RemoveAt(0);
        }

        private static bool StringToKeyword(string text, out Keyword value)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Substring(0, 1).ToUpperInvariant() + text.Substring(1);

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

        private static bool IsNodeListSameType<T>(IList nodeList, out IList<T> result)
            where T : INode
        {
            result = new List<T>();

            foreach (object Node in nodeList)
                if (Node is T AsT)
                    result.Add(AsT);
                else
                    return false;

            return true;
        }

        private static bool GetRenamedBinarySymbol(IIdentifier symbol, out IIdentifier renamedSymbol)
        {
            renamedSymbol = null;
            bool Result = false;

            if (GetRenamedBinarySymbol(symbol.Text, out string renamedSymbolText))
            {
                renamedSymbol = CreateSimpleIdentifier(renamedSymbolText);
                Result = true;
            }

            return Result;
        }

        private static bool GetRenamedUnarySymbol(IIdentifier symbol, out IIdentifier renamedSymbol)
        {
            renamedSymbol = null;
            bool Result = false;

            if (GetRenamedUnarySymbol(symbol.Text, out string renamedSymbolText))
            {
                renamedSymbol = CreateSimpleIdentifier(renamedSymbolText);
                Result = true;
            }

            return Result;
        }
    }
}
