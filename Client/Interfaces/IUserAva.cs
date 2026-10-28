using System;
using CommunityToolkit.Mvvm.Input;

namespace Client.Interfaces;

public interface IUserAva
{
    public string Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    
    
    
    public string _id { get; set; }

    public string DetailsLines { get; }

    public bool IsExpanded { get; set; }

    IRelayCommand ToggleExpandedCommand { get; }
}
