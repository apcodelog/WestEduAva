using System;
using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Models;
using Core.Models;

namespace Client.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ObservableCollection<CourseAva> Courses { get; set; } = [];

    public CoursesViewModel()
    {
        PageTitle = "Kurser - From Dev Preview Constructor";

        var list = new Storage<CourseAva>().Read(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "TestCourseData.json"));
        Courses = new ObservableCollection<CourseAva>(list);
    }
}
