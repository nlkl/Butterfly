using Optional;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Mapping
{
    public static class TemplateMappingExtensions
    {
        public static Option<TItem> ResolveAs<TItem>(this ITemplateMapping mapping, Item item) where TItem : IItem
        {
            return mapping.Resolve(item).SomeWhen(i => i is TItem).Map(i => (TItem)i);
        }
    }
}
