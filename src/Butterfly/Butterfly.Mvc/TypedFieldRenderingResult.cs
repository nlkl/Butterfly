using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Mvc
{
    public class TypedFieldRenderingResult
    {
        public string FirstPart { get; private set; }
        public string LastPart { get; private set; }

        public override string ToString() => FirstPart + LastPart;

        public TypedFieldRenderingResult(string firstPart, string lastPart)
        {
            FirstPart = firstPart ?? string.Empty;
            LastPart = lastPart ?? string.Empty;
        }
    }
}
