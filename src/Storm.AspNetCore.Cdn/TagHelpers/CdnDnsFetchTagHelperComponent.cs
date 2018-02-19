// <copyright file="CdnDnsFetchTagHelperComponent.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.TagHelpers
{
    using System;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Storm.AspNetCore.Cdn.Config;

    public class CdnDnsFetchTagHelperComponent : TagHelperComponent
    {
        private readonly CdnOptions options;

        public CdnDnsFetchTagHelperComponent(CdnOptions options)
        {
            this.options = options;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context.TagName.Equals("head", StringComparison.Ordinal))
            {
                foreach (var provider in this.options.Providers)
                {
                    var linkTag = $"<link rel=\"dns-prefetch\" href=\"{provider.Value.Host}\">";
                    output.PostContent.AppendHtml(linkTag);
                }
            }
        }
    }
}