using Microsoft.AspNetCore.Mvc;
using MyGeneralNotes.API.Attributes;
using MyGeneralNotes.Application.UseCases.Routines.Delete;
using MyGeneralNotes.Application.UseCases.Routines.GetById;
using MyGeneralNotes.Application.UseCases.Routines.Register;
using MyGeneralNotes.Application.UseCases.Routines.Update;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.API.Controllers;
[AuthenticatedUser]
public class RoutineController : MyGeneralNotesController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRoutine), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterRoutine(
    [FromServices] IRegisterRoutineUseCase useCase,
    [FromBody] RequestRoutine request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet("{routineId}")]
    [ProducesResponseType(typeof(ResponseRoutine), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoutine(
    [FromServices] IGetRoutineByIdUseCase useCase,
    [FromRoute] long routineId)
    {
        var response = await useCase.GetRoutineById(routineId);
        return Ok(response);
    }

    [HttpPut]
    [Route("{routineId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateRoutine(
    [FromServices] IUpdateRoutineUseCase useCase,
    [FromBody] RequestRoutine request,
    [FromRoute] long routineId)
    {
        await useCase.Update(routineId, request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{routineId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteRoutine(
    [FromServices] IDeleteRoutineUseCase useCase,
    [FromRoute] long routineId)
    {
        await useCase.Delete(routineId);
        return NoContent();
    }
}
