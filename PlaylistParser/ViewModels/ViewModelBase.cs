using CommunityToolkit.Mvvm.ComponentModel;
using PlaylistParser.Services;

namespace PlaylistParser.ViewModels;

public abstract class ViewModelBase : ObservableObject
{
    protected NavigationService  NavigationService { get; }

    public ViewModelBase(NavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}