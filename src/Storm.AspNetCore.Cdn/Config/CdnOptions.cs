// <copyright file="CdnOptions.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CdnOptions
    {
        /// <summary>
        /// Gets or sets a value indicating the default cdn provider to be used when none is explicitly requested, not required when only 1 provider is configured
        /// </summary>
        public string DefaultProvider { get; set; }

        public Dictionary<string, CdnProviderOptions> Providers { get; set; } = new Dictionary<string, CdnProviderOptions>();

        internal CdnProviderOptions GetProviderOptionsOrDefault(string providerName = null)
        {
            if (!string.IsNullOrWhiteSpace(providerName))
            {
                if (this.Providers.ContainsKey(providerName))
                {
                    return this.Providers[providerName];
                }

                throw new ArgumentNullException(nameof(this.DefaultProvider), $"No cdn provider was not for the given provider name [{providerName}]");
            }

            if (this.Providers.Count == 1)
            {
                return this.Providers.First().Value;
            }

            if (!string.IsNullOrWhiteSpace(this.DefaultProvider) && this.Providers.ContainsKey(this.DefaultProvider))
            {
                return this.Providers[this.DefaultProvider];
            }

            throw new ArgumentNullException(nameof(this.DefaultProvider), "No default cdn provider was specified");
        }
    }
}