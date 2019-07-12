# Feature Call Rules

Calling a feature with arguments must follow these requirements:

+ The target of the call must be a procedure or a function, or an agent of procedure or function type.
+ The list of arguments must be either a list of [assignment arguments](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Argument/AssignmentArgument.md), or a list of [positional arguments](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Argument/PositionalArgument.md). Mixing them is not allowed.
+ There must be as many arguments as necessary so that, for at least one of the feature overloads, after parameters with default values are discarded, all parameters can be assigned to an argument.
+ For all overloads for which the condition above is fulfilled, there must be at least one where all arguments conform to the parameter type, with conversion allowed.
+ There must be at most one such overload, and if this condition is satisfied this overload is the one called.
