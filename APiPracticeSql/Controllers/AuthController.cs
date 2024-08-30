using ApiPractice.DAL.Entities;
using APiPracticeSql.Dtos.UserDtos;
using APiPracticeSql.Services.Interfaces;
using APiPracticeSql.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Web;

namespace APiPracticeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly JwtSetting _jwtSetting;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public AuthController(IOptions<JwtSetting> jwtSetting, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IMapper mapper, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _jwtSetting = jwtSetting.Value;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user = await _userManager.FindByNameAsync(registerDto.UserName);
            if (user != null) return Conflict();

            user = new()
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string link = Url.Action(nameof(VerifyEmail), "Auth", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
            //string link = $"https://localhost:7122/auth/verifyEmail?email={user.Email}&token={token}";


            string body = string.Empty;
            using (StreamReader stream = new StreamReader("wwwroot/template/verifyEmailTemplate.html"))
            {
                body = stream.ReadToEnd();
            };
            body = body.Replace("{{link}}", link);
            body = body.Replace("{{username}}", user.UserName);

            _emailService.SendEmail(user.Email, "Verify Email", body);

            return Ok();
        }

        [HttpGet("verifyEmail")]
        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            AppUser? appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null) return NotFound();
            await _userManager.ConfirmEmailAsync(appUser, token);
            return RedirectToAction(nameof(Login));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null) return BadRequest();
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                return BadRequest();
            if (!user.EmailConfirmed)
                return BadRequest("Confirm email...");

            var userRoles = await _userManager.GetRolesAsync(user);


            return Ok(new { token = _tokenService.GetToken(userRoles, user, _jwtSetting) });
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

        [HttpGet("profile")]

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return Ok(new { id = user.Id, name = user.UserName });
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(_mapper.Map<List<UserReturnDto>>(users));
        }

        [HttpPost("resetPasswordSendEmail")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            AppUser? appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
                return BadRequest();

            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            string link = Url.Action(nameof(ResetPassword), "Auth", new { email = appUser.Email, token }, Request.Scheme, Request.Host.ToString());

            string body = "";
            using (StreamReader stream = new StreamReader("wwwroot/template/forgotPassword.html"))
            {
                body = stream.ReadToEnd();
            };
            body = body.Replace("{{link}}", link);
            body = body.Replace("{{username}}", appUser.UserName);

            _emailService.SendEmail(appUser.Email, "Reset Password", body);

            return Ok();
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string token, [FromBody]ResetPasswordDto resetPasswordDto)
        {
            token=HttpUtility.UrlDecode(token);

            AppUser? appUser = await _userManager.FindByEmailAsync(email);
            if (appUser==null) return BadRequest();

            var result = await _userManager.ResetPasswordAsync(appUser, token, resetPasswordDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            await _userManager.UpdateSecurityStampAsync(appUser);
            return Ok();
        }
    }
}
