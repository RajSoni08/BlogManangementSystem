using AutoMapper;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class HomeRepository : Repository<UserDTO>, IHomeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IMapper _mapper;
        private string secretKey;
        public HomeRepository(ApplicationDbContext db, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : base(db)
        {
            _db = db;
            _mapper = mapper;
            userManager = _userManager;
            roleManager = _roleManager;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }



        public bool IsUniqueUser(string username)
        {
            var user = _db.user.FirstOrDefault(x => x.Name == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            bool IsValid = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if (user == null || IsValid==false)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            var roles = await userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                  //  new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),

                User = _mapper.Map<UserDTO>(user),

            };
            return loginResponseDTO;


        }
        public async Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDTO.UserName,
            
                Email = registrationRequestDTO.UserName,
                NormalizedEmail = registrationRequestDTO.UserName.ToUpper()



            };
            try
            {
                var result = await userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                        await roleManager.CreateAsync(new IdentityRole("Reader"));
                    }
                    await userManager.AddToRoleAsync(user, "Admin");
                    var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registrationRequestDTO.UserName);
                    return _mapper.Map<UserDTO>(userToReturn);
                }

            }
            catch (Exception e)
            {
            }

            return new UserDTO();
        }

        //public async Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        //{
        //    UserDTO user = new()
        //    {
        //        Name = registrationRequestDTO.UserName,
        //        Password = registrationRequestDTO.Password,
        //        Email = registrationRequestDTO.Email,
        //        Role = registrationRequestDTO.Role
        //    };
        //    _db.user.Add(user);
        //    await _db.SaveChangesAsync();
        //    user.Password = "";
        //    return user;
        //}
        public async Task<UserDTO> UpdateAsync(UserDTO entity)
        {
            _db.user.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }


    }
