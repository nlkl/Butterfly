using Sitecore.Collections;
using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Butterfly.Mvc
{
    internal static class MvcRenderFieldArgsExtensions
    {
        public static void ApplyParameters(this RenderFieldArgs args, object parameters)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            CopyProperties(parameters, args);
            CopyProperties(parameters, args.Parameters);
        }

        private static void CopyProperties(object source, SafeDictionary<string, string> target)
        {
            var sourceDict = HtmlHelper.AnonymousObjectToHtmlAttributes(source);
            foreach (var property in sourceDict)
            {
                var value = property.Value;
                if (value != null)
                {
                    target[property.Key] = value.ToString();
                }
            }
        }

        private static void CopyProperties(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            foreach (var sourceProperty in sourceType.GetProperties())
            {
                var targetProperty = targetType.GetProperty(sourceProperty.Name);
                if (targetProperty != null && targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                {
                    var value = sourceProperty.GetValue(source, null);
                    targetProperty.SetValue(target, value, null);
                }
            }
        }
    }
}
