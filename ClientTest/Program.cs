using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using ServiceTest;

namespace ClientTest
{
    internal class Program
    {
        private static readonly ChannelFactory<IService> channelFactory = new ChannelFactory<IService>("testEndpoint");

        public static void Main(string[] args)
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 5; i++)
            {
                tasks.Add(new Task(ScheduleCalls));
                tasks[i].Start();
            }

            Task.WaitAll(tasks.ToArray());
        }

        public static void ScheduleCalls()
        {
            Parallel.For(0, 10000, i =>
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
        }
    }
}