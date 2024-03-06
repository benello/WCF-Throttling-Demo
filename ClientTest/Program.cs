using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using ServiceTest;

namespace ClientTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var channelFactory = new ChannelFactory<IService>("testEndpoint");
            
            var threads = new List<Thread>();

            for (int i = 0; i < 100; i++)
            {
                var thread = new Thread(() =>
                {
                    try
                    {
                        var client = channelFactory.CreateChannel();
                        client.LongProcess();
                        ((IClientChannel)client).Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        // swallow
                    }
                });
                threads.Add(thread);
            }
            
            threads.ForEach(thread => thread.Start());

            threads.ForEach(thread => thread.Join());
        }
    }
}