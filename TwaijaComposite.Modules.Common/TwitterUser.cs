using System;
using System.Xml.Serialization;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Microsoft.Practices.Unity;
using System.Reflection;
using TwaijaComposite.Modules.Common.Services;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Interfaces;
using System.Runtime.Serialization;

namespace TwaijaComposite.Modules.Common
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class TwitterUser:ITwitterUser
    {

       public TwitterUser()
       { 
          var list= new List<string>();
          list.Add(TwaijaComposite.Modules.Common.Resources.ClipboardOperationKeys.ReplyToKey);
          list.Add(TwaijaComposite.Modules.Common.Resources.ClipboardOperationKeys.TwitterDirectMessageKey);
          PossiblePostalOperations = list;       
       }

        public Common.TwitterToken Token
        {
            get;
            set;
        }

        int NOOT = 20;
        public int NOOTimeline
        {
            get { return NOOT; }
            set { NOOT = value; }
        }

        int NOOM = 20;
        public int NOOMentions
        {
            get { return NOOM; }
            set { NOOM = value; }
        }
 
        int NOOD = 20;
        public int NOODirectMessages
        {
            get { return NOOD; }
            set { NOOD = value; }
        }

        int NOOU=20;
        public int NOOUserTimeline
        {
            get { return NOOU; }
            set { NOOU = value; }
        }

        int NOOS = 20;
        public int NOOSearch
        {
            get { return NOOS; }
            set { NOOS = value; }
        }
        private int _rf = 60000;

        public int ColumnRefreshTime
        {
            get { return _rf; }
            set
            {
                _rf = value;
                OnPropertyChanged("ColumnRefreshTime");
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
#if !SILVERLIGHT
        [field:NonSerialized]
#endif
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged = (p, v) => { };
    
        public string ScreenName
        {
            get;
            set;
        }
        private string _postalServiceKey = ServiceKeys.DefaultTwitterPostalServiceKey;

        public string DefaultPostalServiceKey
        {
            get { return _postalServiceKey; }
            set{_postalServiceKey=value;}
        }

        private string _pictureServiceKey = ServiceKeys.DefaultTwitterPictureServiceKey;

        public string DefaultPictureServiceKey
        {
            get { return _pictureServiceKey; }
            set { _pictureServiceKey = value; }
        }

        public IList<string> PossiblePostalOperations
        {
            get;
            private set;
        }

        private Uri defaultimage = null;
        public Uri DisplayImage
        {
            get
            {
                return defaultimage;
            }
            set
            {
                defaultimage = value;
            }
        }

        public IUserMemento CreateUserMemento()
        {
            return new TwitterUserMemento(this);
        }
        /*Twitter User Memento class ..This class is used to save user data in the option file*/
#if !SILVERLIGHT
    [Serializable]
#else
    [DataContract]
#endif
        public class TwitterUserMemento : IUserMemento
        {
#if SILVERLIGHT
        [DataMember]
#endif
            public Common.TwitterToken Token
            {
                get;
                set;
            }
#if SILVERLIGHT
        [DataMember]
#endif
            public int NOOTimeline
            {
                get;
                set;
            }
#if SILVERLIGHT
        [DataMember]
#endif
           public int NOOMentions
           {
               get;
               set;
           }
#if SILVERLIGHT
        [DataMember]
#endif
           public int NOODirectMessages
           {
               get;
               set;
           }
#if SILVERLIGHT
        [DataMember]
#endif
           public int NOOUserTimeline
           {
               get;
               set;
           }
#if SILVERLIGHT
        [DataMember]
#endif
           public int NOOSearch
           {
               get;
               set;
           }
#if SILVERLIGHT
        [DataMember]
#endif
           public int ColumnRefreshTime
           {
               get;
               set;
           }

#if SILVERLIGHT
        [DataMember]
#endif
            public string ScreenName
            {
                get;
                set;
            }

#if SILVERLIGHT
        [DataMember]
#endif
            public string DefaultPostalServiceKey
            {
                get;
                set;
            }
#if SILVERLIGHT
        [DataMember]
#endif
            public string DefaultPictureServiceKey
            {
                get;
                set;
            }

#if SILVERLIGHT
        [DataMember]
#endif
            public IList<string> PossiblePostalOperations
            {
                get;
                set;
            }
#if SILVERLIGHT
        [DataMember]
#endif
            public Uri DisplayImage
            {
                get;
                set;
            }
            public TwitterUserMemento(ITwitterUser user)
            {
                if (user == null)
                {
                    throw new ArgumentNullException("User passed to usermemento is null");
                }
                NOOUserTimeline=user.NOOUserTimeline;
                NOOTimeline=user.NOOTimeline;
                NOOSearch=user.NOOSearch;
                NOOMentions=user.NOOMentions;
                NOODirectMessages=user.NOODirectMessages;
                ColumnRefreshTime=user.ColumnRefreshTime;
                DefaultPictureServiceKey=user.DefaultPictureServiceKey;
                DefaultPostalServiceKey=user.DefaultPostalServiceKey;
                DisplayImage=user.DisplayImage;
                ScreenName=user.ScreenName;
                Token=user.Token;
                PossiblePostalOperations=user.PossiblePostalOperations ;
            }
            public IUser CreateUser()
            {
                return new TwitterUser() {NOOUserTimeline=this.NOOUserTimeline,NOOTimeline=this.NOOTimeline, NOOSearch=this.NOOSearch, NOOMentions=this.NOOMentions, NOODirectMessages=this.NOODirectMessages, ColumnRefreshTime=this.ColumnRefreshTime, DefaultPictureServiceKey=this.DefaultPictureServiceKey,DefaultPostalServiceKey=this.DefaultPostalServiceKey, DisplayImage=this.DisplayImage, ScreenName=this.ScreenName, Token=this.Token, PossiblePostalOperations=this.PossiblePostalOperations  };
            }
        }

    }
}






//System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
//{
//    return null;
//}

//void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
// {

//     reader.ReadToFollowing("ScreenName");
//   // reader.MoveToElement();
//   //reader.ReadStartElement("TwitterUser");
//   ScreenName= reader.ReadElementContentAsString();
//   reader.ReadToFollowing("IPictureService");
//   var type = reader.GetAttribute("Type");
//   reader.ReadStartElement("IPictureService");
//   PictureService=new XmlSerializer(Type.GetType(type)).Deserialize(reader) as TwaijaComposite.Modules.Common.Interfaces.IPictureService;
//   reader.ReadEndElement();
//}

//void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
//{
//    writer.WriteStartElement("ScreenName");
//    writer.WriteValue(ScreenName);
//    writer.WriteEndElement();
//    writer.WriteStartElement("IPictureService");
//    writer.WriteAttributeString("Type", PictureService.GetType().AssemblyQualifiedName);
//    new XmlSerializer(PictureService.GetType()).Serialize(writer, PictureService);
//    writer.WriteEndElement();
//}


//public Common.Interfaces.IPostMessageService PostalService
//{
//    get;
//    private set;
//}

























































































