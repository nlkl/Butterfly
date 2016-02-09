using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace Butterfly.Mapping
{
    public class ManualTemplateMap : TemplateMap
    {
        public void Register(TemplateID templateId, Func<Item, IItem> itemFactory)
        {
            Register(templateId.ID.Guid, itemFactory);
        }

        public void Register(ID templateId, Func<Item, IItem> itemFactory)
        {
            Register(templateId.Guid, itemFactory);
        }

        public void Register(Guid templateId, Func<Item, IItem> itemFactory)
        {
            if (itemFactory == null) throw new ArgumentNullException(nameof(itemFactory));
            mappings.Add(templateId, itemFactory);
        }
    }
}
