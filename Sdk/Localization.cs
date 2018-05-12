using System;
using System.Collections.Generic;
using System.IO;

namespace FbFarm.Sdk
{    
    public static class Localization
    {
        // hard-coded is not good
        // Allow user change datafolder
        public static string DataFolder = ".\\Data\\Localization";

        // allow specify which language to use
        private static string _languageName;
        private static List<string> _languageNames;
        private static Dictionary<string, Dictionary<string, string>> _languageMap;

        static Localization()
        {
            // init dictionary <language, dictionary<key, value>>            
            _languageNames = new List<string>();
            _languageMap = new Dictionary<string, Dictionary<string, string>>();  
            _loadLanguage();
        }
        private static void _loadLanguage()
        {            
            // load language
            if (Directory.Exists(DataFolder))
            {
                var langFiles = Directory.GetFiles(DataFolder);
                if (langFiles.Length == 0)
                    throw new FileNotFoundException("No language file found.");

                foreach (var file in langFiles)
	            {
                    var languageName = Path.GetFileNameWithoutExtension(file);
                    // add language
                    _languageNames.Add(languageName);
                    _languageMap[languageName] = new Dictionary<string, string>();
                    // load language value to map
                    var lines = File.ReadAllLines(file);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var line = lines[i].Trim();
                        if (line.StartsWith("//"))
                            continue;
                        var pair = line.Split(',');
                        if (pair.Length == 2)
                            _languageMap[languageName][pair[0]] = pair[1];
                    }
	            }
                
                // set default language
                _languageName = _languageNames[0];
            }
            else 
            {
                throw new DirectoryNotFoundException("Localization directory doesn't exist");
            }
        }

        // Setup FBClient language
        public static String LanguageName
        {
            get
            {
                return _languageName;
            }
            set
            {
                if (!_languageNames.Contains(value))
                    throw new Exception("Doesn't support this language.");
                _languageName = value;
            }
        }
        
        // Anchor string -- pattern depend on user language
        public static string EditProfilePicture
        {
            get 
            {
                return _languageMap[_languageName]["EditProfilePicture"];
            }
        }
        public static string AddProfilePicture
        {
            get
            {
                return _languageMap[_languageName]["AddProfilePicture"];
            }
        }
        public static string IsGroupAdmin
        {
            get
            {
                return _languageMap[_languageName]["AddProfilePicture"];
            }
        }
        public static string PageNotFound
        {
            get
            {
                return _languageMap[_languageName]["PageNotFound"];
            }
        }
        public static string Like
        {
            get
            {
                return _languageMap[_languageName]["Like"];
            }
        }
        public static string GroupYouAreIn
        {
            get
            {
                return _languageMap[_languageName]["GroupYouAreIn"];                
            }
        }
        public static string TimeLine
        {
            get 
            {
                return _languageMap[_languageName]["TimeLine"];
            }
        }
        public static string Friends
        {
            get 
            {
                return _languageMap[_languageName]["Friends"];
            }
        }
        public static string Photos
        {
            get 
            {
                return _languageMap[_languageName]["Photos"];
            }
        }
        public static string Likes
        {
            get 
            {
                return _languageMap[_languageName]["Likes"];
            }
        }
        public static string Followers
        {
            get 
            {
                return _languageMap[_languageName]["Followers"];
            }
        }
        public static string Following
        {
            get 
            {
                return _languageMap[_languageName]["Following"];
            }
        }
        public static string ActivityLog
        {
            get 
            {
                return _languageMap[_languageName]["Activity Log"];
            }
        }
        public static string AddFriend
        {
            get 
            {
                return _languageMap[_languageName]["Add Friend"];
            }
        }
        public static string Message
        {
            get
            {
                return _languageMap[_languageName]["Message"];
            }
        }
        public static string Follow
        {
            get
            {
                return _languageMap[_languageName]["Follow"];
            }
        }
        public static string More
        {
            get
            {
                return _languageMap[_languageName]["More"];
            }
        }
        public static string CurrentCity
        {
            get
            {
                return _languageMap[_languageName]["Current City"];
            }
        }
        public static string HomeTown
        {
            get
            {
                return _languageMap[_languageName]["Hometown"];
            }
        }
    }
}

