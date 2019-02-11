using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Socha3.Common.Extensions
{
    public static class IHtmlContentExtensions
    {
        public static HtmlString ToHtmlString(this IHtmlContent content)
        {
            return new HtmlString(content.ToString());
        }

    }
}
