namespace ogrencievin.Models;

public class StringConverter
{
   public static string[] StringToArray(string text,char seperator){
      if(text.Contains("\r\n")){
        text = text.Replace("\r\n","");
      }
      text = text.Trim();
      string[] tempText = text.Split(seperator);
      return tempText;
   } 
}
