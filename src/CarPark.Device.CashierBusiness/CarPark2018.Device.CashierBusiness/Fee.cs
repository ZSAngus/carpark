using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.Device;
using MacauPass.POSCom;
using MacauPass.POSCom.Package;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using N910POSDll;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkyInno.Lib.Common;
using log4net;

namespace CarPark2018.Device.CashierBusiness;

public class Fee : IFeeCenterV5, IFeeCenterV4, IFeeCenterV3, IFeeCenterV2, IFeeCenter, IMPPOSTranscation, IFeeCenterCommunication, IHandContrast, IBOC_N910, IMPPOSMPay
{
	private Thread displayThread;

	private ILog Logger;

	private CounterLED m_CounterLED = null;

	private LogOnResult m_LogonResult;

	private ECR_PAXS80 m_MPFeePos = null;

	private StarTSP600Printer m_PaperPrinter = null;

	private CounterRFIDReader m_RfidReader;

	private TickerQRCodeControler m_TickerQRCodeReader = null;

	private TickerControler m_TicketReader = null;

	private SmartCardReadEvent m_SmartCardReadEvent;

	private TicketScanEventHandler m_TicketScanEvent;

	private MasterPrinter m_MasterPrinter = null;

	private QRScaner m_QRScaner = null;

	private MPScaner m_MPScaner = null;

	private N910POS m_BOCPos;

	private QRScanerPay m_QRScanerPay;

	private UserControl innerControl = null;

	private static JObject Setting;

	public UserControl GetInnerControl
	{
		get
		{
			if (innerControl == null)
			{
				innerControl = new UserControl();
			}
			return innerControl;
		}
	}

	public event SmartCardReadEvent SmartCardReadEvent
	{
		add
		{
			SmartCardReadEvent smartCardReadEvent = m_SmartCardReadEvent;
			SmartCardReadEvent smartCardReadEvent2;
			do
			{
				smartCardReadEvent2 = smartCardReadEvent;
				SmartCardReadEvent value2 = (SmartCardReadEvent)Delegate.Combine(smartCardReadEvent2, value);
				smartCardReadEvent = Interlocked.CompareExchange(ref m_SmartCardReadEvent, value2, smartCardReadEvent2);
			}
			while (smartCardReadEvent != smartCardReadEvent2);
		}
		remove
		{
			SmartCardReadEvent smartCardReadEvent = m_SmartCardReadEvent;
			SmartCardReadEvent smartCardReadEvent2;
			do
			{
				smartCardReadEvent2 = smartCardReadEvent;
				SmartCardReadEvent value2 = (SmartCardReadEvent)Delegate.Remove(smartCardReadEvent2, value);
				smartCardReadEvent = Interlocked.CompareExchange(ref m_SmartCardReadEvent, value2, smartCardReadEvent2);
			}
			while (smartCardReadEvent != smartCardReadEvent2);
		}
	}

	public event TicketScanEventHandler TicketScanEvent
	{
		add
		{
			TicketScanEventHandler ticketScanEventHandler = m_TicketScanEvent;
			TicketScanEventHandler ticketScanEventHandler2;
			do
			{
				ticketScanEventHandler2 = ticketScanEventHandler;
				TicketScanEventHandler value2 = (TicketScanEventHandler)Delegate.Combine(ticketScanEventHandler2, value);
				ticketScanEventHandler = Interlocked.CompareExchange(ref m_TicketScanEvent, value2, ticketScanEventHandler2);
			}
			while (ticketScanEventHandler != ticketScanEventHandler2);
		}
		remove
		{
			TicketScanEventHandler ticketScanEventHandler = m_TicketScanEvent;
			TicketScanEventHandler ticketScanEventHandler2;
			do
			{
				ticketScanEventHandler2 = ticketScanEventHandler;
				TicketScanEventHandler value2 = (TicketScanEventHandler)Delegate.Remove(ticketScanEventHandler2, value);
				ticketScanEventHandler = Interlocked.CompareExchange(ref m_TicketScanEvent, value2, ticketScanEventHandler2);
			}
			while (ticketScanEventHandler != ticketScanEventHandler2);
		}
	}

	public event Action<DeviceStatusInfo> GateStatusChangeEvent;

	public event Action<NoticeInfo> NoticeEvent;

	public event Action<ParkingSpacesInfo> ParkingSpacesChangeEvent;

	public event ExitContrastEventHandler ExitContrastEvent;

	public event RecordContrastEventHandler RecordContrastEvent;

	public event Action<DisabilityPressInfo> DisabilityPressEvent;

	public event Action<string> QRCodeScanEvent;

	public event TicketStateChangeEvent TicketStateChangeEvent;

	public event Action<string> QRCodeScanPayEvent;

	static Fee()
	{
		Setting = null;
	}

	public Fee()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		m_TickerQRCodeReader = null;
		m_CounterLED = null;
		m_RfidReader = null;
		displayThread = null;
		m_PaperPrinter = null;
		m_LogonResult = null;
		m_MPFeePos = null;
	}

	public ActiveResult Active()
	{
		return m_MPFeePos.Active();
	}

	public string CalcPayment(decimal source, EnumPaymentRate sourceRate, EnumPaymentRate targetRate)
	{
		decimal num = source;
		if (m_LogonResult != null)
		{
			switch (sourceRate)
			{
			case EnumPaymentRate.MOP:
				switch (targetRate)
				{
				case EnumPaymentRate.RMB:
					num = source * m_LogonResult.EPA_M2R;
					break;
				case EnumPaymentRate.HKD:
					num = source * m_LogonResult.EPA_M2H;
					break;
				}
				break;
			case EnumPaymentRate.RMB:
				switch (targetRate)
				{
				case EnumPaymentRate.MOP:
					num = source * m_LogonResult.EPA_R2M;
					break;
				case EnumPaymentRate.HKD:
					throw new NotSupportedException();
				}
				break;
			case EnumPaymentRate.HKD:
				switch (targetRate)
				{
				case EnumPaymentRate.MOP:
					num = source * m_LogonResult.EPA_H2M;
					break;
				case EnumPaymentRate.RMB:
				case EnumPaymentRate.HKD:
					throw new NotSupportedException();
				}
				break;
			}
		}
		return num.ToString("F2");
	}

	public void DisplayFee(string Fee)
	{
		try
		{
			if (Fee != "READY.")
			{
				m_CounterLED.DisplayCash(Fee);
				FreeThreaad();
				displayThread = new Thread((ThreadStart)delegate
				{
					Thread.Sleep(60000);
					try
					{
						m_CounterLED.DisplayCash("READY.");
					}
					catch (Exception message2)
					{
						Logger.Error(message2);
					}
				});
				displayThread.IsBackground = true;
				displayThread.Start();
				return;
			}
			FreeThreaad();
			ThreadStart start = delegate
			{
				Thread.Sleep(10000);
				try
				{
					m_CounterLED.DisplayCash("READY.");
				}
				catch (Exception message2)
				{
					Logger.Error(message2);
				}
			};
			displayThread = new Thread(start);
			displayThread.IsBackground = true;
			displayThread.Start();
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void FreeThreaad()
	{
		if (displayThread != null && displayThread.IsAlive)
		{
			displayThread.Abort();
		}
		displayThread = null;
	}

	public void EjectTicket()
	{
		try
		{
			if (Config.SystemConfig.TicketType == 0)
			{
				m_TickerQRCodeReader.EjectTicket();
			}
			else if (Config.SystemConfig.TicketType == 1)
			{
				m_TicketReader.EjectTicket();
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public void InitDevices()
	{
		ThreadStart threadStart = null;
		try
		{
			RequestArgs requestArgs = new RequestArgs(DataBuffer.APPOnlyID);
			requestArgs.Extend1 = "getSetting";
			ChargeContext chargeContext = new ChargeContext();
			ResponseArgs responseArgs = chargeContext.CommunicationChannel.ExtendRequestResponseInterface(requestArgs);
			chargeContext.CommunicationChannel.Disconnect();
			initSetting(responseArgs.Extend2);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			MessageBox.Show(ex.Message);
			return;
		}
		try
		{
			m_LogonResult = SkyInno.Lib.Common.BaseSerializable.FromBinaryConfig<LogOnResult>();
		}
		catch (Exception ex2)
		{
			Exception message = ex2;
			Logger.Error(message);
		}
		try
		{
			m_CounterLED = new CounterLED(Config.SystemConfig.FeeLED.ComPort, Config.SystemConfig.FeeLED.BauadRate);
			m_CounterLED.Open();
		}
		catch (Exception ex3)
		{
			Exception message = ex3;
			Logger.Error(message);
		}
		try
		{
			if (Config.SystemConfig.TicketType == 1)
			{
				m_TicketReader = new TickerControler(Config.SystemConfig.FeeTicketOperator.ComPort, Config.SystemConfig.FeeTicketOperator.BauadRate);
				m_TicketReader.Open();
				m_TicketReader.TicketMoveEvent += m_TicketReader_TicketMoveEvent;
			}
			else if (Config.SystemConfig.TicketType == 0)
			{
				m_TickerQRCodeReader = new TickerQRCodeControler(Config.SystemConfig.FeeTicketOperator.ComPort, Config.SystemConfig.FeeTicketOperator.BauadRate);
				m_TickerQRCodeReader.Open();
				m_TickerQRCodeReader.TicketMoveEvent += m_TickerQRCodeReader_TicketMoveEvent;
				m_TickerQRCodeReader.EjectTicket();
			}
		}
		catch (Exception ex4)
		{
			Exception message = ex4;
			Logger.Error(message);
		}
		try
		{
			m_RfidReader = new CounterRFIDReader(Config.SystemConfig.FeeSmartCard.ComPort, Config.SystemConfig.FeeSmartCard.BauadRate);
			m_RfidReader.SmartCardReadEvent += m_RfidReader_SmartCardReadEvent;
			m_RfidReader.Open();
		}
		catch (Exception ex5)
		{
			Exception message = ex5;
			Logger.Error(message);
		}
		try
		{
			m_MasterPrinter = new MasterPrinter();
			m_PaperPrinter = new StarTSP600Printer(Config.SystemConfig.FeePrinter.ComPort, Config.SystemConfig.FeePrinter.BauadRate);
			m_PaperPrinter.Open();
		}
		catch (Exception ex6)
		{
			Exception message = ex6;
			Logger.Error(message);
		}
		try
		{
			m_MPFeePos = new ECR_PAXS80(Config.SystemConfig.FeeMPassPOS.ComPort, Config.SystemConfig.FeeMPassPOS.BauadRate);
			m_MPFeePos.PortID = int.Parse(Config.SystemConfig.FeeMPassPOS.GateID);
			m_MPFeePos.Open();
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
		}
		try
		{
			m_MPScaner = new MPScaner(Config.SystemConfig.QRScaner.ComPort, Config.SystemConfig.QRScaner.BauadRate);
			m_MPScaner.Open();
			m_MPScaner.QRCodeEvent += m_QRScaner_QRCodeEvent;
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
		}
		try
		{
			m_BOCPos = new N910POS(Config.SystemConfig.FeeQPassPOS.ComPort, Config.SystemConfig.FeeQPassPOS.BauadRate);
			m_BOCPos.Open();
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
		}
		try
		{
			m_QRScanerPay = new QRScanerPay(Config.SystemConfig.QRScanerPay.ComPort, Config.SystemConfig.QRScanerPay.BauadRate);
			m_QRScanerPay.Open();
			m_QRScanerPay.QRCodeEvent += m_QRScaner_SmartPayEvent;
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
		}
	}

	private void NewDelegate()
	{
		DateTime dateTime = DateTime.Now.AddDays(-1.0);
		while (true)
		{
			bool flag = true;
			Thread.Sleep(60000);
			if (!(dateTime.Date != DateTime.Now.Date))
			{
				continue;
			}
			try
			{
				LogOnResult logOnResult = ((IMPPOSTranscation)DeviceManager.FeeCenterModule).Logon();
				if (logOnResult.CommandResult == MacauPass.POSCom.Package.CommandResult.Fail)
				{
					Logger.Error("MPPOS LogonError");
				}
				SignTransactionsResult signTransactionsResult = SignInTransactions();
				if (signTransactionsResult.CommandResult == MacauPass.POSCom.Package.CommandResult.Fail)
				{
					MessageBox.Show(signTransactionsResult.ErrDescription);
				}
				dateTime = DateTime.Now;
			}
			catch (Exception message)
			{
				Logger.Error(message);
			}
		}
	}

	public LogOffResult Logoff()
	{
		return m_MPFeePos.LogOff();
	}

	public LogOnResult Logon()
	{
		LogOnResult logOnResult = m_MPFeePos.LogOn();
		if (logOnResult.CommandResult == MacauPass.POSCom.Package.CommandResult.Success)
		{
			logOnResult.SaveBinaryConfig();
			m_LogonResult = logOnResult;
		}
		return logOnResult;
	}

	private void m_RfidReader_SmartCardReadEvent(string cardNum)
	{
		if (m_SmartCardReadEvent != null)
		{
			m_SmartCardReadEvent(cardNum);
		}
	}

	private void m_QRScaner_QRCodeEvent(string cardcode)
	{
		if (m_MPScaner != null)
		{
			this.QRCodeScanEvent(cardcode);
		}
	}

	private void m_QRScaner_SmartPayEvent(string cardcode)
	{
		if (m_QRScanerPay != null)
		{
			this.QRCodeScanPayEvent(cardcode);
		}
	}

	private void m_TickerQRCodeReader_TicketMoveEvent(TicketState curState)
	{
		Console.WriteLine("m_TicketReader_TicketMoveEvent");
		if (curState != TicketState.Ticket)
		{
			return;
		}
		string text = null;
		try
		{
			m_TickerQRCodeReader.Waiting();
			text = m_TickerQRCodeReader.ReadFullTruck();
			Logger.Info("ReadTicket:" + text);
			Console.WriteLine("ReadTicket=" + text);
			TicketInfo ticketInfo = TicketInfo.FromString(text);
			FeeInfo feeInfo = null;
			if (m_TicketScanEvent != null)
			{
				feeInfo = m_TicketScanEvent(ticketInfo);
			}
			if (feeInfo.TicketAction != EnumTicketAction.Keep)
			{
				if (feeInfo.TicketAction == EnumTicketAction.Reject)
				{
					Console.WriteLine("EjectTicket()");
					EjectTicket();
				}
				else
				{
					Logger.Debug("Start to print ticket ,neet print:" + feeInfo.NeedPrint);
					Console.WriteLine("Start to print ticket ,neet print=" + feeInfo.GetWriteData() + feeInfo.GetPrintData());
					m_TickerQRCodeReader.PrintData(feeInfo.GetWriteData(), feeInfo.Fee.ToString());
				}
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
			m_TickerQRCodeReader.EjectTicket();
		}
	}

	private void m_TicketReader_TicketMoveEvent(TicketState curState)
	{
		if (curState != TicketState.Ticket)
		{
			return;
		}
		string text = null;
		try
		{
			text = m_TicketReader.ReadFullTruck();
			Logger.Info("ReadTicket:" + text);
			TicketInfo ticketInfo = TicketInfo.FromString(text);
			FeeInfo feeInfo = null;
			if (m_TicketScanEvent != null)
			{
				feeInfo = m_TicketScanEvent(ticketInfo);
			}
			if (feeInfo.TicketAction == EnumTicketAction.Keep)
			{
				return;
			}
			if (feeInfo.TicketAction == EnumTicketAction.Reject)
			{
				EjectTicket();
				return;
			}
			Logger.Debug("Start to print ticket ,neet print:" + feeInfo.NeedPrint);
			if (feeInfo.NeedPrint)
			{
				m_TicketReader.PutPrintData(feeInfo.GetPrintData());
			}
			else
			{
				m_TicketReader.PutPrintData("");
			}
			m_TicketReader.PutWriteData(feeInfo.GetWriteData());
			m_TicketReader.Write_Print();
			Thread.Sleep(500);
			m_TicketReader.EjectTicket();
		}
		catch (Exception message)
		{
			Logger.Error(message);
			m_TicketReader.EjectTicket();
		}
		EjectTicket();
	}

	public void MakeTicket(FeeInfo info)
	{
		try
		{
			if (Config.SystemConfig.TicketType == 1)
			{
				if (info.TicketType == EnumTicketType.Ticket_TimeOut)
				{
					m_TicketReader.PutPrintData("*");
				}
				else
				{
					m_TicketReader.PutPrintData(info.GetPrintData());
				}
				m_TicketReader.PutWriteData(info.GetWriteData());
				m_TicketReader.Write_Print();
				Thread.Sleep(500);
				m_TicketReader.EjectTicket();
			}
			else if (Config.SystemConfig.TicketType == 0)
			{
				if (info.TicketType == EnumTicketType.Lost)
				{
					m_TickerQRCodeReader.PrintLost(info.GetWriteData(), info.Fee.ToString(), info.LPRSNumber, info.FieldStr);
				}
				else
				{
					m_TickerQRCodeReader.PrintData(info.GetWriteData(), info.Fee.ToString());
				}
			}
		}
		catch (OperationCanceledException ex)
		{
			Logger.Debug("OperationCanceledException thrown");
			throw ex;
		}
		catch (Exception message)
		{
			Logger.Error(message);
			EjectTicket();
		}
	}

	public void OpenCash()
	{
		if (m_PaperPrinter != null)
		{
			try
			{
				m_PaperPrinter.OpenDrawer();
			}
			catch (Exception message)
			{
				Logger.Error(message);
			}
		}
	}

	public void Print(object printStac)
	{
		try
		{
			m_MasterPrinter.Print(printStac.ToString());
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public void PrintTicket(string TicketNumber, string CardType)
	{
		try
		{
			string text = CardType;
			CardType = text.Split('|')[0];
			int num = int.Parse(CardType) + 5;
			string data = string.Format("{5}{0}{1}{2}{3}{4}0078", num, TicketNumber, "0", DateTime.Now.ToString("yyMMddHHmmss"), DateTime.Now.ToString("yyMMddHHmmss"), "2243");
			m_TicketReader.PutWriteData(data);
			data = CardType + "      " + TicketNumber + "  " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " $" + text.Split('|')[1];
			m_TicketReader.PutPrintData(data);
			Thread.Sleep(200);
			m_TicketReader.Print();
			Thread.Sleep(500);
			EjectTicket();
		}
		catch (Exception message)
		{
			Logger.Error(message);
			EjectTicket();
		}
	}

	public QueryResult QueryCard(int TransactionCount)
	{
		return m_MPFeePos.QueryCard(TransactionCount);
	}

	public ReloadResult Reload(decimal amt, string cashType, string valType)
	{
		ReloadResult reloadResult = m_MPFeePos.Reload(amt, cashType, valType);
		if (reloadResult.RETCODE == MacauPass.POSCom.CommandConsts.POS_IN_UPDATE)
		{
			MessageBox.Show("POS正在更新，請稍候");
		}
		return reloadResult;
	}

	public MacauPass.POSCom.Package.SaleResult Sale(decimal amt)
	{
		MacauPass.POSCom.Package.SaleResult saleResult = m_MPFeePos.Sale(amt);
		if (saleResult.RETCODE == MacauPass.POSCom.CommandConsts.POS_IN_UPDATE)
		{
			MessageBox.Show("POS正在更新，請稍候");
		}
		return saleResult;
	}

	public SignTransactionsResult SignInTransactions()
	{
		return m_MPFeePos.SignTransactions();
	}

	public void CalcCurrentQtyInfo()
	{
	}

	public EnumParkType CheckParkType(GateLoopInfo status)
	{
		throw new NotImplementedException();
	}

	public string GetCarParkSerial(EnumParkType parkType)
	{
		return "";
	}

	public void UpdateCarCount()
	{
	}

	public void ManualChange(ManualChangeInfo args)
	{
	}

	public void ManualUpBar(ManualUpBarInfo manualUpBarInfo)
	{
	}

	private void m_callBack_ParkingSpacesChangeNotice_Event(ParkingSpacesChangeNoticeArgs obj)
	{
		ParkingSpacesInfo parkingSpacesInfo = new ParkingSpacesInfo();
		parkingSpacesInfo.AreaID = obj.parkAreaID;
		parkingSpacesInfo.CurrCount = obj.ParkingSpacesCount;
		parkingSpacesInfo.parkType = obj.parkType;
		parkingSpacesInfo.ManualFull = obj.ManualFull;
		this.ParkingSpacesChangeEvent(parkingSpacesInfo);
	}

	private void m_callBack_SystemNotice_Event(NoticeArgs obj)
	{
		NoticeInfo noticeInfo = new NoticeInfo();
		noticeInfo.Content = obj.Content;
		if (obj.noticeType == NoticeType.Error)
		{
			noticeInfo.PassStatus = EnumPassStatus.Error;
		}
		else if (obj.noticeType == NoticeType.Normal)
		{
			noticeInfo.PassStatus = EnumPassStatus.Normal;
		}
		else if (obj.noticeType == NoticeType.SystemPrompt)
		{
			noticeInfo.PassStatus = EnumPassStatus.Normal;
		}
		this.NoticeEvent(noticeInfo);
	}

	private void m_callBack_SingleGateStatusChangeNotice_Event(DeviceStatus obj)
	{
		DeviceStatusInfo deviceStatusInfo = new DeviceStatusInfo();
		deviceStatusInfo.DeviceCode = obj.DeviceCode;
		deviceStatusInfo.DeviceType = obj.DeviceType;
		deviceStatusInfo.GateID = obj.GateID;
		this.GateStatusChangeEvent(deviceStatusInfo);
	}

	private void m_callBack_DisabilityPressCallBack(DisabilityPressArgs obj)
	{
		DisabilityPressInfo disabilityPressInfo = new DisabilityPressInfo();
		disabilityPressInfo.GateID = obj.GateID;
		disabilityPressInfo.OnlyID = obj.OnlyID;
		disabilityPressInfo.OperationPC = obj.OperationPC;
		disabilityPressInfo.PressParkType = obj.PressParkType;
		disabilityPressInfo.PrintParkType = obj.PrintParkType;
		disabilityPressInfo.ReceiveID = obj.ReceiveID;
		disabilityPressInfo.ShiffCode = obj.ShiffCode;
		disabilityPressInfo.IsCancel = obj.IsCancel;
		this.DisabilityPressEvent(disabilityPressInfo);
	}

	private void m_callBack_ExitContrastArgs_Event(ExitContrastArgs obj)
	{
		ExitContrastInfo exitContrastInfo = new ExitContrastInfo();
		exitContrastInfo.CallTimestamp = obj.CallTimestamp;
		exitContrastInfo.EnterImagePath = obj.EnterImagePath;
		exitContrastInfo.EnterValue = obj.EnterValue;
		exitContrastInfo.ExitImagePath = obj.ExitImagePath;
		exitContrastInfo.ExitValue = obj.ExitValue;
		exitContrastInfo.GateID = obj.GateID;
		exitContrastInfo.GuID = obj.GuID;
		exitContrastInfo.IsPass = obj.IsPass;
		exitContrastInfo.OnlyID = obj.OnlyID;
		exitContrastInfo.ShowTime = obj.ShowTime;
		exitContrastInfo = this.ExitContrastEvent(exitContrastInfo);
	}

	private void m_callBack_RecordContrastArgs_Event(RecordContrastArgs obj)
	{
		RecordContrastInfo recordContrastInfo = new RecordContrastInfo();
		recordContrastInfo.CallTimestamp = obj.CallTimestamp;
		recordContrastInfo.CurrResult = obj.currResult;
		recordContrastInfo.GateID = obj.GateID;
		recordContrastInfo.GuID = obj.GuID;
		recordContrastInfo.ImagePath = obj.ImagePath;
		recordContrastInfo.IsPass = obj.IsPass;
		recordContrastInfo.OnlyID = obj.OnlyID;
		recordContrastInfo.Registration = obj.Registration;
		recordContrastInfo.ShowTime = obj.ShowTime;
		recordContrastInfo = this.RecordContrastEvent(recordContrastInfo);
	}

	public void AgainCamera(AgainCameraInfo againCameraInfo)
	{
	}

	public void RefreshSystem(int args)
	{
	}

	public void UpdateParkAreaExtend(UpdateParkAreaExtendInfo updateParkAreaExtendInfo)
	{
	}

	public void DisabilityPress(DisabilityPressInfo disabilityPressInfo)
	{
	}

	public void ExitContrast(ExitContrastInfo exitContrastInfo)
	{
	}

	public void RecordContrast(RecordContrastInfo recordContrastInfo)
	{
	}

	public static void initSetting(string JsonStr)
	{
		Setting = (JObject)JsonConvert.DeserializeObject(JsonStr);
		Config.SystemConfig.FeeLED = new ComSettings
		{
			GateID = getSettingVal<string>("org", "FLEDGateID"),
			ComPort = getSettingVal<string>("org", "FLEDComPort"),
			BauadRate = getSettingVal<int>("org", "FLEDBauadRate")
		};
		Config.SystemConfig.FeePrinter = new ComSettings
		{
			GateID = getSettingVal<string>("org", "FPGateID"),
			ComPort = getSettingVal<string>("org", "FPComPort"),
			BauadRate = getSettingVal<int>("org", "FPBauadRate")
		};
		Config.SystemConfig.FeeSmartCard = new ComSettings
		{
			GateID = getSettingVal<string>("org", "FSCGateID"),
			ComPort = getSettingVal<string>("org", "FSCComPort"),
			BauadRate = getSettingVal<int>("org", "FSCBauadRate")
		};
		Config.SystemConfig.FeeTicketOperator = new ComSettings
		{
			GateID = getSettingVal<string>("org", "FTOGateID"),
			ComPort = getSettingVal<string>("org", "FTOComPort"),
			BauadRate = getSettingVal<int>("org", "FTOBauadRate")
		};
		Config.SystemConfig.FeeMPassPOS = new ComSettings
		{
			GateID = getSettingVal<string>("org", "FMPOSGateID"),
			ComPort = getSettingVal<string>("org", "FMPOSComPort"),
			BauadRate = getSettingVal<int>("org", "FMPOSBauadRate")
		};
		Config.SystemConfig.QRScaner = new ComSettings
		{
			GateID = getSettingVal<string>("org", "QRSGateID"),
			ComPort = getSettingVal<string>("org", "QRSComPort"),
			BauadRate = getSettingVal<int>("org", "QRSBauadRate")
		};
		Config.SystemConfig.QRScanerPay = new ComSettings
		{
			GateID = getSettingVal<string>("org", "QRSPGateID"),
			ComPort = getSettingVal<string>("org", "QRSPComPort"),
			BauadRate = getSettingVal<int>("org", "QRSPBauadRate")
		};
		Config.SystemConfig.FeeQPassPOS = new ComSettings
		{
			GateID = getSettingVal<string>("org", "QPassPOSGateID"),
			ComPort = getSettingVal<string>("org", "QPassPOSComPort"),
			BauadRate = getSettingVal<int>("org", "QPassPOSBauadRate")
		};
		Config.SystemConfig.CounterRFIDReaderMode = getSettingVal<int>("org", "CounterRFIDReaderMode");
		Config.SystemConfig.TicketType = getSettingVal<int>("org", "TicketType");
	}

	private static T getSettingVal<T>(string index, string key)
	{
		T result = default(T);
		object obj = Setting[index][key];
		if (obj == null)
		{
			return result;
		}
		return (T)Convert.ChangeType(obj.ToString(), typeof(T));
	}

	private void ResetLocalDevices(string cmd)
	{
		try
		{
			RequestArgs requestArgs = new RequestArgs(DataBuffer.APPOnlyID);
			requestArgs.Extend1 = "getSetting";
			ChargeContext chargeContext = new ChargeContext();
			ResponseArgs responseArgs = chargeContext.CommunicationChannel.ExtendRequestResponseInterface(requestArgs);
			chargeContext.CommunicationChannel.Disconnect();
			Setting = (JObject)JsonConvert.DeserializeObject(responseArgs.Extend2);
			switch (cmd)
			{
			case "TicketMachine":
				Config.SystemConfig.FeeTicketOperator = new ComSettings
				{
					GateID = getSettingVal<string>("org", "FTOGateID"),
					ComPort = getSettingVal<string>("org", "FTOComPort"),
					BauadRate = getSettingVal<int>("org", "FTOBauadRate")
				};
				Config.SystemConfig.TicketType = getSettingVal<int>("org", "TicketType");
				try
				{
					if (Config.SystemConfig.TicketType == 1)
					{
						if (m_TicketReader != null)
						{
							m_TicketReader.TicketMoveEvent -= m_TicketReader_TicketMoveEvent;
							m_TicketReader.Close();
							m_TicketReader = null;
						}
						m_TicketReader = new TickerControler(Config.SystemConfig.FeeTicketOperator.ComPort, Config.SystemConfig.FeeTicketOperator.BauadRate);
						m_TicketReader.Open();
						m_TicketReader.TicketMoveEvent += m_TicketReader_TicketMoveEvent;
					}
					else if (Config.SystemConfig.TicketType == 0)
					{
						if (m_TickerQRCodeReader != null)
						{
							m_TickerQRCodeReader.TicketMoveEvent -= m_TickerQRCodeReader_TicketMoveEvent;
							m_TickerQRCodeReader.Close();
							m_TickerQRCodeReader = null;
						}
						m_TickerQRCodeReader = new TickerQRCodeControler(Config.SystemConfig.FeeTicketOperator.ComPort, Config.SystemConfig.FeeTicketOperator.BauadRate);
						m_TickerQRCodeReader.Open();
						m_TickerQRCodeReader.TicketMoveEvent += m_TickerQRCodeReader_TicketMoveEvent;
						m_TickerQRCodeReader.EjectTicket();
					}
					break;
				}
				catch (Exception message3)
				{
					Logger.Error(message3);
					break;
				}
			case "CashierScreen":
				Config.SystemConfig.FeeLED = new ComSettings
				{
					GateID = getSettingVal<string>("org", "FLEDGateID"),
					ComPort = getSettingVal<string>("org", "FLEDComPort"),
					BauadRate = getSettingVal<int>("org", "FLEDBauadRate")
				};
				try
				{
					if (m_CounterLED != null)
					{
						m_CounterLED.Close();
						m_CounterLED = null;
					}
					m_CounterLED = new CounterLED(Config.SystemConfig.FeeLED.ComPort, Config.SystemConfig.FeeLED.BauadRate);
					m_CounterLED.Open();
					break;
				}
				catch (Exception message2)
				{
					Logger.Error(message2);
					break;
				}
			case "ReceiptPrinter":
				Config.SystemConfig.FeePrinter = new ComSettings
				{
					GateID = getSettingVal<string>("org", "FPGateID"),
					ComPort = getSettingVal<string>("org", "FPComPort"),
					BauadRate = getSettingVal<int>("org", "FPBauadRate")
				};
				break;
			case "CardReader":
				Config.SystemConfig.FeeSmartCard = new ComSettings
				{
					GateID = getSettingVal<string>("org", "FSCGateID"),
					ComPort = getSettingVal<string>("org", "FSCComPort"),
					BauadRate = getSettingVal<int>("org", "FSCBauadRate")
				};
				Config.SystemConfig.CounterRFIDReaderMode = getSettingVal<int>("org", "CounterRFIDReaderMode");
				try
				{
					if (m_RfidReader != null)
					{
						m_RfidReader.SmartCardReadEvent -= m_RfidReader_SmartCardReadEvent;
						m_RfidReader.Close();
						m_RfidReader = null;
					}
					m_RfidReader = new CounterRFIDReader(Config.SystemConfig.FeeSmartCard.ComPort, Config.SystemConfig.FeeSmartCard.BauadRate);
					m_RfidReader.SmartCardReadEvent += m_RfidReader_SmartCardReadEvent;
					m_RfidReader.Open();
					break;
				}
				catch (Exception message)
				{
					Logger.Error(message);
					break;
				}
			case "MPassPos":
				Config.SystemConfig.FeeMPassPOS = new ComSettings
				{
					GateID = getSettingVal<string>("org", "FMPOSGateID"),
					ComPort = getSettingVal<string>("org", "FMPOSComPort"),
					BauadRate = getSettingVal<int>("org", "FMPOSBauadRate")
				};
				try
				{
					if (m_MPFeePos != null)
					{
						m_MPFeePos.Close();
						m_MPFeePos = null;
					}
					m_MPFeePos = new ECR_PAXS80(Config.SystemConfig.FeeMPassPOS.ComPort, Config.SystemConfig.FeeMPassPOS.BauadRate);
					m_MPFeePos.PortID = int.Parse(Config.SystemConfig.FeeMPassPOS.GateID);
					m_MPFeePos.Open();
					break;
				}
				catch (Exception arg)
				{
					Logger.Error(arg);
					break;
				}
			case "QRScaner":
				Config.SystemConfig.QRScaner = new ComSettings
				{
					GateID = getSettingVal<string>("org", "QRSGateID"),
					ComPort = getSettingVal<string>("org", "QRSComPort"),
					BauadRate = getSettingVal<int>("org", "QRSBauadRate")
				};
				try
				{
					if (m_MPScaner != null)
					{
						m_MPScaner.QRCodeEvent -= m_QRScaner_QRCodeEvent;
						m_MPScaner.Close();
						m_MPScaner = null;
					}
					m_MPScaner = new MPScaner(Config.SystemConfig.QRScaner.ComPort, Config.SystemConfig.QRScaner.BauadRate);
					m_MPScaner.Open();
					m_MPScaner.QRCodeEvent += m_QRScaner_QRCodeEvent;
					break;
				}
				catch (Exception arg)
				{
					Logger.Error(string.Format("QRScaner ResetErr:", arg));
					break;
				}
			}
		}
		catch (Exception arg)
		{
			Logger.Error(arg);
		}
	}

	private static void EndAsyncReset(IAsyncResult ar)
	{
		Action<string> action = null;
		try
		{
			AsyncResult asyncResult = (AsyncResult)ar;
			action = (Action<string>)asyncResult.AsyncDelegate;
			action.EndInvoke(ar);
		}
		catch (Exception)
		{
		}
	}

	public bool ResetDevices(string args)
	{
		bool flag = false;
		try
		{
			Action<string> action = ResetLocalDevices;
			action.BeginInvoke(args, EndAsyncReset, null);
			return true;
		}
		catch (Exception message)
		{
			Logger.Error(message);
			return false;
		}
	}

	public CheckLineResult CHECKLINE()
	{
		return m_BOCPos.CHECKLINE();
	}

	public LogonResult LOGON()
	{
		return m_BOCPos.LOGON();
	}

	public N910POSDll.SaleResult SALE(decimal amt)
	{
		return m_BOCPos.SALE(amt);
	}

	public ReloadResult ReloadMPay(decimal amt, string cashType, string valType, string barcode)
	{
		ReloadResult reloadResult = null;
		if (string.IsNullOrWhiteSpace(barcode))
		{
			return m_MPFeePos.ReloadByPax(amt, cashType, valType);
		}
		return m_MPFeePos.ReloadByPC(amt, cashType, valType, barcode);
	}

	public MacauPass.POSCom.Package.SaleResult SaleMPay(decimal amt, string barcode)
	{
		MacauPass.POSCom.Package.SaleResult saleResult = null;
		if (string.IsNullOrWhiteSpace(barcode))
		{
			return m_MPFeePos.SaleByPax(amt);
		}
		return m_MPFeePos.SaleByPC(amt, barcode);
	}

	public VoidResult VoidTransactionMPay(string invoiceNo, string TerminalID, decimal amt)
	{
		throw new NotImplementedException();
	}
}
