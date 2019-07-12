# Command Overload Type

A command overload type doesn't exist on its own, it is always found in the context of a [procedure type](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Type/ProcedureType.md), where it describes the type of one of the overloads.

In a given procedure type, all overload types must abide by the [overload declaration rules](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Misc/OverloadDeclarationRules.md).

# Translation to C&#35;

Overload types are not translated to C#, they are only used by the compiler to validate the source code.