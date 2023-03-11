

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ogrencievin.Models.Entities
{

   public sealed class User:IdentityUser {
      //TODO : Favoriler
      public List<Estate>? favoriteEstates {get;set;}
      public List<Estate>? sharedEstates{get;set;}
      public List<Message>? sendedMessages{get;set;}
      public List<Message>? receivedMessages {get;set;}
      public List<User>? activeChatUsers {get;set;}
   }
}