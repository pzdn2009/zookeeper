using System;
using System.Text;

namespace SigmalHex.Zookeeper
{
    public class ZkSerialization : IZkSerialization
    {
        public bool Enable
        {
            get; set;
        }

        public void Deserilize(IZkCache zkCache, string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var arrs = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arrs.Length; i += 2)
                {
                    zkCache.SetValue(arrs[i], arrs[i + 1]);
                }
            }
        }

        public string Serialize(IZkCache zkCache)
        {
            var sb = new StringBuilder();
            foreach (var item in zkCache.GetAll())
            {
                sb.AppendLine(item.Key);
                sb.AppendLine(item.Value);
            }

            return sb.ToString();
        }
    }
}
