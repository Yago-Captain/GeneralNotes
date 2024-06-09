using MyGeneralNotes.Domain.Extensions;
using System.Globalization;

namespace MyGeneralNotes.API.Middleware;

public class CultureMiddleware(RequestDelegate nextRequest)
{
    private readonly RequestDelegate _nextRequest = nextRequest;

    public async Task Invoke(HttpContext context)
    {
        var supportedLeng = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if (requestedCulture.NotEmpty()
            && supportedLeng.Exists(c => c.Name.Equals(requestedCulture)))
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _nextRequest(context);
    }
}
