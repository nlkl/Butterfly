using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Data;
using System.Linq.Expressions;

namespace Butterfly.Mapping
{
    public abstract class TemplateMap : ITemplateMap
    {
        protected IDictionary<Guid, Func<Item, IItem>> Mappings { get; set; } = new Dictionary<Guid, Func<Item, IItem>>();

        public virtual IItem Map(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            Func<Item, IItem> itemFactory;
            if (Mappings != null && Mappings.TryGetValue(item.TemplateID.Guid, out itemFactory))
            {
                return itemFactory(item);
            }

            return new TypedItem(item);
        }

        protected Func<Item, IItem> CreateItemFactory(Type type)
        {
            var ctor = type.GetConstructor(new[] { typeof(Item) });
            if (ctor == null) throw new InvalidOperationException($"Butterfly model of type '{type.AssemblyQualifiedName}' does not have a constructor taking a single item as input.");

            var param = Expression.Parameter(typeof(Item), "item");
            var lambda = Expression.Lambda<Func<Item, IItem>>(Expression.New(ctor, param), param);

            return lambda.Compile();
        }
    }
}
