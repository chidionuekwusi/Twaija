using System;

namespace TwaijaComposite.Modules.Common
{
    public class PropertyChangeReaction
    {
        public PropertyChangeReaction(string propertyName, Action reaction = null)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("You Must Provide a valid PropertyName");
            }
            PropertyName = propertyName;
            ChangeReaction = reaction;
        }
        public string PropertyName { get; set; }
        public Action ChangeReaction { get; set; }
    }
}
