using System.IO;
using System.ServiceModel;

namespace WcfPowerChat.Contracts
{
    [ServiceContract]
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void LoginResponse(bool ok, string msg);
        [OperationContract(IsOneWay = true)]
        void LogoutResponse(bool ok, string msg);
        [OperationContract(IsOneWay = true)]
        void ShowText(string text);
        [OperationContract(IsOneWay = true)]
        void ShowImage(Stream image);
        [OperationContract(IsOneWay = true)]
        void ShowFile(Stream file);
    }
}
