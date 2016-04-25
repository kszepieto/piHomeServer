using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using piHome.DataAccess.Interfaces;
using piHome.Models.Entities.Auth;
using piHome.WebHost.Infrastructure.Exceptions;
using piHome.WebHost.WebModels.Auth;

namespace piHome.WebHost.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAuthDalHelper _authDalHelper;
        private readonly IMapper _mapper;

        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserRegistrationVM userRegistrationVM)
        {
            var result = await _authDalHelper.RegisterUser(_mapper.Map<UserEntity>(userRegistrationVM));
            if (!result.Succeeded)
            {
                throw new InvalidInputException("User registration error", result.Errors);
            }
            
            return Ok();
        }

        #region C'tor

        public AccountController(IAuthDalHelper authDalHelper, IMapper mapper)
        {
            _authDalHelper = authDalHelper;
            _mapper = mapper;
        }

        #endregion
    }
}
