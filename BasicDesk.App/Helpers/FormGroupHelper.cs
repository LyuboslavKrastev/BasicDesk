using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Text.Encodings.Web;

namespace BasicDesk.App.Helpers
{
    public static class FormGroupHelper
    {
        public static IHtmlContent InputFormGroupFor<TModel, TResult>(
               this IHtmlHelper<TModel> htmlHelper,
               Expression<Func<TModel, TResult>> expression)
        {
            using (var writer = new StringWriter())
            {
                var label = htmlHelper.LabelFor(expression, new { @class = "control-label col-sm-2" });
                var editor = htmlHelper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } });
                var validationMessage = htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" });

                writer.Write("<div class=\"form-group\">");
                label.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("<div class=\"col-sm-8\">");
                editor.WriteTo(writer, HtmlEncoder.Default);
                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div></div>");

                return new HtmlString(writer.ToString());
            }
        }

       public static IHtmlContent DisabledInputFormGroupFor<TModel, TResult>(
       this IHtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TResult>> expression)
        {
            using (var writer = new StringWriter())
            {
                var label = htmlHelper.LabelFor(expression, new { @class = "control-label col-sm-2" });
                var editor = htmlHelper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control", @disabled = "disabled"} });
                var validationMessage = htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" });

                writer.Write("<div class=\"form-group\">");
                label.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("<div class=\"col-sm-8\">");
                editor.WriteTo(writer, HtmlEncoder.Default);
                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div></div>");

                return new HtmlString(writer.ToString());
            }
        }

        public static IHtmlContent TextAreaFormGroupFor<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            using (var writer = new StringWriter())
            {
                var label = htmlHelper.LabelFor(expression, new { @class = "control-label col-sm-2" });
                var textArea = htmlHelper.TextAreaFor(expression, new { @class = "form-control", rows = "20" });
                var validationMessage = htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" });


                writer.Write("<div class=\"form-group\">");
                label.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("<div class=\"col-sm-8\">");
                textArea.WriteTo(writer, HtmlEncoder.Default);
                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div></div>");

                return new HtmlString(writer.ToString());
            }
        }
    }
}
