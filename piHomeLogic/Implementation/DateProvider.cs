using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.Logic.Interfaces;

namespace piHome.Logic.Implementation
{
    public class DateProvider : IDateProvider
    {
        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
