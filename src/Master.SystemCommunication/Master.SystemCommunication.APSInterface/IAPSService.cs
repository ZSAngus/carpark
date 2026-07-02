using System;
using System.Collections.Generic;
using System.ServiceModel;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.APSInterface;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IAPSServiceCallBack))]
public interface IAPSService : IService
{
	/// <summary>
	/// 計算收費
	/// </summary>
	/// <param name="calcChargeArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ChargeRecord CalcCharge(CalcChargeArgs calcChargeArgs);

	/// <summary>
	/// 有優惠
	/// </summary>
	/// <param name="calcChargeArgs"></param>
	/// <param name="discount"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ChargeRecord CalcChargeByDiscount(CalcChargeArgs calcChargeArgs, barsys_Discount discount);

	/// <summary>
	/// 保存收費
	/// </summary>
	/// <param name="aps_TimeChargeRecord"></param>
	/// <param name="discount"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool SaveCharge(ChargeRecord chargeRecord, APS_TimeChargeRecord aps_TimeChargeRecord, barsys_Discount discount);

	/// <summary>
	/// 重新上傳收費
	/// </summary>
	/// <param name="aps_TimeChargeRecord"></param>
	/// <param name="customFreeRecord"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool SaveChargeRecord(ChargeRecord chargeRecord, APS_TimeChargeRecord aps_TimeChargeRecord, CustomFreeRecord customFreeRecord);

	/// <summary>
	/// 檢查優惠券是否有效
	/// </summary>
	/// <param name="checkBarsys_DiscountArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	barsys_Discount CheckBarsys_Discount(CheckBarsys_DiscountArgs checkBarsys_DiscountArgs);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	TransactionData DamageTicket(DamageTicketArgs DamageTicketArgs);

	/// <summary>
	/// Aps機站臺號
	/// </summary>
	/// <param name="StationID"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	APS_TimeChargeRecord GetListAPS_TimeChargeRecord(int StationID);

	/// <summary>
	/// 取場號
	/// </summary>
	/// <param name="getSerialArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetSerialArgs GetSerial(GetSerialArgs getSerialArgs);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	TransactionData LostTicket(LostTicketArgs lostTicketArgs);

	/// <summary>
	/// 轉更
	/// </summary>
	/// <param name="endShiftArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ShiftRecord EndShift(EndShiftArgs endShiftArgs);

	/// <summary>
	///             取系統時間
	/// </summary>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	DateTime GetSystemTime();

	/// <summary>
	/// 取更次詳細信息
	/// </summary>
	/// <param name="shiftRecordID"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	List<APS_ShiftRecordDetailed> EndShiftAPS_ShiftRecordDetailed(string shiftRecordID);

	/// <summary>
	/// 憑證使用記錄
	/// </summary>
	/// <param name="aps_CertificateUseRecord"></param>
	/// <param name="staffCode"></param>
	/// <param name="isInsert"></param>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool APS_CertificateUse(APS_CertificateUseRecord aps_CertificateUseRecord, string staffCode, bool isInsert);

	/// <summary>
	/// 獲取aps系統參數
	/// </summary>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	APS_SystemSetting GetApsSystemSetting();

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	StaffInfo APSLogin(APSLoginArgs apsLoginArgs);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	List<SysRole> GetStaffTypeRole(APSLoginArgs apsLoginArgs);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	APS_Certificate FindCertificateInfo(FindCertificateArgs findCertificateArgs);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	APS_Syslog SaveSystemLog(APS_Syslog aps_Syslog);
}
