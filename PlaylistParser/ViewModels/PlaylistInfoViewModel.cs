using System.Collections.Generic;
using PlaylistParser.Interfaces;
using PlaylistParser.Services;

namespace PlaylistParser.ViewModels;

public class PlaylistInfoViewModel : ViewModelBase,  INavigationAware
{
    public PlaylistInfoViewModel(NavigationService navigationService) : base(navigationService)
    {
        
    }
    
    #region -- Public Properties --

    public bool IsErrorTextVisible { get; set; } = true;
    
    public string Url { get; set; }

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
}