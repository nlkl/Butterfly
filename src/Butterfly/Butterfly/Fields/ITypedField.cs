using Optional;
using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Butterfly.Fields
{
    public interface ITypedField
    {
        Field InnerField { get; }
        string RawValue { get; set; }
    }
}
