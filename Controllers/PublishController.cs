using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ogrencievin.Models;
using ogrencievin.Models.Entities;
using ogrencievin.Models.Image;

namespace ogrencievin.Controllers;

public class PublishController : Controller{
    private readonly UserManager<User> userManager;
    private readonly IRepository<Estate> estateRepository;
    private readonly SaveImage saveImage;
    public PublishController(UserManager<User> userManager,IRepository<Estate> estateRepository,SaveImage saveImage){
        this.userManager = userManager;
        this.estateRepository = estateRepository;
        this.saveImage = saveImage;
    }
    public IActionResult Index(){
        ViewData["Username"] = User.Identity.Name; 
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(ViewModelEstate item){
        if(ModelState.IsValid){
            var estate = (Estate) item;
            estate.address = estate.address.ToUpper();
            estate.author = await userManager.FindByNameAsync(User.Identity.Name);
            estateRepository.Add(estate);
            saveImage.WriteImages(item.image);
            return Json("Urun Eklendi");
        }
        return RedirectToAction("Index","Home");
    }
}