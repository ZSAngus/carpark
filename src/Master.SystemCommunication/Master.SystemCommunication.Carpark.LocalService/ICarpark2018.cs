using System;
using System.ServiceModel;
using CarPark.Core;
using CarPark.DB;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 2018版車場接口
/// 時租、月租、銀聯、澳門通
/// Roger Zhang20171215
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required)]
public interface ICarpark2018
{
	/// <summary>
	/// 獲取月租卡信息，用於離線通行
	/// </summary>
	/// <param name="getOfflineCardArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetOfflineCardReturn GetOfflineCard(GetOfflineCardArgs getOfflineCardArgs);

	/// <summary>
	/// 保存澳门通打包数据, ReaderNo
	/// </summary>          SIString
	/// <param name="packMPassTransactionDataArgs"></param>
	/// <param name="mPass_Gate_Transaction_SIStrings"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	PackMPassTransactionDataReturn PackMPassTransactionData(PackMPassTransactionDataArgs packMPassTransactionDataArgs, MPass_Gate_Transaction_SIStrings mPass_Gate_Transaction_SIStrings);

	/// <summary>
	/// 更新开闸状态
	/// </summary>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool UpdatePassTraceOpenGate(int PassTraceID);

	/// <summary>
	/// 更新车牌表信息
	/// </summary>
	/// <param name="updateLicensePlate_PassTraceArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool UpdateLicensePlate_PassTrace(UpdateLicensePlate_PassTraceArgs updateLicensePlate_PassTraceArgs);

	/// <summary>
	/// 保存通过轨迹
	/// </summary>
	/// <param name="passTrace"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool SavePassTrace(PassTrace passTrace);

	/// <summary>
	/// 取服務器時間
	/// </summary>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	DateTime GetSynTime();

	/// <summary>
	/// 保存起杆次數
	/// </summary>
	/// <param name="saveUpBarrierCountArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	SaveUpBarrierCountReturn SaveUpBarrierCount(SaveUpBarrierCountArgs saveUpBarrierCountArgs);

	/// <summary>
	/// 時租入口
	/// </summary>
	/// <param name="enterTicketArgs"></param>
	/// <param name="parkType"></param>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	EnterTicketReturn EnterTicket(EnterTicketArgs enterTicketArgs, EnumParkType parkType);

	/// <summary>
	/// 月租入口
	/// </summary>
	/// <param name="enterMonthArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	EnterMonthReturn EnterMonth(EnterMonthArgs enterMonthArgs, EnumParkType parkType);

	/// <summary>
	/// 澳門通入口
	/// </summary>
	/// <param name="enterMPassArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	EnterMPassReturn EnterMPass(EnterMPassArgs enterMPassArgs, EnumParkType parkType, MPass_Gate_Transaction mPass_Gate_Transaction);

	/// <summary>
	///             銀聯入口
	/// </summary>
	/// <param name="enterQPassArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	EnterQPassReturn EnterQPass(EnterQPassArgs enterQPassArgs, EnumParkType parkType, BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend);

	/// <summary>
	/// 时租出口
	/// </summary>
	/// <param name="exitTicketArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ExitTicketReturn ExitTicket(ExitTicketArgs exitTicketArgs, EnumParkType parkType);

	/// <summary>
	/// 月租出口
	/// </summary>
	/// <param name="exitMonthArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ExitMonthReturn ExitMonth(ExitMonthArgs exitMonthArgs, EnumParkType parkType);

	/// <summary>
	/// 出口澳门通校验并计算费用
	/// </summary>
	/// <param name="exitMPassCalcCheckArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ExitMPassCalcCheckReturn ExitMPassCalcCheck(ExitMPassCalcCheckArgs exitMPassCalcCheckArgs, EnumParkType parkType, out ChargeRecord chargeRecord);

	/// <summary>
	/// 出口澳门通保存扣值结果
	/// </summary>
	/// <param name="exitMPassSaveArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ExitMPassSaveReturn ExitMPassSave(ExitMPassSaveArgs exitMPassSaveArgs, EnumParkType parkType, ChargeRecord chargeRecord, MPass_Gate_Transaction mPass_Gate_Transaction);

	/// <summary>
	/// 出口校验闪付并计算收费用
	/// </summary>
	/// <param name="exitQPasssCalcCheckArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ExitQPassCalcCheckReturn ExitQPassCalcCheck(ExitQPassCalcCheckArgs exitQPassCalcCheckArgs, EnumParkType parkType, out ChargeRecord chargeRecord);

	/// <summary>
	/// 出口闪付保存扣值结果
	/// </summary>
	/// <param name="exitQPasssSaveArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ExitQPassSaveReturn ExitQPassSave(ExitQPassSaveArgs exitQPassSaveArgs, EnumParkType parkType, ChargeRecord chargeRecord, BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend);

	/// <summary>
	/// 入口檢測到車牌
	/// </summary>
	/// <param name="enterLicensePlateArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	EnterLicensePlateReturn EnterLicensePlate(EnterLicensePlateArgs enterLicensePlateArgs, EnumParkType parkType);

	/// <summary>
	///  出口檢測到車牌
	/// </summary>
	/// <param name="exitLicensePlateArgs"></param>
	/// <param name="parkType"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ExitLicensePlateReturn ExitLicensePlate(ExitLicensePlateArgs exitLicensePlateArgs, EnumParkType parkType);
}
