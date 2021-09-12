#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented

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
        public static Attachment CreateAttachment(ObjectType attachType)
        {
            Attachment Result = new Attachment();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AttachTypeBlocks = BlockListHelper<ObjectType>.CreateSimpleBlockList(attachType);
            Result.Instructions = CreateEmptyScope();

            return Result;
        }

        public static Attachment CreateAttachment(BlockList<ObjectType> attachTypeBlocks, Scope instructions)
        {
            Attachment Result = new Attachment();
            Result.Documentation = CreateEmptyDocumentation();
            Result.AttachTypeBlocks = attachTypeBlocks;
            Result.Instructions = instructions;

            return Result;
        }

        public static Conditional CreateConditional(Expression booleanExpression)
        {
            Conditional SimpleConditional = new Conditional();
            SimpleConditional.Documentation = CreateEmptyDocumentation();
            SimpleConditional.BooleanExpression = booleanExpression;
            SimpleConditional.Instructions = CreateEmptyScope();

            return SimpleConditional;
        }

        public static Conditional CreateConditional(Expression booleanExpression, Instruction instruction)
        {
            Conditional SimpleConditional = new Conditional();
            SimpleConditional.Documentation = CreateEmptyDocumentation();
            SimpleConditional.BooleanExpression = booleanExpression;
            SimpleConditional.Instructions = CreateSimpleScope(instruction);

            return SimpleConditional;
        }

        public static Conditional CreateConditional(Expression booleanExpression, Scope instructions)
        {
            Conditional SimpleConditional = new Conditional();
            SimpleConditional.Documentation = CreateEmptyDocumentation();
            SimpleConditional.BooleanExpression = booleanExpression;
            SimpleConditional.Instructions = instructions;

            return SimpleConditional;
        }

        public static With CreateWith(Expression firstExpression)
        {
            BaseNode.Range FirstRange = CreateRange(firstExpression);

            With Result = new With();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RangeBlocks = BlockListHelper<BaseNode.Range>.CreateSimpleBlockList(FirstRange);
            Result.Instructions = CreateEmptyScope();

            return Result;
        }

        public static With CreateWith(Expression firstExpression, Instruction instruction)
        {
            BaseNode.Range FirstRange = CreateRange(firstExpression);

            With Result = new With();
            Result.Documentation = CreateEmptyDocumentation();
            Result.RangeBlocks = BlockListHelper<BaseNode.Range>.CreateSimpleBlockList(FirstRange);
            Result.Instructions = CreateSimpleScope(instruction);

            return Result;
        }

        public static BaseNode.Range CreateRange(Expression leftExpression)
        {
            BaseNode.Range Result = new BaseNode.Range();
            Result.Documentation = CreateEmptyDocumentation();
            Result.LeftExpression = leftExpression;
            Result.RightExpression = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());

            return Result;
        }

        public static EntityDeclaration CreateEntityDeclaration(Name entityName, ObjectType entityType)
        {
            EntityDeclaration SimpleEntityDeclaration = new EntityDeclaration();
            SimpleEntityDeclaration.Documentation = CreateEmptyDocumentation();
            SimpleEntityDeclaration.EntityName = entityName;
            SimpleEntityDeclaration.EntityType = entityType;
            SimpleEntityDeclaration.DefaultValue = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());

            return SimpleEntityDeclaration;
        }

        public static Identifier CreateEmptyExportIdentifier()
        {
            return CreateSimpleIdentifier("All");
        }

        public static Export CreateSimpleExport(string nameText)
        {
            Export SimpleExport = new Export();
            SimpleExport.Documentation = CreateEmptyDocumentation();
            SimpleExport.EntityName = CreateSimpleName(nameText);
            SimpleExport.ClassIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

            return SimpleExport;
        }

        public static Class CreateSimpleClass(string nameText)
        {
            Guid ClassGuid = Guid.NewGuid();

            Class SimpleClass = new Class();
            SimpleClass.Documentation = CreateEmptyDocumentation();
            SimpleClass.EntityName = CreateSimpleName(nameText);
            SimpleClass.FromIdentifier = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
            SimpleClass.CopySpecification = CopySemantic.Reference;
            SimpleClass.Cloneable = CloneableStatus.Cloneable;
            SimpleClass.Comparable = ComparableStatus.Comparable;
            SimpleClass.IsAbstract = false;
            SimpleClass.ImportBlocks = BlockListHelper<Import>.CreateEmptyBlockList();
            SimpleClass.GenericBlocks = BlockListHelper<Generic>.CreateEmptyBlockList();
            SimpleClass.ExportBlocks = BlockListHelper<Export>.CreateEmptyBlockList();
            SimpleClass.TypedefBlocks = BlockListHelper<Typedef>.CreateEmptyBlockList();
            SimpleClass.InheritanceBlocks = BlockListHelper<Inheritance>.CreateEmptyBlockList();
            SimpleClass.DiscreteBlocks = BlockListHelper<Discrete>.CreateEmptyBlockList();
            SimpleClass.ClassReplicateBlocks = BlockListHelper<ClassReplicate>.CreateEmptyBlockList();
            SimpleClass.FeatureBlocks = BlockListHelper<Feature>.CreateEmptyBlockList();
            SimpleClass.ConversionBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
            SimpleClass.InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
            SimpleClass.ClassGuid = ClassGuid;
            SimpleClass.ClassPath = string.Empty;

            return SimpleClass;
        }

        public static Library CreateSimpleLibrary(string nameText)
        {
            Library SimpleLibrary = new Library();
            SimpleLibrary.Documentation = CreateEmptyDocumentation();
            SimpleLibrary.EntityName = CreateSimpleName(nameText);
            SimpleLibrary.FromIdentifier = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
            SimpleLibrary.ImportBlocks = BlockListHelper<Import>.CreateEmptyBlockList();
            SimpleLibrary.ClassIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

            return SimpleLibrary;
        }

        public static GlobalReplicate CreateSimpleGlobalReplicate(string nameText)
        {
            GlobalReplicate SimpleGlobalReplicate = new GlobalReplicate();
            SimpleGlobalReplicate.Documentation = CreateEmptyDocumentation();
            SimpleGlobalReplicate.ReplicateName = CreateSimpleName(nameText);
            SimpleGlobalReplicate.Patterns = new List<Pattern>();

            Pattern FirstPattern = CreateEmptyPattern();
            SimpleGlobalReplicate.Patterns.Add(FirstPattern);

            return SimpleGlobalReplicate;
        }

        public static Import CreateSimpleImport(string identifierText, string fromText, ImportType type)
        {
            Import SimpleImport = new Import();
            SimpleImport.Documentation = CreateEmptyDocumentation();
            SimpleImport.LibraryIdentifier = CreateSimpleIdentifier(identifierText);
            SimpleImport.FromIdentifier = OptionalReferenceHelper<Identifier>.CreateReference(CreateSimpleIdentifier(fromText));
            SimpleImport.Type = type;
            SimpleImport.RenameBlocks = BlockListHelper<Rename>.CreateEmptyBlockList();

            return SimpleImport;
        }

        public static Root CreateRoot(IList<Class> classList, IList<Library> libraryList, IList<GlobalReplicate> globalReplicateList)
        {
            Root EmptyRoot = new Root();
            EmptyRoot.Documentation = CreateEmptyDocumentation();
            EmptyRoot.ClassBlocks = BlockListHelper<Class>.CreateBlockList(classList);
            EmptyRoot.LibraryBlocks = BlockListHelper<Library>.CreateBlockList(libraryList);
            EmptyRoot.Replicates = globalReplicateList;

            return EmptyRoot;
        }

        public static Root CreateRoot(IList<Block<Class>> classBlockList, IList<Block<Library>> libraryBlockList, IList<GlobalReplicate> globalReplicateList)
        {
            Root EmptyRoot = new Root();
            EmptyRoot.Documentation = CreateEmptyDocumentation();
            EmptyRoot.ClassBlocks = BlockListHelper<Class>.CreateBlockList(classBlockList);
            EmptyRoot.LibraryBlocks = BlockListHelper<Library>.CreateBlockList(libraryBlockList);
            EmptyRoot.Replicates = globalReplicateList;

            return EmptyRoot;
        }
    }
}
