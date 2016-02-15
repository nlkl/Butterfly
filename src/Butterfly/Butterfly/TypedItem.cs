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
using Butterfly.Editing;
using Optional;
using Sitecore.Resources.Media;
using Sitecore;

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

        public string Name
        {
            get { return InnerItem.Name; }
            set
            {
                using (Edit())
                {
                    InnerItem.Name = value;
                }
            }
        }

        public string DisplayName
        {
            get { return InnerItem.DisplayName; }
            set
            {
                using (Edit())
                {
                    InnerItem[FieldIDs.DisplayName] = value;
                }
            }
        }

        public string Path => InnerItem.Paths.FullPath;
        public virtual IAxes Axes => axes;

        public TypedItem(Item item, ITemplateMapping templateMapping)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (templateMapping == null) throw new ArgumentNullException(nameof(templateMapping));

            this.innerItem = item;
            this.mapping = templateMapping;
            this.axes = new Axes(item, templateMapping);
        }

        public IDisposable Edit() => new BorrowingEditContext(InnerItem);

        public virtual Option<string> GenerateUrl()
        {
            if (InnerItem.Paths.IsMediaItem)
            {
                return MediaManager.GetMediaUrl(InnerItem).NoneWhen(string.IsNullOrWhiteSpace);
            }

            return LinkManager.GetItemUrl(InnerItem).NoneWhen(string.IsNullOrWhiteSpace);
        }
    }
}
