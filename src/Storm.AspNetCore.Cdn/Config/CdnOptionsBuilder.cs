// <copyright file="CdnOptionsBuilder.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Config
{
    using Microsoft.Extensions.DependencyInjection;

    internal class CdnOptionsBuilder : ICdnOptionsBuilder
    {
        public CdnOptionsBuilder(IServiceCollection serviceCollection)
        {
            this.Services = serviceCollection;
        }

        public IServiceCollection Services { get; private set; }
    }
}