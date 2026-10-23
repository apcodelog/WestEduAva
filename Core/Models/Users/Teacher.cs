using System;
using Core.Enums;
using Core.Models.ConsoleHelpers;
using Core.Models.Managers;

namespace Core.Models.Users;

public class Teacher : UserBase
{
    public List<KnowledgeTagEnum> KnowledgeList { get; set; } = [];
    public List<Course> Courses { get; set; } = [];

    public override void SetUserInfo()
    {
        base.SetUserInfo();

        var types = Enum.GetValues<KnowledgeTagEnum>().ToList();
        var typeNames = Enum.GetNames<KnowledgeTagEnum>().ToList();
        KnowledgeList = ConsoleInputHelper.GetMultiSelect(
            "Lägg till kunskapsområden",
            types,
            typeNames);

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
        cells.Add($"Kunskapsområden: {string.Join(", ", KnowledgeList)}");
        cells.Add($"Kurser: {string.Join(", ", Courses.Select(o => o.ToStringShort()))}");
        return cells;
    }

    public override string ToString()
    {
        return base.ToString() + $" Kunskapsområden: {string.Join(", ", KnowledgeList)} Kurser: {string.Join(", ", Courses.Select(o => o.ToStringShort()))}";
    }
}
