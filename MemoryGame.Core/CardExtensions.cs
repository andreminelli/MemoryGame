using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Core
{
    public static class CardExtensions
    {
        public static IEnumerable<Card<T>> ToPieces<T>(this T[] values)
        {
            for(var i=0; i<values.Length; i++)
            {
                yield return new Card<T>(values[i]);
            }
        }
    }
}
