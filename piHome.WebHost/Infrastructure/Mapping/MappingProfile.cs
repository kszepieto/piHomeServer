using AutoMapper;
using piHome.Models.Entities.Auth;
using piHome.Models.Entities.Circuits;
using piHome.Models.ValueObjects;
using piHome.WebHost.WebModels.Auth;
using piHome.WebHost.WebModels.Circuits;

namespace piHome.WebHost.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<UserRegistrationVM, UserEntity>()
                .ForSourceMember(sm => sm.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dm => dm.Id, opt => opt.Ignore());

            CreateMap<StateChangeVM, StateChange>()
                .ForMember(dm => dm.State, sm => sm.MapFrom(src => src.NewState));

            CreateMap<CircuitStateEntity, CircuitStateVM>();
        }
    }
}
