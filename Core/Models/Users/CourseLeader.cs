using System;
using Core.Models.ConsoleHelpers;
using Core.Models.Managers;

namespace Core.Models.Users;

public class CourseLeader : Student
{
    public DateOnly HiringDate { get; set; }

    public override void SetUserInfo()
    {
        base.SetUserInfo();
        HiringDate = ConsoleInputHelper.GetDate("Skriv anställningsdatum");
    }

    public override List<string> GetInfoCells()
    {
        var cells = base.GetInfoCells();
        cells.Add($"Anställningsdatum: {HiringDate}");
        return cells;
    }

    public override string ToString()
    {
        return base.ToString() + $" Anställningsdatum: {HiringDate}";
    }
}
