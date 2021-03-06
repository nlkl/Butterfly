﻿using Optional;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly
{
    public interface IItem
    {
        Item InnerItem { get; }

        ID Id { get; }
        ID TemplateId { get; }
        IEnumerable<ID> TemplateIds { get; }

        string Name { get; set; }
        string DisplayName { get; set; }

        string Path { get; }
        Option<string> GenerateUrl();

        IAxes Axes { get; }
        IDisposable Edit();
    }
}
