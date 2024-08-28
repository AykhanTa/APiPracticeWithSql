using ApiPractice.DAL.Entities;
using APiPracticeSql.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APiPracticeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user=await _userManager.FindByNameAsync(registerDto.UserName);
            if (user != null) return Conflict();

            user = new()
            {
                FullName = registerDto.UserName,
                UserName= registerDto.UserName,
                Email=registerDto.Email
            };
            IdentityResult result=await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null) return BadRequest();
            var result=await _userManager.CheckPasswordAsync(user,loginDto.Password);
            if (!result)
                return BadRequest();

            //jwt token
            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> CreateRole()
        //{
        //    if (await _roleManager.RoleExistsAsync("member"))
        //        await _roleManager.CreateAsync(new() { Name= "member" });
        //    if (await _roleManager.RoleExistsAsync("admin"))
        //        await _roleManager.CreateAsync(new() { Name = "admin" });
        //    return Ok();
        //}
    }
}
