using System.Collections.Generic;
using piHome.Models.Dtos.UserSettings;
using piHome.Models.Entities.UserSettings;

namespace piHome.Logic.Interfaces
{
    public interface ICircuitsHandlingSetsProcessor
    {
        string CreateCircuitsHandlingSet(CircuitsHandlingSetEntity circuitsHandlingSetEntity);

        void UpdateCircuitsHandlingSet(string id, CircuitsHandlingSetEntity circuitsHandlingSetEntity);

        List<CircuitsHandlingSetDto> GetCircuitsHandlingSets(string userId, bool privateOnly);

        void DeleteCircuitsHandlingSet(string id);

        void ToggleCircuitsHandlingSet(string id, bool enableDisable);
    }
}
