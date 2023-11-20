using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using todoMaui.Models;

namespace todoMaui.Services
{
    public interface IAppService
    {
        public Task<string> AuthenticateUser(LoginModel loginModel);
        Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registrationModel);
    }

    public class AppService : IAppService
    {
        public string BaseAddress = "";

        public async Task<string> AuthenticateUser(LoginModel loginModel)
        {
            string returnStr = string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyDetail = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,"userid1"),
                new Claim(JwtRegisteredClaimNames.Name,$"{loginModel.UserName}")
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "jwt_audience",
                Issuer = "jwt_issuer",
                Expires = DateTime.UtcNow.AddDays(1),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return OK(tokenHandler.WriteToken(token));
            return tokenHandler.WriteToken(token);



            //CreatePasswordHash(loginModel.Password, out byte[] passwordHash, out byte[] passwordSalt);
            //User user = new User();
            //user.Username = loginModel.UserName;
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            if (loginModel.UserName == "a@b.c" && loginModel.Password == "password")
            {
                return "valid user";
            }
            else
            {
                return "";
            }

            using (var client = new HttpClient())
            {
                var url = $"{BaseAddress}{APIs.AuthenticateUser}";

                var serialized = JsonConvert.SerializeObject(loginModel);

                StringContent stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    returnStr = await response.Content.ReadAsStringAsync();
                }
            }

            return returnStr;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registrationModel)
        {
            bool isSuccess;
            string errorMessage;
            return (true, "success");


            using (var client = new HttpClient())
            {
                var url = $"{BaseAddress}{APIs.RegisterUser}";

                var serialized = JsonConvert.SerializeObject(registrationModel);

                StringContent stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    isSuccess = true;
                }
                else
                {
                    errorMessage = await response.Content.ReadAsStringAsync();
                }
            }

            return (isSuccess, errorMessage);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
