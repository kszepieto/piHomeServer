﻿using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using piHome.DataAccess.Entities;
using piHome.DataAccess.Interfaces;
using piHome.Models.Enums;

namespace piHome.DataAccess.Implementation
{
    public class CircuitsRepository : BaseRepository, ICircuitsRepository
    {
        public bool GetCircuitState(Circuit circuit)
        {
            return _dbContext.CircuitsState.Find(c => c.Circuit == circuit).Single().State;
        }

        public List<CircuitStateEntity> GetCircuitStates()
        {
            return _dbContext.CircuitsState.Find(c => true).SortBy(c => c.Name).ToList();
        }

        public CircuitStateHistory GetLastRowHistoricalState(Circuit circuit)
        {
            var circuits = _dbContext.CircuitsStateHistory.Find(c => c.Circuit == circuit && c.TurnedOnLength == 0)
                .SortByDescending(c => c.TurnOnTime).Limit(1).ToList();

            return circuits.SingleOrDefault();
        }

        public void Insert(CircuitStateHistory circuitStateHistory)
        {
            _dbContext.CircuitsStateHistory.InsertOne(circuitStateHistory);
        }

        public void Update(CircuitStateHistory circuitStateHistory)
        {
            _dbContext.CircuitsStateHistory.FindOneAndReplace(c => c.Id == circuitStateHistory.Id, circuitStateHistory);
        }

        public CircuitsRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
