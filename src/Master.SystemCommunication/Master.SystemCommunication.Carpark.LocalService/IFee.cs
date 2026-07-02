using System.Collections.Generic;
using System.ServiceModel;
using CarPark.Core;
using CarPark.DB;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 收費系統接口定義
/// </summary>     
[ServiceContract(SessionMode = SessionMode.Required)]
public interface IFee
{
	/// <summary>
	/// 人手起閘
	/// </summary>
	/// <param name="gateID"></param>
	/// <param name="systemName">系統名稱</param>
	/// <param name="userName">用戶code</param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool ManualUpBar(ManualUpBarArgs manualUpBarArgs);

	/// <summary>
	/// 手動滿字
	/// </summary>       
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool ManualChange(ManualChangeArgs manualChangeArgs);

	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void AgainCamera(AgainCameraArgs againCameraArgs);

	/// <summary>
	/// 獲取閘機狀態
	/// </summary>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	Dictionary<int, GateObj> GetDeviceStatus();

	/// <summary>
	/// 更改車位
	/// </summary>
	/// <param name="parkAreaExtendArgs"></param>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool UpdateParkAreaExtend(ParkAreaExtendArgs parkAreaExtendArgs);

	/// <summary>
	/// 刷新系統
	/// </summary>
	/// <param name="parkAreaExtendArgs"></param>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool RefreshSystem(int args);

	/// <summary>
	/// 取入場信息
	/// </summary>
	/// <param name="getTransactionDataArgs"></param>
	/// <param name="transactionData"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetTransactionDataReturn GetTransactionData(GetTransactionDataArgs getTransactionDataArgs, out TransactionData transactionData);

	/// <summary>
	/// 生成一個失票號
	/// </summary>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	CreateLostReturn CreateLost(CreateLostArgs createLostArgs);

	/// <summary>
	/// 計算收費
	/// </summary>
	/// <param name="calcTicketFeeArgs"></param>
	/// <param name="chargeRecord"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	CalcTicketFeeReturn CalcTicketFee(CalcTicketFeeArgs calcTicketFeeArgs, EnumParkType parkType, EnumBillType billType, out ChargeRecord chargeRecord);

	/// <summary>
	/// 保存收費
	/// </summary>
	/// <param name="saveChargeRecordArgs"></param>
	/// <param name="chargeRecord"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	SaveChargeRecordReturn SaveChargeRecord(SaveChargeRecordArgs saveChargeRecordArgs, EnumParkType parkType, ChargeRecord chargeRecord);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetCurrShiftRecordReturn GetCurrShiftRecord(GetCurrShiftRecordArgs getCurrShiftRecordArgs, out ShiftRecord shiftRecord);

	/// <summary>
	/// 取當前收費記錄
	/// </summary>
	/// <param name="getCurrChargeRecordArgs"></param>
	/// <param name="chargeRecords"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetCurrChargeRecordReturn GetCurrChargeRecord(GetCurrChargeRecordArgs getCurrChargeRecordArgs, out List<ChargeRecord> chargeRecords);

	/// <summary>
	/// 結更
	/// </summary>
	/// <param name="endCurrShiftRecordArgs"></param>
	/// <param name="shiftRecord">結更的數據</param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	EndCurrShiftRecordReturn EndCurrShiftRecord(EndCurrShiftRecordArgs endCurrShiftRecordArgs, out ShiftRecord shiftRecord);

	/// <summary>
	/// 獲取免費商戶同類型
	/// </summary>
	/// <param name="getFreeTypeAndFreeTenatArgs"></param>
	/// <param name="customFreeTypes"></param>
	/// <param name="customFreeTenats"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetFreeTypeAndFreeTenatReturn GetFreeTypeAndFreeTenat(GetFreeTypeAndFreeTenatArgs getFreeTypeAndFreeTenatArgs, out List<CustomFreeType> customFreeTypes, out List<CustomFreeTenat> customFreeTenats);

	/// <summary>
	/// 收費處登錄
	/// </summary>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	CashierLoginReturn CashierLogin(CashierLoginArgs cashierLoginArgs, out StaffInfo staffInfo, out List<SysRole> SysRoles);

	/// <summary>
	/// 修改密碼
	/// </summary>
	/// <param name="updatePasswordArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	UpdatePasswordReturn UpdatePassword(UpdatePasswordArgs updatePasswordArgs);

	/// <summary>
	/// 取初始化信息
	/// </summary>
	/// <param name="getInitInfoArgs"></param>
	/// <param name="CardTypes"></param>
	/// <param name="StaffTypes"></param>
	/// <param name="ParkGates"></param>
	/// <param name="ParkAreas"></param>
	/// <param name="ParkAreaExtends"></param>
	/// <param name="shiftRecord"></param>
	/// <param name="companyInfo"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetInitInfoReturn GetInitInfo(GetInitInfoArgs getInitInfoArgs, out List<CardType> CardTypes, out List<StaffType> StaffTypes, out List<ParkGate> ParkGates, out List<ParkArea> ParkAreas, out List<ParkAreaExtend> ParkAreaExtends, out ShiftRecord shiftRecord, out CompanyInfo companyInfo);

	/// <summary>
	/// 取所有區域車位信息
	/// </summary>
	/// <param name="getParkAreaExtendArgs"></param>
	/// <param name="parkAreaExtends"></param>
	/// <param name="parkAreas"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetParkAreaReturn GetParkAreaExtend(GetParkAreaArgs getParkAreaExtendArgs, out List<ParkAreaExtend> parkAreaExtends, out List<ParkArea> parkAreas);

	/// <summary>
	/// 更新車位信息
	/// </summary>
	/// <param name="updateParkAreaExtendArgs"></param>
	/// <param name="parkAreaExtend"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ModifyParkAreaExtendReturn ModifyParkAreaExtend(ModifyParkAreaExtendArgs modifyParkAreaExtendArgs, ParkAreaExtend parkAreaExtend);

	/// <summary>
	/// 添加收費規則
	/// </summary>
	/// <param name="addTimeChargeArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	AddTimeChargeReturn AddTimeCharge(AddTimeChargeArgs addTimeChargeArgs, TimeCharge timeCharge);

	/// <summary>
	/// 刪除收費規則
	/// </summary>
	/// <param name="deleteTimeChargeArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	DeleteTimeChargeReturn DeleteTimeCharge(DeleteTimeChargeArgs deleteTimeChargeArgs, TimeCharge timeCharge);

	/// <summary>
	/// 修改收費規則
	/// </summary>
	/// <param name="updateTimeChargeArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	UpdateTimeChargeReturn UpdateTimeCharge(UpdateTimeChargeArgs updateTimeChargeArgs, TimeCharge timeCharge, List<TimeChargeExt> timeChargeExt);

	/// <summary>
	/// 獲取所有收費規則
	/// </summary>
	/// <param name="getTimeChargeArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetTimeChargeReturn GetTimeCharge(GetTimeChargeArgs getTimeChargeArgs, out List<TimeCharge> timeCharges, out List<TimeChargeExt> timeChargeExt);

	/// <summary>
	/// 補票操作
	/// </summary>
	/// <param name="timeCompensationFareArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	TimeCompensationFareReturn TimeCompensationFare(TimeCompensationFareArgs timeCompensationFareArgs, out TransactionData transactionData);

	/// <summary>
	/// 打印收據
	/// </summary>
	/// <param name="getLastChargeRecordArgs"></param>
	/// <param name="chargeRecord"></param>
	/// <param name="mPass_POS_Transaction_Detail"></param>
	/// <param name="transactionData"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetLastChargeRecordReturn GetLastChargeRecord(GetLastChargeRecordArgs getLastChargeRecordArgs, out ChargeRecord chargeRecord, out MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail, out TransactionData transactionData);

	/// <summary>
	/// 保存退優惠信息
	/// </summary>
	/// <param name="voidCharge"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool SaveVoidCharge(VoidCharge voidCharge);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	SaveChargeRecordReturn SaveElectronicChargeRecord(SaveChargeRecordArgs saveChargeRecordArgs, EnumParkType parkType, ChargeRecord chargeRecord, MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail, BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend, BOC_N910_POS_Card_Payment_DetailEX bOC_N910_POS_Card_Payment_DetailEX = null, ScanPayment scanPayment = null);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	SaveMPassChargeReturn SaveMPassCharge(SaveMPassChargeArgs saveMPassChargeArgs, ChargeRecord chargeRecord, MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail, BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend, BOC_N910_POS_Card_Payment_DetailEX bOC_N910_POS_Card_Payment_DetailEX = null, ScanPayment scanPayment = null);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetRentalChargeReturn GetRentalCharge(GetRentalChargeArgs getRentalChargeArgs, out Card card, out RentalType rentalType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	SaveMPass_POS_SigninReturn SaveMPass_POS_Signin(SaveMPass_POS_SigninArgs SaveMPass_POS_SigninArgs, MPass_POS_Signin mPass_POS_Signin, MPass_POS_Signin_Detail mPass_POS_Signin_Detail);
}
