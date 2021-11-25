namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;
    using Contracts;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        /// <summary>
        /// Gets the simpler unicode character corresponding to a sequence of characters for a binary operator.
        /// This method does the opposite of <see cref="GetInverseRenamedBinarySymbol(string, out string)"/>.
        /// </summary>
        /// <param name="symbol">The sequence of character.</param>
        /// <param name="renamedSymbol">The unicode character upon return.</param>
        /// <returns>True if the provided sequence could be translated; otherwise, false.</returns>
        public static bool GetRenamedBinarySymbol(string symbol, out string renamedSymbol)
        {
            switch (symbol)
            {
                case ">=":
                    renamedSymbol = "≥";
                    return true;

                case "<=":
                    renamedSymbol = "≤";
                    return true;

                case "=>":
                    renamedSymbol = "⇒";
                    return true;
            }

            Contract.Unused(out renamedSymbol);
            return false;
        }

        /// <summary>
        /// Gets the sequence of characters that corresponds to a unicode character for a binary operator.
        /// This method does the opposite of <see cref="GetRenamedBinarySymbol(string, out string)"/>.
        /// </summary>
        /// <param name="symbol">The unicode character.</param>
        /// <param name="renamedSymbol">The sequence of character upon return.</param>
        /// <returns>True if the provided character could be translated; otherwise, false.</returns>
        public static bool GetInverseRenamedBinarySymbol(string symbol, out string renamedSymbol)
        {
            switch (symbol)
            {
                case "≥":
                    renamedSymbol = ">=";
                    return true;

                case "≤":
                    renamedSymbol = "<=";
                    return true;

                case "⇒":
                    renamedSymbol = "=>";
                    return true;
            }

            Contract.Unused(out renamedSymbol);
            return false;
        }

        private static bool GetRenamedBinarySymbol(Identifier symbol, out Identifier renamedSymbol)
        {
            if (GetRenamedBinarySymbol(symbol.Text, out string renamedSymbolText))
            {
                renamedSymbol = CreateSimpleIdentifier(renamedSymbolText);
                return true;
            }

            Contract.Unused(out renamedSymbol);
            return false;
        }

        /// <summary>
        /// Gets the simpler unicode character corresponding to a sequence of characters for a unary operator.
        /// This method does the opposite of <see cref="GetInverseRenamedUnarySymbol(string, out string)"/>.
        /// </summary>
        /// <param name="symbol">The sequence of character.</param>
        /// <param name="renamedSymbol">The unicode character upon return.</param>
        /// <returns>True if the provided sequence could be translated; otherwise, false.</returns>
        public static bool GetRenamedUnarySymbol(string symbol, out string renamedSymbol)
        {
            switch (symbol)
            {
                case "sqrt":
                    renamedSymbol = "√";
                    return true;
            }

            Contract.Unused(out renamedSymbol);
            return false;
        }

        /// <summary>
        /// Gets the sequence of characters that corresponds to a unicode character for a unary operator.
        /// This method does the opposite of <see cref="GetRenamedUnarySymbol(string, out string)"/>.
        /// </summary>
        /// <param name="symbol">The unicode character.</param>
        /// <param name="renamedSymbol">The sequence of character upon return.</param>
        /// <returns>True if the provided character could be translated; otherwise, false.</returns>
        public static bool GetInverseRenamedUnarySymbol(string symbol, out string renamedSymbol)
        {
            switch (symbol)
            {
                case "√":
                    renamedSymbol = "sqrt";
                    return true;
            }

            Contract.Unused(out renamedSymbol);
            return false;
        }

        private static bool GetRenamedUnarySymbol(Identifier symbol, out Identifier renamedSymbol)
        {
            if (GetRenamedUnarySymbol(symbol.Text, out string renamedSymbolText))
            {
                renamedSymbol = CreateSimpleIdentifier(renamedSymbolText);
                return true;
            }

            Contract.Unused(out renamedSymbol);
            return false;
        }

        private static bool SplitIdentifier(Identifier identifier, char startTag, char endTag, out IList<Identifier> split)
        {
            IList<string> SplitList = SplitString(identifier.Text, startTag, endTag);

            if (SplitList.Count > 1)
            {
                split = new List<Identifier>();

                foreach (string Item in SplitList)
                {
                    Identifier NewIdentifier = CreateSimpleIdentifier(Item.Trim());
                    split.Add(NewIdentifier);
                }

                return true;
            }

            Contract.Unused(out split);
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

        private static bool SplitArgument(Argument argument, out IList<Argument> split)
        {
            if (argument is PositionalArgument AsPositionalArgument && AsPositionalArgument.Source is QueryExpression AsQueryExpression && IsQuerySimple(AsQueryExpression))
            {
                Identifier QueryIdentifier = AsQueryExpression.Query.Path[0];
                if (SplitIdentifier(QueryIdentifier, '(', ')', out IList<Identifier> SplitIdentifierList))
                {
                    split = new List<Argument>();

                    foreach (Identifier Item in SplitIdentifierList)
                    {
                        Argument NewArgument = CreateSimplePositionalArgument(Item.Text);
                        split.Add(NewArgument);
                    }

                    return true;
                }
            }

            Contract.Unused(out split);
            return false;
        }

        private static bool ComplexifyWithArguments(QualifiedName qualifiedName, out QualifiedName newQualifiedName, out List<Argument> argumentList)
        {
            return ComplexifyWithArguments(qualifiedName, '(', ')', out newQualifiedName, out argumentList);
        }

        private static bool ComplexifyWithArguments(QualifiedName qualifiedName, char leftSymbol, char rightSymbol, out QualifiedName newQualifiedName, out List<Argument> argumentList)
        {
            int BreakPathIndex = -1;
            string BeforeText = String.Empty;
            string AfterText = String.Empty;

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

            if (BreakPathIndex >= 0)
            {
                string Text = qualifiedName.Path[qualifiedName.Path.Count - 1].Text;

                if (Text.EndsWith(rightSymbol.ToString(), StringComparison.InvariantCulture))
                {
                    List<Identifier> CommandIdentifierList = new List<Identifier>();
                    for (int i = 0; i < BreakPathIndex; i++)
                        CommandIdentifierList.Add(CreateSimpleIdentifier(qualifiedName.Path[i].Text));
                    CommandIdentifierList.Add(CreateSimpleIdentifier(BeforeText));

                    newQualifiedName = CreateQualifiedName(CommandIdentifierList);

                    List<Identifier> ArgumentIdentifierList = new List<Identifier>();

                    if (BreakPathIndex + 1 < qualifiedName.Path.Count)
                    {
                        ArgumentIdentifierList.Add(CreateSimpleIdentifier(AfterText));
                        for (int i = BreakPathIndex + 1; i + 1 < qualifiedName.Path.Count; i++)
                            ArgumentIdentifierList.Add(CreateSimpleIdentifier(qualifiedName.Path[i].Text));
                        ArgumentIdentifierList.Add(CreateSimpleIdentifier(Text.Substring(0, Text.Length - 1)));
                    }
                    else
                        ArgumentIdentifierList.Add(CreateSimpleIdentifier(AfterText.Substring(0, AfterText.Length - 1)));

                    QualifiedName ArgumentQuery = CreateQualifiedName(ArgumentIdentifierList);
                    Expression ArgumentExpression = CreateQueryExpression(ArgumentQuery, new List<Argument>());

                    argumentList = new List<Argument>();
                    PositionalArgument FirstArgument = CreatePositionalArgument(ArgumentExpression);
                    argumentList.Add(FirstArgument);

                    return true;
                }
            }

            Contract.Unused(out newQualifiedName);
            Contract.Unused(out argumentList);
            return false;
        }

        private static bool SplitEntityDeclaration(EntityDeclaration entityDeclaration, out IList<EntityDeclaration> split)
        {
            if (entityDeclaration.EntityType is SimpleType AsSimpleType && AsSimpleType.Sharing == SharingType.NotShared && !entityDeclaration.DefaultValue.IsAssigned)
            {
                int ColonIndex;
                int CommaIndex;
                EntityDeclaration FirstEntityDeclaration;
                EntityDeclaration SecondEntityDeclaration;

                if ((ColonIndex = entityDeclaration.EntityName.Text.IndexOf(":", StringComparison.InvariantCulture)) >= 0)
                {
                    string FirstName = entityDeclaration.EntityName.Text.Substring(0, ColonIndex);
                    string FirstType = entityDeclaration.EntityName.Text.Substring(ColonIndex + 1);

                    FirstEntityDeclaration = CreateEntityDeclaration(CreateSimpleName(FirstName), CreateSimpleType(SharingType.NotShared, CreateSimpleIdentifier(FirstType)));

                    Name SecondName = CreateEmptyName();
                    ObjectType SecondType = (ObjectType)DeepCloneNode(AsSimpleType, cloneCommentGuid: false);

                    SecondEntityDeclaration = CreateEntityDeclaration(SecondName, SecondType);
                }
                else if ((CommaIndex = AsSimpleType.ClassIdentifier.Text.IndexOf(",", StringComparison.InvariantCulture)) >= 0)
                {
                    Name FirstName = (Name)DeepCloneNode(entityDeclaration.EntityName, cloneCommentGuid: false);
                    string FirstType = AsSimpleType.ClassIdentifier.Text.Substring(0, CommaIndex);

                    FirstEntityDeclaration = CreateEntityDeclaration(FirstName, CreateSimpleType(SharingType.NotShared, CreateSimpleIdentifier(FirstType)));

                    string SecondName = AsSimpleType.ClassIdentifier.Text.Substring(CommaIndex + 1);
                    ObjectType SecondType = CreateEmptySimpleType();

                    SecondEntityDeclaration = CreateEntityDeclaration(CreateSimpleName(SecondName), SecondType);
                }
                else
                {
                    Contract.Unused(out split);
                    return false;
                }

                split = new List<EntityDeclaration>();
                split.Add(FirstEntityDeclaration);
                split.Add(SecondEntityDeclaration);
                return true;
            }

            Contract.Unused(out split);
            return false;
        }

        private static bool SplitTypeArgument(TypeArgument typeArgument, out IList<TypeArgument> split)
        {
            if (typeArgument is PositionalTypeArgument AsPositionalTypeArgument && AsPositionalTypeArgument.Source is SimpleType AsSimpleType)
            {
                Identifier ClassIdentifier = AsSimpleType.ClassIdentifier;
                if (SplitIdentifier(ClassIdentifier, '[', ']', out IList<Identifier> SplitIdentifierList))
                {
                    split = new List<TypeArgument>();

                    foreach (Identifier Item in SplitIdentifierList)
                    {
                        TypeArgument NewTypeArgument = CreateSimplePositionalTypeArgument(Item.Text);
                        split.Add(NewTypeArgument);
                    }

                    return true;
                }
            }

            Contract.Unused(out split);
            return false;
        }

        private static bool SplitObjectType(ObjectType objectType, out IList<ObjectType> split)
        {
            if (objectType is SimpleType AsSimpleType)
            {
                Identifier ClassIdentifier = AsSimpleType.ClassIdentifier;
                if (SplitIdentifier(ClassIdentifier, ',', ',', out IList<Identifier> SplitIdentifierList))
                {
                    split = new List<ObjectType>();

                    foreach (Identifier Item in SplitIdentifierList)
                    {
                        ObjectType NewObjectType = CreateSimpleSimpleType(Item.Text);
                        split.Add(NewObjectType);
                    }

                    return true;
                }
            }

            Contract.Unused(out split);
            return false;
        }

        private static bool IsQuerySimple(QueryExpression node)
        {
            return node.Query.Path.Count == 1 && node.ArgumentBlocks.NodeBlockList.Count == 0;
        }

        private static bool ParsePattern(QueryExpression node, string patternText, out string beforeText, out string afterText)
        {
            Debug.Assert(node.Query.Path.Count > 0, $"The parsed {nameof(node)} always has a valid path");

            string Text = node.Query.Path[0].Text;
            return ParsePattern(Text, patternText, out beforeText, out afterText);
        }

        private static bool ParsePattern(CommandInstruction node, string patternText, out string beforeText, out string afterText)
        {
            Debug.Assert(node.Command.Path.Count > 0, $"The parsed {nameof(node)} always has a valid path");

            string Text = node.Command.Path[0].Text;
            return ParsePattern(Text, patternText, out beforeText, out afterText);
        }

        private static bool ParsePattern(string text, string patternText, out string beforeText, out string afterText)
        {
            int PatternIndex = text.IndexOf(patternText, StringComparison.InvariantCulture);
            if (PatternIndex >= 0)
            {
                beforeText = text.Substring(0, PatternIndex).Trim();
                afterText = text.Substring(PatternIndex + patternText.Length).Trim();
                return true;
            }

            Contract.Unused(out beforeText);
            Contract.Unused(out afterText);
            return false;
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
    }
}
