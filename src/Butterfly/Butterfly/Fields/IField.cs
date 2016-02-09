using Butterfly.Rendering;
using Sitecore.Data;
using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Butterfly.Fields
{
    public interface IField
    {
        Field InnerField { get; }
        ID Id { get; }

        string RawValue { get; set; }
        bool HasValue { get; }

        IFieldRenderer Renderer { get; }
    }
}
