using CognifyzDatabaseIntegrationAndUserAuthentication.Context;
using CognifyzDatabaseIntegrationAndUserAuthentication.Dto;
using CognifyzDatabaseIntegrationAndUserAuthentication.Entities;
using CognifyzDatabaseIntegrationAndUserAuthentication.Implementation.Interface;
using CognifyzDatabaseIntegrationAndUserAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.Data;
using System.Net.Mail;

namespace CognifyzDatabaseIntegrationAndUserAuthentication.Implementation.Services
{
    public class UserService : IUserService 
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(ApplicationDbContext context,
                          UserManager<User> userManager,
                          SignInManager<User> signInManager,
                          RoleManager<IdentityRole> _roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = _roleManager;
        }
        public async Task<List<UserDto>> GetAllUser()
        {
            return await _context.Users
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Address = x.Address, 
                    UserName = x.UserName,
                    UserRole = x.UserRole,
                    Email = x.Email,
                    FullName = x.PhoneNumber
                }).ToListAsync();
        }

        public async Task<Status> Login(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
            {
                return new Status { Success = false, Message = "User not found" };
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return new Status { Success = true, Message = "Login successful" };
            }
            else
            {
                return new Status { Success = false, Message = "Invalid username or password" };
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        } 

        public async Task<UserDto> GetUserDetail(string id)
        {
            return await _context.Users 
                .Where(x => x.Id == id)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Address = x.Address,
                    UserName = x.UserName,
                    UserRole = x.UserRole,
                    Email = x.Email,
                    FullName = x.PhoneNumber

                }).FirstOrDefaultAsync();
        }
    }
}
