using Butterfly.Utils;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Butterfly.Fields
{
    public abstract class TypedField : ITypedField
    {
        private readonly Lazy<Field> innerField;

        protected readonly ID ownerItemId;
        protected readonly string fieldName;

        protected Item OwnerItem => InnerField.Item;

        public Field InnerField => Contracts.EnsureNotNull(innerField.Value, $"Field '{fieldName}' not found in item with ID '{ownerItemId}'.");

        public TypedField(Item item, string fieldName)
        {
            Contracts.ArgNotNull(item, nameof(item));
            Contracts.ArgNotNull(fieldName, nameof(fieldName));

            this.fieldName = fieldName;
            this.ownerItemId = item.ID;
            this.innerField = new Lazy<Field>(() => item.Fields[fieldName]);
        }

        public string RawValue
        {
            get
            {
                return InnerField.Value ?? string.Empty;
            }

            set
            {
                using (new EditContext(OwnerItem))
                {
                    InnerField.Value = value;
                }
            }
        }

        public void Load()
        {
            var value = innerField.Value;
        }
    }
}
