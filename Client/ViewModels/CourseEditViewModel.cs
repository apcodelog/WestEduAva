using System;
using Client.Interfaces;
using Client.Services;
using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Core.Enums;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Core.Models;
using Client.Enums;

namespace Client.ViewModels;

public partial class CourseEditViewModel : ViewModelBase
{
    private readonly IDataObjectService _dataObjectService;
    private string? _dbId;
    private string? _interalId;

    [ObservableProperty]
    public partial string CourseNumber { get; set; } = "";
    [ObservableProperty]
    public partial string Title { get; set; } = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CourseTypeIsOnSite))]
    [NotifyPropertyChangedFor(nameof(CourseTypeIsOnline))]
    public partial CourseTypeEnum CourseType { get; set; } = CourseTypeEnum.OnSite;
    
    [ObservableProperty]
    public partial int Days { get; set; } = 20;
    [ObservableProperty]
    public partial DateTime StartDate { get; set; } = new DateTime(2027, 01, 01);
    [ObservableProperty]
    public partial DateTime EndDate { get; set; } = new DateTime(2027, 01, 31);


    
    public bool CourseTypeIsOnSite
    {
        get => CourseType == CourseTypeEnum.OnSite;
        set
        {
            if (value == true) CourseType = CourseTypeEnum.OnSite;
        }
    }
    public bool CourseTypeIsOnline
    {
        get => CourseType == CourseTypeEnum.Online;
        set
        {
            if (value == true) CourseType = CourseTypeEnum.Online;
        }
    }


    public CourseEditViewModel()
    {
        _dataObjectService = new DataObjectService(ViewTypeEnum.Courses);
        PageTitle = "Ny / Ändra kurs - For preview during development";
    }

    public CourseEditViewModel(DataObjectService dataObjectService, string? internalId)
    {
        _dataObjectService = dataObjectService;
        _interalId = internalId;

        if (!string.IsNullOrWhiteSpace(internalId))
        {
            PageTitle = "Ändra i kurs";
            Init();
        }
        else
        {
            PageTitle = "Ny kurs";
        }
    }

    public async void Init() => await GetCourse();
    private async Task GetCourse()
    {
        var course = await _dataObjectService.Find<CourseAva>("id", _interalId!); // Id expected to not be null

        _dbId = course._id;

        CourseNumber = course.CourseNumber;
        Title = course.Title;
        CourseType = course.CourseType;
        Days = course.Days;
        StartDate = course.StartDate;
        EndDate = course.EndDate;
    }

    [RelayCommand]
    public async Task Save()
    {
        Course course = new Course()
        {
            CourseNumber = CourseNumber,
            Title = Title,
            CourseType = CourseType,
            Days = Days,
            StartDate = StartDate,
            EndDate = EndDate
        };

        if (string.IsNullOrWhiteSpace(_dbId))
            await _dataObjectService.Add<Course>(course);
        else
            await _dataObjectService.Update<Course>(course, _dbId);
    }
}
