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

The graph is a tree graph is the sense that all nodes, except one, are the end of one, and only one, one-way arrow.

All nodes may contain zero, one or more outgoing one-way arrows to other nodes. The source code graph must not have any cycle.

The only node that is not the end of an arrow is called the Root node, also called the top of the tree.

## Node definition

Each node is made of one or more components, each component being of one the following kind :

- A discrete value: one value among a finite choice of values, whose semantic is specified in the node description. We will see that boolean values (true or false) fall into this category.
- A string. Strings are defined in a dedicated section.
- A unique identifier, specified as a sequence of 128 bits.
- A single outgoing arrow to another node. An arrow is either optional or required, as specified in the node description.
- An ordered, finite sequence of outgoing arrows to other nodes. Unless specified in the node description, the sequence may be empty.

Note that Easly has the concept of numerical values, but in the graph they appear as strings.

A source code graph is valid if and only if:

- It contains one and only one Root node.
- It only links nodes that are listed in this specification.
- All nodes, with the exception of the Root node, have one and only one incoming arrow.
- All required arrows are populated. Optional arrows, as the name indicates, may or may not be populated.
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

When the description of a node specifies that a string must be a valid identifier or name, it means the string must fulfill the following requirements:

1. It must contain printable characters or white spaces only. Annex A defines which characters are valid printable characters, and which are valid white spaces.
2. It must contain at least one printable character, meaning it cannot be empty or made only of a sequence of white spaces.
3. The first character, as well as the last character, must be printable characters. They can be the same for a string that contains only one character.

### Storage and comparison of identifiers and names

After a string has been verified to be a valid identifier or name, a few operations are performed by the compiler before it is stored for comparison with other strings.

- The string is normalized to form NFD, as per the Unicode standard version 6.1.0.
- All sequences of contiguous white spaces are replaced by a single space character, code point U+0020 (commonly called space).
- The purpose of this operation is to make sure that strings that visually look identical are indeed the same for the compiler. Editors are encouraged, but not required, to create and manipulate source code that already uses only normalized strings.

### Unique identifiers

A unique identifier is a simple sequence of 128 bits and is expected to be unique. The compiler relies on this identifier for various tasks, for example when connecting classes and libraries, or to report errors.

In the remaining of this document, unique identifiers are called “Uuid” (for Universally Unique IDentifier).

The sequence made of only 0 is reserved and not allowed in source code.

## Grammar

With the previous section having defined what a valid source code graph is, and what a valid node is, this section will list all nodes that the language defines, and for each of them additional requirements. These requirements are not related to the semantic of the language, or how nodes are related to each other, but simple requirements that all nodes of a particular kind must follow for the source code to be acceptable.

In the following tables, nodes and their components are given a name so we can refer to them in subsequent sections.

### Node groups

For some arrow node components, the arrow may point to several different nodes, not only one. To make this specification easier to read, groups of nodes have been given a name, such that when the arrow is said to be required to point to “Any” followed by a node group name, it means any node of that group.

After individual nodes are listed in the next section, all groups are listed with the names of nodes they contain.

Note that a node can belong to several groups. These groups are just shortcut in the documentation, not really a concept of the language.

### Block List

Easly supports a feature similar to C macroes and called replication. This feature uses lists of blocks that are themselves list of arrows. The format is the same for all block list, only the type of the destination node is different. Also note that in a block list nothing is optional.
    
### Node list

Tables are organized as follow:

- The name of the table is the name of the node. If applicable, names of groups the node belongs to are listed after the mention “member of”.
- In the first column are the names of components. They are listed in alphabetical order. Implementations are free to store them in any order. 
- The second column indicates the kind of the component: either discrete, string, Uuid, block list, list of arrows, or arrow.
- The third column indicates, for arrows only, if the component is optional or required.
- The fourth column lists additional requirements. For discrete values, it's the list of choices, separated with commas. Refer to a dedicated section for the semantic associated to these choices.

For strings, requirements on allowed characters. Some nodes have requirements that are too complex to describe here, and a reference to a specific sections is provided instead.
For block list and arrows, the name of the node it must point to. If it can point to several nodes, the requirement is expressed as “Any” followed by the name of the group that specifies these node names. The name of the node, or group of nodes, appears in italic characters to avoid confusion. To further help distinguish them, node names are singular while group names are plural.

The first two tables define the structure of a block list.

***

*Block List*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Node Block List | List of Arrows | N/A | *Block*

***

*Block*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Replication | Discrete | N/A | Normal, Replicated
Replication Pattern | Arrow | No | *Pattern*
Source Identifier | Arrow | No | *Identifier*
Node List | List of Arrows | N/A | Any node

***

*Agent Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Delegated | Arrow | No | *Identifier*
Base Type | Arrow | Yes | Any *Object Types*

***

*Anchored Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Anchored Name | Arrow | No | *Qualified Name*
Anchor Kind | Discrete | N/A | Declaration, Creation

***

*As Long As Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Continue Condition | Arrow | No | Any *Expressions*
Continuation Blocks | Block list | N/A | *Continuation*
Else Instructions | Arrow | Yes | *Scope*

***

*Assertion*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Tag | Arrow | Yes | *Name*
Boolean Expression | Arrow | No | Any *Expressions*

***

*Assertion Tag Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Tag Identifier | Arrow | No | *Identifier*

***

*Assignment Argument*, member of: *Arguments*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parameter Blocks | Block list | N/A | *Identifier*
Source | Arrow | No | Any *Expressions*

***

*Assignment Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Destination Blocks | Block list | N/A | *QualifiedName*
Source | Arrow | No | Any *Expressions*

***

*Assignment Type Argument*, member of: *Type Argument*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parameter Identifier | Arrow | No | *Identifier*
Source | Arrow | No | Any *Object Types*

***

*Attachment*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Attach Type Blocks | Block list | N/A | Any *Object Types*
Instructions | Arrow | No | *Scope*

***

*Attachment Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Arrow | No | Any *Expressions*
Entity Name Blocks | Block list | N/A | *Name*
Attachment Blocks | Block list | N/A | *Attachment*
Else Instructions | Arrow | Yes | *Scope*

***

*Attribute Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Arrow | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Name | Arrow | No | *Name*
Entity Type | Arrow | No | Any *Object Types*
Ensure Blocks | Block list | N/A | *Assertion*

***

*Binary Conditional Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Left Expression | Arrow | No | Any *Expressions*
Conditional | Discrete | N/A | And, Or
Right Expression | Arrow | No | Any *Expressions*

***

*Binary Operator Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Left Expression | Arrow | No | Any *Expressions*
Operator | Arrow | No | *Identifier*
Right Expression | Arrow | No | Any *Expressions*

***

*Check Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Boolean Expression | Arrow | No | Any *Expressions*

***

*Class*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Arrow | No | *Name*
From Identifier | Arrow | Yes | *Identifier*
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
Class Identifier | Arrow | No | *Identifier*
Constant Identifier | Arrow | No | *Identifier*

***

*Class Replicate*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Replicate Name | Arrow | No | *Name*
Pattern Blocks | Block list | N/A | *Pattern*

***

*Clone Of Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Type | Discrete | N/A | Shallow, Deep 
Source | Arrow | No | Any *Expressions*

***

*Command Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Command | Arrow | No | *QualifiedName*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Command Overload*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parameter Blocks | Block list | N/A | *Entity Declaration*
Parameter End | Discrete | N/A | Closed, Open
Command Body | Arrow | No | Any *Bodies*

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
Boolean Expression | Arrow | No | Any *Expressions*
Instructions | Arrow | No | *Scope*

***

*Constant Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Arrow | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Name | Arrow | No | *Name*
Entity Type | Arrow | No | Any *Object Types*
Constant Value | Arrow | No | Any *Expressions*

***

*Constraint*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parent Type | Arrow | No | Any *Object Types*
Rename Blocks | Block list | N/A | *Rename*

***

*Continuation*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Instructions | Arrow | No | *Scope*
Cleanup Blocks | Block list | N/A | Any *Instructions*

***

*Create Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Identifier | Arrow | No | *Identifier*
Creation Routine Identifier | Arrow | No | *Identifier*
Argument Blocks | Block list | N/A | Any *Arguments*
Processor | Arrow | Yes | *Qualified Name*

***

*Creation Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Arrow | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Name | Arrow | No | *Name*
Overload Blocks | Block list | N/A | *CommandOverload*

***

*Debug Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Instructions | Arrow | No | *Scope*

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
Entity Name | Arrow | No | *Name*
Numeric Value | Arrow | Yes | Any *Expressions*

***

*Document*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Comment | String | N/A | 
Uuid | Uuid | N/A | 

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
Entity Name | Arrow | No | *Name*
Entity Type | Arrow | No | Any *Object Types*
Default Value | Arrow | Yes | Any *Expressions*

***

*Entity Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Query | Arrow | No | *Qualified Name*

***

*Equality Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Left Expression | Arrow | No | Any *Expressions*
Comparison | Discrete | N/A | Equal, Different 
Equality | Discrete | N/A | Physical, Deep 
Right Expression | Arrow | No | Any *Expressions*

***

*Exception Handler*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Exception Identifier | Arrow | No | *Identifier*
Instructions | Arrow | No | *Scope*

***

*Export*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Arrow | No | *Name*
Class Identifier Blocks | Block list | N/A | *Identifier*

***

*Export Change*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Arrow | No | *Identifier*
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
While Condition | Arrow | No | Any *Expression*
Loop Instruction Blocks | Block list | N/A | Any *Instructions*
Iteration Instruction Blocks | Block list | N/A | Any *Instructions*
Invariant Blocks | Block list | N/A | *Assertion*
Variant | Arrow | No | Any *Expressions*

***

*Function Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Arrow | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Name | Arrow | No | *Name*
Once | Discrete | N/A | Normal, Object, Processor, Process 
Overload Blocks | Block list | N/A | *Query Overload*

***

*Function Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
BaseType | Arrow | No | Any *Object Types*
Overload Blocks | Block list | N/A | *Query Overload Type*

***

*Generic*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Arrow | No | *Name*
Default Value | Arrow | Yes | Any *Object Types*
Constraint Blocks | Block list | N/A | *Constraint*

***

*Generic Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Sharing | Discrete | N/A | NotShared, ReadWrite, ReadOnly, WriteOnly 
Class Identifier | Arrow | No | *Identifier*
Type Argument Blocks | Block list | N/A | *Type Argument*

***

*Global Replicate*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Replicate Name | Arrow | No | *Name*
Patterns | List of arrows | N/A | *Pattern*

***

*Identifier*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A |

***

*If Then Else Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Conditional Blocks | Block list | N/A | *Conditional*
Else Instructions | Arrow | Yes | *Scope*

***

*Import*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Library Identifier | Arrow | No | *Identifier*
From Identifier | Arrow | Yes | *Identifier*
Type | Discrete | N/A | Latest, Strict, Stable 
Rename Blocks | Block list | N/A | *Rename*

***

*Index Assignment Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Destination | Arrow | No | *Qualified Name*
Argument Blocks | Block list | N/A | Any *Arguments*
Source | Arrow | No | Any *Expression*

***

*Indexer Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Arrow | No | *Identifier*
Export | Discrete | N/A | Exported, Private
Entity Type | Arrow | No | Any *Object Types*
Index Parameter Blocks | Block list | N/A | *Entity Declaration*
Parameter End | Discrete | N/A | Closed, Open
Modified Query Blocks | Block list | N/A | *Identifier*
GetterBody | Arrow | No | Any *Bodies*
SetterBody | Arrow | No | Any *Bodies*

***

*Indexer Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Base Type | Arrow | No | Any *Object Types*
Entity Type | Arrow | No | Any *Object Types*
Index Parameter Blocks | Block list | N/A | *Entity Declaration*
Parameter End | Discrete | N/A | Closed, Open 
Indexer Kind | Discrete | N/A | ReadOnly, WriteOnly, ReadWrite 
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
Indexed Expression | Arrow | No | Any *Expressions*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Inheritance*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parent Type | Arrow | No | Any *Object Types*
Conformance | Discrete | N/A | Conformant, NonConformant 
Rename Blocks | Block list | N/A | *Rename*
Forget Indexer | Discrete | N/A | False, True 
Forget Blocks | Block list | N/A | *Identifier*
Keep Indexer | Discrete | N/A | 
Keep Blocks | Block list | N/A | *Identifier*
Discontinue Indexer Indexer | Discrete | N/A | 
Discontinue Blocks | Block list | N/A | *Identifier*
Export Change Blocks | Block list | N/A | *Export Change*

***

*Initialized Object Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Class Identifier | Arrow | No | *Identifier*
Assignment Blocks | Block list | N/A | *Assignment Argument*

***

*Inspect Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Arrow | No | Any *Expressions*
With Blocks | Block list | N/A | *With*
Else Instructions | Arrow | Yes | *Scope*

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
Source | Arrow | No | Any *Expressions*

***

*Keyword Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Value | Discrete | N/A | True, False, Current, Value, Result, Retry, Exception

***

*Library*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Arrow | No | *Name*
From Identifier | Arrow | Yes | *Identifier*
Import Blocks | Block list | N/A | *Import*
Class Identifier Blocks | Block list | N/A | *Identifier*

***

*Manifest Character Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | 

***

*Manifest Number Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | 

***

*Manifest String Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | 

***

*Name*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | 

***

*New Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Object | Arrow | N/A | *Qualified Name*

***

*Old Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Query | Arrow | No | *Qualified Name*

***

*Over Loop Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Over List | Arrow | No | Any *Expressions*
Indexer Blocks | Block list | N/A | *Name*
Iteration | Discrete | N/A | Single, Nested 
Loop Instructions | Arrow | No | *Scope*
Exit Entity Name | Arrow | Yes | *Identifier*
Invariant Blocks | Block list | N/A | *Assertion*

***

*Pattern*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Text | String | N/A | 

***

*Positional Argument*, member of: *Arguments*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Arrow | No | Any *Expressions*

***

*Positional Type Argument*, member of: *Type Arguments*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Arrow | No | Any *Object Types*

***

*Precursor Body*, member of: *Bodies*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Require Blocks | Block list | N/A | *IAssertion*
Ensure Blocks | Block list | N/A | *IAssertion*
Exception Identifier Blocks | Block list | N/A | *Identifier*
Ancestor Type | Arrow | Yes | Any *Object Types*

***

*Precursor Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Ancestor Type | Arrow | Yes | Any *Object Types*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Precursor Index Assignment Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Ancestor Type | Arrow | Yes | Any *Object Types*
Argument Blocks | Block list | N/A | Any *Arguments*
Source | Arrow | No | Any *Expressions*

***

*Precursor Index Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Ancestor Type | Arrow | Yes | Any *Object Types*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Precursor Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Ancestor Type | Arrow | Yes | Any *Object Types*
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
Export Identifier | Arrow | No | *Identifier*
Once | Discrete | N/A | Normal, Object, Processor, Process
Entity Name | Arrow | No | *Name*
Overload Blocks | Block list | N/A | *Command Overload*

***

*Procedure Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
BaseType | Arrow | No | Any *Object Types* 
Overload Blocks | Block list | N/A | *CommandOver load Type*

***

*Property Feature*, member of: *Features*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Export Identifier | Arrow | No | *Identifier*
Once | Discrete | N/A | Normal, Object, Processor, Process
Entity Name | Arrow | No | *Name*
Entity Type | Arrow | No | Any *Object Types*
Property Kind | Discrete | N/A | ReadOnly, WriteOnly, ReadWrite 
Modified Query Blocks | Block list | N/A | *Identifier*
Getter Body | Arrow | Yes | Any *Bodies*
Setter Body | Arrow | Yes | Any *Bodies*

***

*Property Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Base Type | Arrow | No | Any *Object Types*
Entity Type | Arrow | No | Any *Object Types*
Property Kind | Discrete | N/A | ReadOnly, WriteOnly, ReadWrite 
Get Ensure Blocks | Block list | N/A | *Assertion*
Get Exception Identifier Blocks | Block list | N/A | *Identifier*
Set Ensure Blocks | Block list | N/A | *Assertion*
Set Exception Identifier Blocks | Block list | N/A | *Identifier*

***

*Query Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Query | Arrow | No | *Qualified Name*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Query Overload*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Parameter Blocks | Block list | N/A | *Entity Declaration*
Parameter End | Discrete | N/A | Closed, Open 
Result Blocks | Block list | N/A | *Entity Declaration*
Modified Query Blocks | Block list | N/A | *Identifier*
Variant | Arrow | Yes | Any *Expressions*
Query Body | Arrow | No | Any *Bodies*

***

*Range*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Left Expression | Arrow | No | Any *Expressions*
Right Expression | Arrow | Yes | Any *Expressions*

***

*Raise Event Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Query Identifier | Arrow | No | *Identifier*
Event | Discrete | N/A | Single, Forever 

***

*Release Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Arrow | No | *Qualified Name*

***

*Rename*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source Identifier | Arrow | No | *Identifier*
Destination Identifier | Arrow | No | *Identifier*

***

*Result Of Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Source | Arrow | No | Any *Expressions*

***

*Root*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Class Blocks | Block list | N/A | *Class*
Library Blocks | Block list | N/A | *Library*
Replicates | List of arrows | N/A | *GlobalReplicate*

***

*Simple Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Sharing | Discrete | N/A | NotShared, ReadWrite, ReadOnly, WriteOnly 
Class Identifier | Arrow | No | *Identifier*

***

*Scope*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Declaration Blocks | Block list | N/A | *Entity Declaration*
Instruction Blocks | Block list | N/A | Any *Instructions*

***

*Throw Instruction*, member of: *Instructions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Exception Type | Arrow | No | Any *Object Types*
Creation Routine | Arrow | No | *Identifier*
Argument Blocks | Block list | N/A | Any *Arguments*

***

*Tuple Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Sharing | Discrete | N/A | NotShared, ReadWrite, ReadOnly, WriteOnly 
Entity Declaration Blocks | Block list | N/A | *Entity Declaration*

***

*Typedef*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Entity Name | Arrow | No | *Name*
Defined Type | Arrow | No | Any *Object Types*

***

*Unary Not Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Right Expression  | Arrow | No | Any *Expressions*

***

*Unary Operator Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Operator | Arrow | No | *Identifier*
Right Expression  | Arrow | No | Any *Expressions*

***

*With*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Range Blocks | Block list | N/A | *Range*
Instructions | Arrow | No | *Scope*
