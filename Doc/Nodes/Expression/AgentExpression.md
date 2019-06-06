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

Note that `My Type` is optional. If not provided, the feature is obtained from the class itself, directly of through inheritance.

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
    
Its type is:

+ A procedure type if the feature is a procedure or a creation feature, with parameters matching those of the procedure.
+ A function type if the feature is a function, with parameters and results matching those of the function.
+ A property type if the feature is a property.

 
 