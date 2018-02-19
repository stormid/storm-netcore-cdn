// <copyright file="CdnTagHelper.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    public abstract class CdnTagHelper : TagHelper
    {
        private readonly ICdnUriProvider cdnUriProvider;
        private readonly string urlAttributeName;

        public CdnTagHelper(ICdnUriProvider cdnUriProvider, string urlAttributeName)
        {
            this.cdnUriProvider = cdnUriProvider;
            this.urlAttributeName = urlAttributeName;
        }

        [HtmlAttributeName("storm-cdn-ignore")]
        public bool IgnoreCdn { get; set; }

        [HtmlAttributeName("storm-cdn-provider")]
        public string Provider { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!this.IgnoreCdn)
            {
                if (context.AllAttributes.TryGetAttribute(this.urlAttributeName, out var src))
                {
                    var srcValue = src.Value.ToString();
                    if (!srcValue.StartsWith("~"))
                    {
                        var cdnUri = this.cdnUriProvider.GetUri(src.Value.ToString(), this.Provider);
                        output.Attributes.SetAttribute(src.Name, cdnUri);
                    }
                }
            }
        }
    }
}