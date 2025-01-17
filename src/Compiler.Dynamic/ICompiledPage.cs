// MIT License.

using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;

namespace WebForms.Compiler.Dynamic;

internal interface ICompiledPage : IDisposable
{
    PathString Path { get; }

    string AspxFile { get; }

    Type? Type { get; }

    Exception? Exception { get; }

    IReadOnlyCollection<string> FileDependencies { get; }

    ICollection<ICompiledPage> PageDependencies { get; }

    MetadataReference? MetadataReference { get; }
}
