using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
#if !SILVERLIGHT
using System.Runtime.Serialization.Formatters.Binary;
#endif
//using Twitterizer;
//using System.Windows.Input;
using System.Collections.ObjectModel;
#if !SILVERLIGHT
using System.Media;
#endif
using System.Security.Cryptography;
using System.Xml.Serialization;
//using System.Windows.Media;
using System.ComponentModel;
using System.Collections;
using System.Threading;
using System.Reflection;
//using System.Windows;
namespace TwaijaComposite.Modules.Common.Services
{
    public static class Utilities
    {
        static Utilities()
        {
            
        }
        //public static void Alert()
        //{
        //    ViewModels.DispatcherModels.ViewModelService.Dispatcher.Invoke(new ThreadStart(() =>
        //    {
        //        Utilities.AlertSoundPlayer.Position = TimeSpan.FromSeconds(0.0);
        //        Utilities.AlertSoundPlayer.Volume = Utilities.mainoptions.GeneralOptions.notificationvolume;
        //        Utilities.AlertSoundPlayer.Play();
        //    }), null);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of Objects in the Collection</typeparam>
        /// <param name="o">Collection to be reversed</param>
        public static ICollection<T> ReverseCollection<T>(ICollection<T> o) 
        {
            var col = o as Collection<T>;
            var collection = new Collection<T>();
            for (int x = col.Count - 1; x >= 0; x--)
            {
                collection.Add(col[x]);
            }
            return collection; 
        }
        //public static string ServiceType(string url)
        //{
        //    if (url.Contains("twimg"))
        //    {
        //        return url;
        //    }
        //    var status = new Status(new Uri("/Image/TimelineButtons/earth.png", UriKind.Relative), "Expanding link:" + url);
        //    string uri = "";
        //    try
        //    {
               
        //        MainWindowViewModel.AppStatus.addStatus(status);
        //        var request = System.Net.HttpWebRequest.Create(url);
        //        request.Method = "GET";
        //        var response = request.GetResponse();
        //        uri = response.ResponseUri.AbsoluteUri;
        //        MainWindowViewModel.AppStatus.removeStatus(status);
        //    }
        //    catch (Exception) { MainWindowViewModel.AppStatus.removeStatus(status); }
        //    if (uri.Contains("locker"))
        //    {
        //        return "http://api.plixi.com/api/tpapi.svc/imagefromurl?size=big&url=" + uri;
        //    }
        //    if (uri.Contains("twitpic"))
        //    {
        //        return uri.Replace("twitpic.com/", "twitpic.com/show/large/") + ".jpg";
        //    }
        //    if (uri.Contains("yfrog"))
        //    {
        //        return uri + ":medium";
        //    }
        //    if (uri.Contains("photobucket"))
        //    {
        //        return uri;
        //    }
        //    //if (uri.ToLower().Contains("twitter") && uri.Contains("photo"))
        //    //{
        //    //    return uri;
        //    //}
        //    return "Handled";
        //}
        //public static void DeleteTokenFile()
        //{
        //    File.Delete(Utilities.TOKENFOLDER+"/oeetyu45.twj");
        //    if(mainoptions.Users.Count==0)
        //    System.Windows.Application.Current.Shutdown(0);
        //}
        //public static bool ReadTokenFile()
        //{

        //    try
        //    {
              
        //        Directory.SetCurrentDirectory(Utilities.TOKENFOLDER);
        //        using (FileStream fs = File.Open(Utilities.TOKENFOLDER+"/oeetyu45.twj", FileMode.Open))
        //        {
        //            DES des= new DESCryptoServiceProvider();
        //            using (CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(DesKey, DesIV), CryptoStreamMode.Read))
        //            {
        //               BinaryFormatter formatter = new BinaryFormatter();
        //               mainoptions = (UsersInformation)formatter.Deserialize(cs);
        //               if (mainoptions.Users.Count == 0)
        //               {
        //                   throw new Exception("No Users");
        //               }
        //               mainoptions.GeneralOptions.WireUpEvents();
                      
        //            }
        //        }
        //        return true;

        //    }
        //    catch (DirectoryNotFoundException)
        //    {
        //        return false;
        //    }
        //    catch (FileNotFoundException)
        //    {
        //       // ViewModelBase.SendMessage(MessageBusType.OpenDialog, "Option File not found", DialogType.MessageBox, null, "MessageBoxDiag");
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //}
        //public static void CreateTokenFile()
        //{
        //    int attempts = 0;
        //    System.Threading.AutoResetEvent reset = new System.Threading.AutoResetEvent(false);
        //    while (true)
        //    {
        //        try
        //        {

        //            using (var fs = File.Create(Utilities.TOKENFOLDER + "/oeetyu45.twj"))
        //            {
        //                DES des = new DESCryptoServiceProvider();
        //                using (CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(DesKey, DesIV), CryptoStreamMode.Write))
        //                {
        //                    BinaryFormatter formatter = new BinaryFormatter();
        //                    formatter.Serialize(cs, mainoptions);
        //                }
        //            }
        //            break;
        //        }
        //        catch (Exception m)
        //        {
        //            if (attempts >= 3)
        //            {

        //                break;
        //            }
        //            reset.WaitOne(3000);
        //            System.Windows.MessageBox.Show(m.Message);
        //            attempts++;

        //        }
        //    }
        //}
        #if !SILVERLIGHT
        public static String EncryptString(string message,byte[] DesKey,byte[] DesIV)
        {
            try
            {
                DES des = new DESCryptoServiceProvider();
                var mem = new MemoryStream();
                
                using (CryptoStream stream = new CryptoStream(mem, des.CreateEncryptor(DesKey, DesIV), CryptoStreamMode.Write))
                {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(message);
                    writer.Flush();
                    stream.FlushFinalBlock();
                    writer.Flush();
                    return Convert.ToBase64String(mem.GetBuffer(), 0,Convert.ToInt32(mem.Length));
                }
                
                
            }
            catch(Exception) { return null; }
        }
        public static String DecryptString(string message,byte[] DesKey,byte[] DesIV)
        {
            try
            {
                MemoryStream mem = new MemoryStream(Convert.FromBase64String(message));
                DES des = new DESCryptoServiceProvider();
                using (CryptoStream stream = new CryptoStream(mem, des.CreateDecryptor(DesKey, DesIV), CryptoStreamMode.Read))
                {
                    StreamReader reader = new StreamReader(stream);
                   return reader.ReadToEnd();
                }
                
                
            }
            catch (Exception) { return null; }
            

        }
        public static void CopyData(Stream from, Stream to,long totallength)
        {

            int len;
            try
            {
                from.Position = 0;
            }
            catch (Exception)
            {

            }
            long written = 0;
            byte[] buffer = new byte[1000];
            while (written<totallength)
            {
               // if ((totallength - written) >= 1000)
               // {
                    len = from.Read(buffer, 0, 1000);
                    if (len == 0)
                        break;

                    to.Write(buffer, 0, len);
                    to.Flush();
                    written = written + len;
                //}
                //else
                //{
                //    buffer = new byte[(totallength - written)];
                //    len=from.Read(buffer,0,Convert.ToInt32(totallength-written));
                //    to.Write(buffer, 0, len);
                //    written = written + len;
                //}
            }
        }
        #endif
        public static string ParseTime(DateTime CreatedDate, DateTime Now)
        {

            var madeTime = CreatedDate;
            long difference;
            DateTime result;

            if (Now.CompareTo(madeTime) == 1)
            {
                difference = Now.Ticks - madeTime.Ticks;
                result = new DateTime(difference);
            }
            else
                if (madeTime.CompareTo(Now) == 1)
                {
                    return "Right Now!";

                }
                else
                {
                    return "a few seconds ago";
                }
            if (madeTime.Year == Now.Year)
            {
                if (madeTime.Month == Now.Month)
                {
                    if (madeTime.DayOfYear == Now.DayOfYear)
                    {

                        if (result.Hour < 1)
                        {
                            if (result.Minute < 1)
                            {
                                if (result.Second == 0)
                                {
                                    return "Right Now!";
                                }
                                return Convert.ToString(result.Second) + " Seconds ago";
                            }
                            return Convert.ToString(result.Minute) + " Minutes ago";
                        }
                        return Convert.ToString(result.Hour) + " Hours ago";
                    }
                    else
                    {
                        int time = Now.DayOfYear - madeTime.DayOfYear;
                        if (time == 1)
                            return "Yesterday";

                        return Convert.ToString(time).Replace("-", string.Empty) + " Days(s) ago , ";
                    }
                }
                else
                {
                    if (Now.DayOfYear - madeTime.DayOfYear < 31)
                        return Convert.ToString(Now.DayOfYear - madeTime.DayOfYear) + " Days(s) ago , ";

                    if (result.Month == 1)
                        return "Last Month";

                    return Convert.ToString(result.Month) + " Months ago";
                }
            }
            else
            {
                return Convert.ToString(Now.Year - madeTime.Year).Replace("-", string.Empty) + " Year(s) ago , ";

            }
            
        }
        //public static void DisplayError(Exception e)
        //{
        //    if (Utilities.mainoptions.GeneralOptions.DisplayErrorMessages)
        //        Utilities.SendMessage(MessageBusType.OpenDialog, "Error: " + e.Message, DialogType.MessageBox, null, "MessageBoxDiag");
        //}
    }

#if !SILVERLIGHT
    [Serializable]
#endif
    public class UIWrapper:System.ComponentModel.INotifyPropertyChanged
    {

        public UIWrapper()
        {
        }
        public UIWrapper(List<object> list)
        {
            itemList = list;
        }
#if !SILVERLIGHT
    [field:NonSerialized]
#endif
        public event EventHandler<System.ComponentModel.PropertyChangedEventArgs> Notify;
        private string name;
        private List<object> itemList;
        private object selectedItem;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public List<object> ItemList
        {
            get { return itemList; }
            set { itemList = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ItemList"));
            }
        }
        public object SelectedItem
        {
            get {
                if (selectedItem == null)
                {
                    if (itemList != null&&itemList.Count>0)
                    {
                        selectedItem = itemList[0];
                    }
                }
                return selectedItem; }
            set
            {
                
                    if (Notify != null&& isEnabled)
                    Notify(value, new System.ComponentModel.PropertyChangedEventArgs(PropertyName));
                    selectedItem = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("SelectedItem"));
                
            }

        }
        public void Refresh()
        {
            SelectedItem = selectedItem;
        }
       private bool isEnabled=false;
       public bool IsEnabled
       {
           get { return isEnabled; }
           set
           {
               if (value != isEnabled)
               {
                   isEnabled = value;
                   if(PropertyChanged!=null)
                   PropertyChanged(this,new System.ComponentModel.PropertyChangedEventArgs("IsEnabled"));
               }
           }
       }
        public string PropertyName;

        #region INotifyPropertyChanged Members
        #if !SILVERLIGHT
        [field: NonSerializedAttribute()]
     #endif
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
#if !SILVERLIGHT
    [Serializable]

    public class MyBindingList<T> : BindingList<T>
    {
        volatile List<T> newBuffer;
        volatile List<T> oldBuffer;
        public MyBindingList()
            : base()
        {
            newBuffer = new List<T>();
            oldBuffer = new List<T>();
        }
        public void MyRemoveAt(int index)
        {

            var o = this.Items[index];
            oldBuffer.Add(o);
            this.Items.RemoveAt(index);
        }
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (RaiseListChangedEvents)
            {
                if (e is MyListChangedEventArgs)
                {
                    base.OnListChanged(e);
                    ResetBuffers();
                }             
            }
        }
        public void MyAdd(T o)
        {

            newBuffer.Add(o);     
            this.Add(o);
        }
        public void MyRemove(T o)
        {
            oldBuffer.Add(o);          
            this.Remove(o);
        }
        public void MyInsert(T o,int index)
        {
            newBuffer.Add(o);          
            this.Insert(index,o);
        }
        public void DelayChanges()
        {
            this.RaiseListChangedEvents = false;
        }
        public void DisableDelay()
        {
            this.RaiseListChangedEvents=true;
        }
        public void MyClear()
        {

            foreach (T o in Items)
            {
                oldBuffer.Add(o);
            }
            
            this.Clear();
        }
        public void CommitChanges()
        {
            this.RaiseListChangedEvents = true;
            OnListChanged(new MyListChangedEventArgs(ListChangedType.Reset,this.Items.Count-1) { NewItems = newBuffer, OldItems = oldBuffer });
            this.RaiseListChangedEvents = false;
           
        }
        protected void ResetBuffers()
        {
            this.newBuffer.Clear();
            this.oldBuffer.Clear();
        }
    }
    public class MyListChangedEventArgs : ListChangedEventArgs
    {
        public MyListChangedEventArgs(ListChangedType type, int newindex)
            : base(type, newindex)
        {
        }
        public IList OldItems { get; set; }
        public IList NewItems { get; set; }
    }
#endif
    //public class Usage : Attribute
    //{
       
    //    public string use;
    //    public Usage(string use)
    //        : base()
    //    {
    //        this.use = use;
    //    }
    //}
   
    //public class CustomEventArgs<T> : EventArgs where T : class
    //{
    //    public T SingleParameter { get; set; }
    //    public ICollection<T> MultipleParameters { get; set; }
    //    public CustomEventArgs(ICollection<T> parameters)
    //    {
    //       MultipleParameters=parameters;
    //    }
    //    public CustomEventArgs(T singleparam)
    //    {
    //        SingleParameter = singleparam;
    //    }
    //}
    //public class Pre_Post_ClipboardSendMessageEventArgs : CustomEventArgs<string>
    //{
    //    public object[] OptionalParameters { get; set; }
    //    public Pre_Post_ClipboardSendMessageEventArgs(string message, int optionalparamtersize)
    //        : base(message)
    //    {
    //        OptionalParameters = new object[optionalparamtersize];
    //    }
    //}
    //public class AddColumnEventArgs : CustomEventArgs<ContentViewModel>
    //{
    //    public AddColumnEventArgs(ContentViewModel model)
    //        : base(model)
    //    {
    //    }
    //}
}
