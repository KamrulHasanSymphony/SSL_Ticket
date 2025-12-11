
using SSL.CS.SSL.Common.Models;
using System.Security.Claims;


namespace SSL_ERP.Middleware
{
    public class DBMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public DBMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, DbConfig dbConfig)
        {
            // Get the authenticated user
            ClaimsPrincipal user = context.User;

            // Check if the user is authenticated
            if (user.Identity is { IsAuthenticated: true })
            {
                // Access user's claims
                var dbClaim = user.FindFirst(ClaimNames.Database);
                var sageDbClaim = user.FindFirst(ClaimNames.SageDatabase);
                string? dbName = dbClaim?.Value;
                string? sageDbName = sageDbClaim?.Value;

                dbConfig.DbName = dbName;

                dbConfig.SageDbName = sageDbName;
            }         


            await _next(context);
        }
    }

}
