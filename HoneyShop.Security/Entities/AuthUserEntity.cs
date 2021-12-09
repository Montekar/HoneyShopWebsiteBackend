namespace HoneyShop.Security.Entities
{
    public class AuthUserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}