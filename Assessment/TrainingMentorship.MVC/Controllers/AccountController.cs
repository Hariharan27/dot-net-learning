using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using TrainingMentorship.MVC.Models;
using TrainingMentorship.MVC.Services;
namespace TrainingMentorship.MVC.Controllers;

public class AccountController : Controller
    {
    private readonly AuthApiService _auth;

        public AccountController(AuthApiService auth)
        {
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _auth.LoginAsync(model.Email, model.Password);

            if (result == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View(model);
            }

            // Save Session Values
            HttpContext.Session.SetString("JwtToken", result.Token);
            HttpContext.Session.SetString("UserRole", result.Role.ToString());
            HttpContext.Session.SetString("FullName", result.FullName);
            HttpContext.Session.SetInt32("UserId", result.UserId);

        // ✅ Redirect based on role
        switch (result.Role.ToString())
        {
            case "Admin":
                return RedirectToAction("Index", "TrainingProgram");

            case "Mentor":
                return RedirectToAction("Index", "TrainingProgram");

            case "Trainee":
                return RedirectToAction("Index", "TrainingProgram");

            default:
                return RedirectToAction("Login");
        }
    }

    public IActionResult Logout()
    {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
     }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var success = await _auth.RegisterAsync(model);

        if (!success)
        {
            ViewBag.Error = "Email already exists.";
            return View(model);
        }

        TempData["Success"] = "User registered successfully!";
        return RedirectToAction("Register");
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


}



