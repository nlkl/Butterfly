using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Mapping
{
    public interface ITemplateMapping
    {
        IItem Resolve(Item item);
    }
}
