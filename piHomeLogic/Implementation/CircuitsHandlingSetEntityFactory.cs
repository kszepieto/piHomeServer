using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.DataAccess.Interfaces;
using piHome.Logic.Shared.Interfaces;
using piHome.Models.Dtos.UserSettings;
using piHome.Models.Entities.UserSettings;

namespace piHome.Logic.Implementation
{
    public interface ICircuitsHandlingSetEntityFactory
    {
        CircuitsHandlingSetEntity CreateNew(CircuitsHandlingSetDto handlingSetDto);
        CircuitsHandlingSetEntity CreateExisting(CircuitsHandlingSetDto handlingSetDto);
    }

    //TODO
    public class CircuitsHandlingSetEntityFactory : ICircuitsHandlingSetEntityFactory
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserSettingsDalHelper _userSettingsDalHelper;

        public CircuitsHandlingSetEntityFactory(IUserProvider userProvider, IUserSettingsDalHelper userSettingsDalHelper)
        {
            _userProvider = userProvider;
            _userSettingsDalHelper = userSettingsDalHelper;
        }

        public CircuitsHandlingSetEntity CreateNew(CircuitsHandlingSetDto handlingSetDto)
        {
            var handlingSet = new CircuitsHandlingSetEntity();
            Map(handlingSetDto, handlingSet);
            handlingSet.Owner = _userProvider.GetCurrentUser();

            return handlingSet;
        }

        public CircuitsHandlingSetEntity CreateExisting(CircuitsHandlingSetDto handlingSetDto)
        {
            var handlingSet = _userSettingsDalHelper.GetById(handlingSetDto.Id);
            Map(handlingSetDto, handlingSet);

            return handlingSet;
        }

        private void Map(CircuitsHandlingSetDto source, CircuitsHandlingSetEntity destination)
        {
            destination.IsPrivate = source.IsPrivate;
            destination.IsEnabled = source.IsEnabled;
            destination.Name = source.Name;
            destination.StatesOnActivation = source.StatesOnActivation;
            destination.StatesOnDeactivation = source.StatesOnDeactivation;
        }
    }
}
