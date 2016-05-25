using System.Windows.Input;
using System;
namespace TwaijaComposite.Modules.Authentication
{
    public interface IAuthenticateStrategy
    {
        /// <summary>
        /// Display name for the Strategy
        /// </summary>
        string Name { get; }
        /// <summary>
        /// This is the current address of the Webbrowser;
        /// </summary>
        Uri Address { get; set; }
        /// <summary>
        /// This is the Command invoked when the webbrowser navigates to a different address
        /// </summary>
        ICommand BrowserNavigationReaction { get; }
        /// <summary>
        /// This event signals a successful Authentication
        /// </summary>
        event EventHandler Close;
        /// <summary>
        /// This event tells the authentication view that it should bring up the Browser
        /// </summary>
        event Action RequestBrowserTransition;
        /// <summary>
        /// This explains how authentication takes place
        /// </summary>
        string Hint { get; }
        /// <summary>
        /// Image.
        /// </summary>
        string ThumbnailImage { get; }
    }
}
