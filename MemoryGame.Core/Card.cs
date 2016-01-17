using System;
using System.Diagnostics;

namespace MemoryGame.Core
{
    [DebuggerDisplay("Piece {value}")]
    public class Card<T>
    {
        internal readonly T value;

        public Card(T value)
        {
            if (default(T).Equals(value))
                throw new ArgumentException();
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            var pieceObj = obj as Card<T>;
            if (pieceObj != null)
            {
                return value.Equals(pieceObj.value);
            }
            return value.Equals(obj);
        }

        public override int GetHashCode() => value.GetHashCode();
    }
}