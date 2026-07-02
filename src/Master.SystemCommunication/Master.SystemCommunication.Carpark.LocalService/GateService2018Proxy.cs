using System;
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

public class GateService2018Proxy : DuplexClientBase<IGateService2018>, IGateService2018, ICarpark2018, ILongConnection, IService, IReceiptData, IDeviceMonitoring, ILPRSContrast, IDisability, IParkingSpaces, ICommunicationExtend
{
	public GateService2018Proxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public GateService2018Proxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public GateService2018Proxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public GateService2018Proxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public GateService2018Proxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public bool UpdatePassTraceOpenGate(int PassTraceID)
	{
		return base.Channel.UpdatePassTraceOpenGate(PassTraceID);
	}

	public bool UpdateLicensePlate_PassTrace(UpdateLicensePlate_PassTraceArgs updateLicensePlate_PassTraceArgs)
	{
		return base.Channel.UpdateLicensePlate_PassTrace(updateLicensePlate_PassTraceArgs);
	}

	public bool SavePassTrace(PassTrace passTrace)
	{
		return base.Channel.SavePassTrace(passTrace);
	}

	public EnterTicketReturn EnterTicket(EnterTicketArgs enterTicketArgs, EnumParkType parkType)
	{
		return base.Channel.EnterTicket(enterTicketArgs, parkType);
	}

	public EnterMonthReturn EnterMonth(EnterMonthArgs enterMonthArgs, EnumParkType parkType)
	{
		return base.Channel.EnterMonth(enterMonthArgs, parkType);
	}

	public EnterMPassReturn EnterMPass(EnterMPassArgs enterMPassArgs, EnumParkType parkType, MPass_Gate_Transaction mPass_Gate_Transaction)
	{
		return base.Channel.EnterMPass(enterMPassArgs, parkType, mPass_Gate_Transaction);
	}

	public EnterQPassReturn EnterQPass(EnterQPassArgs enterQPassArgs, EnumParkType parkType, BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend)
	{
		return base.Channel.EnterQPass(enterQPassArgs, parkType, bOC_Gate_TransactionExtend);
	}

	public ExitTicketReturn ExitTicket(ExitTicketArgs exitTicketArgs, EnumParkType parkType)
	{
		return base.Channel.ExitTicket(exitTicketArgs, parkType);
	}

	public ExitMonthReturn ExitMonth(ExitMonthArgs exitMonthArgs, EnumParkType parkType)
	{
		return base.Channel.ExitMonth(exitMonthArgs, parkType);
	}

	public ExitMPassCalcCheckReturn ExitMPassCalcCheck(ExitMPassCalcCheckArgs exitMPassCalcCheckArgs, EnumParkType parkType, out ChargeRecord chargeRecord)
	{
		return base.Channel.ExitMPassCalcCheck(exitMPassCalcCheckArgs, parkType, out chargeRecord);
	}

	public ExitMPassSaveReturn ExitMPassSave(ExitMPassSaveArgs exitMPassSaveArgs, EnumParkType parkType, ChargeRecord chargeRecord, MPass_Gate_Transaction mPass_Gate_Transaction)
	{
		return base.Channel.ExitMPassSave(exitMPassSaveArgs, parkType, chargeRecord, mPass_Gate_Transaction);
	}

	public ExitQPassCalcCheckReturn ExitQPassCalcCheck(ExitQPassCalcCheckArgs exitQPassCalcCheckArgs, EnumParkType parkType, out ChargeRecord chargeRecord)
	{
		return base.Channel.ExitQPassCalcCheck(exitQPassCalcCheckArgs, parkType, out chargeRecord);
	}

	public ExitQPassSaveReturn ExitQPassSave(ExitQPassSaveArgs exitQPassSaveArgs, EnumParkType parkType, ChargeRecord chargeRecord, BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend)
	{
		return base.Channel.ExitQPassSave(exitQPassSaveArgs, parkType, chargeRecord, bOC_Gate_TransactionExtend);
	}

	public bool Join(ProgramInfo programInfo)
	{
		return base.Channel.Join(programInfo);
	}

	public void RunListen()
	{
		base.Channel.RunListen();
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

	public EnterLicensePlateReturn EnterLicensePlate(EnterLicensePlateArgs enterLicensePlateArgs, EnumParkType parkType)
	{
		return base.Channel.EnterLicensePlate(enterLicensePlateArgs, parkType);
	}

	public ExitLicensePlateReturn ExitLicensePlate(ExitLicensePlateArgs exitLicensePlateArgs, EnumParkType parkType)
	{
		return base.Channel.ExitLicensePlate(exitLicensePlateArgs, parkType);
	}

	public EnterPrintData GetEnterLastPrintData(int GateID)
	{
		return base.Channel.GetEnterLastPrintData(GateID);
	}

	public ExitPrintData GetExitLastPrintData(int GateID)
	{
		return base.Channel.GetExitLastPrintData(GateID);
	}

	public void UploadGateStatus(GateStatusArgs gateStatus)
	{
		base.Channel.UploadGateStatus(gateStatus);
	}

	public void UploadSingleGateStatus(DeviceStatus deviceStatus)
	{
		base.Channel.UploadSingleGateStatus(deviceStatus);
	}

	public void RecordContrast(RecordContrastArgs recordContrast)
	{
		base.Channel.RecordContrast(recordContrast);
	}

	public void ExitContrast(ExitContrastArgs exitContrast)
	{
		base.Channel.ExitContrast(exitContrast);
	}

	public void DisabilityPress(DisabilityPressArgs disabilityPressArgs)
	{
		base.Channel.DisabilityPress(disabilityPressArgs);
	}

	public DateTime GetSynTime()
	{
		return base.Channel.GetSynTime();
	}

	public SaveUpBarrierCountReturn SaveUpBarrierCount(SaveUpBarrierCountArgs saveUpBarrierCountArgs)
	{
		return base.Channel.SaveUpBarrierCount(saveUpBarrierCountArgs);
	}

	public GetParkGateReturn GetParkGate(GetParkGateArgs getParkGateArgs, out ParkGate parkAreaExtends)
	{
		return base.Channel.GetParkGate(getParkGateArgs, out parkAreaExtends);
	}

	public GetOfflineCardReturn GetOfflineCard(GetOfflineCardArgs getOfflineCardArgs)
	{
		return base.Channel.GetOfflineCard(getOfflineCardArgs);
	}

	public PackMPassTransactionDataReturn PackMPassTransactionData(PackMPassTransactionDataArgs packMPassTransactionDataArgs, MPass_Gate_Transaction_SIStrings mPass_Gate_Transaction_SIStrings)
	{
		return base.Channel.PackMPassTransactionData(packMPassTransactionDataArgs, mPass_Gate_Transaction_SIStrings);
	}

	public void ExtendRequestInterface(RequestArgs requestArgs)
	{
		base.Channel.ExtendRequestInterface(requestArgs);
	}

	public ResponseArgs ExtendRequestResponseInterface(RequestArgs requestArgs)
	{
		return base.Channel.ExtendRequestResponseInterface(requestArgs);
	}
}
