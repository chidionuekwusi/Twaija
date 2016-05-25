using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TwaijaComposite.Modules.Common
{
    public interface IPictureTray
    {
         bool IsEmpty { get; }
         Uri Picture { get; set; }
         void EmptyTray();
    }
    public class PictureTrayImp:IPictureTray,INotifyPropertyChanged
    {
        private Uri _picture;
        public Uri Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                if (_picture != value)
                {
                    _picture = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Picture"));
                    if (value != null)
                    {
                        isEmpty = false;
                    }
                    else
                    {
                        IsEmpty = true;
                    }
                }
            }
        }
        private bool isEmpty=true;
        public bool IsEmpty
        {
            get { return isEmpty; }
            private set { isEmpty = value; }
        }

        public void EmptyTray()
        {
            IsEmpty = true;
            Picture = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
