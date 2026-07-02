using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CarPark.Core;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Extend;
using Master.SystemCommunication.LPRSInterface;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 收費代理
/// </summary>
public class CashierServiceProxy : DuplexClientBase<ICashierService>, ICashierService, ILPRSContrast, IFee, IService, ILongConnection, IDisability, IGateStatusEvent, ISystemEvent, IParkingSpaces, ICommunicationExtend, ILicensePlatePayment
{
	public CashierServiceProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public CashierServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public CashierServiceProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public CashierServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public CashierServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public void RecordContrast(RecordContrastArgs recordContrast)
	{
		base.Channel.RecordContrast(recordContrast);
	}

	public void ExitContrast(ExitContrastArgs exitContrast)
	{
		base.Channel.ExitContrast(exitContrast);
	}

	public bool ManualUpBar(ManualUpBarArgs manualUpBarArgs)
	{
		return base.Channel.ManualUpBar(manualUpBarArgs);
	}

	public bool ManualChange(ManualChangeArgs manualChangeArgs)
	{
		return base.Channel.ManualChange(manualChangeArgs);
	}

	public void Connect()
	{
		base.Channel.Connect();
	}

	public void Disconnect()
	{
		base.Channel.Disconnect();
	}

	public void Test()
	{
		base.Channel.Test();
	}

	public bool Join(ProgramInfo programInfo)
	{
		return base.Channel.Join(programInfo);
	}

	public void AgainCamera(AgainCameraArgs againCameraArgs)
	{
		base.Channel.AgainCamera(againCameraArgs);
	}

	public Dictionary<int, GateObj> GetDeviceStatus()
	{
		return base.Channel.GetDeviceStatus();
	}

	public bool UpdateParkAreaExtend(ParkAreaExtendArgs parkAreaExtendArgs)
	{
		return base.Channel.UpdateParkAreaExtend(parkAreaExtendArgs);
	}

	public bool RefreshSystem(int args)
	{
		return base.Channel.RefreshSystem(args);
	}

	public void DisabilityPress(DisabilityPressArgs disabilityPressArgs)
	{
		base.Channel.DisabilityPress(disabilityPressArgs);
	}

	public void SystemNotice(NoticeArgs noticeArgs)
	{
		base.Channel.SystemNotice(noticeArgs);
	}

	public void RunListen()
	{
		base.Channel.RunListen();
	}

	public CalcTicketFeeReturn CalcTicketFee(CalcTicketFeeArgs calcTicketFeeArgs, EnumParkType parkType, EnumBillType billType, out ChargeRecord chargeRecord)
	{
		return base.Channel.CalcTicketFee(calcTicketFeeArgs, parkType, billType, out chargeRecord);
	}

	public SaveChargeRecordReturn SaveChargeRecord(SaveChargeRecordArgs saveChargeRecordArgs, EnumParkType parkType, ChargeRecord chargeRecord)
	{
		return base.Channel.SaveChargeRecord(saveChargeRecordArgs, parkType, chargeRecord);
	}

	public GetCurrShiftRecordReturn GetCurrShiftRecord(GetCurrShiftRecordArgs getCurrShiftRecordArgs, out ShiftRecord shiftRecord)
	{
		return base.Channel.GetCurrShiftRecord(getCurrShiftRecordArgs, out shiftRecord);
	}

	public GetCurrChargeRecordReturn GetCurrChargeRecord(GetCurrChargeRecordArgs getCurrChargeRecordArgs, out List<ChargeRecord> chargeRecords)
	{
		return base.Channel.GetCurrChargeRecord(getCurrChargeRecordArgs, out chargeRecords);
	}

	public EndCurrShiftRecordReturn EndCurrShiftRecord(EndCurrShiftRecordArgs endCurrShiftRecordArgs, out ShiftRecord shiftRecord)
	{
		return base.Channel.EndCurrShiftRecord(endCurrShiftRecordArgs, out shiftRecord);
	}

	public GetFreeTypeAndFreeTenatReturn GetFreeTypeAndFreeTenat(GetFreeTypeAndFreeTenatArgs getFreeTypeAndFreeTenatArgs, out List<CustomFreeType> customFreeTypes, out List<CustomFreeTenat> customFreeTenats)
	{
		return base.Channel.GetFreeTypeAndFreeTenat(getFreeTypeAndFreeTenatArgs, out customFreeTypes, out customFreeTenats);
	}

	public GetTransactionDataReturn GetTransactionData(GetTransactionDataArgs getTransactionDataArgs, out TransactionData transactionData)
	{
		return base.Channel.GetTransactionData(getTransactionDataArgs, out transactionData);
	}

	public CreateLostReturn CreateLost(CreateLostArgs createLostArgs)
	{
		return base.Channel.CreateLost(createLostArgs);
	}

	public CashierLoginReturn CashierLogin(CashierLoginArgs cashierLoginArgs, out StaffInfo staffInfo, out List<SysRole> SysRoles)
	{
		return base.Channel.CashierLogin(cashierLoginArgs, out staffInfo, out SysRoles);
	}

	public UpdatePasswordReturn UpdatePassword(UpdatePasswordArgs updatePasswordArgs)
	{
		return base.Channel.UpdatePassword(updatePasswordArgs);
	}

	public GetInitInfoReturn GetInitInfo(GetInitInfoArgs getInitInfoArgs, out List<CardType> CardTypes, out List<StaffType> StaffTypes, out List<ParkGate> ParkGates, out List<ParkArea> ParkAreas, out List<ParkAreaExtend> ParkAreaExtends, out ShiftRecord shiftRecord, out CompanyInfo companyInfo)
	{
		return base.Channel.GetInitInfo(getInitInfoArgs, out CardTypes, out StaffTypes, out ParkGates, out ParkAreas, out ParkAreaExtends, out shiftRecord, out companyInfo);
	}

	public GetParkAreaReturn GetParkAreaExtend(GetParkAreaArgs getParkAreaExtendArgs, out List<ParkAreaExtend> parkAreaExtends, out List<ParkArea> parkAreas)
	{
		return base.Channel.GetParkAreaExtend(getParkAreaExtendArgs, out parkAreaExtends, out parkAreas);
	}

	public ModifyParkAreaExtendReturn ModifyParkAreaExtend(ModifyParkAreaExtendArgs modifyParkAreaExtendArgs, ParkAreaExtend parkAreaExtend)
	{
		return base.Channel.ModifyParkAreaExtend(modifyParkAreaExtendArgs, parkAreaExtend);
	}

	public AddTimeChargeReturn AddTimeCharge(AddTimeChargeArgs addTimeChargeArgs, TimeCharge timeCharge)
	{
		return base.Channel.AddTimeCharge(addTimeChargeArgs, timeCharge);
	}

	public DeleteTimeChargeReturn DeleteTimeCharge(DeleteTimeChargeArgs deleteTimeChargeArgs, TimeCharge timeCharge)
	{
		return base.Channel.DeleteTimeCharge(deleteTimeChargeArgs, timeCharge);
	}

	public UpdateTimeChargeReturn UpdateTimeCharge(UpdateTimeChargeArgs updateTimeChargeArgs, TimeCharge timeCharge, List<TimeChargeExt> timeChargeExt)
	{
		return base.Channel.UpdateTimeCharge(updateTimeChargeArgs, timeCharge, timeChargeExt);
	}

	public GetTimeChargeReturn GetTimeCharge(GetTimeChargeArgs getTimeChargeArgs, out List<TimeCharge> timeCharges, out List<TimeChargeExt> timeChargeExt)
	{
		return base.Channel.GetTimeCharge(getTimeChargeArgs, out timeCharges, out timeChargeExt);
	}

	public TimeCompensationFareReturn TimeCompensationFare(TimeCompensationFareArgs timeCompensationFareArgs, out TransactionData transactionData)
	{
		return base.Channel.TimeCompensationFare(timeCompensationFareArgs, out transactionData);
	}

	public GetLastChargeRecordReturn GetLastChargeRecord(GetLastChargeRecordArgs getLastChargeRecordArgs, out ChargeRecord chargeRecord, out MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail, out TransactionData transactionData)
	{
		return base.Channel.GetLastChargeRecord(getLastChargeRecordArgs, out chargeRecord, out mPass_POS_Transaction_Detail, out transactionData);
	}

	public bool SaveVoidCharge(VoidCharge voidCharge)
	{
		return base.Channel.SaveVoidCharge(voidCharge);
	}

	public SaveChargeRecordReturn SaveElectronicChargeRecord(SaveChargeRecordArgs saveChargeRecordArgs, EnumParkType parkType, ChargeRecord chargeRecord, MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail, BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend, BOC_N910_POS_Card_Payment_DetailEX bOC_N910_POS_Card_Payment_DetailEX = null, ScanPayment scanPayment = null)
	{
		return base.Channel.SaveElectronicChargeRecord(saveChargeRecordArgs, parkType, chargeRecord, mPass_POS_Transaction_Detail, bOC_Gate_TransactionExtend, bOC_N910_POS_Card_Payment_DetailEX, scanPayment);
	}

	public SaveMPassChargeReturn SaveMPassCharge(SaveMPassChargeArgs saveMPassChargeArgs, ChargeRecord chargeRecord, MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail, BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend, BOC_N910_POS_Card_Payment_DetailEX bOC_N910_POS_Card_Payment_DetailEX = null, ScanPayment scanPayment = null)
	{
		return base.Channel.SaveMPassCharge(saveMPassChargeArgs, chargeRecord, mPass_POS_Transaction_Detail, bOC_Gate_TransactionExtend);
	}

	public GetRentalChargeReturn GetRentalCharge(GetRentalChargeArgs getRentalChargeArgs, out Card card, out RentalType rentalType)
	{
		return base.Channel.GetRentalCharge(getRentalChargeArgs, out card, out rentalType);
	}

	public SaveMPass_POS_SigninReturn SaveMPass_POS_Signin(SaveMPass_POS_SigninArgs SaveMPass_POS_SigninArgs, MPass_POS_Signin mPass_POS_Signin, MPass_POS_Signin_Detail mPass_POS_Signin_Detail)
	{
		return base.Channel.SaveMPass_POS_Signin(SaveMPass_POS_SigninArgs, mPass_POS_Signin, mPass_POS_Signin_Detail);
	}

	public GetCardInfoReturn GetCardInfo(GetCardInfoArgs getCardInfoArgs, out List<Card> cardList)
	{
		return base.Channel.GetCardInfo(getCardInfoArgs, out cardList);
	}

	public GetView_TransactionAndLPReturn GetView_TransactionAndLP(GetView_TransactionAndLPArgs getView_TransactionAndLPArgs, out List<view_transactionandlp> _view_transactionandlp)
	{
		return base.Channel.GetView_TransactionAndLP(getView_TransactionAndLPArgs, out _view_transactionandlp);
	}

	public void ExtendRequestInterface(RequestArgs requestArgs)
	{
		base.Channel.ExtendRequestInterface(requestArgs);
	}

	public ResponseArgs ExtendRequestResponseInterface(RequestArgs requestArgs)
	{
		return base.Channel.ExtendRequestResponseInterface(requestArgs);
	}

	public CorrectLicensePlateReturn CorrectLicensePlate(CorrectLicensePlateArgs correctLicensePlateArgs)
	{
		return base.Channel.CorrectLicensePlate(correctLicensePlateArgs);
	}

	public ManualEntryLicensePlateReturn ManualEntryLicensePlate(ManualEntryLicensePlateArgs manualEntryLicensePlateArgs, EnumParkType parkType, EnumBillType billType)
	{
		return base.Channel.ManualEntryLicensePlate(manualEntryLicensePlateArgs, parkType, billType);
	}

	public CalcTicketFeeReturnV2 CalcTicketFeeV2(CalcTicketFeeArgsV2 calcTicketFeeArgs, EnumParkType parkType, EnumBillType billType, out ChargeRecord chargeRecord)
	{
		return base.Channel.CalcTicketFeeV2(calcTicketFeeArgs, parkType, billType, out chargeRecord);
	}

	public DateTime GetSynTime()
	{
		return base.Channel.GetSynTime();
	}
}
