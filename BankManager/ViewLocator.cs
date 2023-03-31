using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using BankManager.ViewModels;

namespace BankManager;

public class ViewLocator : IDataTemplate
{
    public IControl Build(object p_data)
    {
        var name = p_data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object p_data)
    {
        return p_data is ViewModelBase;
    }
}