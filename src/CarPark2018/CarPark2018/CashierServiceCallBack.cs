using System;
using System.Reflection;
using CarPark.DB;
using CarPark.DB.Context;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Extend;
using Master.SystemCommunication.LPRSInterface;
using Master.SystemCommunication.Lib;
using log4net;

namespace CarPark2018;

public class CashierServiceCallBack : ICashierServiceCallBack, ILPRSContrastCallback, IFeeCallBack, ICallback, ILongConnectionCallBack, IDisabilityCallBack, IParkingSpacesEventCallback, IGateStatusEventCallback, ISystemEventCallback, ICallBackExtend, ILicensePlatePaymentCallBack
{
	private ILog Logger = null;

	public event Action<DisabilityPressArgs> DisabilityPressArgs_Event;

	public event Action<ParkAreaExtend> ParkingSpacesChangeNotice_Event;

	public event Action<NoticeArgs> SystemNotice_Event;

	public event Action<DeviceStatus> SingleGateStatusChangeNotice_Event;

	public event Action<ExitContrastArgs> ExitContrastArgs_Event;

	public event Action<RecordContrastArgs> RecordContrastArgs_Event;

	public event Action<PassTrace> PassTraceChange_Event;

	public event Action<CallBallArgs> CallBackExtend_Event;

	public void ExitContrastCallBack(ExitContrastArgs exitContrast)
	{
		try
		{
			LogDebug("ExitContrastCallBack");
			this.ExitContrastArgs_Event(exitContrast);
		}
		catch (Exception ex)
		{
			LogError(ex.Message);
		}
	}

	public void RecordContrastCallBack(RecordContrastArgs recordContrast)
	{
		try
		{
			LogDebug("RecordContrastCallBack");
			this.RecordContrastArgs_Event(recordContrast);
		}
		catch (Exception ex)
		{
			LogError(ex.Message);
		}
	}

	public void Callback()
	{
		LogDebug("Callback");
	}

	public int Listen(string programId)
	{
		return 1;
	}

	public void SingleGateStatusChangeNotice(DeviceStatus deviceStatus)
	{
		try
		{
			LogDebug("SingleGateStatusChangeNotice" + deviceStatus.DeviceCode);
			this.SingleGateStatusChangeNotice_Event(deviceStatus);
		}
		catch (Exception ex)
		{
			LogError(ex.Message);
		}
	}

	public void SystemNotice(NoticeArgs noticeArgs)
	{
		try
		{
			LogDebug("SystemNotice" + noticeArgs.Content);
			this.SystemNotice_Event(noticeArgs);
		}
		catch (Exception ex)
		{
			LogError(ex.Message);
		}
	}

	public void DisabilityPressCallBack(DisabilityPressArgs disabilityPressArgs)
	{
		try
		{
			LogDebug("DisabilityPressCallBack" + disabilityPressArgs.OnlyID);
			this.DisabilityPressArgs_Event(disabilityPressArgs);
		}
		catch (Exception ex)
		{
			LogError(ex.Message);
		}
	}

	public void ParkingSpacesChangeNotice(ParkAreaExtend parkAreaExtend)
	{
		try
		{
			LogDebug("ParkingSpacesChangeNotice: " + parkAreaExtend.TimeChargRemain);
			this.ParkingSpacesChangeNotice_Event(parkAreaExtend);
		}
		catch (Exception ex)
		{
			LogError(ex.Message);
		}
	}

	public void PassTraceChange(PassTrace passTrace)
	{
		try
		{
			LogDebug("PassTraceChange");
			this.PassTraceChange_Event(passTrace);
		}
		catch (Exception ex)
		{
			LogError(ex.Message);
		}
	}

	public void ServerStatusChange(ServerStatusChangeArgs serverStatusChangeArgs)
	{
		LogDebug("ServerStatusChange");
		try
		{
			LogDebug($"ServerA:{serverStatusChangeArgs.ServerA},ServerB:{serverStatusChangeArgs.ServerB}");
			DBContext.ServerA = serverStatusChangeArgs.ServerA;
			DBContext.ServerB = serverStatusChangeArgs.ServerB;
		}
		catch (Exception msg)
		{
			LogError(msg);
		}
	}

	public void CallBackExtend(CallBallArgs args)
	{
		try
		{
			LogDebug("CallBackExtend");
			this.CallBackExtend_Event(args);
		}
		catch (Exception msg)
		{
			LogError(msg);
		}
	}

	public void CallBadkExtendByte(CallBallArgs args)
	{
		LogDebug("CallBadkExtendByte");
	}

	public void CallBadkExtendResponse(CallBallArgs args)
	{
		LogDebug("CallBadkExtendResponse");
	}

	private void LogError(object msg)
	{
		if (Logger == null)
		{
			Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}
		Logger.Error(msg);
		Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff"), msg));
	}

	private void LogDebug(object msg)
	{
		if (Logger == null)
		{
			Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}
		Logger.Debug(msg);
		Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff"), msg));
	}

	private void LogInfo(object msg)
	{
		if (Logger == null)
		{
			Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}
		Logger.Info(msg);
		Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff"), msg));
	}

	private void LogWARN(object msg)
	{
		if (Logger == null)
		{
			Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}
		Logger.Warn(msg);
		Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff"), msg));
	}

	private void LogFATAL(object msg)
	{
		if (Logger == null)
		{
			Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}
		Logger.Fatal(msg);
		Console.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff"), msg));
	}
}
