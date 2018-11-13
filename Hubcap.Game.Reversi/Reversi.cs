using System;
using System.Collections.Generic;

namespace Hubcap.Game.Reversi
{
    public class ReversiException : Exception { }

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
            TurnDisks(board, x, y, disc);

            //TODO: Make move
            board[y, x] = disc;
            return board;
        }

        private static void TurnDisks(char[,] board, int x, int y, char disc)
        {
            if (board[y, x] != ' ') throw new ReversiException();

            var otherDisk = disc == 'X' ? 'O' : 'X';

            var ok = false;
            for (var horizontal = -1; horizontal < 2; horizontal++)
            {
                for (var vertical = -1; vertical < 2; vertical++)
                {
                    if (horizontal == 0 && vertical == 0) continue;

                    var ix = x + horizontal;
                    var iy = y + vertical;
                    string state = null;
                    var toTurn = new List<Tuple<int, int>>();
                    while (ix < 8 && ix >= 0 && iy < 8 && iy >= 0)
                    {
                        if (board[iy, ix] == otherDisk && (state == null || state == "opponent"))
                        {
                            toTurn.Add(new Tuple<int, int>(ix, iy));
                            state = "opponent";
                        }
                        else if (board[iy, ix] == disc && state == "opponent")
                        {
                            foreach (var tuple in toTurn)
                            {
                                board[tuple.Item2, tuple.Item1] = disc;
                            }
                            state = "close";
                        }
                        else if (board[iy, ix] == disc && state == null)
                        {
                            state = "fail";
                        }

                        if (state != "opponent")
                            break;

                        ix = ix + horizontal;
                        iy = iy + vertical;
                    }

                    if (state == "close")
                        ok = true;
                }
            }

            if (!ok)
                throw new ReversiException();
        }
    }
}