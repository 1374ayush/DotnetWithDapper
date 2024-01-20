using Dapper;
using Data.DbContext;
using Data.DTO;
using Data.Models;
using Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class GenericRepo<T> : IGenricRepo<T> where T : class
    {
        private readonly DapperDbContext _dbContext;

        //constructor
        public GenericRepo(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddUser(UserCreationDto data)
        {
            var query = "INSERT INTO NewUsers ( Name, Email, Password) VALUES (@Name, @Email, @Password)";

            try
            {
                using (var connection = _dbContext.CreateConnection())
                {
                    //data will consist of the variables of samename as kept in query, basically it is an object
                    await connection.ExecuteAsync(query, data);

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public async Task<T> DeleteUser(int Id)
        {
            var query = "DELETE FROM NewUsers WHERE Id = @Id";

            using (var connection = _dbContext.CreateConnection())
            {
                try
                {
                    T data = await GetById(Id);
                    if (data != null)
                    {
                        await connection.ExecuteAsync(query, new { Id });
                        return data;
                    }
                    else
                    {
                        throw new Exception("No data found");
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public async Task<T> GetById(int Id)
        {
            var query = "SELECT * from NewUsers where id = @Id";

            using (var connection = _dbContext.CreateConnection())
            {
                //getting the data of particular id
                var userData = await connection.QuerySingleOrDefaultAsync<T>(query, new { Id });

                return userData;
            }
        }


        public async Task<IEnumerable<T>> GetData()
        {
            var query = "SELECT * FROM NewUsers";
            using (var connection = _dbContext.CreateConnection())
            {
                var data = await connection.QueryAsync<User>(query);

                return (IEnumerable<T>)data.ToList();
            }
        }

        public async Task<bool> UpdateUser(int id, UserUpdateDto data)
        {
            var parameter = new DynamicParameters();
            parameter.Add("Id", id, DbType.Int32);
            parameter.Add("Name", data.Name, DbType.String);
            parameter.Add("Email", data.Email, DbType.String);
            parameter.Add("Password", data.Password, DbType.String);

            var query = "Update NewUsers SET Name = @Name, Email = @Email, Password = @Password WHERE Id = @Id";
            try
            {

                using (var connection = _dbContext.CreateConnection())
                {
                    T tempData = await GetById(id);
                    if (tempData != null)
                    {
                        await connection.ExecuteAsync(query, parameter);
                        return true;
                    }
                    else
                    {
                        throw new Exception("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }

}
