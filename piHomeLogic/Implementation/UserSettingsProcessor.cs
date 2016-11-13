using System;
using System.Collections.Generic;
using System.Linq;
using piHome.DataAccess.Interfaces;
using piHome.Logic.Interfaces;
using piHome.Models.Dtos.UserSettings;
using piHome.Models.Entities.UserSettings;
using piHome.Utils.Exceptions;

namespace piHome.Logic.Implementation
{
    public class CircuitsHandlingSetsProcessor : ICircuitsHandlingSetsProcessor
    {
        private readonly IUserSettingsDalHelper _userSettingsDalHelper;
        private readonly ICircuitsHandlingSetEntityFactory _circuitsHandlingSetEntityFactory;

        public CircuitsHandlingSetsProcessor(IUserSettingsDalHelper userSettingsDalHelper, ICircuitsHandlingSetEntityFactory circuitsHandlingSetEntityFactory)
        {
            _userSettingsDalHelper = userSettingsDalHelper;
            _circuitsHandlingSetEntityFactory = circuitsHandlingSetEntityFactory;
        }

        public string CreateCircuitsHandlingSet(CircuitsHandlingSetDto circuitsHandlingSetEntity)
        {
            var entity = _circuitsHandlingSetEntityFactory.CreateNew(circuitsHandlingSetEntity);
            entity.GetValidationErrors().ThrowIfAny();
            
            return _userSettingsDalHelper.Create(entity);
        }

        public void UpdateCircuitsHandlingSet(CircuitsHandlingSetDto circuitsHandlingSetEntity)
        {
            var entity = _circuitsHandlingSetEntityFactory.CreateExisting(circuitsHandlingSetEntity);
            entity.GetValidationErrors().ThrowIfAny();

            _userSettingsDalHelper.Update(entity);
        }

        public List<CircuitsHandlingSetListItemDto> GetCircuitsHandlingSets(string userId, bool privateOnly)
        {
            return _userSettingsDalHelper.GetCircuitsHandlingSets(userId, privateOnly);
        }

        public void DeleteCircuitsHandlingSet(string id)
        {
            _userSettingsDalHelper.Delete(id);
        }

        public void ToggleCircuitsHandlingSet(string id, bool enableDisable)
        {
            _userSettingsDalHelper.ToggleCircuitsHandlingSet(id, enableDisable);
        }
    }
}