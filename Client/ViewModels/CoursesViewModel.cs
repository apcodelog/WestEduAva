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

    [ObservableProperty]
    public partial ObservableCollection<CourseAva> Courses { get; set; } = [];

    public CoursesViewModel()
    {
        _dataObjectService = new DataObjectService(ViewTypeEnum.Courses);
        PageTitle = "Kurser - From Dev Preview Constructor";

        var list = new Storage<CourseAva>().Read(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "TestData_courses.json"));
        Courses = new ObservableCollection<CourseAva>(list);
    }

    public CoursesViewModel(IDataObjectService dataObjectService)
    {
        _dataObjectService = dataObjectService;
        PageTitle = "Kurser";
        Init();
    }

    private async void Init()
    {
        await LoadCourses();
    }

    [RelayCommand]
    public async Task Delete(string interalId)
    {
        try
        {
            CourseAva course = await _dataObjectService.Find<CourseAva>("id", interalId);
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
}
