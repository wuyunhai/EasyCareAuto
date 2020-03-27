using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace MES.SocketService
{ 
    public class ConfigHelper
    {
        private Configuration config = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ConfigHelper()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 构造函数（指定配置文件名）
        /// </summary>
        /// <param name="configFile"></param>
        public ConfigHelper(string configFile)
        {
            if (!File.Exists(configFile))
            {
                File.Create(configFile);
            }
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = configFile;
            config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 判断appSettings中是否有此项
        /// </summary>
        public bool IsKeyExists(string key)
        {
            if (config.AppSettings.Settings[key] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取AppSettings配置节中的Key值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns>值</returns>
        public string GetKeyValue(string key)
        {
            try
            {
                return config.AppSettings.Settings[key].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 保存appSettings中某key的value值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="value">新值</param>
        public bool SaveKeyValue(string key, string value)
        {
            try
            {
                config.AppSettings.Settings[key].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取ConnectionStrings配置节中的值
        /// </summary>
        /// <returns>连接字符串</returns>
        public string GetConnectionStrings()
        {
            return config.ConnectionStrings.ConnectionStrings["connectionString"].ConnectionString;
        }

        /// <summary>
        /// 获取指定连接字符串配置名的值
        /// </summary>
        /// <param name="connName">连接配置名</param>
        /// <returns>连接字符串</returns>
        public string GetConnectionString(string connName)
        {
            string connectionString = config.ConnectionStrings.ConnectionStrings[connName].ConnectionString;
            return connectionString;
        }

        /// <summary>
        /// 保存节点中ConnectionStrings的子节点配置项的值
        /// </summary>
        /// <param name="value"></param>
        public void SaveConnectionString(string value)
        {
            config.ConnectionStrings.ConnectionStrings["connectionString"].ConnectionString = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        /// <summary>
        /// 保存节点中ConnectionStrings的子节点配置项的值
        /// </summary>
        /// <param name="elementValue"></param>
        public void SaveConnectionString(string connName, string value)
        {
            config.ConnectionStrings.ConnectionStrings[connName].ConnectionString = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(connName);
        }
    }
}
