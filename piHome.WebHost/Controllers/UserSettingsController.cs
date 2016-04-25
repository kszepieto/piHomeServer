using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using piHome.Logic.Interfaces;
using piHome.Models.Dtos.UserSettings;
using piHome.Models.Entities.UserSettings;
using piHome.WebHost.Infrastructure.Helpers;
using piHome.WebHost.Infrastructure.Mapping;
using piHome.WebHost.WebModels.UserSettings;

namespace piHome.WebHost.Controllers
{
    [RoutePrefix("api/Settings")]
    public class UserSettingsController : ApiController
    {
        private readonly ICircuitsHandlingSetsProcessor _circuitsHandlingSetsProcessor;
        private readonly IMapper _mapper;

        [Route("CircuitsHandlingSet/")]
        [HttpGet]
        public List<CircuitsHandlingSetListVM> GetCircuitsHandlingSets(bool privateOnly)
        {
            var userId = User.GetLoggedUserId();
            var definedSets = _circuitsHandlingSetsProcessor.GetCircuitsHandlingSets(userId, privateOnly);

            return _mapper.MapList<CircuitsHandlingSetDto, CircuitsHandlingSetListVM>(definedSets);
        }

        [Route("CircuitsHandlingSet/:id")]
        [HttpPost]
        public string CreateCircuitsHandlingSet(CircuitsHandlingSetVM circuitsHandlingSetVM)
        {
            var circuitsHandlingSet = _mapper.Map<CircuitsHandlingSetEntity>(circuitsHandlingSetVM);
            
            return _circuitsHandlingSetsProcessor.CreateCircuitsHandlingSet(circuitsHandlingSet);
        }

        [Route("CircuitsHandlingSet/:id")]
        [HttpPut]
        public void UpdateCircuitsHandlingSet(string id, CircuitsHandlingSetVM circuitsHandlingSetVM)
        {
            var circuitsHandlingSet = _mapper.Map<CircuitsHandlingSetEntity>(circuitsHandlingSetVM);

            _circuitsHandlingSetsProcessor.UpdateCircuitsHandlingSet(id, circuitsHandlingSet);
        }

        [Route("CircuitsHandlingSet/:id")]
        [HttpDelete]
        public void DeleteCircuitsHandlingSet(string id)
        {
            _circuitsHandlingSetsProcessor.DeleteCircuitsHandlingSet(id);
        }

        [Route("CircuitsHandlingSet/activate/:id")]
        [HttpPost]
        public void ActivateSet(string id)
        {
            _circuitsHandlingSetsProcessor.ToggleCircuitsHandlingSet(id, true);
        }

        [Route("CircuitsHandlingSet/deactivate/:id")]
        [HttpPost]
        public void DeactivateSet(string id)
        {
            _circuitsHandlingSetsProcessor.ToggleCircuitsHandlingSet(id, false);
        }

        public UserSettingsController(ICircuitsHandlingSetsProcessor circuitsHandlingSetsProcessor, IMapper mapper)
        {
            _circuitsHandlingSetsProcessor = circuitsHandlingSetsProcessor;
            _mapper = mapper;
        }
    }
}
