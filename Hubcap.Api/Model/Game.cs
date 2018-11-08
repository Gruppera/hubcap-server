using System;

namespace Hubcap.Api.Model
{
    public class Game
    {
        public int Turn { get; set; }
        public Guid Player1 { get; set; }
        public Guid Player2 { get; set; }
        public Guid NextPlayer => Turn % 2 == 0 ? Player2 : Player1;
        public object Board { get; set; }
    }
}