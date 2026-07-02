using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 发布车场状态
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICarparkStatusServiceCallBack))]
public interface ICarparkStatusService : IService, ILongConnection, IGateStatusEvent, ISystemEvent, IParkingSpaces
{
}
