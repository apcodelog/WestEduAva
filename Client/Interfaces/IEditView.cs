using System;
using System.Threading.Tasks;

namespace Client.Interfaces;

public interface IEditView
{
    Task Save();
    void Cancel();
}
