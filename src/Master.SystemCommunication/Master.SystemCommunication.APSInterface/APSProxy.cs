using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.APSInterface;

public class APSProxy : DuplexClientBase<IAPSService>, IAPSService, IService
{
	public APSProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public APSProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public APSProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public APSProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public APSProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public ChargeRecord CalcCharge(CalcChargeArgs calcChargeArgs)
	{
		return base.Channel.CalcCharge(calcChargeArgs);
	}

	public ChargeRecord CalcChargeByDiscount(CalcChargeArgs calcChargeArgs, barsys_Discount discount)
	{
		return base.Channel.CalcChargeByDiscount(calcChargeArgs, discount);
	}

	public bool SaveCharge(ChargeRecord chargeRecord, APS_TimeChargeRecord aps_TimeChargeRecord, barsys_Discount discount)
	{
		return base.Channel.SaveCharge(chargeRecord, aps_TimeChargeRecord, discount);
	}

	public bool SaveChargeRecord(ChargeRecord chargeRecord, APS_TimeChargeRecord aps_TimeChargeRecord, CustomFreeRecord customFreeRecord)
	{
		return base.Channel.SaveChargeRecord(chargeRecord, aps_TimeChargeRecord, customFreeRecord);
	}

	public barsys_Discount CheckBarsys_Discount(CheckBarsys_DiscountArgs checkBarsys_DiscountArgs)
	{
		return base.Channel.CheckBarsys_Discount(checkBarsys_DiscountArgs);
	}

	public TransactionData DamageTicket(DamageTicketArgs DamageTicketArgs)
	{
		return base.Channel.DamageTicket(DamageTicketArgs);
	}

	public APS_TimeChargeRecord GetListAPS_TimeChargeRecord(int StationID)
	{
		return base.Channel.GetListAPS_TimeChargeRecord(StationID);
	}

	public GetSerialArgs GetSerial(GetSerialArgs getSerialArgs)
	{
		return base.Channel.GetSerial(getSerialArgs);
	}

	public TransactionData LostTicket(LostTicketArgs lostTicketArgs)
	{
		return base.Channel.LostTicket(lostTicketArgs);
	}

	public ShiftRecord EndShift(EndShiftArgs endShiftArgs)
	{
		return base.Channel.EndShift(endShiftArgs);
	}

	public DateTime GetSystemTime()
	{
		return base.Channel.GetSystemTime();
	}

	public List<APS_ShiftRecordDetailed> EndShiftAPS_ShiftRecordDetailed(string shiftRecordID)
	{
		return base.Channel.EndShiftAPS_ShiftRecordDetailed(shiftRecordID);
	}

	public bool APS_CertificateUse(APS_CertificateUseRecord aps_CertificateUseRecord, string staffCode, bool isInsert)
	{
		return base.Channel.APS_CertificateUse(aps_CertificateUseRecord, staffCode, isInsert);
	}

	public APS_SystemSetting GetApsSystemSetting()
	{
		return base.Channel.GetApsSystemSetting();
	}

	public StaffInfo APSLogin(APSLoginArgs apsLoginArgs)
	{
		return base.Channel.APSLogin(apsLoginArgs);
	}

	public List<SysRole> GetStaffTypeRole(APSLoginArgs apsLoginArgs)
	{
		return base.Channel.GetStaffTypeRole(apsLoginArgs);
	}

	public APS_Certificate FindCertificateInfo(FindCertificateArgs findCertificateArgs)
	{
		return base.Channel.FindCertificateInfo(findCertificateArgs);
	}

	public APS_Syslog SaveSystemLog(APS_Syslog aps_Syslog)
	{
		return base.Channel.SaveSystemLog(aps_Syslog);
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
}
