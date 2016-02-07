using Butterfly.Fields;
using Sitecore.Mvc.Common;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Butterfly.Mvc
{
    public static class MvcFieldRenderingExtensions
    {
        private const string renderFieldPipeline = "renderField";

        public static IHtmlString Render(this ITypedField field, object parameters = null)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));
            var renderingResult = field.GetRenderer().Render(parameters);
            return new HtmlString(renderingResult.ToString());
        }

        public static IDisposable BeginRender(this ITypedField field, object parameters = null)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            var viewContext = ContextService.Get().GetCurrent<ViewContext>();
            if (viewContext == null)
            {
                return null;
            }

            var renderingResult = field.GetRenderer().Render(parameters);
            return new MvcWrappedContent(viewContext, renderingResult.FirstPart, renderingResult.LastPart);
        }

        public static TypedFieldRenderer GetRenderer(this ITypedField field) => new TypedFieldRenderer(field);
    }
}
