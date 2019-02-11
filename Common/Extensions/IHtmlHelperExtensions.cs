using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Socha3.Common.Extensions
{
    public static class IHtmlHelperExtensions
    {
        public static HtmlString Button(this IHtmlHelper helper, string InnerHtml, object HtmlAttributes)
        {
            return Button(helper, InnerHtml, HtmlHelper.AnonymousObjectToHtmlAttributes(HtmlAttributes));
        }

        public static HtmlString Button(this IHtmlHelper helper, string InnerHtml, IDictionary<string, object> HtmlAttributes)
        {
            var builder = new TagBuilder("button");
            builder.InnerHtml.SetContent(InnerHtml);
            builder.MergeAttributes(HtmlAttributes);
            return new HtmlString(builder.ToString());
        }
    }
}
