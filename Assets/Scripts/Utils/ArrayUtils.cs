using System.Collections.Generic;

#pragma warning disable 0649

namespace Utils
{
    public static class ArrayUtils
    {
   
        public static bool IsNullOrEmpty<T,V>(this IDictionary<T,V> dictionary)
        {
            return dictionary == null || dictionary.Count == 0;
        }
        
        public static bool IsNullOrEmpty<T>(this ICollection<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
