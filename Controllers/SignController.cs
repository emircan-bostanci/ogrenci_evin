using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ogrencievin.Models;
using ogrencievin.Models.Entities;

namespace ogrencievin.Controllers;

[AllowAnonymous]
public class SignController : Controller
{
    private readonly ClaimManager claimManager;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    public SignController(ClaimManager claimManager,UserManager<User> userManager,SignInManager<User> signInManager) {
        this.claimManager = claimManager;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    public IActionResult Login(){
        //TODO : Login 
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(User user){
        User? temp = await userManager.FindByNameAsync(user.UserName);
        if(ModelState.IsValid == false)
            return View();
        if(temp != null){
            await signInManager.SignOutAsync();
            var result = await signInManager.PasswordSignInAsync(temp,user.PasswordHash,false,false);
            if(result.Succeeded){
                return RedirectToAction("Index","Home",null);
            }
            else{
                ModelState.AddModelError("PasswordUsernameError","Kullanıcı Adı ve Şifre Aynı Olmalı");
            }
        }
        return View();
    }
    public IActionResult Register(){
        //TODO : Register
        return View();
    }
    //TODO REGISTER ISLEMI IdentityResult result = await userManager.CreateAsync(user,user.PasswordHash); 
    //            if(result.Succeeded)
    [HttpPost]
    public async Task<IActionResult> Register(User user){
        if(ModelState.IsValid){
        User temp = await userManager.FindByNameAsync(user.UserName);
        if(temp == null){
            var newUser = await userManager.CreateAsync(user,user.PasswordHash);
            if(newUser.Succeeded){
                return RedirectToAction("Login");
            }
            else
                newUser.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code,e.Description));
        }
        }
        return View();
    }
    public async Task<IActionResult> Logout(){
        //TODO : Logout
        await signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }
}