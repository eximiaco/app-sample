using Microsoft.AspNetCore.Http;

namespace System
{
    internal static class HttpContextAccessorExtensions
    {
        internal static string GetAuthorizationToken(this IHttpContextAccessor httpContextAccessor)
        {
            var authorizationToken = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            return authorizationToken.Split(' ')[1];
        }
    }
}
