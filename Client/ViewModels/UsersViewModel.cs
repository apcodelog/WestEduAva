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
using Core.Models.Users;

namespace Client.ViewModels;

public partial class UsersViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainViewModel;
    private readonly IDataObjectService _dataObjectService;
    private readonly ViewTypeEnum _viewType;

    private readonly Dictionary<ViewTypeEnum, Func<Task<IEnumerable<IUserAva>>>> _loadMap = new();
    private readonly Dictionary<ViewTypeEnum, Func<string, Task>> _deleteMap = new();
    private readonly Dictionary<ViewTypeEnum, ViewTypeEnum> _goToAddMap = new();
    private readonly Dictionary<ViewTypeEnum, ViewTypeEnum> _goToEditMap = new();

    private string _deleteTargetId = "";

    [ObservableProperty]
    public partial ObservableCollection<IUserAva> Users { get; set; } = [];

    [ObservableProperty]
    public partial bool PopupVisible { get; set; } = false;
    [ObservableProperty]
    public partial bool IsBusy { get; set; } = false;

    public string AddObjectButtonText => "+ Lägg till " + _dataObjectService.PageTitleSingular.ToLowerInvariant();

    public UsersViewModel()
    {
        MapsInit();

        // Uses students viewtype as example for development preview
        var viewType = ViewTypeEnum.Students;

        _viewType = viewType;
        _dataObjectService = new DataObjectService(viewType);
        _mainViewModel = new MainWindowViewModel(new Factories.PageViewFactory());

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", $"TestData_{_dataObjectService.Resource}.json");
        PageTitle = _dataObjectService.PageTitlePlural + " - For development preview";

        var list = new Storage<StudentAva>().Read(path).Cast<IUserAva>();
        Users = new ObservableCollection<IUserAva>(list);
    }

    public UsersViewModel(MainWindowViewModel mainViewModel, ViewTypeEnum viewType)
    {
        MapsInit();

        _viewType = viewType;
        _dataObjectService = new DataObjectService(viewType);
        _mainViewModel = mainViewModel;
        PageTitle = _dataObjectService.PageTitlePlural;

        Init();
    }

    private void MapsInit()
    {
        _loadMap[ViewTypeEnum.Students] = async () => (await _dataObjectService.List<StudentAva>()).Cast<IUserAva>();
        _loadMap[ViewTypeEnum.Teachers] = async () => (await _dataObjectService.List<TeacherAva>()).Cast<IUserAva>();

        _deleteMap[ViewTypeEnum.Students] = (id) => DeleteFinish<StudentAva, Student>(id);
        _deleteMap[ViewTypeEnum.Teachers] = (id) => DeleteFinish<TeacherAva, Teacher>(id);

        _goToAddMap[ViewTypeEnum.Students] = ViewTypeEnum.NewStudent;
        _goToAddMap[ViewTypeEnum.Teachers] = ViewTypeEnum.NewTeacher;

        _goToEditMap[ViewTypeEnum.Students] = ViewTypeEnum.EditStudent;
        _goToEditMap[ViewTypeEnum.Teachers] = ViewTypeEnum.EditTeacher;
    }

    private async void Init()
    {
        await LoadUsers();
    }

    private async Task LoadUsers()
    {
        var list = await _loadMap[_viewType]();
        Users = new ObservableCollection<IUserAva>(list);
    }

    [RelayCommand]
    public async Task Delete()
    {
        await _deleteMap[_viewType](_deleteTargetId);
    }
    public async Task DeleteFinish<TAva, T>(string internalId) 
        where TAva : class, IUserAva 
        where T : class
    {
        IsBusy = true;
        try
        {
            TAva user = await _dataObjectService.Find<TAva>("id", internalId);
            if (user is not null)
            {
                await _dataObjectService.Delete<T>(user._id);
                await LoadUsers();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            HidePopup();
        }
    }

    [RelayCommand]
    public void GoToAddUserView()
    {
        _mainViewModel.GoToView(_goToAddMap[_viewType]);
    }

    [RelayCommand]
    public void GoToEditUserView(string interalId)
    {
        _mainViewModel.GoToView(_goToEditMap[_viewType], interalId);
    }




    [RelayCommand]
    public void ShowPopup(string internalId)
    {
        _deleteTargetId = internalId;
        PopupVisible = _mainViewModel.BlockForPopup = true;
    }
            
    [RelayCommand]
    public void HidePopup()
    {
        PopupVisible = _mainViewModel.BlockForPopup = false;
        IsBusy = false;
    }
}
