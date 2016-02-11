using Butterfly.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Butterfly.Web.Fields
{
    public interface IRenderableField : IField, IHtmlString
    {
    }
}
