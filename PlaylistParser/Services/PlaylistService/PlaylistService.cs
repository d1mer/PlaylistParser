using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PlaylistParser.Models;

namespace PlaylistParser.Services.PlaylistService;

public class PlaylistService : IPlaylistService
{
    #region -- IPlaylistService implementation --

    public async Task<Playlist?> GetPlaylistAsync(string url)
    {
        Playlist? playlist = null;
        IWebDriver? driver = null;

        try
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument(
                "user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");
            
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);

            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(url);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d =>
            {
                var pageSource = d.PageSource;
                return !pageSource.Contains("Tuning in") &&
                       (pageSource.Contains("music-track") || pageSource.Contains("trackList"));
            });

            await Task.Delay(2000);

            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            await Task.Delay(1000);

            var html = driver.PageSource;
            
            // DELETE THIS CODE
            var debugPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 
                "amazon_music_debug.html"
            );
            await System.IO.File.WriteAllTextAsync(debugPath, html);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            playlist = ParseHtmlDocumentAsync(doc);
        }
        catch (Exception e)
        {
            Console.WriteLine($"GetPlaylistAsync: {e.Message}");
        }
        finally
        {
            driver?.Quit();
            driver?.Dispose();
        }

        return playlist;
    }

    #endregion

    #region -- Private helpers --

    private Playlist ParseHtmlDocumentAsync(HtmlDocument doc)
    {
        Playlist playlist = new();

        try
        {
            var headerNode = doc.DocumentNode.SelectSingleNode("//music-detail-header[@headline]");

            if (headerNode != null)
            {
                playlist.Title = headerNode.GetAttributeValue("headline", "");
                playlist.Description = headerNode.GetAttributeValue("secondary-text", "");
                playlist.ImageUrl = headerNode.GetAttributeValue("image-src", "");
            }
            
            var trackNodes = doc.DocumentNode.SelectNodes("//music-image-row[@primary-text]");

            if (trackNodes?.Count > 0)
            {
                playlist.Tracks = new List<Track>();

                foreach (var trackNode in trackNodes)
                {
                    var track = new Track
                    {
                        Name = trackNode.GetAttributeValue("primary-text", ""),
                        Artist = trackNode.GetAttributeValue("secondary-text-1", ""),
                        Album = trackNode.GetAttributeValue("secondary-text-2", ""),
                        ImageUrl = trackNode.GetAttributeValue("image-src", ""),
                    };
                    
                    var durationNode =  trackNode.SelectSingleNode(".//div[@class='col4']//span");

                    if (durationNode != null)
                    {
                        track.Duration = durationNode.InnerText.Trim();
                    }

                    if (!string.IsNullOrEmpty(track.Name))
                    {
                        playlist.Tracks.Add(track);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"ParseHtmlDocumentAsync: {e.Message}");
        }
        
        return playlist;
    }

    #endregion
}