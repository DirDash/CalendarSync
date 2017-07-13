using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynchronizerLib
{
    public static class SynchronizationConfigManager
    {
        private static int _synchronizationIntervalInDays;
        private static bool _autosyncronizationMode;
        private static int _autosyncIntervalInSeconds;

        private static string _outlookCategoryForImported;
        private static List<string> _outlookNonSynchronizeCategories;

        private static string _googleCategoryColorIDForImported;
        private static List<string> _googleNonSynchronizeCategories;

        public static int SynchronizationIntervalInDays
        {
            get { return _synchronizationIntervalInDays; }
            set
            {
                _synchronizationIntervalInDays = value;
                ChangeConfigValue("syncIntervalInDays", value.ToString());
            }
        }

        public static bool AutosyncronizationMode
        {
            get { return _autosyncronizationMode; }
            set
            {
                _autosyncronizationMode = value;
                ChangeConfigValue("autosync", value.ToString());
            }
        }

        public static int AutosyncIntervalInSeconds
        {
            get { return _autosyncIntervalInSeconds; }
            set
            {
                _autosyncIntervalInSeconds = value;
                ChangeConfigValue("autosyncIntervalSec", value.ToString());
            }
        }

        public static string OutlookCategoryForImported
        {
            get { return _outlookCategoryForImported; }
            set
            {
                _outlookCategoryForImported = value;
                ChangeConfigValue("outlookCategoryForImported", value);
            }
        }

        public static string GoogleCategoryColorIDForImported
        {
            get { return _googleCategoryColorIDForImported; }
            set
            {
                _googleCategoryColorIDForImported = value;
                ChangeConfigValue("googleColorIDForImported", value);
            }
        }

        public static List<string> GoogleNonSynchronizeCategories
        {
            get { return _googleNonSynchronizeCategories; }
            set
            {
                if (value != null)
                {
                    _googleNonSynchronizeCategories = value;
                    string toConfig = String.Empty;
                    if (value.Count != 0)
                    {
                        int i = 0;
                        for (; i < value.Count - 1; i++)
                            toConfig += value[i] + ",";
                        toConfig += value[i];
                    }
                    ChangeConfigValue("googleNonSynchronizeCategories", toConfig);
                }
                else
                    throw new ArgumentNullException();
            }
        }

        public static List<string> OutlookNonSynchronizeCategories
        {
            get { return _outlookNonSynchronizeCategories; }
            set
            {
                if (value != null)
                {
                    _outlookNonSynchronizeCategories = value;
                    string toConfig = String.Empty;
                    if (value.Count != 0)
                    {
                        int i = 0;
                        for (; i < value.Count - 1; i++)
                            toConfig += value[i] + ",";
                        toConfig += value[i];
                    }
                    ChangeConfigValue("outlookNonSynchronizeCategories", toConfig);
                }
                else
                    throw new ArgumentNullException();
            }
        }

        static SynchronizationConfigManager()
        {
            LoadConfigKeys();
        }

        private static void LoadConfigKeys()
        {
            SynchronizationIntervalInDays = int.Parse(ConfigurationManager.AppSettings["syncIntervalInDays"]);
            switch (ConfigurationManager.AppSettings["autosync"])
            {
                case "False":
                    AutosyncronizationMode = false;
                    break;
                case "True":
                    AutosyncronizationMode = true;
                    break;
            }
            AutosyncIntervalInSeconds = int.Parse(ConfigurationManager.AppSettings["autosyncIntervalSec"]);
            OutlookCategoryForImported = ConfigurationManager.AppSettings["outlookCategoryForImported"];
            GoogleCategoryColorIDForImported = ConfigurationManager.AppSettings["googleColorIDForImported"];
            GoogleNonSynchronizeCategories = ConfigurationManager.AppSettings["googleNonSynchronizeCategories"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            OutlookNonSynchronizeCategories = ConfigurationManager.AppSettings["outlookNonSynchronizeCategories"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private static void ChangeConfigValue(string configKey, string newConfigValue)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            currentConfig.AppSettings.Settings[configKey].Value = newConfigValue;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
