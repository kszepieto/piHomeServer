using AutoMapper;
using piHome.Models.Auth;
using piHome.Models.Circuit;
using piHome.WebHost.WebModels.Auth;
using piHome.WebHost.WebModels.Circuits;

namespace piHome.WebHost.Infrastructure.Mapping
{
    public class MappingsProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<UserRegistrationVM, User>()
                .ForSourceMember(sm => sm.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dm => dm.Id, opt => opt.Ignore());

            CreateMap<StateChangeVM, StateChange>()
                .ForMember(dm => dm.State, sm => sm.MapFrom(src => src.NewState));

            CreateMap<CircuitState, CircuitStateVM>();
        }
    }
}
