using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryGame.Core
{
    public class Board
    {
        public static Board<T> From<T>(T[] values)
        {
            return new Board<T>(values.ToPieces());
        }
    }

    public class Board<T> : IEnumerable<Card<T>>
    {
        public const int NONE = -1;

        readonly int numberOfPairs;
        readonly Card<T>[] places;

        int currentCardUp = NONE;

        public int RemainingPairs { get; private set; }

        public event EventHandler<TurnResultEventArgs> TurnEnded;

        public Board(IEnumerable<Card<T>> pieces)
        {
            this.numberOfPairs = pieces.Count();
            this.places = new Card<T>[numberOfPairs * 2];
            this.RemainingPairs = numberOfPairs;
            ShuffleInBoard(pieces);
        }
        
        public async Task TurnUp(int cardPosition)
        {
            places[cardPosition].TurnUp();
            if (currentCardUp == NONE)
            {
                await OnTurnEndedAsync(TurnResult.Pending);
                currentCardUp = cardPosition;
            }
            else
            {
                if (places[currentCardUp].IsMatch(places[cardPosition]))
                {
                    await OnTurnEndedAsync(TurnResult.Match);
                    places[currentCardUp].Match();
                    places[cardPosition].Match();
                    RemainingPairs--;
                }
                else
                {
                    await OnTurnEndedAsync(TurnResult.Mismatch);
                    places[currentCardUp].TurnDown();
                    places[cardPosition].TurnDown();
                }

                currentCardUp = NONE;
            }
        }
       
        public IEnumerator<Card<T>> GetEnumerator() => this.places.Cast<Card<T>>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ShuffleInBoard(IEnumerable<Card<T>> pieces)
        {
            for (var i = 0; i < numberOfPairs; i++)
            {
                var piece = pieces.ElementAt(i);
                this.places[i] = piece;
                this.places[numberOfPairs + i] = new Card<T>(piece.Value);
            }

            var rnd = new Random((int)DateTime.Now.Ticks);
            var randomPlaces = this.places.OrderBy(p => rnd.Next()).ToArray();
            randomPlaces.CopyTo(this.places, 0);
        }

        Task OnTurnEndedAsync(TurnResult result)
        {
            var args = new TurnResultEventArgs(result);
            TurnEnded?.Invoke(this, args);
            return args.WaitForDeferralsAsync();
        }

        internal int FindMatch(int cardPosition)
        {
            var cardToFind = places.ElementAt(cardPosition);
            for (var position = 0; position < places.Length; position++)
            {
                if (position != cardPosition && cardToFind.IsMatch(places[position]))
                {
                    return position;
                }
            }

            return NONE;
        }
    }
}