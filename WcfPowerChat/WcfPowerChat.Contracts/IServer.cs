using System.IO;
using System.ServiceModel;

namespace WcfPowerChat.Contracts
{
    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IServer
    {
        [OperationContract(IsOneWay = true)]
        void Login(string name);
        [OperationContract(IsOneWay = true)]
        void Logout();
        [OperationContract(IsOneWay = true)]
        void SendText(string text);
        [OperationContract(IsOneWay = true)]
        void SendImage(Stream image);
        [OperationContract(IsOneWay = true)]
        void SendFile(Stream file);
    }
}
