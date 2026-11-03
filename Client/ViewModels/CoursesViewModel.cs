using System;
using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Models;
using Core.Models;
using Client.Interfaces;
using Client.Services;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Client.Enums;

namespace Client.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    private readonly IDataObjectService _dataObjectService;

    private readonly MainWindowViewModel _mainViewModel;

    [ObservableProperty]
    public partial ObservableCollection<CourseAva> Courses { get; set; } = [];

    [ObservableProperty]
    public partial bool PopupVisible { get; set; } = false;

    private string _deleteTargetId = "";

    public CoursesViewModel()
    {
        _dataObjectService = new DataObjectService(ViewTypeEnum.Courses);
        _mainViewModel = new MainWindowViewModel(new Factories.PageViewFactory());
        PageTitle = "Kurser - From Dev Preview Constructor";

        var list = new Storage<CourseAva>().Read(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "TestData_courses.json"));
        Courses = new ObservableCollection<CourseAva>(list);
    }

    public CoursesViewModel(MainWindowViewModel mainViewModel, IDataObjectService dataObjectService)
    {
        _dataObjectService = dataObjectService;
        _mainViewModel = mainViewModel;
        PageTitle = "Kurser";
        Init();
    }

    private async void Init()
    {
        await LoadCourses();
    }

    [RelayCommand]
    public async Task Delete()
    {
        try
        {
            CourseAva course = await _dataObjectService.Find<CourseAva>("id", _deleteTargetId);
            if (course is not null)
            {
                await _dataObjectService.Delete<Course>(course._id);
                await LoadCourses();
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

    private async Task LoadCourses()
    {
        try
        {
            Courses = new ObservableCollection<CourseAva>(await _dataObjectService.List<CourseAva>());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    [RelayCommand]
    public void ShowPopup(string internalId)
    {
        _deleteTargetId = internalId;
        PopupVisible = _mainViewModel.IsBusy = true;
    }
            
    [RelayCommand]
    public void HidePopup()
    {
        PopupVisible = _mainViewModel.IsBusy = false;
    }
}
