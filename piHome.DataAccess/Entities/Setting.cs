using piHome.Models.Circuit.Enums;

namespace piHome.DataAccess.Entities
{
    public class Setting : BaseEntity
    {
        public SettingKey Key { get; set; }
        public string Value { get; set; }
    }
}
