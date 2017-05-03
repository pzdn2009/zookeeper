using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SigmalHex.Zookeeper
{
    /// <summary>
    /// 內存中讀取，用於緩存所有配置
    /// </summary>
    public class ZkCache : IZkCache
    {
        private static object lockObject = new object();
        private static Dictionary<string, string> dictZkCache = new Dictionary<string, string>();
        private static Dictionary<string, HashSet<string>> dictZkCacheSub = new Dictionary<string, HashSet<string>>();

        public string GetValue(string path)
        {
            if (dictZkCache.ContainsKey(path))
            {
                return dictZkCache[path];
            }

            return null;
        }

        public void SetValue(string path, string value)
        {
            lock (lockObject)
            {
                dictZkCache[path] = value;
            }
        }

        public void Print()
        {
            foreach (var item in dictZkCache)
            {
                Console.WriteLine(item.Key + " --> " + item.Value);
            }
        }

        public IDictionary<string, string> GetAll()
        {
            return new ReadOnlyDictionary<string, string>(dictZkCache);
        }

        public void RemoveValue(string path)
        {
            lock (this)
            {
                if (dictZkCache.ContainsKey(path))
                {
                    dictZkCache.Remove(path);
                }
            }
        }

        public bool Exists(string path)
        {
            return dictZkCache.ContainsKey(path);
        }

        public void AddSub(string path, string sub)
        {
            if(!dictZkCacheSub.ContainsKey(path))
            {
                dictZkCacheSub[path] = new HashSet<string>();
            }

            dictZkCacheSub[path].Add(sub);
        }

        public void ClearSub(string path)
        {
            if (dictZkCacheSub.ContainsKey(path))
            {
                dictZkCacheSub[path].Clear();
            }
        }

        public IList<string> GetAllSub(string path)
        {
            if (dictZkCacheSub.ContainsKey(path))
            {
                return new ReadOnlyCollection<string>(dictZkCacheSub[path].ToList());
            }

            return new List<string>();
        }
    }
}
