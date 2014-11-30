using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.DataAccess.Entities;
using piHome.Models.Enums;
using piHome.WebApi.Models;

namespace piHome.DataAccess.Interfaces
{
    public interface ICircuitsRepository : IBaseRepository
    {
        bool GetCircuitState(Circuit circuit);
        List<CircuitStateEntity> GetCircuitStates();
        CircuitHistoricalState GetLastRowHistoricalState(Circuit circuit);
    }
}
