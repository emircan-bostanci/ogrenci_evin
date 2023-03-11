using System.Linq;
using Microsoft.EntityFrameworkCore;
using ogrencievin.Models.Entities;

namespace ogrencievin.Models;

public class Repository<T> : IRepository<T> where T : class 
{
    private readonly Context context;
    public Repository(Context context){
       this.context = context; 
    }
    public void Add(T item)
    {
        context.Set<T>().Add(item);
        context.SaveChanges();
    }

    public void ClearRange(int from, int to)
    {
        var items = context.Set<T>().Skip(from).Take(to - from).ToList();
        context.Set<T>().RemoveRange(items);
        context.SaveChanges();
    }

    public void Delete(T item)
    {
        context.Set<T>().RemoveRange(item);
        context.SaveChanges();
    }

    public virtual bool Find(T findItem)
    {
        T item = context.Set<T>().FirstOrDefault(user => user == findItem); 
        if(item == null){
            return false;
        }
        return true;
    }

    public T[] GetAll()
    {
        return context.Set<T>().ToArray();
    }
    public void Change(T item,T changeTo){
        var findedItem = context.Set<T>().FirstOrDefault(i => i == item);
        foreach(var property in typeof(T).GetProperties()){
            if(property.Name != "id"){
                property.SetValue(findedItem,property.GetValue(changeTo));
            }
        }
        context.SaveChanges();
    }
    public T? FindById(int id){
        var item = context.Set<T>().AsEnumerable().FirstOrDefault<T>(x => (int?)typeof(T).GetProperty("id").GetValue(x) == id);
        return item;
    }
    public T[] IncludeProperty(string includeProperty){
        return context.Set<T>().Include(includeProperty).ToArray();
    }
    public T[] findKeywordOnProperties(string[] properties,string[] keywords){
        List<T> findedItems = new List<T>();
        var tempKeywords = keywords.AsEnumerable();
        foreach(var property in properties){
            foreach(var item in GetAll()){
                List<string> itemKeywords = new List<string>();
                itemKeywords =StringConverter.StringToArray(typeof(T).GetProperty(property).GetValue(item).ToString(),' ').ToList();
                foreach(var i in tempKeywords)
                    if(itemKeywords.Any(keyword => keyword == i)){
                       findedItems.Add(item); 
                    }
                }
            }
        return findedItems.ToArray();
   }
    public T[] FindByProperty(string property,Object find){
        return context.Set<T>().AsEnumerable().Where(f => typeof(T).GetProperty(property).GetValue(f) == find).ToArray();
    }
}