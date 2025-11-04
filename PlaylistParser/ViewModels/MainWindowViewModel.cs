using CommunityToolkit.Mvvm.ComponentModel;
using PlaylistParser.Services;

namespace PlaylistParser.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private object currentViewModel;
    public MainWindowViewModel(NavigationService  navigationService) : base(navigationService)
    {
        NavigationService.Initialize(vm => CurrentViewModel = vm);
        NavigationService.NavigateTo<UrlViewModel>();
    }
}