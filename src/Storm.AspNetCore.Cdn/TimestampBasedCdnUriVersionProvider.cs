// <copyright file="TimestampBasedCdnUriVersionProvider.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn
{
    using System;
    using Storm.AspNetCore.Cdn.Config;

    public class TimestampBasedCdnUriVersionProvider : ICdnUriVersionProvider
    {
        public string GetVersion(string resourceUri, CdnProviderOptions cdnProviderOptions)
        {
            return DateTimeOffset.UtcNow.Ticks.ToString();
        }
    }
}