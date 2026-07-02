using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.RFIDInterface;

/// <summary>
/// RFID控制，讀
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IRFIDControlCallback))]
public interface IRFIDControlService : IService, ILongConnection
{
	[OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
	void ScanRFID(ScanRFIDArgs scanRFIDArgs);
}
