using Butterfly.Fields;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
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

        private static IHtmlString Render(this ITypedField field, object parameters = null)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            var parts = MvcFieldRenderer.RenderParts(field, parameters);
            return new HtmlString(parts.FirstPart + parts.LastPart);
        }

        private static IDisposable BeginRender(this ITypedField field, object parameters = null)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            return new MvcFieldRenderer(field, parameters);
        }
    }
}
