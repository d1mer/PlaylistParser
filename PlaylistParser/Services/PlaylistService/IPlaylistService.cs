using System.Threading.Tasks;
using PlaylistParser.Models;

namespace PlaylistParser.Services.PlaylistService;

public interface IPlaylistService
{
    Task<Playlist?> GetPlaylistAsync(string url);
}