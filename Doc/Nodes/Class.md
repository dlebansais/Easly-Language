# Class

Classes in Easly are similar to C# classes, but with major differences in supported features. This document will try to list all of them and elaborate on how Easly code is translated to C# code.

The first sections are dedicated to features that Easly and C# have in common, followed by additional features that can be found in Easly.

## Reference type

In C# a reference type is declared with the `class` keyword, and a value type with `struct`. In Easly, both types are defined in a class, and the copy semantic (what differentiate a reference type from a value type) is specified. Usually, since `Reference` is the default, it is not displayed in the Easly editor.






An attachment is a part of the [Attachment Instruction](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Instruction/AttachmentInstruction.md) where a set of variables are tested to see if they conform to given types (provided in the attachment). If they do, instructions contained in the attachment are executed.

Attachments contain instruction but also have their own scope and can declare local variables.

See the [Attachment instruction](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Instruction/AttachmentInstruction.md) for details. 

# Translation to C&#35;

```csharp
```
