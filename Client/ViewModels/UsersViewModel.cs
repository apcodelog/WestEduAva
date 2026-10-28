using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Models;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using Core.Models.Users;
using Core.Models;
using Client.Enums;
using Client.Interfaces;
using System.Linq;

namespace Client.ViewModels;

public partial class UsersViewModel : ViewModelBase
{
    private readonly Dictionary<ViewTypeEnum, (string pageTitle, string resource)> pageTitleMap = new Dictionary<ViewTypeEnum, (string pageTitle, string resource)>
    {
        {ViewTypeEnum.Students, ("Elever", "students")},
        {ViewTypeEnum.Teachers, ("Lärare", "teachers")}
    };

    [ObservableProperty]
    public partial ObservableCollection<IUserAva> Users { get; set; } = [];

    public UsersViewModel()
    {
        var viewType = ViewTypeEnum.Students;
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
        var info = pageTitleMap[viewType];
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", $"TestData_{info.resource}.json");
        PageTitle = info.pageTitle;

        var list = viewType switch
        {
            ViewTypeEnum.Students => new Storage<StudentAva>().Read(path).Cast<IUserAva>(),
            ViewTypeEnum.Teachers => new Storage<TeacherAva>().Read(path).Cast<IUserAva>(),
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
