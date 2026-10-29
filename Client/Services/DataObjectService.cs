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
    public string Resource { get; }

    public string PageTitleSingular { get; }

    public string PageTitlePlural { get; }

    public DataObjectService(ViewTypeEnum viewType)
    {
        switch (viewType)
        {
            case ViewTypeEnum.Courses:
                Resource = "courses";
                PageTitleSingular = "Kurs";
                PageTitlePlural = "Kurser";
                break;

            case ViewTypeEnum.Students:
                Resource = "students";
                PageTitleSingular = "Elev";
                PageTitlePlural = "Elever";
                break;

            case ViewTypeEnum.Teachers:
                Resource = "teachers";
                PageTitleSingular = "Lärare";
                PageTitlePlural = "Lärare";
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task Add<T>(T data) where T : class
    {
        var httpService = new HttpService<T>();
        await httpService.Post(Resource, data);
    }

    public async Task Delete<T>(string dbId) where T : class
    {
        var httpService = new HttpService<T>();
        await httpService.Delete(Resource, dbId);
    }

    public async Task<T> Find<T>(string parameter, string value) where T : class
    {
        var httpService = new HttpService<T>();
        var searchCriteria = "{" + $"\"{parameter}\":\"{value}\"" + "}";
        return await httpService.Get(Resource, searchCriteria);
    }

    public async Task<List<T>> List<T>() where T : class
    {
        var httpService = new HttpService<T>();
        return await httpService.Get(Resource);
    }

    public async Task Update<T>(T data, string dbId) where T : class
    {
        var httpService = new HttpService<T>();
        await httpService.Put(Resource, data, dbId);
    }
}
