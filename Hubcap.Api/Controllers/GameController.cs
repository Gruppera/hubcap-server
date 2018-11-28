using System.Collections.Generic;
using System.Threading;
using Hubcap.Api.Logic;
using Hubcap.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hubcap.Api.Controllers
{
    [Route("api/game")]
    [ApiController]
    [HubcapApi]
    public class GameController : ControllerBase
    {
        private readonly GameLogic _gameLogic;

        public GameController(GameLogic gameLogic)
        {
            _gameLogic = gameLogic;
        }

        public string PlayerKey { get; set; }
        public string GameKey { get; set; }

        [HttpGet]
        [Route("iwannaplay")]
        public ActionResult<string> IWantToPlay(string opponent)
        {
            if (string.IsNullOrEmpty(PlayerKey))
                return BadRequest("PlayerKey not set");

            var game = string.IsNullOrEmpty(opponent) ?
                _gameLogic.CreateGameSession(PlayerKey) :
                _gameLogic.CreateGameSession(PlayerKey, opponent);

            if (game == null)
                return NotFound($"{opponent} isn't available to play against");

            return game;
        }

        [HttpGet]
        [Route("getboard")]
        public ActionResult<string> GetNextMove()
        {
            if (string.IsNullOrEmpty(PlayerKey))
                return BadRequest("PlayerKey not set");
            if (string.IsNullOrEmpty(GameKey))
                return BadRequest("GameKey not set");

            while (true)
            {
                var gameSession = _gameLogic.GetGameSession(GameKey);
                if (gameSession == null) return BadRequest("Invalid game key.");

                if (gameSession.NextPlayer == PlayerKey)
                    return Ok(JsonConvert.SerializeObject(gameSession));

                Thread.Sleep(100);
            }
        }

        [HttpPut]
        [Route("move")]
        public ActionResult MakeMove(int xMove, int yMove)
        {
            if (string.IsNullOrEmpty(PlayerKey))
                return BadRequest("PlayerKey not set");
            if (string.IsNullOrEmpty(GameKey))
                return BadRequest("GameKey not set");

            var gameSession = _gameLogic.GetGameSession(GameKey);
            if (gameSession == null)
                return BadRequest("Invalid game key.");
            if (gameSession.NextPlayer != PlayerKey)
                return BadRequest("Not your turn.");

            var potentialError = _gameLogic.UpdateGameState(gameSession, xMove, yMove);
            if (!string.IsNullOrEmpty(potentialError))
                return BadRequest(potentialError);

            return Ok();
        }
    }
}