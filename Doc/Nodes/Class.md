# Class

Classes in Easly are similar to C# classes, but with major differences in supported features. This document will try to list all of them and elaborate on how Easly code is translated to C# code.

The first sections are dedicated to features that Easly and C# have in common, followed by additional features that can be found in Easly. Finally, a list of feature not supported in Easly is provided, with ways to obtain similar result but with a different approach.

## Common features

### Reference type

In C# a reference type is declared with the `class` keyword, and a value type with `struct`. In Easly, both types are defined in a class, and the copy semantic (what differentiate a reference type from a value type) is specified. Usually, since `Reference` is the default, it is not displayed in the Easly editor.

### Null references

While not a class concept, note that Easly doesn't have `null` references. Every references must be initialized somehow to an existing object.

### Creating an object

In C#, an object is created with the `new` instruction, and one of the class constructor is called based on arguments of this instruction. In Easly, one must specify explicitely the name of the constructor (it must be a creation feature), and only then the corresponding overload is selected by arguments. This allows for example two constructors for a `Complex` class, one with cartesian coordinates and another with polar coordinates.

```csharp
class Complex
{
    public Complex(double x, double y)
    {
        /*...*/
    }

    // Not allowed in C#
    public Complex(double rho, double theta)
    {
        /*...*/
    }
}
```

To enable using multiple constructors, the compiler first creates a base object (for which no constructor is declared), then adds a call to an initialization routine that corresponds to the creation feature in Easly.

### Releasing an object

Both languages use a garbage collector, and both can explicitely free memory and collect it. For this purpose Easly uses the [Release Instruction](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Instruction/ReleaseInstruction.md), while the same can be obtained in C# with a `using` empty block.

### Access rights

A C# class can have various access rights. Easly supports `public` (the default), `protected` and `private`, but doesn't support `internal`. Instead, the visibility of a class can be fine-tuned with an export specification. This specification can be a library name, and if the library includes all classes in an assembly one can obtain the same visibility as internal.

### Inheritance

A major difference between Easly and C# is that Easly support multiple inheritance. The compiler tries to obtain the same inheritance hierarchy in C# using the following strategy:

+ If the class inherits from no class or from one class only, the hierarchy is preserved and this parent (if any) is the C# base class.
+ If one of the parents contains one or more extern bodies, this parent becomes the C# base class. Other parents are implemented as described in the last case below.
+ Otherwise, the C# class has no base, but instead inherits from as many interfaces as there are parents, and code from parents is duplicated in this child.

### Abstract class

Abstract classes are pretty much the same in Easly and C#.

### Generic class

Easly supports generic with constraints but is more flexible than C# on constraints. If a constraint cannot be expressed directly in C#, it is translated to an interface with appropriate methods.

### Comparing objects

In C#, a class inherits from objects (directly or indirectly through its base class) and therefore contains the `Equal` method that compares the current object with another. Easly is in some way more restrictive, as you can explicitely disallow comparison (at least using the [Equality Expression](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Expression/EqualityExpression.md), you can always write your own comparison code).

If comparison is enabled, the translation is straightforward, and either compare references, or use the `Equal` method to compare by value.

## Features only supported in Easly

### From identifier

An Easly class can have an additional identifier to separate it from other classes of the same name. This is translated to C# by:

+ Adding the identifier as a suffix to the class name.
+ Storing the class file in a directory named from this identifier.

### Clone

An Easly class can be cloned with the [Clone Of Expression](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Expression/CloneOfExpression.md). There is no such support in C#, but the compiler has all the necessary knowledge to create a `Clone` feature that does exactly that. 

### Invariant

A class can have several invariants, expressions that constitute a permanent contract and must always evaluate to `True` in any possible state of an instance of the class.

Class invariant are implemented like other [assertions](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Assertion.md) in a dedicated `ClassInvariant` method, and a call to this method is added at end of each method in the class.

### Class UUID

Each class has its own unique ID, available using a [Preprocessor Expression](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Expression/PreprocessorExpression.md). This ID doesn't change when the class is modified.

### Class Path

Each class has its own unique path string, created at compile time, and available using a [Preprocessor Expression](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Expression/PreprocessorExpression.md). This string is intended to be added to logs, to help with debugging.

## Features not supported in Easly

### Nested classes

Easly doesn't support nesting classes. Instead, the class visibility can be restricted to the other class that is supposed to use it.

### Sealed classes

Easly doesn't support sealing. Sealing in C# makes sense in the following situations:

+ A class contains security-critical code that must not be replaced.<br>This can be obtained by creating objects of that class using a proxy, and exporting creation features to that proxy only.
+ Avoiding performance loss.<br>If the performance loss can be detected, it should be prevented by testing. If it cannot be detected, there is no performance loss.
+ Classes not designed to be subclassed.<br>Easly is a language for professional programmers, and it is expected that being able to always subclass is more important than preventing accidental subclassing by beginners.

 
