using Data.DTO;
using Data.Models;


namespace Data.Repository.Interfaces
{
    public interface IauthRepo
    {
        Task<UserDataSec> register(UserDto user);
        Task<string> login(UserDto user);
    }
}