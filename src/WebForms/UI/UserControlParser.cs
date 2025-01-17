// MIT License.

/*
 * Implements the ASP.NET template parser
 *
 * Copyright (c) 1998 Microsoft Corporation
 */

namespace System.Web.UI;

using System;
using System.Collections;
using System.Web.Compilation;

/*
 * Parser for declarative controls
 */
internal class UserControlParser : TemplateControlParser
{
#if PORT_CACHING
    private string _provider;
    private bool _fSharedPartialCaching;
    internal bool FSharedPartialCaching { get { return _fSharedPartialCaching; } }
    internal string Provider { get { return _provider; } }
#else
    internal static bool FSharedPartialCaching => false;
    internal static string Provider => null;
#endif

    internal override BaseCodeDomTreeGenerator GetGenerator() => new UserControlCodeDomTreeGenerator(this);

    // Get default settings from config
    internal override void ProcessConfigSettings()
    {
        base.ProcessConfigSettings();

        ApplyBaseType();
    }

    // Get the default baseType from PagesConfig.
    internal virtual void ApplyBaseType()
    {
        if (PageParser.DefaultUserControlBaseType != null)
        {
            BaseType = PageParser.DefaultUserControlBaseType;
        }
        else if (PagesConfig != null && PagesConfig.UserControlBaseTypeInternal != null)
        {
            BaseType = PagesConfig.UserControlBaseTypeInternal;
        }
    }

    internal override Type DefaultBaseType { get { return typeof(System.Web.UI.UserControl); } }

    internal const string defaultDirectiveName = "control";
    internal override string DefaultDirectiveName
    {
        get { return defaultDirectiveName; }
    }

    internal override Type DefaultFileLevelBuilderType
    {
        get
        {
            return typeof(FileLevelUserControlBuilder);
        }
    }

    internal override RootBuilder CreateDefaultFileLevelBuilder()
    {

        return new FileLevelUserControlBuilder();
    }

    /*
     * Process the contents of the <%@ OutputCache ... %> directive
     */
    internal override void ProcessOutputCacheDirective(string directiveName, IDictionary directive)
    {
#if PORT_OUTPUTCACHING
        string sqlDependency;

        Util.GetAndRemoveBooleanAttribute(directive, "shared", ref _fSharedPartialCaching);

        _provider = Util.GetAndRemoveNonEmptyAttribute(directive, "providerName");
        if (_provider == OutputCache.ASPNET_INTERNAL_PROVIDER_NAME)
        {
            _provider = null;
        }
        OutputCache.ThrowIfProviderNotFound(_provider);

        sqlDependency = Util.GetAndRemoveNonEmptyAttribute(directive, "sqldependency");

        if (sqlDependency != null)
        {
            // Validate the sqldependency attribute
            SqlCacheDependency.ValidateOutputCacheDependencyString(sqlDependency, false);
            OutputCacheParameters.SqlDependency = sqlDependency;
        }

        base.ProcessOutputCacheDirective(directiveName, directive);
#endif
        throw new NotImplementedException("Output caching is not available");
    }

    internal override bool FVaryByParamsRequiredOnOutputCache
    {
        get { return OutputCacheParameters.VaryByControl == null; }
    }

    internal override string UnknownOutputCacheAttributeError
    {
        get { return SR.Attr_not_supported_in_ucdirective; }
    }
}

