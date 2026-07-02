using System;
using System.ServiceModel;
using CarPark.Core;
using CarPark.DB;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 車牌付接收費處接口
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required)]
public interface ILicensePlatePayment
{
	/// <summary>
	///             糾正車牌
	/// </summary>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	CorrectLicensePlateReturn CorrectLicensePlate(CorrectLicensePlateArgs correctLicensePlateArgs);

	/// <summary>
	/// 補錄入場車牌
	/// </summary>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ManualEntryLicensePlateReturn ManualEntryLicensePlate(ManualEntryLicensePlateArgs manualEntryLicensePlateArgs, EnumParkType parkType, EnumBillType billType);

	/// <summary>
	/// 計算收費2.0
	/// </summary>
	/// <param name="calcTicketFeeArgs"></param>
	/// <param name="chargeRecord"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	CalcTicketFeeReturnV2 CalcTicketFeeV2(CalcTicketFeeArgsV2 calcTicketFeeArgs, EnumParkType parkType, EnumBillType billType, out ChargeRecord chargeRecord);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	DateTime GetSynTime();
}
