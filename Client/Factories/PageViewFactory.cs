using System;
using Client.Enums;
using Client.Models;
using Client.Services;
using Client.ViewModels;
using Core.Models;

namespace Client.Factories;

public class PageViewFactory
{
    public ViewModelBase LoadPageViewModel(MainWindowViewModel mainViewModel, ViewTypeEnum viewType)
    {
        return viewType switch
        {
            ViewTypeEnum.Home => new HomeViewModel(),

            ViewTypeEnum.Courses => new CoursesViewModel(new DataObjectService(ViewTypeEnum.Courses)),
            ViewTypeEnum.NewCourse => new CourseEditViewModel(mainViewModel, new DataObjectService(ViewTypeEnum.Courses), null),

            ViewTypeEnum.Students => new UsersViewModel(mainViewModel, ViewTypeEnum.Students),
            ViewTypeEnum.NewStudent => new UserEditViewModel(mainViewModel, ViewTypeEnum.Students, null),

            ViewTypeEnum.Teachers => new UsersViewModel(mainViewModel, ViewTypeEnum.Teachers),
            ViewTypeEnum.NewTeacher => new UserEditViewModel(mainViewModel, ViewTypeEnum.Teachers, null),

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public ViewModelBase LoadPageViewModel(MainWindowViewModel mainViewModel, ViewTypeEnum viewType, string internalId)
    {
        return viewType switch
        {
            ViewTypeEnum.EditCourse => new CourseEditViewModel(mainViewModel, new DataObjectService(ViewTypeEnum.Courses), internalId),

            ViewTypeEnum.EditStudent => new UserEditViewModel(mainViewModel, ViewTypeEnum.Students, internalId),

            ViewTypeEnum.EditTeacher => new UserEditViewModel(mainViewModel, ViewTypeEnum.Teachers, internalId),

            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
