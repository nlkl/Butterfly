using Optional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Rendering
{
    public interface IFieldRenderer
    {
        Option<IFieldRenderingResult> RenderingResult { get; }
        IFieldRenderingResult Render(object parameters = null);
    }
}
