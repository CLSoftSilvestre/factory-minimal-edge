using System;
using S7.Net;

namespace factory_minimal_edge_plc_connector
{
    class Program
    {
        static void Main(string[] args)
        {
            Plc plc = new Plc(CpuType.Logo0BA8, "127.0.0.1", 0, 2);
            //plc.Open();

            Console.WriteLine("Hello World!");
            //plc.Close();
        }
    }
}
