using System;

namespace Core.Interfaces;

public interface IDataObject
{
    void SetUserInfo();

    List<string> GetInfoCells();
}
