using System.Net.Http;
using Tharga.Toolkit.Console.Commands.Base;

namespace Hubcap.TestClient
{
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

            var r = GameApi.Client.PutAsync($"api/game/move?GameKey={_game.GameKey}&PlayerKey={_game.PlayerKey}&xMove={x}&yMove={y}",
                    new ByteArrayContent(new byte[0])).GetAwaiter().GetResult();
            if (!r.IsSuccessStatusCode)
                OutputError(r.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
    }
}