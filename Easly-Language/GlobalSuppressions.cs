﻿using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Regions can be good")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "No copyright")]
[assembly: SuppressMessage("Design", "CA1030:Use events where appropriate", Justification = "Defining a language", Scope = "member", Target = "~M:Easly.IEvent.Raise")]
[assembly: SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Defining a language")]
[assembly: SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Defining a language")]
[assembly: SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "Defining a language")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Special case", Scope = "member", Target = "~P:Easly.EventBase.IsTrue")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Special case", Scope = "member", Target = "~P:Easly.EventBase.IsFalse")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Properties assigned using tools")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Special case", Scope = "member", Target = "~P:Easly.SpecializedTypeEntity`1.Singleton")]

[assembly: SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "TODO: no longer use a symbol for 'Implies'", Scope = "member", Target = "~M:Easly.EventBase.op_Division(Easly.EventBase,Easly.EventBase)~Easly.EventBase")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "TODO: fix it", Scope = "member", Target = "~F:Easly.OptionalReference`1.ItemInternal")]
[assembly: SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "TODO: fix it", Scope = "member", Target = "~F:Easly.OptionalReference`1.ItemInternal")]
[assembly: SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names", Justification = "TODO: fix it")]
