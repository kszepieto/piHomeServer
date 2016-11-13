using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.Models.Dtos.UserSettings;
using piHome.Models.Entities.UserSettings;

namespace piHome.DataAccess.Interfaces
{
    public interface IUserSettingsDalHelper
    {
        string Create(CircuitsHandlingSetEntity circuitsHandlingSetEntity);
        void Update(CircuitsHandlingSetEntity circuitsHandlingSetEntity);
        CircuitsHandlingSetEntity GetById(string id);
        List<CircuitsHandlingSetListItemDto> GetCircuitsHandlingSets(string userId, bool privateOnly);
        void Delete(string id);
        void ToggleCircuitsHandlingSet(string id, bool enableDisable);
    }
}
