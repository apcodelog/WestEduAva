using System;
using Core.Enums;

namespace Core.Models;

public static class EnumExtensions
{
    public static string ToSwedish(this CourseTypeEnum type) => type switch
    {
        CourseTypeEnum.Unknown => "Inget värde",
        CourseTypeEnum.OnSite => "På plats",
        CourseTypeEnum.Online => "Online",
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Oväntat värde")
    };
}
