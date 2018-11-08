using System;
using NUnit.Framework;

namespace Hubcap.Game.Reversi.Tests
{
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
            Assert.Throws<InvalidOperationException>(newBoard);
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
            Assert.Throws<InvalidOperationException>(newBoard);
        }

        [Test]
        public void Placing_X_on_00_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 0, 0, 'X');

            //Assert
            Assert.Throws<InvalidOperationException>(newBoard);
        }

        [Test]
        public void Placing_X_on_77_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 7, 7, 'X');

            //Assert
            Assert.Throws<InvalidOperationException>(newBoard);
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
            Assert.Throws<InvalidOperationException>(newBoard);
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
            Assert.Throws<InvalidOperationException>(newBoard);
        }

        [Test]
        public void Placing_X_on_45_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 4, 5, 'X');

            //Assert
            Assert.Throws<InvalidOperationException>(newBoard);
        }

        [Test]
        public void Placing_X_on_25_on_initial_board_should_throw()
        {
            //Arrange
            var board = Reversi.GetInitialState();

            //Act
            void newBoard() => Reversi.Move(board, 2, 5, 'X');

            //Assert
            Assert.Throws<InvalidOperationException>(newBoard);
        }

        [Test]
        public void Placing_X_on_25_on_valid_board()
        {
            //Arrange
            var board = Reversi.GetInitialState();
            board[2,5] = 'X';

            //Act
            Reversi.Move(board, 2, 5, 'X');

            //Assert
        }
    }
}
