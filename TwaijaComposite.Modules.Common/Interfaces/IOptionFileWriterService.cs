using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IOptionFileWriterService
    {
        /// <summary>
        /// Reads the Token File from the specified folder.
        /// </summary>
        /// <param name="directory">string path to read from</param>
        /// <param name="file">the returned file ,returns false if the file is not found </param>
        /// <param name="expectedReturnType">the expected return type</param>
        /// <returns></returns>
        bool ReadFile(string directory,Type expectedReturnType,out object file,params object[] optionalparameter);
        /// <summary>
        /// Creates the Token file at the specified folder
        /// </summary>
        /// <param name="directory">string path to save to</param>
        /// <param name="file">file to save</param>
        /// <returns></returns>
        bool CreateFile(string directory, object file,params object[] optionalparameters);
    }
}
