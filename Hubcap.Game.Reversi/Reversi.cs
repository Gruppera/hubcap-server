using System;
using System.Collections.Generic;
using System.Linq;

namespace Hubcap.Game.Reversi
{
    public class ReversiException : Exception
    {
        public ReversiException(string message)
            : base(message)
        {
        }
    }

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
            var validMoves = GetMoves(board, disc);
            if (validMoves.Length == 0)
                throw new ReversiException("No moves are possible");
            if (!validMoves.Contains((x, y)))
                throw new ReversiException("Invalid move!");

            TurnDisks(board, x, y, disc);

            board[y, x] = disc;
            return board;
        }

        private static void TurnDisks(char[,] board, int x, int y, char disc)
        {
            if (board[y, x] != ' ') throw new ReversiException("Occupied space.");

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
                throw new ReversiException($"Invalid move {x}, {y}.");
        }

        public static (int x, int y)[] GetMoves(char[,] board, char disc)
        {
            var otherDisk = disc == 'X' ? 'O' : 'X';

            var validMoves = new HashSet<(int,int)>();

            for (var horizontal = -1; horizontal < 2; horizontal++)
            {
                for (var vertical = -1; vertical < 2; vertical++)
                {
                    if (horizontal == 0 && vertical == 0)
                        continue;

                    // Checking all the positions on the board
                    for (var x = 0; x < 8; x++)
                    {
                        for (var y = 0; y < 8; y++)
                        {
                            var line = new List<char>();
                            (int x, int y) temp = (x, y);

                            do
                            {
                                var c = board[temp.x, temp.y];
                                line.Add(c);
                                temp.x += horizontal;
                                temp.y += vertical;
                            }
                            while (InBounds(temp.x, temp.y));

                            if (line.Count < 3)
                                continue;

                            if (line[0] != ' ')
                                continue;
                            
                            if (line[1] != otherDisk)
                                continue;
                            
                            line.RemoveRange(0,2);
                            if (!line.Contains(disc))
                                continue;

                            // If we get here the x,y is valid to place disc on
                            validMoves.Add((y, x));
                        }
                    }
                }
            }

            return validMoves.ToArray();

            bool InBounds(int x, int y)
            {
                return x >= 0 && x < 8 && y >= 0 && y < 8;
            }
        }
    }
}