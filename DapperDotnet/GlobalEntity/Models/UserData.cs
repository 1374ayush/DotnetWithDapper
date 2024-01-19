
namespace GlobalEntity.Models
{
    //this is for authorization usinf jwt token
    public class UserData
    {
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHash { get; set; } 
    }

}
