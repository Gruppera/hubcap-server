using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

            var gameKey = string.IsNullOrEmpty(opponent) ?
                _gameLogic.CreateGameSession(PlayerKey) :
                _gameLogic.CreateGameSession(PlayerKey, opponent);

            if (_gameLogic.IsRandy(gameKey))
            {
                var session = _gameLogic.GetGameSession(gameKey);
                if (session.NextPlayer.StartsWith("randy_"))
                {
                    RandyMove(session);
                }
            }

            if (gameKey == null)
                return NotFound($"{opponent} isn't available to play against");

            return gameKey;
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
                {
                    var r = new
                    {
                        gameSession.Turn,
                        State = gameSession.State.ToString(),
                        Board = gameSession.Board,
                        YourToken = gameSession.PlayerOne == PlayerKey ? "X" : (gameSession.PlayerTwo == PlayerKey ? "O" : null)
                    };
                    return Ok(JsonConvert.SerializeObject(r));
                }
                if (gameSession.State == Model.Game.GameState.Finished)
                {
                    var r = new
                    {
                        gameSession.Turn,
                        State = gameSession.State.ToString(),
                        gameSession.Board,
                        gameSession.PlayerOneScore,
                        gameSession.PlayerTwoScore,
                        gameSession.Moves
                    };
                    return Ok(JsonConvert.SerializeObject(r));
                }

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

            Task.Run(() =>
            {
                if (_gameLogic.IsRandy(GameKey))
                {
                    System.Diagnostics.Debug.WriteLine("Randy's turn!");
                    var session = _gameLogic.GetGameSession(GameKey);
                    RandyMove(session);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Not Randy");
                }
            });

            return Ok();
        }

        private void RandyMove(Model.Game session)
        {
            var possibleMoves = _gameLogic.GetPossibleMoves(session.Board as char[,], 'O');

            if (possibleMoves.Length == 0)
            {
                System.Diagnostics.Debug.WriteLine($"No moves, skipping turn.");
                _gameLogic.UpdateGameState(session, -1, -1);
            }
            else
            {
                var r = new Random();
                var oneMove = possibleMoves[r.Next(possibleMoves.Length)];
                System.Diagnostics.Debug.WriteLine($"Making move {oneMove.x}, {oneMove.y}.");
                _gameLogic.UpdateGameState(session, oneMove.x, oneMove.y);
            }

        }
    }
}