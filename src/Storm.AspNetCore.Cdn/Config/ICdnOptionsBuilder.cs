// <copyright file="ICdnOptionsBuilder.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Config
{
    using Microsoft.Extensions.DependencyInjection;

    public interface ICdnOptionsBuilder
    {
        IServiceCollection Services { get; }
    }
}