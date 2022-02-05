using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTracking.Common.ExtensionMethod;

namespace TimeTracking.Web.Helper.ExtensionMethod
{
    public static class HtmlHelper
    {


        public static IHtmlContent RadioButtonFromEnum<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression,
            Type type
            )
        {
            var result = Enum.GetValues(type).Cast<object>()
                .Aggregate(string.Empty, (current, item) =>
                    $"{current}<div class=\"col-6\"><label>{htmlHelper.RadioButtonFor(expression, item.To<int>(), new { id = item }).GetString()}&nbsp;&nbsp;{item.ToString().AddSpacesToSentence()}</label></div>");
            return new HtmlString(result);
        }

        public static IHtmlContent InputSubmit(this IHtmlHelper htmlHelper, string value)
        {
            return new HtmlString(
                $"<div class=\"form-group\"><input type=\"submit\" value=\"{value}\" class=\"btn btn-primary float-right\" /></div>");
        }

        public static string GetString(this IHtmlContent content)
        {
            using var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
