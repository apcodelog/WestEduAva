using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Client.Interfaces;

public interface IDataObjectService<T> where T : class
{
    Task Add(T data);
    Task Delete(string dbId);
    Task<T> Find(string parameter, string value);
    Task<List<T>> List();
    Task Update(T data, string dbId);
}
