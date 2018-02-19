// <copyright file="CdnOptionsBuilderExtensions.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Config
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.DependencyInjection;
    using Storm.AspNetCore.Cdn.TagHelpers;

    public static class CdnOptionsBuilderExtensions
    {
        /// <summary>
        /// Determines whether to append a dns-prefetch link tag to the document head for each configured cdn provider
        /// </summary>
        /// <param name="builder">the builder options</param>
        /// <param name="enabled">Sets whether to apply the dns-prefetch tags</param>
        /// <returns>the current builder options</returns>
        public static ICdnOptionsBuilder ApplyDnsPrefetchTags(this ICdnOptionsBuilder builder, bool enabled = true)
        {
            if (enabled)
            {
                builder.Services.AddSingleton<ITagHelperComponent, CdnDnsFetchTagHelperComponent>();
            }

            return builder;
        }

        /// <summary>
        /// Optionally specifies a custom implementation of <see cref="ICdnUriVersionProvider"/> to use when creating a version string to append to a matching cdn uri
        /// </summary>
        /// <typeparam name="TCdnUriVersionProvider">A class that generates a suitable string to append to a cdn uri as a version or cachebusting attribute</typeparam>
        /// <param name="builder">the options builder</param>
        /// <returns>the current options builder</returns>
        public static ICdnOptionsBuilder UsingVersionProvider<TCdnUriVersionProvider>(this ICdnOptionsBuilder builder)
            where TCdnUriVersionProvider : class, ICdnUriVersionProvider
        {
            builder.Services.AddSingleton<ICdnUriVersionProvider, TCdnUriVersionProvider>();
            return builder;
        }

        /// <summary>
        /// Optionally specifies a custom implementation of <see cref="CdnUriProvider"/> used to process resource uri's into cdn uri's
        /// </summary>
        /// <remarks>ONly for advanced custom usage scenarios</remarks>
        /// <typeparam name="TCdnUriProvider">A class that generates a cdn uri</typeparam>
        /// <param name="builder">the options builder</param>
        /// <returns>the current options builder</returns>
        public static ICdnOptionsBuilder UsingCdnUriProvider<TCdnUriProvider>(this ICdnOptionsBuilder builder)
            where TCdnUriProvider : CdnUriProvider
        {
            builder.Services.AddSingleton<ICdnUriProvider, TCdnUriProvider>();
            return builder;
        }
    }
}