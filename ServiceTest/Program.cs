using System;
using System.ServiceModel;

namespace ServiceTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            var selfHost = new ServiceHost(typeof(Service));

            try
            {
                selfHost.Open();
                
                Console.WriteLine("Service started");
                Console.ReadKey();
                
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine($"An exception was encountered: {ce.Message}");
                selfHost.Abort();
                // swallow
            }
        }
    }
}