using System;
using System.Collections.Generic;
using System.Linq;

namespace Hubcap.Api.Model
{
    public class Game
    {
        public int Turn { get; set; } = new Random().Next(0, 2);
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public string NextPlayer => State != GameState.Finished ? (Turn % 2 == 0 ? PlayerTwo : PlayerOne) : null;
        public object Board { get; set; }
        public List<Move> Moves { get; } = new List<Move>();
        public GameState State { get; set; } = GameState.NotStarted;
        public int PlayerOneScore => CalcScore('X');
        public int PlayerTwoScore => CalcScore('O');

        private int CalcScore(char disc)
        {
            var score = 0;
            var b = (char[,])Board;

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (b[i, j] == disc)
                        score++;
                }
            }

            return score;
        }

        public class Move
        {
            public char Disc { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        public enum GameState
        {
            NotStarted,
            Ongoing,
            Finished
        }
    }

}