using System;
using System.Configuration;
using ZooKeeperNet;

namespace SigmalHex.Zookeeper
{
    public class ZookeeperClientFactory
    {
        public static string ZkConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ZkConnectionString"].ConnectionString;
            }
        }

        public static TimeSpan ZkSessionTimeout
        {
            get
            {
                try
                {
                    return TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["ZkSessionTimeout"].Trim()));
                }
                catch { }

                return TimeSpan.FromSeconds(10);
            }
        }

        public static ZooKeeper GetZookeeper(IWatcher globalWather = null)
        {
            return new ZooKeeper(ZkConnectionString, ZkSessionTimeout, globalWather);
        }
    }
}
