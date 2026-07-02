using System.ServiceModel;
using CarPark.Core;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 收據打印接口
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required)]
public interface IReceiptData
{
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	EnterPrintData GetEnterLastPrintData(int GateID);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ExitPrintData GetExitLastPrintData(int GateID);
}
