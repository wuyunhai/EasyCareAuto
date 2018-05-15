
using System.Reflection;
using System.Configuration.Install;

namespace MES.SocketService
{
    /// <summary>
    /// 安装为Windows Service
    /// </summary>
    public static class SelfInstaller
    {
        private static readonly string _execPath = Assembly.GetExecutingAssembly().Location;

        public static bool InstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { _execPath });
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <returns></returns>
        public static bool UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { "/u", _execPath });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
