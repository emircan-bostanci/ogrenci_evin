namespace ogrencievin.Models.Image;

static class ImageExtension{
    public static string[] SelectExtensions(ImageTypes[] imageTypes){
        List<String> extensions;
        extensions = new List<string>();
        foreach(ImageTypes imageType in imageTypes){
            if(imageType == ImageTypes.JPG)
                extensions.Add(".jpg");
            if(imageType == ImageTypes.PNG)
                extensions.Add(".png");
            if(imageType == ImageTypes.TIFF)
                extensions.Add(".tiff");
            if(imageType == ImageTypes.GIF)
                extensions.Add(".gif");
            if(imageType == ImageTypes.PDF)
                extensions.Add(".pdf");
            if(imageType == ImageTypes.RAW)
                extensions.Add(".raw");
        }
        return extensions.ToArray();
    }
}
public enum ImageTypes {
    JPG,
    PNG,
    TIFF,
    GIF,
    PDF,
    RAW
}
