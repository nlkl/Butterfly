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
            Mappings.Add(templateId, itemFactory);
        }

        public void Register<TItem>(TemplateID templateId) where TItem : IItem
        {
            Register<TItem>(templateId.ID.Guid);
        }

        public void Register<TItem>(ID templateId) where TItem : IItem
        {
            Register<TItem>(templateId.Guid);
        }

        public void Register<TItem>(Guid templateId) where TItem : IItem
        {
            var itemFactory = CreateItemFactory(typeof(TItem));
            Mappings.Add(templateId, itemFactory);
        }
    }
}
