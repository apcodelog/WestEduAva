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
    public void GoToView(ViewTypeEnum viewType) => CurrentView = _pageViewFactory.LoadPageViewModel(this, viewType);

    [RelayCommand]
    public void GoToHome() => CurrentView = _pageViewFactory.LoadPageViewModel(this, ViewTypeEnum.Home);
    [RelayCommand]
    public void GoToCourses() => CurrentView = _pageViewFactory.LoadPageViewModel(this, ViewTypeEnum.Courses);
    [RelayCommand]
    public void GoToCourseEdit(string interalId) => CurrentView = _pageViewFactory.LoadPageViewModel(this, ViewTypeEnum.EditCourse, interalId);
    [RelayCommand]
    public void GoToCourseNew() => CurrentView = _pageViewFactory.LoadPageViewModel(this, ViewTypeEnum.NewCourse);
    [RelayCommand]
    public void GoToStudents() => CurrentView = _pageViewFactory.LoadPageViewModel(this, ViewTypeEnum.Students);
    [RelayCommand]
    public void GoToTeachers() => CurrentView = _pageViewFactory.LoadPageViewModel(this, ViewTypeEnum.Teachers);
}
