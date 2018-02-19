// <copyright file="ScriptCdnTagHelper.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement(TagName)]
    public class ScriptCdnTagHelper : CdnTagHelper
    {
        private const string TagName = "script";

        public ScriptCdnTagHelper(ICdnUriProvider cdnUriProvider)
            : base(cdnUriProvider, "src")
        {
        }
    }
}