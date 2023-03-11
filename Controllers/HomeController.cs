using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogrencievin.Models;
using ogrencievin.Models.Entities;

namespace ogrencievin.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Estate> estateRepository;
    private readonly UserManager<User> userManager;
    public HomeController(UserManager<User> userManager, ILogger<HomeController> logger,IRepository<Estate> estateRepository)
    {
       _logger = logger;
       this.estateRepository =estateRepository;
       this.userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        return View(estateRepository.GetAll().Reverse());
    }
    [HttpGet]
    public async Task<IActionResult> Index(int postId)
    {
        return View(estateRepository.IncludeProperty("images").Reverse());
    }
    public async Task<IActionResult> AddToFavorite(int postId){
        var item = estateRepository.FindById(postId);
        
        if(HttpContext.User.Identity.IsAuthenticated ){
            var user = await userManager.GetUserAsync(User);
            estateRepository.IncludeProperty("likers");
            var changedEstate = estateRepository.FindById(postId);
            changedEstate.likers.Add(user);
            estateRepository.Change(estateRepository.FindById(postId),changedEstate);

            await userManager.UpdateAsync(user);
        }
        return RedirectToAction("Index");
    }
    public IActionResult Search(string tags){
        estateRepository.IncludeProperty("images");
        if(tags == null){
            return View("Index",estateRepository.GetAll());
        }
        return View("Index",estateRepository.findKeywordOnProperties(new string[]{"descriptions","rooms"},StringConverter.StringToArray(tags,' ')));
    }
    public IActionResult OrderById(){
        estateRepository.IncludeProperty("images");
        return View("Index",estateRepository.GetAll().OrderByDescending(keyword => keyword.id).Reverse());
    }
    public IActionResult Details(int id){
        if(id == null){
            return RedirectToAction("Index");
        }
        estateRepository.IncludeProperty("author");
        return View(estateRepository.IncludeProperty("images").FirstOrDefault(i => i.id == id));
    }
    public IActionResult Privacy()
    {
        return View();
    }
   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}