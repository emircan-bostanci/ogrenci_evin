namespace ogrencievin.Models;

public class Level{
    public static int calculateLevel<T>(List<T> list){
        return list.Count;
    }
}