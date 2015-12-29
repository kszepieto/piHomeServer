using System;
using System.Linq;
using piHome.DataAccess.Entities;
using piHome.Models.Enums;
using piHome.Utils;
using SQLite;

namespace piHome.DataAccess.Implementation
{
    public class SqlLiteDb
    {
        private readonly SQLiteConnection _connection;

        #region C'stor

        public SqlLiteDb(string databaseName)
        {
            _connection = new SQLiteConnection(databaseName);
        }
 
        #endregion
        
        public SQLiteConnection Connection { get { return _connection; } }

        public void InitializeDB()
        {
            LogHelper.LogMessage("SqlLite InitializeDB");

            _connection.CreateTable<CircuitStateEntity>();
            _connection.CreateTable<CircuitHistoricalState>();

            foreach (var circuitName in Enum.GetNames(typeof(Circuit)))
            {
                var circuit = (Circuit)Enum.Parse(typeof(Circuit), circuitName);
                InitializeCircuit(_connection, circuit);
            }
        }

        private static void InitializeCircuit(SQLiteConnection connection, Circuit circuit)
        {
            var existingCircuit = connection.Query<CircuitStateEntity>("select * from CircuitStateEntity where Circuit = ?", circuit)
                                            .SingleOrDefault();
            if (existingCircuit == null)
            {
                connection.Insert(new CircuitStateEntity
                {
                    Circuit = circuit,
                    State = false,
                    Name = "Circuit " + circuit
                });
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}