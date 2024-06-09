using Microsoft.AspNetCore.Mvc;
using MyGeneralNotes.Application.UseCases.Login.DoLogin;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Response;
using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.API.Controllers;

public class LoginController : MyGeneralNotesController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase loginUseCase, [FromBody] RequestLogin request)
    {
        var response = await loginUseCase.Execute(request);

        return Ok(response);
    }
}
