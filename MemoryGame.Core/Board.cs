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
        readonly Piece<T>[] pieces;

        public Board(IEnumerable<Piece<T>> pieces)
        {
            this.numberOfPairs = pieces.Count();
            this.pieces = new Piece<T>[numberOfPairs * 2];
            for(var i=0; i<numberOfPairs; i++)
            {
                var piece = pieces.ElementAt(i);
                this.pieces[i] = piece;
                this.pieces[numberOfPairs + i] = piece;
            }
        }

        public IEnumerator<Piece<T>> GetEnumerator() => this.pieces.Cast<Piece<T>>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}