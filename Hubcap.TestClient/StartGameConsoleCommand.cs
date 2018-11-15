using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
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

            var a = JsonConvert.DeserializeObject<dynamic>(game);
            var b = (string[,])a.Board;

            foreach (var s in b)
            {
                OutputInformation(string.Join("", s));
            }

            //OutputTable(new []{""}, b.SelectMany(x => x));

            OutputInformation(game);
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