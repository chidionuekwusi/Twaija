using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TwaijaComposite.Modules.Common
{
    public class PropertyChangedManager
    {
        object owner;
        public PropertyChangeReaction this[string propertyName]
        {
            get
            {
                if (!PropertyMap.ContainsKey(propertyName))
                {
                    PropertyChangeReaction nameAndAction = new PropertyChangeReaction("DummyPropertyName");
                    PropertyMap.Add(propertyName, nameAndAction);
                    return nameAndAction;
                }
                else
                {
                    return PropertyMap[propertyName];
                }
            }
            set
            {
                PropertyMap[propertyName] = value;
            }
        }
       public  PropertyChangedManager(object owner)
        {
            PropertyMap = new Dictionary<string, PropertyChangeReaction>();
            this.owner = owner;
        }
        private Dictionary<string, PropertyChangeReaction> PropertyMap;
        public void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("PropertyChangedEventArgs passed to property Manager was null");
            }
            if (sender == null)
            {
               throw new ArgumentNullException("Sender object passed to Property Manager was null");
            }
            if (PropertyMap[e.PropertyName] != null)
            {

                string ChangedPropertyName = e.PropertyName;
                string RecievingPropertyName = PropertyMap[ChangedPropertyName].PropertyName;
                Action action = PropertyMap[ChangedPropertyName].ChangeReaction;
                var changedinfo = sender.GetType().GetProperty(ChangedPropertyName);
                if (changedinfo == null)
                {
                    throw new ArgumentNullException("Invalid Recieving Property Name,thrown from:Property Manager");
                }
                var changedvalue = changedinfo.GetValue(sender, null);
                var recieveinginfo = owner.GetType().GetProperty(RecievingPropertyName);
                if (recieveinginfo == null)
                {
                    throw new  ArgumentNullException("Invalid Changed Property Name,thrown from:Property Manager");
                }
                try
                {
                    recieveinginfo.SetValue(owner, changedvalue, null);
                }
                catch (Exception r)
                {
                    throw new Exception("An Error Occurred while trying to setValue of the Recieving Property, Original exception message:" + r.Message, r);
                }
                if (action != null)
                {
                    action();
                }

            }
        }
    }
}
