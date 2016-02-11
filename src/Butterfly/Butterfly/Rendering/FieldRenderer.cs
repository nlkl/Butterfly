using Butterfly.Fields;
using Optional;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Butterfly.Rendering
{
    public class FieldRenderer : IFieldRenderer
    {
        private const string renderFieldPipeline = "renderField";

        private readonly Item item;
        private readonly string fieldName;

        public Option<IFieldRenderingResult> RenderingResult { get; private set; } = Option.None<IFieldRenderingResult>();

        public FieldRenderer(Item item, string fieldName)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (fieldName == null) throw new ArgumentNullException(nameof(fieldName));

            this.item = item;
            this.fieldName = fieldName;
        }

        public IFieldRenderingResult Render(object parameters = null)
        {
            var renderFieldArgs = new RenderFieldArgs()
            {
                Item = item,
                FieldName = fieldName
            };

            if (parameters != null)
            {
                renderFieldArgs.ApplyParameters(parameters);
            }

            CorePipeline.Run(renderFieldPipeline, renderFieldArgs);
            var result = renderFieldArgs.Result;

            var beginResult = result?.FirstPart;
            var endResult = result?.LastPart;

            var renderingResult = new FieldRenderingResult(beginResult, endResult);
            RenderingResult = renderingResult.Some<IFieldRenderingResult>();

            return renderingResult;
        }
    }
}
