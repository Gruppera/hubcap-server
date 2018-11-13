using Hubcap.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hubcap.Api.Model
{
    public class HubcapApiAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var c = context.Controller as GameController;
            if (c is null) return;

            var gameKey = GetFromHeadersOrQueryString(context.HttpContext, "GameKey");
            var playerKey = GetFromHeadersOrQueryString(context.HttpContext, "PlayerKey");

            c.GameKey = gameKey;
            c.PlayerKey = playerKey;

            base.OnActionExecuting(context);
        }

        private string GetFromHeadersOrQueryString(HttpContext context, string key)
        {
            var hasHeader = context.Request.Headers.TryGetValue(key, out var headerValue);
            if (hasHeader) return headerValue;

            var hasQs = context.Request.Query.TryGetValue(key, out var qs);
            if (hasQs) return qs;

            return null;
        }
    }
}