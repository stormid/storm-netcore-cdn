// <copyright file="FromAssemblyCdnUriVersionProvider.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn
{
    using System;
    using System.Text;
    using Storm.AspNetCore.Cdn.Config;

    public class FromAssemblyCdnUriVersionProvider<TAssemblyOfType> : ICdnUriVersionProvider
    {
        private static readonly string VersionString = GetAssemblyVersionStamp();

        public string GetVersion(string resourceUri, CdnProviderOptions cdnProviderOptions)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(GetAssemblyVersionStamp()));
        }

        private static string GetAssemblyVersionStamp()
        {
            return typeof(TAssemblyOfType).Assembly.GetName().Version.ToString(3);
        }
    }
}