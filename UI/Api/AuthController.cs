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
                    result = new Result<string>(await GenerateToken(model.Email));
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

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            Result<string> result;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    result = new Result<string>(false, ResultTypeEnum.Information, "Mail adresiniz doğrulanmamış!");
                }
                else
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                       $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                    result = new Result<string>("Mail adresinize parola sıfırlama linki gönderilmiştir!");
                }
            }
            else
            {
                result = new Result<string>(false, ResultTypeEnum.Error, "Lütfen tüm bilgileri eksiksiz girdiğinizden emin olunuz!");
            }

            return Json(result);
        }

        private async Task<string> GenerateToken(string userName)
        {
            var user = await _userManager.FindByEmailAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);
            var someClaims = new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, userName), new Claim(JwtRegisteredClaimNames.Email, userName) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(someClaims, "Token");
            // Adding roles code
            // Roles property is string collection but you can modify Select code if it it's not
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("denemedenemedenemedeneme"));
            var token = new JwtSecurityToken(
                audience: "mysite.com",
                issuer: "mysite.com",
                claims: claimsIdentity.Claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}