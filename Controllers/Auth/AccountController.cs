using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsApi.Models;
using NewsApi.Models.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace NewsApi.Controllers.Auth
{
    public class AccountController : Controller
    {
        private readonly NewsContext _context;
        public AccountController(NewsContext context)
        {
            _context = context;
            users = _context.Users.ToList();
        }

        // временная заглушка вместо данных из БД
        private List<User> users { get; set; }

        [HttpPost("/token")]
        public IActionResult Token(string userName, string password)
        {
            var identity = GetIdentity(userName, password);
            if (identity == null)
                return BadRequest(new { errorText = "Invalid userName or password!" });

            var now = DateTime.UtcNow;
            // создание jwt Токена
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                accec_token = encodedJwt,
                User = identity.Name,
                Role = identity.Claims.FirstOrDefault().Value
            };

            return Json(response);
        }

        // функция для поиска пользователся и возвращающая claimsIdentity
        private ClaimsIdentity GetIdentity(string userName, string password)
        {
            User user = users.FirstOrDefault(x => x.Login == userName && x.Password == password);
            if (user == null)
                return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Role)
            };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

    }
}
