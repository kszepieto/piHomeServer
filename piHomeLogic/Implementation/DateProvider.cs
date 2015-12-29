using System;
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
