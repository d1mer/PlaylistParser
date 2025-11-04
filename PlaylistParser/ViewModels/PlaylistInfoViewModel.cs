using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PlaylistParser.Interfaces;
using PlaylistParser.Services;

namespace PlaylistParser.ViewModels;

public class PlaylistInfoViewModel : ViewModelBase,  INavigationAware
{
    public PlaylistInfoViewModel(NavigationService navigationService) : base(navigationService)
    {
        GoBackCommand = new RelayCommand(OnGoBack);
    }
    

    #region -- Public Properties --

    public bool IsErrorTextVisible { get; set; } = false;
    
    public bool IsLoaderVisible { get; set; } = true;
    
    public string Url { get; set; }
    
    public ICommand GoBackCommand { get; }

    #endregion
    
    #region -- INavigationAware implementations--
    
    public void OnNavigatedTo(Dictionary<string, object> parameters)
    {
        if (parameters != null && parameters.TryGetValue("url", out object url))
        {
            Url = url.ToString();
        }
    }

    #endregion
    
    #region -- Private helpers --

    private void OnGoBack()
    {
        NavigationService.GoBack();
    }
    
    #endregion
}