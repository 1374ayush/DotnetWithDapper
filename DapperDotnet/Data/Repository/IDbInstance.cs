using Data.DbContext;
using Data.DTO;
using Data.Models;

namespace Data.Repository
{
    public interface IDbInstance
    {
        DapperDbContext GiveInstance();
        Task<IEnumerable<User>> GetData();
        Task<User> GetById(int Id);
        Task<Boolean> AddUser(UserCreationDto data);
        Task<Boolean> UpdateUser(int id, UserUpdateDto data);
        Task<User> DeleteUser(int Id);
    }
}