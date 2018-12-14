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

### Node list

Tables are organized as follow:

- The name of the table is the name of the node. If applicable, names of groups the node belongs to are listed after the mention “member of”.
- In the first column are the names of components. They are listed in alphabetical order. Implementations are free to store them in any order. 
- The second column indicates the kind of the component: either discrete, string, Uuid or arrow.
- The third column indicates, for arrows only, if the component is optional or required.
- The fourth column lists additional requirements. For discrete values, it's the list of choices, separated with commas. Refer to a dedicated section for the semantic associated to these choices.
For strings, requirements on allowed characters. Some nodes have requirements that are too complex to describe here, and a reference to a specific sections is provided instead.
For arrows, the name of the node it must point to. If it can point to several nodes, the requirement is expressed as “Any” followed by the name of the group that specifies these node names. The name of the node, or group of nodes, appears in italic characters to avoid confusion. To further help distinguish them, node names are singular while group names are plural.

*Agent Expression*, member of: *Expressions*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Delegated | Arrow | No | Identifier
Base Type | Arrow | Yes | Any Object Types

*Anchored Type*, member of: *Object Types*

Name | Kind | Optional | Requirement
------------ | ------------- | ------------- | -------------
Anchored Name | Arrow | No | Qualified Name
Anchor Kind | Discrete | N/A | Declaration, Creation



