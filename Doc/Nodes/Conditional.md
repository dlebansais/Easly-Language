# Conditional

A conditional is a part of the [If Then Else Instruction](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Instruction/IfThenElseInstruction.md) where an expression is evaluated and checked if true (or, in the case of an event, is signaled). If if is, instructions contained in the conditional are executed.

Conditionals contain instructions but also have their own scope and can declare local variables.

The expression evaluated in the conditional must have only one result, of type `Boolean` or `Event`, or a type that conforms to any of these two after conversion.

See the [If Then Else Instruction](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Instruction/IfThenElseInstruction.md) for details. 
