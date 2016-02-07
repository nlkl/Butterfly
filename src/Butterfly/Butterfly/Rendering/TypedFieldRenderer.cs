using Butterfly.Fields;
using Butterfly.Utils;
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
    public class TypedFieldRenderer
    {
        private const string renderFieldPipeline = "renderField";

        private readonly Item item;
        private readonly string fieldName;

        public Option<TypedFieldRenderingResult> RenderingResult { get; private set; } = Option.None<TypedFieldRenderingResult>();

        public TypedFieldRenderer(ITypedField field)
        {
            Contracts.ArgNotNull(field, nameof(field));

            this.item = field.InnerField.Item;
            this.fieldName = field.InnerField.Name;
        }

        public TypedFieldRenderingResult Render(object parameters = null)
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

            var firstPart = result?.FirstPart;
            var lastPart = result?.LastPart;

            var renderingResult = new TypedFieldRenderingResult(firstPart, lastPart);
            RenderingResult = renderingResult.Some();

            return renderingResult;
        }
    }
}
