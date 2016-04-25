namespace piHome.Models.Entities.Auth
{
    public class UserEntity : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
