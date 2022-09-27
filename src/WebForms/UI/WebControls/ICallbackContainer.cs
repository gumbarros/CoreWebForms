// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Web.UI.WebControls;

public interface ICallbackContainer
{

    /// <summary>
    /// Enables controls to obtain client-side script options that will cause
    /// (when invoked) a server callback to the form on a button click.
    /// </summary>
    string GetCallbackScript(IButtonControl buttonControl, string argument);
}
