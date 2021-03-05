## Can't build VB.NET projet from CLI

builds from VS 2019, no issue

>Build started...
1>------ Build started: Project: ClassLibraryVb, Configuration: Debug Any CPU ------
1>  ClassLibraryVb -> \experiments-2020\dotnet build research\ClassLibraryVb\bin\Debug\ClassLibraryVb.dll
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========

fails from cli using `dotnet build`:

>Build FAILED.

>\experiments-2020\dotnet build research\ClassLibraryVb\Class1.vb(1,9): warning BC40056: Namespace or type specified in the Imports 'Microsoft.Practices.EnterpriseLibrary.Data' doesn't contain any public member or cannot be found. Make sure the namespace or the type is defined and contains at least one public member. Make sure the imported element name doesn't use any aliases. [\experiments-2020\dotnet build research\ClassLibraryVb\ClassLibraryVb.vbproj]
\experiments-2020\dotnet build research\ClassLibraryVb\Class1.vb(4,33): error BC30002: Type 'Database' is not defined. [\experiments-2020\dotnet build research\ClassLibraryVb\ClassLibraryVb.vbproj]
    1 Warning(s)
    1 Error(s)