using System.ComponentModel.DataAnnotations;
using ogrencievin.Models.Image;

namespace ogrencievin.Models.CustomValidations;

public sealed class AllowedExtensionsAttribute : ValidationAttribute{
   private string[] imageExtensions; 
   public AllowedExtensionsAttribute(ImageTypes[] imageTypes){
    imageExtensions =  ImageExtension.SelectExtensions(imageTypes);
   }
   protected override ValidationResult IsValid(object value,ValidationContext validationContext){
        List<IFormFile>? files = value as List<IFormFile>;
        if(files == null){
            return  new ValidationResult("Dosya Bos Olamaz");
        }
        foreach(var file in files){
        string extension = Path.GetExtension(file.FileName);
            if(!imageExtensions.Contains(extension.ToLower()))
                return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success;
   }
   
}
