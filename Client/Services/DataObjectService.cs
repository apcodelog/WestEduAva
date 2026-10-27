using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;

namespace Client.Services;

public class DataObjectService<T> : IDataObjectService<T> where T : class
{
    private readonly string _resource;

    public DataObjectService(string resource)
    {
        _resource = resource;
    }

    public async Task Add(T data)
    {
        var httpService = new HttpService<T>();
        await httpService.Post(_resource, data);
    }

    public async Task Delete(string dbId)
    {
        var httpService = new HttpService<T>();
        await httpService.Delete(_resource, dbId);
    }

    public async Task<T> Find(string parameter, string value)
    {
        var httpService = new HttpService<T>();
        var searchCriteria = "{" + $"\"{parameter}\":\"{value}\"" + "}";
        return await httpService.Get(_resource, searchCriteria);
    }

    public async Task<List<T>> List()
    {
        var httpService = new HttpService<T>();
        return await httpService.Get(_resource);
    }

    public async Task Update(T data, string dbId)
    {
        var httpService = new HttpService<T>();
        await httpService.Put(_resource, data, dbId);
    }
}
