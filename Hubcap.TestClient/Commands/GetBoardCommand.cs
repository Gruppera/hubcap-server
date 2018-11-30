using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Tharga.Toolkit.Console.Commands.Base;

namespace Hubcap.TestClient
{
    public class GetBoardCommand : ActionCommandBase
    {
        private readonly Game _game;

        public GetBoardCommand(Game game) : base("GetBoard")
        {
            _game = game;
        }

        public override void Invoke(string[] param)
        {
            var response = GameApi.Client
                .GetAsync($"api/game/getboard?GameKey={_game.GameKey}&PlayerKey={_game.PlayerKey}").GetAwaiter()
                .GetResult();
            var game = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var gameObj = JsonConvert.DeserializeObject<Response>(game);

            if (gameObj.State == "Finished")
            {
                OutputInformation($"Game is finished (took {gameObj.Turn} turns)");
                OutputInformation($"PlayerOne score is {gameObj.PlayerOneScore}");
                OutputInformation($"PlayerTwo score is {gameObj.PlayerTwoScore}");
            }
            else
            {
                OutputInformation($"Turn: {gameObj.Turn}");
                OutputInformation($"Your token: {gameObj.YourToken}");
            }

            PrintBoard(gameObj.Board);
        }

        private void PrintBoard(char[,] board)
        {
            var dimSideIndex = Math.Sqrt(board.Length);

            var rows = new List<List<string>> { Line() };
            for (var x = 0; x < dimSideIndex; x++)
            {
                var cols = new List<string>();
                for (var y = 0; y < dimSideIndex; y++)
                {
                    cols.Add($"| {board[x, y]} |");
                }
                rows.Add(cols);
                rows.Add(Line());
            }

            OutputTable(rows);

            List<string> Line()
            {
                return new List<string> { " --- ", " --- ", " --- ", " --- ", " --- ", " --- ", " --- ", " --- " };
            }
        }

        public class Response
        {
            public int Turn { get; set; }
            public char YourToken { get; set; }
            public char[,] Board { get; set; }
            public string State { get; set; }
            public string PlayerOneScore { get; set; }
            public string PlayerTwoScore { get; set; }
            public List<Move> Moves { get; set; }

            public class Move
            {
                public char Disc { get; set; }
                public int X { get; set; }
                public int Y { get; set; }
            }
        }
    }
}