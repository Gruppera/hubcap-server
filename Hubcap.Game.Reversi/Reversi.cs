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
            for (var horizontal = -1; horizontal < 2; horizontal++)
            {
                for(var vertical = -1; vertical < 2; vertical++)
                {
                    if (horizontal != 0 || vertical != 0)
                    {
                        var ix = x + horizontal;
                        var iy = y + vertical;
                        ok = null;
                        while (ix < 8 && ix >= 0 && iy < 8 && iy >= 0)
                        {
                            if (board[ix, iy] == otherDisk && (ok == null || ok == true))
                                ok = true;
                            if (board[ix, iy] == disc && ok == true)
                                break;
                            if (board[ix, iy] == disc && ok == null)
                            {
                                ok = false;
                                break;
                            }

                            ix = ix + horizontal;
                            iy = iy + vertical;
                        }

                        if (ok != null && ok == true)
                            return board;
                    }
                    //else if (horizontal != 0)
                    //{
                    //    //Check horizontal
                    //    for (var i = x + horizontal; i < 8 && i > 0; i = i + horizontal)
                    //    {
                    //        if (board[i, y] == otherDisk && (ok == null || ok == true))
                    //            ok = true;
                    //        if (board[i, y] == disc && ok == true)
                    //            break;
                    //        if (board[i, y] == disc && ok == null)
                    //        {
                    //            ok = false;
                    //            break;
                    //        }
                    //    }
                    //}
                    //else if (vertical != 0)
                    //{
                    //    //Check vertical
                    //    for (var i = y + vertical; i < 8 && i > 0; i = i + vertical)
                    //    {
                    //        if (board[x, i] == otherDisk && (ok == null || ok == true))
                    //            ok = true;
                    //        if (board[x, i] == disc && ok == true)
                    //            break;
                    //        if (board[x, i] == disc && ok == null)
                    //        {
                    //            ok = false;
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }

            if (ok == null || ok == false)
                throw new InvalidOperationException();

            return board;
        }
    }
}