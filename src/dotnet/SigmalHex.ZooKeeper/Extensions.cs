using System.Text;

namespace SigmalHex.Zookeeper
{
    public static class Extensions
    {
        public static string GetString(this byte[] data)
        {
            return GetString(data, Encoding.UTF8);
        }

        public static string GetString(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }
    }
}
