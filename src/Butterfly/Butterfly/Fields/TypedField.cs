using Butterfly.Rendering;
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
        private readonly Item ownerItem;
        private readonly string fieldName;

        protected Item OwnerItem => ownerItem;

        public Field InnerField => innerField.Value.ValueOrFailure($"Field '{fieldName}' not found in item with ID '{ownerItem.ID}'.");
        public ID Id => InnerField.ID;
        public IFieldRenderer Renderer => renderer;

        public TypedField(Item item, string fieldName)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (fieldName == null) throw new ArgumentNullException(nameof(fieldName));

            this.fieldName = fieldName;
            this.ownerItem = item;
            this.innerField = new Lazy<Option<Field>>(() => item.Fields[fieldName].SomeNotNull());
            this.renderer = InitializeFieldRenderer(item, fieldName);
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

        protected virtual IFieldRenderer InitializeFieldRenderer(Item item, string fieldName) => new FieldRenderer(item, fieldName);
    }
}
