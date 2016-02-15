using Optional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly
{
    public interface IAxes
    {
        Option<IItem> Parent();
        Option<TItem> Parent<TItem>() where TItem : IItem;

        Option<IItem> NextSibling();
        Option<TItem> NextSibling<TItem>() where TItem : IItem;

        Option<IItem> PreviousSibling();
        Option<TItem> PreviousSibling<TItem>() where TItem : IItem;

        IEnumerable<IItem> Siblings();
        IEnumerable<TItem> Siblings<TItem>() where TItem : IItem;

        IEnumerable<IItem> SiblingsAndSelf();
        IEnumerable<TItem> SiblingsAndSelf<TItem>() where TItem : IItem;

        IEnumerable<IItem> Children();
        IEnumerable<TItem> Children<TItem>() where TItem : IItem;

        IEnumerable<IItem> Ancestors();
        IEnumerable<TItem> Ancestors<TItem>() where TItem : IItem;

        IEnumerable<IItem> AncestorsAndSelf();
        IEnumerable<TItem> AncestorsAndSelf<TItem>() where TItem : IItem;

        IEnumerable<IItem> Descendants();
        IEnumerable<TItem> Descendants<TItem>() where TItem : IItem;

        IEnumerable<IItem> DescendantsAndSelf();
        IEnumerable<TItem> DescendantsAndSelf<TItem>() where TItem : IItem;
    }
}
