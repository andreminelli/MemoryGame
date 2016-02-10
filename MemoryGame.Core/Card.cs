using System;
using System.Diagnostics;

namespace MemoryGame.Core
{
    [DebuggerDisplay("Piece {Value}")]
    public class Card<T>
    {
        public T Value
        {
            get;
        }

        public event EventHandler StatusChanged;

        public Card(T value)
        {
            if (value.Equals(default(T)))
                throw new ArgumentException();
            Value = value;
        }

        public CardStatus Status { get; private set; }

        public bool IsMatch(Card<T> other)
        {
            return this.Equals(other);
        }

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

        internal void TurnUp()
        {
            if (Status == CardStatus.Down)
            {
                SetStatus(CardStatus.Up);
            }
        }

        internal void TurnDown()
        {
            if (Status == CardStatus.Up)
            {
                SetStatus(CardStatus.Down);
            }
        }

        internal void Match()
        {
            if (Status == CardStatus.Up)
            {
                SetStatus(CardStatus.Matched);
            }
        }

        void SetStatus(CardStatus newStatus)
        {
            Status = newStatus;
            OnStatusChanged();
        }

        void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, new EventArgs());
        }
    }
}