using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Mapping
{
    public class TemplateMappingAttribute : Attribute
    {
        public Guid TemplateId { get; private set; } = Guid.Empty;

        public TemplateMappingAttribute(string templateId)
        {
            Guid id;
            if (Guid.TryParse(templateId, out id))
            {
                TemplateId = id;
            }
        }
    }
}
