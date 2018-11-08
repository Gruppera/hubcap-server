using System;
using System.Collections.Generic;
using System.Linq;
using Hubcap.Game.Reversi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hubcap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static Dictionary<Guid, Model.Game> _db = new Dictionary<Guid, Model.Game>();

        [HttpGet]
        [Route("iwannaplay")]
        public ActionResult<Guid> IWantToPlay(Guid playerKey)
        {
            return GetGame(playerKey);
        }

        Guid GetGame(Guid playerKey)
        {
            var game = _db.Where(x => x.Value.Player2 == Guid.Empty).ToList();

            if (!game.Any())
            {
                var gameKey = Guid.NewGuid();
                var g = new Model.Game {Board = Reversi.GetInitialState(), Player1 = playerKey};
                _db.Add(gameKey, g);
                return gameKey;
            }

            var g2 = game.First();
            g2.Value.Player2 = playerKey;

            return g2.Key;
        }

        [HttpGet]
        [Route("get")]
        public ActionResult<string> GetNextMove(Guid gameKey, Guid playerKey)
        {
            while (true)
            {
                _db.TryGetValue(gameKey, out var game);
                if (game == null) return BadRequest("Invalid game key.");
                if (game.NextPlayer == playerKey)
                    return Ok(JsonConvert.SerializeObject(game));
                System.Threading.Thread.Sleep(100);
            }
        }

        [HttpGet]
        [Route("move")]
        public ActionResult MakeMove(Guid gameKey, Guid playerKey)
        {
            _db.TryGetValue(gameKey, out var game);
            if (game == null) return BadRequest("Invalid game key.");
            if (game.NextPlayer != playerKey) return BadRequest("Not your turn.");

            Reversi.Move((char[,])game.Board, 1,1, 'x');

            game.Turn++;
            return Ok();
        }
    }
}