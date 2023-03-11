namespace ogrencievin.Models.Image;

public class SaveImage{
    private readonly string savePath;
    public SaveImage(string savePath)
    {
        this.savePath = savePath;
    }
    public void WriteImages(List<IFormFile> images){
        Directory.CreateDirectory(savePath);
        foreach(var image in images){
            if(image.Length > 0){
                var tempExtension = Path.GetExtension(image.FileName);
                var tempName =image.FileName;
                var tempPath = Path.Combine(savePath,$"{tempName}");
                using(Stream fileStream = new FileStream(tempPath,FileMode.Create)){
                    image.CopyTo(fileStream);
                }
            }
        }
    }
}