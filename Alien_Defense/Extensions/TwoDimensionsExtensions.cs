using Alien_Defense.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Extensions
{
    /// <summary>
    /// класс расширения для получения IEnumerable
    /// </summary>
    public static class ArrayExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this ICell[,] target)
        {
            foreach (var item in target)
                yield return (T)item;
        }
        public static IEnumerable<T> ToEnumerable<T>(this HashSet<ITower> target)
        {
            foreach (var item in target)
                yield return (T)item;
        }
    }
}
