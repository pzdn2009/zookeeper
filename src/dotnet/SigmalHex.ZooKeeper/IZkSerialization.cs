namespace SigmalHex.Zookeeper
{
    public interface IZkSerialization
    {
        string Serialize(IZkCache zkCache);

        void Deserilize(IZkCache zkCache, string str);

        bool Enable { get; set; }
    }
}
