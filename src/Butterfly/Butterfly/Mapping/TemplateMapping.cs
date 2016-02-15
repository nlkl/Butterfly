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
    public abstract class TemplateMapping : ITemplateMapping
    {
        protected IDictionary<Guid, Func<Item, ITemplateMapping, IItem>> Mappings { get; set; } = new Dictionary<Guid, Func<Item, ITemplateMapping, IItem>>();

        public virtual IItem Resolve(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            Func<Item, ITemplateMapping, IItem> itemFactory;
            if (Mappings != null && Mappings.TryGetValue(item.TemplateID.Guid, out itemFactory))
            {
                return itemFactory(item, this);
            }

            return new TypedItem(item, this);
        }

        protected Func<Item, ITemplateMapping, IItem> CreateItemFactory(Type type)
        {
            var ctor = type.GetConstructor(new[] { typeof(Item), typeof(ITemplateMapping) });
            if (ctor == null) throw new InvalidOperationException($"Butterfly model of type '{type.AssemblyQualifiedName}' does not have a constructor taking an item and a template mapping.");

            var itemParameter = Expression.Parameter(typeof(Item), "item");
            var templateMappingParameter = Expression.Parameter(typeof(ITemplateMapping), "templateMapping");

            var ctorExpression = Expression.New(ctor, itemParameter, templateMappingParameter);
            var itemFactoryExpression = Expression.Lambda<Func<Item, ITemplateMapping, IItem>>(ctorExpression, itemParameter, templateMappingParameter);

            return itemFactoryExpression.Compile();
        }
    }
}
