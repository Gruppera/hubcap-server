using System;
using System.Collections.Generic;
using System.Linq;
using Hubcap.Game.Reversi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hubcap.Api.Controllers
{
    public class Game
    {
        public Guid Player1 { get; set; }
        public Guid Player2 { get; set; }
        public object Board { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static Dictionary<Guid, Game> _db = new Dictionary<Guid, Game>();

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
                var g = new Game { Board = new Reversi().GetInitialState(), Player1 = playerKey };
                _db.Add(gameKey, g);
                return gameKey;
            }

            var g2 = game.First();
            g2.Value.Player2 = playerKey;

            return g2.Key;
        }

        [HttpGet]
        [Route("get")]
        public ActionResult<string> GetNextMove(Guid gameKey)
        {
            _db.TryGetValue(gameKey, out var game);

            return JsonConvert.SerializeObject(game);
        }

        [HttpGet]
        public ActionResult<string> MakeMove(Guid gameKey, string moveData)
        {
            return "Move";
        }
    }
}
