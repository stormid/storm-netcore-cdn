// <copyright file="CdnClassFixture.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Tests
{
    using System;

    using DotNet.Extensions.FileProviders;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Storm.AspNetCore.Cdn.Config;

    public class CdnClassFixture
    {
        public IServiceProvider GetServiceProvider(string jsonConfiguration = null)
        {
            var configBuilder = new ConfigurationBuilder();

            if (!string.IsNullOrWhiteSpace(jsonConfiguration))
            {
                configBuilder.AddJsonFile(s => { s.FileProvider = new StringFileProvider(jsonConfiguration); });
            }

            var config = configBuilder.Build();

            var sc = new ServiceCollection();

            this.ConfigureServiceCollection(sc);

            sc
                .AddSingleton<IConfiguration>(config)
                .AddOptions()
                .AddCdn();

            return sc.BuildServiceProvider();
        }

        protected virtual void ConfigureServiceCollection(IServiceCollection serviceCollection)
        {
        }
    }
}