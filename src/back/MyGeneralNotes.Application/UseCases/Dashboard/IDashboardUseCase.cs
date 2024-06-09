using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.Application.UseCases.Dashboard;
public interface IDashboardUseCase
{
    Task<ResponseDashboard> GetDashboard(RequestDashboard request);
}
