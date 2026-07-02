using System;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Extend;
using Master.SystemCommunication.LPRSInterface;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

public class GateSingleChannelCallBack : IGateService2018CallBack, ILongConnectionCallBack, ICallback, ILPRSContrastCallback, IDisabilityCallBack, IParkingSpacesEventCallback, ICallBackExtend
{
	public bool OpenGate(int gateID)
	{
		throw new NotImplementedException();
	}

	public void AgainCamera(AgainCameraArgs againCameraArgs)
	{
		throw new NotImplementedException();
	}

	public bool ManualChange(ManualChangeArgs manualChangeArgs)
	{
		throw new NotImplementedException();
	}

	public bool ManualUpBar(ManualUpBarArgs manualUpBarArgs)
	{
		throw new NotImplementedException();
	}

	public int Listen(string programId)
	{
		throw new NotImplementedException();
	}

	public void Callback()
	{
		throw new NotImplementedException();
	}

	public void ExitContrastCallBack(ExitContrastArgs exitContrast)
	{
		throw new NotImplementedException();
	}

	public void RecordContrastCallBack(RecordContrastArgs recordContrast)
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
