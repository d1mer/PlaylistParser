using System.Collections.Generic;

namespace PlaylistParser.Models;

public class Playlist
{
    public string Title { get; set; } = string.Empty;
    
    public string ImageUrl { get; set; } = string.Empty;
     
    public string Description { get; set; } = string.Empty;
    
    public List<Track> Tracks { get; set; } = new();
}