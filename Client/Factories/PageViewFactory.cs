using System;
using Client.Enums;
using Client.Models;
using Client.Services;
using Client.ViewModels;
using Core.Models;

namespace Client.Factories;

public class PageViewFactory
{
    public ViewModelBase LoadPageViewModel(ViewTypeEnum viewType)
    {
        return viewType switch
        {
            ViewTypeEnum.Home => new HomeViewModel(),
            ViewTypeEnum.Courses => new CoursesViewModel(new DataObjectService<CourseAva>()),
            ViewTypeEnum.NewCourse => new CourseEditViewModel(new DataObjectService<Course>(), null),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public ViewModelBase LoadPageViewModel(ViewTypeEnum viewType, string internalId)
    {
        return viewType switch
        {
            ViewTypeEnum.EditCourse => new CourseEditViewModel(new DataObjectService<Course>(), internalId),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
