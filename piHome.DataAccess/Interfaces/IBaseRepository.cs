using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piHome.DataAccess.Interfaces
{
    public interface IBaseRepository
    {
        void Insert<T>(T item);
        void Update<T>(T item);
    }
}
