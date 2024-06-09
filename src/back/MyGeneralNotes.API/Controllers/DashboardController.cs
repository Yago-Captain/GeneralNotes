using Microsoft.AspNetCore.Mvc;
using MyGeneralNotes.API.Attributes;
using MyGeneralNotes.Application.UseCases.Dashboard;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.API.Controllers;

[AuthenticatedUser]
public class DashboardController : MyGeneralNotesController
{
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDashboard), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Dashboard(
    [FromServices] IDashboardUseCase dashboard,
    [FromBody] RequestDashboard request)
    {
        var result = await dashboard.GetDashboard(request);
        if (result.Routines.Count != 0)
        {
            return Ok(result);
        }
        return NoContent();
    }
}
