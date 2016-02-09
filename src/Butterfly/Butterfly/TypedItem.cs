using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Butterfly
{
    public class TypedItem : IItem
    {
        private readonly Item innerItem;

        public ID Id => InnerItem.ID;
        public Item InnerItem => innerItem;

        public string Name => InnerItem.Name;
        public string DisplayName => InnerItem.DisplayName;

        public TypedItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            this.innerItem = item;
        }
    }
}
