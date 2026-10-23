using System;
using Core.Models.ConsoleHelpers;

namespace Core.Models.Users;

public class SchoolAdmin : UserBase
{
    public string SSN { get; set; } = ""; // Social Security Number
    public DateOnly HiringDate { get; set; }

    public override void SetUserInfo()
    {
        base.SetUserInfo();
        SSN = ConsoleInputHelper.GetString("Skriv personnr.");
        HiringDate = ConsoleInputHelper.GetDate("Skriv anställningsdatum");
    }

    public override List<string> GetInfoCells()
    {
        var cells = base.GetInfoCells();
        cells.Add($"Personnr.: {SSN}");
        cells.Add($"Anställningsdatum: {HiringDate}");
        return cells;
    }

    public override string ToString()
    {
        return base.ToString() + $" Personnr.: {SSN} Anställningsdatum: {HiringDate}";
    }
}
