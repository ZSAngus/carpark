using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.Device;
using CarPark.Lib;
using CarPark.UserControls.SysConfig;
using CarPark2018.Forms;
using CarPark2018.LPPayForms;
using CarPark2018.Properties;
using CarPark2018.UserControls;
using MacauPass.POSCom.Package;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using Newtonsoft.Json;
using SkyInno.Lang;
using log4net;

namespace CarPark2018;

public class FormMain : Form
{
	private ILog Logger;

	private Thread TimeThread;

	private List<view_transactionandlp> mListLPRS;

	private int nPageCount;

	private int nCurentPage;

	private int nPageNum = 6;

	private DateTime nFeetime = DateTime.Now;

	private bool nIsImageAct;

	private IContainer components;

	private ToolStrip toolStrip1;

	private ToolStripButton btnLogin;

	private ToolStripDropDownButton btnLanguage;

	private ToolStripMenuItem btnCHT;

	private ToolStripMenuItem btnPT;

	private ToolStripSeparator ts1;

	private ToolStripButton btnOpenDrawer;

	private ToolStripButton btnShift;

	private ToolStripButton btnExit;

	private ToolStripSeparator toolStripSeparator1;

	private Panel panBottom;

	private Panel panFill;

	private UCGatesEX ucGatesEX2;

	private UCParkAreaEX2 ucParkAreaEX2;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripButton btnTimeChargeEx;

	private ToolStripButton btnParkAreaExtend;

	private ToolStripButton btnVoidCharge;

	private ToolStripButton btnOther;

	private ToolStripButton btnRentalType;

	private ToolStripButton btnMpassQuery;

	private ToolStripButton btnMpassCharge;

	private ToolStripButton btnDepositCharge;

	private ToolStripButton btnCheckRental;

	private ToolStripButton btnSetFree;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator4;

	private TabControl tabCarparkInfo;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private UCPasstraceEX2 ucPasstraceEX2;

	private PictureBox picLP1;

	private PictureBox picLP4;

	private PictureBox picLP3;

	private PictureBox picLP2;

	private PictureBox picLP6;

	private PictureBox picLP5;

	private Label labLP;

	private TextBox txtLP;

	private Label labEndTime;

	private DateTimePicker dpEndTime;

	private DateTimePicker dpStarttime;

	private Label labLP1;

	private Label labText1;

	private Label labText6;

	private Label labLP6;

	private Label labText4;

	private Label labLP4;

	private Label labText5;

	private Label labLP5;

	private Label labText3;

	private Label labLP3;

	private Label labText2;

	private Label labLP2;

	private CheckBox ckStartTime;

	private Button btnSearch;

	private Button btnNext;

	private Button btnPrv;

	private ToolStripButton btnLPCheck;

	private Panel panParkRemain;

	private Label labPCar;

	private Label labPMotor;

	private Label labPCRe;

	private Label labPMRe;

	private ToolStripButton btnLost;

	private ToolStripButton btnTransferTicket;

	private UCClock ucClock1;

	private ToolStripButton btnSwitchCam;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label labFinishMsg;

	private Label label6;

	private System.Windows.Forms.Timer timerPaidMsg;

	private TextBox txtDisplay;

	private string lastContent;

	private bool isFirstLoad = true;

	[DllImport("user32.dll ")]
	private static extern bool SetForegroundWindow(IntPtr hWnd);

	[DllImport("user32.dll")]
	private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	[DllImport("user32.dll")]
	private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	public FormMain()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		TimeThread = new Thread(TimeStart)
		{
			IsBackground = true
		};
		TimeThread.Start();
		LangManager.LanguageChangedEvent += LangManager_LanguageChangedEvent;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent += FeeCenterModule_TicketScanEvent;
			try
			{
				Common.m_callBack.DisabilityPressArgs_Event += FormMainNew_DisabilityPressEvent;
				Common.m_callBack.SingleGateStatusChangeNotice_Event += FormMainNew_GateStatusChangeEvent;
				Common.m_callBack.SystemNotice_Event += FormMainNew_NoticeEvent;
				Common.m_callBack.ParkingSpacesChangeNotice_Event += FormMainNew_ParkingSpacesChangeEvent;
				Common.m_callBack.RecordContrastArgs_Event += FormMainNew_RecordContrastEvent;
				Common.m_callBack.ExitContrastArgs_Event += FormMain_ExitContrastEvent;
				Common.m_callBack.PassTraceChange_Event += FormMainNew_PasstraceChangeEvent;
				Common.m_callBack.CallBackExtend_Event += FormMainNew_CallBackExtend_Event;
			}
			catch (Exception message)
			{
				Logger.Error(message);
			}
		}
		ReadTextFile();
		FormFee.Self();
		base.Activated += FormMain_Activated;
	}

	public void TimeStart()
	{
	}

	private FeeInfo FeeCenterModule_TicketScanEvent(TicketInfo ticketInfo)
	{
		FeeInfo feeInfo = new FeeInfo
		{
			CarParkSerialNo = ticketInfo.CarParkSerialNo,
			InTime = ticketInfo.InTime,
			ParkType = ticketInfo.ParkType,
			ParkTypeStr = ticketInfo.ParkTypeStr,
			TicketNumber = ticketInfo.TicketNumber,
			TicketAction = EnumTicketAction.Reject
		};
		FeeInfo rtn = feeInfo;
		try
		{
			if (ticketInfo.IsEmptyOrInValid)
			{
				Console.WriteLine("ticketInfo.IsEmptyOrInValid");
				Global.ShowMessage(LangManager.GetLangString("Alert.TicketNotValid"));
				return rtn;
			}
			if (DataBuffer2018.CurrentStaff == null)
			{
				Global.ShowMessage(LangManager.GetLangString("Alert.Login"));
				return rtn;
			}
			Invoke((MethodInvoker)delegate
			{
				TransactionData transactionData = new TransactionData();
				ChargeRecord chargeRecord = new ChargeRecord();
				try
				{
					GetTransactionDataArgs getTransactionDataArgs = new GetTransactionDataArgs
					{
						TicketNumber = rtn.TicketNumber
					};
					ChargeContext chargeContext = new ChargeContext();
					GetTransactionDataReturn transactionData2 = chargeContext.CommunicationChannel.GetTransactionData(getTransactionDataArgs, out transactionData);
					chargeContext.CommunicationChannel.Disconnect();
					if (transactionData2 != null)
					{
						if (transactionData2.ISOK)
						{
							using (FormTimeCharge formTimeCharge = new FormTimeCharge
							{
								m_TransactionData = transactionData
							})
							{
								if (formTimeCharge.ShowDialog() == DialogResult.OK)
								{
									chargeRecord = formTimeCharge.m_ChargeRecord;
									rtn.Fee = chargeRecord.TotalCharge;
									rtn.FeeTime = chargeRecord.ChargeTime;
									rtn.NeedPrint = true;
									rtn.TicketAction = EnumTicketAction.Normal;
									if (chargeRecord.TotalCharge != 0m && formTimeCharge.m_mpass == null && formTimeCharge.m_boc == null)
									{
										try
										{
											DeviceManager.FeeCenterModule.OpenCash();
											return;
										}
										catch (Exception message)
										{
											Logger.Error(message);
											return;
										}
									}
								}
								return;
							}
						}
						Global.ShowMessage(LangManager.GetLangString(transactionData2.ErrCode));
					}
				}
				catch (TimeoutException)
				{
					Global.ShowMessage("操作超時，請重新操作");
					try
					{
						DeviceManager.FeeCenterModule.EjectTicket();
					}
					catch (Exception ex4)
					{
						Logger.Error(ex4);
						Console.WriteLine(ex4.Message);
					}
				}
				catch (Exception ex5)
				{
					try
					{
						DeviceManager.FeeCenterModule.EjectTicket();
					}
					catch (Exception ex6)
					{
						Logger.Error(ex6);
						Console.WriteLine(ex6.Message);
					}
					Console.WriteLine(ex5.Message);
				}
			});
		}
		catch (Exception ex)
		{
			try
			{
				DeviceManager.FeeCenterModule.EjectTicket();
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2);
				Console.WriteLine(ex2.Message);
			}
			Logger.Error(ex);
			Global.ShowMessage(ex.Message);
		}
		return rtn;
	}

	private void FormMainNew_RecordContrastEvent(RecordContrastArgs recordContrastInfo)
	{
	}

	private void FormMain_ExitContrastEvent(ExitContrastArgs exitContrastInfo)
	{
	}

	private void FormMainNew_ParkingSpacesChangeEvent(ParkAreaExtend obj)
	{
		Invoke((MethodInvoker)delegate
		{
			Console.WriteLine("FormMainNew_ParkingSpacesChangeEvent");
			UCParkAreaEX_Item2 uCParkAreaEX_Item = ucParkAreaEX2.m_AreaItems.First((UCParkAreaEX_Item2 m) => m.Name == "Area" + obj.AreaID + obj.ParkTypeID);
			uCParkAreaEX_Item.labTimeRemain.Text = obj.TimeChargRemain.ToString();
			int num = obj.FloatParkSupply - obj.FloatParkUse;
			uCParkAreaEX_Item.labStaffRemain.Text = num.ToString();
			int num2 = Convert.ToInt32((!obj.FloatParkSupply5.HasValue) ? "0" : obj.FloatParkSupply5.ToString()) - Convert.ToInt32((!obj.FloatParkUse5.HasValue) ? "0" : obj.FloatParkUse5.ToString());
			uCParkAreaEX_Item.labStudentRemain.Text = num2.ToString();
			uCParkAreaEX_Item.setFull_callBack(obj.CustomFunnSigh);
			if (obj.ParkTypeID == 1)
			{
				labPCRe.Text = obj.TimeChargRemain.ToString();
				if (obj.TimeChargRemain <= 0)
				{
					labPCRe.ForeColor = Color.Red;
				}
				else
				{
					labPCRe.ForeColor = Color.Black;
				}
			}
			else if (obj.ParkTypeID == 2)
			{
				labPMRe.Text = obj.TimeChargRemain.ToString();
				if (obj.TimeChargRemain <= 0)
				{
					labPMRe.ForeColor = Color.Red;
				}
				else
				{
					labPMRe.ForeColor = Color.Black;
				}
			}
			if (obj.TimeChargRemain <= 0)
			{
				uCParkAreaEX_Item.labTimeRemain.ForeColor = Color.Red;
			}
			else
			{
				uCParkAreaEX_Item.labTimeRemain.ForeColor = Color.White;
			}
			if (num <= 0)
			{
				uCParkAreaEX_Item.labStaffRemain.ForeColor = Color.Red;
			}
			else
			{
				uCParkAreaEX_Item.labStaffRemain.ForeColor = Color.White;
			}
			if (num2 <= 0)
			{
				uCParkAreaEX_Item.labStudentRemain.ForeColor = Color.Red;
			}
			else
			{
				uCParkAreaEX_Item.labStudentRemain.ForeColor = Color.White;
			}
		}, null);
	}

	private void FormMainNew_NoticeEvent(NoticeArgs obj)
	{
		Console.WriteLine("FormMainNew_NoticeEvent");
	}

	private void FormMainNew_PasstraceChangeEvent(PassTrace obj)
	{
		Console.WriteLine("FormMainNew_PasstraceChangeEvent");
		ucPasstraceEX2.Add(obj);
	}

	private void FormMainNew_GateStatusChangeEvent(DeviceStatus obj)
	{
		try
		{
			Console.WriteLine("FormMainNew_GateStatusChangeEvent");
			GateStatus gs = new GateStatus
			{
				ErrCode = obj.DeviceCode
			};
			ucGatesEX2.m_GateItems.First((UCGatesEX_Item m) => m.Name == "Gate" + obj.GateID).deviceState(gs, obj, obj.DeviceType);
		}
		catch (Exception ex)
		{
			FormMainNew_NoticeEvent(new NoticeArgs
			{
				Content = ex.ToString()
			});
		}
	}

	private void FormMainNew_DisabilityPressEvent(DisabilityPressArgs obj)
	{
		try
		{
			ucGatesEX2.m_GateItems.First((UCGatesEX_Item m) => m.Name == "Gate" + obj.GateID).DisabilityState(obj);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void btnDemage_Click(object sender, EventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent -= FeeCenterModule_TicketScanEvent;
		}
		using (FormTimeChargeDemage formTimeChargeDemage = new FormTimeChargeDemage())
		{
			formTimeChargeDemage.ShowDialog();
		}
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent += FeeCenterModule_TicketScanEvent;
		}
	}

	private void btnLost_Click(object sender, EventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent -= FeeCenterModule_TicketScanEvent;
		}
		using (FormTimeChargeLost formTimeChargeLost = new FormTimeChargeLost())
		{
			formTimeChargeLost.ShowDialog();
		}
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent += FeeCenterModule_TicketScanEvent;
		}
	}

	private void btnCompensationFare_Click(object sender, EventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent -= FeeCenterModule_TicketScanEvent;
		}
		using (FormTimeCompensationFare formTimeCompensationFare = new FormTimeCompensationFare())
		{
			formTimeCompensationFare.ShowDialog();
		}
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent += FeeCenterModule_TicketScanEvent;
		}
	}

	private void toolStripButton3_Click(object sender, EventArgs e)
	{
		using FormTimeCharge formTimeCharge = new FormTimeCharge();
		formTimeCharge.ShowDialog();
	}

	private void toolStripButton1_Click(object sender, EventArgs e)
	{
		using FormTimeChargeTimeOut formTimeChargeTimeOut = new FormTimeChargeTimeOut();
		formTimeChargeTimeOut.ShowDialog();
	}

	private void panExit_Click(object sender, EventArgs e)
	{
		if (Global.ShowDialog(LangManager.GetLangString("CarPark.FormMainNew.buttonExit"), OkFocus: true) != DialogResult.Cancel)
		{
			Close();
		}
	}

	private void FormMain_Load(object sender, EventArgs e)
	{
		LoadImage();
		ThreadPool.QueueUserWorkItem(delegate
		{
			try
			{
				LogOnResult logOnResult = ((IMPPOSTranscation)DeviceManager.FeeCenterModule).Logon();
				if (logOnResult.CommandResult == CommandResult.Fail)
				{
					Global.ShowMessage(logOnResult.ErrDescription);
				}
			}
			catch (Exception message2)
			{
				Logger.Error(message2);
			}
		});
		Thread thread = new Thread(doUpload);
		thread.IsBackground = true;
		thread.Start();
		try
		{
			if (DataBuffer2018.ParkAreaExtends != null)
			{
				foreach (ParkAreaExtend parkAreaExtend in DataBuffer2018.ParkAreaExtends)
				{
					switch (parkAreaExtend.ParkType)
					{
					case EnumParkType.MCycle:
						labPMRe.Text = parkAreaExtend.TimeChargRemain.ToString();
						if (parkAreaExtend.TimeChargRemain <= 0)
						{
							labPMRe.ForeColor = Color.Red;
						}
						break;
					case EnumParkType.Private:
						labPCRe.Text = parkAreaExtend.TimeChargRemain.ToString();
						if (parkAreaExtend.TimeChargRemain <= 0)
						{
							labPCRe.ForeColor = Color.Red;
						}
						break;
					}
				}
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		ucClock1.Start();
	}

	private void NewDelegate()
	{
		DateTime dateTime = DateTime.Now.AddDays(-1.0);
		while (true)
		{
			Thread.Sleep(60000);
			if (!(dateTime.Date != DateTime.Now.Date) || DateTime.Now.Hour != Config.MPPOSSyncTime)
			{
				continue;
			}
			try
			{
				if (((IMPPOSTranscation)DeviceManager.FeeCenterModule).Logon().CommandResult == CommandResult.Fail)
				{
					Logger.Error("MPPOS LogonError");
				}
				SignTransactionsResult signTransactionsResult = SignInTransactions(((IMPPOSTranscation)DeviceManager.FeeCenterModule).SignInTransactions());
				if (signTransactionsResult.CommandResult == CommandResult.Fail)
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

	public SignTransactionsResult SignInTransactions(SignTransactionsResult m_SignTransactionsResult)
	{
		MPass_POS_Signin mPass_POS_Signin = new MPass_POS_Signin
		{
			ShiftID = DataBuffer.CurrentShiftRecord.ShiftID,
			SignInTime = DateTime.Now
		};
		MPass_POS_Signin_Detail mPass_POS_Signin_Detail = null;
		EntityHelper.CopyEntity(mPass_POS_Signin, mPass_POS_Signin);
		if (m_SignTransactionsResult.ERRQPCount == null || m_SignTransactionsResult.ERRQPCount.Count <= 0)
		{
			return m_SignTransactionsResult;
		}
		foreach (SignTransactionResultERRQP item in m_SignTransactionsResult.ERRQPCount)
		{
			mPass_POS_Signin_Detail = new MPass_POS_Signin_Detail
			{
				SiginInID = mPass_POS_Signin.SigiInID
			};
			EntityHelper.CopyEntity(item, mPass_POS_Signin_Detail);
		}
		try
		{
			SaveMPass_POS_SigninArgs saveMPass_POS_SigninArgs = new SaveMPass_POS_SigninArgs();
			Common._Carpark2018ServiceContext.CommunicationChannel.SaveMPass_POS_Signin(saveMPass_POS_SigninArgs, mPass_POS_Signin, mPass_POS_Signin_Detail);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		return m_SignTransactionsResult;
	}

	private void LoadImage()
	{
		btnLogin.Image = ImageManager.GetImage("", "ICON01");
		btnLanguage.Image = ImageManager.GetImage("", "ICON02");
		btnOpenDrawer.Image = ImageManager.GetImage("", "ICON06");
		btnShift.Image = ImageManager.GetImage("", "ICON07");
		btnExit.Image = ImageManager.GetImage("", "ICON13");
		btnParkAreaExtend.Image = ImageManager.GetImage("", "ICON14");
		btnTimeChargeEx.Image = ImageManager.GetImage("", "ICON15");
		btnVoidCharge.Image = ImageManager.GetImage("", "ICON16");
		btnOther.Image = ImageManager.GetImage("", "ICON10");
		btnRentalType.Image = ImageManager.GetImage("", "ICON18");
		btnDepositCharge.Image = ImageManager.GetImage("", "ICON17");
		btnMpassCharge.Image = ImageManager.GetImage("", "ICON19");
		btnMpassQuery.Image = ImageManager.GetImage("", "ICON20");
		btnCheckRental.Image = ImageManager.GetImage("", "ICON21");
		btnLPCheck.Image = ImageManager.GetImage("", "ICON03");
		btnLost.Image = ImageManager.GetImage("", "ICON04");
		btnTransferTicket.Image = ImageManager.GetImage("", "ICON22");
		btnSwitchCam.Image = ImageManager.GetImage("", "ICON15");
		btnSetFree.Image = ImageManager.GetImage("", "ICON05");
	}

	private void InitButton()
	{
		foreach (Control control in base.Controls)
		{
			if (control is ToolStrip)
			{
				_ = (ToolStrip)control;
			}
		}
	}

	private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		ucClock1.Stop();
	}

	private void btnLogin_Click(object sender, EventArgs e)
	{
		try
		{
			if (DataBuffer2018.CurrentStaff != null)
			{
				DataBuffer2018.CurrentStaff = null;
				btnLogin.Text = LangManager.GetLangString("CarPark.FormMain.btnLogin");
				EnabledState(status: false);
				return;
			}
			Thread thread = new Thread(NewDelegate);
			thread.IsBackground = true;
			thread.Start();
			using (FormLogin formLogin = new FormLogin())
			{
				if (formLogin.ShowDialog() == DialogResult.OK)
				{
					DataBuffer2018.CurrentStaff = formLogin.StaffInfo;
					btnLogin.Text = formLogin.StaffInfo.StaffName;
					EnabledState(status: true);
				}
			}
			ReadTextFile();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnLogin_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			btnLogin_Click(null, null);
		}
		else if (e.Button == MouseButtons.Right && DataBuffer2018.CurrentStaff != null)
		{
			btnChangePsw_Click(null, null);
		}
	}

	private void btnChangePsw_Click(object sender, EventArgs e)
	{
		new FormChangePsw().ShowDialog();
	}

	private void EnabledState(bool status)
	{
		btnOpenDrawer.Enabled = status;
		btnShift.Enabled = status;
		btnParkAreaExtend.Enabled = status;
		btnVoidCharge.Enabled = status;
		ucParkAreaEX2.Enabled = status;
		ucGatesEX2.Enabled = status;
		btnOther.Enabled = status;
		btnRentalType.Enabled = status;
		btnDepositCharge.Enabled = status;
		btnMpassCharge.Enabled = status;
		btnMpassQuery.Enabled = status;
		btnCheckRental.Enabled = status;
		btnLPCheck.Enabled = status;
		btnLost.Enabled = status;
		btnTransferTicket.Enabled = status;
		btnSetFree.Enabled = status;
		panFill.Enabled = status;
		if (status)
		{
			try
			{
				CheckRole(this);
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				MessageBox.Show(ex.Message);
			}
		}
		btnSwitchCam.Enabled = status;
	}

	private void FormMain_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			btnLogin_Click(null, null);
		}
		if (e.KeyCode == Keys.Return)
		{
			btnSearch_Click(null, null);
		}
		if (e.KeyCode == Keys.Escape)
		{
			ClearLPInfo();
			Action<RequestArgs> action = EQM_Fee;
			RequestArgs obj = new RequestArgs
			{
				Extend1 = "FEE_CANCEL",
				Extend3 = Settings.Default.ServerEQM
			};
			action.BeginInvoke(obj, EndAsync, null);
		}
		_ = e.KeyCode;
		_ = e.KeyCode;
		if (e.KeyCode != Keys.Oemplus)
		{
			return;
		}
		try
		{
			GetLastChargeRecordArgs getLastChargeRecordArgs = new GetLastChargeRecordArgs();
			getLastChargeRecordArgs.PayStationName = Settings.Default.OnlyID;
			getLastChargeRecordArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			ChargeRecord chargeRecord = new ChargeRecord();
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = new MPass_POS_Transaction_Detail();
			TransactionData transactionData = new TransactionData();
			GetLastChargeRecordReturn lastChargeRecord = Common._Carpark2018ServiceContext.CommunicationChannel.GetLastChargeRecord(getLastChargeRecordArgs, out chargeRecord, out mPass_POS_Transaction_Detail, out transactionData);
			if (lastChargeRecord.ISOK)
			{
				if (chargeRecord.PayType.HasValue && chargeRecord.PayType == 2)
				{
					MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail2 = mPass_POS_Transaction_Detail;
					if (mPass_POS_Transaction_Detail2 == null)
					{
						Logger.Error($"找不到交易編號為{chargeRecord.TimeChargeID}的POS交易記錄");
						return;
					}
					PrintUtils.smethod_0(chargeRecord, mPass_POS_Transaction_Detail2, transactionData);
					Logger.Debug(lastChargeRecord.FormatReceiptStr);
					DeviceManager.FeeCenterModule.Print(lastChargeRecord.FormatReceiptStr);
				}
				else
				{
					Logger.Debug(lastChargeRecord.FormatReceiptStr);
					DeviceManager.FeeCenterModule.Print(lastChargeRecord.FormatReceiptStr);
				}
			}
			else
			{
				Console.WriteLine(lastChargeRecord.ErrCode);
				Logger.Error(lastChargeRecord.ErrCode);
				MessageBox.Show(lastChargeRecord.ErrCode);
			}
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
			MessageBox.Show(ex.Message);
		}
	}

	private void btnShift_Click(object sender, EventArgs e)
	{
		using FormShift formShift = new FormShift();
		formShift.ShowDialog();
	}

	private void btnTimeChargeEx_Click(object sender, EventArgs e)
	{
		using UcTimeCharge ucTimeCharge = new UcTimeCharge();
		ucTimeCharge.ShowDialog();
	}

	private void btnParkAreaExtend_Click(object sender, EventArgs e)
	{
		using FormParkAreaExtend formParkAreaExtend = new FormParkAreaExtend();
		formParkAreaExtend.ShowDialog();
	}

	private void btnVoidCharge_Click(object sender, EventArgs e)
	{
		using FormVoidCharge formVoidCharge = new FormVoidCharge();
		formVoidCharge.ShowDialog();
	}

	private void btnOther_Click(object sender, EventArgs e)
	{
		try
		{
			int? num = 0;
			foreach (StaffType staffType in DataBuffer2018.StaffTypes)
			{
				if (DataBuffer2018.CurrentStaff.StaffTypeId == staffType.StaffTypeID)
				{
					num = staffType.SystemCode;
					break;
				}
			}
			if (num.HasValue)
			{
				string url;
				if (Config.AutoLoginBackstage)
				{
					string[] obj = new string[9]
					{
						Config.ReportPath,
						"/default.aspx?StaffCode=",
						DataBuffer2018.CurrentStaff.StaffCode,
						"&StaffPwd=",
						DataBuffer2018.CurrentStaff.StaffPwd,
						"&subSystem=",
						null,
						null,
						null
					};
					int num2 = 6;
					int? num3 = num;
					obj[num2] = num3.ToString();
					obj[7] = "&lang=";
					obj[8] = ((LangManager.CurLanguage == SysLanguage.CHT) ? "zh-CN" : "en-US");
					url = string.Concat(obj);
				}
				else
				{
					url = Config.ReportPath + "/default.aspx";
				}

				// 主機存活檢查：不通則只換 IP
				Uri primaryUri = new Uri(Config.ReportPath);
				string backupUrl = Config.BackupReportPath;
				if (string.IsNullOrEmpty(backupUrl))
					backupUrl = Properties.Settings.Default.BackupReportPath;
				if (!string.IsNullOrEmpty(backupUrl) && !IsHostAlive(primaryUri.Host, primaryUri.Port, 3000))
				{
					Uri backupUri = new Uri(backupUrl);
					url = new UriBuilder(url) { Host = backupUri.Host, Port = backupUri.Port }.ToString();
				}

				Process.Start(url);
			}
			else
			{
				MessageBox.Show("該員工沒有系統權限");
			}
		}
		catch (Exception ex)
		{
			Global.ShowMessage(LangManager.GetLangString(ex.Message));
			Logger.Error(ex);
		}
		ReadTextFile();
	}

	private bool IsHostAlive(string host, int port, int timeoutMs)
	{
		try
		{
			System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
			IAsyncResult ar = client.BeginConnect(host, port, null, null);
			bool connected = ar.AsyncWaitHandle.WaitOne(timeoutMs, false);
			if (connected)
			{
				client.EndConnect(ar);
				return true;
			}
			client.Close();
			return false;
		}
		catch
		{
			return false;
		}
	}

	private void btnCHT_Click(object sender, EventArgs e)
	{
		LangManager.CurLanguage = SysLanguage.CHT;
	}

	private void btnPT_Click(object sender, EventArgs e)
	{
		LangManager.CurLanguage = SysLanguage.ENG;
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		btnExit.Text = LangManager.GetLangString("CarPark.FormMain.panExit");
		btnLanguage.Text = LangManager.GetLangString("CarPark.FormMain.btnLanguage");
		btnOpenDrawer.Text = LangManager.GetLangString("CarPark.FormMain.btnOpenDrawer");
		btnOther.Text = LangManager.GetLangString("CarPark2018.Forms.FormMain.btnOther");
		btnParkAreaExtend.Text = LangManager.GetLangString("CarPark.Forms.FormSystemConfig.tabLocationExtend");
		btnShift.Text = LangManager.GetLangString("CarPark.FormMain.btnShift");
		btnTimeChargeEx.Text = LangManager.GetLangString("CarPark2018.Forms.FormTimeChargeEx.labTitle");
		btnVoidCharge.Text = LangManager.GetLangString("CarPark2018.Forms.FormVoidCharge.labTitle");
		btnTransferTicket.Text = LangManager.GetLangString("CarPark.FormMain.btnTransferTicket");
		btnRentalType.Text = LangManager.GetLangString("CarPark.FormMain.btnRentalType");
		btnDepositCharge.Text = LangManager.GetLangString("CarPark.FormMain.btnDepositCharge");
		btnCheckRental.Text = LangManager.GetLangString("CarPark.FormMain.btnCheckRental");
		btnMpassQuery.Text = LangManager.GetLangString("CarPark.FormMain.btnMpassQuery");
		btnMpassCharge.Text = LangManager.GetLangString("CarPark.FormMain.btnMpassCharge");
		btnSetFree.Text = LangManager.GetLangString("CarPark.FormMain.btnSetFree");
		btnLPCheck.Text = LangManager.GetLangString("CarPark.FormMain.btnLPCheck");
		if (DataBuffer2018.CurrentStaff == null)
		{
			btnLogin.Text = LangManager.GetLangString("CarPark.FormMain.btnLogin");
		}
		ckStartTime.Text = LangManager.GetLangString("CarPark.FormMain.ckStartTime");
		labEndTime.Text = LangManager.GetLangString("CarPark.FormMain.labEndTime");
		labLP.Text = LangManager.GetLangString("CarPark.FormMain.labLP");
		btnSearch.Text = LangManager.GetLangString("CarPark.FormMain.btnSearch");
		btnNext.Text = LangManager.GetLangString("CarPark.FormMain.btnNext");
		btnPrv.Text = LangManager.GetLangString("CarPark.FormMain.btnPrv");
		labPCar.Text = LangManager.GetLangString("CarPark.FormMain.labPCar");
		labPMotor.Text = LangManager.GetLangString("CarPark.FormMain.labPMotor");
		btnLost.Text = LangManager.GetLangString("CarPark.FormMain.btnLost");
		tabPage1.Text = LangManager.GetLangString("CarPark.FormMain.tabPage1");
		tabPage2.Text = LangManager.GetLangString("CarPark.FormMain.tabPage2");
		btnSwitchCam.Text = LangManager.GetLangString("CarPark.FormMain.btnSwitchCam");
	}

	private void btnOpenDrawer_Click(object sender, EventArgs e)
	{
		try
		{
			DeviceManager.FeeCenterModule.OpenCash();
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		try
		{
			LPDBHelper.LogOperation(33, "", (DataBuffer2018.CurrentStaff != null) ? DataBuffer2018.CurrentStaff.StaffCode : "Auto", Settings.Default.OnlyID);
		}
		catch (Exception message2)
		{
			Logger.Error(message2);
		}
	}

	private void btnRentalType_Click(object sender, EventArgs e)
	{
		try
		{
			using FormRentalCharge formRentalCharge = new FormRentalCharge();
			formRentalCharge.ShowDialog();
		}
		catch (Exception ex)
		{
			Global.ShowMessage(ex.Message);
		}
	}

	private void btnMpassQuery_Click(object sender, EventArgs e)
	{
		try
		{
			using FormMPassQuery formMPassQuery = new FormMPassQuery();
			formMPassQuery.ShowDialog();
		}
		catch (Exception ex)
		{
			Global.ShowMessage(ex.Message);
		}
	}

	private void btnMpassCharge_Click(object sender, EventArgs e)
	{
		try
		{
			using FormMPassCharge formMPassCharge = new FormMPassCharge();
			formMPassCharge.ShowDialog();
		}
		catch (Exception ex)
		{
			Global.ShowMessage(ex.Message);
		}
	}

	private void btnDepositCharge_Click(object sender, EventArgs e)
	{
		try
		{
			using FormDepositCharge formDepositCharge = new FormDepositCharge();
			formDepositCharge.ShowDialog();
		}
		catch (Exception ex)
		{
			Global.ShowMessage(ex.Message);
		}
	}

	private void doUpload()
	{
		List<ChargeRecord> list = DBHelper.ExecuteList<ChargeRecord>("select * from ChargeRecord where isupload='0'", CommandType.Text, (IDbDataParameter[])null);
		if (list == null)
		{
			return;
		}
		foreach (ChargeRecord item in list)
		{
			SaveChargeRecordArgs saveChargeRecordArgs = DBHelper.SelectSaveChargeRecordArgs(item.TimeChargeID);
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = DBHelper.SelectMPass_POS_Transaction_Detail(item.TimeChargeID);
			BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend = DBHelper.SelectBOC_Gate_TransactionExtend(item.TimeChargeID);
			try
			{
				if (saveChargeRecordArgs != null && Common._Carpark2018ServiceContext.CommunicationChannel.SaveElectronicChargeRecord(saveChargeRecordArgs, (EnumParkType)item.ParkTypeID, item, mPass_POS_Transaction_Detail, bOC_Gate_TransactionExtend).ISOK)
				{
					DBHelper.ExecuteNonQuery($"update ChargeRecord set isupload='1' where timechargeid={item.TimeChargeID}", CommandType.Text, (IDbDataParameter[])null);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("重新上传数据失败\n" + ex.ToString());
				Logger.Error(ex);
			}
		}
	}

	private void btnCheckRental_Click(object sender, EventArgs e)
	{
		using FormCheckRental formCheckRental = new FormCheckRental();
		formCheckRental.ShowDialog();
	}

	private void btnTransferTicket_Click(object sender, EventArgs e)
	{
		try
		{
			using FormTransferTicket formTransferTicket = new FormTransferTicket();
			formTransferTicket.ShowDialog();
		}
		catch (Exception)
		{
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
			using FormTimeCharge formTimeCharge = new FormTimeCharge();
			formTimeCharge.ShowDialog();
		}
		catch (Exception)
		{
		}
	}

	private void ckStartTime_CheckedChanged(object sender, EventArgs e)
	{
		if (ckStartTime.Checked)
		{
			dpStarttime.Enabled = true;
			dpEndTime.Enabled = true;
		}
		else
		{
			dpStarttime.Enabled = false;
			dpEndTime.Enabled = false;
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		Console.Write("btnSearch_Click");
		if (!ckStartTime.Checked && string.IsNullOrEmpty(txtLP.Text.Trim()))
		{
			txtLP.Text = "";
			ClearLPInfo();
			return;
		}
		try
		{
			nFeetime = DateTime.Now;
			ClearLPInfo();
			GetView_TransactionAndLPArgs getView_TransactionAndLPArgs = new GetView_TransactionAndLPArgs();
			getView_TransactionAndLPArgs.LicensePlate = txtLP.Text;
			if (ckStartTime.Checked)
			{
				getView_TransactionAndLPArgs.InStartTime = dpStarttime.Value;
				getView_TransactionAndLPArgs.InEndTime = dpEndTime.Value;
			}
			mListLPRS = new List<view_transactionandlp>();
			btnSearch.Enabled = false;
			Application.DoEvents();
			LPDBHelper.GetView_TransactionAndLP(getView_TransactionAndLPArgs, out mListLPRS);
			if (mListLPRS.Count > 0)
			{
				nPageCount = mListLPRS.Count / nPageNum;
				if (mListLPRS.Count % nPageNum > 0)
				{
					nPageCount++;
				}
				nCurentPage = 1;
				LoadLPInfo();
			}
			else
			{
				ClearLPInfo();
				Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.Show4"));
			}
			btnSearch.Enabled = true;
			Application.DoEvents();
		}
		catch (TimeoutException message)
		{
			Logger.Error(message);
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
			btnSearch.Enabled = true;
			Application.DoEvents();
		}
		catch (Exception message2)
		{
			Logger.Error(message2);
			btnSearch.Enabled = true;
			Application.DoEvents();
		}
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		if (nCurentPage < nPageCount)
		{
			nCurentPage++;
			LoadLPInfo();
		}
	}

	private void btnPrv_Click(object sender, EventArgs e)
	{
		if (nCurentPage > 1)
		{
			nCurentPage--;
			LoadLPInfo();
		}
	}

	private void btnChargeRecord_Click(object sender, EventArgs e)
	{
	}

	private void picLP1_Click(object sender, EventArgs e)
	{
		try
		{
			PictureBox pictureBox = (PictureBox)sender;
			Console.WriteLine(pictureBox.TabIndex);
			nFeetime = DateTime.Now;
			CalcTicketFeeArgsV2 calcTicketFeeArgsV = new CalcTicketFeeArgsV2();
			calcTicketFeeArgsV.TicketNumber = "UNKNOWN";
			calcTicketFeeArgsV.PayStationName = Settings.Default.OnlyID;
			calcTicketFeeArgsV.SerialNumber = "";
			calcTicketFeeArgsV.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			calcTicketFeeArgsV.ISFine = false;
			calcTicketFeeArgsV.ChargeTime = nFeetime;
			calcTicketFeeArgsV.InTime = nFeetime;
			calcTicketFeeArgsV.BarCode = "";
			switch (pictureBox.TabIndex)
			{
			case 0:
				calcTicketFeeArgsV.TicketNumber = labLP1.Text;
				break;
			case 1:
				calcTicketFeeArgsV.TicketNumber = labLP2.Text;
				break;
			case 2:
				calcTicketFeeArgsV.TicketNumber = labLP3.Text;
				break;
			case 3:
				calcTicketFeeArgsV.TicketNumber = labLP4.Text;
				break;
			case 4:
				calcTicketFeeArgsV.TicketNumber = labLP5.Text;
				break;
			case 5:
				calcTicketFeeArgsV.TicketNumber = labLP6.Text;
				break;
			}
			if (string.IsNullOrWhiteSpace(calcTicketFeeArgsV.TicketNumber))
			{
				return;
			}
			using FormLPPayFeeCashier formLPPayFeeCashier = new FormLPPayFeeCashier();
			formLPPayFeeCashier.FeeArgs = calcTicketFeeArgsV;
			formLPPayFeeCashier.Transactionandlp = mListLPRS[(nCurentPage - 1) * nPageNum + pictureBox.TabIndex];
			formLPPayFeeCashier.IsImageActivate = nIsImageAct;
			formLPPayFeeCashier.ShowDialog();
			if (formLPPayFeeCashier.DialogResult == DialogResult.OK)
			{
				if (SysLanguage.ENG == LangManager.CurLanguage)
				{
					labFinishMsg.Text = "Payment Completed";
				}
				else
				{
					labFinishMsg.Text = "交易完成";
				}
				labFinishMsg.ForeColor = Color.Green;
				TickMsg();
				ClearLPInfo();
				txtLP.Text = "";
				txtLP.Focus();
			}
			else
			{
				if (SysLanguage.ENG == LangManager.CurLanguage)
				{
					labFinishMsg.Text = "Payment Cancel";
				}
				else
				{
					labFinishMsg.Text = "交易取消";
				}
				labFinishMsg.ForeColor = Color.Red;
				TickMsg();
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
		}
	}

	private void btnLPCheck_Click(object sender, EventArgs e)
	{
		try
		{
			using FormLicensePlateCorrect formLicensePlateCorrect = new FormLicensePlateCorrect();
			formLicensePlateCorrect.ShowDialog();
		}
		catch (Exception message)
		{
			Logger.Error(message);
			Global.ShowMessage("錯誤，請重新操作");
		}
	}

	private void LoadLPInfo()
	{
		bool flag = (nIsImageAct = LPDBHelper.Ping(Config.LicensePlatePath));
		try
		{
			labLP1.Text = mListLPRS[(nCurentPage - 1) * nPageNum].InCardCode;
			labText1.Text = ShowInfo(mListLPRS[(nCurentPage - 1) * nPageNum]);
		}
		catch (Exception)
		{
			labLP1.Text = "";
			labText1.Text = "";
		}
		try
		{
			if (flag)
			{
				picLP1.Image = Image.FromFile(Config.LicensePlatePath + mListLPRS[(nCurentPage - 1) * nPageNum].ImagePath);
			}
			else
			{
				picLP1.Image = ImageManager.GetImage("", "cancel");
			}
		}
		catch (Exception ex2)
		{
			Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + ex2.Message);
			picLP1.Image = ImageManager.GetImage("", "cancel");
		}
		try
		{
			labLP2.Text = mListLPRS[(nCurentPage - 1) * nPageNum + 1].InCardCode;
			labText2.Text = ShowInfo(mListLPRS[(nCurentPage - 1) * nPageNum + 1]);
		}
		catch (Exception)
		{
			labLP2.Text = "";
			labText2.Text = "";
		}
		try
		{
			if (flag)
			{
				picLP2.Image = Image.FromFile(Config.LicensePlatePath + mListLPRS[(nCurentPage - 1) * nPageNum + 1].ImagePath);
			}
			else
			{
				picLP2.Image = ImageManager.GetImage("", "cancel");
			}
		}
		catch (Exception)
		{
			picLP2.Image = ImageManager.GetImage("", "cancel");
		}
		try
		{
			labLP3.Text = mListLPRS[(nCurentPage - 1) * nPageNum + 2].InCardCode;
			labText3.Text = ShowInfo(mListLPRS[(nCurentPage - 1) * nPageNum + 2]);
		}
		catch (Exception)
		{
			labLP3.Text = "";
			labText3.Text = "";
		}
		try
		{
			if (flag)
			{
				picLP3.Image = Image.FromFile(Config.LicensePlatePath + mListLPRS[(nCurentPage - 1) * nPageNum + 2].ImagePath);
			}
			else
			{
				picLP3.Image = ImageManager.GetImage("", "cancel");
			}
		}
		catch (Exception)
		{
			picLP3.Image = ImageManager.GetImage("", "cancel");
		}
		try
		{
			labLP4.Text = mListLPRS[(nCurentPage - 1) * nPageNum + 3].InCardCode;
			labText4.Text = ShowInfo(mListLPRS[(nCurentPage - 1) * nPageNum + 3]);
		}
		catch (Exception)
		{
			labLP4.Text = "";
			labText4.Text = "";
		}
		try
		{
			if (flag)
			{
				picLP4.Image = Image.FromFile(Config.LicensePlatePath + mListLPRS[(nCurentPage - 1) * nPageNum + 3].ImagePath);
			}
			else
			{
				picLP4.Image = ImageManager.GetImage("", "cancel");
			}
		}
		catch (Exception)
		{
			picLP4.Image = ImageManager.GetImage("", "cancel");
		}
		try
		{
			labLP5.Text = mListLPRS[(nCurentPage - 1) * nPageNum + 4].InCardCode;
			labText5.Text = ShowInfo(mListLPRS[(nCurentPage - 1) * nPageNum + 4]);
		}
		catch (Exception)
		{
			labLP5.Text = "";
			labText5.Text = "";
		}
		try
		{
			if (flag)
			{
				picLP5.Image = Image.FromFile(Config.LicensePlatePath + mListLPRS[(nCurentPage - 1) * nPageNum + 4].ImagePath);
			}
			else
			{
				picLP5.Image = ImageManager.GetImage("", "cancel");
			}
		}
		catch (Exception)
		{
			picLP5.Image = ImageManager.GetImage("", "cancel");
		}
		try
		{
			labLP6.Text = mListLPRS[(nCurentPage - 1) * nPageNum + 5].InCardCode;
			labText6.Text = ShowInfo(mListLPRS[(nCurentPage - 1) * nPageNum + 5]);
		}
		catch (Exception)
		{
			labLP6.Text = "";
			labText6.Text = "";
		}
		try
		{
			if (flag)
			{
				picLP6.Image = Image.FromFile(Config.LicensePlatePath + mListLPRS[(nCurentPage - 1) * nPageNum + 5].ImagePath);
			}
			else
			{
				picLP6.Image = ImageManager.GetImage("", "cancel");
			}
		}
		catch (Exception)
		{
			picLP6.Image = ImageManager.GetImage("", "cancel");
		}
		new List<LPShowItem>
		{
			new LPShowItem(labLP1.Text, labText1.Text, picLP1.Image),
			new LPShowItem(labLP2.Text, labText2.Text, picLP2.Image),
			new LPShowItem(labLP3.Text, labText3.Text, picLP3.Image),
			new LPShowItem(labLP4.Text, labText4.Text, picLP4.Image),
			new LPShowItem(labLP5.Text, labText5.Text, picLP5.Image),
			new LPShowItem(labLP6.Text, labText6.Text, picLP6.Image)
		};
		if (nCurentPage <= 1)
		{
			btnPrv.Visible = false;
			btnNext.Visible = true;
		}
		else if (nCurentPage >= nPageCount)
		{
			btnNext.Visible = false;
			btnPrv.Visible = true;
		}
		else
		{
			btnNext.Visible = true;
			btnPrv.Visible = true;
		}
	}

	private void ClearLPInfo()
	{
		try
		{
			nPageCount = 0;
			nCurentPage = 0;
			if (mListLPRS != null)
			{
				mListLPRS.Clear();
			}
			labLP1.Text = "";
			labText1.Text = "";
			picLP1.Image = ImageManager.GetImage("", "cancel");
			labLP2.Text = "";
			labText2.Text = "";
			picLP2.Image = ImageManager.GetImage("", "cancel");
			labLP3.Text = "";
			labText3.Text = "";
			picLP3.Image = ImageManager.GetImage("", "cancel");
			labLP4.Text = "";
			labText4.Text = "";
			picLP4.Image = ImageManager.GetImage("", "cancel");
			labLP5.Text = "";
			labText5.Text = "";
			picLP5.Image = ImageManager.GetImage("", "cancel");
			labLP6.Text = "";
			labText6.Text = "";
			picLP6.Image = ImageManager.GetImage("", "cancel");
			new List<LPShowItem>
			{
				new LPShowItem(labLP1.Text, labText1.Text, picLP1.Image),
				new LPShowItem(labLP2.Text, labText2.Text, picLP2.Image),
				new LPShowItem(labLP3.Text, labText3.Text, picLP3.Image),
				new LPShowItem(labLP4.Text, labText4.Text, picLP4.Image),
				new LPShowItem(labLP5.Text, labText5.Text, picLP5.Image),
				new LPShowItem(labLP6.Text, labText6.Text, picLP6.Image)
			};
			if (nCurentPage <= 1)
			{
				btnPrv.Visible = false;
				btnNext.Visible = true;
			}
			else if (nCurentPage >= nPageCount)
			{
				btnNext.Visible = false;
				btnPrv.Visible = true;
			}
			else
			{
				btnNext.Visible = true;
				btnPrv.Visible = true;
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private string ShowInfo(view_transactionandlp item)
	{
		string empty = string.Empty;
		CheckFeeReturn checkFeeReturn = LPDBHelper.CheckFeeInfo(new CheckFeeArgs
		{
			TransactionID = item.TransactionID,
			PayLicensePlate = item.InCardCode
		});
		if (checkFeeReturn.IsPaid)
		{
			if (checkFeeReturn.IsTimeout)
			{
				if (SysLanguage.ENG == LangManager.CurLanguage)
				{
					return "Timeout";
				}
				return "已超時";
			}
			if (SysLanguage.ENG == LangManager.CurLanguage)
			{
				return "Paid";
			}
			return "已付費";
		}
		return item.InTime.ToString("yyyy-MM-dd HH:mm");
	}

	private void txtLP_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b')
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	private void btnReleaseMan_Click(object sender, EventArgs e)
	{
		try
		{
			DataBuffer2018.CheckRole(MethodBase.GetCurrentMethod());
			using FormLPPayReleaseMgmt formLPPayReleaseMgmt = new FormLPPayReleaseMgmt();
			formLPPayReleaseMgmt.ShowDialog();
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Global.ShowMessage(LangManager.GetLangString(ex.Message));
		}
	}

	private void btnLost_Click_1(object sender, EventArgs e)
	{
		using FormLPPayLost formLPPayLost = new FormLPPayLost();
		formLPPayLost.ShowDialog();
	}

	private void CheckRole(object target)
	{
		Type type = target.GetType();
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
		for (int i = 0; i < fields.Length; i++)
		{
			object value = fields[i].GetValue(target);
			if (!(value is ToolStripButton))
			{
				continue;
			}
			ToolStripButton toolStripButton = value as ToolStripButton;
			string str = type.FullName + "." + toolStripButton.Name;
			if (!(toolStripButton.Name == "btnLogin") && !(toolStripButton.Name == "btnLanguage") && !(toolStripButton.Name == "btnExit") && !(toolStripButton.Name == "btnOther") && !(toolStripButton.Name == "btnParkAreaExtend"))
			{
				Console.WriteLine(str);
				if (DataBuffer2018.SysRoles.FirstOrDefault((SysRole m) => m.RoleClass == str) == null)
				{
					toolStripButton.Enabled = false;
				}
				else
				{
					toolStripButton.Enabled = true;
				}
			}
		}
	}

	private void btnSwitchCam_Click(object sender, EventArgs e)
	{
		try
		{
			IntPtr intPtr = FindWindow(null, "Master視屏監控系統");
			if (!(intPtr == IntPtr.Zero) && intPtr.ToInt32() != 0)
			{
				ShowWindow(intPtr, 3);
				SetForegroundWindow(intPtr);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void FormMainNew_CallBackExtend_Event(CallBallArgs args)
	{
		Logger.Debug("Extend1:" + args.Extend1 + "/Extend2:" + args.Extend2);
		Invoke((Action)delegate
		{
			try
			{
				if (args.Extend1 == "SEARCH")
				{
					SearchPlate searchPlate = (SearchPlate)JsonConvert.DeserializeObject(args.Extend2, typeof(SearchPlate));
					if (!string.IsNullOrWhiteSpace(searchPlate.Licenseplate))
					{
						Clipboard.SetText(searchPlate.Licenseplate);
						DateTime now = DateTime.Now;
						CalcTicketFeeArgsV2 calcTicketFeeArgsV = new CalcTicketFeeArgsV2
						{
							TicketNumber = searchPlate.Licenseplate,
							PayStationName = Settings.Default.OnlyID,
							SerialNumber = "",
							StaffCode = DataBuffer2018.CurrentStaff.StaffCode,
							ISFine = false,
							ChargeTime = now,
							InTime = now,
							BarCode = ""
						};
						if (!string.IsNullOrWhiteSpace(calcTicketFeeArgsV.TicketNumber))
						{
							view_transactionandlp transactionandlp = new view_transactionandlp
							{
								TransactionID = searchPlate.TransactionID
							};
							using FormLPPayFeeCashier formLPPayFeeCashier = new FormLPPayFeeCashier();
							formLPPayFeeCashier.FeeArgs = calcTicketFeeArgsV;
							formLPPayFeeCashier.Transactionandlp = transactionandlp;
							formLPPayFeeCashier.IsImageActivate = true;
							formLPPayFeeCashier.ShowDialog();
							if (formLPPayFeeCashier.DialogResult == DialogResult.OK)
							{
								ClearLPInfo();
								txtLP.Text = "";
								txtLP.Focus();
							}
							return;
						}
						Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
					}
				}
				else
				{
					((IFeeCenterV4)DeviceManager.FeeCenterModule).ResetDevices(args.Extend1);
				}
			}
			catch (Exception message)
			{
				Logger.Error(message);
			}
		}, null);
	}

	public void EQM_Fee(RequestArgs args)
	{
		try
		{
			Common._Carpark2018ServiceContext.CommunicationChannel.ExtendRequestInterface(args);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private static void EndAsync(IAsyncResult ar)
	{
		try
		{
			((Action<int>)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
		}
		catch (Exception)
		{
		}
	}

	private void TickMsg()
	{
		try
		{
			timerPaidMsg.Stop();
			timerPaidMsg.Start();
		}
		catch (Exception)
		{
		}
	}

	private void timerPaidMsg_Tick(object sender, EventArgs e)
	{
		labFinishMsg.Text = "";
		timerPaidMsg.Stop();
	}

	private void btnSetFree_Click(object sender, EventArgs e)
	{
		using FormSetFree formSetFree = new FormSetFree();
		formSetFree.ShowDialog();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.btnLogin = new System.Windows.Forms.ToolStripButton();
		this.btnLanguage = new System.Windows.Forms.ToolStripDropDownButton();
		this.btnCHT = new System.Windows.Forms.ToolStripMenuItem();
		this.btnPT = new System.Windows.Forms.ToolStripMenuItem();
		this.ts1 = new System.Windows.Forms.ToolStripSeparator();
		this.btnShift = new System.Windows.Forms.ToolStripButton();
		this.btnOpenDrawer = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.btnMpassQuery = new System.Windows.Forms.ToolStripButton();
		this.btnMpassCharge = new System.Windows.Forms.ToolStripButton();
		this.btnExit = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.btnParkAreaExtend = new System.Windows.Forms.ToolStripButton();
		this.btnLost = new System.Windows.Forms.ToolStripButton();
		this.btnSetFree = new System.Windows.Forms.ToolStripButton();
		this.btnTransferTicket = new System.Windows.Forms.ToolStripButton();
		this.btnCheckRental = new System.Windows.Forms.ToolStripButton();
		this.btnLPCheck = new System.Windows.Forms.ToolStripButton();
		this.btnOther = new System.Windows.Forms.ToolStripButton();
		this.btnVoidCharge = new System.Windows.Forms.ToolStripButton();
		this.btnSwitchCam = new System.Windows.Forms.ToolStripButton();
		this.btnTimeChargeEx = new System.Windows.Forms.ToolStripButton();
		this.tabCarparkInfo = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.ucParkAreaEX2 = new CarPark2018.UserControls.UCParkAreaEX2();
		this.ucGatesEX2 = new CarPark2018.UserControls.UCGatesEX();
		this.ucPasstraceEX2 = new CarPark2018.UserControls.UCPasstraceEX2();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.labFinishMsg = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.panParkRemain = new System.Windows.Forms.Panel();
		this.labPMRe = new System.Windows.Forms.Label();
		this.labPMotor = new System.Windows.Forms.Label();
		this.labPCRe = new System.Windows.Forms.Label();
		this.labPCar = new System.Windows.Forms.Label();
		this.btnPrv = new System.Windows.Forms.Button();
		this.btnNext = new System.Windows.Forms.Button();
		this.btnSearch = new System.Windows.Forms.Button();
		this.ckStartTime = new System.Windows.Forms.CheckBox();
		this.labText6 = new System.Windows.Forms.Label();
		this.labLP6 = new System.Windows.Forms.Label();
		this.labText4 = new System.Windows.Forms.Label();
		this.labLP4 = new System.Windows.Forms.Label();
		this.labText5 = new System.Windows.Forms.Label();
		this.labLP5 = new System.Windows.Forms.Label();
		this.labText3 = new System.Windows.Forms.Label();
		this.labLP3 = new System.Windows.Forms.Label();
		this.labText2 = new System.Windows.Forms.Label();
		this.labLP2 = new System.Windows.Forms.Label();
		this.labText1 = new System.Windows.Forms.Label();
		this.labLP1 = new System.Windows.Forms.Label();
		this.dpEndTime = new System.Windows.Forms.DateTimePicker();
		this.dpStarttime = new System.Windows.Forms.DateTimePicker();
		this.labEndTime = new System.Windows.Forms.Label();
		this.txtLP = new System.Windows.Forms.TextBox();
		this.labLP = new System.Windows.Forms.Label();
		this.picLP6 = new System.Windows.Forms.PictureBox();
		this.picLP5 = new System.Windows.Forms.PictureBox();
		this.picLP3 = new System.Windows.Forms.PictureBox();
		this.picLP2 = new System.Windows.Forms.PictureBox();
		this.picLP4 = new System.Windows.Forms.PictureBox();
		this.picLP1 = new System.Windows.Forms.PictureBox();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.btnRentalType = new System.Windows.Forms.ToolStripButton();
		this.btnDepositCharge = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.panBottom = new System.Windows.Forms.Panel();
		this.panFill = new System.Windows.Forms.Panel();
		this.timerPaidMsg = new System.Windows.Forms.Timer(this.components);
		this.ucClock1 = new CarPark2018.UserControls.UCClock();
		this.toolStrip1.SuspendLayout();
		this.tabCarparkInfo.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.panParkRemain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLP6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLP5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLP3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLP2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLP4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLP1).BeginInit();
		this.panFill.SuspendLayout();
		base.SuspendLayout();
		this.toolStrip1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.btnLogin, this.btnLanguage, this.ts1, this.btnShift, this.btnOpenDrawer, this.toolStripSeparator1, this.btnMpassQuery, this.btnMpassCharge, this.btnExit, this.toolStripSeparator2,
			this.btnParkAreaExtend, this.btnLost, this.btnSetFree, this.btnTransferTicket, this.btnCheckRental, this.btnLPCheck, this.btnOther
		});
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(1366, 27);
		this.toolStrip1.TabIndex = 2;
		this.toolStrip1.Text = "toolStrip1";
		this.btnLogin.CheckOnClick = true;
		this.btnLogin.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnLogin.Name = "btnLogin";
		this.btnLogin.Size = new System.Drawing.Size(61, 24);
		this.btnLogin.Text = "請登錄";
		this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnLogin.MouseDown += new System.Windows.Forms.MouseEventHandler(btnLogin_MouseDown);
		this.btnLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.btnCHT, this.btnPT });
		this.btnLanguage.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnLanguage.Name = "btnLanguage";
		this.btnLanguage.Size = new System.Drawing.Size(54, 24);
		this.btnLanguage.Text = "語言";
		this.btnLanguage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnCHT.Name = "btnCHT";
		this.btnCHT.Size = new System.Drawing.Size(142, 24);
		this.btnCHT.Tag = "1";
		this.btnCHT.Text = "繁體中文";
		this.btnCHT.Click += new System.EventHandler(btnCHT_Click);
		this.btnPT.Name = "btnPT";
		this.btnPT.Size = new System.Drawing.Size(142, 24);
		this.btnPT.Tag = "2";
		this.btnPT.Text = "English";
		this.btnPT.Click += new System.EventHandler(btnPT_Click);
		this.ts1.Name = "ts1";
		this.ts1.Size = new System.Drawing.Size(6, 27);
		this.btnShift.Enabled = false;
		this.btnShift.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnShift.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnShift.Name = "btnShift";
		this.btnShift.Size = new System.Drawing.Size(77, 24);
		this.btnShift.Text = "轉更結算";
		this.btnShift.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnShift.Click += new System.EventHandler(btnShift_Click);
		this.btnOpenDrawer.Enabled = false;
		this.btnOpenDrawer.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpenDrawer.Name = "btnOpenDrawer";
		this.btnOpenDrawer.Size = new System.Drawing.Size(77, 24);
		this.btnOpenDrawer.Text = "開啟錢箱";
		this.btnOpenDrawer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnOpenDrawer.Click += new System.EventHandler(btnOpenDrawer_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
		this.btnMpassQuery.Enabled = false;
		this.btnMpassQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnMpassQuery.Name = "btnMpassQuery";
		this.btnMpassQuery.Size = new System.Drawing.Size(77, 24);
		this.btnMpassQuery.Text = "餘額查詢";
		this.btnMpassQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnMpassQuery.Click += new System.EventHandler(btnMpassQuery_Click);
		this.btnMpassCharge.Enabled = false;
		this.btnMpassCharge.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnMpassCharge.Name = "btnMpassCharge";
		this.btnMpassCharge.Size = new System.Drawing.Size(45, 24);
		this.btnMpassCharge.Text = "增值";
		this.btnMpassCharge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnMpassCharge.Click += new System.EventHandler(btnMpassCharge_Click);
		this.btnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnExit.Name = "btnExit";
		this.btnExit.Size = new System.Drawing.Size(45, 24);
		this.btnExit.Text = "退出";
		this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnExit.Click += new System.EventHandler(panExit_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
		this.btnParkAreaExtend.Enabled = false;
		this.btnParkAreaExtend.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnParkAreaExtend.Name = "btnParkAreaExtend";
		this.btnParkAreaExtend.Size = new System.Drawing.Size(77, 24);
		this.btnParkAreaExtend.Text = "車位設定";
		this.btnParkAreaExtend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnParkAreaExtend.Click += new System.EventHandler(btnParkAreaExtend_Click);
		this.btnLost.Enabled = false;
		this.btnLost.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnLost.Name = "btnLost";
		this.btnLost.Size = new System.Drawing.Size(77, 24);
		this.btnLost.Text = "失票處理";
		this.btnLost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnLost.Click += new System.EventHandler(btnLost_Click_1);
		this.btnSetFree.Enabled = false;
		this.btnSetFree.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSetFree.Name = "btnSetFree";
		this.btnSetFree.Size = new System.Drawing.Size(77, 24);
		this.btnSetFree.Text = "優惠處理";
		this.btnSetFree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnSetFree.Click += new System.EventHandler(btnSetFree_Click);
		this.btnTransferTicket.Enabled = false;
		this.btnTransferTicket.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnTransferTicket.Name = "btnTransferTicket";
		this.btnTransferTicket.Size = new System.Drawing.Size(61, 24);
		this.btnTransferTicket.Text = "轉紙票";
		this.btnTransferTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnTransferTicket.Click += new System.EventHandler(btnTransferTicket_Click);
		this.btnCheckRental.Enabled = false;
		this.btnCheckRental.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnCheckRental.Name = "btnCheckRental";
		this.btnCheckRental.Size = new System.Drawing.Size(77, 24);
		this.btnCheckRental.Text = "月票查詢";
		this.btnCheckRental.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnCheckRental.Click += new System.EventHandler(btnCheckRental_Click);
		this.btnLPCheck.Enabled = false;
		this.btnLPCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnLPCheck.Name = "btnLPCheck";
		this.btnLPCheck.Size = new System.Drawing.Size(77, 24);
		this.btnLPCheck.Text = "車牌管理";
		this.btnLPCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnLPCheck.Click += new System.EventHandler(btnLPCheck_Click);
		this.btnOther.Enabled = false;
		this.btnOther.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOther.Name = "btnOther";
		this.btnOther.Size = new System.Drawing.Size(45, 24);
		this.btnOther.Text = "更多";
		this.btnOther.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnOther.Click += new System.EventHandler(btnOther_Click);
		this.btnVoidCharge.Enabled = false;
		this.btnVoidCharge.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnVoidCharge.Name = "btnVoidCharge";
		this.btnVoidCharge.Size = new System.Drawing.Size(77, 24);
		this.btnVoidCharge.Text = "優惠審核";
		this.btnVoidCharge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnVoidCharge.Click += new System.EventHandler(btnVoidCharge_Click);
		this.btnSwitchCam.Enabled = false;
		this.btnSwitchCam.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSwitchCam.Name = "btnSwitchCam";
		this.btnSwitchCam.Size = new System.Drawing.Size(77, 24);
		this.btnSwitchCam.Text = "切換視頻";
		this.btnSwitchCam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnSwitchCam.Click += new System.EventHandler(btnSwitchCam_Click);
		this.btnTimeChargeEx.Enabled = false;
		this.btnTimeChargeEx.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnTimeChargeEx.Name = "btnTimeChargeEx";
		this.btnTimeChargeEx.Size = new System.Drawing.Size(77, 24);
		this.btnTimeChargeEx.Text = "收費規則";
		this.btnTimeChargeEx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnTimeChargeEx.Click += new System.EventHandler(btnTimeChargeEx_Click);
		this.tabCarparkInfo.Controls.Add(this.tabPage1);
		this.tabCarparkInfo.Controls.Add(this.tabPage2);
		this.tabCarparkInfo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabCarparkInfo.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.tabCarparkInfo.Location = new System.Drawing.Point(12, 0);
		this.tabCarparkInfo.Multiline = true;
		this.tabCarparkInfo.Name = "tabCarparkInfo";
		this.tabCarparkInfo.SelectedIndex = 0;
		this.tabCarparkInfo.Size = new System.Drawing.Size(1342, 741);
		this.tabCarparkInfo.TabIndex = 0;
		this.tabPage1.Controls.Add(this.ucParkAreaEX2);
		this.tabPage1.Controls.Add(this.ucGatesEX2);
		this.tabPage1.Controls.Add(this.ucPasstraceEX2);
		this.tabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20f);
		this.tabPage1.Location = new System.Drawing.Point(4, 36);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Size = new System.Drawing.Size(1334, 701);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "車場信息";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.ucParkAreaEX2.BackColor = System.Drawing.Color.Transparent;
		this.ucParkAreaEX2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.ucParkAreaEX2.Enabled = false;
		this.ucParkAreaEX2.Location = new System.Drawing.Point(12, 6);
		this.ucParkAreaEX2.Name = "ucParkAreaEX2";
		this.ucParkAreaEX2.Size = new System.Drawing.Size(361, 322);
		this.ucParkAreaEX2.TabIndex = 0;
		this.ucGatesEX2.BackColor = System.Drawing.Color.Transparent;
		this.ucGatesEX2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.ucGatesEX2.Enabled = false;
		this.ucGatesEX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucGatesEX2.Location = new System.Drawing.Point(379, 6);
		this.ucGatesEX2.Name = "ucGatesEX2";
		this.ucGatesEX2.Size = new System.Drawing.Size(609, 322);
		this.ucGatesEX2.TabIndex = 1;
		this.ucPasstraceEX2.BackColor = System.Drawing.Color.Transparent;
		this.ucPasstraceEX2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.ucPasstraceEX2.Location = new System.Drawing.Point(0, 340);
		this.ucPasstraceEX2.Name = "ucPasstraceEX2";
		this.ucPasstraceEX2.Size = new System.Drawing.Size(999, 277);
		this.ucPasstraceEX2.TabIndex = 2;
		this.txtDisplay = new System.Windows.Forms.TextBox();
		this.txtDisplay.Multiline = true;
		this.txtDisplay.Location = new System.Drawing.Point(1000, 6);
		this.txtDisplay.Size = new System.Drawing.Size(320, 610);
		this.txtDisplay.ReadOnly = true;
		this.txtDisplay.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.tabPage1.Controls.Add(this.txtDisplay);
		this.tabPage2.Controls.Add(this.labFinishMsg);
		this.tabPage2.Controls.Add(this.label6);
		this.tabPage2.Controls.Add(this.label5);
		this.tabPage2.Controls.Add(this.label4);
		this.tabPage2.Controls.Add(this.label3);
		this.tabPage2.Controls.Add(this.label2);
		this.tabPage2.Controls.Add(this.label1);
		this.tabPage2.Controls.Add(this.panParkRemain);
		this.tabPage2.Controls.Add(this.btnPrv);
		this.tabPage2.Controls.Add(this.btnNext);
		this.tabPage2.Controls.Add(this.btnSearch);
		this.tabPage2.Controls.Add(this.ckStartTime);
		this.tabPage2.Controls.Add(this.labText6);
		this.tabPage2.Controls.Add(this.labLP6);
		this.tabPage2.Controls.Add(this.labText4);
		this.tabPage2.Controls.Add(this.labLP4);
		this.tabPage2.Controls.Add(this.labText5);
		this.tabPage2.Controls.Add(this.labLP5);
		this.tabPage2.Controls.Add(this.labText3);
		this.tabPage2.Controls.Add(this.labLP3);
		this.tabPage2.Controls.Add(this.labText2);
		this.tabPage2.Controls.Add(this.labLP2);
		this.tabPage2.Controls.Add(this.labText1);
		this.tabPage2.Controls.Add(this.labLP1);
		this.tabPage2.Controls.Add(this.dpEndTime);
		this.tabPage2.Controls.Add(this.dpStarttime);
		this.tabPage2.Controls.Add(this.labEndTime);
		this.tabPage2.Controls.Add(this.txtLP);
		this.tabPage2.Controls.Add(this.labLP);
		this.tabPage2.Controls.Add(this.picLP6);
		this.tabPage2.Controls.Add(this.picLP5);
		this.tabPage2.Controls.Add(this.picLP3);
		this.tabPage2.Controls.Add(this.picLP2);
		this.tabPage2.Controls.Add(this.picLP4);
		this.tabPage2.Controls.Add(this.picLP1);
		this.tabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20f);
		this.tabPage2.Location = new System.Drawing.Point(4, 36);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Size = new System.Drawing.Size(1334, 701);
		this.tabPage2.TabIndex = 0;
		this.tabPage2.Text = "收费管理";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.labFinishMsg.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.labFinishMsg.Location = new System.Drawing.Point(666, 68);
		this.labFinishMsg.Name = "labFinishMsg";
		this.labFinishMsg.Size = new System.Drawing.Size(254, 39);
		this.labFinishMsg.TabIndex = 37;
		this.labFinishMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(771, 394);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(29, 31);
		this.label6.TabIndex = 36;
		this.label6.Text = "6";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(398, 394);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 31);
		this.label5.TabIndex = 35;
		this.label5.Text = "5";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(27, 394);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(29, 31);
		this.label4.TabIndex = 34;
		this.label4.Text = "4";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(771, 119);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(29, 31);
		this.label3.TabIndex = 33;
		this.label3.Text = "3";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(398, 119);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(29, 31);
		this.label2.TabIndex = 32;
		this.label2.Text = "2";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(27, 119);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(29, 31);
		this.label1.TabIndex = 31;
		this.label1.Text = "1";
		this.panParkRemain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panParkRemain.Controls.Add(this.labPMRe);
		this.panParkRemain.Controls.Add(this.labPMotor);
		this.panParkRemain.Controls.Add(this.labPCRe);
		this.panParkRemain.Controls.Add(this.labPCar);
		this.panParkRemain.Location = new System.Drawing.Point(1131, 394);
		this.panParkRemain.Name = "panParkRemain";
		this.panParkRemain.Size = new System.Drawing.Size(191, 230);
		this.panParkRemain.TabIndex = 29;
		this.labPMRe.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labPMRe.Location = new System.Drawing.Point(0, 171);
		this.labPMRe.Name = "labPMRe";
		this.labPMRe.Size = new System.Drawing.Size(191, 57);
		this.labPMRe.TabIndex = 3;
		this.labPMRe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labPMotor.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labPMotor.Location = new System.Drawing.Point(0, 114);
		this.labPMotor.Name = "labPMotor";
		this.labPMotor.Size = new System.Drawing.Size(191, 57);
		this.labPMotor.TabIndex = 2;
		this.labPMotor.Text = "電單車";
		this.labPMotor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labPCRe.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labPCRe.Location = new System.Drawing.Point(0, 57);
		this.labPCRe.Name = "labPCRe";
		this.labPCRe.Size = new System.Drawing.Size(191, 57);
		this.labPCRe.TabIndex = 1;
		this.labPCRe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labPCar.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labPCar.Location = new System.Drawing.Point(0, 0);
		this.labPCar.Name = "labPCar";
		this.labPCar.Size = new System.Drawing.Size(191, 57);
		this.labPCar.TabIndex = 0;
		this.labPCar.Text = "私家車";
		this.labPCar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnPrv.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.btnPrv.Location = new System.Drawing.Point(1151, 210);
		this.btnPrv.Name = "btnPrv";
		this.btnPrv.Size = new System.Drawing.Size(150, 50);
		this.btnPrv.TabIndex = 28;
		this.btnPrv.Text = "上一頁";
		this.btnPrv.UseVisualStyleBackColor = true;
		this.btnPrv.Click += new System.EventHandler(btnPrv_Click);
		this.btnNext.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.btnNext.Location = new System.Drawing.Point(1151, 119);
		this.btnNext.Name = "btnNext";
		this.btnNext.Size = new System.Drawing.Size(150, 50);
		this.btnNext.TabIndex = 27;
		this.btnNext.Text = "下一頁";
		this.btnNext.UseVisualStyleBackColor = true;
		this.btnNext.Click += new System.EventHandler(btnNext_Click);
		this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.btnSearch.Location = new System.Drawing.Point(961, 19);
		this.btnSearch.Name = "btnSearch";
		this.btnSearch.Size = new System.Drawing.Size(150, 50);
		this.btnSearch.TabIndex = 26;
		this.btnSearch.Text = "查詢";
		this.btnSearch.UseVisualStyleBackColor = true;
		this.btnSearch.Click += new System.EventHandler(btnSearch_Click);
		this.ckStartTime.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.ckStartTime.Location = new System.Drawing.Point(35, 30);
		this.ckStartTime.Name = "ckStartTime";
		this.ckStartTime.Size = new System.Drawing.Size(158, 31);
		this.ckStartTime.TabIndex = 25;
		this.ckStartTime.Text = "開始時間";
		this.ckStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.ckStartTime.UseVisualStyleBackColor = true;
		this.ckStartTime.CheckedChanged += new System.EventHandler(ckStartTime_CheckedChanged);
		this.labText6.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labText6.ForeColor = System.Drawing.Color.Black;
		this.labText6.Location = new System.Drawing.Point(894, 627);
		this.labText6.Name = "labText6";
		this.labText6.Size = new System.Drawing.Size(209, 35);
		this.labText6.TabIndex = 24;
		this.labText6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labLP6.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labLP6.ForeColor = System.Drawing.Color.Red;
		this.labLP6.Location = new System.Drawing.Point(766, 627);
		this.labLP6.Name = "labLP6";
		this.labLP6.Size = new System.Drawing.Size(122, 35);
		this.labLP6.TabIndex = 23;
		this.labLP6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labText4.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labText4.ForeColor = System.Drawing.Color.Black;
		this.labText4.Location = new System.Drawing.Point(158, 627);
		this.labText4.Name = "labText4";
		this.labText4.Size = new System.Drawing.Size(209, 35);
		this.labText4.TabIndex = 22;
		this.labText4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labLP4.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labLP4.ForeColor = System.Drawing.Color.Red;
		this.labLP4.Location = new System.Drawing.Point(30, 627);
		this.labLP4.Name = "labLP4";
		this.labLP4.Size = new System.Drawing.Size(122, 35);
		this.labLP4.TabIndex = 21;
		this.labLP4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labText5.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labText5.ForeColor = System.Drawing.Color.Black;
		this.labText5.Location = new System.Drawing.Point(521, 627);
		this.labText5.Name = "labText5";
		this.labText5.Size = new System.Drawing.Size(209, 35);
		this.labText5.TabIndex = 20;
		this.labText5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labLP5.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labLP5.ForeColor = System.Drawing.Color.Red;
		this.labLP5.Location = new System.Drawing.Point(393, 627);
		this.labLP5.Name = "labLP5";
		this.labLP5.Size = new System.Drawing.Size(122, 35);
		this.labLP5.TabIndex = 19;
		this.labLP5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labText3.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labText3.ForeColor = System.Drawing.Color.Black;
		this.labText3.Location = new System.Drawing.Point(894, 354);
		this.labText3.Name = "labText3";
		this.labText3.Size = new System.Drawing.Size(209, 35);
		this.labText3.TabIndex = 18;
		this.labText3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labLP3.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labLP3.ForeColor = System.Drawing.Color.Red;
		this.labLP3.Location = new System.Drawing.Point(766, 354);
		this.labLP3.Name = "labLP3";
		this.labLP3.Size = new System.Drawing.Size(122, 35);
		this.labLP3.TabIndex = 17;
		this.labLP3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labText2.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labText2.ForeColor = System.Drawing.Color.Black;
		this.labText2.Location = new System.Drawing.Point(521, 354);
		this.labText2.Name = "labText2";
		this.labText2.Size = new System.Drawing.Size(209, 35);
		this.labText2.TabIndex = 16;
		this.labText2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labLP2.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labLP2.ForeColor = System.Drawing.Color.Red;
		this.labLP2.Location = new System.Drawing.Point(393, 354);
		this.labLP2.Name = "labLP2";
		this.labLP2.Size = new System.Drawing.Size(122, 35);
		this.labLP2.TabIndex = 15;
		this.labLP2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labText1.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labText1.ForeColor = System.Drawing.Color.Black;
		this.labText1.Location = new System.Drawing.Point(158, 354);
		this.labText1.Name = "labText1";
		this.labText1.Size = new System.Drawing.Size(209, 35);
		this.labText1.TabIndex = 14;
		this.labText1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labLP1.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labLP1.ForeColor = System.Drawing.Color.Red;
		this.labLP1.Location = new System.Drawing.Point(30, 354);
		this.labLP1.Name = "labLP1";
		this.labLP1.Size = new System.Drawing.Size(122, 35);
		this.labLP1.TabIndex = 13;
		this.labLP1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dpEndTime.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dpEndTime.Enabled = false;
		this.dpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dpEndTime.Location = new System.Drawing.Point(199, 70);
		this.dpEndTime.Name = "dpEndTime";
		this.dpEndTime.Size = new System.Drawing.Size(255, 38);
		this.dpEndTime.TabIndex = 12;
		this.dpStarttime.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dpStarttime.Enabled = false;
		this.dpStarttime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dpStarttime.Location = new System.Drawing.Point(199, 26);
		this.dpStarttime.Name = "dpStarttime";
		this.dpStarttime.Size = new System.Drawing.Size(255, 38);
		this.dpStarttime.TabIndex = 11;
		this.labEndTime.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.labEndTime.Location = new System.Drawing.Point(50, 70);
		this.labEndTime.Name = "labEndTime";
		this.labEndTime.Size = new System.Drawing.Size(140, 31);
		this.labEndTime.TabIndex = 10;
		this.labEndTime.Text = "結束時間";
		this.labEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtLP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtLP.Location = new System.Drawing.Point(657, 25);
		this.txtLP.MaxLength = 7;
		this.txtLP.Name = "txtLP";
		this.txtLP.Size = new System.Drawing.Size(255, 38);
		this.txtLP.TabIndex = 7;
		this.txtLP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtLP_KeyPress);
		this.labLP.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.labLP.Location = new System.Drawing.Point(489, 29);
		this.labLP.Name = "labLP";
		this.labLP.Size = new System.Drawing.Size(156, 31);
		this.labLP.TabIndex = 6;
		this.labLP.Text = "車牌";
		this.labLP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.picLP6.BackColor = System.Drawing.Color.Black;
		this.picLP6.Location = new System.Drawing.Point(771, 394);
		this.picLP6.Name = "picLP6";
		this.picLP6.Size = new System.Drawing.Size(340, 230);
		this.picLP6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picLP6.TabIndex = 5;
		this.picLP6.TabStop = false;
		this.picLP6.Click += new System.EventHandler(picLP1_Click);
		this.picLP5.BackColor = System.Drawing.Color.Black;
		this.picLP5.Location = new System.Drawing.Point(398, 394);
		this.picLP5.Name = "picLP5";
		this.picLP5.Size = new System.Drawing.Size(340, 230);
		this.picLP5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picLP5.TabIndex = 4;
		this.picLP5.TabStop = false;
		this.picLP5.Click += new System.EventHandler(picLP1_Click);
		this.picLP3.BackColor = System.Drawing.Color.Black;
		this.picLP3.Location = new System.Drawing.Point(771, 119);
		this.picLP3.Name = "picLP3";
		this.picLP3.Size = new System.Drawing.Size(340, 230);
		this.picLP3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picLP3.TabIndex = 2;
		this.picLP3.TabStop = false;
		this.picLP3.Click += new System.EventHandler(picLP1_Click);
		this.picLP2.BackColor = System.Drawing.Color.Black;
		this.picLP2.Location = new System.Drawing.Point(398, 119);
		this.picLP2.Name = "picLP2";
		this.picLP2.Size = new System.Drawing.Size(340, 230);
		this.picLP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picLP2.TabIndex = 1;
		this.picLP2.TabStop = false;
		this.picLP2.Click += new System.EventHandler(picLP1_Click);
		this.picLP4.BackColor = System.Drawing.Color.Black;
		this.picLP4.Location = new System.Drawing.Point(27, 394);
		this.picLP4.Name = "picLP4";
		this.picLP4.Size = new System.Drawing.Size(340, 230);
		this.picLP4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picLP4.TabIndex = 3;
		this.picLP4.TabStop = false;
		this.picLP4.Click += new System.EventHandler(picLP1_Click);
		this.picLP1.BackColor = System.Drawing.Color.Black;
		this.picLP1.Location = new System.Drawing.Point(27, 119);
		this.picLP1.Name = "picLP1";
		this.picLP1.Size = new System.Drawing.Size(340, 230);
		this.picLP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picLP1.TabIndex = 0;
		this.picLP1.TabStop = false;
		this.picLP1.Click += new System.EventHandler(picLP1_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
		this.btnRentalType.Enabled = false;
		this.btnRentalType.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnRentalType.Name = "btnRentalType";
		this.btnRentalType.Size = new System.Drawing.Size(77, 24);
		this.btnRentalType.Text = "月租續費";
		this.btnRentalType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnRentalType.Click += new System.EventHandler(btnRentalType_Click);
		this.btnDepositCharge.Enabled = false;
		this.btnDepositCharge.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnDepositCharge.Name = "btnDepositCharge";
		this.btnDepositCharge.Size = new System.Drawing.Size(77, 24);
		this.btnDepositCharge.Text = "月租按金";
		this.btnDepositCharge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.btnDepositCharge.Click += new System.EventHandler(btnDepositCharge_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
		this.panBottom.BackColor = System.Drawing.Color.Transparent;
		this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panBottom.Location = new System.Drawing.Point(0, 668);
		this.panBottom.Name = "panBottom";
		this.panBottom.Size = new System.Drawing.Size(1366, 100);
		this.panBottom.TabIndex = 3;
		this.panFill.Controls.Add(this.tabCarparkInfo);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Enabled = false;
		this.panFill.Location = new System.Drawing.Point(0, 27);
		this.panFill.Name = "panFill";
		this.panFill.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
		this.panFill.Size = new System.Drawing.Size(1366, 741);
		this.panFill.TabIndex = 4;
		this.timerPaidMsg.Interval = 5000;
		this.timerPaidMsg.Tick += new System.EventHandler(timerPaidMsg_Tick);
		this.ucClock1.ClockColor = System.Drawing.Color.Black;
		this.ucClock1.DateTime = new System.DateTime(2019, 3, 28, 15, 36, 30, 812);
		this.ucClock1.Font = new System.Drawing.Font("新細明體", 9f);
		this.ucClock1.IsDrawShadow = true;
		this.ucClock1.IsTimerEnable = false;
		this.ucClock1.Location = new System.Drawing.Point(1000, 0);
		this.ucClock1.Name = "ucClock1";
		this.ucClock1.SevenSegmentClockStyle = CarPark2018.UserControls.SevenSegmentClockStyle.DateAndTime;
		this.ucClock1.Size = new System.Drawing.Size(318, 48);
		this.ucClock1.TabIndex = 5;
		this.ucClock1.Timer = null;
		this.ucClock1.Click += new System.EventHandler(ucClock1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1366, 768);
		base.Controls.Add(this.ucClock1);
		base.Controls.Add(this.panFill);
		base.Controls.Add(this.toolStrip1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.KeyPreview = true;
		base.Name = "FormMain";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "CarPark2018";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMain_FormClosing);
		base.Load += new System.EventHandler(FormMain_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMain_KeyDown);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		this.tabCarparkInfo.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.panParkRemain.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.picLP6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLP5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLP3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLP2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLP4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLP1).EndInit();
		this.panFill.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void ReadTextFile()
	{
		string path = Path.Combine(Application.StartupPath, "notice.txt");
		if (File.Exists(path))
		{
			try
			{
				string text = File.ReadAllText(path);
				if (!isFirstLoad)
				{
					if (string.IsNullOrWhiteSpace(lastContent) && !string.IsNullOrWhiteSpace(text))
					{
						MessageBox.Show("通告已更新，請留意！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else if (!string.IsNullOrWhiteSpace(lastContent) && !text.Equals(lastContent))
					{
						MessageBox.Show("通告已更新，請留意！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				txtDisplay.Text = text;
				txtDisplay.Visible = !string.IsNullOrWhiteSpace(text);
				lastContent = text;
				isFirstLoad = false;
				return;
			}
			catch (Exception message)
			{
				Logger.Error(message);
				txtDisplay.Visible = false;
				return;
			}
		}
		txtDisplay.Visible = false;
	}

	private void ucClock1_Click(object sender, EventArgs e)
	{
		ReadTextFile();
	}

	private void FormMain_Activated(object sender, EventArgs e)
	{
		ReadTextFile();
	}
}
