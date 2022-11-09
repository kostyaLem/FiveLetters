using DevExpress.Mvvm;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FiveLetters.UI.Services;

internal sealed class DialogService : IDialogService
{
    private readonly IDictionary<Type, Type> _viewsMap;

    public DialogService(IDictionary<Type, Type> viewsMap)
    {
        _viewsMap = viewsMap;
    }

    public void ShowDialog<T>() where T : ViewModelBase
    {
        if (_viewsMap.TryGetValue(typeof(T), out var viewType))
        {
            var view = (Window)App.ServiceProvider.GetRequiredService(viewType);
            view.ShowDialog();
            return;                        
        }

        throw new ArgumentException($"Can't find view for Type {typeof(T).Name}");
    }
}
