using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PlaylistParser.Services;

namespace PlaylistParser.ViewModels;

public class UrlViewModel : ViewModelBase
{
    public UrlViewModel(NavigationService navigationService) : base(navigationService)
    {
        GetInfoCommand = new RelayCommand(OnGetInfo);
    }
    
    #region -- Public properties --

    public string Url { get; set; }
    
    public ICommand GetInfoCommand { get; }

    #endregion

    #region -- Private helpers --

    private void OnGetInfo()
    {
        if (!string.IsNullOrWhiteSpace(Url))
        {
            NavigationService.NavigateTo<PlaylistInfoViewModel>(new Dictionary<string, object>
            {
                {"url", Url}
            });
        }
    }

    #endregion
}