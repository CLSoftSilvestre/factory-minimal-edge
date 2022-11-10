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

                    for (var x = 0; x < 10; x++)
                    {
                        Thread.Sleep(1000);
                        var dword = (uint)plc.Read("DB1.DBD2");
                        float result = dword.ConvertToFloat();
                        Console.WriteLine(x + " - Variavel DB1.DBD2 - " + result);
                    }

                    Console.WriteLine("End of read plc Tag");
                }
                
            }
        }
    }
}
