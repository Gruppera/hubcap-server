using System;

namespace Hubcap.Api.Model
{
    public class Game
    {
        public int Turn { get; set; } = new Random().Next(0, 2);
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public string NextPlayer => Turn % 2 == 0 ? PlayerTwo : PlayerOne;
        public object Board { get; set; }
    }
}