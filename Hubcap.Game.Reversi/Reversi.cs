using System;

namespace Hubcap.Game.Reversi
{
    public static class Reversi
    {
        public static char[,] GetInitialState()
        {
            var tmp = new[,]
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

            var otherDisk = disc == 'X' ? 'O' : 'X';

            for (var horizontal = -1; horizontal < 2; horizontal++)
            {
                for(var vertical = -1; vertical < 2; vertical++)
                {
                    if (horizontal == 0 && vertical == 0) continue;

                    var ix = x + horizontal;
                    var iy = y + vertical;
                    string state = null;
                    while (ix < 8 && ix >= 0 && iy < 8 && iy >= 0)
                    {
                        if (board[ix, iy] == otherDisk && state == null)
                        {
                            state = "opponent";
                        }
                        else if (board[ix, iy] == disc && state == "opponent")
                        {
                            state = "close";
                        }
                        else if (board[ix, iy] == disc && state == null)
                        {
                            state = "fail";
                        }

                        if (state != "opponent")
                            break;

                        ix = ix + horizontal;
                        iy = iy + vertical;
                    }

                    if (state == "close")
                        return board;
                }
            }

            throw new InvalidOperationException();
        }
    }
}