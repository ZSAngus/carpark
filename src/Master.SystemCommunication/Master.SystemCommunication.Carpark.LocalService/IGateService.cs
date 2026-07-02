using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.LPRSInterface;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 閘機服務
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IGateCallBack))]
public interface IGateService : IService, ICarpark, ILPRSData, IReceiptData, ILongConnection, IDeviceMonitoring, ILPRSContrast, IGateEvent, IDisability
{
}
