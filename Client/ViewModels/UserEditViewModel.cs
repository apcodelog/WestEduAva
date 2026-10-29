using System;
using System.Threading.Tasks;
using Client.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class UserEditViewModel : ViewModelBase, IEditView
{
    [ObservableProperty]
    public partial int PreviousPage { get; set; }

    [ObservableProperty]
    public partial string Title { get; set; } = ""; // TEMPORARY

    public UserEditViewModel()
    {
        PreviousPage = 0;
    }

    [RelayCommand]
    public Task Save()
    {
        throw new NotImplementedException();
    }

    public void Cancel()
    {
        throw new NotImplementedException();
    }
}
