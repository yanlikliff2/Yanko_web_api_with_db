using Microsoft.Extensions.Options;
using Yanko_web3_v2.Helpers;
using Yanko_web3_v2.Models;


namespace Yanko_web3_v2.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }
        public async Task Invoke(HttpContext context, PractDbContext dbContext, IJwtUtils jwtUtils) 
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId != null)
            {
                // Directly retrieve the user by ID without external method call
                var user = await dbContext.UserTables.FindAsync(accountId.Value);

                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }

            await _next(context);
        }
    }
}
