using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.DataInterfaces
{
    public class MentionEntity
    {
        string ScreenName { get; set; }
        decimal Id { get; set; }
        public MentionEntity(string name, decimal id)
        {
            ScreenName = name;
            Id = id;
        }
    }
}
