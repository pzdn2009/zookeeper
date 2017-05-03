using System;

namespace SigmalHex.Zookeeper
{
    public class NodePathOrderAttribute : Attribute
    {
        public NodePathOrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; set; }
    }
}
