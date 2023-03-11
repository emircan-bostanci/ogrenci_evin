using System.ComponentModel.DataAnnotations;

namespace ogrencievin.Models;
public class ViewModelUser{

    [Required]
    public string username{get;set;}
    [Required]
    public string password{get;set;}
}