using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Data;
using System.Reflection;
using System.Linq.Expressions;

namespace Butterfly.Mapping
{
    public class AutoTemplateMap : TemplateMap
    {
        public AutoTemplateMap(params Assembly[] assembliesToScan)
        {
            if (assembliesToScan == null) throw new ArgumentNullException(nameof(assembliesToScan));

            mappings = assembliesToScan
                .Where(a => a != null)
                .Select(ScanAssemblyForMappings)
                .Aggregate(Enumerable.Empty<KeyValuePair<Guid, Func<Item, IItem>>>(), (ms, m) => ms.Union(m))
                .ToDictionary(m => m.Key, m => m.Value);
        }

        private IEnumerable<KeyValuePair<Guid, Func<Item, IItem>>> ScanAssemblyForMappings(Assembly assembly)
        {
            var typesAndAttrs =
                from type in assembly.GetTypes()
                let attr = type.GetCustomAttribute<TemplateMappingAttribute>()
                where attr != null
                select new { Type = type, MappingAttribute = attr };

            foreach (var typeAndAttr in typesAndAttrs)
            {
                var templateId = typeAndAttr.MappingAttribute.TemplateId;
                var itemFactory = CreateItemFactory(typeAndAttr.Type);

                yield return new KeyValuePair<Guid, Func<Item, IItem>>(templateId, itemFactory);
            }
        }

        private Func<Item, IItem> CreateItemFactory(Type type)
        {
            var ctor = type.GetConstructor(new[] { typeof(Item) });
            if (ctor == null) throw new InvalidOperationException($"Automapped Butterfly models must have a constructor taking a single item as input.");
            var param = Expression.Parameter(typeof(Item), "item");
            var lambda = Expression.Lambda<Func<Item, IItem>>(Expression.New(ctor, param), param);

            return lambda.Compile();
        }
    }
}
