using Optional;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Mapping
{
    public static class TemplateMapper
    {
        private static ITemplateMap map = new ManualTemplateMap();

        public static Option<TItem> MapAs<TItem>(Item item) where TItem : IItem
        {
            return Map(item).SomeWhen(i => i is TItem).Map(i => (TItem)i);
        }

        public static IItem Map(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return map.Map(item);
        }

        public static void Configure(ITemplateMap templateMap)
        {
            map = templateMap;
        }
    }
}
