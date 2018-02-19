// <copyright file="PostConfigureCdnOptions.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Config
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Options;

    public class PostConfigureCdnOptions : IPostConfigureOptions<CdnOptions>
    {
        public void PostConfigure(string name, CdnOptions options)
        {
            if (options.Providers.Count > 1 && string.IsNullOrWhiteSpace(options.DefaultProvider))
            {
                throw new ArgumentException("You must specify a default cdn provider via configuration when you have multiple providers configured");
            }

            if (options.GetProviderOptionsOrDefault() == null)
            {
                throw new ArgumentOutOfRangeException(nameof(options.DefaultProvider), $"The default cdn provider name [{options.DefaultProvider}] can not be found in the list of configured providers [{string.Join(", ", options.Providers.Select(s => s.Key))}]");
            }

            if (options.Providers.Select(s => s.Value.Host).Any(s => !s.PathAndQuery.Equals("/") || !new[] { "http", "https" }.Contains(s.Scheme)))
            {
                throw new ArgumentException("The host property for all cdn providers must only use either HTTP or HTTPS, contain, hostname or IP address and optionally a port (e.g. https://cdn.example.com:1234)");
            }
        }
    }
}