using System.Linq;
using NUnit.Framework;

namespace Hubcap.Game.Reversi.Tests
{
    [TestFixture]
    public class ReversiCanMoveTests
    {
        [Test]
        public void Place_X_on_initial_board()
        {
            // Arrange
            var board = Reversi.GetInitialState();
            var expected = new (int, int)[]
            {
                (4, 2),
                (5, 3),
                (2, 4),
                (3, 5)
            };

            // Act
            var res = Reversi.GetMoves(board, 'X');

            // Assert
            Assert.True(ArraysContentEqual(expected, res), "Expected and Result are not equal");

        }

        [Test]
        public void Place_O_on_initial_board()
        {
            // Arrange
            var board = Reversi.GetInitialState();
            var expected = new (int, int)[]
            {
                (3,2),
                (2,3),
                (5,4),
                (4,5)
            };

            // Act
            var res = Reversi.GetMoves(board, 'O');

            // Assert
            Assert.True(ArraysContentEqual(expected, res), "Expected and Result are not equal");
        }

        [Test]
        public void ArrayContentEqualTests()
        {
            var a1 = new (int, int)[] { (1,2), (3,4) };
            var a2 = new (int, int)[] { (3,4), (1,2) };
            Assert.True(ArraysContentEqual(a1, a2));

            var b1 = new (int, int)[] { (1,2) };
            var b2 = new (int, int)[] { };
            Assert.False(ArraysContentEqual(b1, b2));

            var c1 = new (int, int)[] { (1,2) };
            var c2 = new (int, int)[] { (3,4) };
            Assert.False(ArraysContentEqual(c1, c2));
        }

        private bool ArraysContentEqual((int,int)[] one, (int,int)[] two)
        {
            if (one.Length != two.Length)
                return false;
            if (one.Any(i2 => two.Any(x => x.Item1 == i2.Item1 && x.Item2 == i2.Item2)))
                return true;

            return false;
        }
    }
}