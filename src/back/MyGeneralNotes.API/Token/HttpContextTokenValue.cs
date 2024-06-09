using MyGeneralNotes.Domain.Security.Tokens;

namespace MyGeneralNotes.API.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextTokenValue(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string Value()
    {
        var auth = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        return auth["Bearer ".Length..].Trim();
    }
}
