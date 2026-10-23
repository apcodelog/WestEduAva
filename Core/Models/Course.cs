using System;
using System.Data.Common;
using Core.Enums;
using Core.Interfaces;
using Core.Models.ConsoleHelpers;

namespace Core.Models;

public class Course : IDataObject
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CourseNumber { get; set; } = "";
    public string Title { get; set; } = "";
    public CourseTypeEnum CourseType { get; set; }
    public int Days { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public void SetUserInfo()
    {
        CourseNumber = ConsoleInputHelper.GetString("Skriv kursnummer");
        Title = ConsoleInputHelper.GetString("Skriv kurstitel");
        CourseType = ConsoleInputHelper.GetEnum<CourseTypeEnum>("Välj kurstyp");
        Days = ConsoleInputHelper.GetInt("Skriv antal dagar i kursen");
        StartDate = ConsoleInputHelper.GetDate("Skriv startdatum");
        EndDate = ConsoleInputHelper.GetDate("Skriv slutdatum");
    }
    
    public List<string> GetInfoCells()
    {
        return [
            $"Kursnr.: {CourseNumber}",
            $"Titel: {Title}",
            $"Kurstyp: {CourseType.ToSwedish()}",
            $"Längd: {Days} dagar",
            $"Startdatum: {StartDate}",
            $"Slutdatum: {EndDate}"
        ];
    }

    public override string ToString()
    {
        return $"Kursnr.: {CourseNumber} Titel: {Title} Kurstyp: {CourseType.ToSwedish()} Längd: {Days} dagar Startdatum: {StartDate} Slutdatum: {EndDate}";
    }

    public string ToStringShort()
    {
        return $"{CourseNumber}-{Title}";
    }
}
