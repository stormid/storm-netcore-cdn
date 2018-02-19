// <copyright file="ImgCdnTagHelper.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement(TagName)]
    public class ImgCdnTagHelper : CdnTagHelper
    {
        private const string TagName = "img";

        public ImgCdnTagHelper(ICdnUriProvider cdnUriProvider)
            : base(cdnUriProvider, "src")
        {
        }
    }
}