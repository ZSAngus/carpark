using System;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Extend;
using Master.SystemCommunication.LPRSInterface;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

public class ChargeContextCallBack : ICashierServiceCallBack, ILPRSContrastCallback, IFeeCallBack, ICallback, ILongConnectionCallBack, IDisabilityCallBack, IParkingSpacesEventCallback, IGateStatusEventCallback, ISystemEventCallback, ICallBackExtend, ILicensePlatePaymentCallBack
{
	public void ExitContrastCallBack(ExitContrastArgs exitContrast)
	{
		throw new NotImplementedException();
	}

	public void RecordContrastCallBack(RecordContrastArgs recordContrast)
	{
		throw new NotImplementedException();
	}

	public void Callback()
	{
		throw new NotImplementedException();
	}

	public int Listen(string programId)
	{
		throw new NotImplementedException();
	}

	public void DisabilityPressCallBack(DisabilityPressArgs disabilityPressArgs)
	{
		throw new NotImplementedException();
	}

	public void ParkingSpacesChangeNotice(ParkAreaExtend parkAreaExtend)
	{
		throw new NotImplementedException();
	}

	public void SingleGateStatusChangeNotice(DeviceStatus deviceStatus)
	{
		throw new NotImplementedException();
	}

	public void SystemNotice(NoticeArgs noticeArgs)
	{
		throw new NotImplementedException();
	}

	public void PassTraceChange(PassTrace passTrace)
	{
		throw new NotImplementedException();
	}

	public void ServerStatusChange(ServerStatusChangeArgs serverStatusChangeArgs)
	{
		throw new NotImplementedException();
	}

	public void CallBackExtend(CallBallArgs args)
	{
		throw new NotImplementedException();
	}

	public void CallBadkExtendResponse(CallBallArgs args)
	{
		throw new NotImplementedException();
	}

	public void CallBadkExtendByte(CallBallArgs args)
	{
		throw new NotImplementedException();
	}
}
