using Data.DbContext;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalEntity.Models;
using Data.DTO;
using System.Security.Cryptography;
using Dapper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Data.Repository
{
    public class authRepo : IauthRepo
    {
        private readonly DapperDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public authRepo(DapperDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<UserDataSec> register(UserDto user)
        {
            //CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            UserDataSec tempUser = new UserDataSec();

            tempUser.PasswordHash = user.Password;
            tempUser.Password = user.Password;
            tempUser.Email = user.Email;

            var query = "INSERT INTO Users (Email, Password, PasswordHash) VALUES (@Email, @Password, @PasswordHash)";
            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, tempUser);
            }

            return tempUser;
        }

        /*private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwprdSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwprdSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }*/

        public async Task<string> login(UserDto user)
        {
            using(var connection = _dbContext.CreateConnection())
            {
                var query = "Select * from Users where Email = @Email";

                UserDataSec userData =await connection.QuerySingleOrDefaultAsync<UserDataSec>(query, new {Email = user.Email});
                if (userData == null)
                {
                    return null;
                }

                
               // bool response = VerifyPassword(user.Password, userData.Password, userData.PasswordHash);
                if (user.Password == userData.Password) {
                    string token = CreateToken(user);
                    return token;
                }
                else return null;
            }
        }

        private string CreateToken(UserDto user) {
            List<Claim> claims = new List<Claim> {

                new Claim(ClaimTypes.Name, user.Email)
               };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSetting:Token").Value
                ));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims:claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials:cred
                   );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

      /*  public bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }*/

    }
}
