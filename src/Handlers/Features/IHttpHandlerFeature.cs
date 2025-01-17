// MIT License.

#if NETCOREAPP

using System.Web;

namespace Microsoft.AspNetCore.SystemWebAdapters.Features;

public interface IHttpHandlerFeature
{
    IHttpHandler? Current { get; set; }

    IHttpHandler? Previous { get; }
}

#endif
