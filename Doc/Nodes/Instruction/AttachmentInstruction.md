# Attachment Instruction

The purpose of this instruction is test one or more objects, to see if they conform to provided types. When they do, instructions are then executed with this knowledge.

The attachment instruction contains:

+ An expression that provides the objects to test. Usually, it's one object only, but a Query Expression for example can return more than one. 
+ A set of names that will be used to create new variables if an attachment is successful. There must be as many names as there are objects to test.
+ A list of [attachments](https://github.com/dlebansais/Easly-Language/blob/master/Doc/Nodes/Attachment.md), tested in the order they appear, with types to test. In each attachment, there must be as many types as there are objects to test.
+ An else clause if no attachment succeeded.

One after another, attachments check if the n<sup>th</sup> object conforms to the n<sup>th</sup> type in the attachment. Note that conversion isn't applied in this case.
If, for an attachment, all objects conforms simultaneously, this process stops. New variables, with names declared at the begining and with types declared in the matching attachment, are created. Instructions in the attachment are then executed, with these variables available (in addition to local variables that the attachment can also declare).

<!---
Mode=Default
{BaseNode.AttachmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IAttachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Attachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{Easly.OptionalReference`1[[BaseNode.IScope, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IAttachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Attachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{0751aeaf-57e5-4d25-a220-7aaa2d7d21f6}True {BaseNode.Scope, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{16a954ae-3bef-4abd-9c33-62089bbaaf3f}{BaseNode.Block`2[[BaseNode.IAttachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Attachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{645fe46d-7b3c-4934-a7bd-366912dfdb21}{BaseNode.Block`2[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{6f15ffad-f113-4255-8d5d-c9b8adebc953}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IAttachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *3
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{aaf00e27-1572-4c5b-bb25-5969e041a5f5}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{a052d47b-40e6-401d-9638-5bfd719ffa4e}{BaseNode.Block`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{83432245-e113-4a59-864d-0a6b594e5d81}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{5ed43e59-160a-414a-81bd-8dbfdf747b38}{BaseNode.Attachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Attachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Attachment, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{a8f85410-4c79-4fe2-a021-1c07548be8b4}0x00000000"";{9854eaa7-ef15-445d-9d25-78f11b4c7cba}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{ef0a8131-bb05-4716-a511-3f5b0dbb5f97}{BaseNode.Name, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Foo"{BaseNode.BlockList`2[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Scope, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Scope, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Scope, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{74956743-c5d7-4de1-ab4d-8d1c29cf4b37}"";{f069e1b2-e237-4ee7-a590-782085dd9e1c}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X""";{a904f2d3-1e75-48c6-88ae-a597740292be}"";{b47042c9-01ae-4fa1-ba4d-5942a5454843}"";{cce88633-9d38-4fff-a828-676a1735a368}{BaseNode.PositionalArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{54a83cdb-c986-42e8-b504-2da376ef3486}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{86468dde-4dc7-4c36-a270-6ee0d3032384}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{227cfc4c-57e2-4a3b-8e93-5b1acba74c60}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{c5e89f2c-27b0-43b8-b715-72f3064000ee}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.BlockList`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{91ada0f6-a39c-411d-885b-20804b0294ab}{BaseNode.CommandInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{197bf452-9ba5-43cf-9fbc-f9788659f921}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{1bd2e4b5-edc4-4afb-9d36-591a6a6b14ec}"";{129d1825-7f5c-4f33-8cf7-d8d347d27e39}"";{a905d1df-46eb-457e-b35a-7fcb684c57be}{BaseNode.Block`2[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{c22329de-17a3-449e-9207-7361d810a78c}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{7a7a0038-32ab-43d6-af4a-99479f14ef02}{BaseNode.Block`2[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{8e741a2d-705f-4fe3-8f3d-2eb1014392ec}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{9d4e174c-2d21-4ab5-a6c4-3ae531f02d60}{BaseNode.Block`2[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.ObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{22ac874c-52c7-4dad-a87d-15995018cf19}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IEntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.EntityDeclaration, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{65cdc776-e1b1-4c7d-b40d-e7cd378f365e}"";{f997289d-8f90-4ade-bcc4-a1e58eef6e46}"";{8812b976-c8e0-4cd5-94b2-a02d26dc79cd}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{e9477915-5830-4123-a7be-66ef920cf875}0x00000000"";{c257b8fe-2bd2-43a4-9762-e0c139f88079}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{577d158e-fefa-45a0-8f96-3dad0b7905cc}0x00000000"";{91c5bc56-bc5d-49d5-b093-9f4e712c13c9}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IObjectType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{cec202fe-a94d-4d17-90a2-2670f70f04a8}0x00000000"";{914be5a2-1457-400d-b2b7-7ffe4dfeb9a2}{BaseNode.Block`2[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Instruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{0e81689a-ec06-4e42-9475-5ef8ee509302}"";{ec3a1c14-8daf-43e9-8b27-a05783f692b1}"";{87461f07-02fc-4e1c-b8fe-631b5fb7e714}{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *3
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{5e0804a8-dec9-4764-9139-844434533943}{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *3
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{561142e0-445a-48c7-b108-e6e0022bee41}{BaseNode.SimpleType, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *3
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{8e82ac2b-5c3d-459e-ae7a-4dc23c1442de}0x00000000"";{b3f89126-df09-4872-9f05-f13a51c8a576}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{55335752-da47-421c-b103-8160ceb4e110}"";{7d7edebe-c888-49d8-9e15-1f6b63eceac8}"";{91866f9c-35f7-425d-907f-f42186f0db2c}{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{ab7bb747-23c1-46e4-99a9-3b66762b46dc}"";{e0a3b0d3-03d5-49e2-b2f4-e6b1021356e9}"";{00036ffb-96c1-4ba8-84c8-26f2eb8bee24}{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;0x00000000"";{7aee58b3-19e9-4b4a-97ef-8fd1e068a18f}"";{4de6f960-9304-4e57-b95c-42f7a05c34e0}"";{f33f1d5b-1943-46d9-a90f-eeefb5bb6fa4}{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.AssignmentInstruction, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Fail"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Function Entity""";{0bfa3ef6-4b9c-427b-8dba-42226ad9e61b}{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{e64ac5e0-7579-4b0b-9987-f7e8935a33d4}"";{92c741ab-ef54-4c4d-88c0-a3faa1f005b2}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Procedure Entity""";{48a5673c-036f-47c2-b365-30c635617e0a}{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{0435a738-24d4-40a1-9684-fc8fee60f4d7}"";{577fa600-64d7-4d91-8467-3117680950b4}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Property Entity""";{b0e49097-bee3-4cf8-a94b-ef16ef47a752}{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.ManifestNumberExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.BlockList`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QueryExpression, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{f7318dd0-3c17-44e5-ba6a-44717a8f8dcd}"";{d18d6958-fb01-4faf-ba3d-20de739ccf4f}"";{7abdb3b9-9e7a-4135-9987-db32d63581ff}"";{8c617d01-a592-4453-9d13-fe2d40e18b10}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{ebf5343d-757a-4447-a021-0fa751abd3d4}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{6d0292d9-1c3f-4845-91d0-556c31734d8a}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{b5412ada-b9ad-484d-879d-1b560494317c}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{013dfa11-da89-4d8c-94ea-c4e32708f885}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{9680ebdd-3aca-4ce3-b52e-e27adcff358e}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{afe55713-20ab-4552-931b-464f6a84d639}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"0"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{8bf1bc11-8eab-4bac-bf51-fbfaebb35137}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{82622f84-94cb-41e8-b59b-eeaf6e36354a}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{cc35f428-086c-43b1-ab41-6198fd014588}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"1"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{38ec3c20-6da6-47f8-bcb5-f58fc1ad6599}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"1"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{bb503775-a566-4b71-a6e8-0983b5ff85f2}{BaseNode.BlockList`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{2bba9b2c-0fcc-44d7-a7e9-54d304007dfe}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{3da1e6bf-6b1a-41ae-832f-e2611e24f275}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *3
"";{caca02a2-f636-4c53-b34d-7b9c6864342e}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{eae152b2-0c4c-4a4b-ba71-e9d300990a0c}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *3
"";{ee6a172a-487b-491b-9c25-c02c48c28238}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{3bbca115-3171-4233-a4a7-d186a5eba097}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
"";{15d6bc5f-d43e-4492-8d19-16e012fdd593}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{ef5371d4-1960-4cee-9f39-76151716ca45}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *3
"";{aa950863-2776-46ef-ba2b-f644e285a47e}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{d1fce145-3e5f-4ca6-9945-eb7a9fe396bd}"";{1eba409c-9596-4cc8-ab7a-81c32a80215c}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{6271df15-3f83-4712-891f-009dcf3776a0}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
"";{d8e2b550-5757-4077-89d4-10b3b5b6a1b4}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{ac7cb323-d055-4907-b2d1-dfa12e610c1d}"";{4c5db2cf-4200-4647-a4d2-d9f2b24d5108}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{596da39b-58ef-4516-873c-f3539d921058}"";{839756a4-6e29-4010-89d0-40144d4b0114}{BaseNode.Block`2[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IBlock`2[[BaseNode.IArgument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[BaseNode.Argument, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *0
"";{cf7ab2be-426f-4f85-9aed-2ee10eb08671}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *2
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{0c4e362b-965c-4ad3-a036-78de32b70971}0x00000000"";{1452e4e2-bc57-49dd-8fdc-402b39a9ae42}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{3a910317-e67f-4563-93ee-e32d50fad00b}0x00000000"";{6b6613a2-b998-4f1f-8b25-ce5e473f1043}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{09908e7e-bea1-48f6-8d1a-d3ce8d4418fc}0x00000000"";{313e5535-a264-44ea-87f8-d1f38b8bcc30}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{75e9d0d2-c631-4422-84ea-9ef035ddbdac}0x00000000"";{9ee181ac-fe09-4440-89c4-2d8af6ae1e0e}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{69698e74-8c33-410f-973d-17a7dccfbc42}0x00000000"";{9956eaa6-a6a8-4fc8-a9ed-d664ba2c6acd}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IQualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
;0x00000000;{BaseNode.Pattern, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
"";{b0adc3d3-825d-4e7e-9d54-2a815e123ace}0x00000000"";{d2f916b3-52c2-4f0a-a93d-bbb12903e613}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{2ad3658b-8964-464e-ad1f-3d31964d3d29}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Parameter List"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Count""";{65e10e35-538f-4af8-805a-86c2749fef09}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result List"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Count""";{e2e5621f-faf7-4efb-b798-3bbd63d980c7}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Base Type""";{b446f927-2de2-4bc8-ba9f-9bc9e38b0b99}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Parameter List"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Count""";{75f99b6c-704a-431e-a91c-6811518e342e}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{adb5d60d-a444-4eb9-ac88-8017b05514f0}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Base Type""";{346464c4-822b-4cab-bf86-acc31e1d5aae}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{9da8e50d-4a96-4229-9c03-909577d2eb7f}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"""";{7aad28ba-9ad4-4230-b9b7-9f90dd89bc6a}{BaseNode.QualifiedName, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"*"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;""{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"X"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Property Type"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{0eef24cd-bd0f-4eb7-8547-20982801127e}"";{892e2b0a-eb29-4c2d-97a1-1ada8bc80c4a}"";{417e5f00-787f-4ec3-bb96-f559cb88a0d1}"";{925de991-927b-47d8-ba9d-d4896edac3f9}"";{997dee59-95dc-4c44-a2d8-a2d8afac7680}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{0f8e23f9-832c-4e4b-8ceb-976f510cfd34}"";{1a4f01bc-de32-4541-b4a7-e2ced918ca08}"";{a52ef60a-5c68-47e6-ac37-977bb262fbfe}"";{ba7e25c4-e79c-4d00-bf48-99a50fabd0c3}"";{16e0df8e-c194-40ea-a44c-f3170748aa4f}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{28173395-0a87-4ede-9ec7-4d5ae1b350c8}"";{de638399-9c75-4ae4-be63-33535e3abb0d}"";{c40c26bf-9944-4700-b6e7-90e7f742e56f}"";{8f2f5d42-53db-4635-960e-2860f0722308}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{ad52b504-5d4f-48a4-800b-b98922334dc0}"";{5dfa931b-cb73-4e2f-84d0-9c2b3751bdf6}"";{b5ba8414-cf36-438d-a1c6-09fb7f99efe8}"";{68b81c20-8d8d-4334-ad8e-df7839b1089e}"";{5e88dbba-89fd-42cc-8cab-d97ebc8ad19d}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{01a25f46-9d71-43aa-9999-cf06719410e7}"";{6e8facd3-e21c-4a90-b3d7-6894cd8f48ca}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{71a63c53-dd18-4ccb-ab47-75d4b3d84a4c}"";{e859a5b5-4d05-4c30-8ece-b6c6527d6bed}"";{4b081a17-a033-4bca-b687-d303241cefc1}"";{9fe50276-9b3f-4184-b477-e60e457df340}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{af554188-291d-4555-b00b-2d4ebd56172a}"";{e250c427-219f-44ba-8e9b-7e54927548f2}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{c98fb792-d596-4614-8704-48f7a0684564}"";{19830905-3626-436d-bb65-c367f5054144}{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;{System.Collections.Generic.List`1[[BaseNode.IIdentifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089} *1
"";{38479ecc-1514-43b0-b7c2-b90da0304847}"";{5e6a9f21-24ce-42cd-8e72-f721f2d49917}"";{14202323-816c-4eaf-bb53-4e2415417de2}"";{229d2297-e6f2-4c84-803f-c13a4a3f0aa9}"";{6786beba-10e1-4568-aee2-73d633f4529b}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{b50dad8e-162a-4843-b7d8-dd734224e8ba}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{74c242e5-5960-4d0f-ba3f-570dd6e4a4eb}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{5de7d238-0670-4c2a-a635-15bce14349ba}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{931579c9-1155-4ff8-97b8-61708ffe10d3}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{9ce3b58f-14cb-4cdc-b9db-5141523b50ef}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{32a2d78c-b244-4217-bfa5-3ddac3684f6e}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{d255ece8-06e2-4b8d-b0c4-03d03119e7ba}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004"";{cc462eb9-abf3-4207-bf21-37e43f1d4277}{BaseNode.Identifier, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
0x00000004{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Parameter Count"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result Count"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Parameter Count"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result Count"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Type"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Parameter Count"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Result Count"{BaseNode.Document, Easly-Language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
;"Type""";{cf75cfdc-5522-468c-ae84-d8758d59e6fe}"";{667fa367-3521-4aad-980b-a58dce423c24}"";{7b47e16b-e0ea-4d7c-8346-246c79840f85}"";{2f70ad53-f020-465e-8787-09ffe4b32017}"";{320e5516-6dbd-4cf3-ae46-c9718bcd0e92}"";{18c81e3f-17f7-4dc2-90fa-864f04c3dccf}"";{eef8ac79-f373-4887-a877-3b30f90a2a6d}"";{81e745e6-3e94-46fd-8f6d-cbd17c5ccb70}"";{3ecac113-0249-4975-9c3c-3909ba3f3d05}
-->

<pre>
<b>attach&nbsp;</b>Foo<b>(</b>0<b>)&nbsp;to&nbsp;</b>X
&nbsp;&nbsp;&nbsp;<b>as&nbsp;</b><i>Function&nbsp;Entity</i>
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Parameter&nbsp;Count&nbsp;<b>&#8592;&nbsp;</b>X<b>&#183;</b>Parameter&nbsp;List<b>&#183;</b>Count
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;Count&nbsp;<b>&#8592;&nbsp;</b>X<b>&#183;</b>Result&nbsp;List<b>&#183;</b>Count
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;<b>&#8592;&nbsp;</b>X<b>&#183;</b>Base&nbsp;Type
&nbsp;&nbsp;&nbsp;<b>else&nbsp;as&nbsp;</b><i>Procedure&nbsp;Entity</i>
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Parameter&nbsp;Count&nbsp;<b>&#8592;&nbsp;</b>X<b>&#183;</b>Parameter&nbsp;List<b>&#183;</b>Count
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;Count&nbsp;<b>&#8592;&nbsp;</b>0
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Type&nbsp;<b>&#8592;&nbsp;</b>X<b>&#183;</b>Base&nbsp;Type
&nbsp;&nbsp;&nbsp;<b>else&nbsp;as&nbsp;</b><i>Property&nbsp;Entity</i>
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Parameter&nbsp;Count&nbsp;<b>&#8592;&nbsp;</b>1
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Result&nbsp;Count&nbsp;<b>&#8592;&nbsp;</b>1
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Type&nbsp;<b>&#8592;&nbsp;</b>X<b>&#183;</b>Property&nbsp;Type
&nbsp;&nbsp;&nbsp;<b>else</b>
&nbsp;&nbsp;&nbsp;<b>do</b>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fail
<b>end</b>
</pre>

In the code above, the attachment instruction has an explicit else clause. When this clause is not present, the compiler checks that all possible types have been covered. If it's not the case, and a combination of types could be non-conforming to all attachments, an error is issued at compile time.

# Translation to C&#35;

```csharp
```
