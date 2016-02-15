using Butterfly.Mapping;
using Optional.Unsafe;
using Sitecore.FakeDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var templateMapping = new AutoTemplateMapping(assembly);

            using (var db = new Db())
            {
                var pageTemplate = new DbTemplate("Page", new Sitecore.Data.ID("{B30304AF-6B39-4AB7-832E-C858A76B9C7B}"))
                {
                    "Title"
                };
                db.Add(pageTemplate);

                var articlePageTemplate = new DbTemplate("ArticlePage", new Sitecore.Data.ID("{13175CCF-54D3-43B3-B2D8-EF8863916B53}"))
                {
                    BaseIDs = new [] { pageTemplate.ID }
                };
                articlePageTemplate.Add("Content");
                db.Add(articlePageTemplate);

                var page1Item = new DbItem("Page1")
                {
                    TemplateID = pageTemplate.ID
                };
                page1Item.Add("Title", "Welcome");
                db.Add(page1Item);

                var articlePage1Item = new DbItem("ArticlePage1")
                {
                    TemplateID = articlePageTemplate.ID
                };
                articlePage1Item.Add("Title", "Welcome to article 1");
                articlePage1Item.Add("Content", "This is the content of article 1");
                db.Add(articlePage1Item);


                var page1 = templateMapping.ResolveAs<IPageItem>(db.GetItem(page1Item.ID)).ValueOrFailure();
                Console.WriteLine(page1.Title.RawValue);

                var article1 = templateMapping.ResolveAs<IArticlePageItem>(db.GetItem(articlePage1Item.ID)).ValueOrFailure();

                Console.WriteLine(article1.Title.RawValue);
                Console.WriteLine(article1.Content.RawValue);

                Console.ReadKey();
            }
        }
    }
}
