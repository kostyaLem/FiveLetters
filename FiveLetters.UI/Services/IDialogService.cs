using DevExpress.Mvvm;
using HandyControl.Controls;
using System;
using System.Collections.Generic;

namespace FiveLetters.UI.Services;

internal interface IDialogService
{
    public void ShowDialog<T>() where T : ViewModelBase;
}

internal sealed class DialogService : IDialogService
{
    private readonly IDictionary<Type, Window> _viewsMap;

    public DialogService(IDictionary<Type, Window> viewsMap)
    {
        _viewsMap = viewsMap;
    }

    public void ShowDialog<T>() where T : ViewModelBase
    {
        if (_viewsMap.TryGetValue(typeof(T), out var view))
        {
            view.ShowDialog();
            return;                        
        }

        throw new ArgumentException($"Can't find view for Type {typeof(T).Name}");
    }
}
