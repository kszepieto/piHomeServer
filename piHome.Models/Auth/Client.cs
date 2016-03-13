using System;
using MongoDB.Bson;

namespace piHome.Models.Auth
{
    public class Client : BaseEntity
    {
        public string ClientId { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}