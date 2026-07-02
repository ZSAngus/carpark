using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CarPark.Core;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.LPRSInterface;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

public class GateServiceProxy : DuplexClientBase<IGateService>, IGateService, IService, ICarpark, ILPRSData, IReceiptData, ILongConnection, IDeviceMonitoring, ILPRSContrast, IGateEvent, IDisability
{
	public GateServiceProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public GateServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public GateServiceProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public GateServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public GateServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public GateResult EnterCarMove(int GateID, string realCardNum, int parkType)
	{
		return base.Channel.EnterCarMove(GateID, realCardNum, parkType);
	}

	public GateResult ExitCarMove(int GateID, string realCardNum, int parkType)
	{
		return base.Channel.ExitCarMove(GateID, realCardNum, parkType);
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

	public GateResult ExitQPassPURCHASE_CARD(ChargeRecord chargeRecord, BOC_Gate_TransactionExtend boc_transaction)
	{
		return base.Channel.ExitQPassPURCHASE_CARD(chargeRecord, boc_transaction);
	}

	public GateResult ExitMPassDeULMPC(ChargeRecord chargeRecord, MPass_Gate_Transaction mp_transaction)
	{
		return base.Channel.ExitMPassDeULMPC(chargeRecord, mp_transaction);
	}

	public GateResult EnterQPassCarMove(int GateID, int parkType, BOC_Gate_TransactionExtend bocTransaction)
	{
		return base.Channel.EnterQPassCarMove(GateID, parkType, bocTransaction);
	}

	public GateResult EnterMPassCarMove(int GateID, int parkType, MPass_Gate_Transaction mpass_Transaction)
	{
		return base.Channel.EnterMPassCarMove(GateID, parkType, mpass_Transaction);
	}

	public GateResultEX EnterHourly(int GateID, string ticketNo, int parkType)
	{
		return base.Channel.EnterHourly(GateID, ticketNo, parkType);
	}

	public GateResultEX ExitHourly(int GateID, string ticketNo, int parkType)
	{
		return base.Channel.ExitHourly(GateID, ticketNo, parkType);
	}

	public GateResultEX CheckMPassExit(int GateID, string cardNum, EnumParkType parkType)
	{
		return base.Channel.CheckMPassExit(GateID, cardNum, parkType);
	}

	public GateResultEX CheckQPassExit(int GateID, string cardNum, EnumParkType parkType)
	{
		return base.Channel.CheckQPassExit(GateID, cardNum, parkType);
	}

	public GateResultEX ExitMPassLPRS(ChargeRecord chargeRecord, MPass_Gate_Transaction mp_transaction, LPRSOptions lprsOptions)
	{
		return base.Channel.ExitMPassLPRS(chargeRecord, mp_transaction, lprsOptions);
	}

	public GateResultEX ExitQPassLPRS(ChargeRecord chargeRecord, BOC_Gate_TransactionExtend boc_transaction, LPRSOptions lprsOptions)
	{
		return base.Channel.ExitQPassLPRS(chargeRecord, boc_transaction, lprsOptions);
	}

	public bool SaveEnterPasstrace(string PassCode, string CardNum, int gateID, EnumPassStatus status, EnumParkType parktype, int? rentalTypeID, DateTime passTime, int billType)
	{
		return base.Channel.SaveEnterPasstrace(PassCode, CardNum, gateID, status, parktype, rentalTypeID, passTime, billType);
	}

	public bool SaveExitPasstrace(string PassCode, string CardNum, int gateID, EnumPassStatus status, EnumParkType parktype, int? rentalTypeID, DateTime passTime, int billType)
	{
		return base.Channel.SaveExitPasstrace(PassCode, CardNum, gateID, status, parktype, rentalTypeID, passTime, billType);
	}

	public bool CheckIsOpenGate(LPRSobject obj)
	{
		return base.Channel.CheckIsOpenGate(obj);
	}

	public bool UpdatePassRecord(LPRSobject obj)
	{
		return base.Channel.UpdatePassRecord(obj);
	}

	public bool UpdatePassTrace(int passID)
	{
		return base.Channel.UpdatePassTrace(passID);
	}

	public EnterPrintData GetEnterLastPrintData(int GateID)
	{
		return base.Channel.GetEnterLastPrintData(GateID);
	}

	public ExitPrintData GetExitLastPrintData(int GateID)
	{
		return base.Channel.GetExitLastPrintData(GateID);
	}

	public GateResultEX EnterMPassLPRSCarMove(int GateID, int parkType, MPass_Gate_Transaction mpass_Transaction)
	{
		return base.Channel.EnterMPassLPRSCarMove(GateID, parkType, mpass_Transaction);
	}

	public GateResultEX EnterQPassLPRSCarMove(int GateID, int parkType, BOC_Gate_TransactionExtend bocTransaction)
	{
		return base.Channel.EnterQPassLPRSCarMove(GateID, parkType, bocTransaction);
	}

	public GateResultEX EnterCarLPRSMove(int GateID, string realCardNum, int parkType)
	{
		return base.Channel.EnterCarLPRSMove(GateID, realCardNum, parkType);
	}

	public GateResultEX ExitCarLPRSMove(int GateID, string realCardNum, int parkType)
	{
		return base.Channel.ExitCarLPRSMove(GateID, realCardNum, parkType);
	}

	public bool Join(ProgramInfo programInfo)
	{
		return base.Channel.Join(programInfo);
	}

	public int EnterTicket(int GateID, string TicketNum, EnumParkType parkType, DateTime PassTime, EnumCardType cardType)
	{
		return base.Channel.EnterTicket(GateID, TicketNum, parkType, PassTime, cardType);
	}

	public int ExitTicket(int GateID, string TicketNum, EnumParkType parkType, DateTime PassTime, EnumCardType cardType)
	{
		return base.Channel.ExitTicket(GateID, TicketNum, parkType, PassTime, cardType);
	}

	public void RecordContrast(RecordContrastArgs c_RecordContrast)
	{
		base.Channel.RecordContrast(c_RecordContrast);
	}

	public void ExitContrast(ExitContrastArgs c_ExitContrast)
	{
		base.Channel.ExitContrast(c_ExitContrast);
	}

	public void UploadGateStatus(GateStatusArgs gateStatus)
	{
		base.Channel.UploadGateStatus(gateStatus);
	}

	public void UploadSingleGateStatus(DeviceStatus deviceStatus)
	{
		base.Channel.UploadSingleGateStatus(deviceStatus);
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
}
