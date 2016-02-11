using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;
using Optional;
using Butterfly.Mapping;
using Sitecore.Globalization;
using Sitecore.Links;

namespace Butterfly
{
    public class TypedItem : IItem
    {
        private readonly Item innerItem;

        public Item InnerItem => innerItem;

        public ID Id => InnerItem.ID;
        public ID TemplateId => InnerItem.TemplateID;

        public IEnumerable<ID> TemplateIds
        {
            get
            {
                var templateIds = InnerItem
                    .Template?
                    .BaseTemplates?
                    .Select(t => t.ID)?
                    .Distinct()?
                    .ToArray();

                return templateIds ?? Enumerable.Empty<ID>();
            }
        }

        public string Name => InnerItem.Name;
        public string DisplayName => InnerItem.DisplayName;

        public ItemUri Uri => InnerItem.Uri;
        public string DatabaseName => InnerItem.Database.Name;
        public string LanguageName => InnerItem.Language.Name;
        public string Path => InnerItem.Paths.FullPath;
        public int Version => InnerItem.Version.Number;
        public bool IsLatestVersion => InnerItem.Versions.IsLatestVersion();

        public TypedItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            this.innerItem = item;
        }

        public virtual string GenerateUrl()
        {
            throw new NotImplementedException();
        }

        public virtual string GenerateUrl(UrlOptions options)
        {
            throw new NotImplementedException();
        }

        public Option<IItem> Parent() => innerItem.Parent.SomeNotNull().Map(MapItem);
        public Option<TItem> Parent<TItem>() where TItem : IItem => InnerItem.Parent.SomeNotNull().FlatMap(MapItemAs<TItem>);

        public Option<IItem> NextSibling() => InnerItem.Axes.GetNextSibling().SomeNotNull().Map(MapItem);
        public Option<TItem> NextSibling<TItem>() where TItem : IItem => InnerItem.Axes.GetNextSibling().SomeNotNull().FlatMap(MapItemAs<TItem>);

        public Option<IItem> PreviousSibling() => InnerItem.Axes.GetPreviousSibling().SomeNotNull().Map(MapItem);
        public Option<TItem> PreviousSibling<TItem>() where TItem : IItem => InnerItem.Axes.GetPreviousSibling().SomeNotNull().FlatMap(MapItemAs<TItem>);

        public IEnumerable<IItem> Children() => InnerItem.Children.Select(MapItem).ToArray();
        public IEnumerable<TItem> Children<TItem>() where TItem : IItem => Children().OfType<TItem>();

        public IEnumerable<IItem> Ancestors() => InnerItem.Axes.GetAncestors().Select(MapItem).ToArray();
        public IEnumerable<TItem> Ancestors<TItem>() where TItem : IItem => Ancestors().OfType<TItem>();

        public IEnumerable<IItem> Descendants() => InnerItem.Axes.GetDescendants().Select(MapItem).ToArray();
        public IEnumerable<TItem> Descendants<TItem>() where TItem : IItem => Descendants().OfType<TItem>();

        protected virtual IItem MapItem(Item item) => TemplateMapper.Map(item);
        protected virtual Option<TItem> MapItemAs<TItem>(Item item) where TItem : IItem => TemplateMapper.MapAs<TItem>(item);
    }
}
