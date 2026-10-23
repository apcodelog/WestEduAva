using System;
using Core.Interfaces;
using Core.Models.ConsoleHelpers;
using Core.Models.Managers;

namespace Core.Models.Users;

public class Student : UserBase
{
    public string SSN { get; set; } = ""; // Social Security Number
    public List<Course> Courses { get; set; } = [];

    public override void SetUserInfo()
    {
        base.SetUserInfo();
        SSN = ConsoleInputHelper.GetString("Skriv personnr.");

        var courses = new Storage<Course>().Read(new DataObjectManager<Course>().GetDataGroup().Path);
        Courses = ConsoleInputHelper.GetMultiSelect(
            "Lägg till kurser", 
            courses,
            courses.Select(o => o.ToStringShort()).ToList()
            );
    }

    public override List<string> GetInfoCells()
    {
        var cells = base.GetInfoCells();
        cells.Add($"Personnr.: {SSN}");
        cells.Add($"Kurser: {string.Join(", ", Courses.Select(o => o.ToStringShort()))}");
        return cells;
    }

    public override string ToString()
    {
        return base.ToString() + $" Personnr.: {SSN} Kurser: {string.Join(", ", Courses.Select(o => o.ToStringShort()))}";
    }
}
