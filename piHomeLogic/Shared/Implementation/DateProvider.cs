using System;
using piHome.Logic.Shared.Interfaces;

namespace piHome.Logic.Shared.Implementation
{
    public class DateProvider : IDateProvider
    {
        public DateTime GetUtcDateTimeDate()
        {
            return DateTime.UtcNow;
        }
    }
}
