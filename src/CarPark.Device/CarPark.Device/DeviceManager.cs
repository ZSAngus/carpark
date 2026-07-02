using System;
using System.IO;
using System.Reflection;
using log4net;

namespace CarPark.Device;

public class DeviceManager
{
	private static ILog Logger;

	public static IEnterGate EnterGateModule { get; set; }

	public static IExitGate ExitGateModule { get; set; }

	public static IFeeCenter FeeCenterModule { get; set; }

	public static IRFIDGate RFIDGateModule { get; set; }

	public static ICamera CameraModule { get; set; }

	static DeviceManager()
	{
		Class2.hEE203xzkPmdM();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public DeviceManager()
	{
		Class2.hEE203xzkPmdM();
	}

	public static void LoadAssembly(bool bEnterGate, bool bExitGate, bool bFeeCenter, bool bRFIDGate, bool bCamera)
	{
		string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "carpark.device.*.dll", SearchOption.TopDirectoryOnly);
		foreach (string path in files)
		{
			LoadAssembly(path, bEnterGate, bExitGate, bFeeCenter, bRFIDGate, bCamera);
		}
	}

	private static void LoadAssembly(string path, bool bEnterGate, bool bExitGate, bool bFeeCenter, bool bRFIDGate, bool bCamera)
	{
		try
		{
			Assembly assembly = Assembly.LoadFrom(path);
			Type typeFromHandle = typeof(IEnterGate);
			Type typeFromHandle2 = typeof(IExitGate);
			Type typeFromHandle3 = typeof(IFeeCenter);
			Type typeFromHandle4 = typeof(IRFIDGate);
			Type typeFromHandle5 = typeof(ICamera);
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				if (EnterGateModule == null && typeFromHandle.IsAssignableFrom(type) && typeFromHandle != type && bEnterGate)
				{
					EnterGateModule = Activator.CreateInstance(type) as IEnterGate;
					EnterGateModule.InitDevices();
				}
				if (ExitGateModule == null && typeFromHandle2.IsAssignableFrom(type) && typeFromHandle2 != type && bExitGate)
				{
					ExitGateModule = Activator.CreateInstance(type) as IExitGate;
					ExitGateModule.InitDevices();
				}
				if (FeeCenterModule == null && typeFromHandle3.IsAssignableFrom(type) && typeFromHandle3 != type && bFeeCenter)
				{
					FeeCenterModule = Activator.CreateInstance(type) as IFeeCenter;
					FeeCenterModule.InitDevices();
				}
				if (RFIDGateModule == null && typeFromHandle4.IsAssignableFrom(type) && typeFromHandle4 != type && bRFIDGate)
				{
					RFIDGateModule = Activator.CreateInstance(type) as IRFIDGate;
					RFIDGateModule.InitDevices();
				}
				if (CameraModule == null && typeFromHandle5.IsAssignableFrom(type) && typeFromHandle5 != type && bCamera)
				{
					CameraModule = Activator.CreateInstance(type) as ICamera;
					CameraModule.InitDevices();
				}
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void LoadAssembly()
	{
		string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "carpark.device.*.dll", SearchOption.TopDirectoryOnly);
		foreach (string path in files)
		{
			LoadAssembly(path);
		}
	}

	private static void LoadAssembly(string path)
	{
		try
		{
			Assembly assembly = Assembly.LoadFrom(path);
			Type typeFromHandle = typeof(IEnterGate);
			Type typeFromHandle2 = typeof(IExitGate);
			Type typeFromHandle3 = typeof(IFeeCenter);
			Type typeFromHandle4 = typeof(IRFIDGate);
			Type typeFromHandle5 = typeof(ICamera);
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				if (EnterGateModule == null && typeFromHandle.IsAssignableFrom(type) && typeFromHandle != type)
				{
					EnterGateModule = Activator.CreateInstance(type) as IEnterGate;
					EnterGateModule.InitDevices();
				}
				if (ExitGateModule == null && typeFromHandle2.IsAssignableFrom(type) && typeFromHandle2 != type)
				{
					ExitGateModule = Activator.CreateInstance(type) as IExitGate;
					ExitGateModule.InitDevices();
				}
				if (FeeCenterModule == null && typeFromHandle3.IsAssignableFrom(type) && typeFromHandle3 != type)
				{
					FeeCenterModule = Activator.CreateInstance(type) as IFeeCenter;
					FeeCenterModule.InitDevices();
				}
				if (RFIDGateModule == null && typeFromHandle4.IsAssignableFrom(type) && typeFromHandle4 != type)
				{
					RFIDGateModule = Activator.CreateInstance(type) as IRFIDGate;
					RFIDGateModule.InitDevices();
				}
				if (CameraModule == null && typeFromHandle5.IsAssignableFrom(type) && typeFromHandle5 != type)
				{
					CameraModule = Activator.CreateInstance(type) as ICamera;
					CameraModule.InitDevices();
				}
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}
}
