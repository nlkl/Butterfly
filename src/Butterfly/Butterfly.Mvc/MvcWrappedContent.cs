using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Butterfly.Mvc
{
    internal class MvcWrappedContent : IDisposable
    {
        private readonly ViewContext viewContext;
        private readonly string firstPart = string.Empty;
        private readonly string lastPart = string.Empty;

        public MvcWrappedContent(ViewContext viewContext, string firstPart, string lastPart)
        {
            if (viewContext == null) throw new ArgumentNullException(nameof(viewContext));
            if (firstPart == null) throw new ArgumentNullException(nameof(firstPart));
            if (lastPart == null) throw new ArgumentNullException(nameof(lastPart));

            this.viewContext = viewContext;
            this.firstPart = firstPart;
            this.lastPart = lastPart;

            viewContext.Writer.Write(firstPart);
        }

        public void Dispose()
        {
            viewContext.Writer.Write(lastPart);
        }
    }
}
