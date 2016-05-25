using System;
using TwaijaComposite.Modules.Common.Interfaces;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TwaijaComposite.Modules.Common.DataInterfaces
{
    public interface ITwitterUser : IUser
    {
        TwitterToken Token { get; set; }
        int NOOTimeline { get; set; }
        int NOOMentions { get; set; }
        int NOODirectMessages { get; set; }
        int NOOUserTimeline { get; set; }
        int NOOSearch { get; set; }
        int ColumnRefreshTime { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
