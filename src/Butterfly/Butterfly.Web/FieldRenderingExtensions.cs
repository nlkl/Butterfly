using Butterfly.Fields;
using Optional.Unsafe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Butterfly.Web
{
    public static class FieldRenderingExtensions
    {
        private const string renderFieldPipeline = "renderField";

        public static IHtmlString Render(this IField field, object parameters = null)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            var renderingResult = field.Renderer.Render(parameters);
            return new HtmlString(renderingResult.Result);
        }

        public static IHtmlString RenderBegin(this IField field, object parameters = null)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            var renderingResult = field.Renderer.Render(parameters);
            return new HtmlString(renderingResult.BeginResult);
        }

        public static IHtmlString RenderEnd(this IField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            var renderingResult = field.Renderer
                .RenderingResult
                .ValueOrFailure("Cannot render the last part of a field, before rendering the first part.");
            return new HtmlString(renderingResult.EndResult);
        }
    }
}
