using System;
using System.Linq;
using Hubcap.Api.Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hubcap.Api.Controllers
{
    [Route("api/help")]
    [ApiController]
    public class ReversiHelpController : ControllerBase
    {
        private readonly GameLogic _gameLogic;

        public ReversiHelpController(GameLogic gameLogic)
        {
            _gameLogic = gameLogic;
        }

        [HttpGet]
        [Route("possibleMoves")]
        public ActionResult<string> GetPossibleMoves(string disc, string gameBoard)
        {
            if (!char.TryParse(disc, out var discChar))
                return BadRequest("provided disc is invalid");

            char[,] board;
            try
            {
                board = JsonConvert.DeserializeObject<char[,]>(gameBoard);
            }
            catch (Exception)
            {
                return BadRequest("provided gameBoard is invalid");
            }
            
            var moves = _gameLogic.GetPossibleMoves(board, discChar);
            var responseData = moves.Select(x => new
            {
                horizontal = x.x,
                vertical = x.y
            }).ToArray();

            return Ok(JsonConvert.SerializeObject(responseData));
        }
    }
}