using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Models;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using Core.Models;
using Client.Enums;
using Client.Interfaces;
using System.Linq;
using Client.Services;

namespace Client.ViewModels;

public partial class UsersViewModel : ViewModelBase
{
    private readonly Dictionary<ViewTypeEnum, (string pageTitle, string resource)> pageTitleMap = new Dictionary<ViewTypeEnum, (string pageTitle, string resource)>
    {
        {ViewTypeEnum.Students, ("Elever", "students")},
        {ViewTypeEnum.Teachers, ("Lärare", "teachers")}
    };

    private readonly IDataObjectService _dataObjectService;
    private readonly ViewTypeEnum _viewType;

    [ObservableProperty]
    public partial ObservableCollection<IUserAva> Users { get; set; } = [];

    public UsersViewModel()
    {
        var viewType = ViewTypeEnum.Students;

        _viewType = viewType;
        _dataObjectService = new DataObjectService(viewType);
        var info = pageTitleMap[viewType];
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", $"TestData_{info.resource}.json");
        PageTitle = info.pageTitle + " - For development preview";

        var list = viewType switch
        {
            ViewTypeEnum.Students => new Storage<StudentAva>().Read(path).Cast<IUserAva>(),
            ViewTypeEnum.Teachers => new Storage<TeacherAva>().Read(path).Cast<IUserAva>(),
            _ => throw new ArgumentOutOfRangeException()
        };
        Users = new ObservableCollection<IUserAva>(list);
    }

    public UsersViewModel(ViewTypeEnum viewType)
    {
        _viewType = viewType;
        _dataObjectService = new DataObjectService(viewType);
        var info = pageTitleMap[viewType];
        PageTitle = info.pageTitle;

        Init();
    }

    private async void Init()
    {
        await LoadUsers();
    }

    private async Task LoadUsers()
    {
        var list = _viewType switch
        {
            ViewTypeEnum.Students => (await _dataObjectService.List<StudentAva>()).Cast<IUserAva>(),
            ViewTypeEnum.Teachers => (await _dataObjectService.List<TeacherAva>()).Cast<IUserAva>(),

            _ => throw new ArgumentOutOfRangeException()
        };
        Users = new ObservableCollection<IUserAva>(list);
    }

    [RelayCommand]
    public async Task Delete(string internalId)
    {
        Console.WriteLine("Not implemented");
    }
}
