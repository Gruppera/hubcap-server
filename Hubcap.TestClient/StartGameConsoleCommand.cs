using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tharga.Toolkit.Console.Commands.Base;

namespace Hubcap.TestClient
{
    public class StartGameConsoleCommand : ActionCommandBase
    {
        private readonly Game _game;

        public StartGameConsoleCommand(Game game) : base("Start")
        {
            _game = game;
        }

        public override void Invoke(string[] param)
        {
            var player = QueryParam<string>("player name", param);

            var response = GameApi.Client.GetAsync($"api/game/iwannaplay?PlayerKey={player}").GetAwaiter().GetResult();
            var gameKey = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _game.GameKey = gameKey;
            _game.PlayerKey = player;
        }
    }

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

        class Response
        {
            public char[,] Board { get; set; }
        }
    }

    public class MoveCommand : ActionCommandBase
    {
        private readonly Game _game;

        public MoveCommand(Game game) : base("Move")
        {
            _game = game;
        }

        public override void Invoke(string[] param)
        {
            var x = QueryParam<int>("X", param);
            var y = QueryParam<int>("Y", param);

            GameApi.Client
                .PutAsync($"api/game/move?GameKey={_game.GameKey}&PlayerKey={_game.PlayerKey}&xMove={x}&yMove={y}",
                    new ByteArrayContent(new byte[0])).GetAwaiter().GetResult();
        }
    }
}