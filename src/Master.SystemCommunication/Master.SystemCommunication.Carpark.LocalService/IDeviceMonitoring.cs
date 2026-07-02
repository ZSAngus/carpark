using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

[ServiceContract(SessionMode = SessionMode.Required)]
public interface IDeviceMonitoring
{
	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void UploadGateStatus(GateStatusArgs gateStatus);

	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void UploadSingleGateStatus(DeviceStatus deviceStatus);
}
