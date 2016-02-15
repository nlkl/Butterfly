using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optional;
using Butterfly.Mapping;
using Sitecore.Data;

namespace Butterfly
{
    public class Axes : IAxes
    {
        private readonly Item ownerItem;
        private readonly ITemplateMapping mapping;

        public Axes(Item item, ITemplateMapping templateMapping)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (templateMapping == null) throw new ArgumentNullException(nameof(templateMapping));
            this.ownerItem = item;
            this.mapping = templateMapping;
        }

        public Option<IItem> Parent() => ownerItem.Parent.SomeNotNull().Map(mapping.Resolve);
        public Option<TItem> Parent<TItem>() where TItem : IItem => As<TItem>(Parent());

        public Option<IItem> PreviousSibling() => ownerItem.Axes.GetPreviousSibling().SomeNotNull().Map(mapping.Resolve);
        public Option<TItem> PreviousSibling<TItem>() where TItem : IItem => As<TItem>(PreviousSibling());

        public Option<IItem> NextSibling() => ownerItem.Axes.GetNextSibling().SomeNotNull().Map(mapping.Resolve);
        public Option<TItem> NextSibling<TItem>() where TItem : IItem => As<TItem>(NextSibling());

        public IEnumerable<IItem> Ancestors() => ownerItem.Axes.GetAncestors().Select(mapping.Resolve).ToArray();
        public IEnumerable<TItem> Ancestors<TItem>() where TItem : IItem => As<TItem>(Ancestors());

        public IEnumerable<IItem> AncestorsAndSelf() => Ancestors().Concat(new[] { mapping.Resolve(ownerItem) }).ToArray();
        public IEnumerable<TItem> AncestorsAndSelf<TItem>() where TItem : IItem => As<TItem>(AncestorsAndSelf());

        public IEnumerable<IItem> Children() => ownerItem.Children.Select(mapping.Resolve).ToArray();
        public IEnumerable<TItem> Children<TItem>() where TItem : IItem => As<TItem>(Children());

        public IEnumerable<IItem> Descendants() => ownerItem.Axes.GetDescendants().Select(mapping.Resolve).ToArray();
        public IEnumerable<TItem> Descendants<TItem>() where TItem : IItem => As<TItem>(Descendants());

        public IEnumerable<IItem> DescendantsAndSelf() => Descendants().Concat(new[] { mapping.Resolve(ownerItem) }).ToArray();
        public IEnumerable<TItem> DescendantsAndSelf<TItem>() where TItem : IItem => As<TItem>(DescendantsAndSelf());

        public IEnumerable<IItem> Siblings() => Siblings(false);
        public IEnumerable<TItem> Siblings<TItem>() where TItem : IItem => As<TItem>(Siblings());

        public IEnumerable<IItem> SiblingsAndSelf() => Siblings(true);
        public IEnumerable<TItem> SiblingsAndSelf<TItem>() where TItem : IItem => As<TItem>(SiblingsAndSelf());

        private IEnumerable<IItem> Siblings(bool includeSelf)
        {
            var siblings = new List<Item>();

            var sibling = ownerItem.Axes.GetPreviousSibling();
            while (sibling != null)
            {
                siblings.Add(sibling);
                sibling = sibling.Axes.GetPreviousSibling();
            }
            siblings.Reverse();

            if (includeSelf)
            {
                siblings.Add(ownerItem);
            }

            sibling = ownerItem.Axes.GetNextSibling();
            while (sibling != null)
            {
                siblings.Add(sibling);
                sibling = sibling.Axes.GetNextSibling();
            }

            return siblings.Select(mapping.Resolve).ToArray();
        }

        private Option<TItem> As<TItem>(Option<IItem> item) where TItem : IItem => item.Filter(i => i is TItem).Map(i => (TItem)i);
        private IEnumerable<TItem> As<TItem>(IEnumerable<IItem> item) where TItem : IItem => item.OfType<TItem>().ToArray();
    }
}
