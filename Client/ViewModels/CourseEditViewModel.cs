using System;
using Client.Interfaces;
using Client.Services;
using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Core.Enums;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Core.Models;

namespace Client.ViewModels;

public partial class CourseEditViewModel : ViewModelBase
{
    private readonly (IDataObjectService<Course> normal, IDataObjectService<CourseAva> ava) _dataObjectService;
    private string? _dbId;
    private string? _interalId;

    [ObservableProperty]
    public partial string CourseNumber { get; set; } = "";
    [ObservableProperty]
    public partial string Title { get; set; } = "";
    [ObservableProperty]
    public partial CourseTypeEnum CourseType { get; set; } = CourseTypeEnum.OnSite;
    [ObservableProperty]
    public partial int Days { get; set; } = 20;
    [ObservableProperty]
    public partial DateTime StartDate { get; set; } = new DateTime(2027, 01, 01);
    [ObservableProperty]
    public partial DateTime EndDate { get; set; } = new DateTime(2027, 01, 31);


    public CourseEditViewModel()
    {
        _dataObjectService = (new DataObjectService<Course>(), new DataObjectService<CourseAva>());
        PageTitle = "Ny / Ändra kurs - For preview during development";
    }

    public CourseEditViewModel(IDataObjectService<Course> dataObjectService, DataObjectService<CourseAva> dataObjectServiceAva, string? internalId)
    {
        _dataObjectService = (dataObjectService, dataObjectServiceAva);
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
        var course = await _dataObjectService.ava.Find("id", _interalId!); // Id expected to not be null

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
            await _dataObjectService.normal.Add(course);
        else
            await _dataObjectService.normal.Update(course, _dbId);
    }
}
