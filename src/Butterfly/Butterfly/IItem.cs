using Optional;
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

        string Name { get; }
        string DisplayName { get; }

        ItemUri Uri { get; }
        string DatabaseName { get; }
        string LanguageName { get; }
        string Path { get; }
        int Version { get; }
        bool IsLatestVersion { get; }

        string GenerateUrl();
        string GenerateUrl(UrlOptions options);

        Option<IItem> Parent();
        Option<TItem> Parent<TItem>() where TItem : IItem;
        Option<IItem> NextSibling();
        Option<TItem> NextSibling<TItem>() where TItem : IItem;
        Option<IItem> PreviousSibling();
        Option<TItem> PreviousSibling<TItem>() where TItem : IItem;
        IEnumerable<IItem> Children();
        IEnumerable<TItem> Children<TItem>() where TItem : IItem;
        IEnumerable<IItem> Ancestors();
        IEnumerable<TItem> Ancestors<TItem>() where TItem : IItem;
        IEnumerable<IItem> Descendants();
        IEnumerable<TItem> Descendants<TItem>() where TItem : IItem;
    }
}
