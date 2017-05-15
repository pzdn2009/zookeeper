using Org.Apache.Zookeeper.Data;
using System.Collections.Generic;
using ZooKeeperNet;

namespace SigmalHex.Zookeeper
{
    /// <summary>
    /// 配置中心
    /// </summary>
    public class ConfigCenterTools : UtilsBase
    {
        internal class ConfigCenterWatcher : IWatcher
        {
            private IZkCache zkCache;
            private IZooKeeper Zookeeper;
            public ConfigCenterWatcher(IZooKeeper zooKeeper, IZkCache zkCache)
            {
                this.zkCache = zkCache;
                this.Zookeeper = zooKeeper;
            }

            public void Process(WatchedEvent ev)
            {
                var path = ev.Path;
                var value = string.Empty;
                //debug
                switch (ev.Type)
                {
                    case EventType.None:
                        break;
                    case EventType.NodeCreated:
                        value = Zookeeper.GetData(path, this, null).GetString();
                        zkCache.SetValue(path, value);
                        Zookeeper.Exists(path, this);
                        LOG.Debug(" >>NodeCreated:" + value);
                        System.Console.WriteLine(" >>NodeCreated:" + value);
                        break;
                    case EventType.NodeDeleted:
                        Zookeeper.Exists(path, this);
                        LOG.Debug(" >>NodeDeleted:" + value);
                        System.Console.WriteLine(" >>NodeDeleted:" + value);
                        break;
                    case EventType.NodeDataChanged:
                        value = Zookeeper.GetData(path, this, null).GetString();
                        zkCache.SetValue(path, value);
                        Zookeeper.Exists(path, this);
                        LOG.Debug(" >>NodeDataChanged:" + value);
                        System.Console.WriteLine(" >>NodeDataChanged:" + value);
                        break;
                    case EventType.NodeChildrenChanged:
                        break;
                    default:
                        break;
                }
            }
        }

        public ConfigCenterTools() : this(ZookeeperClientFactory.GetZookeeper(), new ZkCache(), new ZkSerialization())
        {
        }

        protected ConfigCenterTools(ZooKeeper zk, IZkCache zkCache, IZkSerialization zkSerialization)
            : base(zk, zkCache, zkSerialization)
        {
        }

        public string Watch(string path)
        {
            var stat = Zookeeper.Exists(path, new ConfigCenterWatcher(Zookeeper, ZkCache));
            if (stat != null)
            {
                var data = Zookeeper.GetData(path, false, null).GetString();
                ZkCache.SetValue(path, data);
                return data;
            }

            return null;
        }

        public void SetNode(string path, string value)
        {
            SetNode(path, value, base.Acl);
        }

        public void SetNode(string path, string value, IEnumerable<ACL> acl)
        {
            var stat = Zookeeper.Exists(path, false);
            if (stat == null)
            {
                var createRet = Zookeeper.Create(path, value.GetBytes(), acl, CreateMode.Persistent);
            }
            else
            {
                Zookeeper.SetData(path, value.GetBytes(), stat.Version);
            }
        }

        public void ImportConfigs(Dictionary<PathRoot, string> configs)
        {
            foreach (var config in configs)
            {
                SetNode(config.Key.ToPath(), config.Value);
            }
        }

        public void ImportConfigs(Dictionary<string, string> configs)
        {
            foreach (var config in configs)
            {
                SetNode(config.Key, config.Value);
            }
        }

        public string GetConfig(string path, bool local = true)
        {
            if (local)
            {
                return ZkCache.GetValue(path);
            }
            else
            {
                return Watch(path);
            }
        }

        public string GetConfig(PathRoot path, bool local = true)
        {
            return GetConfig(path.ToPath(), local);
        }
    }
}
