using System;
using Client.ViewModels;

namespace Client.Factories;

public class PageViewFactory
{
    public ViewModelBase LoadPageViewModel<T>() where T : ViewModelBase
    {
        return typeof(T) switch
        {
            _ when typeof(T) == typeof(HomeViewModel) => new HomeViewModel(),
            _ when typeof(T) == typeof(CoursesViewModel) => new CoursesViewModel(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
