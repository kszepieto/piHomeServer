using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using piHome.DataAccess.Interfaces;
using piHome.Models.Dtos.UserSettings;
using piHome.Models.Entities.Circuits;
using piHome.Models.Entities.UserSettings;
using piHome.Utils.Exceptions;

namespace piHome.DataAccess.Implementation
{
    public class UserSettingsDalHelper : BaseDalHelper, IUserSettingsDalHelper
    {
        public string Create(CircuitsHandlingSetEntity circuitsHandlingSetEntity)
        {
            _dbContext.CircuitsHandlingSets.InsertOne(circuitsHandlingSetEntity);

            return circuitsHandlingSetEntity.Id;
        }

        public void Update(CircuitsHandlingSetEntity circuitsHandlingSetEntity)
        {
            _dbContext.CircuitsHandlingSets.FindOneAndReplace(x => x.Id == circuitsHandlingSetEntity.Id, circuitsHandlingSetEntity);
        }

        public CircuitsHandlingSetEntity GetById(string id)
        {
            var entity = _dbContext.CircuitsHandlingSets.Find(x => x.Id == id).SingleOrDefault();
            if (entity == null)
            {
                throw new EntityNotFoundException(id, entity.GetType());//TODO
            }

            return entity;
        }

        public List<CircuitsHandlingSetListItemDto> GetCircuitsHandlingSets(string userId, bool privateOnly)
        {
            var filter = Builders<CircuitsHandlingSetEntity>.Filter.Where(x => x.IsEnabled);
            if (privateOnly)
            {
                //filter = filter & Builders<CircuitsHandlingSetEntity>.Filter.Where(x => x.CreatedBy == userId);
            }
            else
            {
                filter = filter & Builders<CircuitsHandlingSetEntity>.Filter.Where(x => !x.IsPrivate);
            }

            return _dbContext.CircuitsHandlingSets.Find(filter).Project(x => new CircuitsHandlingSetListItemDto
            {
                Id = x.Id,
                IsPrivate = x.IsPrivate,
                Name = x.Name
            }).SortBy(x => x.Name).ToList();
        }

        public void Delete(string id)
        {
            _dbContext.CircuitsHandlingSets.DeleteOne(x => x.Id == id);
        }

        public void ToggleCircuitsHandlingSet(string id, bool enableDisable)
        {
            _dbContext.CircuitsHandlingSets.UpdateOne(x => x.Id == id, Builders<CircuitsHandlingSetEntity>.Update.Set(c => c.IsEnabled, enableDisable));
        }

        public UserSettingsDalHelper(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}