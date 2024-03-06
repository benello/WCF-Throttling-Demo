using System;
using System.ServiceModel;
using System.Threading;

namespace ServiceTest
{
    [ServiceBehavior(
        ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerSession)]
    public class Service 
        : IService
    {
        public void LongProcess()
        {
            Console.WriteLine($"Process received for thread {Environment.CurrentManagedThreadId}");
            Thread.Sleep(55000);
        }
    }
}