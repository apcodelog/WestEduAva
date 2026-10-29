using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Client.Interfaces;

public interface IDataObjectService
{
    Task Add<T>(T data) where T : class;
    Task Delete<T>(string dbId) where T : class;
    Task<T> Find<T>(string parameter, string value) where T : class;
    Task<List<T>> List<T>() where T : class;
    Task Update<T>(T data, string dbId) where T : class;
}
