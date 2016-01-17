using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace MemoryGame.Core.Tests
{
    [TestFixture]
    public class BoardFeatures
    {
        int[] pieces = { 1, 2, 3, 4, 5, 6, 7 };
        int numberOfPairs;

        Board<int> board;

        [SetUp]
        public void Setup()
        {
            numberOfPairs = pieces.Count();
            board = Board.From(pieces);
        }

        [Test]
        public void CreateBoard()
        {
            // Arrange

            // Act

            // Assert
            board.ShouldNotBe(null);
        }

        [Test]
        public void NumberOfPiecesIsDoubleNumberOfPairs()
        {
            // Arrange

            // Act

            // Assert
            board.Count().ShouldBe(2 * numberOfPairs);
            board.ShouldAllBe(p => p != null);
        }

        [Test]
        public void AllPiecesHavePairs()
        {
            // Arrange

            // Act
            var pieceGroups = board.GroupBy(p => p);

            // Assert
            pieceGroups.Count().ShouldBe(numberOfPairs);
            pieceGroups.ShouldAllBe(g => g.Count() == 2);
        }

        [Test]
        public void PiecesAreRandomlyDistributed()
        {
            // Arrange
            var piecePositions = new HashSet<int>();

            // Act
            for(var i=0; i < 400; i++)
            {
                Setup();
                var firstPiece = board.First();
                var matchPosition = board.ToList().FindIndex(1, p => p == firstPiece);
                piecePositions.Add(matchPosition);
            }

            // Assert
            piecePositions.Count().ShouldBeGreaterThanOrEqualTo(numberOfPairs-1);
        }
    }
}
