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
    public class AutoTemplateMapping : TemplateMapping
    {
        public AutoTemplateMapping(params Assembly[] assembliesToScan)
        {
            if (assembliesToScan == null) throw new ArgumentNullException(nameof(assembliesToScan));

            Mappings = assembliesToScan
                .Where(a => a != null)
                .Select(ScanAssemblyForMappings)
                .Aggregate(Enumerable.Empty<KeyValuePair<Guid, Func<Item, ITemplateMapping, IItem>>>(), (ms, m) => ms.Union(m))
                .ToDictionary(m => m.Key, m => m.Value);
        }

        private IEnumerable<KeyValuePair<Guid, Func<Item, ITemplateMapping, IItem>>> ScanAssemblyForMappings(Assembly assembly)
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

                yield return new KeyValuePair<Guid, Func<Item, ITemplateMapping, IItem>>(templateId, itemFactory);
            }
        }
    }
}
