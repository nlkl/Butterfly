using Butterfly.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Web.Fields
{
    public class RenderableField : TypedField, IRenderableField
    {
        public RenderableField(Item item, string fieldName) : base(item, fieldName)
        {
        }

        public string ToHtmlString() => Renderer.Render().Result;
    }
}
