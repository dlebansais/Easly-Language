# Class

Classes in Easly are similar to C# classes, but with major differences in supported features. This document will try to list all of them and elaborate on how Easly code is translated to C# code.

The first sections are dedicated to features that Easly and C# have in common, followed by additional features that can be found in Easly.

## Reference type

In C# a reference type is declared with the `class` keyword, and a value type with `struct`. In Easly, both types are defined in a class, and the copy semantic (what differentiate a reference type from a value type) is specified. Usually, since `Reference` is the default, it is not displayed in the Easly editor.

## Null references

While not a class concept, note that Easly doesn't have `null` references. Every references must be initialized somehow.

## Creating an object

In C#, an object is created with the new instruction, and one of the class constructor is called based on arguments of this instruction. In Easly, one must specify explicitely the name of the constructor (that must be a creation feature), and only then the corresponding overload is selected by arguments. This allows for example two constructors for a `Complex` class, one with cartesian coordinates and another with polar coordinates.

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

## Releasing an object

Both languages use a garbage collector, and both can explicitely free memory and collect it. For this purpose Easly uses the [Release Instruction](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Instruction/ReleaseInstruction.md).
