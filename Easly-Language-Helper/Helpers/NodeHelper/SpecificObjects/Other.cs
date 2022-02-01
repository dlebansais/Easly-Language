namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using BaseNode;
using Range = BaseNode.Range;
using Contracts;
using Easly;

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

        Document Document = CreateEmptyDocument();
        IBlockList<ObjectType> AttachTypeBlocks = BlockListHelper<ObjectType>.CreateSimpleBlockList(AttachType);
        Scope Instructions = CreateEmptyScope();
        Attachment Result = new(Document, AttachTypeBlocks, Instructions);

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

        Document Document = CreateEmptyDocument();
        Attachment Result = new(Document, AttachTypeBlocks, Instructions);

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

        Document Document = CreateEmptyDocument();
        Scope Instructions = CreateEmptyScope();
        Conditional SimpleConditional = new(Document, BooleanExpression, Instructions);

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

        Document Document = CreateEmptyDocument();
        Scope Instructions = CreateSimpleScope(Instruction);
        Conditional SimpleConditional = new(Document, BooleanExpression, Instructions);

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

        Document Document = CreateEmptyDocument();
        Conditional SimpleConditional = new(Document, BooleanExpression, Instructions);

        return SimpleConditional;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Discrete"/> with provided values.
    /// </summary>
    /// <param name="nameText">The discrete name.</param>
    /// <returns>The created instance.</returns>
    public static Discrete CreateSimpleDiscrete(string nameText)
    {
        Contract.RequireNotNull(nameText, out string NameText);

        Document Document = CreateEmptyDocument();
        Name EntityName = CreateSimpleName(NameText);
        IOptionalReference<Expression> NumericValue = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
        Discrete SimpleDiscrete = new(Document, EntityName, NumericValue);

        return SimpleDiscrete;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Discrete"/> with provided values.
    /// </summary>
    /// <param name="nameText">The discrete name.</param>
    /// <param name="numericValue">The discrete value.</param>
    /// <returns>The created instance.</returns>
    public static Discrete CreateDiscrete(string nameText, Expression numericValue)
    {
        Contract.RequireNotNull(nameText, out string NameText);
        Contract.RequireNotNull(numericValue, out Expression NumericValue);

        Document Document = CreateEmptyDocument();
        Name EntityName = CreateSimpleName(NameText);
        IOptionalReference<Expression> OptionalNumericValue = OptionalReferenceHelper<Expression>.CreateReference(NumericValue);
        OptionalNumericValue.Assign();
        Discrete Result = new(Document, EntityName, OptionalNumericValue);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="ExportChange"/> with provided values.
    /// </summary>
    /// <param name="exportIdentifier">The modified export.</param>
    /// <param name="identifierBlocks">The new class names.</param>
    /// <returns>The created instance.</returns>
    public static ExportChange CreateExportChange(Identifier exportIdentifier, IBlockList<Identifier> identifierBlocks)
    {
        Contract.RequireNotNull(exportIdentifier, out Identifier ExportIdentifier);
        Contract.RequireNotNull(identifierBlocks, out IBlockList<Identifier> IdentifierBlocks);

        Document Document = CreateEmptyDocument();
        ExportChange Result = new(Document, ExportIdentifier, IdentifierBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Generic"/> with provided values.
    /// </summary>
    /// <param name="nameText">The generic name.</param>
    /// <returns>The created instance.</returns>
    public static Generic CreateSimpleGeneric(string nameText)
    {
        Contract.RequireNotNull(nameText, out string NameText);

        Document Document = CreateEmptyDocument();
        Name EntityName = CreateSimpleName(NameText);
        IOptionalReference<ObjectType> DefaultValue = OptionalReferenceHelper<ObjectType>.CreateReference(CreateDefaultObjectType());
        IBlockList<Constraint> ConstraintBlocks = BlockListHelper<Constraint>.CreateEmptyBlockList();
        Generic SimpleGeneric = new(Document, EntityName, DefaultValue, ConstraintBlocks);

        return SimpleGeneric;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Generic"/> with provided values.
    /// </summary>
    /// <param name="entityName">The generic name.</param>
    /// <param name="defaultValue">The generic default value.</param>
    /// <param name="constraintBlocks">The constraints for this generic.</param>
    /// <returns>The created instance.</returns>
    public static Generic CreateGeneric(Name entityName, ObjectType defaultValue, IBlockList<Constraint> constraintBlocks)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(defaultValue, out ObjectType DefaultValue);
        Contract.RequireNotNull(constraintBlocks, out IBlockList<Constraint> ConstraintBlocks);

        Document Document = CreateEmptyDocument();
        IOptionalReference<ObjectType> OptionalDefaultValue = OptionalReferenceHelper<ObjectType>.CreateReference(DefaultValue);
        Generic Result = new(Document, EntityName, OptionalDefaultValue, ConstraintBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Rename"/> with provided values.
    /// </summary>
    /// <param name="sourceIdentifierText">The feature to rename.</param>
    /// <param name="destinationIdentifierText">The new name.</param>
    /// <returns>The created instance.</returns>
    public static Rename CreateRename(string sourceIdentifierText, string destinationIdentifierText)
    {
        Contract.RequireNotNull(sourceIdentifierText, out string SourceIdentifierText);
        Contract.RequireNotNull(destinationIdentifierText, out string DestinationIdentifierText);

        Document Document = CreateEmptyDocument();
        Identifier SourceIdentifier = CreateSimpleIdentifier(SourceIdentifierText);
        Identifier DestinationIdentifier = CreateSimpleIdentifier(DestinationIdentifierText);
        Rename Result = new(Document, SourceIdentifier, DestinationIdentifier);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Typedef"/> with provided values.
    /// </summary>
    /// <param name="entityName">The typedef name.</param>
    /// <param name="definedType">The typedef type.</param>
    /// <returns>The created instance.</returns>
    public static Typedef CreateTypedef(Name entityName, ObjectType definedType)
    {
        Contract.RequireNotNull(entityName, out Name EntityName);
        Contract.RequireNotNull(definedType, out ObjectType DefinedType);

        Document Document = CreateEmptyDocument();
        Typedef Result = new(Document, EntityName, DefinedType);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="With"/> with no instructions.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>The created instance.</returns>
    public static With CreateSimpleWith(Expression expression)
    {
        Contract.RequireNotNull(expression, out Expression Expression);

        Document Document = CreateEmptyDocument();
        Range FirstRange = CreateSingleRange(Expression);
        IBlockList<Range> RangeBlocks = BlockListHelper<Range>.CreateSimpleBlockList(FirstRange);
        Scope Instructions = CreateEmptyScope();
        With SimpleWith = new(Document, RangeBlocks, Instructions);

        return SimpleWith;
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

        Document Document = CreateEmptyDocument();
        Range FirstRange = CreateSingleRange(Expression);
        IBlockList<Range> RangeBlocks = BlockListHelper<Range>.CreateSimpleBlockList(FirstRange);
        Scope Instructions = CreateSimpleScope(Instruction);
        With SimpleWith = new(Document, RangeBlocks, Instructions);

        return SimpleWith;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Range"/> with a single value.
    /// </summary>
    /// <param name="leftExpression">The value.</param>
    /// <returns>The created instance.</returns>
    public static Range CreateSingleRange(Expression leftExpression)
    {
        Contract.RequireNotNull(leftExpression, out Expression LeftExpression);

        Document Document = CreateEmptyDocument();
        IOptionalReference<Expression> RightExpression = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
        Range SingleRange = new(Document, LeftExpression, RightExpression);

        return SingleRange;
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

        Document Document = CreateEmptyDocument();
        IOptionalReference<Expression> DefaultValue = OptionalReferenceHelper<Expression>.CreateReference(CreateDefaultExpression());
        EntityDeclaration Result = new(Document, EntityName, EntityType, DefaultValue);

        return Result;
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

        Document Document = CreateEmptyDocument();
        Name EntityName = CreateSimpleName(NameText);
        Identifier FirstIdentifier = CreateEmptyIdentifier();
        IBlockList<Identifier> ClassIdentifierBlocks = BlockListHelper<Identifier>.CreateSimpleBlockList(FirstIdentifier);
        Export SimpleExport = new(Document, EntityName, ClassIdentifierBlocks);

        return SimpleExport;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Inheritance "/> with the specified parent type name.
    /// </summary>
    /// <param name="parentTypeName">The parent type name.</param>
    /// <returns>The created instance.</returns>
    public static Inheritance CreateSimpleInheritance(string parentTypeName)
    {
        Contract.RequireNotNull(parentTypeName, out string ParentTypeName);

        Document Document = CreateEmptyDocument();
        ObjectType ParentType = CreateSimpleSimpleType(ParentTypeName);
        IBlockList<Rename> RenameBlocks = BlockListHelper<Rename>.CreateEmptyBlockList();
        IBlockList<Identifier> ForgetBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IBlockList<Identifier> KeepBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IBlockList<Identifier> DiscontinueBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IBlockList<ExportChange> ExportChangeBlocks = BlockListHelper<ExportChange>.CreateEmptyBlockList();
        Inheritance SimpleInheritance = new(Document, ParentType, ConformanceType.Conformant, RenameBlocks, forgetIndexer: false, ForgetBlocks, keepIndexer: false, KeepBlocks, discontinueIndexer: false, DiscontinueBlocks, ExportChangeBlocks);

        return SimpleInheritance;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Inheritance "/>.
    /// </summary>
    /// <param name="parentType">The parent type.</param>
    /// <param name="conformanceType">The conformance type.</param>
    /// <param name="renameBlocks">The rename blocks.</param>
    /// <param name="forgetIndexer">The forget Indexer flag.</param>
    /// <param name="forgetBlocks">The forget blocks.</param>
    /// <param name="keepIndexer">The keep Indexer flag.</param>
    /// <param name="keepBlocks">The keep blocks.</param>
    /// <param name="discontinueIndexer">The discontinue Indexer flag.</param>
    /// <param name="discontinueBlocks">The discontinue blocks.</param>
    /// <param name="exportChangeBlocks">The export change blocks.</param>
    /// <returns>The created instance.</returns>
    public static Inheritance CreateInheritance(ObjectType parentType,
                                                ConformanceType conformanceType,
                                                IBlockList<Rename> renameBlocks,
                                                bool forgetIndexer,
                                                IBlockList<Identifier> forgetBlocks,
                                                bool keepIndexer,
                                                IBlockList<Identifier> keepBlocks,
                                                bool discontinueIndexer,
                                                IBlockList<Identifier> discontinueBlocks,
                                                IBlockList<ExportChange> exportChangeBlocks)
    {
        Contract.RequireNotNull(parentType, out ObjectType ParentType);
        Contract.RequireNotNull(renameBlocks, out IBlockList<Rename> RenameBlocks);
        Contract.RequireNotNull(forgetBlocks, out IBlockList<Identifier> ForgetBlocks);
        Contract.RequireNotNull(keepBlocks, out IBlockList<Identifier> KeepBlocks);
        Contract.RequireNotNull(discontinueBlocks, out IBlockList<Identifier> DiscontinueBlocks);
        Contract.RequireNotNull(exportChangeBlocks, out IBlockList<ExportChange> ExportChangeBlocks);

        Document Document = CreateEmptyDocument();
        Inheritance Result = new(Document, ParentType, conformanceType, RenameBlocks, forgetIndexer, ForgetBlocks, keepIndexer, KeepBlocks, discontinueIndexer, DiscontinueBlocks, ExportChangeBlocks);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Class"/> with default values.
    /// </summary>
    /// <param name="nameText">The class name.</param>
    /// <returns>The created instance.</returns>
    public static Class CreateSimpleClass(string nameText)
    {
        Contract.RequireNotNull(nameText, out string NameText);

        Document Document = CreateEmptyDocument();
        Name EntityName = CreateSimpleName(NameText);
        IOptionalReference<Identifier> FromIdentifier = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
        IBlockList<Import> ImportBlocks = BlockListHelper<Import>.CreateEmptyBlockList();
        IBlockList<Generic> GenericBlocks = BlockListHelper<Generic>.CreateEmptyBlockList();
        IBlockList<Export> ExportBlocks = BlockListHelper<Export>.CreateEmptyBlockList();
        IBlockList<Typedef> TypedefBlocks = BlockListHelper<Typedef>.CreateEmptyBlockList();
        IBlockList<Inheritance> InheritanceBlocks = BlockListHelper<Inheritance>.CreateEmptyBlockList();
        IBlockList<Discrete> DiscreteBlocks = BlockListHelper<Discrete>.CreateEmptyBlockList();
        IBlockList<ClassReplicate> ClassReplicateBlocks = BlockListHelper<ClassReplicate>.CreateEmptyBlockList();
        IBlockList<Feature> FeatureBlocks = BlockListHelper<Feature>.CreateEmptyBlockList();
        IBlockList<Identifier> ConversionBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        IBlockList<Assertion> InvariantBlocks = BlockListHelper<Assertion>.CreateEmptyBlockList();
        Guid ClassGuid = Guid.NewGuid();
        Class SimpleClass = new(Document, EntityName, FromIdentifier, CopySemantic.Reference, CloneableStatus.Cloneable, ComparableStatus.Comparable, isAbstract: false, ImportBlocks, GenericBlocks, ExportBlocks, TypedefBlocks, InheritanceBlocks, DiscreteBlocks, ClassReplicateBlocks, FeatureBlocks, ConversionBlocks, InvariantBlocks, ClassGuid, classPath: string.Empty);

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

        Document Document = CreateEmptyDocument();
        Name EntityName = CreateSimpleName(NameText);
        IOptionalReference<Identifier> FromIdentifier = OptionalReferenceHelper<Identifier>.CreateReference(CreateEmptyIdentifier());
        IBlockList<Import> ImportBlocks = BlockListHelper<Import>.CreateEmptyBlockList();
        IBlockList<Identifier> ClassIdentifierBlocks = BlockListHelper<Identifier>.CreateEmptyBlockList();
        Library SimpleLibrary = new(Document, EntityName, FromIdentifier, ImportBlocks, ClassIdentifierBlocks);

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

        Document Document = CreateEmptyDocument();
        Name ReplicateName = CreateSimpleName(NameText);
        Pattern FirstPattern = CreateEmptyPattern();
        List<Pattern> Patterns = new() { FirstPattern };
        GlobalReplicate SimpleGlobalReplicate = new(Document, ReplicateName, Patterns);

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

        Document Document = CreateEmptyDocument();
        Name ReplicateName = CreateSimpleName(NameText);
        Pattern FirstPattern = CreateEmptyPattern();
        IBlockList<Pattern> PatternBlocks = BlockListHelper<Pattern>.CreateSimpleBlockList(FirstPattern);
        ClassReplicate SimpleClassReplicate = new(Document, ReplicateName, PatternBlocks);

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

        Document Document = CreateEmptyDocument();
        Identifier LibraryIdentifier = CreateSimpleIdentifier(IdentifierText);
        IOptionalReference<Identifier> FromIdentifier = OptionalReferenceHelper<Identifier>.CreateReference(CreateSimpleIdentifier(FromText));
        IBlockList<Rename> RenameBlocks = BlockListHelper<Rename>.CreateEmptyBlockList();
        Import SimpleImport = new(Document, LibraryIdentifier, FromIdentifier, importType, RenameBlocks);

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

        Document Document = CreateEmptyDocument();
        IBlockList<Class> ClassBlocks = BlockListHelper<Class>.CreateBlockListFromNodeList(ClassList);
        IBlockList<Library> LibraryBlocks = BlockListHelper<Library>.CreateBlockListFromNodeList(LibraryList);
        Root Result = new(Document, ClassBlocks, LibraryBlocks, GlobalReplicateList);

        return Result;
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

        Document Document = CreateEmptyDocument();
        IBlockList<Class> ClassBlocks = BlockListHelper<Class>.CreateBlockListFromBlockList(ClassBlockList);
        IBlockList<Library> LibraryBlocks = BlockListHelper<Library>.CreateBlockListFromBlockList(LibraryBlockList);
        Root Result = new(Document, ClassBlocks, LibraryBlocks, GlobalReplicateList);

        return Result;
    }
}
