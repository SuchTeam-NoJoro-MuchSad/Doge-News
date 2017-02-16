using System;
using System.Collections.Generic;
using System.Linq;

namespace DogeNews.Common.Extension
{
    public static class AttributeExtensions
    {
        public static IList<Type> GetAttributeValues<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            
            return (IList<Type>)valueSelector(att);
        }
    }
}