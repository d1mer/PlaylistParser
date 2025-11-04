using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using PlaylistParser.Interfaces;
using PlaylistParser.ViewModels;

namespace PlaylistParser.Services;

public class NavigationService
{
    private readonly Stack<object> _navigationStack = new();
    private Action<object> _setCurrentViewModel;

    public void Initialize(Action<object> setCurrentViewModel)
    {
        _setCurrentViewModel = setCurrentViewModel;
    }

    public void NavigateTo<TViewModel>(Dictionary<string, object> parameters = null) where TViewModel : ViewModelBase
    {
        var viewModel = App.Services.GetRequiredService<TViewModel>();

        if (viewModel is INavigationAware navigationAware)
        {
            navigationAware.OnNavigatedTo(parameters);
        }
        
        _navigationStack.Push(viewModel);
        _setCurrentViewModel?.Invoke(viewModel);
    }

    public void GoBack()
    {
        if (_navigationStack.Count > 0)
        {
            _navigationStack.Pop();
        }

        if (_navigationStack.Count > 0)
        {
            var prevViewModel = _navigationStack.Peek();
            _setCurrentViewModel?.Invoke(prevViewModel);
        }
    }
}