using System;
using System.Configuration;

namespace StoreTransferKit.Helper {
    /// <summary>
    ///     Get and set configuration in `app.config` file.
    /// </summary>
    public class ConfigurationHelper {
        
        public static string ConnectionString {
            get {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }

        public static string ReadSetting(string key) {
            string result = String.Empty;
            try {
                result = ConfigurationManager.AppSettings[key] ?? String.Empty;
            } catch (ConfigurationErrorsException) {
                throw new ConfigurationErrorsException("Error reading app settings");
            }
            return result;
        }

        public static void AddUpdateAppSettings(string key, string value) {
            try {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null) {
                    settings.Add(key, value);
                } else {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            } catch (ConfigurationErrorsException) {
                throw new ConfigurationErrorsException("Error writing app settings");
            }
        }
    }
}