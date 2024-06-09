using Microsoft.AspNetCore.Mvc;
using MyGeneralNotes.API.Attributes;
using MyGeneralNotes.Application.UseCases.User.ChangePassword;
using MyGeneralNotes.Application.UseCases.User.Profile;
using MyGeneralNotes.Application.UseCases.User.Register;
using MyGeneralNotes.Application.UseCases.User.Update;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Response;
using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.API.Controllers;


public class UserController : MyGeneralNotesController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase registerUserUse,
        [FromBody] RequestRegisteredUser request)
    {
        var result = await registerUserUse.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserProfile), StatusCodes.Status200OK)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase userProfile)
    {
        var result = await userProfile.Execute();

        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateUserUseCase useCase,
        [FromBody] RequestUpdateUser request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePassword request)
    {
        await useCase.Execute(request);

        return NoContent();
    }
}