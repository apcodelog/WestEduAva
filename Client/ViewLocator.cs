using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Client.ViewModels;

namespace Client;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null) return null;

        var dataType = data.GetType();

        string viewName = data.GetType().FullName!;
        if (dataType.IsGenericType)
        {
            viewName = viewName.Substring(0, viewName.IndexOf('`'));
        }

        viewName = viewName.Replace("ViewModel", "View", StringComparison.InvariantCulture);
        var view = Type.GetType(viewName);

        if (view is null) return null;

        var control = (Control)Activator.CreateInstance(view)!;
        control.DataContext = data;

        return control;
    }

    public bool Match(object? data) => data is ViewModelBase;
}
