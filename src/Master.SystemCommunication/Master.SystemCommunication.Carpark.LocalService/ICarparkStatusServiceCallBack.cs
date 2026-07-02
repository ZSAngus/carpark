using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 车场状态回调
/// </summary>
[ServiceContract]
public interface ICarparkStatusServiceCallBack : ICallback, ILongConnectionCallBack, IParkingSpacesEventCallback, IGateStatusEventCallback, ISystemEventCallback
{
}
