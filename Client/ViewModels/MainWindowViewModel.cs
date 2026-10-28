using System;
using Client.Enums;
using Client.Factories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly PageViewFactory _pageViewFactory;

    [ObservableProperty]
    public partial ViewModelBase CurrentView { get; set; } = new();

    public MainWindowViewModel(PageViewFactory pageViewFactory)
    {
        _pageViewFactory = pageViewFactory;
        GoToHome();
    }

    [RelayCommand]
    public void GoToHome() => CurrentView = _pageViewFactory.LoadPageViewModel(ViewTypeEnum.Home);
    [RelayCommand]
    public void GoToCourses() => CurrentView = _pageViewFactory.LoadPageViewModel(ViewTypeEnum.Courses);
    [RelayCommand]
    public void GoToCourseEdit(string interalId) => CurrentView = _pageViewFactory.LoadPageViewModel(ViewTypeEnum.EditCourse, interalId);
    [RelayCommand]
    public void GoToCourseNew() => CurrentView = _pageViewFactory.LoadPageViewModel(ViewTypeEnum.NewCourse);
    [RelayCommand]
    public void GoToStudents() => CurrentView = _pageViewFactory.LoadPageViewModel(ViewTypeEnum.Students);
    public void GoToTeachers() => CurrentView = _pageViewFactory.LoadPageViewModel(ViewTypeEnum.Teachers);
}
