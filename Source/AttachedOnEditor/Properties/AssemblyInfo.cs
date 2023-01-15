using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("KSP-Recall :: Attached On Editor for KSP 1.9.x")]
[assembly: AssemblyDescription("<TBD>")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(KSP_Recall.LegalMamboJambo.Company)]
[assembly: AssemblyProduct(KSP_Recall.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright(KSP_Recall.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark(KSP_Recall.LegalMamboJambo.Trademark)]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion(KSP_Recall.Version.Number)]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]
[assembly: KSPAssemblyDependency("KSPe.Light.Recall", 2, 3)]
//[assembly: KSPAssemblyDependency("KSP-Recall", KSP_Recall.Version.major, KSP_Recall.Version.minor, KSP_Recall.Version.patch)]
