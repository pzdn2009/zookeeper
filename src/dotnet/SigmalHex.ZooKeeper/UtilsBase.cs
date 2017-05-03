using System;
using System.IO;
using System.Timers;
using ZooKeeperNet;
using ZooKeeperNet.Recipes;

namespace SigmalHex.Zookeeper
{
    public class UtilsBase: ProtocolSupport
    {
        private Timer timer;

        public UtilsBase(IZooKeeper zooKeeper, IZkCache zkCache, IZkSerialization zkSerialization) : this(zooKeeper, zkCache, zkSerialization, TimeSpan.FromMinutes(1), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "zookeeper.cache"))
        {
        }

        public UtilsBase(IZooKeeper zooKeeper, IZkCache zkCache, IZkSerialization zkSerialization, TimeSpan syncInterval, string filePath) : base(zooKeeper)
        {
            if (zooKeeper == null)
            {
                throw new ArgumentNullException("zookeeper");
            }

            ZkCache = zkCache ?? throw new ArgumentNullException("zkCache");

            ZkSerialization = zkSerialization;

            this.LocalZkFilePath = filePath;

            SyncInterval = syncInterval;
            timer = new Timer(SyncInterval.TotalSeconds);
            timer.Elapsed += Timer_Elapsed;

            Init();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();

                SyncToLocal();

                timer.Start();
            }
            catch (Exception ex)
            {
                LOG.Error(ex);
            }
        }

        public string LocalZkFilePath { get; set; }

        public IZkCache ZkCache { get; set; }

        public IZkSerialization ZkSerialization { get; set; }

        public TimeSpan SyncInterval { get; set; }

        public void Init()
        {
            var ret = RetryOperation(() =>
            {
                return Zookeeper.Exists("/", false) != null;
            });

            if (!ret)
            {
                if (ZkSerialization.Enable)
                {
                    var str = File.ReadAllText(LocalZkFilePath);
                    ZkSerialization.Deserilize(ZkCache, str);
                }
            }
        }

        public void SyncToLocal()
        {
            if (ZkSerialization.Enable)
            {
                var str = ZkSerialization.Serialize(ZkCache);
                File.WriteAllText(LocalZkFilePath, str);
            }
        }
    }
}
