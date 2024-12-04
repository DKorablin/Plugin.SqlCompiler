using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: Guid("0c74164b-9a37-4d5c-a6e1-195421c59908")]
[assembly: CLSCompliant(true)]

#if NETCOREAPP
[assembly: AssemblyMetadata("ProjectUrl", "https://github.com/DKorablin/Plugin.SqlCompiler")]
#else

[assembly: AssemblyTitle("Plugin.sqlCompiler")]
[assembly: AssemblyDescription("Compile Microsoft SQL Assemblies for XML manipulation")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Danila Korablin")]
[assembly: AssemblyProduct("Plugin.sqlCompiler")]
[assembly: AssemblyCopyright("Copyright © Danila Korablin 2013-2024")]
#endif