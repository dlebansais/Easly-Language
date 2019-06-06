# Easly - Language Specification

Easly is a programming language supporting a rich and complex set of features. Its primary purpose is to protect developers from making mistakes, and it enforces many practices that usually are simple guidelines. Its secondary purpose is to be compatible with a large set of other languages, for a smooth integration in existing development processes. Therefore, Easly is targeted mainly at professional programmers.

## History

Easly design started in 2012.

## Purpose

The purpose of this document is to provide a comprehensive and complete reference for the Easly Language.
Easly is a programming language. That is, a language used to communicate with computers. Because it is quite complex, and designed mostly for professional programmers, a full reference is mandatory.
This document defines the precise rules of the language, and in particular is the definite reference to know if a program is correct, and how the computer will behave when it executes the program's instructions.

## Structure of this document

For each concept, a formal definition is provided, and as often as possible, is accompanied by readable explanations, as well as examples.

Some books that describe programming languages in full go from top to bottom: starting with the most generic concepts, they go more and more into details until they deal with syntax and grammar issues. Others do the opposite, they start with the syntax, and build from there, explaining higher level language features as made from previous blocks.

This reference takes a mixed, yet different approach. It describes concepts of the language the way a compiler needs to understand them. In other words, this is the ideal document to build an Easly compiler, because all steps are explained in the order a compiler meets them.

For instance, we'll begin with the definition of the source code that a compiler receives as input, and end with the output it generates. Therefore, if you are looking for the description of a specific feature, it is better to look for it directly in the index at the end, rather than trying to guess where you can find it. It might be explained next to other, completely unrelated features.

## Aspects not covered

This document does not define file formats, or a lexical syntax. In fact, many details are left open for implementers of development tools.

This approach has the disadvantage that two different implementations of an Easly compiler or editor will probably be mutually incompatible. However, this is a common situation in the industry anyway, because implementations often deviate on details that were left aside, or ignored, in specifications. If specifications are too tight instead, then no room is left for innovation and  in the long run languages suffer from that.

This document doesn't specify properties of the computer where Easly programs will executes either, other than in very general terms. Because Easly can be used to make small low level drivers or big high-level applications, it focuses on programming an “ideal” computer. In practice, implementers specify how the final program is generated for the computer they target. Another consideration to take into account it that the output of the compiler may be source code for a development process in another language. In that case, many technical details can be postponed beyond the place where Easly fits in the process.

An editor has been developed in parallel with the language, its documentation  is covered in a separate document, and not considered part of the language. Therefore it will not be specified here.

## Credits
...

# Specifications

## Definition of Source Code

Defining a programming language means defining what sentences in that language mean. Therefore it begins with defining what is a sentence. The approach taken in this document is to consider that everything begins with input data for a compiler. How that input was created, which editor was used for instance, is irrelevant as long as the input data is correct from the perspective of the compiler. In the rest of this document, all input of the compiler will be collectively called “the source code”.

This specification does not attempt to specify how the source code is stored before the compiler processes it. In particular, it does not defines a file format, or any format at all. Anyone implementing its own compiler is free to choose a file format if applicable, or some other storage method like a database.

This approach allows implementors to integrate an Easly compiler into their own development system regardless of how information is stored and exchanged. Only the most basic piece of information needed to create source is specified.

## Graph Definition

An Easly source code is a tree graph connecting nodes. This section will focus on the properties of the graph, and will give an overview of the node components.

The graph is a tree graph is the sense that all nodes, except one, are the end of one, and only one, one-way reference.

All nodes may contain zero, one or more outgoing one-way references to other nodes. The source code graph must not have any cycle.

The only node that is not the destination of an reference is called the Root node, also called the top of the tree.

## Node definition

Each node is made of one or more components, each component being of one the following kind :

- A discrete value: one value among a finite choice of values, whose semantic is specified in the node description. We will see that boolean values (true or false) fall into this category.
- A string. Strings are defined in a dedicated section.
- A unique identifier, specified as a sequence of 128 bits.
- A single outgoing reference to another node. An reference is either optional or required, as specified in the node description.
- An ordered, finite sequence of outgoing references to other nodes. Unless specified in the node description, the sequence may be empty.

Note that Easly has the concept of numerical values, but in the graph they appear as strings.

A source code graph is valid if and only if:

- It contains one and only one Root node.
- It only links nodes that are listed in this specification.
- All nodes, with the exception of the Root node, have one and only one incoming reference.
- All required references are populated. Optional references, as the name indicates, may or may not be populated.
- It doesn't contain cycles.

## Node components

The previous section listed the five types of components that constitute nodes. This section will define them more precisely.

### Discrete values

Nodes may contain discrete values, to choose amongst a finite set of values. Implementations are free to chose how these values are stored, and compared to allowed values.

This document gives a common name to values, as well as the associated semantic. For instance, some nodes may include a discrete value to choose between two options, one called "true" and the other called "false". In this case, this document specifies what is the result of each choice.
There are no default value, in the sense that no particular value is preferred over others in a set. Some are, however, used a lot more often and maybe considered the default in, say, an editor. But not in the language.

### Strings

Nodes in Easly may include strings. This section defines what a string is and how to handle them.

A string is an ordered sequence of characters, as defined by the Unicode standard version 6.1.0 (specified at www.unicode.org). The reader not familiar with the definition of a character in the Unicode standard should refer to their introduction in chapter 1 of the standard.

Unless specified, any sequence of characters is valid, including the empty sequence (also called the empty string).

However, a few nodes have additional requirements on strings and allow only some sequences. The section that describe the node will also list these requirements. Since one of them comes quite frequently, it is summarized below instead of being repeated throughout this document.

### Valid identifiers and names

When the description of a node specifies that a string must be a valid identifier or a valid name, it means the string must fulfill the following requirements:

1. It must contain printable characters or white spaces only. Annex A defines which characters are valid printable characters, and which are valid white spaces.
2. It must contain at least one printable character, meaning it cannot be empty or made only of a sequence of white spaces.
3. The first character, as well as the last character, must be printable characters. They can be the same for a string that contains only one character.

### Storage and comparison of identifiers and names

After a string has been verified to be a valid identifier or name, a few operations are performed by the compiler before it is stored for comparison with other strings.

- The string is normalized to form NFD, as per the Unicode standard version 6.1.0.
- All sequences of contiguous white spaces are replaced by a single space character, code point U+0020 (commonly called space).

The purpose of this operation is to make sure that strings that visually look identical are indeed the same for the compiler. Editors are encouraged, but not required, to create and manipulate source code that already uses only normalized strings.

### Unique identifiers

A unique identifier is a simple sequence of 128 bits and is expected to be unique. The compiler relies on this identifier for various tasks, for example when connecting classes and libraries, or to report errors.

In the remaining of this document, unique identifiers are called “Uuid” (for Universally Unique IDentifier).

The sequence made of only 0 is reserved and not allowed in source code.

## Grammar

With the previous section having defined what a valid source code graph is, and what a valid node is, this section will list all nodes that the language defines, and for each of them additional requirements. These requirements are not related to the semantic of the language, or how nodes are related to each other, but simple requirements that all nodes of a particular kind must follow for the source code to be acceptable.

In the following tables, nodes and their components are given a name so we can refer to them in subsequent sections.

### Node groups

For some reference components, the reference may point to several different nodes, not only one. To make this specification easier to read, groups of nodes have been given a name, such that when the reference is said to be required to point to “Any” followed by a node group name, it means any node of that group. For example, “Any *Expressions*” means any node member of the *Expressions* group.

After individual nodes are listed in the next section, all groups are listed with the names of nodes they contain.

Note that a node can belong to several groups. These groups are just shortcut in the documentation, not really a concept of the language.
    
### Nodes

Tables are organized as follow:

- The name of the table is the name of the node. If applicable, names of groups the node belongs to are listed after the mention “member of”.
- In the first column are the names of components. They are listed in alphabetical order. Implementations are free to store them in any order. 
- The second column indicates the kind of the component: either discrete, string, Uuid, block list (see below), list of references, or reference.
- The third column indicates, for references only, if the component is optional or required (block list, and list of references, are never optional, just empty if allowed).
- The fourth column lists additional requirements.
  * For discrete values, it's the list of choices, separated with commas. Refer to a dedicated section for the semantic associated to these choices.
  * For strings, the requirement is on allowed characters.
  * Some nodes have requirements that are too complex to describe in the column, and a reference to a specific sections is provided instead.
  * For block list and references, the name of the node it must point to. If it can point to several nodes, the requirement is expressed as “Any” followed by the name of the group that specifies these node names. The name of the node, or group of nodes, appears in italic characters to avoid confusion. To further help distinguish them, node names are singular while group names are plural.

### Block List

Easly supports a feature similar to C macroes, called replication. This feature uses lists of blocks that are themselves list of references. The format is the same for all block lists, only the type of the destination node is different. Also note that in a block list nothing is optional.

The two following tables define the structure of a block list.

*Block List*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Node Block List | List of references | N/A | *Block*

***

*Block*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Replication | Discrete | N/A | Normal, Replicated
Replication Pattern | Reference | No | *Pattern*
Source Identifier | Reference | No | *Identifier*
Node List | List of references | N/A | Any node

### Documentation

While not stricly necessary for a compiler, comments in the source code must of course be expected. To make this specification simpler, all nodes, including the Block List and Block nodes, are expected to contain a reference to a document node. If found, this reference is ignored by the compiler.
 
The following table defines the structure of a document node.

*Document*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Comment | String | N/A | 
Uuid | Uuid | N/A | 

### Other nodes

*Agent Expression*, member of: *Expressions*
See [Agent Expression](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Expression/AgentExpression.md)

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Delegated | Reference | No | *Identifier*
Base Type | Reference | Yes | Any *Object Types*

***

*Anchored Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Anchored Name | Reference | No | *Qualified Name*
Anchor Kind | Discrete | N/A | Declaration, Creation

***

*As Long As Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Continue Condition | Reference | No | Any *Expressions*
Continuation Blocks | Block list | N/A | *Continuation*
Else Instructions | Reference | Yes | *Scope*

***

*Assertion*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Tag | Reference | Yes | *Name*
Boolean Expression | Reference | No | Any *Expressions*

***

*Assertion Tag Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Tag Identifier | Reference | No | *Identifier*

***

*Assignment Argument*, member of: *Arguments*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parameter Blocks | Block list | N/A | *Identifier*
Source | Reference | No | Any *Expressions*

***

*Assignment Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Destination Blocks | Block list | N/A | *QualifiedName*
Source | Reference | No | Any *Expressions*

***

*Assignment Type Argument*, member of: *Type Argument*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parameter Identifier | Reference | No | *Identifier*
Source | Reference | No | Any *Object Types*

***

*Attachment*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Attach Type Blocks | Block list | N/A | Any *Object Types*
Instructions | Reference | No | *Scope*

***

*Attachment Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Reference | No | Any *Expressions*
Entity Name Blocks | Block list | N/A | *Name*
Attachment Blocks | Block list | N/A | *Attachment*
Else Instructions | Reference | Yes | *Scope*

***

*Attribute Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Reference | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Name | Reference | No | *Name*
Entity Type | Reference | No | Any *Object Types*
Ensure Blocks | Block list | N/A | *Assertion*

***

*Binary Conditional Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Left Expression | Reference | No | Any *Expressions*
Conditional | Discrete | N/A | And, Or
Right Expression | Reference | No | Any *Expressions*

***

*Binary Operator Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Left Expression | Reference | No | Any *Expressions*
Operator | Reference | No | *Identifier*
Right Expression | Reference | No | Any *Expressions*

***

*Check Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Boolean Expression | Reference | No | Any *Expressions*

***

*Class*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Reference | No | *Name*
From Identifier | Reference | Yes | *Identifier*
Copy Specification | Discrete | N/A | Any, Reference, Value
Cloneable | Discrete | N/A | Cloneable, Single
Comparable | Discrete | N/A | Comparable, Uncomparable
Is Abstract | Discrete | N/A | False, True
Import Blocks | Block List | N/A | *Import*
Generic Blocks | Block List | N/A | *Generic*
Export Blocks | Block List | N/A | *Export*
Typedef Blocks | Block List | N/A | *Typedef*
Inheritance Blocks | Block List | N/A | *Inheritance*
Discrete Blocks | Block List | N/A | *Discrete*
Class Replicate Blocks | Block List | N/A | *Class Replicate*
Feature Blocks | Block List | N/A | Any *Features*
Conversion Blocks | Block List | N/A | *Conversion*
Invariant Blocks | Block List | N/A | *Assertion*
Class Guid | Uuid | N/A |
Class Path | String | N/A |

***

*Class Constant Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Class Identifier | Reference | No | *Identifier*
Constant Identifier | Reference | No | *Identifier*

***

*Class Replicate*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Replicate Name | Reference | No | *Name*
Pattern Blocks | Block list | N/A | *Pattern*

***

*Clone Of Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Type | Discrete | N/A | Shallow, Deep 
Source | Reference | No | Any *Expressions*

***

*Command Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Command | Reference | No | *QualifiedName*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Command Overload*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parameter Blocks | Block list | N/A | *Entity Declaration*
Parameter End | Discrete | N/A | Closed, Open
Command Body | Reference | No | Any *Bodies*

***

*Command Overload Type*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parameter Blocks | Block list | N/A | *Entity Declaration*
Parameter End | Discrete | N/A | Closed, Open
Require Blocks | Block list | N/A | *Assertion*
Ensure Blocks | Block list | N/A | *Assertion*
Exception Identifier Blocks | Block list | N/A | *Identifier*

***

*Conditional*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Boolean Expression | Reference | No | Any *Expressions*
Instructions | Reference | No | *Scope*

***

*Constant Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Reference | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Name | Reference | No | *Name*
Entity Type | Reference | No | Any *Object Types*
Constant Value | Reference | No | Any *Expressions*

***

*Constraint*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parent Type | Reference | No | Any *Object Types*
Rename Blocks | Block list | N/A | *Rename*

***

*Continuation*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Instructions | Reference | No | *Scope*
Cleanup Blocks | Block list | N/A | Any *Instructions*

***

*Create Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Identifier | Reference | No | *Identifier*
Creation Routine Identifier | Reference | No | *Identifier*
Argument Blocks | Block list | N/A | Any *Arguments*
Processor | Reference | Yes | *Qualified Name*

***

*Creation Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Reference | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Name | Reference | No | *Name*
Overload Blocks | Block list | N/A | *CommandOverload*

***

*Debug Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Instructions | Reference | No | *Scope*

***

*Deferred Body*, member of: *Bodies*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Require Blocks | Block list | N/A | *IAssertion*
Ensure Blocks | Block list | N/A | *IAssertion*
Exception Identifier Blocks | Block list | N/A | *Identifier*

***

*Discrete*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Reference | No | *Name*
Numeric Value | Reference | Yes | Any *Expressions*

***

*Effective Body*, member of: *Bodies*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Require Blocks | Block list | N/A | *IAssertion*
Ensure Blocks | Block list | N/A | *IAssertion*
Exception Identifier Blocks | Block list | N/A | *Identifier*
Entity Declaration Blocks | Block list | N/A | *Entity Declaration*
Body Instruction Blocks | Block list | N/A | Any *Instructions*
Exception Handler Blocks | Block list | N/A | *ExceptionHandler*

***

*Entity Declaration*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Reference | No | *Name*
Entity Type | Reference | No | Any *Object Types*
Default Value | Reference | Yes | Any *Expressions*

***

*Entity Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Query | Reference | No | *Qualified Name*

***

*Equality Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Left Expression | Reference | No | Any *Expressions*
Comparison | Discrete | N/A | Equal, Different 
Equality | Discrete | N/A | Physical, Deep 
Right Expression | Reference | No | Any *Expressions*

***

*Exception Handler*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Exception Identifier | Reference | No | *Identifier*
Instructions | Reference | No | *Scope*

***

*Export*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Reference | No | *Name*
Class Identifier Blocks | Block list | N/A | *Identifier*

***

*Export Change*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Reference | No | *Identifier*
Identifier Blocks | Block list | N/A | *Identifier*

***

*Extern Body*, member of: *Bodies*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Require Blocks | Block list | N/A | *IAssertion*
Ensure Blocks | Block list | N/A | *IAssertion*
Exception Identifier Blocks | Block list | N/A | *Identifier*

***

*For Loop Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Declaration Blocks | Block list | N/A | *Entity Declaration*
Init Instruction Blocks | Block list | N/A | Any *Instructions*
While Condition | Reference | No | Any *Expression*
Loop Instruction Blocks | Block list | N/A | Any *Instructions*
Iteration Instruction Blocks | Block list | N/A | Any *Instructions*
Invariant Blocks | Block list | N/A | *Assertion*
Variant | Reference | No | Any *Expressions*

***

*Function Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Reference | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Name | Reference | No | *Name*
Once | Discrete | N/A | Normal, Object, Processor, Process 
Overload Blocks | Block list | N/A | *Query Overload*

***

*Function Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
BaseType | Reference | No | Any *Object Types*
Overload Blocks | Block list | N/A | *Query Overload Type*

***

*Generic*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Reference | No | *Name*
Default Value | Reference | Yes | Any *Object Types*
Constraint Blocks | Block list | N/A | *Constraint*

***

*Generic Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Sharing | Discrete | N/A | Not Shared, Read Write, Read Only, Write Only 
Class Identifier | Reference | No | *Identifier*
Type Argument Blocks | Block list | N/A | *Type Argument*

***

*Global Replicate*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Replicate Name | Reference | No | *Name*
Patterns | List of references | N/A | *Pattern*

***

*Identifier*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | Valid identifier

***

*If Then Else Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Conditional Blocks | Block list | N/A | *Conditional*
Else Instructions | Reference | Yes | *Scope*

***

*Import*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Library Identifier | Reference | No | *Identifier*
From Identifier | Reference | Yes | *Identifier*
Type | Discrete | N/A | Latest, Strict, Stable 
Rename Blocks | Block list | N/A | *Rename*

***

*Index Assignment Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Destination | Reference | No | *Qualified Name*
Argument Blocks | Block list | N/A | Any *Arguments*
Source | Reference | No | Any *Expression*

***

*Indexer Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Reference | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Type | Reference | No | Any *Object Types*
Index Parameter Blocks | Block list | N/A | *Entity Declaration*
Parameter End | Discrete | N/A | Closed, Open
Modified Query Blocks | Block list | N/A | *Identifier*
GetterBody | Reference | No | Any *Bodies*
SetterBody | Reference | No | Any *Bodies*

***

*Indexer Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Base Type | Reference | No | Any *Object Types*
Entity Type | Reference | No | Any *Object Types*
Index Parameter Blocks | Block list | N/A | *Entity Declaration*
Parameter End | Discrete | N/A | Closed, Open 
Indexer Kind | Discrete | N/A | Read Only, Write Only, Read Write 
Get Require Blocks | Block list | N/A | *Assertion*
Get Ensure Blocks | Block list | N/A | *Assertion*
Get Exception Identifier Blocks | Block list | N/A | *Identifier*
Set Require Blocks | Block list | N/A | *Assertion*
Set Ensure Blocks | Block list | N/A | *Assertion*
Set Exception Identifier Blocks | Block list | N/A | *Identifier*

***

*Index Query Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Indexed Expression | Reference | No | Any *Expressions*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Inheritance*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parent Type | Reference | No | Any *Object Types*
Conformance | Discrete | N/A | Conformant, Non Conformant 
Rename Blocks | Block list | N/A | *Rename*
Forget Indexer | Discrete | N/A | False, True 
Forget Blocks | Block list | N/A | *Identifier*
Keep Indexer | Discrete | N/A | False, True
Keep Blocks | Block list | N/A | *Identifier*
Discontinue Indexer Indexer | Discrete | N/A | False, True 
Discontinue Blocks | Block list | N/A | *Identifier*
Export Change Blocks | Block list | N/A | *Export Change*

***

*Initialized Object Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Class Identifier | Reference | No | *Identifier*
Assignment Blocks | Block list | N/A | *Assignment Argument*

***

*Inspect Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Reference | No | Any *Expressions*
With Blocks | Block list | N/A | *With*
Else Instructions | Reference | Yes | *Scope*

***

*Keyword Anchored Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Anchor | Discrete | N/A | True, False, Current, Value, Result, Retry, Exception 

***

*Keyword Assignment Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Destination | Discrete | N/A | True, False, Current, Value, Result, Retry, Exception
Source | Reference | No | Any *Expressions*

***

*Keyword Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Value | Discrete | N/A | True, False, Current, Value, Result, Retry, Exception

***

*Library*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Reference | No | *Name*
From Identifier | Reference | Yes | *Identifier*
Import Blocks | Block list | N/A | *Import*
Class Identifier Blocks | Block list | N/A | *Identifier*

***

*Manifest Character Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | Exactly one character

***

*Manifest Number Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | See below

***

*Manifest String Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | 

***

*Name*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | Valid name

***

*New Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Object | Reference | N/A | *Qualified Name*

***

*Old Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Query | Reference | No | *Qualified Name*

***

*Over Loop Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Over List | Reference | No | Any *Expressions*
Indexer Blocks | Block list | N/A | *Name*
Iteration | Discrete | N/A | Single, Nested 
Loop Instructions | Reference | No | *Scope*
Exit Entity Name | Reference | Yes | *Identifier*
Invariant Blocks | Block list | N/A | *Assertion*

***

*Pattern*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | Valid identifier

***

*Positional Argument*, member of: *Arguments*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Reference | No | Any *Expressions*

***

*Positional Type Argument*, member of: *Type Arguments*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Reference | No | Any *Object Types*

***

*Precursor Body*, member of: *Bodies*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Require Blocks | Block list | N/A | *IAssertion*
Ensure Blocks | Block list | N/A | *IAssertion*
Exception Identifier Blocks | Block list | N/A | *Identifier*
Ancestor Type | Reference | Yes | Any *Object Types*

***

*Precursor Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Ancestor Type | Reference | Yes | Any *Object Types*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Precursor Index Assignment Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Ancestor Type | Reference | Yes | Any *Object Types*
Argument Blocks | Block list | N/A | Any *Arguments*
Source | Reference | No | Any *Expressions*

***

*Precursor Index Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Ancestor Type | Reference | Yes | Any *Object Types*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Precursor Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Ancestor Type | Reference | Yes | Any *Object Types*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Preprocessor Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Value | Discrete | N/A | Date And Time, Compilation Discrete Identifier, Class Path, Compiler Version, Conformance To Standard, Discrete Class Identifier, Counter, Debugging, Random Integer

***

*Procedure Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Reference | No | *Identifier*
Once | Discrete | N/A | Normal, Object, Processor, Process
Entity Name | Reference | No | *Name*
Overload Blocks | Block list | N/A | *Command Overload*

***

*Procedure Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
BaseType | Reference | No | Any *Object Types* 
Overload Blocks | Block list | N/A | *CommandOver load Type*

***

*Property Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Reference | No | *Identifier*
Once | Discrete | N/A | Normal, Object, Processor, Process
Entity Name | Reference | No | *Name*
Entity Type | Reference | No | Any *Object Types*
Property Kind | Discrete | N/A | Read Only, Write Only, Read Write 
Modified Query Blocks | Block list | N/A | *Identifier*
Getter Body | Reference | Yes | Any *Bodies*
Setter Body | Reference | Yes | Any *Bodies*

***

*Property Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Base Type | Reference | No | Any *Object Types*
Entity Type | Reference | No | Any *Object Types*
Property Kind | Discrete | N/A | Read Only, Write Only, Read Write 
Get Ensure Blocks | Block list | N/A | *Assertion*
Get Exception Identifier Blocks | Block list | N/A | *Identifier*
Set Ensure Blocks | Block list | N/A | *Assertion*
Set Exception Identifier Blocks | Block list | N/A | *Identifier*

***

*Query Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Query | Reference | No | *Qualified Name*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Query Overload*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parameter Blocks | Block list | N/A | *Entity Declaration*
Parameter End | Discrete | N/A | Closed, Open 
Result Blocks | Block list | N/A | *Entity Declaration*
Modified Query Blocks | Block list | N/A | *Identifier*
Variant | Reference | Yes | Any *Expressions*
Query Body | Reference | No | Any *Bodies*

***

*Range*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Left Expression | Reference | No | Any *Expressions*
Right Expression | Reference | Yes | Any *Expressions*

***

*Raise Event Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Query Identifier | Reference | No | *Identifier*
Event | Discrete | N/A | Single, Forever 

***

*Release Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Reference | No | *Qualified Name*

***

*Rename*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source Identifier | Reference | No | *Identifier*
Destination Identifier | Reference | No | *Identifier*

***

*Result Of Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Reference | No | Any *Expressions*

***

*Root*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Class Blocks | Block list | N/A | *Class*
Library Blocks | Block list | N/A | *Library*
Replicates | List of references | N/A | *GlobalReplicate*

***

*Scope*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Declaration Blocks | Block list | N/A | *Entity Declaration*
Instruction Blocks | Block list | N/A | Any *Instructions*

***

*Simple Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Sharing | Discrete | N/A | Not Shared, Read Write, Read Only, Write Only 
Class Identifier | Reference | No | *Identifier*

***

*Throw Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Exception Type | Reference | No | Any *Object Types*
Creation Routine | Reference | No | *Identifier*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Tuple Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Sharing | Discrete | N/A | Not Shared, Read Write, Read Only, Write Only 
Entity Declaration Blocks | Block list | N/A | *Entity Declaration*

***

*Typedef*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Reference | No | *Name*
Defined Type | Reference | No | Any *Object Types*

***

*Unary Not Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Right Expression | Reference | No | Any *Expressions*

***

*Unary Operator Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Operator | Reference | No | *Identifier*
Right Expression  | Reference | No | Any *Expressions*

***

*With*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Range Blocks | Block list | N/A | *Range*
Instructions | Reference | No | *Scope*

### Node groups membership

This section is informative only. All node groups are listed with their members.

Group Name | Member
------------ | -------------
Arguments | Assignment Argument, Positional Argument 
Bodies | Deferred Body, Effective Body, Extern Body, Precursor Body 
Expressions | Agent Expression, Assertion Tag Expression, Binary Conditional Expression, Binary Operator Expression, Class Constant Expression, Clone Of Expression, Entity Expression, Equality Expression, Index Query Expression, Initialized Object Expression, Keyword Expression, Manifest Character Expression, Manifest Number Expression, Manifest Numeric Expression, Manifest String Expression, New Expression, Old Expression, Precursor Expression, Precursor Index Expression, Preprocessor Expression, Query Expression, Result Of Expression, Unary Not Expression, Unary Operator Expression 
Features | Attribute Feature, Constant Feature, Creation Feature, Function Feature, Indexer Feature, Procedure Feature, Property Feature 
Instructions | As Long As Instruction, Assignment Instruction, Attachment Instruction, Check Instruction, Command Instruction, Create Instruction, Debug Instruction, For Loop Instruction, If Then Else Instruction, Index Assignment Instruction, Inspect Instruction, Keyword Assignment Instruction, Over Loop Instruction, Precursor Index Assignment Instruction, Precursor Instruction, Raise Event Instruction, Release Instruction, Throw Instruction 
Object Types | Anchored Type, Function Type, Generic Type, Indexer Type, Keyword Anchored Type, Procedure Type, Property Type, Simple Type, Tuple Type 
Type Arguments | Assignment Type Argument, Positional Type Argument 

## Annex A

The definition of printable characters and whitespace characters goes as follow:

### Printable characters:

All characters in range U+0021 to U+007F

All characters in range U+00A1 to U+00AD

All characters in range U+00AE to U+034F

All characters in range U+0350 to U+0378

All characters in range U+037A to U+037F

All characters in range U+0384 to U+0E01EF
  
### Whitespace characters:

U+0009, U+0020, U+00A0, U+1680, U+2000, U+2001, U+2002, U+2003, U+2004, U+2005, U+2006, U+2007, U+2008, U+2009, U+200A, U+202F, U+205F, U+3000.

## Annex B

The definition of built-in constructs.

Name | Class Guid
------------ | -------------
Any | c6297be4-e121-400f-ad78-79df3ecf2858
Any Detachable Reference | a035583e-30d7-4434-be9a-8c4780029ead
Any Once Reference | 894e9bd1-f516-42c4-a95f-a40e698eea97
Any Optional Reference | ba18c1c9-f75f-4783-b99a-acfe78ff21fc
Any Stable Reference | d3c9682c-5b9f-437c-97a2-f92a76dd2904
Attribute Entity | 1a3bcacb-9e25-4b39-bb4a-aaf1d826a578
Bit Field Enumeration | fefd8baf-090a-40c1-85e2-ffc6c7236bdd
Boolean | 0ce78910-608d-41ee-a5e2-666d834bbb86
Character | 040a43ee-aa73-457f-9a32-022c2d89e041
Constant Entity | 123ac71a-2be4-4677-924e-b5013472d3b0
Detachable Reference | bc6c211d-737b-4de5-bb69-22fa6f145018
Entity | 04abab65-fca1-4cc5-8584-c527f20ae95a
Enumeration | 56943636-5db9-48f6-8456-9bb593698aa2
Event | 296a70a8-d07a-4f9a-ab09-46fbd9a4f08e
Exception | d7a3c606-3521-46df-8bd5-d1f16074bd60
Feature Entity | a24ff4d8-4cc7-40ca-9e20-5990537c683a
Function Entity | f497d05b-32a2-420f-8d0b-add133e17098
Hashtable | d50a2e83-7d47-4aa1-9cb3-1e3f91f3e299
Indexer Entity | 3c5d167e-8406-418d-814a-8fc296524513
Key Value Pair | a531b63d-73cd-48ee-8115-027c4eb21473
List | ce8fb534-1c8b-4563-a7d2-e617571e5333
Local Entity | 43b2429b-e9d6-417e-b6fe-942fa261b176
Named Feature Entity | c1555fa7-928a-40ee-b21f-ded92bc7b756
Number | 337731dc-37ba-47a3-9e85-1d7c2304e36e
Once Reference | 415dd598-bc65-4e74-ae7d-2f9aafd1d975
Optional Reference | 9af0a9f5-820f-4ddb-b221-f7a1b3446676
Over Loop Source | 4273b1ee-4b1b-4ae1-bf22-f72563f87c02
Procedure Entity | 085550a0-9456-4791-b4a4-303b685a2b99
Property Entity | 270a1358-45e3-4d36-86f1-3e150bbf341d
Sealable Hashtable | d6d59964-9b33-434e-8511-2453e84fbd68
Specialized Type Entity | a3a0be99-7c93-4d88-8afc-14d53cce8345
Stable Reference | c3ba9569-b9c7-4e30-9146-d489f2e49a64
String | 46a55923-c615-49b8-a4b7-620738d4a941
Type Entity | eda30eb6-27d8-4632-8cf3-394c9bc61eb6

