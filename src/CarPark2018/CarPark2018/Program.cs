using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.DB.Context;
using CarPark.Device;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using log4net;

namespace CarPark2018;

internal static class Program
{
	private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

	private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
	{
		Logger.Fatal(e.Exception);
	}

	private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		Logger.Fatal(e.ExceptionObject);
	}

	[STAThread]
	private static void Main(string[] args)
	{
		Application.EnableVisualStyles();
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		Application.ThreadException += Application_ThreadException;
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		ImageManager.InitPicImageList();
		try
		{
			if ((from m in Process.GetProcesses()
				where m.ProcessName == Process.GetCurrentProcess().ProcessName
				select m).Count() <= 1)
			{
				try
				{
					GetInitInfoArgs getInitInfoArgs = new GetInitInfoArgs();
					getInitInfoArgs.PayStationName = Settings.Default.OnlyID;
					DataBuffer.APPOnlyID = Settings.Default.OnlyID;
					List<CardType> CardTypes = new List<CardType>();
					List<StaffType> StaffTypes = new List<StaffType>();
					List<ParkGate> ParkGates = new List<ParkGate>();
					List<ParkArea> ParkAreas = new List<ParkArea>();
					List<ParkAreaExtend> ParkAreaExtends = new List<ParkAreaExtend>();
					ShiftRecord shiftRecord = new ShiftRecord();
					CompanyInfo companyInfo = new CompanyInfo();
					DBContext.Ip = Settings.Default.ServerIP;
					DBContext.UserName = Settings.Default.UserName;
					DBContext.Pwd = Settings.Default.Pwd;
					DBContext.DBName = Settings.Default.DBName;
					DBContext.SetDBTwo(Settings.Default.UserName, Settings.Default.Pwd, Settings.Default.ServerIP2, Settings.Default.DBName);
					GetInitInfoReturn initInfo = Common._Carpark2018ServiceContext.CommunicationChannel.GetInitInfo(getInitInfoArgs, out CardTypes, out StaffTypes, out ParkGates, out ParkAreas, out ParkAreaExtends, out shiftRecord, out companyInfo);
					try
					{
						RequestArgs requestArgs = new RequestArgs(Settings.Default.OnlyID);
						requestArgs.Extend1 = "getSetting";
						ChargeContext chargeContext = new ChargeContext();
						ResponseArgs responseArgs = chargeContext.CommunicationChannel.ExtendRequestResponseInterface(requestArgs);
						chargeContext.CommunicationChannel.Disconnect();
						Config.InitSettings(responseArgs.Extend2);
					}
					catch (Exception ex)
					{
						Logger.Error(ex);
						MessageBox.Show(ex.Message);
						return;
					}
					DeviceManager.LoadAssembly();
					if (initInfo.ISOK)
					{
						DataBuffer2018.CardTypes = CardTypes;
						DataBuffer2018.StaffTypes = StaffTypes;
						DataBuffer2018.ParkGates = ParkGates;
						DataBuffer2018.ParkAreas = ParkAreas;
						DataBuffer2018.ParkAreaExtends = ParkAreaExtends;
						try
						{
							DataBuffer.CurrentShiftRecord = shiftRecord;
							DataBuffer.CompanyInfo = companyInfo;
						}
						catch (Exception ex2)
						{
							MessageBox.Show(ex2.Message);
						}
						Application.Run(new FormMain());
					}
					else
					{
						Logger.Error(initInfo.ErrCode);
						Console.WriteLine(initInfo.ErrCode);
						MessageBox.Show(initInfo.ErrCode);
					}
					return;
				}
				catch (Exception ex3)
				{
					Logger.Error(ex3);
					Console.WriteLine(ex3.Message);
					MessageBox.Show(ex3.Message);
					return;
				}
			}
			MessageBox.Show("系統已運行！");
		}
		catch (Exception ex4)
		{
			Logger.Error(ex4);
			MessageBox.Show(ex4.Message);
		}
	}
}
