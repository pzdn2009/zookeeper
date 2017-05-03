using System.Collections.Generic;

namespace SigmalHex.Zookeeper
{
    public interface IZkCache
    {
        string GetValue(string path);

        bool Exists(string path);

        void SetValue(string path, string value);

        void RemoveValue(string path);

        void AddSub(string path, string sub);

        void ClearSub(string path);

        IList<string> GetAllSub(string path);

        IDictionary<string, string> GetAll();
    }
}
