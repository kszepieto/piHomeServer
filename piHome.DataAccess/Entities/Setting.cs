using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.Models.Enums;

namespace piHome.DataAccess.Entities
{
    public class Setting : BaseEntity
    {
        public SettingKey Key { get; set; }
        public string Value { get; set; }
    }
}
