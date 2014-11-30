using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.DataAccess.Implementation;

namespace piHome.DataAccess.Interfaces
{
    public abstract class BaseRepository
    {
        protected readonly SqlLiteDb _db;

        #region C'stor

        protected BaseRepository(SqlLiteDb db)
        {
            _db = db;
        }
 
        #endregion

        public void Insert<T>(T item)
        {
            _db.Connection.Insert(item);
        }

        public void Update<T>(T item)
        {
            _db.Connection.Update(item);
        }
    }
}
