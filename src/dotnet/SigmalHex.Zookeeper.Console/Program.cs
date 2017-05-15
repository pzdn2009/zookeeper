namespace SigmalHex.Zookeeper.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConfigCenterTools t = new ConfigCenterTools();
            System.Console.WriteLine(t.GetConfig("/test")); 
            //System.Console.WriteLine(t.Watch("/test"));
            //System.Console.WriteLine(t.ZkCache.GetValue("/test"));
            System.Console.ReadLine();
        }
    }
}
