// <copyright file="ICdnUriProvider.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn
{
    using System;

    public interface ICdnUriProvider
    {
        Uri GetUri(string resourceUri, string providerName);

        Uri GetUri(string resourceUri);
    }
}
