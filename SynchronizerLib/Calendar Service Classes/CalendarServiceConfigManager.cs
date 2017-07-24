using System;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SynchronizerLib
{
    public class CalendarServiceConfigManager
    {
        private static readonly string _outFilterKey = "outFilter";
        private static readonly string _outTransformationConditionKey = "outTransformationCondition";
        private static readonly string _outTransformationKey = "outTransformation";
        private static readonly string _inTransformationConditionKey = "inTransformationCondition";
        private static readonly string _inTransformationKey = "inTransformation";
        private static readonly string _bannedToSyncToServicesKey = "bannedToSyncToServices";

        public string SettingSectionName { get; private set; }

        private string _outFilter;
        private EventTransformation _outTransformation;
        private EventTransformation _inTransformation;
        private List<string> _bannedToSyncToServices;

        public CalendarServiceConfigManager(string settingSectionName)
        {
            SettingSectionName = settingSectionName;
            LoadConfigKeys();
        }

        public string OutFilter
        {
            get { return _outFilter; }
            set
            {
                if (value != null)
                {                    
                    _outFilter = value;
                    ChangeConfigValue(_outFilterKey, value);
                }
            }
        }

        public EventTransformation OutTransformation
        {
            get { return _outTransformation; }
            set
            {
                if (value != null)
                {
                    _outTransformation = value;
                    ChangeConfigValue(_outTransformationConditionKey, value.Condition);
                    ChangeConfigValue(_outTransformationKey, value.Transformation);
                }
            }
        }

        public EventTransformation InTransformation
        {
            get { return _inTransformation; }
            set
            {
                if (value != null)
                {
                    _inTransformation = value;
                    ChangeConfigValue(_inTransformationConditionKey, value.Condition);
                    ChangeConfigValue(_inTransformationKey, value.Transformation);
                }
            }
        }

        public List<string> BannedToSyncToServices
        {
            get { return _bannedToSyncToServices; }
            set
            {
                if (value != null)
                {
                    _bannedToSyncToServices = value;
                    string services = String.Empty;
                    for (int i = 0; i < value.Count; i++)
                        if (i < value.Count - 1)
                            services += value[i] + ", ";
                        else
                            services += value[i];
                    ChangeConfigValue(_bannedToSyncToServicesKey, services);
                }
            }
        }

        private void LoadConfigKeys()
        {
            _outFilter = (ConfigurationManager.GetSection(SettingSectionName) as NameValueCollection)[_outFilterKey];
            _outTransformation = new EventTransformation((ConfigurationManager.GetSection(SettingSectionName) as NameValueCollection)[_outTransformationConditionKey],
                                                         (ConfigurationManager.GetSection(SettingSectionName) as NameValueCollection)[_outTransformationKey]);
            _inTransformation = new EventTransformation((ConfigurationManager.GetSection(SettingSectionName) as NameValueCollection)[_inTransformationConditionKey],
                                                         (ConfigurationManager.GetSection(SettingSectionName) as NameValueCollection)[_inTransformationKey]);
            _bannedToSyncToServices = (ConfigurationManager.GetSection(SettingSectionName) as NameValueCollection)[_bannedToSyncToServicesKey].Replace(" ", "")
                                                                                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private void ChangeConfigValue(string configKey, string newConfigValue)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            (currentConfig.GetSection(SettingSectionName) as AppSettingsSection).Settings[configKey].Value = newConfigValue;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(SettingSectionName);
            LoadConfigKeys();
        }
    }
}
