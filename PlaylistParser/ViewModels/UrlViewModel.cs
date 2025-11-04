using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace PlaylistParser.ViewModels;

public class UrlViewModel : ViewModelBase
{
    public UrlViewModel()
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
            
        }
    }

    #endregion
}