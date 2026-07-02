using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.CarparkCloud;

/// <summary>
/// 車場雲後臺接口
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IMCCloudServiceCallback))]
public interface IMCCloudService : IService, ILongConnection, IDeviceMonitoring
{
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool UPloadQPassChargeRecord(BOCFileArgs Record);

	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void ParkingSpacesChange(ParkingSpacesChangeArgs parkingSpacesChangeArgs);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool UploadPassTrace(PassTraceArgs gatePassTracee);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool UploadChargeRecord(ChargeRecordArgs chargeRecord);
}
