# Assignment Type Argument

When declaring a class type with generic arguments, such as `List[Number]`, a generic argument can be provided with the name of the formal generic parameter it's assigned to:

<!---
Mode=Default
{BaseNode.Class, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{0c91525c-e934-4a94-8f78-cbaed128a95f};"easly.Classes";{BaseNode.BlockList`2[[BaseNode.IClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;0x00000000;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000001;{BaseNode.BlockList`2[[BaseNode.IDiscrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Discrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IExport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Export, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Feature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IImport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Import, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Inheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;False;{BaseNode.BlockList`2[[BaseNode.ITypedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Typedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IDiscrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Discrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{b0d72e3e-a449-4448-bb46-1dd6caa6c6e6}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"List"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IExport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Export, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Feature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IImport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Import, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Inheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ITypedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Typedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{2d0a57bc-763d-438c-85f6-c27d9680b47e}0x00000000"";{78ae47cb-aa69-46a8-b029-de06d4f067fd}0x00000004"";{03a46a9d-dc0b-44ba-a01d-b2d2bec377cd}0x00000000"";{0f6d65df-78c5-454d-8273-19647e42e1ad}"";{52c5a627-3e03-4e5d-a803-103ffaf42f3c}0x00000000"";{b837402a-85eb-4f8c-8331-6f21423e626a}0x00000004"";{240cd251-c454-45ef-9bfd-188a48a0ee9e}{BaseNode.Block`2[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{a9985185-4e66-4e8a-82ed-37e3181a176e}0x00000004"";{9e8b90db-c959-4215-b5ea-87b453810ec9}0x00000000"";{2e18c4c0-80d1-404a-ada7-43f069a556aa}0x00000000"";{ebb1bbd8-8f0e-436f-8457-4ac6b0320b90}0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{ed04bba4-68f6-4da8-b356-4607014c1e6a}{BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.BlockList`2[[BaseNode.IConstraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Constraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{c3352a45-05d6-4125-a928-6cfe3d13a29e}"";{90dc9084-4e5a-46ef-9773-25ddede243ef}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IConstraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Constraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
False"";{d0aa504d-cbb6-4c2d-9b55-4a9d9ad5f11a}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"T""";{81ce8c7a-2e2b-4d0a-8d72-ed4f1605a6c2}0x00000000"";{fd2a47fc-bcdd-4855-9668-7b534e5e0730}
-->

<pre>
<b>class&nbsp;</b>List
<b>generic</b>
&nbsp;&nbsp;&nbsp;T
<b>end</b>
</pre>

<!---
Mode=Default
{BaseNode.AttributeFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.GenericType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{f87f4bfc-a814-4a8c-b906-49be19307456}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"All""";{7ce7ad8f-8cf7-4d71-8b7b-7e2b5553f628}0x00000000"";{17c9d1d4-ed25-405d-a754-cdbf428c8141}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"List""";{0f8839d8-9511-40c8-8982-3a8cd20c3631}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{dd7b1d55-ca51-4919-bf99-45e0bb1ba2cb}"";{c9bd49cc-a369-4860-8432-93b1f7a18372}"";{27901666-66f4-49f3-a252-2b309bc945ba}{BaseNode.Block`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{f4867432-8a99-49a4-aecc-d354b88df256}{BaseNode.AssignmentTypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{4e8683da-b749-43ac-a355-0e6a22eb4483}"";{285343fe-763e-415c-a9ba-028741f0a3e3}"";{c3ba6d0a-a060-4ad1-aa3f-e3ef495edad9}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"T"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{ab30e39b-21f1-4470-aa11-cf9ace836bec}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{69250d89-075f-4fd5-beed-06461a2b1bdc}"";{4912d586-a3f6-4c57-9c19-2caf91d7d750}
-->

<pre>
X<b>:&nbsp;</b>List<b>[</b>T&nbsp;<b>&#8592;&nbsp;</b><i>Number</i><b>]</b>
</pre>

The generic name is usually omitted, and most source code use [Positional Type arguments](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/TypeArgument/PositionalTypeArgument.md).
Note that the two notations cannot be mixed. When declaring a generic type, one must use either all assignment or all positional type arguments.

## Benefits

The benefits of using assignment type arguments are:

+ The intent of the declaration, and which type parameter is assigned to what is explicit.
+ Type parameters can be assigned out of order.

## Default value

If one of the type parameters has a default value, it can be ommitted in the declaration:

<!---
Mode=Default
{BaseNode.Class, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{0c91525c-e934-4a94-8f78-cbaed128a95f};"easly.Classes";{BaseNode.BlockList`2[[BaseNode.IClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;0x00000000;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000001;{BaseNode.BlockList`2[[BaseNode.IDiscrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Discrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IExport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Export, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Feature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IImport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Import, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Inheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;False;{BaseNode.BlockList`2[[BaseNode.ITypedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Typedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IDiscrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Discrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{b0d72e3e-a449-4448-bb46-1dd6caa6c6e6}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Dictionary"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IExport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Export, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Feature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IImport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Import, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Inheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ITypedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Typedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{2d0a57bc-763d-438c-85f6-c27d9680b47e}0x00000000"";{78ae47cb-aa69-46a8-b029-de06d4f067fd}0x00000004"";{03a46a9d-dc0b-44ba-a01d-b2d2bec377cd}0x00000000"";{0f6d65df-78c5-454d-8273-19647e42e1ad}"";{52c5a627-3e03-4e5d-a803-103ffaf42f3c}0x00000000"";{b837402a-85eb-4f8c-8331-6f21423e626a}0x00000004"";{240cd251-c454-45ef-9bfd-188a48a0ee9e}{BaseNode.Block`2[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{a9985185-4e66-4e8a-82ed-37e3181a176e}{BaseNode.Block`2[[BaseNode.IImport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Import, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{9e8b90db-c959-4215-b5ea-87b453810ec9}0x00000000"";{2e18c4c0-80d1-404a-ada7-43f069a556aa}0x00000000"";{ebb1bbd8-8f0e-436f-8457-4ac6b0320b90}0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IImport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{ed04bba4-68f6-4da8-b356-4607014c1e6a}{BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{8f6ad520-5254-46b2-b233-5cc4f678c0d7}{BaseNode.Import, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.BlockList`2[[BaseNode.IConstraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Constraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IConstraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Constraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{c3352a45-05d6-4125-a928-6cfe3d13a29e}"";{90dc9084-4e5a-46ef-9773-25ddede243ef}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IRename, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Rename, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{d000a4f2-5115-4777-9652-b3b00b5d2ca1}"";{e012f2e4-3959-4742-9810-770333ad4806}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IConstraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Constraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
True {BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{d0aa504d-cbb6-4c2d-9b55-4a9d9ad5f11a}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Key"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IConstraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Constraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
False"";{240ed674-5ae6-4ce2-848e-b81fd2ae529d}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Value""";{755e51da-887d-4dbc-be0f-39f59412a4ee}False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Language"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IRename, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Rename, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{81ce8c7a-2e2b-4d0a-8d72-ed4f1605a6c2}0x00000000{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{fd2a47fc-bcdd-4855-9668-7b534e5e0730}"";{cac1fa78-3a03-4da4-807c-5b4b4451ed18}0x00000000"";{c3833eee-f8e6-4da9-b6a7-7c197d828c82}"";{dc96f6b1-26cd-4eb9-80e7-97debfb818e5}"";{b2cfd861-3ec5-45b4-b81f-1aaa4cc3eb89}0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"String""";{0f35684a-3e83-4aee-b442-87ff3d814bc8}"";{eacfb529-9544-4a4f-9b83-fa62d7e74741}
-->

<pre>
<b>class&nbsp;</b>Dictionary
<b>import</b>
&nbsp;&nbsp;&nbsp;latest&nbsp;Language
<b>generic</b>
&nbsp;&nbsp;&nbsp;Key&nbsp;<b>=&nbsp;</b><i>String</i>
&nbsp;&nbsp;&nbsp;Value
<b>end</b>
</pre>

<!---
Mode=Default
{BaseNode.EffectiveBody, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{bdc91731-a5e2-4c9e-8c5e-19d5b98d8890}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{81800545-f876-4bfd-aa33-1f14f64abaac}0x00000000"";{aeaf2977-d876-4c53-b937-6bdf638e39dd}0x00000000"";{a4e3b03f-919b-4718-afa1-5629b4788c58}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{b20f4c4c-278f-4d99-bc74-3d370423865d}0x00000000"";{4931df78-5fe7-40fb-bcc7-f44fdeec827e}0x00000000"";{67eb306b-5218-4fc7-9faa-d0d3bbe54af4}0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{71e919cc-0a1c-4875-8812-19ccf21af40c}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.GenericType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.GenericType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{7a31402c-a966-4140-9d89-b5ea10a5ab4f}"";{ec88bee7-5401-45c7-bfa5-2009ceb5274b}False"";{65a7e8e1-1c2b-441d-81d4-59110afc8a4b}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
False"";{08c2c7b6-3e10-4354-89fc-625e13d50363}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Y"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{e8989a84-0448-4e97-a74a-612c89626a96}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Dictionary""";{668349c9-4252-441f-9bd2-3ddf144c36d0}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{2c0ca144-410b-4aed-ae00-60f0ef404464}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Dictionary""";{9e541096-4347-421d-80b5-b768261d55ed}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{281141bf-9fc5-4c1e-ba1f-a234a0a3fd22}"";{47353e0c-91ef-4d52-86e8-9a2b2397d972}{BaseNode.Block`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{54d7d2a4-0415-4b80-b89e-40bddd2b4f36}"";{4fd68ea1-4d93-4609-923f-11f0a43702eb}{BaseNode.Block`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3989dd2a-5228-4590-91f1-ef6756540831}{BaseNode.AssignmentTypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.AssignmentTypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{3fb5cd6e-93cb-4938-b2fe-7b52217a1296}{BaseNode.AssignmentTypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{121b26a1-0924-45d9-8f6d-1fc7466ede5f}"";{dfadcd05-9111-4b3f-9e4c-84aa03d5fca0}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{8b7e55f5-2142-4911-8e46-be73bca52549}"";{c0584bc6-bec6-4d9b-ba59-dee647971f54}"";{2a676ca1-4cd5-44b9-9db5-534f3fe9a9e7}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Value"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{29967893-94a9-405e-804d-2be06b1d44a9}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Key"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{f3278afd-25f2-4d0b-8c1e-df6338e0f59b}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Value"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{44b7ad54-1002-4074-bcb2-bcd16707fdfb}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{53bf2b88-121c-4958-bddf-55a29c77718f}"";{db06712f-86d8-41dd-9a88-39cf3c2d2712}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{5bfcb709-241a-467f-9e33-0a30a3b10f64}"";{5b25e1dc-337a-4211-8bf8-843c0eb73da5}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{73d81bab-030e-4153-b384-546f11f0d1d7}"";{e6972527-c4ef-483e-a003-f82dbb1bee85}"";{d670a60d-f291-468d-963a-b2a2643ef6cd}"";{c3fe5cd9-4afe-4225-a86e-51088bf32df1}
-->

<pre>
<b>local</b>
&nbsp;&nbsp;&nbsp;X<b>:&nbsp;</b>Dictionary<b>[</b>Value&nbsp;<b>&#8592;&nbsp;</b><i>Number</i><b>,&nbsp;</b>Key&nbsp;<b>&#8592;&nbsp;</b><i>Number</i><b>]</b>
&nbsp;&nbsp;&nbsp;Y<b>:&nbsp;</b>Dictionary<b>[</b>Value&nbsp;<b>&#8592;&nbsp;</b><i>Number</i><b>]</b>
<b>do</b>
</pre>

# Translation to C&#35;

Translating type arguments to C# code is straightforward. Once the proper argument order has been verified, type arguments distributed to all generic parameters, and the default value extracted from type parameters if not assigned explicitely, type arguments are just written separated by comma.

However, since C# use a hierarchy where interface and implementation are separate, the compiler always include two C# type for one Easly type: the interface type, and the class type. This results in code looking as follow:

<!---
Mode=Default
{BaseNode.Class, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{0c91525c-e934-4a94-8f78-cbaed128a95f};"easly.Classes";{BaseNode.BlockList`2[[BaseNode.IClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;0x00000000;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000001;{BaseNode.BlockList`2[[BaseNode.IDiscrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Discrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IExport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Export, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Feature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IImport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Import, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Inheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;False;{BaseNode.BlockList`2[[BaseNode.ITypedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Typedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ClassReplicate, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IDiscrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Discrete, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{b0d72e3e-a449-4448-bb46-1dd6caa6c6e6}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Type 1"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IExport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Export, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Feature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IImport, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Import, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Inheritance, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ITypedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Typedef, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{2d0a57bc-763d-438c-85f6-c27d9680b47e}0x00000000"";{78ae47cb-aa69-46a8-b029-de06d4f067fd}0x00000004"";{03a46a9d-dc0b-44ba-a01d-b2d2bec377cd}0x00000000"";{0f6d65df-78c5-454d-8273-19647e42e1ad}"";{52c5a627-3e03-4e5d-a803-103ffaf42f3c}0x00000000"";{b837402a-85eb-4f8c-8331-6f21423e626a}0x00000004"";{240cd251-c454-45ef-9bfd-188a48a0ee9e}{BaseNode.Block`2[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{a9985185-4e66-4e8a-82ed-37e3181a176e}0x00000004"";{9e8b90db-c959-4215-b5ea-87b453810ec9}0x00000000"";{2e18c4c0-80d1-404a-ada7-43f069a556aa}0x00000000"";{ebb1bbd8-8f0e-436f-8457-4ac6b0320b90}0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IGeneric, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{b76054f4-3bc2-406a-ac1f-957fa3c02a2e}{BaseNode.Generic, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.BlockList`2[[BaseNode.IConstraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Constraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{415e02f6-9706-41ea-9e5d-4cfa3af3bbec}"";{003063d6-4d61-4eda-b500-132aa9874a3b}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IConstraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Constraint, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
False"";{f135264c-c00e-4e09-ab93-8c09a3fedbf8}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"T""";{d753416d-dad4-4288-97cd-0bf877ba68e6}0x00000000"";{e52216ac-e7ec-472c-b743-cacde9c39184}
-->

<pre>
<b>class&nbsp;</b>Type&nbsp;1
<b>generic</b>
&nbsp;&nbsp;&nbsp;T
<b>end</b>
</pre>

<!---
Mode=Default
{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.GenericType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
False"";{b6a08734-129b-4f7c-ad25-689b2c674f34}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{ea9b321a-5122-4e3e-9144-535a77ced0c5}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Type 1""";{d5261936-0f21-46da-a140-4cb1758a76f1}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{65b38916-0e34-4367-8d0b-6753c9ffa69f}"";{5f92c100-7d36-40a7-83b6-c6655e76ffee}{BaseNode.Block`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{8975cf90-c743-4386-a311-d471ee88bafc}{BaseNode.AssignmentTypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3d6a7ce7-7686-4d45-8dff-a992d93f0957}"";{2087b558-fe4c-4925-9a49-a9cb68c0e1a3}"";{e3fd90d5-dcda-4652-82e3-1f4f3877381c}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"T"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{e7d25633-4899-4c06-99de-f009e5e42f23}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Type 2""";{944af782-a733-43cd-8c0d-7f9d92da0e56}"";{f2296d01-1edf-4ff4-9714-38a96ab3fbc3}
-->

<pre>
X<b>:&nbsp;</b>Type&nbsp;1<b>[</b>T&nbsp;<b>&#8592;&nbsp;</b><i>Type&nbsp;2</i><b>]</b>
</pre>

```csharp
Type_1<IType_2, Type_2> X;
```
