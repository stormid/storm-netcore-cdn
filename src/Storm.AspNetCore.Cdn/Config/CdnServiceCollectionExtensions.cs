// <copyright file="CdnServiceCollectionExtensions.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Config
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;

    public static class CdnServiceCollectionExtensions
    {
        public static IServiceCollection AddCdn(this IServiceCollection serviceCollection, Action<ICdnOptionsBuilder> cdnOptionsBuilder = null)
        {
            var builder = new CdnOptionsBuilder(serviceCollection);

            cdnOptionsBuilder?.Invoke(builder);

            serviceCollection
                    .AddSingleton<IConfigureOptions<CdnOptions>, ConfigureCdnOptions>()
                    .AddSingleton<IPostConfigureOptions<CdnOptions>, PostConfigureCdnOptions>()
                    .AddSingleton(s => s.GetRequiredService<IOptions<CdnOptions>>().Value);

            serviceCollection.TryAddSingleton<ICdnUriProvider, CdnUriProvider>();
            serviceCollection.TryAddSingleton<ICdnUriVersionProvider, TimestampBasedCdnUriVersionProvider>();

            return serviceCollection;
        }
    }
}