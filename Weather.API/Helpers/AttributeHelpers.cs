using System;
using System.Linq;

namespace Weather.API.Helpers
{
    public static class AttributeHelpers
    {
        public static T2 GetAttributeValue<T1, T2>(this Type type, Func<T1, T2> valueSelector) where T1 : Attribute
        {
            var att = type.GetCustomAttributes(typeof(T1), true).FirstOrDefault() as T1;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(T2);
        }

        public static T2 GetAttributeByMethod<T1, T2>(this Type type, string methodName)
        {
            return ((T2)typeof(T1).GetMethod(methodName)?.GetCustomAttributes(typeof(T2), false)
                .FirstOrDefault());
        }
    }
}
