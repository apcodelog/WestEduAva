using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Client.Enums;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Enums;
using Core.Models;
using Core.Models.Users;

namespace Client.ViewModels;

public partial class UserEditViewModel : ViewModelBase, IEditView
{
    private readonly MainWindowViewModel _mainViewModel;
    private readonly DataObjectService _dataObjectService;
    private readonly ViewTypeEnum _previousPage;
    private string? _dbId;
    private string? _internalId;

    [ObservableProperty]
    public partial string FirstName { get; set; } = "";
    [ObservableProperty]
    public partial string LastName { get; set; } = "";
    [ObservableProperty]
    public partial string SSN { get; set; } = "";
    [ObservableProperty]
    public partial string Phone { get; set; } = "";

    [ObservableProperty]
    public partial string Email { get; set; } = "";
    
    [ObservableProperty]
    public partial Address Address { get; set; } = new Address() {StreetAddress = "", PostalCode = "", Town = "", Country = ""};

    public ObservableCollection<KnowledgeTagAva> KnowledgeTags { get; set; }

    public bool ShowingStudent => _previousPage == ViewTypeEnum.Students;
    public bool ShowingTeacher => _previousPage == ViewTypeEnum.Teachers;

    public UserEditViewModel()
    {
        PageTitle = "For develop preview";
        _previousPage = ViewTypeEnum.Students;
        _mainViewModel = new MainWindowViewModel(new Factories.PageViewFactory());
        _dataObjectService = new DataObjectService(ViewTypeEnum.Students);

        KnowledgeTags = new(Enum.GetValues<KnowledgeTagEnum>().Skip(1).Select(o => new KnowledgeTagAva(o, false)));
    }

    public UserEditViewModel(MainWindowViewModel mainViewModel, ViewTypeEnum previousPage, string? internalId)
    {
        _mainViewModel = mainViewModel;
        _dataObjectService = new DataObjectService(previousPage);
        _previousPage = previousPage;
        _internalId = internalId;

        KnowledgeTags = new(Enum.GetValues<KnowledgeTagEnum>().Skip(1).Select(o => new KnowledgeTagAva(o, false)));

        if (string.IsNullOrWhiteSpace(_internalId))
        {
            PageTitle = "Ny";
        }
        else
        {
            PageTitle = "Ändra";
            Init();
        }
    }

    public async void Init() => await GetUser();
    private async Task GetUser()
    {
        if (_previousPage == ViewTypeEnum.Students)
        {
            var user = await _dataObjectService.Find<StudentAva>("id", _internalId!);

            _dbId = user._id;

            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.Phone;
            
            if (user.Address != null)
                Address = user.Address;

            SSN = user.SSN;
        }
        else if (_previousPage == ViewTypeEnum.Teachers)
        {
            var user = await _dataObjectService.Find<TeacherAva>("id", _internalId!);

            _dbId = user._id;

            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.Phone;
            
            if (user.Address != null)
                Address = user.Address;

            if (user.KnowledgeList != null)
            {
                KnowledgeTags.Clear();
                foreach (var t in Enum.GetValues<KnowledgeTagEnum>().Skip(1).Select(o => new KnowledgeTagAva(o, user.KnowledgeList.Contains(o))))
                    KnowledgeTags.Add(t);
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    private bool IsValidEmail(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        foreach (char c in input)
        {
            if (!char.IsLetterOrDigit(c) && c != '.' && c != '@')
                return false;
        }

        var split = input.Split('@');
        return split.Length == 2 && split[1].Contains('.');
    }

    [RelayCommand]
    public async Task Save()
    {
        if (!IsValidEmail(Email))
        {
            Console.WriteLine("Invalid email");
            return;
        }

        if (_previousPage == ViewTypeEnum.Students)
        {
            Student student = new Student()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Address = Address,

                SSN = SSN
            };
            await SaveAndReturn<Student>(student);
        }
        else if (_previousPage == ViewTypeEnum.Teachers)
        {
            Teacher teacher = new Teacher()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Address = Address,
                KnowledgeList = KnowledgeTags.Where(o => o.IsChecked).Select(o => o.Tag).ToList()
            };

            await SaveAndReturn<Teacher>(teacher);
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }        
    }
    private async Task SaveAndReturn<T>(T data) where T : class
    {
        if (string.IsNullOrWhiteSpace(_dbId))
            await _dataObjectService.Add<T>(data);
        else
            await _dataObjectService.Update<T>(data, _dbId);

        _mainViewModel.GoToView(_previousPage);
    }

    [RelayCommand]
    public void Cancel()
    {
        _mainViewModel.GoToView(_previousPage);
    }
}
