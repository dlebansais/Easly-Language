# Assertion

An assertion is an expression, part of a contract, that indicates what must be true for a contract to be fulfilled. It can appear:

+ In a Require clause.<br>In this case, the assertion is a requirement that must be fulfilled by the caller, or the environment. Assertion expressions can evaluate parameters.
+ In an Ensure clause.<br>In this case, the assertion is a guarantee that is fulfilled by the object, and any result, after operations have been executed.
+ In an invariant.<br>An invariant represents both a requirement and a guarantee, and is always true except while the code associated to the invariant is executed (or an attribute or property modified).
  * In any method of a class for the class invariant.
  * After initialization, and after the iteration part, of For Loop or Over Loop instructions.

The following example demonstrate the use of assertions in Require and Ensure clauses.

<!---
Mode=Default
{BaseNode.FunctionFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{60b80624-e44c-4bc4-97aa-c52d4c852b12}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Square Root"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"All"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{46d1a6b2-b307-4582-ab35-709b36058a4a}"";{2c5eb6f1-3be9-43c0-848a-ddd83593f363}"";{af67b229-afdf-494e-8653-d64b30d376d7}{BaseNode.Block`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{464b6ede-7fd6-4f80-a328-a97e0ee71f08}{BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.EffectiveBody, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{2a9b97f6-6dbc-4cee-9c03-d5c71f22b1c5}"";{0c632f18-c09c-47e8-8573-88fe6caa78e4}"";{3c4636ea-9f5a-488e-8bf0-1c6afe9f30ac}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
False"";{58c04ef7-4d13-40e6-a3b5-f2172cc1cd2b}0x00000000"";{45d55be7-e1e2-4e25-8b10-5c5897c6e0de}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{d2828d37-0bc2-4f2e-8eec-57d2188bf36d}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{03c181d2-1b59-47fe-a02c-60bbdeddb052}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{82fccaf2-dd3a-4ce7-aa81-d51674f81346}0x00000000"";{cf525166-4c9b-42c4-80bb-a4c49a28d95d}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{5d5047ba-6bbe-4d28-b4c1-a9f85808295b}0x00000000"";{e0f80875-618b-47c5-86d0-fe506f3c34dd}0x00000000"";{57f2eaf8-26be-44a0-a452-62d163e740c2}0x00000000"";{cb91820e-95cb-4418-9cc9-3ff5b32abb5b}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{9c1c2a04-46c7-4571-bd23-a0aed39501cb}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3a533b5f-c711-466c-8612-874a73254b68}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{d5094978-fce6-4f18-b946-7d924a261340}"";{b7838181-0c4c-4c39-9549-71ae5db1115e}"";{6e08ae6b-028f-4bf7-99ce-d862645e0bd3}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{a236fc9c-a9d8-4c7d-aaae-c7caab10c76e}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{83a0eb06-c088-4437-bc9c-9f7a0f4136ec}"";{0a75e1bb-77e0-4a27-a8c9-8778886c5736}False"";{ec277826-2ae5-4279-a8dd-4666fe93454a}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.EqualityExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{01f5c5b8-d085-46bb-ad9d-d296a76ae8d4}"";{012b6ac0-200c-4ec8-ba54-a6f7aff5f94a}{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{23b7c88f-52ae-4fdc-b5af-101d929a2a60}"";{37f748e7-ae5a-4829-a415-c6c675e7a960}False"";{a4f10c00-4455-4f4a-b226-0ed2548af391}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{9d6a4300-107f-433a-bf83-a02e7f8ddcf7}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{3402170b-48f3-48c5-bd6d-6a75c28afffa}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{f490a707-6471-4e13-b5df-9e5a441f54bf}False0x00000000;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{9c11f614-591e-470d-b4c2-8fa940c96838}False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{87048515-e81e-4ffc-93ba-108eac0123fd}False"";{502276e9-2bcf-41cb-802f-b246cb6dcf61}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{252f73ab-baa0-4b72-921c-bfda1f261492}"";{41099dbb-2953-4c6d-b049-1652ff763f58}"";{59e91d39-0af9-4029-975d-96cb79c5b9a9}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"≥"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0""";{cb5c99b8-206a-48bf-981a-1a6e41a30f45}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{dbd8ea10-5165-4662-8bdb-55e1bb99106f}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"≥"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0""";{e4f9dba4-3fa6-4419-9f24-958f96c2decc}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{437d76b3-9536-456a-92f0-71d0c0f026df}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{47bfef50-a9ce-4d9b-8b6e-9aa4f07171b6}"";{012995cf-63fe-42a1-91e9-313d6b4535cc}"";{81ef5697-4dd7-4667-8873-5e9c213c5e97}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{70a4f4d4-b9d0-410d-b440-e322cb40caff}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{79bf7f03-c495-4ebe-a724-a2e8b6e3d276}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{b5f38ff5-8f74-4c48-839b-53a011199ed0}"";{3f6cdd31-4c1b-46cc-be7a-cc2f1d1c40c4}"";{c1ca0da1-ad36-4983-9ca2-f73a5a448096}0x00000000"";{1805c2b9-6b48-457a-8751-02d09b46322c}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{02db8f04-828f-406e-9310-b5ae59b524a8}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{5fa0ded8-76d5-43c3-8300-23217802b7c0}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{477fa809-4345-4ae6-9b90-07dcf79a4462}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{b087f371-5fae-4a00-ad98-e1583870aad1}0x00000000"";{8be2409a-4040-4ab3-a2e5-be4a1b4236b3}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{68e200ad-b23d-44f1-b71d-db9c9d1a3809}0x00000000"";{9731bd2a-a79d-49ad-8318-7ca2899fe44b}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result""";{4180b596-e2a2-47c7-a589-a72a54e3c103}0x00000000"";{643e48b4-d2a3-4f43-b0be-22b9797cc857}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{bcfd86f1-37f6-4108-a1f7-92727ce865b2}0x00000000"";{e7a47a95-2a76-454d-b09d-1d357f797199}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X""";{9e9f160c-4ba2-43f0-baf0-ff93006d064d}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result""";{1a7bb50b-ec5a-4f99-820a-a5f90a82bde3}"";{66ccbfc4-626d-4f19-bb0d-654a1a2ff30a}"";{63227146-d03c-4fcf-80e0-9757e38328ea}"";{53b1ead3-5cbd-497b-8ab4-d4a31df7d534}
-->

<pre>
Square&nbsp;Root&nbsp;<b>function</b>
&nbsp;&nbsp;&nbsp;<b>parameter</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>result</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>require</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&nbsp;&#8805;&nbsp;0
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;<b>ensure</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;&#8805;&nbsp;0
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>(</b>Result&nbsp;*&nbsp;Result<b>)&nbsp;</b>=<b>&nbsp;</b>X
<b>end</b>
</pre>

An assertion expression must have a single result, and conform or convert to boolean.

It is evaluated at compile time, when possible. If the compiler cannot evaluate the expression, it can instead insert a debug instruction that will compare the expression to `True` in the code.

Sometimes, executing a debug instruction can take so much time it's not practical. If the compiler is able to detect it, the assertion can be replaced with a simple comment instead.

An assertion can have an optional tag:

<!---
Mode=Default
{BaseNode.FunctionFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{60b80624-e44c-4bc4-97aa-c52d4c852b12}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Square Root"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"All"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{46d1a6b2-b307-4582-ab35-709b36058a4a}"";{2c5eb6f1-3be9-43c0-848a-ddd83593f363}"";{af67b229-afdf-494e-8653-d64b30d376d7}{BaseNode.Block`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{464b6ede-7fd6-4f80-a328-a97e0ee71f08}{BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.EffectiveBody, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{2a9b97f6-6dbc-4cee-9c03-d5c71f22b1c5}"";{0c632f18-c09c-47e8-8573-88fe6caa78e4}"";{3c4636ea-9f5a-488e-8bf0-1c6afe9f30ac}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
False"";{58c04ef7-4d13-40e6-a3b5-f2172cc1cd2b}0x00000000"";{45d55be7-e1e2-4e25-8b10-5c5897c6e0de}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{d2828d37-0bc2-4f2e-8eec-57d2188bf36d}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{03c181d2-1b59-47fe-a02c-60bbdeddb052}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{82fccaf2-dd3a-4ce7-aa81-d51674f81346}0x00000000"";{cf525166-4c9b-42c4-80bb-a4c49a28d95d}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{5d5047ba-6bbe-4d28-b4c1-a9f85808295b}0x00000000"";{e0f80875-618b-47c5-86d0-fe506f3c34dd}0x00000000"";{57f2eaf8-26be-44a0-a452-62d163e740c2}0x00000000"";{cb91820e-95cb-4418-9cc9-3ff5b32abb5b}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{9c1c2a04-46c7-4571-bd23-a0aed39501cb}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3a533b5f-c711-466c-8612-874a73254b68}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{d5094978-fce6-4f18-b946-7d924a261340}"";{b7838181-0c4c-4c39-9549-71ae5db1115e}"";{6e08ae6b-028f-4bf7-99ce-d862645e0bd3}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{a236fc9c-a9d8-4c7d-aaae-c7caab10c76e}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{83a0eb06-c088-4437-bc9c-9f7a0f4136ec}"";{0a75e1bb-77e0-4a27-a8c9-8778886c5736}False"";{ec277826-2ae5-4279-a8dd-4666fe93454a}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.EqualityExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{01f5c5b8-d085-46bb-ad9d-d296a76ae8d4}"";{012b6ac0-200c-4ec8-ba54-a6f7aff5f94a}{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{23b7c88f-52ae-4fdc-b5af-101d929a2a60}"";{37f748e7-ae5a-4829-a415-c6c675e7a960}False"";{a4f10c00-4455-4f4a-b226-0ed2548af391}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{9d6a4300-107f-433a-bf83-a02e7f8ddcf7}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{3402170b-48f3-48c5-bd6d-6a75c28afffa}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{d8f4cf8e-a3ee-4743-bf8a-361495e91c04}True {BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000000;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{9c11f614-591e-470d-b4c2-8fa940c96838}False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{ba49c5d7-ed4d-43fd-929f-e1732e414c3b}True {BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{502276e9-2bcf-41cb-802f-b246cb6dcf61}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{252f73ab-baa0-4b72-921c-bfda1f261492}"";{41099dbb-2953-4c6d-b049-1652ff763f58}"";{e0febfc4-0d35-4814-9a14-57f5e3c18da3}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"≥"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Valid Output""";{cb5c99b8-206a-48bf-981a-1a6e41a30f45}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{44e5b872-b243-4bf2-b6a0-46ba099d859d}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"≥"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Valid Input""";{e4f9dba4-3fa6-4419-9f24-958f96c2decc}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{6a880a0d-82dc-4ff9-aaa5-9f41149d4e61}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{04f35df4-0eef-4a26-a266-83c988d9acf6}"";{2f3e87fc-0417-4b5a-8e59-989ae20529d7}"";{6ccda120-a227-4f0e-abcb-da9d7d59d9a9}"";{81ef5697-4dd7-4667-8873-5e9c213c5e97}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{70a4f4d4-b9d0-410d-b440-e322cb40caff}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{aca278df-83de-450d-8344-2df9781e37c6}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{0eb0e0cb-3d70-4a86-bcc7-316c30c56df2}"";{14f36134-9b99-47f1-afa3-7e66e485aac5}"";{b074797d-6e08-4a04-9856-70cca4d4652d}"";{45ddffa5-f88b-4332-af76-02704303fb80}0x00000000"";{54ebb318-e011-40ef-9296-33277762c1b2}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{02db8f04-828f-406e-9310-b5ae59b524a8}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{5fa0ded8-76d5-43c3-8300-23217802b7c0}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{477fa809-4345-4ae6-9b90-07dcf79a4462}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{b087f371-5fae-4a00-ad98-e1583870aad1}0x00000000"";{8be2409a-4040-4ab3-a2e5-be4a1b4236b3}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{2bf80051-e193-4297-b95a-9dc131c35935}0x00000000"";{a24ff713-cfb4-4a0e-aa64-34999d0bd4ed}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result""";{4180b596-e2a2-47c7-a589-a72a54e3c103}0x00000000"";{643e48b4-d2a3-4f43-b0be-22b9797cc857}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{bcfd86f1-37f6-4108-a1f7-92727ce865b2}0x00000000"";{e7a47a95-2a76-454d-b09d-1d357f797199}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X""";{63314d97-716e-4cc9-97ba-fdbaffe4f0d1}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result""";{1a7bb50b-ec5a-4f99-820a-a5f90a82bde3}"";{fce3b554-1f45-46eb-8e31-b310ef19d970}"";{63227146-d03c-4fcf-80e0-9757e38328ea}"";{53b1ead3-5cbd-497b-8ab4-d4a31df7d534}
-->

<pre>
Square&nbsp;Root&nbsp;<b>function</b>
&nbsp;&nbsp;&nbsp;<b>parameter</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>result</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>require</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Valid&nbsp;Input<b>:&nbsp;</b>X&nbsp;&#8805;&nbsp;0
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;<b>ensure</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Valid&nbsp;Output<b>:&nbsp;</b>Result&nbsp;&#8805;&nbsp;0
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>(</b>Result&nbsp;*&nbsp;Result<b>)&nbsp;</b>=<b>&nbsp;</b>X
<b>end</b>
</pre>

Tags are used to document the purpose of the contract, but also to reuse an assertion that was defined in a precursor. In the example above, if the `Square Root` feature is overriden in a descendant of the class, the overriding feature can refer to original assertions through their tag (See Assertion Tag expression).

## Assertions in types

Procedure, function, property and indexer types can include assertions. When one is present, the equivalent assertion must be present in the agent using this type (see Agents), and it means an implied contract every time a variable of that type is used.

For example, the following procedure type includes a requirement on the first parameter:

<!---
Mode=Default
{BaseNode.ProcedureType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.ICommandOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.CommandOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{d57f6819-bf8c-483b-8829-2e84b95b44fa}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ICommandOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.CommandOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"My Type""";{7e5f35e1-1993-4553-9595-458b140deeee}"";{163be116-7cec-46b7-847a-853a00048b13}{BaseNode.Block`2[[BaseNode.ICommandOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.CommandOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{f3b88275-5213-43ea-915a-cea10bedc00a}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.ICommandOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{2fee9397-0e2c-4d5b-bfcf-bb711e2919f3}{BaseNode.CommandOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{030c9e0c-3447-48c4-a5b1-ec91d602ff13}"";{93982fcd-8bee-46dc-95fe-53980acd732d}"";{eda756fe-d4f7-4f89-a12b-c48706e9f9e1}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{29fafa08-2c89-4881-9b39-0aaa1fe473a8}0x00000000"";{f018ec8b-364d-4f88-8c1b-1a4adacbed7b}0x00000000"";{a35dfec2-1293-4f7b-b21d-8d17600c22f2}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{ce05e007-8e56-450b-96d2-e55755aaed2c}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{bf4f8bac-5480-4171-8891-bd886622fb13}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{37c679ef-60b8-4084-9d6e-c744beb8a0e3}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{ba8ebf9d-bfcc-45a6-9a87-29160052b9d3}"";{8ae81a79-50bc-4b42-a84c-c940075cdbba}{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{fd64365d-9e8e-415c-b748-809e10191947}"";{d27ddc2a-6945-4fbc-8d72-4cd1192388c1}False"";{96ebbd23-faaf-498d-985d-fc1ba7aea18f}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{c1c28a38-6250-40ed-a83b-9d6d7a08fe96}True {BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{29aec30d-428c-454a-9af3-cb4479f33469}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{f33f8d81-6b0c-4c2d-b542-5519684817aa}"";{e403b270-fee6-444e-85c6-613e87e0fa0b}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"≥"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Valid Input""";{77f17ad2-fb34-47c5-ac40-ba0877955adc}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{75ffa599-cb82-4c5b-875b-165e5a162e87}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{3a0569f9-b9ca-42f1-88f8-c215a424577d}"";{71638f9a-a59f-4e5d-9cbf-c474ab6772f8}"";{627bb976-44f0-4a89-b2cf-a06763311140}"";{ac26e400-17e6-4c57-824b-64626c6a1964}0x00000000"";{85257dce-59b2-43ff-b3d5-908cdc26fb23}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X""";{40535155-6b25-42bc-985b-9b4358d17c6a}
-->

<pre>
<b>procedure&nbsp;</b><i>My&nbsp;Type&nbsp;</i><b>&#9484;parameter&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9488;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9474;&nbsp;&nbsp;&nbsp;</b>X<b>:&nbsp;</b><i>Number&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</i><b>&#9474;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9474;require&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9474;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9474;&nbsp;&nbsp;&nbsp;</b>Valid&nbsp;Input<b>:&nbsp;</b>X&nbsp;&#8805;&nbsp;0<b>&#9474;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9492;end&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9496;</b>
</pre>

This means an agent of this type can be assigned the `Square Root` feature, and at the point where the agent is used, the compiler can evaluate the contract and ensure the parameter fits the requirement (is greater than or equal to zero).

Note that the type doesn't include any ensure assertion. In that case, where the agent is used, the compiler will not be able to use the additional info that the `Square Root` feature provide. Taking the square root of the square root for example would fail, since the intermediate result is not guaranteed to be positive.

## Attribute feature assertions

An attribute can have an ensure clause:

<!---
Mode=Default
{BaseNode.AttributeFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{46b2933e-d84d-457e-a456-5ec53c46fb66}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"N"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"All""";{9840331f-c272-465b-801d-1edf794ad3eb}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{0f05dde9-ee08-4f7c-be7b-7b1e572436a4}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{e8719afa-66f3-4825-a691-9f24b34631c9}"";{7ea15f14-f6ff-4012-b362-410748432fc3}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{44a7ac08-3c11-4204-a95b-78c28dd88311}"";{dbedd4eb-4413-42d3-9237-d2d017524ebf}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{1002179b-28f1-4d6f-87c5-f63b6020bd03}"";{427130cc-8c49-4f74-aa55-da1f3657a241}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{e0c5bc3b-2904-4d7b-a4bf-635369289ce0}False"";{db710aa9-4b0f-4b40-aa62-9bef356acc35}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;">"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{b95ed7e1-ed24-4b85-b030-14f8b25290f4}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{cee8d67e-6cc1-4c73-80db-9584074af4c8}"";{a53b2a92-b0f1-4961-a999-60c355d90999}"";{62552fd1-954f-4176-b5de-c2b8795dc3c0}0x00000000"";{83003417-b8c7-4894-ae42-d1e343747fe8}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"N""";{a470a8cd-9149-4ac8-b36b-73987877a46e}
-->

<pre>
N<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>ensure</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;N&nbsp;>&nbsp;0
<b>end</b>
</pre>

In this case, assertions serve both to ensure a contract to anyone reading the value, but also as a requirement for any code changing the attribute.

# Translation to C&#35;

Currently, the technology is not available to evaluate assertions at compile time, and even the simplest case are not implemented that way. Instead, each assertion is translated to a call to `Debug.Assert(...);` and the compiler doesn't check how long executing `Assert` will take.

If a tag is present, a comment is added with the tag.

For assertions in a feature contract, or a class invariant, a summary of the contract is also added, in comments.

```csharp
    // Require:
    //   Valid Input: X ≥ 0
    // Ensure:
    //   Valid Output: Result ≥ 0
    //   (Result * Result) = X
    public virtual double SquareRoot(double X)
    {
        Debug.Assert(X >= 0); // Valid Input

        double Result = default;

        /*...*/

        Debug.Assert(Result >= 0); // Valid Output
        Debug.Assert((Result * Result) == X);

        return Result;
    }
```

For assertions in the class invariant, a dedicated method is added to the class, and called at the end of all other methods of the class.

```csharp
    #region Invariant
    protected virtual void CheckInvariant()
    {
        Debug.Assert(Invariant1);
        Debug.Assert(Invariant2);
        /*...*/
    }
    #endregion
```

```csharp
    /*...*/

    Debug.Assert(Result >= 0); // Valid Output
    Debug.Assert((Result * Result) == X);

    CheckInvariant();

    return Result;
```

For assertions in types, no code is added, because the assertion is tested inside the callee.

For assertions in attribute features, a special procedure is added to the code, to check assertions and then set the attribute. This procedure is called instead of just setting the attribute value.

```csharp
    // Ensure:
    //   My Attribute ≥ 0
    public double MyAttribute { get; private set; }
    private void Set_MyAttribute(double value)
    {
        Debug.Assert(MyAttribute >= 0);

        MyAttribute = value;
    }
```

```csharp
    ...
    Set_MyAttribute(1.0);
    ...
```
