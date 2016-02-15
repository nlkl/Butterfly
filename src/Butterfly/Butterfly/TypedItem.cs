using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;
using Butterfly.Mapping;
using Sitecore.Links;
using Sitecore.Data.Managers;

namespace Butterfly
{
    public class TypedItem : IItem
    {
        private readonly Item innerItem;
        private readonly ITemplateMapping mapping;
        private readonly IAxes axes;

        protected virtual ITemplateMapping Mapping => mapping;

        public Item InnerItem => innerItem;
        public ID Id => InnerItem.ID;

        public ID TemplateId => InnerItem.TemplateID;
        public IEnumerable<ID> TemplateIds
        {
            get
            {
                var templateIds = TemplateManager
                    .GetTemplate(InnerItem)?
                    .GetBaseTemplates()?
                    .Select(t => t.ID)?
                    .Distinct()?
                    .ToArray();

                return templateIds ?? Enumerable.Empty<ID>();
            }
        }

        public virtual IAxes Axes => axes;

        public string Name => InnerItem.Name;
        public string DisplayName => InnerItem.DisplayName;
        public string Path => InnerItem.Paths.FullPath;

        public TypedItem(Item item, ITemplateMapping templateMapping)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (templateMapping == null) throw new ArgumentNullException(nameof(templateMapping));

            this.innerItem = item;
            this.mapping = templateMapping;
            this.axes = new Axes(item, templateMapping);
        }

        public virtual string GenerateUrl() => LinkManager.GetItemUrl(InnerItem);

        public virtual string GenerateUrl(UrlOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            return LinkManager.GetItemUrl(InnerItem, options);
        }
    }
}
