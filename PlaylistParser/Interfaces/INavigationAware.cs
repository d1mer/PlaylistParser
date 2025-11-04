using System.Collections.Generic;

namespace PlaylistParser.Interfaces;

public interface INavigationAware
{
    void OnNavigatedTo(Dictionary<string, object> parameters);
}