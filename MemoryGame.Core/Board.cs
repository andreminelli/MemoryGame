using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.Core
{
    public class Board
    {
        public static Board<T> From<T>(T[] values)
        {
            return new Board<T>(values.ToPieces());
        }
    }

    public class Board<T> : IEnumerable<Piece<T>>
    {
        readonly int numberOfPairs;
        readonly Piece<T>[] places;

        public Board(IEnumerable<Piece<T>> pieces)
        {
            this.numberOfPairs = pieces.Count();
            this.places = new Piece<T>[numberOfPairs * 2];
            ShuffleInBoard(pieces);
        }

        public IEnumerator<Piece<T>> GetEnumerator() => this.places.Cast<Piece<T>>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ShuffleInBoard(IEnumerable<Piece<T>> pieces)
        {
            for (var i = 0; i < numberOfPairs; i++)
            {
                var piece = pieces.ElementAt(i);
                this.places[i] = piece;
                this.places[numberOfPairs + i] = piece;
            }

            var rnd = new Random((int)DateTime.Now.Ticks);
            var randomPlaces = this.places.OrderBy(p => rnd.Next()).ToArray();
            randomPlaces.CopyTo(this.places, 0);
        }


    }
}