using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NanoStarCTF.Service.Helpers;

public static class HttpContextHelper
{
    public static IHttpContextAccessor Accessor { get; set; }
    public static HttpContext HttpContext => Accessor?.HttpContext;
    public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
    public static long? UserId => GetUserId();
    public static string UserRole => GetUserRoul();

    private static long? GetUserId()
    {
        string value = HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value;

        bool canParse = long.TryParse(value, out long id);
        return canParse ? id : null;
    }

    private static string GetUserRoul()
    {
        string value = HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value;
        return value;
    }


}
