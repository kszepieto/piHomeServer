using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using piHome.DataAccess.Entities;
using piHome.DataAccess.Implementation;
using piHome.Models.Enums;

namespace piHome.DataAccess.Test
{
    [TestClass]
    [DeploymentItem(@"piHome.DataAccess\sqlite3.dll")]
    public class CircuitsRepositoryTest
    {
        private static SqlLiteDb _db;

        [ClassInitialize]
        public static void DeleteDbFileBeforeStart(TestContext context)
        {
            _db = new SqlLiteDb("CircuitsRepositoryTest.db");
            _db.InitializeDB();
        }

        [TestMethod]
        public void check_if_GetCircuitState_executes_without_error()
        {
            var repository = new CircuitsRepository(_db);
            var result = repository.GetCircuitState(Circuit.C1);
        }

        [TestMethod]
        public void check_if_GetCircuitStates_executes_without_error()
        {
            var repository = new CircuitsRepository(_db);
            var result = repository.GetCircuitStates();
        }

        [TestMethod]
        public void check_if_GetLastRowHistoricalState_executes_without_error()
        {
            var repository = new CircuitsRepository(_db);
            var result = repository.GetLastRowHistoricalState(Circuit.C1);
        }

        [TestMethod]
        public void check_id_CircuitHistoricalState_insert_suceeds()
        {
            var historyRow = new CircuitHistoricalState {Circuit = Circuit.C1, TurnOnTime = DateTime.Now};
            var repository = new CircuitsRepository(_db);
            
            repository.Insert(historyRow);

            var result = repository.GetLastRowHistoricalState(historyRow.Circuit);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(historyRow.Circuit, result.Circuit);
            Assert.IsTrue(historyRow.TurnOnTime > DateTime.MinValue);
            Assert.IsTrue(result.Id > 0);
        }

        public void check_id_CircuitHistoricalState_update_suceeds()
        {
            var historyRow = new CircuitHistoricalState { Circuit = Circuit.C1, TurnOnTime = DateTime.Now };
            var repository = new CircuitsRepository(_db);

            repository.Insert(historyRow);

            var resultAfterInsert = repository.GetLastRowHistoricalState(historyRow.Circuit);
            
            resultAfterInsert.TurnedOnLength = 10;
            resultAfterInsert.Circuit = Circuit.C2;

            var resultAfterUpdate = repository.GetLastRowHistoricalState(historyRow.Circuit);

            Assert.IsNotNull(resultAfterUpdate);
            Assert.AreEqual(Circuit.C2, resultAfterUpdate.Circuit);
            Assert.AreEqual(10, resultAfterUpdate.TurnedOnLength);
            Assert.IsTrue(resultAfterUpdate.Id > 0);
            Assert.AreEqual(resultAfterInsert.Id, resultAfterUpdate.Id);
        }
    }
}
