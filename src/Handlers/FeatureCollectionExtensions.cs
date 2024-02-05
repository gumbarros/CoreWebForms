// MIT License.

#if !NET8_0_OR_GREATER

using Microsoft.AspNetCore.Http.Features;

namespace Microsoft.AspNetCore.SystemWebAdapters;

internal static class FeatureCollectionExtensions
{
    public static T GetRequiredFeature<T>(this IFeatureCollection features)
        => features.Get<T>() ?? throw new InvalidOperationException($"No feature of type {typeof(T)} is available.");
}

#endif
