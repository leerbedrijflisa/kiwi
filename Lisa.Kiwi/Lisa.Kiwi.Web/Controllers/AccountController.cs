﻿using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.Web.ViewModels;
using Lisa.Kiwi.WebApi.Access;

namespace Lisa.Kiwi.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var authProxy = new AuthenticationProxy("http://localhost:20151", "/api/oauth", "/api/users/");

            var response = await authProxy.Login(model.UserName, model.Password);

            if (response.Status != LoginStatus.Success)
            {
                ModelState.AddModelError("password", "Wachtwoord of gebruikersnaam is onjuist");

                return View();
            }

            var tokenCookie = new HttpCookie("token", response.Token);
            var tokenTypeCookie = new HttpCookie("token_type", response.TokenType);

            var dateToSet = DateTime.Now.AddMinutes(response.TokenExpiresIn);

            tokenCookie.Expires = dateToSet;
            tokenTypeCookie.Expires = dateToSet;
            tokenCookie.HttpOnly = true;
            tokenTypeCookie.HttpOnly = true;

            Response.Cookies.Add(tokenCookie);
            Response.Cookies.Add(tokenTypeCookie);

            // dont store isAdmin value in cookies as it be unsafe

            return RedirectToAction("Index", "Dashboard");
        }
    }
}