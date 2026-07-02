using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Extend;
using Master.SystemCommunication.LPRSInterface;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 收費處回調
/// </summary>
[ServiceContract]
public interface ICashierServiceCallBack : ILPRSContrastCallback, IFeeCallBack, ICallback, ILongConnectionCallBack, IDisabilityCallBack, IParkingSpacesEventCallback, IGateStatusEventCallback, ISystemEventCallback, ICallBackExtend, ILicensePlatePaymentCallBack
{
}
