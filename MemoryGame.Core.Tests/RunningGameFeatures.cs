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
    public class RunningGameFeatures
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
        public void OnStartAllCardsAreTurnDown()
        {
            // Arrange

            // Act

            // Assert
            board.ShouldAllBe(c => c.Status == CardStatus.Down);
        }

        [Test]
        public void OnStartRemainingPairsShouldBeEqualTotalPairs()
        {
            // Arrange

            // Act

            // Assert
            board.RemainingPairs.ShouldBe(numberOfPairs);
        }

        [Test]
        public void Turn1CardUp()
        {
            // Arrange
            var cardPosition = new Random().Next(numberOfPairs);

            // Act
            board.TurnUp(cardPosition);

            // Assert
            board.ElementAt(cardPosition).Status.ShouldBe(CardStatus.Up);
            board.RemainingPairs.ShouldBe(numberOfPairs);
        }

        [Test]
        public void Turn2CardsNotMatchingShouldTurnCardsDown()
        {
            // Arrange
            var firstCardPosition = new Random().Next(numberOfPairs);
            board.TurnUp(firstCardPosition);
            var secondCardPosition = board.FindMatch(firstCardPosition);
            secondCardPosition++;
            if (secondCardPosition >= board.Count())
            {
                if (firstCardPosition == 0)
                    secondCardPosition = 1;
                else
                    secondCardPosition = 0;
            }

            // Act
            board.TurnUp(secondCardPosition);

            // Assert
            board.ElementAt(firstCardPosition).Status.ShouldBe(CardStatus.Down);
            board.ElementAt(secondCardPosition).Status.ShouldBe(CardStatus.Down);
            board.RemainingPairs.ShouldBe(numberOfPairs);
        }

        [Test]
        public void Turn2CardsMatchingShouldHaveAMatch()
        {
            // Arrange
            var firstCardPosition = new Random().Next(numberOfPairs);
            board.TurnUp(firstCardPosition);
            var secondCardPosition = board.FindMatch(firstCardPosition);

            // Act
            board.TurnUp(secondCardPosition);

            // Assert
            board.ElementAt(firstCardPosition).Status.ShouldBe(CardStatus.Matched);
            board.ElementAt(secondCardPosition).Status.ShouldBe(CardStatus.Matched);
            board.RemainingPairs.ShouldBe(numberOfPairs - 1);
        }
    }
}
