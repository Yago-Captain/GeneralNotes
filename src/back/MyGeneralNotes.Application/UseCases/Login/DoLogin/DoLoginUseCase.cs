using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Response;
using MyGeneralNotes.Communication.Responses;
using MyGeneralNotes.Domain.Repositories.User;
using MyGeneralNotes.Domain.Security.Cryptography;
using MyGeneralNotes.Domain.Security.Tokens;
using MyGeneralNotes.Exceptions.ExceptionsBase;

namespace MyGeneralNotes.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accesTokenGenerator) : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository = repository;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IAccessTokenGenerator _accesTokenGenerator = accesTokenGenerator;

    public async Task<ResponseRegisteredUser> Execute(RequestLogin request)
    {
        var encriptedPassword = _passwordEncripter.Encrypt(request.Password);
        var user = await _repository.GetUserByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();
        return new ResponseRegisteredUser
        {
            Name = user.Name,
            Tokens = new ResponseTokens
            {
                AccessToken = _accesTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}
