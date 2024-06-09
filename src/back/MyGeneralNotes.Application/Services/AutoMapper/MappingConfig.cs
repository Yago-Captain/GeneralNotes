using AutoMapper;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.Application.Services.AutoMapper;
public class MappingConfig : Profile
{
    public MappingConfig()
    {
        RequestToEntity();
        EntityToRequest();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisteredUser, Domain.Entities.User>()
            .ForMember(to => to.Password, option => option.Ignore());
        CreateMap<RequestRoutine, Domain.Entities.Routine>()
            .ForMember(dest => dest.Exercises, opt => opt.MapFrom(to => to.Exercises));
        CreateMap<RequestExercise, Domain.Entities.Exercise>();

    }

    private void EntityToRequest()
    {
        CreateMap<Domain.Entities.User, ResponseUserProfile>();
        CreateMap<Domain.Entities.Routine, ResponseRoutine>();
        CreateMap<Domain.Entities.Exercise, ResponseExercice>();
        CreateMap<Domain.Entities.Exercise, RequestExercise>();
        CreateMap<Domain.Entities.Routine, ResponseRoutinesDashboard>()
            .ForMember(dest => dest.ExerciseCount, config => config.MapFrom(to => to.Exercises.Count));
    }
}
