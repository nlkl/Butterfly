using Sitecore.Collections;
using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Rendering
{
    internal static class RenderFieldArgsExtensions
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
            var sourceDict = ObjectToHtmlAttributes(source);
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
            foreach (var sourceProperty in GetProperties(source))
            {
                var targetProperty = target.GetType().GetProperty(sourceProperty.Name);
                if (targetProperty != null && targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                {
                    var value = sourceProperty.GetValue(source, null);
                    targetProperty.SetValue(target, value, null);
                }
            }
        }

        private static IDictionary<string, string> ObjectToHtmlAttributes(object source)
        {
            Func<PropertyInfo, string> getName = p => p.Name.Replace('_', '-');
            Func<PropertyInfo, string> getValue = p => p.GetValue(source, null)?.ToString() ?? string.Empty;
            return GetProperties(source).ToDictionary(getName, getValue);
        }

        private static IEnumerable<PropertyInfo> GetProperties(object source)
        {
            if (source == null)
            {
                return Enumerable.Empty<PropertyInfo>();
            }

            return source
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetIndexParameters().Length == 0 && p.GetMethod != null)
                .ToArray();
        }
    }
}
