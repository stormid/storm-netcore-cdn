// <copyright file="ConfigureCdnOptions.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Config
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class ConfigureCdnOptions : IConfigureOptions<CdnOptions>
    {
        private readonly IConfiguration configuration;

        public ConfigureCdnOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(CdnOptions options)
        {
            this.configuration.GetSection("Cdn").Bind(options);
        }
    }
}