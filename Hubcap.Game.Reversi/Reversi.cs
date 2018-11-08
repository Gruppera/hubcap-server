using System;

namespace Hubcap.Game.Reversi
{
    public class Reversi
    {
        public static char[,] GetInitialState()
        {
            //var tmp = new char[8,8];
            var tmp = new char[,]
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

            return tmp;
        }

        public static char[,] Move(char[,] board, int x, int y, char disc)
        {
            if (board[x, y] != ' ') throw new InvalidOperationException();

            var otherDisk = 'X';
            if (disc == 'X')
                otherDisk = 'O';

            bool? ok = null;
            for (var horizontal = 0; horizontal < 2; horizontal++)
            {
                var direction = (horizontal == 0 ? 1 : -1);
                for (var i = x + direction; i < 8 && i > 0; i = i + direction)
                {
                    if (board[i, y] == otherDisk && (ok == null || ok == true))
                        ok = true;
                    if (board[i, y] == disc && ok == true)
                        break;
                    if (board[i, y] == disc && ok == null)
                    {
                        ok = false;
                        break;
                    }
                }
            }

            if (ok == null || ok == false)
                throw new InvalidOperationException();

            return board;
        }
    }
}