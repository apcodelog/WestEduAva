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
            ViewTypeEnum.Courses => new CoursesViewModel(new DataObjectService(ViewTypeEnum.Courses)),
            ViewTypeEnum.NewCourse => new CourseEditViewModel(new DataObjectService(ViewTypeEnum.Courses), null),
            ViewTypeEnum.Students => new UsersViewModel(ViewTypeEnum.Students),
            ViewTypeEnum.Teachers => new UsersViewModel(ViewTypeEnum.Teachers),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public ViewModelBase LoadPageViewModel(ViewTypeEnum viewType, string internalId)
    {
        return viewType switch
        {
            ViewTypeEnum.EditCourse => new CourseEditViewModel(new DataObjectService(ViewTypeEnum.Courses), internalId),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
