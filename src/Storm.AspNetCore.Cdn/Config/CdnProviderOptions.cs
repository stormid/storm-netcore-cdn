// <copyright file="CdnProviderOptions.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Config
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class CdnProviderOptions
    {
        /// <summary>
        /// Gets or sets the Cdn host name and should only contain the uri scheme (http or https) and the host name or ip address (with optional port) of the cdn host.  It should not include any additional paths, use the <see cref="Prefix"/> configuration option to specify any Cdn paths
        /// </summary>
        public Uri Host { get; set; }

        /// <summary>
        /// Gets or sets the path prefix to prepend to all resource uri's when generating a Cdn uri, the prefix must begin with a '/'
        /// </summary>
        public PathString Prefix { get; set; } = "/";

        /// <summary>
        /// Gets or sets a regular expression pattern used to determine whether to apply the Cdn uri to a given subpath
        /// </summary>
        /// <example>(*.svg|*.woff)</example>
        public string ExcludePattern { get; set; }

        /// <summary>
        /// Gets or sets a regular expression pattern used to determine whether to append a version querystring attribute to the generated Cdn uri
        /// </summary>
        /// <example>(.*)</example>
        public string VersionPattern { get; set; }
    }
}