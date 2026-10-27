using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Enums;
using Core.Models;

namespace Client.Models;

[INotifyPropertyChanged]
public partial class CourseAva : Course
{
    public string DetailsLines => $"Startdatum: {StartDate}{Environment.NewLine}" + 
                                  $"Slutdatum: {EndDate}{Environment.NewLine}" + 
                                  $"Kurstyp: {CourseType.ToSwedish()}{Environment.NewLine}" +
                                  $"Antal undervisningsdagar: {Days}";

    [ObservableProperty]
    public partial bool IsExpanded { get; set; } = false;

    [RelayCommand]
    public void ToggleExpanded() => IsExpanded = !IsExpanded;
}
