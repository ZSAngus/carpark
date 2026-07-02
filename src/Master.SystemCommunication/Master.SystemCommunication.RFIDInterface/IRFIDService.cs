using System.Collections.Generic;
using System.ServiceModel;
using CarPark.Core;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.RFIDInterface;

/// <summary>
/// rfid服務
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IRFIDServiceCallBack))]
public interface IRFIDService : IService
{
	/// <summary>
	/// 顯示屏顯示信息
	/// </summary>
	/// <param name="message"></param>
	[OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
	void ShowMessage(ShowMessageArgs showMessageArgs);

	/// <summary>
	/// 發出聲響
	/// </summary>
	/// <param name="second"></param>
	[OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
	void Beep(BeepArgs beepArgs);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool RFID_OpenGate(RFID_OpenGateArgs rfid_OpenGateArgs);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool ChangeTimeChargUse(ChangeTimeChargUseArgs changeTimeChargUseArgs, EnumParkType parkType);

	/// <summary>
	/// 車位數
	/// </summary>
	/// <param name="getTimeChargRemainArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	int GetTimeChargRemain(GetTimeChargRemainArgs getTimeChargRemainArgs, EnumParkType parkType);

	/// <summary>
	/// 取車位詳細列表
	/// </summary>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	List<GetParkAreaExtendArgs> GetParkAreaExtend();
}
