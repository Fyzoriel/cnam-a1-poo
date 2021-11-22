using System;
using System.Linq;

namespace TP3.Extension
{
    public static class EnumExtension
    {
        public static System.Enum GetRandomEnumValue(this Type t)
        {
            return System.Enum.GetValues(t)
                .OfType<System.Enum>()               
                .OrderBy(e => Guid.NewGuid())
                .FirstOrDefault();           
        }
    }
}