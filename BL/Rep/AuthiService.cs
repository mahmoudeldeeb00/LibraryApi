using Api_Project.BL.IRep;
using Api_Project.DAL.Entities;
using Api_Project.Helpers;
using Api_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.BL.Rep
{
    public class AuthiService:IAuthiService
    {
        public UserManager<LibraryUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;

        public JWT _jwt;

        public AuthiService(UserManager<LibraryUser> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> rmanager)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = rmanager;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(LibraryUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Isseur,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.ExpireInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public async Task<AuthenticationModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthenticationModel { Message = "this Email is already registered " };

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthenticationModel { Message = "this User Name is already registered " };

            var user = new LibraryUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Pasword);
            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += error.Description + " ";
                }
                return new AuthenticationModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var JwtSecurityToken = await CreateJwtToken(user);
            return new AuthenticationModel
            {
                Email = user.Email,
                ExpiredOn = JwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                UserName = user.UserName
            };
        }

        public async Task<AuthenticationModel> GetTokenAsync(TokenModel model)
        {
            var AuthModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                AuthModel.Message = "email or password is in correct ";
                return AuthModel;
            }
            var JwtSecurityToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            AuthModel.IsAuthenticated = true;
            AuthModel.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
            AuthModel.Email = user.Email;
            AuthModel.UserName = user.UserName;
            AuthModel.ExpiredOn = JwtSecurityToken.ValidTo;
            AuthModel.Roles = roles.ToList();





            return AuthModel;
        }
        public async Task<string> addRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "User Or Role Is Not defined ";
            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User is already in Role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            return result.Succeeded ? "User added succeessfully " : "failed to add User In this role ";

        }

    }
}
