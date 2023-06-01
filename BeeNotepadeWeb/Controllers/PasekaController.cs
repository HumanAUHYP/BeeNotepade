﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using CoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BeeNotepadeWeb.Models;

namespace BeeNotepadeWeb.Controllers
{
    public class PasekaController : Controller
    {
        private string path = @"..\CoreLibrary\data\";

        // ссылка на объект - хранилище ульев
        BeehiveStorage beehiveStorage;
        FirebaseAuthProvider auth;

        public PasekaController(IWebHostEnvironment _environment, IBeehiveStorage _beehiveStorage)
        {
            beehiveStorage = (BeehiveStorage)_beehiveStorage;
            auth = new FirebaseAuthProvider(
                            new FirebaseConfig("AIzaSyB9UEZ2WNg_Y2eKYTA-Q21OVNQx37HgqRw"));
        }
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("_UserToken");
            

            if (token != null)
            {
                var user = HttpContext.Session.GetString("_UserEmail");
                beehiveStorage.ReadFromFile($"{path}{user}.txt");
                var today = DateTime.Today;
                foreach (Beehive beehive in beehiveStorage.BeeGarden)
                {
                    beehive.DaysForCheck -= (int)Math.Round((today - beehive.AddDate).TotalDays);
                    if (beehive.DaysForCheck < 0)
                        beehive.DaysForCheck = 0;
                }
                var beeGarden = beehiveStorage.BeeGarden;
            
                return View(beeGarden);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(LoginModel loginModel)
        {
            try
            {
                //create the user
                await auth.CreateUserWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                //log in the new user
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //saving the token in a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("Index");
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }

            return View();

        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel loginModel)
        {
            try
            {
                //log in an existing user
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                string email = fbAuthLink.User.Email;
                email = email.Replace('.', '_').Replace('@', '_');
                //save the token to a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);
                    HttpContext.Session.SetString("_UserEmail", email);

                    return RedirectToAction("Index");
                }

            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }

            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_UserToken");
            HttpContext.Session.Remove("_UserEmail");
            return RedirectToAction("SignIn");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Beehive beehive)
        {
            var user = HttpContext.Session.GetString("_UserEmail");
            beehiveStorage.Add(beehive);
            beehiveStorage.WriteInFile($"{path}{user}.txt");
            return RedirectToAction("Index");
        }

        public IActionResult Change(string id)
        {
            return View(beehiveStorage.FindById(id));
        }

        [HttpPost]
        public IActionResult Change(Beehive beehive)
        {
            var user = HttpContext.Session.GetString("_UserEmail");
            beehiveStorage.Change(beehive);
            beehiveStorage.WriteInFile($"{path}{user}.txt");
            return RedirectToAction("Index");
        }
        public IActionResult Remove(string id)
        {
            var user = HttpContext.Session.GetString("_UserEmail");
            beehiveStorage.RemoveById(id);
            beehiveStorage.WriteInFile($"{path}{user}.txt");
            return RedirectToAction("Index");
        }
    }
}
