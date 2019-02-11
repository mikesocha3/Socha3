using System.Web.Mvc;
using System.Collections.Generic;

namespace  Socha3.MemeBox2000
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Button(this HtmlHelper helper, string InnerHtml, object HtmlAttributes)
        {
            return Button(helper, InnerHtml, HtmlHelper.AnonymousObjectToHtmlAttributes(HtmlAttributes));
        }

        public static MvcHtmlString Button(this HtmlHelper helper, string InnerHtml, IDictionary<string, object> HtmlAttributes)
        {
            var builder = new TagBuilder("button");
            builder.InnerHtml = InnerHtml;
            builder.MergeAttributes(HtmlAttributes);
            return MvcHtmlString.Create(builder.ToString());
        }

        //public static MvcHtmlString  Socha3.MemeBox2000SubmitButton(this HtmlHelper Helper, string FormName, IDictionary<string, object> HtmlAttributes)
        //{
        //    var builder = new TagBuilder("button");
        //    builder.InnerHtml = "onclick='$.post';
        //    builder.MergeAttributes(HtmlAttributes);
        //    return MvcHtmlString.Create(builder.ToString());
        //}
    }
}