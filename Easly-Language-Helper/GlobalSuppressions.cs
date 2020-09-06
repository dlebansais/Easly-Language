using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Regions can be good")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "No copyright")]
[assembly: SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Defining a language")]
[assembly: SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "Defining a language")]
[assembly: SuppressMessage("Globalization", "CA1307: Specify StringComparison for clarity", Justification = "Unavailable")]

[assembly: SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1405:Debug.Assert should provide message text", Justification = "<Pending>")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "<Pending>")]
[assembly: SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>", Scope = "type", Target = "~T:BaseNodeHelper.WalkCallbacks`1")]
