using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace TwaijaComposite.Modules.Common.Services
{
    public static class FolderNames
    {
#if SILVERLIGHT
        public static readonly string PROGRAMFILESFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        public static readonly string THEMESFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "/Twaija/Themes";
        public static readonly string SOUNDSFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "/Twaija/Alerts/_Alerts";
        public static readonly string SWAGOMETERFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "/Twaija/Alerts/_Meter";
        public static readonly string TOKENFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "/Twaija/_5e32sf";
        public static readonly string IMAGECACHEFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "/Twaija/imgcache";
#else
        public static readonly string PROGRAMFILESFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        public static readonly string THEMESFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "/Twaija/Themes";
        public static readonly string SOUNDSFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "/Twaija/Alerts/_Alerts";
        public static readonly string SWAGOMETERFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "/Twaija/Alerts/_Meter";
        public static readonly string TOKENFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Twaija/_5e32sf";
        public static readonly string IMAGECACHEFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "/Twaija/imgcache";
#endif
        public static readonly string IMAGESFOLDER = "/Twaija3.0;component/Images/TimelineButtons/ButtonIcons/";
    }
}
