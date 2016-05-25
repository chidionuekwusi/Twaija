using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
#if SILVERLIGHT
using System.Runtime.Serialization;
using System.Reflection;
#endif

namespace TwaijaComposite.Modules.Common
{
    public class ColumnParser
    {
        string ColumnString;
        public string ColumnBuildType { get; set; }
        public string ColumnImpType { get; set; }
        public string ColumnType { get; set; }
        public string User { get; set; }
        public  Dictionary<string, object> Parameters { get; set; }
        public void SetParser(string columnString)
        {
             if (string.IsNullOrEmpty(columnString))
            {
                throw new ArgumentNullException(" You must provide ColumnString ");
            }
            ColumnString = columnString;
        }
        public bool Parse(string columnString)
        {
            if (string.IsNullOrEmpty(columnString))
            {
                throw new ArgumentNullException(" You must provide ColumnString ");
            }
            
            var parts = columnString.Split(new string[]{ColumnWriter.seperator},StringSplitOptions.None);
            if (parts.Length <= 0)
            {
                throw new ArgumentNullException("Invalid ColumnString");
            }
            
            try
            {
                ColumnBuildType = parts[0];
                ColumnImpType = parts[1];
                ColumnType = parts[2];
                User = parts[3];
                if (parts.Length > 4)
                {
                    List<string> parameters = new List<string>(parts);
                    parameters.RemoveRange(0,4);
                    GenerateParameters(parameters.ToArray());
                }
                return true;
            }
            catch
            {
                
            }
            return false;
        }
        void GenerateParameters(params string[] columnString)
        {
            Parameters = new Dictionary<string, object>();
            foreach (string entry in columnString)
            {
                var parts = entry.Split(new string[]{ColumnWriter.parameterKeyValueSeperator},StringSplitOptions.None);
                var Key = parts[0];
                var valueParts = parts[1].Split(new string[]{ColumnWriter.parameterValueSeperator},StringSplitOptions.None);
                var valueType = Type.GetType(valueParts[0]);

                if (valueType.IsValueType)
                {
                    Parameters.Add(Key, Convert.ChangeType(valueParts[1], valueType, System.Globalization.CultureInfo.InvariantCulture));
                }
                else
                {
                    if (valueType.Equals(typeof(string)))
                    {
                        Parameters.Add(Key, valueParts[1]);
                    }
                    else
                    {
                        Parameters.Add(Key, Deserialize(valueType, valueParts[1]));
                    }
                }
            }
        }
         object Deserialize(Type type, string typestring)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                using (StringReader reader = new StringReader(typestring))
                {
                    return serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error occured while De-serializing A Column parameter value", e);
            }
             
        }
        /// <summary>
        /// Returns a string that represents the ColumnBuildType
        /// </summary>
        /// <param name="ColumnString"></param>
        /// <returns></returns>

    }
    public class ColumnWriter
    {
        public const string seperator = "*:*";
        public const string parameterValueSeperator = "[TV]";
        public const string parameterKeyValueSeperator = "*=*";
        private void AppendParameters(Dictionary<string, object> dictionary,StringBuilder builder)
        {
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                builder.AppendFormat("{0}{1}{2}{3}", pair.Key,parameterKeyValueSeperator, pair.Value.GetType().FullName,parameterValueSeperator);

                if (pair.Value.GetType().Equals(typeof(string)))
                {
                    builder.Append(pair.Value);
                }
                else
                {
                    if (pair.Value.GetType().IsValueType)
                    {
                        builder.Append(Convert.ChangeType(pair.Value, typeof(string),System.Globalization.CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        builder.Append(Serialize(pair.Value.GetType(), pair.Value));
                    }

                }
                builder.Append(seperator);
            }
        }
        public string CreateColumnString(string ColumnBuildType, string ColumnImpType, string ColumnType,string User,Dictionary<string,object> Parameters)
        {        
           StringBuilder builder = new StringBuilder();
           builder.AppendFormat("{0}{1}",ColumnBuildType,seperator);
           builder.AppendFormat("{0}{1}",ColumnImpType,seperator);
            builder.AppendFormat("{0}{1}",ColumnType,seperator);
            builder.AppendFormat("{0}{1}",User,seperator);
           AppendParameters(Parameters, builder);
           return builder.ToString().Remove(builder.Length - 3, 3); 
        }
         string Serialize(Type type, object model)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                using (StringWriter writer = new StringWriter())
                {                 
                    serializer.Serialize(writer, model);
                    return writer.ToString();
                }
            }
            catch (Exception w)
            {
                throw new Exception("Error occured while Serializing A Column parameter value", w);
            }
           
        }
    }
}
