using System.Collections.Generic;
using System.Linq;
using ZooKeeperNet;

namespace SigmalHex.Zookeeper
{
    public class PathRoot
    {
        [NodePathOrder(1)]
        public virtual string ServiceType { get; set; }

        [NodePathOrder(2)]
        public virtual string SystemCode { get; set; }

        /// <summary>
        /// eg:DEV,TEST,UAT,PRD
        /// </summary>
        [NodePathOrder(3)]
        public virtual string Environment { get; set; }

        [NodePathOrder(4)]
        public virtual string Node { get; set; }

        public virtual string ToPath()
        {
            var dict = new SortedDictionary<int, string>();
            foreach (var pro in this.GetType().GetProperties())
            {
                var cusAttrs = pro.GetCustomAttributes(typeof(NodePathOrderAttribute), false);
                if (cusAttrs != null && cusAttrs.Length > 0)
                {
                    var val = pro.GetValue(this) ?? string.Empty;

                    dict.Add(((NodePathOrderAttribute)cusAttrs.First()).Order, val.ToString());
                }
            }

            var ret = "/";
            foreach (var item in dict)
            {
                ret = ZKPaths.MakePath(ret, item.Value);
            }

            return ret;
        }
    }
}
