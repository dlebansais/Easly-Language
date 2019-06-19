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

## Assertion Tag

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

Tags are used to document the purpose of the contract, but also to reuse an assertion that was defined in a precursor. In the example above, if the `Square Root` feature is overriden in a descendant of the class, the overriding feature can refer to original assertions through their tag (See [Assertion Tag Expression](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Expression/AssertionTagExpression.md)).

## Assertions in types

Procedure, function, property and indexer types can include assertions. When one is present, the equivalent assertion must be present in the agent using this type (see Agents), and it means an implied contract every time a variable of that type is used.

For example, the following function type includes a requirement on the first parameter:

<!---
Mode=Default
{BaseNode.FunctionType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IQueryOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{83c5b11c-b895-41d0-b7af-db3f784bea35}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQueryOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"My Type""";{ce9e0a72-564c-4eac-9a9a-393df42197bb}"";{1976fbfa-6d82-4404-b00c-97f2d98b021e}{BaseNode.Block`2[[BaseNode.IQueryOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{8edafcfc-b833-41ad-9204-381e0d84332d}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQueryOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{02ca5253-c78d-4be7-90b5-8076c090dcec}{BaseNode.QueryOverloadType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{f0c6f8f6-6b65-49fe-b48e-974ee8dd2593}"";{72736af5-9453-490c-9ade-1f465b2f4489}"";{200b742d-cce7-4b08-a3a5-6ea6745c6afd}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{e36f5469-f745-4d90-899d-de9254186476}0x00000000"";{3d4bd538-c01a-4953-b410-98de9498a8c8}0x00000000"";{66b76077-ca18-46a1-b862-5a366d4bb059}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{e689d7ca-fcdd-49c1-a37a-8404e3c1aa9f}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{a007d575-8b10-4101-9d55-4f8088dbdace}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{dc167b85-5e01-41f9-8921-87a1344b7c1e}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{94b7b1a9-ea3b-43c1-9b40-e9d26998f86d}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{edda2a0a-dd1f-4239-9d90-d136d81e9468}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{073fea44-e3c0-4a27-ad6f-7a8d707b9028}"";{9cbb4089-ad5a-42e0-bb00-3848fd21dcf5}{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{587e2da3-c839-4905-ac78-a0f45b41b83f}"";{cdce8b17-0e8d-46ea-9277-b26db2a48c77}{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{f23fe112-f337-4f65-8f91-0ea821022c63}"";{cde402db-44ba-4562-b7e6-c40b439d829d}False"";{18d7a6f4-91c9-4848-9901-dfec59f8935d}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{2a6c7d17-1f04-4bc3-acea-aa5983df0806}True {BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
False"";{a9315797-db0c-459a-9106-d4875851b289}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{10092297-56ea-42b5-827e-f3a027cbc55a}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{ef7f6fb7-2181-432a-b832-db91b7f3fe25}"";{dfd1b42b-fe7e-413c-bcb8-5679b4967145}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"≥"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Valid Input""";{b01f3866-836f-41fe-93fd-b274d10eea3d}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{0e4e9ab2-cafd-4071-917b-f5e50104d56a}"";{cbefdf7d-af25-4e67-b9cc-3d5c45cdfca3}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{6c98df6c-3da7-4989-8ae1-1ec2079f87e6}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{3a0ed91b-f0d4-47a9-be19-29dced111615}"";{e5419bce-27e2-4856-ac98-e705906f0ebb}"";{88c1c11c-fbed-403c-844e-e15dd79fa310}"";{f0cc4276-90d5-4d0a-875a-7bdae7fdb40a}"";{7d962d27-6ba1-4514-b104-fdd0ba8e4e17}0x00000000"";{52137264-2960-4bd6-bb90-17ec6676cc38}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X""";{ab6c377f-a84d-48ed-9423-9c426c02ae49}
-->

<pre>
<b>function&nbsp;</b><i>My&nbsp;Type&nbsp;</i><b>&#9484;parameter&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9488;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9474;&nbsp;&nbsp;&nbsp;</b>X<b>:&nbsp;</b><i>Number&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</i><b>&#9474;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9474;return&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9474;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9474;&nbsp;&nbsp;&nbsp;</b>Result<b>:&nbsp;</b><i>Number&nbsp;&nbsp;&nbsp;&nbsp;</i><b>&#9474;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9474;require&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9474;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9474;&nbsp;&nbsp;&nbsp;</b>Valid&nbsp;Input<b>:&nbsp;</b>X&nbsp;&#8805;&nbsp;0<b>&#9474;</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&#9492;end&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#9496;</b>
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

## Assertions and inheritance

If a feature is redefined in a descendant class, the redefined feature automatically receives a copy of its precursor. However, the redefined feature might want to relax some of the requirements, and improve on what the precursor ensures.

To achieve this, the redefined feature just need to includes its own Require or Ensure clause. In that case, assertions from the precursor are not copied and a new set of assertions must be included explicitely in the descendant.

To help with this task, the redefined feature can use [Assertion Tag expressions](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Expression/AssertionTagExpression.md), and the code of original assertions will be reused.

# Translation to C&#35;

Currently, the technology is not available to evaluate assertions at compile time, and even the simplest case are not implemented that way. Instead, each assertion is translated to a call to `Debug.Assert(...);` and the compiler doesn't check how long executing `Assert` will take.

If a tag is present, a comment is added with the tag.

## Contract comments

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

## Class invariant

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

## Assertion in types
In this case, no code is added, because the assertion is tested inside the callee.

## Attribute ensure clause

For assertions in attribute features, a special procedure is added to the code, to check assertions and then set the attribute. This procedure is called instead of just setting the attribute value.

```csharp
    // Ensure:
    //   My Attribute ≥ 0
    public double MyAttribute { get; private set; }
    private void Set_MyAttribute(double value)
    {
        MyAttribute = value;

        Debug.Assert(MyAttribute >= 0);
    }
```

```csharp
    ...
    Set_MyAttribute(1.0);
    ...
```

## For loop

Assertions in a For Loop instructions are added after the initialization part, and after the iteration part.

<!---
Mode=Default
{BaseNode.ForLoopInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{ee47faf9-749a-44e3-a540-0e5a0ddadf40}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{b9a77fab-c0c4-4ca8-99d3-3f6c2ba9e49d}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{ab2fa919-cd10-4cc8-8ab6-9a46e248985c}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{b2f6b828-bcce-4c7a-ac42-331d335dc0c8}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{f067e4f7-4a28-4dfc-a028-36874518b95b}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{52ba3c98-9703-4295-b3ef-edd48e757ca6}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{b6882f69-bc81-474b-be34-fe5b9db80caf}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"<"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"10"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{812150aa-5e30-41f7-b363-e1f4d9392e43}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{75792c3a-ea7a-4f4b-b3f3-4253a47e7eaf}"";{6fe5280a-67b9-4f74-b847-9b4a63db938a}"";{9a1bc17e-f054-4067-8717-bc3b4cbc0a84}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{97aad8a6-338f-4f94-900c-b9e39616223e}{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{6cf554f0-fa52-4fa2-a117-0e61ce03f412}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{10b38c5a-57a4-4542-bc09-5c8a4b3af243}{BaseNode.CommandInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{6098730c-c3e5-4f0b-84c4-11ae336e3fb9}{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{1812db23-8a16-4b69-a37f-0e1eb42e438e}0x00000000"";{2c2b1c22-a26d-4b43-8f8d-1fafbd6b2660}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{625a78a8-88bf-46a5-91f3-c48b760eaf80}"";{065f1d6f-77ea-4c6a-92d1-4a7890f10aea}{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{42b32942-3421-4b18-b565-a4f71761af39}"";{864d0aca-a65f-492b-b7f9-7e3146c1c20e}{BaseNode.EqualityExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{6097540e-740d-4897-a696-02dc971d6e36}"";{261d5e82-b3c2-45c4-b766-6bdd661f9dde}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{b6c7900c-1a68-458c-8925-b52a011cb550}"";{9d4724b4-2a5c-4f7a-ba21-69d713165397}{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{a2f568b3-641b-45d6-a5bd-1b9729c5d192}"";{08c55325-14dc-46c3-87ca-f0dc38316673}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"i"False"";{fd74537f-9921-4901-b325-87dec2d57eca}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"i"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{dca62ff6-fbc2-4fa7-92d4-716fa9167cde}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"-1"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{a2fa9742-7f7e-4be1-979f-e4ba353315f5}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0"0x00000000;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{c1670bbd-419b-4a45-b1a4-bf307460d1db}False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
"";{90a1b17b-b610-4f07-ae6f-0eec55f500f5}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{24b15b47-52a8-417d-8d55-e35a15ff67d8}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{424f66cb-8b1e-4cbf-a5e7-31ce485d24b0}"";{d20ab736-1041-411d-b6ec-663a642384ad}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{f0a3adc6-1ac7-4f49-bc88-0b20fe197d7b}"";{a695704d-9c14-471c-9af3-c4debe56d24d}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{2a80145e-2c22-44a9-b7b2-43cb181790f6}"";{52cd42a8-92ee-4cb7-9210-42baf5def87f}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{74046da6-b058-4cc8-9713-e08ebfaec181}"";{21271175-7cc2-43c8-a5a9-7ec1efb14917}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{97142079-30e7-4aa3-9ad1-64a8df98d01c}0x00000000"";{85e35060-1918-4c81-b184-6a6af688c71e}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{aa4a3bae-a21f-418b-9ee2-84f329cdfe3f}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{e5a4fb61-054b-4a3e-9e76-c570d3776658}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{c63299dc-4f83-4639-9945-3de90e5a8b84}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{1447b379-c585-43e4-ac68-e59f1d19d667}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{3a71aa81-abec-4336-947b-833322ff7be6}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"+"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"1"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"i"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Increment"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{7046416e-2c06-4949-8a67-c1ca445fac37}0x00000000"";{cdee7018-54c2-46c8-976e-39dd68532bf4}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{b9647612-97da-4a43-8d8d-98530bd3d943}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{f1329705-d8a6-4fc0-a0d2-6f04e68ed174}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{90ffc225-80d0-4439-b103-68e873359426}0x00000000"";{0022ad17-01f0-4024-adb5-4897552210cb}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{06c67ffb-11e4-4eec-ac7e-0f2976e91275}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{b9056540-3495-480f-a88c-150ec81502cf}"";{06d6f0b7-9a23-4602-aa1e-1d72950eb8d6}"";{f023b70e-745f-413b-b85f-7ac41de69bd1}"";{095fd821-d1ab-4cca-bd7c-47e426ad97e5}"";{a57e14f3-6b4e-4cfc-afea-6e6298548737}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"i"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{c35844e9-cc42-4a58-a3c6-2611e25f0977}"";{3f1afebd-f819-4c60-9697-03f34501deed}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{061a4784-af99-4304-a0b9-2c434c1760a7}"";{8424ac33-5287-4a6f-847d-c9c71827cf55}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"i""";{abcfee3c-5da5-43a1-b594-f8f3c43d83a7}0x00000000"";{698bd90d-454b-43ab-871d-afc42bad875e}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{7443f628-27a0-4923-8659-39a0fa544b2c}"";{c079183a-c228-4f65-9faf-283c26fc50b1}"";{bab4d545-2d7f-48f8-8881-1616392affe0}"";{5c0d9e78-9e9b-43d9-b53a-3747e78e46b0}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{54d9e541-fed6-46d9-97d2-29f2a3bee3cc}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{af0a6518-da48-41e8-a604-ff701786e889}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"My Attribute""";{05c1cce4-10b6-4470-a199-714eadf13daf}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"My Attribute"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"i""";{d004f565-14d7-4478-baf0-1ae2d0048aa8}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"My Attribute""";{81fa4944-1f41-463a-9688-0089b7fed11e}"";{fc62d2e8-5453-4bee-b600-f3ac37a849eb}"";{257ef6dd-d723-46b0-969c-2ac14b0e4345}
-->

<pre>
<b>loop</b>
&nbsp;&nbsp;&nbsp;<b>local</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>init</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;My&nbsp;Attribute&nbsp;<b>&#8592;&nbsp;</b>-1
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i&nbsp;<b>&#8592;&nbsp;</b>0
&nbsp;&nbsp;&nbsp;<b>while&nbsp;</b>i&nbsp;<&nbsp;10
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;My&nbsp;Attribute&nbsp;<b>&#8592;&nbsp;</b>i
&nbsp;&nbsp;&nbsp;<b>iterate</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i<b>&#183;</b>Increment
&nbsp;&nbsp;&nbsp;<b>invariant</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i&nbsp;=<b>&nbsp;(</b>My&nbsp;Attribute&nbsp;+&nbsp;1<b>)</b>
<b>end</b>
</pre>

```csharp
    int i = default;

    Set_MyAttribute(-1);
    i = 0;

    Debug.Assert(i == (MyAttribute + 1));

    while (i < 10)
    {
        Set_MyAttribute(i);

        i++;

        Debug.Assert(i == (MyAttribute + 1));
    }
```

## Over Loop

In an Over Loop instruction, there is no initialization nor iteration step like in the For Loop one. The invariant is therefore evaluated at the end of each loop.

<!---
Mode=Default
{BaseNode.ProcedureFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.ICommandOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.CommandOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{2d3176ca-bd36-43af-807b-7055042d2597}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Foo"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"All"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ICommandOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.CommandOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{fead0fd7-1845-40a1-8db3-4b1700847b5b}"";{ab437c84-faa4-4cbe-a651-4a78150091b1}"";{6f6b5ed5-cc26-424b-b71e-d407664b76af}{BaseNode.Block`2[[BaseNode.ICommandOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.CommandOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.ICommandOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{515bfd90-fb77-438a-b24a-6f03a37a6bf2}{BaseNode.CommandOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.EffectiveBody, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{a27b3afc-5652-4fb8-8f9a-d2c679912fee}"";{1bcdae94-858f-4dbf-bed7-da942614b1aa}{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{84534b28-2b7a-4694-8a4d-1c832a51d329}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{1a8f7b59-51c6-4c22-8550-76b6ecc5b192}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{5c8a4cd0-f420-4eed-aff6-143e6c21d7a9}0x00000000"";{796363b6-e519-4dfd-b03c-8ddeb4c9142f}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{bc33f8b5-b7b4-4d2b-9186-d17cc3027900}0x00000000"";{0f2d94c3-a452-4f46-8690-7b1595262deb}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{5180e746-eeb7-4412-8abc-566e441e793d}0x00000000"";{0a08ab99-6c2c-472d-908f-c0351613207a}0x00000000"";{9ae05ede-9079-46f2-bf25-a115a98715dc}0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{4ec60b94-3f7d-4858-81ac-6841faf61f86}{BaseNode.OverLoopInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{87bc4d43-35c4-420d-85c1-7972b414bf5c}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.Scope, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{0cf8ac2d-7c56-4fc2-bb6a-0fbf55e1355b}"";{e9178c69-f689-454a-9a5d-d090f3b80bd5}{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.GenericType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{5d6b1ec3-d826-44e4-a283-a1073ee900f1}"";{e2a32a8e-1c7c-4255-a844-db476c26d671}"";{9a52faae-8f10-4f31-bc38-3dcdfa7d14e0}False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
False"";{26b36774-e5fb-4753-a0d2-ee197234120a}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Over List"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{ca251772-6957-426e-af8b-dd97af163c2d}{BaseNode.Block`2[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{8a32d1de-1e55-45de-9d36-9a865945f413}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{e0d31713-3e11-443a-8ebc-b5e3184270e9}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{e46d52a7-30c6-4d38-af9c-a864e6d8f495}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{d6787e5e-c99e-47be-9e92-40a47b75f89f}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"List""";{65d39cec-8554-46b4-8a6c-25e938198af0}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{73fd1927-ee53-4310-ae8f-f0129bb9b018}0x00000000"";{06f14e75-bec9-4dfd-842c-e313cf7ad2cf}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{bac06e2e-1f20-4730-9c19-40944c3bd6ba}0x00000000"";{15763b9d-5a18-4f8f-a622-83d81b776ecb}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{4e673005-3a56-417f-9529-719e9f8d9679}"";{c64ffcc4-32e7-49ee-85a5-4fa3b47f1c20}{BaseNode.Block`2[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.TypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{7e5c3cb7-b38c-4fe0-a173-dfac63ef5fd4}{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{a145053d-678e-4723-ad25-897ff8c62809}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Over List"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.ITypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Item""";{1605d145-872c-432a-83fd-2512ea0007c3}"";{13fc98c4-6dcd-444b-bc3f-ae90c0bfea03}{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{4317b3e5-a5a6-4308-9240-01a6c1d4cd4c}"";{5b96a8a3-bac0-4c65-a5bd-c4b3a9330f4d}"";{0b5deb6e-b551-4a60-93c3-61a0e3286e58}{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{d619a86f-3080-4929-8541-6ecdd373eb03}"";{1d9be4ad-c453-44a9-a389-c28f030dcc25}{BaseNode.PositionalTypeArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{9eab86d7-cf8a-4182-b923-98d58ae3d0f5}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{0ae6fc87-da63-4e34-9387-c56b0764326b}False{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{f3933be3-2efc-4922-96c5-25b788937d20}"";{ddc28f5f-693c-40d9-a1cb-7980b66a93cb}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{119bc0de-26fd-4b2c-a488-bbfce46cdc48}"";{8500890a-0b5f-4105-a228-c47b1223c21c}"";{e4df6e5c-4365-4a36-ab09-00fc0781fe31}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"≥"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{0ff8eb1b-5082-4855-8548-ae17fd261943}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3ac76718-bfbf-4ea7-8e4e-26c9fde91a7a}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{209b9cd2-a08d-4c92-aed5-7a6ce6567b41}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{ee64c2e1-f258-4cac-967f-d208d6cb53cb}"";{f6b49d9c-8556-47ae-b082-8ce7a3cced9b}"";{fb18f8af-8d42-41bb-83c1-99530e1e6de2}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{0b51d23d-e7e5-421b-9d9d-43ffa804a9be}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{e464c22a-4961-4605-b349-6c29a6982acb}"";{2d54060f-80e0-475b-96c5-326f61c06d5d}0x00000000"";{b6376212-a6e9-4d92-8a92-7d4a9d9ec3b8}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{6d527ff9-2abd-4d26-969d-d0c90db0e2b8}0x00000000"";{661df5ec-416b-4145-9625-6efacf5d0d10}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{0daaf4ab-3a68-4f60-b29f-91e985dd7670}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Item""";{9a4fa16c-51dc-4751-9c44-e5eb8a331bff}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Item""";{8ec293dc-243f-4a2a-818c-d501d465f845}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{4df2badc-1779-4f82-864f-ea2796a5fe3c}"";{1b7e1ef5-2f6f-4362-b3d1-e086ef19e411}"";{9010d20e-c108-4554-b0c6-81ddc63b1cb9}"";{168432d7-9351-4871-b20c-a4650dc9426a}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"My Attribute""";{ea3cfa06-01f0-4135-b3b8-649557cf4d75}
-->

<pre>
Foo&nbsp;<b>procedure</b>
&nbsp;&nbsp;&nbsp;<b>local</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Over&nbsp;List<b>:&nbsp;</b>List<b>[</b><i>Number</i><b>]</b>
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>over&nbsp;</b>Over&nbsp;List&nbsp;<b>for&nbsp;each&nbsp;</b>Item
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;My&nbsp;Attribute&nbsp;<b>&#8592;&nbsp;</b>Item
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>invariant</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Item&nbsp;&#8805;&nbsp;0
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>end</b>
<b>end</b>
</pre>

```csharp
	public virtual void Foo()
	{
	    IList<int> OverList = default;
	
	    foreach (int Item in OverList)
	    {
	        Set_MyAttribute(Item);
	
	        Debug.Assert(Item >= 0);
	    }
	
	    CheckInvariant();
	}
```

## Inheritance

Features can be redefined according to one of the following schemes:

+ The feature is redefined and has a precursor body.<br>In this case, assertions aren't ported over to the new feature.

```csharp
    public override double SquareRoot(double X)
    {
        return base.SquareRoot(X);
    }
```

+ The feature is redefined, doesn't have a specific Require or Ensure clause, but has its own body that may or may not call the precursor.<br>Assertions are then just copied in the new body.

<!---
Mode=Default
{BaseNode.FunctionFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{4029e719-c281-4a4c-af22-9673df03d053}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Square Root"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"All"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{4f0b0c40-03d6-4745-97cb-875b2106490e}"";{e14bbb37-71a6-418c-a5f6-3ba4d140b2d4}"";{13f730d5-aa1f-4da0-b92a-3bd31d450936}{BaseNode.Block`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{8723adde-0733-49de-8786-aac5ab1b1eff}{BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.EffectiveBody, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3fbe1981-21ea-44c2-ab44-ebaa863fd63a}"";{6f94e533-747b-4a2d-84a6-b8c7665c8839}"";{c31b738e-b28c-493b-9c29-19d1950abb6b}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
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
False"";{c8afa317-36b8-4319-b5c4-f1de8382042f}0x00000000"";{822e944c-b3b7-49c1-bdf0-a190abbe1ea2}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{4de216d3-e36d-4a9a-a0fe-c756aab9bcae}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{f8a509dc-1575-4e48-9461-fa4a70722fd7}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3cb15242-f65b-458f-8a33-3227e682e000}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{1dac5088-1927-42c6-8687-04a09f637a8e}0x00000000"";{4eace4e4-58e9-470b-9720-e92a5c517dab}0x00000000"";{10e0db25-5001-49a0-b46f-1c53a22ed25a}0x00000000"";{e67c1d0a-47e1-4b2c-b38b-eb646816ac5d}0x00000000"";{d5df90a5-c703-4700-828d-2e06c304aec3}0x00000000{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{e904d9f1-e7f0-4017-a6d2-fce68327ec41}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{58a1c70b-bc6a-4a08-ac68-1a78ecdaab63}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{34bdb6da-79e5-4d8b-9be2-31d77c756b4d}"";{aac40086-99e5-46ef-8b8b-75807c624ee1}"";{2b72eed6-4388-4e61-a5f5-97b0ce90f653}{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{0f974145-65df-4046-98af-3de33b21df0f}"";{ba39d3c4-a97e-4d0f-9e0f-263f64385985}False"";{b4891f97-f8a8-47e0-8a40-97dbf7fd5454}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.PrecursorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{68c31c27-04f2-4ed3-9282-adb07a612f8e}"";{4c89972d-6274-435e-b8f3-48f3e8837e9a}False"";{160b68cf-fa71-452e-aed5-3104b62f9c13}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{648b64b5-aedf-4cb4-b367-b95bc7d47292}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{a6a34bef-b818-4998-9e9f-9f10300a6295}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{e5966dba-e57a-4f82-9429-c459d3dd8af4}{Easly.OptionalReference`1[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{f0dcd2df-4294-41d7-94ca-97a4340031f1}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{d1c9f451-3d94-4e91-b3d6-c8f2bf4fe32e}"";{dcdd5142-bade-4575-aac4-8c04ca3b8785}"";{2e7626ed-bf4d-41e0-a6a8-21a936247d13}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{83cfed44-a219-49c1-8ff7-1fb9ac2a2c6d}"";{0bc1a0b9-04e4-4e6e-ae08-051b25a17c9e}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{2cb1ef29-fe39-4364-b012-91e4d5cfbbf0}{BaseNode.Block`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{74f18c31-ce49-4425-b526-058ee8ac05e2}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{5e3f550f-4fa2-4230-a17c-065a0459f53c}"";{81e3b960-a571-48a7-a3fb-8b57b657d7ef}"";{0fa1d93f-c528-4968-a5c5-8fcab806c336}{BaseNode.PositionalArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{2bb07f56-b756-4d6b-8be5-1fcfcac7445f}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{34abb96f-a876-4877-9cb6-cc058c9cd606}"";{05cc4e29-108d-4db0-9833-3ea449c3aba7}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result""";{f982b9d6-fdbd-4c32-a5fb-fbdadb6b04a4}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{eca7120d-29a7-40c2-9b09-09d4f5a4ac50}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{b0d1ef87-c325-4405-89ff-2ea22200dd67}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{e10df255-3867-4208-bd23-1dc819cf2e1a}0x00000000"";{be7ee598-0048-4a3b-98b0-e573e7d91e4d}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X""";{19986018-48f8-46ed-a3a6-fccd2eef7424}
-->

<pre>
Square&nbsp;Root&nbsp;<b>function</b>
&nbsp;&nbsp;&nbsp;<b>parameter</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>result</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;<b>&#8592;&nbsp;precursor&nbsp;(</b>X<b>)</b>
<b>end</b>
</pre>

```csharp
    public override double SquareRoot(double X)
    {
        Debug.Assert(X >= 0); // Valid Input

        double Result = default;

        Result = base.SquareRoot(X);

        Debug.Assert(Result >= 0); // Valid Output
        Debug.Assert((Result * Result) == X);

        return Result;
    }
```

+ The feature is redefined and has its own Require or Ensure clause, and body.<br>We use assertions from the redefined body.

<!---
Mode=Default
{BaseNode.FunctionFeature, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.BlockList`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{4029e719-c281-4a4c-af22-9673df03d053}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Square Root"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"All"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{4f0b0c40-03d6-4745-97cb-875b2106490e}"";{e14bbb37-71a6-418c-a5f6-3ba4d140b2d4}"";{13f730d5-aa1f-4da0-b92a-3bd31d450936}{BaseNode.Block`2[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{8723adde-0733-49de-8786-aac5ab1b1eff}{BaseNode.QueryOverload, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000;{BaseNode.EffectiveBody, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3fbe1981-21ea-44c2-ab44-ebaa863fd63a}"";{6f94e533-747b-4a2d-84a6-b8c7665c8839}"";{c31b738e-b28c-493b-9c29-19d1950abb6b}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
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
False"";{c8afa317-36b8-4319-b5c4-f1de8382042f}0x00000000"";{822e944c-b3b7-49c1-bdf0-a190abbe1ea2}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{4de216d3-e36d-4a9a-a0fe-c756aab9bcae}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ExceptionHandler, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{f8a509dc-1575-4e48-9461-fa4a70722fd7}{BaseNode.Block`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3cb15242-f65b-458f-8a33-3227e682e000}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{1dac5088-1927-42c6-8687-04a09f637a8e}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{4eace4e4-58e9-470b-9720-e92a5c517dab}0x00000000"";{10e0db25-5001-49a0-b46f-1c53a22ed25a}0x00000000"";{e67c1d0a-47e1-4b2c-b38b-eb646816ac5d}0x00000000"";{d5df90a5-c703-4700-828d-2e06c304aec3}{BaseNode.Block`2[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{e904d9f1-e7f0-4017-a6d2-fce68327ec41}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAssertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{58a1c70b-bc6a-4a08-ac68-1a78ecdaab63}{BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{34bdb6da-79e5-4d8b-9be2-31d77c756b4d}"";{aac40086-99e5-46ef-8b8b-75807c624ee1}"";{2b72eed6-4388-4e61-a5f5-97b0ce90f653}{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{56758392-4ac5-47c9-b484-e3136583b7a7}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{34c79864-bf70-4d52-ba6b-c286abe4eb89}{BaseNode.Assertion, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{Easly.OptionalReference`1[[BaseNode.IExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{0f974145-65df-4046-98af-3de33b21df0f}"";{ba39d3c4-a97e-4d0f-9e0f-263f64385985}False"";{b4891f97-f8a8-47e0-8a40-97dbf7fd5454}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.PrecursorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{68c31c27-04f2-4ed3-9282-adb07a612f8e}"";{4c89972d-6274-435e-b8f3-48f3e8837e9a}{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{157e2325-5c77-4a91-9a80-5e55bbcb0c07}"";{cfda8334-5b86-44f3-a2f6-7e6b1d680c2c}{BaseNode.BinaryOperatorExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{e078c530-399d-4818-aa53-0de81084ff04}"";{6221023f-1a00-4ada-b0ac-eb37ceabc70a}False"";{160b68cf-fa71-452e-aed5-3104b62f9c13}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result"{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{648b64b5-aedf-4cb4-b367-b95bc7d47292}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{a6a34bef-b818-4998-9e9f-9f10300a6295}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{e5966dba-e57a-4f82-9429-c459d3dd8af4}{Easly.OptionalReference`1[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{21a6bf0d-7493-4a94-9cbb-1a16fae92055}False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{f7179734-64e9-4b36-a8ec-d0da909a6c77}False"";{f0dcd2df-4294-41d7-94ca-97a4340031f1}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Number""";{d1c9f451-3d94-4e91-b3d6-c8f2bf4fe32e}"";{dcdd5142-bade-4575-aac4-8c04ca3b8785}"";{2e7626ed-bf4d-41e0-a6a8-21a936247d13}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004False{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{83cfed44-a219-49c1-8ff7-1fb9ac2a2c6d}"";{f6f8261f-e4c0-455f-bf5f-054773851aba}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"≥"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"1""";{dc4db179-4b84-4d97-9e2d-b507a66da0bd}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"≥"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"1""";{0bc1a0b9-04e4-4e6e-ae08-051b25a17c9e}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{2cb1ef29-fe39-4364-b012-91e4d5cfbbf0}{BaseNode.Block`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{30a8472c-152e-4ab7-9126-b9c86bd72260}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{6a298bc3-2552-4fcb-94a7-285595d49aa7}"";{817d673f-2f50-40e0-80bf-8708cf91424e}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{c6d1560d-0f92-4ed3-bb63-1fed866dc5bd}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{1c7d64da-20c2-466c-8d17-959e80f352da}"";{dd720710-7683-48c1-95e7-388a513ea0b1}"";{74f18c31-ce49-4425-b526-058ee8ac05e2}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{bbee9851-a0dc-42dc-8c84-8899237612bf}0x00000000"";{47a84b3f-bfed-4673-ac14-c3c4d6656d21}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{6bc8102c-4b9d-4003-8eb6-f1f565fd9159}0x00000000"";{35977f4b-0b4e-4788-af04-7ce8ffdbee57}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{5e3f550f-4fa2-4230-a17c-065a0459f53c}"";{81e3b960-a571-48a7-a3fb-8b57b657d7ef}"";{0fa1d93f-c528-4968-a5c5-8fcab806c336}{BaseNode.PositionalArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X""";{2bb07f56-b756-4d6b-8be5-1fcfcac7445f}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{34abb96f-a876-4877-9cb6-cc058c9cd606}"";{05cc4e29-108d-4db0-9833-3ea449c3aba7}"";{0bed5188-37f4-4471-857b-3102d60819f7}"";{850f39dd-586b-4f90-b687-4fc5e433013b}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result""";{f982b9d6-fdbd-4c32-a5fb-fbdadb6b04a4}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{eca7120d-29a7-40c2-9b09-09d4f5a4ac50}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{b0d1ef87-c325-4405-89ff-2ea22200dd67}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{e10df255-3867-4208-bd23-1dc819cf2e1a}0x00000000"";{be7ee598-0048-4a3b-98b0-e573e7d91e4d}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X""";{19986018-48f8-46ed-a3a6-fccd2eef7424}
-->

<pre>
Square&nbsp;Root&nbsp;<b>function</b>
&nbsp;&nbsp;&nbsp;<b>parameter</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>result</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result<b>:&nbsp;</b><i>Number</i>
&nbsp;&nbsp;&nbsp;<b>require</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X&nbsp;&#8805;&nbsp;1
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;<b>&#8592;&nbsp;precursor&nbsp;(</b>X<b>)</b>
&nbsp;&nbsp;&nbsp;<b>ensure</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;&#8805;&nbsp;1
<b>end</b>
</pre>

```csharp
    // Require:
    //   X ≥ 1
    // Ensure:
    //   Result ≥ 1
    public override double SquareRoot(double X)
    {
        Debug.Assert(X >= 1);

        double Result = default;

        Result = base.SquareRoot(X);

        Debug.Assert(Result >= 1);

        return Result;
    }
```
