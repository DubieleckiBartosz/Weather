using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Weather.API.Helpers
{
    public class EnumHelpers
    {
        public static string GetEnumAttributeValueByString<T>(string value) where T : Enum
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name.ToLower() == value.ToLower())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }


        public static List<string> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetNames(typeof(T)).ToList();
        }
    }
}
