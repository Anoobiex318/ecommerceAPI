using Dapper;
using Ecommerce.Domain.Entities;
using MySqlConnector;

namespace EcommerceAPI.Middleware
{
    public class UserIdAuth
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public UserIdAuth(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.Value.Contains("User", StringComparison.OrdinalIgnoreCase)
                || !context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                var userId = context.Request.Headers["x-user-id"].ToString();
                if (string.IsNullOrEmpty(userId))
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync("User id is required");
                    return;
                }
            }

            await _next(context);
        }
   

    private bool IsValidUserId(Guid userId)
        {
            var connectionString = _configuration.GetConnectionString("MySQLConnection");
            var query = "Select * From Users Where UserId = @userId";
            using var connection = new MySqlConnection(connectionString);
            var user = connection.QueryFirstOrDefaultAsync<UserEntity>(query, new { userId }).Result;
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool StringCompare(HttpContext context, string route)
        {
            var stringComparison = context.Request.Path.Equals(route, StringComparison.OrdinalIgnoreCase);
            return stringComparison;
        }
    }

    public static class UserAuthExtensions
    {
        public static IApplicationBuilder UseUserAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserIdAuth>();
        }
    }
}
