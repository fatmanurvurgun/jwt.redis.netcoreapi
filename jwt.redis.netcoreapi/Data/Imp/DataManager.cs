using jwt.redis.netcoreapi.Domain;
using jwt.redis.netcoreapi.Models.Request;
using jwt.redis.netcoreapi.Models.Response;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Data.Imp
{
    public class DataManager : IDataManager
    {
        private readonly AppSettings _appSettings;
        private ICacheManager _cacheManager;

        public DataManager(IOptions<AppSettings> appSettings,
            ICacheManager cacheManager)
        {
            _appSettings = appSettings.Value;
            _cacheManager = cacheManager;
        }
        public LoginResponseModel Login(LoginRequestModel loginRequestModel)
        {
            if (loginRequestModel == null)
                throw new ArgumentNullException(nameof(loginRequestModel));

            var user = LoadUsers().Where(x => x.Email == loginRequestModel.Email && x.Password == loginRequestModel.Password).FirstOrDefault();

            if (user != null)
            {
                LoginResponseModel loginResponseModel = new LoginResponseModel();
                loginResponseModel.Token = SetToken(user.Id);
                loginRequestModel.Email = user.Email;
                return loginResponseModel;
            }

            return null;
        }


        public List<User> LoadUsers()
        {
            var users = new List<User>();
            users.Add(new User() { Id = 1, Name = "Ali", Surname = "Yılmaz", Password = "123456", Username = "aliyılmaz", Email = "aliyilmaz@gmail.com" });
            users.Add(new User() { Id = 2, Name = "Ahmet", Surname = "Çelebi", Password = "14526", Username = "celebiahmet", Email = "ahmetcelei@gmail.com" });
            users.Add(new User() { Id = 3, Name = "Zeynep", Surname = "Örnek", Password = "45668", Username = "zeynepcan", Email = "zeynep@gmail.com" });
            users.Add(new User() { Id = 4, Name = "Fatmanur", Surname = "Çalışkan", Password = "123654", Username = "fatmanurc", Email = "fatmanurv@gmail.com" });

            return users;
        }

        private string SetToken(int userId)
        {
            var token = GenerateJwtToken(userId);
            string key = $"token_{userId}";
            _cacheManager.Set(key, token, new TimeSpan(7, 0, 0));
            return token;

        }

        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
