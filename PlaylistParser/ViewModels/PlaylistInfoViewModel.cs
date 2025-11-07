using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlaylistParser.Interfaces;
using PlaylistParser.Services;
using PlaylistParser.Services.PlaylistService;

namespace PlaylistParser.ViewModels;

public class PlaylistInfoViewModel : ViewModelBase,  INavigationAware
{
    private readonly IPlaylistService  _playlistService;
    public PlaylistInfoViewModel(NavigationService navigationService, IPlaylistService playlistService) : base(navigationService)
    {
        _playlistService = playlistService;
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
            LoadPlaylist();
        }
        else
        {
            ShowDataError();
        }
    }

    #endregion
    
    #region -- Private helpers --

    private void OnGoBack()
    {
        NavigationService.GoBack();
    }

    private void ShowDataError()
    {
        IsLoaderVisible = false;
        IsErrorTextVisible = true;

        OnPropertyChanged(nameof(IsLoaderVisible));
        OnPropertyChanged(nameof(IsErrorTextVisible));
    }

    private async Task LoadPlaylist()
    {
        await _playlistService.GetPlaylistAsync(Url);
        ShowDataError();
    }
    
    #endregion
}