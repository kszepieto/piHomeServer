using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using piHome.DataAccess.Interfaces;
using piHome.Models.Auth;
using piHome.WebHost.Infrastructure.Exceptions;
using piHome.WebHost.WebModels.Auth;

namespace piHome.WebHost.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserRegistrationVM userRegistrationVM)
        {
            var result = await _authRepository.RegisterUser(_mapper.Map<User>(userRegistrationVM));
            if (!result.Succeeded)
            {
                throw new InvalidInputException("User registration error", result.Errors);
            }
            
            return Ok();
        }

        #region C'tor

        public AccountController(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        #endregion
    }
}
