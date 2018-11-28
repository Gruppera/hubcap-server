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
}