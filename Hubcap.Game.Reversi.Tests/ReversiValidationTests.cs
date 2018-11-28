using System;
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

    [TestFixture]
    public class ReversiTurnTests
    {
        [Test]
        public void Placing_X_on_24_on_initial_board_turns_discs()
        {
            //Arrange
            var board = Reversi.GetInitialState();
            var expected = Reversi.GetInitialState();
            expected[4, 2] = 'X';
            expected[4, 3] = 'X';

            //Act
            var newBoard = Reversi.Move(board, 2, 4, 'X');

            //Assert
            Assert.That(newBoard, Is.EqualTo(expected));
        }

        [Test]
        public void Turn_initial_board()
        {
            //Arrange
            var board = new[,]
            {
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', 'X', 'O', ' ', ' ', ' '},
                {' ', ' ', ' ', 'O', 'X', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}
            };
            var expected = new[,]
            {
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', 'X', 'O', ' ', ' ', ' '},
                {' ', ' ', 'X', 'X', 'X', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}
            };

            //Act
            var newBoard = Reversi.Move(board, 2, 4, 'X');

            //Assert
            Assert.That(newBoard, Is.EqualTo(expected));
        }

        [Test]
        public void Turn_matrix1()
        {
            //Arrange
            var board = new[,]
            {
                {' ', 'O', 'O', 'O', 'O', 'O', 'O', 'X'},
                {'O', 'O', ' ', ' ', ' ', ' ', ' ', ' '},
                {'O', ' ', 'O', ' ', ' ', ' ', ' ', ' '},
                {'O', ' ', ' ', 'O', ' ', ' ', ' ', ' '},
                {'O', ' ', ' ', ' ', 'O', ' ', ' ', ' '},
                {'O', ' ', ' ', ' ', ' ', 'O', ' ', ' '},
                {'O', ' ', ' ', ' ', ' ', ' ', 'O', ' '},
                {'X', ' ', ' ', ' ', ' ', ' ', ' ', 'X'}
            };
            var expected = new[,]
            {
                {'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X'},
                {'X', 'X', ' ', ' ', ' ', ' ', ' ', ' '},
                {'X', ' ', 'X', ' ', ' ', ' ', ' ', ' '},
                {'X', ' ', ' ', 'X', ' ', ' ', ' ', ' '},
                {'X', ' ', ' ', ' ', 'X', ' ', ' ', ' '},
                {'X', ' ', ' ', ' ', ' ', 'X', ' ', ' '},
                {'X', ' ', ' ', ' ', ' ', ' ', 'X', ' '},
                {'X', ' ', ' ', ' ', ' ', ' ', ' ', 'X'}
            };

            //Act
            var newBoard = Reversi.Move(board, 0, 0, 'X');

            //Assert
            Assert.That(newBoard, Is.EqualTo(expected));
        }

        [Test]
        public void Turn_matrix2()
        {
            //Arrange
            var board = new[,]
            {
                {'X', ' ', ' ', ' ', 'X', ' ', ' ', ' '},
                {' ', 'O', ' ', ' ', 'O', ' ', ' ', 'X'},
                {' ', ' ', 'O', ' ', 'O', ' ', 'O', ' '},
                {' ', ' ', ' ', 'O', 'O', 'O', ' ', ' '},
                {'X', 'O', 'O', 'O', ' ', 'O', 'O', 'X'},
                {' ', ' ', ' ', 'O', 'O', 'O', ' ', ' '},
                {' ', ' ', 'O', ' ', 'O', ' ', 'O', ' '},
                {' ', 'X', ' ', ' ', 'X', ' ', ' ', 'X'},
            };
            var expected = new[,]
            {
                {'X', ' ', ' ', ' ', 'X', ' ', ' ', ' '},
                {' ', 'X', ' ', ' ', 'X', ' ', ' ', 'X'},
                {' ', ' ', 'X', ' ', 'X', ' ', 'X', ' '},
                {' ', ' ', ' ', 'X', 'X', 'X', ' ', ' '},
                {'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X'},
                {' ', ' ', ' ', 'X', 'X', 'X', ' ', ' '},
                {' ', ' ', 'X', ' ', 'X', ' ', 'X', ' '},
                {' ', 'X', ' ', ' ', 'X', ' ', ' ', 'X'},
            };

            //Act
            var newBoard = Reversi.Move(board, 4, 4, 'X');

            //Assert
            Assert.That(newBoard, Is.EqualTo(expected));
        }

        [Test]
        public void Turn_matrix3()
        {
            //Arrange
            var board = new[,]
            {
                {'X', ' ', ' ', ' ', ' ', ' ', ' ', 'X'},
                {' ', 'O', ' ', ' ', ' ', ' ', ' ', 'O'},
                {' ', ' ', 'O', ' ', ' ', ' ', ' ', 'O'},
                {' ', ' ', ' ', 'O', ' ', ' ', ' ', 'O'},
                {' ', ' ', ' ', ' ', 'O', ' ', ' ', 'O'},
                {' ', ' ', ' ', ' ', ' ', 'O', ' ', 'O'},
                {' ', ' ', ' ', ' ', ' ', ' ', 'O', 'O'},
                {'X', 'O', 'O', 'O', 'O', 'O', 'O', ' '},
            };
            var expected = new[,]
            {
                {'X', ' ', ' ', ' ', ' ', ' ', ' ', 'X'},
                {' ', 'X', ' ', ' ', ' ', ' ', ' ', 'X'},
                {' ', ' ', 'X', ' ', ' ', ' ', ' ', 'X'},
                {' ', ' ', ' ', 'X', ' ', ' ', ' ', 'X'},
                {' ', ' ', ' ', ' ', 'X', ' ', ' ', 'X'},
                {' ', ' ', ' ', ' ', ' ', 'X', ' ', 'X'},
                {' ', ' ', ' ', ' ', ' ', ' ', 'X', 'X'},
                {'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X'},
            };

            //Act
            var newBoard = Reversi.Move(board, 7, 7, 'X');

            //Assert
            Assert.That(newBoard, Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class ReversiValidationTests
    {
        [Test]
        public void Placing_disc_on_occupied_space_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 3, 3, 'X');

            //Assert
            Assert.Throws<ReversiException>(newBoard);
        }

        [Test]
        public void Placing_X_on_24_on_initial_board_should_return_a_board()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            var newBoard = Reversi.Move(board, 2, 4, 'X');

            //Assert
        }

        [Test]
        public void Placing_X_on_23_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 2, 3, 'X');

            //Assert
            Assert.Throws<ReversiException>(newBoard);
        }

        [Test]
        public void Placing_X_on_00_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 0, 0, 'X');

            //Assert
            Assert.Throws<ReversiException>(newBoard);
        }

        [Test]
        public void Placing_X_on_77_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 7, 7, 'X');

            //Assert
            Assert.Throws<ReversiException>(newBoard);
        }

        [Test]
        public void Placing_X_on_53_on_initial_board_should_return_a_board()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            var newBoard = Reversi.Move(board, 5, 3, 'X');

            //Assert
        }

        [Test]
        public void Placing_X_on_54_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 5, 4, 'X');

            //Assert
            Assert.Throws<ReversiException>(newBoard);
        }

        [Test]
        public void Placing_X_on_42_on_initial_board_should_return_a_board()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            var newBoard = Reversi.Move(board, 4, 2, 'X');

            //Assert
        }

        [Test]
        public void Placing_X_on_35_on_initial_board_should_return_a_board()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            var newBoard = Reversi.Move(board, 3, 5, 'X');

            //Assert
        }

        [Test]
        public void Placing_X_on_32_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 3, 2, 'X');

            //Assert
            Assert.Throws<ReversiException>(newBoard);
        }

        [Test]
        public void Placing_X_on_45_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 4, 5, 'X');

            //Assert
            Assert.Throws<ReversiException>(newBoard);
        }

        [Test]
        public void Placing_X_on_25_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 2, 5, 'X');

            //Assert
            Assert.Throws<ReversiException>(newBoard);
        }

        [Test]
        public void Placing_X_on_25_on_valid_board()
        {
            //Arrange
            var board = Reversi.GetInitialState();
            board[2,5] = 'X';

            //Act
            void newBoard() => Reversi.Move(board, 2, 5, 'X');

            //Assert
            Assert.Throws<ReversiException>(newBoard);
        }
    }
}
