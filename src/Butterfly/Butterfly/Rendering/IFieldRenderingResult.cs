using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Rendering
{
    public interface IFieldRenderingResult
    {
        string Result { get; }
        string BeginResult { get; }
        string EndResult { get; }
    }
}
