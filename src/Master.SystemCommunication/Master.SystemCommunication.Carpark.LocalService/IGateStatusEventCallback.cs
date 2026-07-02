using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 閘機設備狀態改變
/// </summary>
[ServiceContract]
public interface IGateStatusEventCallback
{
	[OperationContract]
	void SingleGateStatusChangeNotice(DeviceStatus deviceStatus);
}
