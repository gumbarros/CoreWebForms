// MIT License.

using System.Text;
using System.Web.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using WebForms;

namespace System.Web;

internal sealed class VirtualPath
{
    private static IFileProvider Files => HttpRuntimeHelper.Services.GetRequiredService<IHostEnvironment>().ContentRootFileProvider;

    public VirtualPath Parent
    {
        get
        {
            var idx = Path.LastIndexOfAny(new[] { '/', '\\' });

            if (idx == -1)
            {
                return "/";
            }

            var result = Path.Substring(0, idx);

            return result + "/";
        }
    }
    public VirtualPath(string path)
    {
        Path = Resolve(path);
    }

    public static string Resolve(string url)
    {
        if (!url.StartsWith('~'))
        {
            return url;
        }

        var vdir = HttpRuntime.AppDomainAppVirtualPath;

        var sb = new StringBuilder(url, 1, url.Length - 1, url.Length + vdir.Length);

        if (sb.Length == 0 || sb[0] != '/')
        {
            sb.Insert(0, '/');
        }

        sb.Insert(0, vdir);
        sb.Replace("//", "/");

        return sb.ToString();
    }

    public string Path { get; }

    public string VirtualPathStringNoTrailingSlash
    {
        get
        {
            if (Path.EndsWith("/"))
            {
                return Path;
            }

            return Path + "/";
        }
    }
    public bool IsRelative
    {
        get
        {
            // Note that we don't need to check for "~/", since _virtualPath never contains
            // app relative paths (_appRelativeVirtualPath does)
            return Path != null && Path[0] != '/';
        }
    }

    public VirtualPath Combine(VirtualPath relativePath)
    {
        if (relativePath == null)
        {
            throw new ArgumentNullException(nameof(relativePath));
        }

        // If it's not relative, return it unchanged
        if (!relativePath.IsRelative)
        {
            return relativePath;
        }

        // The base of the combine should never be relative
        FailIfRelativePath();

        // Combine it with the relative
        var virtualPath = UrlPath2.Combine(Path, relativePath.VirtualPathString);

        // Set the appropriate virtual path in the new object
        return new VirtualPath(virtualPath);
    }

    internal void FailIfRelativePath()
    {
        if (IsRelative)
        {
            throw new ArgumentException("Must be relative path");
        }
    }

    public string VirtualPathString => Path;

    public string Extension => IO.Path.GetExtension(Path);

    public string FileName => IO.Path.GetFileName(Path);

    public object AppRelativeVirtualPathString => Path;

    public static implicit operator VirtualPath(string path) => new(path);
    public static implicit operator string(VirtualPath vpath) => vpath.Path;

    public static VirtualPath CreateAllowNull(string path) => new(path);

    internal static VirtualPath CreateNonRelativeAllowNull(string v) => v;

    internal static string GetAppRelativeVirtualPathStringOrEmpty(VirtualPath vpath)
        => vpath.Path;

    internal static string GetVirtualPathString(VirtualPath vpath)
        => vpath?.Path;

    internal string GetAppRelativeVirtualPathString(VirtualPath templateControlVirtualPath)
    {
        throw new NotImplementedException();
    }

    internal static VirtualPath CreateNonRelative(string value)
    {
        return value;
    }

    internal Stream OpenFile() => Files.GetFileInfo(Path).CreateReadStream();

    internal static VirtualPath Create(string filename) => filename;

    internal bool FileExists() => Files.GetFileInfo(Path).Exists;

    internal string MapPath()
    {
        throw new NotImplementedException();
    }

    internal static VirtualPath CreateTrailingSlash(string virtualPath)
    {
        throw new NotImplementedException();
    }
}
