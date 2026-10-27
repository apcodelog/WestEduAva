using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ViewModelBase CurrentView { get; set; }

    public MainWindowViewModel()
    {
        CurrentView = new CoursesViewModel();
    }
}
