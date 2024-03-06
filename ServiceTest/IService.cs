using System.ServiceModel;

namespace ServiceTest
{
    [ServiceContract(SessionMode = SessionMode.Required, Namespace = "http://ServiceTest.test")]
    public interface IService
    {
        [OperationContract]
        void LongProcess();
    }
}