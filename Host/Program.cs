using System;
using System.ServiceModel;


namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Chat.ServerUser)))
            {
                host.Open();
                Console.WriteLine("Start host");
                Console.ReadKey();
            }
        }
    }
}
