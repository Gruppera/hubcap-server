using System;
using Microsoft.AspNetCore.Mvc;

namespace Hubcap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Guid> IWantToPlay()
        {
            return Guid.NewGuid();
        }

        [HttpGet]
        public ActionResult<string> GetNextMove(Guid gameKey)
        {
            return "value";
        }

        [HttpGet]
        public ActionResult<string> MakeMove(Guid gameKey, string moveData)
        {
            return "Move";
        }
    }
}