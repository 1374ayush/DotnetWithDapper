using Data.DbContext;
using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IGenricRepo<T> where T :class
    {
        Task<IEnumerable<T>> GetData();
        Task<T> GetById(int Id);
        Task<bool> AddUser(UserCreationDto data);
        Task<bool> UpdateUser(int id, UserUpdateDto data);
        Task<T> DeleteUser(int Id);
    }
}
