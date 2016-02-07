using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Rendering
{
    public class TypedFieldRenderingResult : IFieldRenderingResult
    {
        public string Result => BeginResult + EndResult;
        public string BeginResult { get; private set; }
        public string EndResult { get; private set; }

        public TypedFieldRenderingResult(string firstPart, string lastPart)
        {
            BeginResult = firstPart ?? string.Empty;
            EndResult = lastPart ?? string.Empty;
        }
    }
}
