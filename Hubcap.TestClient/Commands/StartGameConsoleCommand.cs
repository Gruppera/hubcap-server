using System.Linq;
using System.Text;
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
            var opponent = QueryParam<string>("opponent (empty for random)", param);

            var url = $"api/game/iwannaplay?PlayerKey={player}";
            if (!string.IsNullOrEmpty(opponent))
                url = $"{url}&opponent={opponent}";

            var response = GameApi.Client.GetAsync(url).GetAwaiter().GetResult();
            var gameKey = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
                OutputWarning($"{response.StatusCode} : {gameKey}");

            _game.GameKey = gameKey;
            _game.PlayerKey = player;
        }
    }
}