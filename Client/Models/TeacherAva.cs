using System;
using Client.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Models.Users;

namespace Client.Models;

[INotifyPropertyChanged]
public partial class TeacherAva : Teacher, IUserAva
{
    public string _id { get; set; } = ""; // restdb standard id

    public string DetailsLines => $"Kunskapsområden: {string.Join(", ", KnowledgeList)}{Environment.NewLine}" + 
                                  $"Telefon: {Phone}{Environment.NewLine}" +
                                  $"Address: {Address}"; 

    [ObservableProperty]
    public partial bool IsExpanded { get; set; } = false;

    [RelayCommand]
    public void ToggleExpanded() => IsExpanded = !IsExpanded;
}
