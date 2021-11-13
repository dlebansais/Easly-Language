namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    /// <summary>
    /// Creates a new instance of a <see cref="Attachment"/> with a single attachment type.
    /// </summary>
    /// <param name="attachType">The type.</param>
    /// <returns>The created instance.</returns>
    public static Attachment CreateAttachment(ObjectType attachType)
    {
        Contract.RequireNotNull(attachType, out ObjectType AttachType);

        Attachment Result = new Attachment();
        Result.Documentation = CreateEmptyDocumentation();
        Result.AttachTypeBlocks = BlockListHelper<ObjectType>.CreateSimpleBlockList(AttachType);
        Result.Instructions = CreateEmptyScope();

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Attachment"/> with provided values.
    /// </summary>
    /// <param name="attachTypeBlocks">The list of attachment types.</param>
    /// <param name="instructions">The attachment isntructions.</param>
    /// <returns>The created instance.</returns>
    public static Attachment CreateAttachment(IBlockList<ObjectType> attachTypeBlocks, Scope instructions)
    {
        Contract.RequireNotNull(attachTypeBlocks, out IBlockList<ObjectType> AttachTypeBlocks);
        Contract.RequireNotNull(instructions, out Scope Instructions);

        if (NodeTreeHelperBlockList.IsBlockListEmpty((IBlockList)AttachTypeBlocks))
            throw new ArgumentException($"{nameof(attachTypeBlocks)} must not be empty");

        Attachment Result = new Attachment();
        Result.Documentation = CreateEmptyDocumentation();
        Result.AttachTypeBlocks = AttachTypeBlocks;
        Result.Instructions = Instructions;

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Conditional"/> with no instructions.
    /// </summary>
    /// <param name="booleanExpression">The conditional expression.</param>
    /// <returns>The created instance.</returns>
    public static Conditional CreateConditional(Expression booleanExpression)
    {
        Contract.RequireNotNull(booleanExpression, out Expression BooleanExpression);

        Conditional SimpleConditional = new Conditional();
        SimpleConditional.Documentation = CreateEmptyDocumentation();
        SimpleConditional.BooleanExpression = BooleanExpression;
        SimpleConditional.Instructions = CreateEmptyScope();

        return SimpleConditional;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Conditional"/> with a single instruction.
    /// </summary>
    /// <param name="booleanExpression">The conditional expression.</param>
    /// <param name="instruction">The instructions.</param>
    /// <returns>The created instance.</returns>
    public static Conditional CreateConditional(Expression booleanExpression, Instruction instruction)
    {
        Contract.RequireNotNull(booleanExpression, out Expression BooleanExpression);
        Contract.RequireNotNull(instruction, out Instruction Instruction);

        Conditional SimpleConditional = new Conditional();
        SimpleConditional.Documentation = CreateEmptyDocumentation();
        SimpleConditional.BooleanExpression = BooleanExpression;
        SimpleConditional.Instructions = CreateSimpleScope(Instruction);

        return SimpleConditional;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Conditional"/> with provided values.
    /// </summary>
    /// <param name="booleanExpression">The conditional expression.</param>
    /// <param name="instructions">The instructions.</param>
    /// <returns>The created instance.</returns>
    public static Conditional CreateConditional(Expression booleanExpression, Scope instructions)
    {
        Contract.RequireNotNull(booleanExpression, out Expression BooleanExpression);
        Contract.RequireNotNull(instructions, out Scope Instructions);

        Conditional SimpleConditional = new Conditional();
        SimpleConditional.Documentation = CreateEmptyDocumentation();
        SimpleConditional.BooleanExpression = BooleanExpression;
        SimpleConditional.Instructions = Instructions;

        return SimpleConditional;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="With"/> with no instructions.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>The created instance.</returns>
    public static With CreateSimpleWith(Expression expression)
    {
        Contract.RequireNotNull(expression, out Expression Expression);

        BaseNode.Range FirstRange = CreateSingleRange(Expression);

        With Result = new With();
        Result.Documentation = CreateEmptyDocumentation();
        Result.RangeBlocks = BlockListHelper<BaseNode.Range>.CreateSimpleBlockList(FirstRange);
        Result.Instructions = CreateEmptyScope();

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="With"/> with a single case expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <param name="instruction">The instruction.</param>
    /// <returns>The created instance.</returns>
    public static With CreateSimpleWith(Expression expression, Instruction instruction)
    {
        Contract.RequireNotNull(expression, out Expression Expression);
        Contract.RequireNotNull(instruction, out Instruction Instruction);

        BaseNode.Range FirstRange = CreateSingleRange(Expression);

        With Result = new With();
        Result.Documentation = CreateEmptyDocumentation();
        Result.RangeBlocks = BlockListHelper<BaseNode.Range>.CreateSimpleBlockList(FirstRange);
        Result.Instructions = CreateSimpleScope(Instruction);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="BaseNode.Range"/> with a single value.
    /// </summary>
    /// <param name="leftExpression">The value.</param>
    /// <returns>The created instance.</returns>
    public static BaseNode.Range CreateSingleRange(Expression leftExpression)
    {
        Contract.RequireNotNull(leftExpression, out Expression LeftExpression);

        BaseNode.Range Result = new BaseNode.Range();
        Result.Documentation = CreateEmptyDocumentation();
        Result.LeftExpression = LeftExpression;
        Result.RightExpression = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="EntityDeclaration"/> with a single destination.
    /// </summary>
    /// <param name="entityName">The destination name.</param>
    /// <param name="entityType">The destination type.</param>
    /// <returns>The created instance.</returns>
    public static EntityDeclaration CreateEntityDeclaration(Name entityName, ObjectType entityType)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(entityType, out ObjectType EntityType);

        EntityDeclaration SimpleEntityDeclaration = new EntityDeclaration();
        SimpleEntityDeclaration.Documentation = CreateEmptyDocumentation();
        SimpleEntityDeclaration.EntityName = EntityName;
        SimpleEntityDeclaration.EntityType = EntityType;
        SimpleEntityDeclaration.DefaultValue = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());

        return SimpleEntityDeclaration;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Identifier"/> corresponding to the unrestricted export identifier.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Identifier CreateEmptyExportIdentifier()
    {
        return CreateSimpleIdentifier("All");
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Export"/> exporting to a single identifier.
    /// </summary>
    /// <param name="nameText">The export name.</param>
    /// <returns>The created instance.</returns>
    public static Export CreateSimpleExport(string nameText)
    {
        Contract.RequireNotNull(nameText, out string NameText);

        Identifier FirstIdentifier = CreateEmptyIdentifier();

        Export SimpleExport = new Export();
        SimpleExport.Documentation = CreateEmptyDocumentation();
        SimpleExport.EntityName = CreateSimpleName(NameText);
        SimpleExport.ClassIdentifierBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(FirstIdentifier);

        return SimpleExport;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Class"/> with default values.
    /// </summary>
    /// <param name="nameText">The class name.</param>
    /// <returns>The created instance.</returns>
    public static Class CreateSimpleClass(string nameText)
    {
        Contract.RequireNotNull(nameText, out string NameText);

        Guid ClassGuid = Guid.NewGuid();

        Class SimpleClass = new Class();
        SimpleClass.Documentation = CreateEmptyDocumentation();
        SimpleClass.EntityName = CreateSimpleName(NameText);
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

    /// <summary>
    /// Creates a new instance of a <see cref="Library"/> with default values.
    /// </summary>
    /// <param name="nameText">The library name.</param>
    /// <returns>The created instance.</returns>
    public static Library CreateSimpleLibrary(string nameText)
    {
        Contract.RequireNotNull(nameText, out string NameText);

        Library SimpleLibrary = new Library();
        SimpleLibrary.Documentation = CreateEmptyDocumentation();
        SimpleLibrary.EntityName = CreateSimpleName(NameText);
        SimpleLibrary.FromIdentifier = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
        SimpleLibrary.ImportBlocks = BlockListHelper<Import>.CreateEmptyBlockList();
        SimpleLibrary.ClassIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();

        return SimpleLibrary;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="GlobalReplicate"/> with default values.
    /// </summary>
    /// <param name="nameText">The global replicate name.</param>
    /// <returns>The created instance.</returns>
    public static GlobalReplicate CreateSimpleGlobalReplicate(string nameText)
    {
        Contract.RequireNotNull(nameText, out string NameText);

        Pattern FirstPattern = CreateEmptyPattern();

        GlobalReplicate SimpleGlobalReplicate = new GlobalReplicate();
        SimpleGlobalReplicate.Documentation = CreateEmptyDocumentation();
        SimpleGlobalReplicate.ReplicateName = CreateSimpleName(NameText);
        SimpleGlobalReplicate.Patterns = new List<Pattern>() { FirstPattern };

        return SimpleGlobalReplicate;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ClassReplicate"/> with default values.
    /// </summary>
    /// <param name="nameText">The global replicate name.</param>
    /// <returns>The created instance.</returns>
    public static ClassReplicate CreateSimpleClassReplicate(string nameText)
    {
        Contract.RequireNotNull(nameText, out string NameText);

        ClassReplicate SimpleClassReplicate = new ClassReplicate();
        SimpleClassReplicate.Documentation = CreateEmptyDocumentation();
        SimpleClassReplicate.ReplicateName = CreateSimpleName(NameText);

        Pattern FirstPattern = CreateEmptyPattern();
        SimpleClassReplicate.PatternBlocks = BlockListHelper<Pattern>.CreateSimpleBlockList(FirstPattern);

        return SimpleClassReplicate;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Import"/> with provided values.
    /// </summary>
    /// <param name="identifierText">The import identifier.</param>
    /// <param name="fromText">The from identifier.</param>
    /// <param name="importType">The import type.</param>
    /// <returns>The created instance.</returns>
    public static Import CreateSimpleImport(string identifierText, string fromText, ImportType importType)
    {
        Contract.RequireNotNull(identifierText, out string IdentifierText);
        Contract.RequireNotNull(fromText, out string FromText);

        Import SimpleImport = new Import();
        SimpleImport.Documentation = CreateEmptyDocumentation();
        SimpleImport.LibraryIdentifier = CreateSimpleIdentifier(IdentifierText);
        SimpleImport.FromIdentifier = OptionalReferenceHelper<Identifier>.CreateReference(CreateSimpleIdentifier(FromText));
        SimpleImport.Type = importType;
        SimpleImport.RenameBlocks = BlockListHelper<Rename>.CreateEmptyBlockList();

        return SimpleImport;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Root"/> with provided values.
    /// </summary>
    /// <param name="classList">The list of classes.</param>
    /// <param name="libraryList">The list of libraries.</param>
    /// <param name="globalReplicateList">The list of global replicates.</param>
    /// <returns>The created instance.</returns>
    public static Root CreateRoot(IList<Class> classList, IList<Library> libraryList, IList<GlobalReplicate> globalReplicateList)
    {
        Contract.RequireNotNull(classList, out IList<Class> ClassList);
        Contract.RequireNotNull(libraryList, out IList<Library> LibraryList);
        Contract.RequireNotNull(globalReplicateList, out IList<GlobalReplicate> GlobalReplicateList);

        Root EmptyRoot = new Root();
        EmptyRoot.Documentation = CreateEmptyDocumentation();
        EmptyRoot.ClassBlocks = BlockListHelper<Class>.CreateBlockListFromNodeList(ClassList);
        EmptyRoot.LibraryBlocks = BlockListHelper<Library>.CreateBlockListFromNodeList(LibraryList);
        EmptyRoot.Replicates = GlobalReplicateList;

        return EmptyRoot;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Root"/> with provided values.
    /// </summary>
    /// <param name="classBlockList">The list of classes.</param>
    /// <param name="libraryBlockList">The list of libraries.</param>
    /// <param name="globalReplicateList">The list of global replicates.</param>
    /// <returns>The created instance.</returns>
    public static Root CreateRoot(IList<IBlock<Class>> classBlockList, IList<IBlock<Library>> libraryBlockList, IList<GlobalReplicate> globalReplicateList)
    {
        Contract.RequireNotNull(classBlockList, out List<IBlock<Class>> ClassBlockList);
        Contract.RequireNotNull(libraryBlockList, out IList<IBlock<Library>> LibraryBlockList);
        Contract.RequireNotNull(globalReplicateList, out IList<GlobalReplicate> GlobalReplicateList);

        Root EmptyRoot = new Root();
        EmptyRoot.Documentation = CreateEmptyDocumentation();
        EmptyRoot.ClassBlocks = BlockListHelper<Class>.CreateBlockListFromBlockList(ClassBlockList);
        EmptyRoot.LibraryBlocks = BlockListHelper<Library>.CreateBlockListFromBlockList(LibraryBlockList);
        EmptyRoot.Replicates = GlobalReplicateList;

        return EmptyRoot;
    }
}
