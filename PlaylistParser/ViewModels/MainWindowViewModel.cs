using CommunityToolkit.Mvvm.ComponentModel;
using PlaylistParser.Views;

namespace PlaylistParser.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private object currentView;
    public MainWindowViewModel()
    {
        var urlViewModel = new UrlViewModel();
        CurrentView = new UrlView {DataContext = urlViewModel};
    }
}