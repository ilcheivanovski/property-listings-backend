using System.IdentityModel.Tokens.Jwt;
using System.Xml.Linq;

namespace PropertyListings.Middleware
{
    public class AddTokenToResponseMiddleware : IMiddleware
    {
        private readonly ILogger<AddTokenToResponseMiddleware> _logger;

        public AddTokenToResponseMiddleware(ILogger<AddTokenToResponseMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string authorizationHeader = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {

                string[] headerParts = authorizationHeader.Split(' ');
                if (headerParts.Length == 2 && headerParts[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    string authToken = headerParts[1];

                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(authToken);

                    string name = jwtSecurityToken.Claims.First(claim => claim.Type == "name").Value;

                    context.Response.Headers.Add("name", name);

                }

            }


            // Continue processing the request
            await next(context);
        }
    }
}
