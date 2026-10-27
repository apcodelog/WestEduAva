using System;
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
        GoToCourseEdit();
    }

    [RelayCommand]
    public void GoToHome() => CurrentView = _pageViewFactory.LoadPageViewModel<HomeViewModel>();
    [RelayCommand]
    public void GoToCourses() => CurrentView = _pageViewFactory.LoadPageViewModel<CoursesViewModel>();
    [RelayCommand]
    public void GoToCourseEdit() => CurrentView = _pageViewFactory.LoadPageViewModel<CourseEditViewModel>();
}
