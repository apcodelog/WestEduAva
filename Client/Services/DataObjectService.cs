using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Enums;
using Client.Interfaces;
using Client.Models;
using Core.Models;
using Core.Models.Users;

namespace Client.Services;

public class DataObjectService : IDataObjectService
{
    private readonly string _resource;

    public DataObjectService(ViewTypeEnum viewType)
    {
        _resource = viewType switch
        {
            ViewTypeEnum.Courses => "courses",

            ViewTypeEnum.Students => "students",

            ViewTypeEnum.Teachers => "teachers",

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task Add<T>(T data) where T : class
    {
        var httpService = new HttpService<T>();
        await httpService.Post(_resource, data);
    }

    public async Task Delete<T>(string dbId) where T : class
    {
        var httpService = new HttpService<T>();
        await httpService.Delete(_resource, dbId);
    }

    public async Task<T> Find<T>(string parameter, string value) where T : class
    {
        var httpService = new HttpService<T>();
        var searchCriteria = "{" + $"\"{parameter}\":\"{value}\"" + "}";
        return await httpService.Get(_resource, searchCriteria);
    }

    public async Task<List<T>> List<T>() where T : class
    {
        var httpService = new HttpService<T>();
        return await httpService.Get(_resource);
    }

    public async Task Update<T>(T data, string dbId) where T : class
    {
        var httpService = new HttpService<T>();
        await httpService.Put(_resource, data, dbId);
    }
}
