using Butterfly.Fields;
using Optional;
using Sitecore.Mvc.Common;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Butterfly.Mvc
{
    internal class MvcFieldRenderer : IDisposable
    {
        private const string renderFieldPipeline = "renderField";

        private readonly ViewContext viewContext;
        private readonly object parameters;
        private readonly ITypedField field;

        private string lastRenderingPart = string.Empty;

        public MvcFieldRenderer(ITypedField field, object parameters = null)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            this.viewContext = ContextService.Get().GetCurrent<ViewContext>();
            this.field = field;
            this.parameters = parameters;

            var parts = RenderParts(field, parameters);
            lastRenderingPart = parts.LastPart;
            viewContext.Writer.Write(parts.FirstPart);
        }

        public void Dispose()
        {
            viewContext.Writer.Write(lastRenderingPart);
        }

        public static FieldRenderResult RenderParts(ITypedField field, object parameters = null)
        {
            var innerField = field.InnerField;
            var item = innerField.Item;

            var renderFieldArgs = new RenderFieldArgs()
            {
                Item = item,
                FieldName = innerField.Name,
            };

            if (parameters != null)
            {
                renderFieldArgs.ApplyParameters(parameters);
            }

            CorePipeline.Run(renderFieldPipeline, renderFieldArgs);
            var result = renderFieldArgs.Result;

            var firstPart = result?.FirstPart;
            var lastPart = result?.LastPart;

            return new FieldRenderResult(firstPart, lastPart);
        }

        public class FieldRenderResult
        {
            public string FirstPart { get; private set; }
            public string LastPart { get; private set; }

            public FieldRenderResult(string firstPart, string lastPart)
            {
                FirstPart = firstPart ?? string.Empty;
                LastPart = lastPart ?? string.Empty;
            }
        }
    }
}
