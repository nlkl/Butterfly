using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace Butterfly.Mapping
{
    public abstract class TemplateMap : ITemplateMap
    {
        protected IDictionary<Guid, Func<Item, IItem>> mappings = new Dictionary<Guid, Func<Item, IItem>>();

        public virtual IItem Map(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            Func<Item, IItem> itemFactory;
            if (mappings != null && mappings.TryGetValue(item.TemplateID.Guid, out itemFactory))
            {
                return itemFactory(item);
            }

            return new TypedItem(item);
        }
    }
}
