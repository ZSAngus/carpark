using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.LPRSInterface;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 閘機回調
/// </summary>
public interface IGateCallBack : ICallback, IGateEventCallBack, ILongConnectionCallBack, ILPRSContrastCallback, IDisabilityCallBack
{
	[OperationContract]
	bool OpenGate(int gateID);

	[OperationContract]
	void AgainCamera(AgainCameraArgs againCameraArgs);

	/// <summary>
	/// 手動滿字
	/// </summary>       
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool ManualChange(ManualChangeArgs manualChangeArgs);

	/// <summary>
	/// 人手起閘
	/// </summary>
	/// <param name="gateID"></param>
	/// <param name="systemName">系統名稱</param>
	/// <param name="userName">用戶code</param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool ManualUpBar(ManualUpBarArgs manualUpBarArgs);
}
