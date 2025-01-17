// MIT License.

namespace System.Web.Compilation;

using System;
using System.CodeDom.Compiler;

/*
 * This class describes a CodeDom compiler, along with the parameters that it uses.
 * The reason we need this class is that if two files both use the same language,
 * but ask for different command line options (e.g. debug vs retail), we will not
 * be able to compile them together.  So effectively, we need to treat them as
 * different languages.
 */
public sealed class CompilerType
{

    public CompilerParameters CompilerParameters { get; }

    public string Language { get; }

    public static CompilerType VisualBasic { get; } = new("VB", null);

    public static CompilerType CSharp { get; } = new("C#", null);

    public bool IsCSharp() => string.Equals("C#", Language, StringComparison.OrdinalIgnoreCase);

    public bool IsVisualBasic() => string.Equals("VB", Language, StringComparison.OrdinalIgnoreCase);

    public static CompilerType Create(string language)
    {
        if (string.Equals(language, "C#", StringComparison.OrdinalIgnoreCase))
        {
            return CSharp;
        }
        else
        if (string.Equals(language, "VB", StringComparison.OrdinalIgnoreCase))
        {
            return VisualBasic;
        }
        else
        {
            return new CompilerType(language, null);
        }
    }

    public static CompilerType GetByExtension(string extension)
    {
        if (string.Equals(".cs", extension, StringComparison.OrdinalIgnoreCase))
        {
            return CSharp;
        }

        if (string.Equals(".vb", extension, StringComparison.OrdinalIgnoreCase))
        {
            return VisualBasic;
        }

        throw new NotSupportedException($"Unknown extension: {extension}");
    }

    internal CompilerType(string language, CompilerParameters compilParams)
    {
        Language = language;

        if (compilParams == null)
        {
            CompilerParameters = new CompilerParameters();
        }
        else
        {
            CompilerParameters = compilParams;
        }
    }

    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Language);

    public override bool Equals(object o)
    {
        if (o is not CompilerType other)
        {
            return false;
        }

        return string.Equals(Language, other.Language, StringComparison.OrdinalIgnoreCase) &&
            CompilerParameters.WarningLevel == other.CompilerParameters.WarningLevel &&
            CompilerParameters.IncludeDebugInformation == other.CompilerParameters.IncludeDebugInformation &&
            CompilerParameters.CompilerOptions == other.CompilerParameters.CompilerOptions;
    }

    public override string ToString() => $"{Language} [warn: {CompilerParameters.WarningLevel}; debug: {CompilerParameters.IncludeDebugInformation}; options: {CompilerParameters.CompilerOptions}]";

#if PORT_ASSEMBLYBUILDER
    internal static AssemblyBuilder GetDefaultAssemblyBuilder(CompilationSection compConfig,
    ICollection referencedAssemblies, VirtualPath configPath, string outputAssemblyName) {

    return GetDefaultAssemblyBuilder(compConfig, referencedAssemblies,
        configPath, null /*generatedFilesDir*/, outputAssemblyName);
}

internal static AssemblyBuilder GetDefaultAssemblyBuilder(CompilationSection compConfig,
    ICollection referencedAssemblies, VirtualPath configPath,
    string generatedFilesDir, string outputAssemblyName) {

    CompilerType ctwp = GetDefaultCompilerTypeWithParams(compConfig, configPath);
    return ctwp.CreateAssemblyBuilder(compConfig, referencedAssemblies,
        generatedFilesDir, outputAssemblyName);
}
#endif
}
