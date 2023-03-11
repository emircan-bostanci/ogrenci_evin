using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ogrencievin.Models.GeoEntity;

namespace ogrencievin.Models.Entities;
public class Estate 
{
    [Key]
    public int id{get;set;}
    public string title {get;set;}
    public string descriptions {get;set;}
    public string address { get;set;}
    public string rooms {get;set;}
    public DateTime publishDate {get;set;}
    public virtual User author {get;set;}
    public virtual List<User> likers {get;set;}
    public List<Image> images{get;set;}
}