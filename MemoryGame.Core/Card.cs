using System;
using System.Diagnostics;

namespace MemoryGame.Core
{
    [DebuggerDisplay("Piece {value}")]
    public class Card<T>
    {
        public T Value
        {
            get;
        }

        public Card(T value)
        {
            if (default(T).Equals(value))
                throw new ArgumentException();
            Value = value;
        }

        public CardStatus Status { get; private set; }

        public override bool Equals(object obj)
        {
            var pieceObj = obj as Card<T>;
            if (pieceObj != null)
            {
                return Value.Equals(pieceObj.Value);
            }
            return Value.Equals(obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public void TurnUp()
        {
            Status = CardStatus.Up;
        }

        public void TurnDown()
        {
            Status = CardStatus.Down;
        }

        public void Match()
        {
            Status = CardStatus.Matched;
        }
    }
}