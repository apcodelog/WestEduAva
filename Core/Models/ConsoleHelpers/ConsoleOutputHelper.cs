using System;
using Core.Interfaces;

namespace Core.Models.ConsoleHelpers;

public static class ConsoleOutputHelper
{
    public static void WriteBreak()
    {
        Console.WriteLine("---------------------------------------------------------------------");
    }

    public static void WriteList<T>(string listTitle, List<T> objs) where T : IDataObject
    {
        Console.WriteLine(listTitle);
        WriteBreak();

        var maxCellLengths = new List<int>();
        for (int i = 0; i < objs[0].GetInfoCells().Count(); i++)
        {
            maxCellLengths.Add(objs.Max(o => o.GetInfoCells()[i].Length) + 2);
        }

        foreach (var o in objs)
        {
            var cells = o.GetInfoCells();
            for (int i = 0; i < cells.Count(); i++)
                Console.Write(cells[i].PadRight(maxCellLengths[i]));

            Console.WriteLine();
        }
    }

    
}
