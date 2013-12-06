﻿using System;
using Microsoft.Web.Administration;

namespace FluentInstallation.Builders
{
    public interface IApplicationOptions : IFluentSyntax
    {
        IApplicationOptions UsingAlias(string alias);
        IApplicationOptions OnPath(string path);
        IApplicationOptions UsingApplicationPool(string applicationPoolName);
        IApplicationOptions ConfigureAdvancedOptions(Action<Application> options);
    }
}