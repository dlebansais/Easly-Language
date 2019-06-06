# Agent Expression

An agent expression turns a feature into an agent. Agents are used to make a call dynamic, instead of always calling the same feature. (See agents).

<div style="margin-top: 10px; line-height:0; background-color:whitesmoke; border-style:solid; border-color: lightgray; border-width: thin; font-family: Courier">
<div style="margin-left: 10px">
<br/>
<p><font color="#000000">&nbsp;&nbsp;Result&nbsp;</font><font color="#0000FF">&#8592&nbsp;</font><font color="#0000FF">agent</font><font color="#0000FF">{</font><font color="#2B91AF">My&nbsp;Type&nbsp;</font><font color="#0000FF">}&nbsp;</font><font color="#000000">My&nbsp;Feature</font></p>
<br/>
</div>
</div>
    
My Feature must be a feature of My Type, available under the same conditions as a regular call. For example:

<div style="margin-top: 10px; line-height:0; background-color:whitesmoke; border-style:solid; border-color: lightgray; border-width: thin; font-family: Courier">
<div style="margin-left: 10px">
<br/>
<p><font color="#000000">Foo&nbsp;</font><font color="#0000FF">procedure</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;</font><font color="#0000FF">local</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X</font><font color="#0000FF">:&nbsp;</font><font color="#2B91AF">My&nbsp;Type</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;</font><font color="#0000FF">do</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&#183;My&nbsp;feature</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;</font><font color="#0000FF">&#8592&nbsp;</font><font color="#0000FF">agent</font><font color="#0000FF">{</font><font color="#2B91AF">My&nbsp;Type</font><font color="#0000FF">}&nbsp;</font><font color="#000000">My&nbsp;Feature</font></p>
<p><font color="#000000"></font><font color="#0000FF">end</font></p>
<br/>
</div>
</div>
    
The agent, result of the expression, can be used wherever a call to My Feature would be valid. 
<div style="margin-top: 10px; line-height:0; background-color:whitesmoke; border-style:solid; border-color: lightgray; border-width: thin; font-family: Courier">
<div style="margin-left: 10px">
<br/>
<p><font color="#000000">Foo&nbsp;</font><font color="#0000FF">procedure</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;</font><font color="#0000FF">local</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X</font><font color="#0000FF">:&nbsp;</font><font color="#2B91AF">My&nbsp;Type</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Y</font><font color="#0000FF">:&nbsp;procedure&nbsp;</font><font color="#2B91AF">My&nbsp;Type&nbsp;</font><font color="#0000FF">&#9484;</font><font color="#0000FF">parameter&nbsp;&nbsp;&nbsp;</font><font color="#0000FF">&#9488;</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font><font color="#0000FF">&#9474;&nbsp;&nbsp;&nbsp;</font><font color="#000000">P</font><font color="#0000FF">:&nbsp;</font><font color="#2B91AF">Number</font><font color="#0000FF">&#9474;</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font><font color="#0000FF">&#9492;</font><font color="#0000FF">end&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font><font color="#0000FF">&#9496;</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;</font><font color="#0000FF">do</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Y&nbsp;</font><font color="#0000FF">&#8592;&nbsp;</font><font color="#0000FF">agent</font><font color="#0000FF">{</font><font color="#2B91AF">My&nbsp;Type</font><font color="#0000FF">}&nbsp;</font><font color="#000000">My&nbsp;Feature</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X</font><font color="#0000FF">&#183;</font><font color="#000000">My&nbsp;Feature</font><font color="#0000FF">(</font><font color="#008000">0</font><font color="#0000FF">)</font></p>
<p><font color="#000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X</font><font color="#0000FF">&#183;</font><font color="#000000">Y</font><font color="#0000FF">(</font><font color="#008000">0</font><font color="#0000FF">)</font></p>
<p><font color="#000000"></font><font color="#0000FF">end</font></p>
<br/>
</div>
</div>
    
Its type is:

+ A procedure type if the feature is a procedure or a creation feature, with parameters matching those of the procedure.
+ A function type if the feature is a function, with parameters and results matching those of the function.
+ A property type if the feature is a property.

 
 