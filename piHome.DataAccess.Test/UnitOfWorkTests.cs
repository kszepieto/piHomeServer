using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using piHome.DataAccess.Entities;
using piHome.DataAccess.Implementation;
using piHome.Models.Enums;

namespace piHome.DataAccess.Test
{
    [TestClass]
    public class UnitOfWorkTests
    {
        public static string InitDbTestPath = "InitTest.db";

        [ClassInitialize]
        public static void DeleteDbFileBeforeStart(TestContext context)
        {
           if (File.Exists(InitDbTestPath))
            {
                File.Delete(InitDbTestPath);    
            }
        }

        [TestMethod]
        [DeploymentItem(@"piHome.DataAccess\sqlite3.dll")]
        public void CanInitialize()
        {
            var db = new SqlLiteDb(InitDbTestPath);
            db.InitializeDB();

            var circuits = db.Connection.Query<CircuitStateEntity>("select * from CircuitStateEntity");
            foreach (var circuitName in Enum.GetNames(typeof(Circuit)))
            {
                var circuit = (Circuit)Enum.Parse(typeof(Circuit), circuitName);
                Assert.IsNotNull(circuits.Single(c => c.Circuit == circuit));
            }

            var circuitsHistory = db.Connection.Query<CircuitHistoricalState>("select * from CircuitHistoricalState");
        }
    }
}
