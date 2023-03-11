namespace ogrencievin.Models.Entities;

public class Message {
    public int id{get;set;}
    public string message {get;set;}
    public User sender{get;set;}
    public User receiver {get;set;}
    public DateTime sendedDate{get;set;}
}