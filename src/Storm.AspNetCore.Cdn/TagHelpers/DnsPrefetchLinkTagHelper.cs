// <copyright file="DnsPrefetchLinkTagHelper.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.TagHelpers
{
    using System;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Storm.AspNetCore.Cdn.Config;

    [HtmlTargetElement("link", Attributes = "[rel=dns-prefetch]", TagStructure = TagStructure.WithoutEndTag)]
    public class DnsPrefetchLinkTagHelper : TagHelper
    {
        private const string CdnProviderAttributeName = "storm-cdn-provider";
        private readonly CdnOptions cdnOptions;

        public DnsPrefetchLinkTagHelper(CdnOptions cdnOptions)
        {
            this.cdnOptions = cdnOptions;
        }

        [HtmlAttributeName(CdnProviderAttributeName)]
        public string Provider { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                var provider = this.cdnOptions.GetProviderOptionsOrDefault(this.Provider);
                output.Attributes.SetAttribute("href", provider.Host);
            }
            catch (Exception)
            {
                output.SuppressOutput();
            }
        }
    }
}