# Agent Expression

An agent expression turns a feature into an agent. Agents are used to make a call dynamic, instead of always calling the same feature. (See agents).

<div style="margin-top: 10px; line-height:0; background-color:whitesmoke; border-style:solid; border-color: lightgray; border-width: thin; font-family: Courier">
<div style="margin-left: 10px">
<br/>
<p><u>UUU</u></p>
<p><b>BBB</b></p>
<p><i>III</i></p>
<p><q>QQQ</q></p>
<p><strong>strong</strong></p>
<p><em>em</em></p>
<p><pre>pre</pre></p>
<p><code>code</code></p>
<p><strike>strike</strike></p>
<p><tt>tt</tt></p>
<ruby>
漢 <rt> ㄏㄢˋ </rt>
</ruby>
<p><span style="color: #000000;">&nbsp;&nbsp;Result&nbsp;</span><span style="color: #0000FF;">&#8592&nbsp;</span><span style="color: #0000FF">agent</span><span style="color: #0000FF">{</span><span style="color: #2B91AF">My&nbsp;Type&nbsp;</span><span style="color: #0000FF">}&nbsp;</span><span style="color: #000000">My&nbsp;Feature</span></p>
<br/>
</div>
</div>
    
`My Feature` must be a feature of `My Type`, available under the same conditions as a regular call. For example:

<div style="margin-top: 10px; line-height:0; background-color:whitesmoke; border-style:solid; border-color: lightgray; border-width: thin; font-family: Courier">
<div style="margin-left: 10px">
<br/>
<p><span style="color: #000000">Foo&nbsp;</span><span style="color: #0000FF">procedure</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;</span><span style="color: #0000FF">local</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X</span><span style="color: #0000FF">:&nbsp;</span><span style="color: #2B91AF">My&nbsp;Type</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;</span><span style="color: #0000FF">do</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&#183;My&nbsp;feature</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;</span><span style="color: #0000FF">&#8592&nbsp;</span><span style="color: #0000FF">agent</span><span style="color: #0000FF">{</span><span style="color: #2B91AF">My&nbsp;Type</span><span style="color: #0000FF">}&nbsp;</span><span style="color: #000000">My&nbsp;Feature</span></p>
<p><span style="color: #000000"></span><span style="color: #0000FF">end</span></p>
<br/>
</div>
</div>

Note that `My Type` is optional. If not provided, the feature is obtained from the class itself, directly of through inheritance.

The agent, result of the expression, can be used wherever a call to `My Feature` would be valid. 
<div style="margin-top: 10px; line-height:0; background-color:whitesmoke; border-style:solid; border-color: lightgray; border-width: thin; font-family: Courier">
<div style="margin-left: 10px">
<br/>
<p><span style="color: #000000">Foo&nbsp;</span><span style="color: #0000FF">procedure</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;</span><span style="color: #0000FF">local</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X</span><span style="color: #0000FF">:&nbsp;</span><span style="color: #2B91AF">My&nbsp;Type</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Y</span><span style="color: #0000FF">:&nbsp;procedure&nbsp;</span><span style="color: #2B91AF">My&nbsp;Type&nbsp;</span><span style="color: #0000FF">&#9484;</span><span style="color: #0000FF">parameter&nbsp;&nbsp;&nbsp;</span><span style="color: #0000FF">&#9488;</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style="color: #0000FF">&#9474;&nbsp;&nbsp;&nbsp;</span><span style="color: #000000">P</span><span style="color: #0000FF">:&nbsp;</span><span style="color: #2B91AF">Number</span><span style="color: #0000FF">&#9474;</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style="color: #0000FF">&#9492;</span><span style="color: #0000FF">end&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style="color: #0000FF">&#9496;</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;</span><span style="color: #0000FF">do</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Y&nbsp;</span><span style="color: #0000FF">&#8592;&nbsp;</span><span style="color: #0000FF">agent</span><span style="color: #0000FF">{</span><span style="color: #2B91AF">My&nbsp;Type</span><span style="color: #0000FF">}&nbsp;</span><span style="color: #000000">My&nbsp;Feature</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X</span><span style="color: #0000FF">&#183;</span><span style="color: #000000">My&nbsp;Feature</span><span style="color: #0000FF">(</span><span style="color: #008000">0</span><span style="color: #0000FF">)</span></p>
<p><span style="color: #000000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X</span><span style="color: #0000FF">&#183;</span><span style="color: #000000">Y</span><span style="color: #0000FF">(</span><span style="color: #008000">0</span><span style="color: #0000FF">)</span></p>
<p><span style="color: #000000"></span><span style="color: #0000FF">end</span></p>
<br/>
</div>
</div>
    
Its type is:

+ A procedure type if the feature is a procedure or a creation feature, with parameters matching those of the procedure.
+ A function type if the feature is a function, with parameters and results matching those of the function.
+ A property type if the feature is a property.

 
 