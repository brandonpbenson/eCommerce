﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MisterRobotoArigato.Data;
using MisterRobotoArigato.Models;
using MisterRobotoArigato.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MisterRobotoArigato.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> RegisterConfirmed(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                List<Claim> claims = new List<Claim>();
                var user = new ApplicationUser
                {
                    UserName = rvm.Email,
                    Email = rvm.Email,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName
                };
                //creates user in the database
                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Succeeded)
                {
                    //capturing the user's name
                    Claim nameClaim = new Claim("FullName", $"{user.FirstName} {user.LastName}");
                    Claim firstNameClaim = new Claim("FirstName", $"{user.FirstName}");

                    //add a basket to the user
                    Claim basketClaim = new Claim("basket", $"{user.Email}");

                    //capturing the user's email
                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);
                    claims.Add(nameClaim);
                    claims.Add(firstNameClaim);
                    claims.Add(emailClaim);

                    //adds claim to the user
                    await _userManager.AddClaimsAsync(user, claims);

                    if (user.Email == "doge@gmail.com" || user.Email == "ecaoile@my.hpu.edu")
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                    }

                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(rvm);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(lvm.Email);
                    if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "You don't know your credentials.");
                }
            }

            return View(lvm);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string remoteError = null)
        {
            if (remoteError != null)
            {
                TempData["ErrorMessage"] = "Error from provider";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            var fullName = info.Principal.FindFirstValue(ClaimTypes.Name);
            string lastName = "";
            int idx = fullName.LastIndexOf(' ');

            if (idx != -1)
            {
                lastName = fullName.Substring(idx + 1);
            }
            return View("ExternalLogin", new ExternalLoginViewModel {
                FirstName = firstName,
                LastName = lastName,
                Email = email });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel elvm)
        {
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    TempData["Error"] = "Error loading information";
                }

                RegisterViewModel rvm = new RegisterViewModel
                {
                    FirstName = elvm.FirstName,
                    LastName = elvm.LastName,
                    Email = elvm.Email,
                    Password = "B@con123"
                };

                //return RedirectToAction("RegisterConfirmed", new { rvm = newRVM } );

                var user = new ApplicationUser
                {
                    FirstName = elvm.FirstName,
                    LastName = elvm.LastName,
                    UserName = elvm.Email,
                    Email = elvm.Email
                };
                var result = await _userManager.CreateAsync(user, rvm.Password);
                List<Claim> claims = new List<Claim>();

                    //capturing the user's name
                    Claim nameClaim = new Claim("FullName", $"{user.FirstName} {user.LastName}");
                    Claim firstNameClaim = new Claim("FirstName", $"{user.FirstName}");

                    //add a basket to the user
                    Claim basketClaim = new Claim("basket", $"{user.Email}");

                    //capturing the user's email
                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);
                    claims.Add(nameClaim);
                    claims.Add(firstNameClaim);
                    claims.Add(emailClaim);

                    //user.SecurityStamp = "1234";
                    //adds claim to the user

                        await _userManager.AddClaimsAsync(user, claims);
                    

                    if (user.Email == "doge@gmail.com" || user.Email == "ecaoile@my.hpu.edu")
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);
                    }

                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                    //var result = await _userManager.CreateAsync(user);

                    //if (result.Succeeded)
                    //{
                    //    result = await _userManager.AddLoginAsync(user, info);
                    //    if (result.Succeeded)
                    //    {
                    //        await _signInManager.SignInAsync(user, isPersistent: false);
                    //        return RedirectToAction("Index", "Home");
                    //    }
                    //}
                
            }

            return View(elvm);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["LoggedOut"] = "User Logged Out";

            return RedirectToAction("Index", "Home");
        }
    }
}
