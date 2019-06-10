# Agent Expression

An agent expression turns a feature into an agent. Agents are used to make a call dynamic, instead of always calling the same feature. (See agents).

<pre>
&nbsp;&nbsp;Result&nbsp;&#8592;&nbsp;<b>agent</b>{<i>My&nbsp;Type</i>}&nbsp;My&nbsp;Feature
</pre>
    
`My Feature` must be a feature of `My Type`, available under the same conditions as a regular call. For example:

<pre>
Foo&nbsp;<b>procedure</b>
&nbsp;&nbsp;&nbsp;<b>local</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X<b>:&nbsp;</b><i>My&nbsp;Type</i>
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&#183;My&nbsp;Feature
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;&#8592;&nbsp;<b>agent</b>{<i>My&nbsp;Type</i>}&nbsp;My&nbsp;Feature
<b>end</b>
</pre>

Note that `My Type` is optional. If not provided, the feature is obtained from the class itself, directly or through inheritance. If the optional type is provided, it must be a class type, or a generic type with constraints, for which `My Feature` can be resolved to an existing feature.

The agent, result of the expression, can be used wherever a call to `My Feature` would be valid.

<pre>
Foo&nbsp;<b>procedure</b>
&nbsp;&nbsp;&nbsp;<b>local</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X<b>:&nbsp;</b><i>My&nbsp;Type</i>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Y<b>:&nbsp;procedure&nbsp;</b><i>My&nbsp;Type&nbsp;</i>&#9484;<b>parameter&nbsp;&nbsp;&nbsp;</b>&#9488;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9474;&nbsp;&nbsp;&nbsp;P<b>:&nbsp;</b><i>Number</i>&#9474;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9492;<b>end&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>&#9496;
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Y&nbsp;&#8592;&nbsp;<b>agent</b>{<i>My&nbsp;Type</i>}&nbsp;My&nbsp;Feature
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&#183;My&nbsp;Feature(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&#183;Y(0)
<b>end</b>
</pre>

## Type    
Its type is:

+ A procedure type if the feature is a procedure or a creation feature, with parameters matching those of the procedure.
+ A function type if the feature is a function, with parameters and results matching those of the function.
+ A property type if the feature is a property.

The base type (of the procedure, function or property type) is obtained as follow:

### Optional type unspecified
If the agent expression doesn't specify the type, the base type is the class type, with generics if any.

<pre>
<b>class&nbsp;</b>My&nbsp;Class
<b>generic</b>
&nbsp;&nbsp;&nbsp;T
<b>feature</b>
&nbsp;&nbsp;&nbsp;Foo&nbsp;<b>procedure</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>local</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X<b>:&nbsp;procedure&nbsp;</b>My&nbsp;Class[<i>T</i>]&nbsp;&#9484;<b>parameter&nbsp;</b>&#9488;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9492;<b>end&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>&#9496;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&nbsp;&#8592;&nbsp;<b>agent&nbsp;My&nbsp;Feature
&nbsp;&nbsp;&nbsp;<b>end</b>
&nbsp;&nbsp;&nbsp;My Feature&nbsp;<b>procedure</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;<b>end</b>
<b>end</b>
</pre>

### Optional type is a class type
If the agent expression specifies a type, and it's a class type (possibly with generic arguments), the base type is the same as this specified type.

<pre>
<b>class&nbsp;</b>My&nbsp;Class
<b>generic</b>
&nbsp;&nbsp;&nbsp;T
<b>feature</b>
&nbsp;&nbsp;&nbsp;Foo&nbsp;<b>procedure</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>local</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X<b>:&nbsp;procedure&nbsp;</b>Other&nbsp;Class[<i>T</i>]&nbsp;&#9484;<b>parameter&nbsp;</b>&#9488;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9492;<b>end&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>&#9496;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&nbsp;&#8592;&nbsp;<b>agent</b>{Other&nbsp;Class[<i>T</i>]}&nbsp;Other&nbsp;Feature
&nbsp;&nbsp;&nbsp;<b>end</b>
&nbsp;&nbsp;&nbsp;My Feature&nbsp;<b>procedure</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;<b>end</b>
<b>end</b>
</pre>

### Optional type is a generic type
If it's a generic type, it has to have at least one constraint (otherwise there would be no feature available) and then the base type is the constraint in which the feature could be found. Note that, if there is a conflict between several constraints using the same name for a feature, this conflict is resolved with renaming in constraints. 

<pre>
<b>class&nbsp;</b>My&nbsp;Class
<b>generic</b>
&nbsp;&nbsp;&nbsp;T
&nbsp;&nbsp;&nbsp;<b>conform&nbsp;to</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hashtable[<i>String</i>,&nbsp;<i>Number</i>]
&nbsp;&nbsp;&nbsp;<b>end</b>
<b>feature</b>
&nbsp;&nbsp;&nbsp;Foo&nbsp;<b>procedure</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>local</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X<b>:&nbsp;procedure&nbsp;</b>Hashtable[<i>String</i>,&nbsp;<i>Number</i>]&nbsp;&#9484;<b>parameter&nbsp;</b>&#9488;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9492;<b>end&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>&#9496;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&nbsp;&#8592;&nbsp;<b>agent</b>{<i>T</i>}&nbsp;Wipe&nbsp;Out
&nbsp;&nbsp;&nbsp;<b>end</b>
<b>end</b>
</pre>

