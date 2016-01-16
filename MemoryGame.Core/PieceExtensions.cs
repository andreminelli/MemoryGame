using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Core
{
    public static class PieceExtensions
    {
        public static IEnumerable<Piece<T>> ToPieces<T>(this T[] values)
        {
            for(var i=0; i<values.Length; i++)
            {
                yield return new Piece<T>(values[i]);
            }
        }
    }
}
