# Overload Declaration Rules

All overloads of a feature (a procedure, function or creation feature) must pass a list of checks.

1. All parameters of an overload must have different names.

+ All overloads are grouped in sets by the number of parameters they have. If a parameter has a default value, the overload belongs to both the set with this parameter, and the set without it.

+ If an overload has an open list of parameters, it belongs to all sets that have as many or more parameters.
 
+ Within a set of n parameters, there must be no pair of overloads for which all parameters types have a common ancestor (other than `Any` of course), with all possible conversions included.

If overloads have results, the following checks are also performed:

5. All results names must be different, and they must be different than parameter names as well.

6. For all results at the n<sup>th</sup> position, one of them must be the ancestor of all others. Note that it can be `Any` as long as `Any` is explicitely the result type. If an overload has less than n results it is ignored. 
