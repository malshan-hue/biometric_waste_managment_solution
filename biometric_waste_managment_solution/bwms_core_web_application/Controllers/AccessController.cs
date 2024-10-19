using bwms_core_domain.SystemModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SimpleCrypto;
using System;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using System.Security.Claims;
using bwms_core_business_layer.Interfaces;

namespace bwms_core_web_application.Controllers
{
    public class AccessController : Controller
    {
        private readonly PBKDF2 _crypto;
        private readonly Random _random;
        private readonly IUserService _userService;
        public AccessController(IUserService userService)
        {
            _crypto = new PBKDF2();
            _random = new Random();
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var userName = User.FindFirst(ClaimTypes.Email)?.Value;

                    User user = await _userService.GetUserByUserName(userName);

                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("UserName", user.UserName);
                    HttpContext.Session.SetString("IsAdmin", user.IsAuthority.ToString());

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.UserName),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("UserId", user.IsAuthority.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    });

                    if (user.IsAuthority)
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Authority" });

                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Residents" });
                    }
                }
                catch (Exception ex)
                {
                    return View();
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User userlogin)
        {

            User user = await _userService.GetUserByUserName(userlogin.UserName);

            try
            {
                if (!_crypto.Compare(user.Password, _crypto.Compute(userlogin.Password, user.PasswordSalt)))
                {
                    return RedirectToAction("Login", "Residents");
                }

                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("IsAdmin", user.IsAuthority.ToString());

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.UserName),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("UserId", user.IsAuthority.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                });

                if (user.IsAuthority)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Authority" });
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Residents" });
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User userLogin)
        {
            User user = new User()
            {
                UserName = userLogin.UserName,
                Password = _crypto.Compute(userLogin.Password),
                PasswordSalt = _crypto.Salt,
                ActivationCode = _random.Next(100000, 1000000),
                UserGlobalIdentity = Guid.NewGuid(),
                IsAuthority = userLogin.UserName.Contains("bwms") ? true : false
            };

            var status = _userService.CreateUser(user);

            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Access");
        }
    }
}
