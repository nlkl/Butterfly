using Butterfly.Rendering;
using Butterfly.Utils;
using Optional;
using Optional.Unsafe;
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
    public class TypedField : IField
    {
        private readonly Lazy<Option<Field>> innerField;
        private readonly IFieldRenderer renderer;

        protected readonly ID ownerItemId;
        protected readonly string fieldName;

        protected Item OwnerItem => InnerField.Item;

        public Field InnerField => innerField.Value.ValueOrFailure($"Field '{fieldName}' not found in item with ID '{ownerItemId}'.");
        public IFieldRenderer Renderer => renderer;

        public TypedField(Item item, string fieldName)
        {
            Contracts.ArgNotNull(item, nameof(item));
            Contracts.ArgNotNull(fieldName, nameof(fieldName));

            this.fieldName = fieldName;
            this.ownerItemId = item.ID;
            this.innerField = new Lazy<Option<Field>>(() => item.Fields[fieldName].SomeNotNull());
            this.renderer = InitializeFieldRenderer();
        }

        public string RawValue
        {
            get { return InnerField.Value ?? string.Empty; }

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

        public virtual bool HasValue => !string.IsNullOrEmpty(RawValue);

        protected virtual IFieldRenderer InitializeFieldRenderer()
        {
            return new TypedFieldRenderer(this);
        }
    }
}
