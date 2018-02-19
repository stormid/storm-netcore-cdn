// <copyright file="LinkCdnTagHelper.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement(TagName)]
    public class LinkCdnTagHelper : CdnTagHelper
    {
        private const string TagName = "link";

        public LinkCdnTagHelper(ICdnUriProvider cdnUriProvider)
            : base(cdnUriProvider, "href")
        {
        }
    }
}