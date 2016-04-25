using System.Collections.Generic;
using piHome.Logic.Interfaces;
using piHome.Models.Dtos.UserSettings;
using piHome.Models.Entities.UserSettings;

namespace piHome.Logic.Implementation
{
    public class CircuitsHandlingSetsProcessor : ICircuitsHandlingSetsProcessor
    {
        public string CreateCircuitsHandlingSet(CircuitsHandlingSetEntity circuitsHandlingSetEntity)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCircuitsHandlingSet(string id, CircuitsHandlingSetEntity circuitsHandlingSetEntity)
        {
            throw new System.NotImplementedException();
        }

        public List<CircuitsHandlingSetDto> GetCircuitsHandlingSets(string userId, bool privateOnly)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCircuitsHandlingSet(string id)
        {
            throw new System.NotImplementedException();
        }

        public void ToggleCircuitsHandlingSet(string id, bool enableDisable)
        {
            throw new System.NotImplementedException();
        }
    }
}