using Data.DTO;
using GlobalEntity.Models;

namespace Data.Repository
{
    public interface IauthRepo
    {
        Task<UserDataSec> register(UserDto user);
        Task<string> login(UserDto user);
    }
}