using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogrencievin.Models;
using ogrencievin.Models.Entities;

namespace ogrencievin.Controllers;

public class ProfileController : Controller{
    private readonly UserManager<User> userManager;
    private readonly IRepository<Estate> estateRepository;
    private readonly User user;
    public ProfileController(UserManager<User> userManager,IRepository<Estate> estateRepository)
    {
        this.userManager = userManager;
        this.estateRepository = estateRepository;
        
    }
    [HttpGet]
    //TODO : Unfavorite
    public async Task<IActionResult> Index(int id)
    {   
        var user = await userManager.GetUserAsync(User);
        estateRepository.IncludeProperty("images");
        estateRepository.IncludeProperty("likers");
        var estates = estateRepository.FindByProperty("likers",user);
        ViewData["estates"] = user.favoriteEstates;
        estateRepository.IncludeProperty("author");
        var publishes = estateRepository.FindByProperty("author",user);
        ViewData["publishes"] = Level.calculateLevel(publishes.ToList()); 
 
        return View(user);
    }
    public async Task<IActionResult> Unfavorite(int id){
        var user = userManager.Users.Include("favoriteEstates").Single(u => u.UserName == User.Identity.Name) ;
        if(id != null){
            user.favoriteEstates.Remove(estateRepository.FindById(id));
            await userManager.UpdateAsync(user);
        }
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Publishes(){
        var user = await userManager.GetUserAsync(User);
        estateRepository.IncludeProperty("images");
        estateRepository.IncludeProperty("author");
        var estates = estateRepository.FindByProperty("author",user);
        ViewData["uploads"] = estates;
        var publishes = estateRepository.FindByProperty("author",user);
        ViewData["publishes"] = Level.calculateLevel(publishes.ToList()); 
 
        return View(user);
    }
    public IActionResult Delete(int id){
        estateRepository.Delete(estateRepository.FindById(id));
        return RedirectToAction("Index");
    }
    

}