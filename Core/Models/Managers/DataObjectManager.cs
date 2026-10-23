using System;
using Core.Interfaces;
using Core.Models.ConsoleHelpers;
using Core.Models.Users;

namespace Core.Models.Managers;

public class DataObjectManager<T> where T : IDataObject, new()
{
    private readonly Dictionary<Type, DataGroup> _dataGroupMap = new()
    {
        {typeof(Course), new DataGroup(
            Path.Combine(Environment.CurrentDirectory, "Data", "courses.json"),
            "Kurs",
            "Kursen",
            "Kurser"
        )},
        {typeof(Student), new DataGroup(
            Path.Combine(Environment.CurrentDirectory, "Data", "students.json"),
            "Student",
            "Studenten",
            "Studenter"
        )},
        {typeof(Teacher), new DataGroup(
            Path.Combine(Environment.CurrentDirectory, "Data", "teachers.json"),
            "Lärare",
            "Läraren",
            "Lärarer"
        )},
        {typeof(CourseLeader), new DataGroup(
            Path.Combine(Environment.CurrentDirectory, "Data", "courseLeaders.json"),
            "Kursledare",
            "Kursledaren",
            "Kursledare"
        )},
        {typeof(SchoolAdmin), new DataGroup(
            Path.Combine(Environment.CurrentDirectory, "Data", "schoolAdmins.json"),
            "Administratör",
            "Administratören",
            "Administratörer"
        )}
    };

    public DataGroup GetDataGroup()
    {
        return _dataGroupMap[typeof(T)];
    }

    public void AddObject() 
    {
        var dataGroup = _dataGroupMap[typeof(T)];
        
        try
        {
            Console.WriteLine();
            Console.WriteLine($"Lägg till ny {dataGroup.TitleIndefinite}");

            T user = new T();
            user.SetUserInfo();

            var storage = new Storage<T>();
            var all = storage.Read(dataGroup.Path);
            all.Add(user);
            storage.Write(dataGroup.Path, all);

            Console.WriteLine($"{dataGroup.TitleDefinite} har sparats");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{dataGroup.TitleDefinite} kunde inte sparas. Felmeddelande: {ex.Message}");
        }
        
        Console.WriteLine();
        ConsoleInputHelper.WaitForAnyKeyToReturnToMenu();
    }

    public void ShowList()
    {
        var dataGroup = _dataGroupMap[typeof(T)];
        
        try
        {
            Console.WriteLine();

            List<T> objs = new Storage<T>().Read(dataGroup.Path);
            
            if (objs.Count == 0)
                Console.WriteLine($"Listan med {dataGroup.TitlePlural} är tom.");
            else
                ConsoleOutputHelper.WriteList(dataGroup.TitlePlural, objs);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Listan med {dataGroup.TitlePlural} kunde inte visas. Felmeddelande: {ex.Message}");
        }

        Console.WriteLine();
        ConsoleInputHelper.WaitForAnyKeyToReturnToMenu();
    }
}
