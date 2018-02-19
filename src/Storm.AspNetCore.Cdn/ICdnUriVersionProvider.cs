// <copyright file="ICdnUriVersionProvider.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn
{
    using Storm.AspNetCore.Cdn.Config;

    public interface ICdnUriVersionProvider
    {
        string GetVersion(string resourceUri, CdnProviderOptions cdnProviderOptions);
    }
}