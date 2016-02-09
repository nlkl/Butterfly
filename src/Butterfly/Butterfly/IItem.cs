using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly
{
    public interface IItem
    {
        Item InnerItem { get; }
        ID Id { get; }

        string Name { get; }
        string DisplayName { get; }

        // TODO: Add rest of interface
    }
}
