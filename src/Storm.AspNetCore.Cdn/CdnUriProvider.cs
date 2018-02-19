// <copyright file="CdnUriProvider.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn
{
    using System;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Storm.AspNetCore.Cdn.Config;

    public class CdnUriProvider : ICdnUriProvider
    {
        private readonly IOptions<CdnOptions> cdnOptions;
        private readonly ICdnUriVersionProvider cdnUriVersionProvider;

        public CdnUriProvider(IOptions<CdnOptions> cdnOptions, ICdnUriVersionProvider cdnUriVersionProvider)
        {
            this.cdnOptions = cdnOptions;
            this.cdnUriVersionProvider = cdnUriVersionProvider;
        }

        public Uri GetUri(string resourceUri, string providerName)
        {
            if (this.cdnOptions.Value.Providers.ContainsKey(providerName ?? string.Empty))
            {
                return this.GetUri(resourceUri, this.cdnOptions.Value.Providers[providerName]);
            }

            return this.GetUri(resourceUri, this.cdnOptions.Value.GetProviderOptionsOrDefault());
        }

        public Uri GetUri(string resourceUri)
        {
            return this.GetUri(resourceUri, this.cdnOptions.Value.GetProviderOptionsOrDefault());
        }

        protected virtual bool ShouldIncludeVersion(string resourceUri, CdnProviderOptions providerOptions)
        {
            if (!string.IsNullOrWhiteSpace(providerOptions.VersionPattern))
            {
                var includeVersion = new Regex(
                    providerOptions.VersionPattern,
                    RegexOptions.CultureInvariant | RegexOptions.Singleline);

                return includeVersion.IsMatch(resourceUri);
            }

            return false;
        }

        protected virtual Uri GetCdnUriWithVersion(Uri cdnUri, string versionString)
        {
            var qs = QueryString.FromUriComponent(cdnUri).Add("_v", versionString);
            var cdnUriBuilder = new UriBuilder(cdnUri)
            {
                Query = qs.ToUriComponent()
            };
            return cdnUriBuilder.Uri;
        }

        protected virtual bool ShouldExcludeFromCdnForPath(string resourceUri, CdnProviderOptions providerOptions)
        {
            if (!string.IsNullOrWhiteSpace(providerOptions.ExcludePattern))
            {
                var excludePattern = new Regex(
                    providerOptions.ExcludePattern,
                    RegexOptions.CultureInvariant | RegexOptions.Singleline);

                return excludePattern.IsMatch(resourceUri);
            }

            return false;
        }

        private Uri GetUri(string resourceUri, CdnProviderOptions providerOptions)
        {
            if (Uri.IsWellFormedUriString(resourceUri, UriKind.Absolute) ||
                this.ShouldExcludeFromCdnForPathCore(resourceUri, providerOptions))
            {
                return new Uri(resourceUri, UriKind.RelativeOrAbsolute);
            }

            resourceUri = resourceUri.StartsWith("/") ? resourceUri : $"/{resourceUri}";

            var cdnUriBuilder = new UriBuilder(providerOptions.Host)
            {
                Path = providerOptions.Prefix.Add(resourceUri)
            };

            var cdnUri = cdnUriBuilder.Uri;

            if (this.ShouldIncludeVersionCore(resourceUri, providerOptions))
            {
                return this.GetCdnUriWithVersion(cdnUri, this.cdnUriVersionProvider.GetVersion(resourceUri, providerOptions));
            }

            return cdnUri;
        }

        private bool ShouldExcludeFromCdnForPathCore(string resourceUri, CdnProviderOptions providerOptions)
        {
            if (string.IsNullOrWhiteSpace(resourceUri) || resourceUri.StartsWith("~"))
            {
                return true;
            }

            return this.ShouldExcludeFromCdnForPath(resourceUri, providerOptions);
        }

        private bool ShouldIncludeVersionCore(string resourceUri, CdnProviderOptions providerOptions)
        {
            return this.ShouldIncludeVersion(resourceUri, providerOptions);
        }
    }
}
