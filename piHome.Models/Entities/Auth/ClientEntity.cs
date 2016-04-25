namespace piHome.Models.Entities.Auth
{
    public class ClientEntity : BaseEntity
    {
        public string ClientId { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}