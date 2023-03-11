using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ogrencievin.Models.Entities;

namespace ogrencievin.Controllers;

public class MessagingController : Controller{
    private readonly UserManager<User> userManager;

    public MessagingController(UserManager<User> userManager)
    {
        this.userManager = userManager;
        
    }
    public async Task<IActionResult> Dm(){
        var tempUser = await userManager.GetUserAsync(User);
        
        return View(tempUser.activeChatUsers);
    }
    [HttpPost]
    public async Task<IActionResult> Messaging(string id,Message sendedMessage){
        var tempUser = await userManager.GetUserAsync(User);
        var sendedUser =  userManager.Users.FirstOrDefault(u => u.Id == id);
        tempUser.activeChatUsers.Add(sendedUser);
        tempUser.sendedMessages.Add(sendedMessage);
        sendedMessage.sender = tempUser;
        sendedMessage.receiver = await userManager.FindByIdAsync(id);
        await userManager.UpdateAsync(tempUser);
        return View("Dm");
   }

}