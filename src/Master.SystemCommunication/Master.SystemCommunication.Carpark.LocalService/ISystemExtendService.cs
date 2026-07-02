using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Extend;

namespace Master.SystemCommunication.Carpark.LocalService;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ISystemExtendServiceCallBack))]
public interface ISystemExtendService : IService, ILongConnection, ICommunicationExtend
{
}
