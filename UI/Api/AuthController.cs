using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityData.Identity;
using Interface.Result;
using Interface.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UI.Models.AccountViewModels;

namespace UI.Api
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            Result<string> result;
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (signInResult.Succeeded)
                {
                    result = new Result<string>(GenerateToken(model.Email));
                }
                else
                {
                    result = new Result<string>(false, ResultTypeEnum.Error, "Girdiğiniz bilgiler yanlış!");
                }
            }
            else
            {
                result = new Result<string>(false, ResultTypeEnum.Error, "Lütfen tüm bilgileri eksiksiz girdiğinizden emin olunuz!");
            }

            return Json(result);
        }

        private string GenerateToken(string userName)
        {
            var someClaims = new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, userName), new Claim(JwtRegisteredClaimNames.Email, userName) };
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("denemedenemedenemedeneme"));
            var token = new JwtSecurityToken(
                audience: "mysite.com",
                issuer: "mysite.com",
                claims: someClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}