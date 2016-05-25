using System;
using System.ComponentModel;
namespace TwaijaComposite.Modules.Common.ViewModels
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public abstract class ViewModelBase : INotifyPropertyChanged,IDisposable
    {
        private static long IDGen;
        public ViewModelBase()
        {
            UniqueID=IDGen++;
        }
        public long UniqueID { get; set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
            
        }


        #endregion
        #region IDisposable Members
        public virtual void Dispose()
        {
          
            IsDisposed = true;
          
        }

        #endregion
        private bool isdisposed;
        public bool IsDisposed
        {
            get
            {               
                    return isdisposed;
               
            }
            set
            {               
                    isdisposed = value;
               
            }
        }
    }


}