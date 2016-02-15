using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Editing
{
    internal class BorrowingEditContext : IDisposable
    {
        private readonly Item item;

        private bool isContextOwner = false;

        public BorrowingEditContext(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            this.item = item;
            if (!item.Editing.IsEditing)
            {
                isContextOwner = true;
                item.Editing.BeginEdit();
            }
        }

        public void Dispose()
        {
            if (isContextOwner)
            {
                item.Editing.EndEdit();
            }
        }
    }
}
