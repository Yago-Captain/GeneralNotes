using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using MyGeneralNotes.Communication.Responses;
using MyGeneralNotes.Domain.Extensions;
using MyGeneralNotes.Domain.Repositories.User;
using MyGeneralNotes.Domain.Security.Tokens;
using MyGeneralNotes.Exceptions;
using MyGeneralNotes.Exceptions.ExceptionsBase;
namespace MyGeneralNotes.API.Filters;

public class AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository) : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator = accessTokenValidator;
    private readonly IUserReadOnlyRepository _repository = repository;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);
            var exist = await _repository.ExistActiveUserWitchIdentifier(userIdentifier);

            if (exist.IsFalse())
            {
                throw new MyGeneralNotesExceptions(MessagesException.USER_WITHOUT_PERMISSION);
            }
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError("TokenIsExpired")
            {
                TokenIsExpired = true,
            });
        }
        catch (MyGeneralNotesExceptions ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError(MessagesException.USER_WITHOUT_PERMISSION));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var auth = context.HttpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(auth))
        {
            throw new MyGeneralNotesExceptions(MessagesException.NO_TOKEN);
        }
        return auth["Bearer ".Length..].Trim();
    }
}
