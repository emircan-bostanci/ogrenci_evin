using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using ogrencievin.Models.CustomValidations;
using ogrencievin.Models.Entities;
using ogrencievin.Models.Image;

namespace ogrencievin.Models;

public sealed class ViewModelEstate{
    public string title {get;set;}
    public string description {get;set;}
    [DataType(DataType.Upload)]
    [Required()]
    [AllowedExtensions(new ImageTypes[]{ImageTypes.JPG,ImageTypes.PNG})]
    public List<IFormFile> image {get;set;}
    public string address{get;set;}
    public string rooms {get;set;}
    public static implicit operator Estate(ViewModelEstate estate){
        Estate tempEstate = new Estate();
        tempEstate.descriptions = estate.description;
        tempEstate.title = estate.title;
        tempEstate.address = estate.address;
        tempEstate.images = new List<Entities.Image>();
        tempEstate.rooms = estate.rooms;
        foreach(var item in estate.image)
            tempEstate.images.Add(new Entities.Image{imagePath = item.FileName});
        return tempEstate;
    }
}