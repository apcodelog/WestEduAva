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
    private readonly MainWindowViewModel _mainViewModel;
    private readonly IDataObjectService _dataObjectService;
    private readonly ViewTypeEnum _viewType;

    [ObservableProperty]
    public partial ObservableCollection<IUserAva> Users { get; set; } = [];

    public string AddObjectButtonText => "+ Lägg till " + _dataObjectService.PageTitleSingular.ToLowerInvariant();

    public UsersViewModel()
    {
        var viewType = ViewTypeEnum.Students;

        _viewType = viewType;
        _dataObjectService = new DataObjectService(viewType);
        _mainViewModel = new MainWindowViewModel(new Factories.PageViewFactory());

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", $"TestData_{_dataObjectService.Resource}.json");
        PageTitle = _dataObjectService.PageTitlePlural + " - For development preview";

        var list = viewType switch
        {
            ViewTypeEnum.Students => new Storage<StudentAva>().Read(path).Cast<IUserAva>(),
            ViewTypeEnum.Teachers => new Storage<TeacherAva>().Read(path).Cast<IUserAva>(),
            _ => throw new ArgumentOutOfRangeException()
        };
        Users = new ObservableCollection<IUserAva>(list);
    }

    public UsersViewModel(MainWindowViewModel mainViewModel, ViewTypeEnum viewType)
    {
        _viewType = viewType;
        _dataObjectService = new DataObjectService(viewType);
        _mainViewModel = mainViewModel;
        PageTitle = _dataObjectService.PageTitlePlural;

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

    [RelayCommand]
    public void GoToAddUserView()
    {
        _mainViewModel.GoToView(_viewType switch
        {
            ViewTypeEnum.Students => ViewTypeEnum.NewStudent,
            ViewTypeEnum.Teachers => ViewTypeEnum.NewTeacher,
            _ => throw new ArgumentOutOfRangeException(nameof(_viewType), _viewType, null)
        });
    }

    [RelayCommand]
    public void GoToEditUserView(string interalId)
    {
        var targetViewType = _viewType switch
        {
            ViewTypeEnum.Students => ViewTypeEnum.EditStudent,
            ViewTypeEnum.Teachers => ViewTypeEnum.EditTeacher,
            _ => throw new ArgumentOutOfRangeException(nameof(_viewType), _viewType, null)
        };
        _mainViewModel.GoToView(targetViewType, interalId);
    }
}
