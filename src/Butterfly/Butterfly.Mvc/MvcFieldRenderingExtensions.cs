using Butterfly.Fields;
using Optional.Unsafe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Butterfly.Mvc
{
    public static class MvcFieldRenderingExtensions
    {
        private const string renderFieldPipeline = "renderField";

        public static IHtmlString Render(this ITypedField field, object parameters = null)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            var renderingResult = field.Renderer.Render(parameters);
            return new HtmlString(renderingResult.ToString());
        }

        public static IHtmlString RenderFirstPart(this ITypedField field, object parameters = null)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            var renderingResult = field.Renderer.Render(parameters);
            return new HtmlString(renderingResult.FirstPart);
        }

        public static IHtmlString RenderLastPart(this ITypedField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            var renderingResult = field.Renderer
                .RenderingResult
                .ValueOrFailure("Cannot render the last part of a field, before rendering the first part.");
            return new HtmlString(renderingResult.LastPart);
        }
    }
}
