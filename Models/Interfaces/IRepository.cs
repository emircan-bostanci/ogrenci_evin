using ogrencievin.Models.Entities;

namespace ogrencievin.Models; 

public interface IRepository <T> where T : class  {
    void Add(T item);
    void Delete(T item);
    void ClearRange(int from,int to);
    T[] GetAll();
    bool Find(T user);
    void Change(T item,T changeTo);

    public T FindById(int id);

    T[] IncludeProperty(string includeProperty);
    T[] FindByProperty(string property,Object find);
    T[] findKeywordOnProperties(string[] properties,string[] keywords);
} 