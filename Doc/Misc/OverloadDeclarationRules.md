# Overload Declaration Rules

All overloads of a feature (a procedure, function or creation feature) must pass a list of checks.

1. All overloads are grouped in sets by the number of parameters they have. If a parameter has a default value, the overload belongs to both the set with this parameter, and the set without it.

2. Within a set of n parameters, there must be no pair of overloads for which all parameters types have a common ancestor (other than Any of course), with all possible conversions included.

If overloads have results, the following checks are also performed:

