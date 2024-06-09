using AutoMapper;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Response;
using MyGeneralNotes.Communication.Responses;
using MyGeneralNotes.Domain.Repositories;
using MyGeneralNotes.Domain.Repositories.User;
using MyGeneralNotes.Domain.Security.Cryptography;
using MyGeneralNotes.Domain.Security.Tokens;
using MyGeneralNotes.Exceptions;
using MyGeneralNotes.Exceptions.ExceptionsBase;

namespace MyGeneralNotes.Application.UseCases.User.Register;
public class RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, IMapper mapper, IPasswordEncripter passwordEncripter, IUnitOfWork unitOfWork, IUserReadOnlyRepository userReadOnlyRepository, IAccessTokenGenerator accesTokenGenerator) : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository = userWriteOnlyRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accesTokenGenerator = accesTokenGenerator;

    public async Task<ResponseRegisteredUser> Execute(RequestRegisteredUser request)
    {

        await ValidateRequest(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUser
        {
            Name = request.Name,
            Tokens = new ResponseTokens
            {
                AccessToken = _accesTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task ValidateRequest(RequestRegisteredUser request)
    {
        var userValidator = new RegisterUserValidator();

        var result = userValidator.Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, MessagesException.EMAIL_ALREADY_REGISTARED));

        if (result.IsValid == false)
        {
            var errorMenssages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMenssages);
        }
    }
}
