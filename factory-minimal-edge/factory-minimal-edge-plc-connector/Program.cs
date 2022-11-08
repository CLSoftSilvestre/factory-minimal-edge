using System;
using S7.Net;
using System.Threading;

namespace factory_minimal_edge_plc_connector
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var plc = new Plc(CpuType.S71200, "169.254.190.100", 0, 0))
            {
                //IP is available
                plc.Open();

                if (plc.IsConnected)
                {
                    Console.WriteLine("Connected -> " + plc.IsConnected);
                    bool varm07 = false;
                    for (var x = 0; x < 10; x++)
                    {
                        Thread.Sleep(1000);
                        varm07 = (bool)plc.Read("M0.7");
                        Console.WriteLine(x + " - Variavel M0.7 - " + varm07);
                    }

                    Console.WriteLine("End of read plc Tag");
                }
                
            }
        }
    }
}
