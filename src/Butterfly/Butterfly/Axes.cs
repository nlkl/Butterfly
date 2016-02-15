using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optional;

namespace Butterfly
{
    public class Axes : IAxes
    {
        private readonly Item ownerItem;

        public Axes(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            this.ownerItem = item;
        }

        public IEnumerable<IItem> Ancestors()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> Ancestors<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItem> AncestorsAndSelf()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> AncestorsAndSelf<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItem> Children()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> Children<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItem> Descendants()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> Descendants<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItem> DescendantsAndSelf()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> DescendantsAndSelf<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }

        public Option<IItem> NextSibling()
        {
            throw new NotImplementedException();
        }

        public Option<TItem> NextSibling<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }

        public Option<IItem> Parent()
        {
            throw new NotImplementedException();
        }

        public Option<TItem> Parent<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }

        public Option<IItem> PreviousSibling()
        {
            throw new NotImplementedException();
        }

        public Option<TItem> PreviousSibling<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItem> Siblings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> Siblings<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItem> SiblingsAndSelf()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> SiblingsAndSelf<TItem>() where TItem : IItem
        {
            throw new NotImplementedException();
        }
    }
}
