using Data.DbContext;
using Data.DTO;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IDbInstance
    {
        DapperDbContext GiveInstance();
        Task<IEnumerable<User>> GetData();
        Task<User> GetById(int Id);
        Task<bool> AddUser(UserCreationDto data);
        Task<bool> UpdateUser(int id, UserUpdateDto data);
        Task<User> DeleteUser(int Id);
    }
}