using Butterfly.Fields;
using Butterfly.Mapping;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Sandbox
{
    public interface IPageItem : IItem
    {
        IField Title { get; }
    }

    [TemplateMapping("{B30304AF-6B39-4AB7-832E-C858A76B9C7B}")]
    public class PageItem : TypedItem, IPageItem
    {
        public IField Title { get; private set; }

        public PageItem(Item item) : base(item)
        {
            Title = new TypedField(item, "Title");
        }
    }

    public interface IArticlePageItem : IPageItem
    {
        IField Content { get; }
    }

    [TemplateMapping("{13175CCF-54D3-43B3-B2D8-EF8863916B53}")]
    public class ArticlePageItem : TypedItem, IArticlePageItem
    {
        public IField Title { get; private set; }
        public IField Content { get; private set; }

        public ArticlePageItem(Item item) : base(item)
        {
            Title = new TypedField(item, "Title");
            Content = new TypedField(item, "Content");
        }
    }
}
