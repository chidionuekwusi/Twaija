using System;
using TwaijaComposite.Modules.Common.Interfaces;
using System.IO;
using System.Security.Cryptography;
#if !SILVERLIGHT
using System.Runtime.Serialization.Formatters.Binary;
#else
using System.Runtime.Serialization;
using System.Collections.Generic;
#endif
namespace TwaijaComposite.Modules.Common.Services
{ 
   public class OptionFileWriterImp:IOptionFileWriterService
    {
        public bool ReadFile(string directory,Type expectedReturnType, out object file, params object[] optionalparameter)
        {
            file = null;
            try
            {
               
               // Directory.SetCurrentDirectory(directory);
                using (FileStream fs = File.Open(directory + "/oeetyu45.twj", FileMode.Open))
                {



#if !SILVERLIGHT
                    DES des = new DESCryptoServiceProvider();
                    using (CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                 
                        BinaryFormatter formatter = new BinaryFormatter();
                        file = formatter.Deserialize(cs);
                       
                    }
#else
                    AesManaged aes=new AesManaged();
                    using(CryptoStream cs=new CryptoStream(fs,aes.CreateDecryptor(Key,IV),CryptoStreamMode.Read))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(expectedReturnType, new List<Type>() {typeof(TwitterUser.TwitterUserMemento)});
                        file=serializer.ReadObject(cs);
                    }
#endif
                }
                return true;

            }
            catch (DirectoryNotFoundException)
            {

            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool CreateFile(string directory, object file, params object[] optionalparameter)
        {
            
                try
                {
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    using (var fs = File.Create(directory + "/oeetyu45.twj"))
                    {
#if !SILVERLIGHT
                        DES des = new DESCryptoServiceProvider();
                        using (CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(cs, file);
                        }
#else
                        AesManaged aes=new AesManaged();
                     
                        using(CryptoStream cs=new CryptoStream(fs,aes.CreateEncryptor(Key,IV),CryptoStreamMode.Write))
                        {
                            DataContractSerializer serializer = new DataContractSerializer(file.GetType(), new List<Type>() {typeof(TwitterUser.TwitterUserMemento)});
                           
                            serializer.WriteObject(cs,file);
                        }
#endif
                    }
                    return true;
                  
                }
                catch (Exception)
                {
                    
                 

                }
                return false;
            
        }

        public byte[] Key
        {
            get
            {
#if SILVERLIGHT
                return System.Text.Encoding.UTF8.GetBytes("gdu28u9o5rgcnj6op075rds3wtu094eh");
#else
                return System.Text.Encoding.ASCII.GetBytes("gdu28u9o");
#endif
            }
        }

        public byte[] IV
        {
            get
            {
                #if SILVERLIGHT
                return System.Text.Encoding.UTF8.GetBytes("9he76hg8hd4yo80p");
#else
                return System.Text.Encoding.ASCII.GetBytes("9he76hg8");
#endif
            }
        }


    

}
}
