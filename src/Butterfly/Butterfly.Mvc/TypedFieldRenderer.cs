using Butterfly.Fields;
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
using System.Web.Mvc;

namespace Butterfly.Mvc
{
    public class TypedFieldRenderer
    {
        private const string renderFieldPipeline = "renderField";

        private readonly Item item;
        private readonly string fieldName;

        public TypedFieldRenderer(ITypedField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

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

            return new TypedFieldRenderingResult(firstPart, lastPart);
        }
    }
}
