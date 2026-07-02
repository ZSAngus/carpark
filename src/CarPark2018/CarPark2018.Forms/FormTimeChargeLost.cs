using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.DB.AdditionalDataSource;
using CarPark.Device;
using CarPark.Lib;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.Forms;

public class FormTimeChargeLost : Form
{
	private readonly DateTime initTime;

	private ILog Logger;

	private ChargeRecord m_ChargeRecord;

	private CreateLostArgs lostArg = new CreateLostArgs();

	private CalcTicketFeeArgs feeArg = new CalcTicketFeeArgs();

	private SaveChargeRecordArgs saveArg = new SaveChargeRecordArgs();

	private string TicketNumber = "";

	private EnumParkType parkType = EnumParkType.None;

	private DateTime inTime;

	private bool Fine = false;

	private string FreeImagePath = null;

	public string Remark = "";

	private bool Syn = false;

	private view_transactionandlp view;

	private ChargeContext chargeContext = new ChargeContext();

	private IContainer components = null;

	private Label labTitle;

	private Panel panel1;

	private Panel panel2;

	private Button btnFree;

	private TextBox txtFree;

	private Label labFreeTime;

	private ComboBox comboParkType;

	private Label labTimeSplit;

	private TextBox txtParkMin;

	private TextBox txtTotalCharge;

	private TextBox txtParkHour;

	private TextBox txtChargeTime;

	private DateTimePicker dtDate;

	private DateTimePicker dtTime;

	private Label labTotalCharge;

	private Label labParkType;

	private Label labParkTime;

	private Label labChargeTime;

	private Button btnFine;

	private Button btnOK;

	private Button btnCancel;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem btnCancelFree;

	private Panel panFill;

	private ComboBox comboParkArea;

	private Label labParkArea;

	private Button btnOther;

	private TextBox txtQRCode;

	private Label labQRCode;

	private Button btnCheck;

	private CheckBox labInTime;

	public FormTimeChargeLost()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		initTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
		InitializeComponent();
		lostArg.PayStationName = Settings.Default.OnlyID;
		lostArg.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
		LoadImage();
	}

	public void LoadImage()
	{
		try
		{
			btnCancel.Image = ImageManager.GetImage("", "cancel");
			btnOK.Image = ImageManager.GetImage("", "ok");
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labChargeTime.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labChargeTime");
		labFreeTime.Text = LangManager.GetLangString("CarPark.Forms.FormTimeCharge.labelX1");
		labInTime.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labInTime");
		labParkTime.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labParkTime");
		labParkType.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labParkType");
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labTitle");
		labTotalCharge.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labTotalCharge");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.btnClose");
		btnCancelFree.Text = LangManager.GetLangString("CarPark.Forms.FormTimeCharge.cancelFree");
		btnFine.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.btnFine");
		btnFree.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.btnFree");
		btnOK.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.btnOK");
		labQRCode.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labQRCode");
		labParkArea.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labParkArea");
		btnCheck.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.btnCheck");
		btnOther.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.btnOther");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		try
		{
			DeviceManager.FeeCenterModule.EjectTicket();
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void btnFine_Click(object sender, EventArgs e)
	{
		Fine = !Fine;
		feeArg.ISFine = Fine;
		txtTotalCharge.ForeColor = (Fine ? Color.Red : Color.Black);
		CalcAmount();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		m_ChargeRecord.BillType = 3;
		m_ChargeRecord.CardCode = TicketNumber;
		m_ChargeRecord.PayType = 0;
		SaveChargeRecord(m_ChargeRecord, null, null, null, null);
	}

	private void CalcAmount()
	{
		try
		{
			feeArg.ChargeTime = initTime;
			feeArg.InTime = inTime;
			feeArg.PayStationName = Settings.Default.OnlyID;
			feeArg.SerialNumber = "";
			feeArg.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			feeArg.TicketNumber = TicketNumber;
			feeArg.AreaID = (int)comboParkArea.SelectedValue;
			feeArg.BarCode = txtQRCode.Text;
			parkType = (EnumParkType)comboParkType.SelectedValue;
			saveArg.CustomFreeID = feeArg.CustomFreeID;
			saveArg.CustomFreeTenatID = feeArg.CustomFreeTenatID;
			saveArg.InTime = feeArg.InTime;
			saveArg.TicketNumber = feeArg.TicketNumber;
			saveArg.FreeImagePath = FreeImagePath;
			saveArg.TransactionDataRemark = comboParkArea.SelectedValue.ToString();
			saveArg.BarCode = txtQRCode.Text;
			CalcTicketFeeReturn calcTicketFeeReturn = chargeContext.CommunicationChannel.CalcTicketFee(feeArg, parkType, feeArg.ISFine ? EnumBillType.TimeChargeLostFine : EnumBillType.TimeChargeLost, out m_ChargeRecord);
			chargeContext.CommunicationChannel.Disconnect();
			if (calcTicketFeeReturn.ISValid)
			{
				txtChargeTime.Text = initTime.ToString(SystemParm.LongTimeFormat);
				txtParkHour.Text = (m_ChargeRecord.ParkMin / 60).ToString();
				txtParkMin.Text = (m_ChargeRecord.ParkMin % 60).ToString();
				txtFree.Text = m_ChargeRecord.FreeMin.ToString();
				comboParkType.SelectedValue = m_ChargeRecord.ParkTypeID;
				txtTotalCharge.Text = m_ChargeRecord.TotalCharge.ToString("f2");
			}
			else
			{
				if (calcTicketFeeReturn.ErrCode == "Coupon_Invalid")
				{
					txtQRCode.Text = "";
					CalcAmount();
				}
				Logger.Debug(calcTicketFeeReturn.ErrCode);
				Global.ShowMessage(calcTicketFeeReturn.ErrCode);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
			btnCancel_Click(null, null);
		}
		catch (Exception)
		{
			btnCancel_Click(null, null);
			throw;
		}
	}

	private void dateTimeInput2_ValueChanged(object sender, EventArgs e)
	{
		try
		{
			if (comboParkType.SelectedIndex >= 0)
			{
				CalcAmount();
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
			btnCancel_Click(null, null);
		}
	}

	private void dateTimeInput1_ValueChanged(object sender, EventArgs e)
	{
		DateTime dateTime = (inTime = new DateTime(dtDate.Value.Year, dtDate.Value.Month, dtDate.Value.Day, dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second));
		if (dateTime > initTime)
		{
			try
			{
				dtDate.Value = initTime;
				dtTime.Value = initTime;
				inTime = initTime;
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}
		try
		{
			if (comboParkType.SelectedIndex >= 0)
			{
				CalcAmount();
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
			btnCancel_Click(null, null);
		}
		catch (Exception message)
		{
			Logger.Error(message);
			try
			{
				DeviceManager.FeeCenterModule.EjectTicket();
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2);
				Console.WriteLine(ex2.Message);
			}
		}
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		try
		{
			DeviceManager.FeeCenterModule.DisplayFee("READY.");
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
		base.OnClosing(e);
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		dtDate.MaxDate = initTime;
		dateTimeInput1_ValueChanged(null, null);
	}

	private void txtTotalCharge_TextChanged(object sender, EventArgs e)
	{
		try
		{
			DeviceManager.FeeCenterModule.DisplayFee(txtTotalCharge.Text);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
	}

	private void btnFree_Click(object sender, EventArgs e)
	{
		try
		{
			using FormTimeChargeFree formTimeChargeFree = new FormTimeChargeFree();
			if (formTimeChargeFree.ShowDialog() == DialogResult.OK)
			{
				feeArg.CustomFreeID = formTimeChargeFree.m_CustomFreeType.CustomFreeTypeID;
				feeArg.CustomFreeTenatID = formTimeChargeFree.m_CustomFreeTenat.TenatID;
				FreeImagePath = formTimeChargeFree.FreeImagePath;
				Remark = formTimeChargeFree.Remark;
				txtQRCode.Text = "";
				CalcAmount();
				btnOK.Focus();
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnCancelFree_Click(object sender, EventArgs e)
	{
		feeArg.CustomFreeID = 0;
		feeArg.CustomFreeTenatID = 0;
		FreeImagePath = null;
		Remark = "";
		txtQRCode.Text = "";
		CalcAmount();
	}

	private void FormTimeChargeLost_Load(object sender, EventArgs e)
	{
		try
		{
			BindingHelper.BindComboBox<EnumParkTypeSource>(comboParkType);
			comboParkArea.DataSource = DataBuffer2018.AllParkAreas;
			comboParkArea.DisplayMember = "AreaName";
			comboParkArea.ValueMember = "AreaID";
			comboParkArea.SelectedValue = Convert.ToInt32(Config.AreaCode);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
		try
		{
			CreateLostReturn createLostReturn = chargeContext.CommunicationChannel.CreateLost(lostArg);
			chargeContext.CommunicationChannel.Disconnect();
			TicketNumber = createLostReturn.TicketNumber;
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Console.WriteLine(ex2.Message);
		}
		dtDate.Value = initTime;
		dtTime.Value = initTime;
		dtDate.ValueChanged += dateTimeInput1_ValueChanged;
		dtTime.ValueChanged += dateTimeInput1_ValueChanged;
		comboParkType.SelectedIndexChanged += dateTimeInput2_ValueChanged;
		comboParkArea.SelectedIndexChanged += dateTimeInput2_ValueChanged;
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent += FeeCenterModule_TicketScanEvent;
		}
	}

	private FeeInfo FeeCenterModule_TicketScanEvent(TicketInfo ticketInfo)
	{
		FeeInfo feeInfo = new FeeInfo();
		if (ticketInfo.IsEmptyOrInValid)
		{
			feeInfo.TicketAction = EnumTicketAction.Keep;
			Invoke((Action)delegate
			{
				btnOK.Enabled = true;
				btnOther.Enabled = true;
				btnOK.Focus();
			});
			return feeInfo;
		}
		Global.ShowMessage(LangManager.GetLangString("Alert.Not_Empty_Ticket"));
		feeInfo.TicketAction = EnumTicketAction.Reject;
		Invoke((Action)delegate
		{
			btnOK.Enabled = false;
			btnOther.Enabled = false;
		});
		return feeInfo;
	}

	private void FormTimeChargeLost_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent -= FeeCenterModule_TicketScanEvent;
		}
	}

	private void btnOther_Click(object sender, EventArgs e)
	{
		FormEpaySale formEpaySale = new FormEpaySale
		{
			ChargeRecord = m_ChargeRecord
		};
		using FormEpaySale formEpaySale2 = formEpaySale;
		if (formEpaySale2.ShowDialog() == DialogResult.OK)
		{
			formEpaySale2.ChargeRecord.BillType = 3;
			formEpaySale2.ChargeRecord.CardCode = TicketNumber;
			formEpaySale2.ChargeRecord.PayType = (int)formEpaySale2.PayTypeFlag;
			SaveChargeRecord(m_ChargeRecord, formEpaySale2.MPass, formEpaySale2.BOC, formEpaySale2.BOC_N910, formEpaySale2.SPay);
		}
	}

	private void SaveChargeRecord(ChargeRecord charge, MPass_POS_Transaction_Detail MPass, BOC_Gate_TransactionExtend boc, BOC_N910_POS_Card_Payment_DetailEX bocn910, ScanPayment scanPayment)
	{
		try
		{
			SaveChargeRecordReturn saveChargeRecordReturn = null;
			ChargeRecord chargeRecord = null;
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = null;
			BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend = null;
			BOC_N910_POS_Card_Payment_DetailEX bOC_N910_POS_Card_Payment_DetailEX = null;
			ScanPayment scanPayment2 = null;
			try
			{
				if (MPass != null)
				{
					if (MPass.PAY_MODE == "mpay")
					{
						charge.subPayType = 2;
					}
					else
					{
						charge.subPayType = 1;
					}
				}
				else if (bocn910 != null)
				{
					charge.subPayType = 8;
				}
				else if (scanPayment != null)
				{
					charge.subPayType = Convert.ToInt32(scanPayment.PayType);
				}
			}
			catch (Exception arg)
			{
				Logger.Error($"[subPayType]{arg}");
			}
			try
			{
				if (labInTime.Checked)
				{
					saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveArg, parkType, charge, MPass, boc, bocn910, scanPayment);
					chargeContext.CommunicationChannel.Disconnect();
				}
				else
				{
					saveArg.TransactionID = view.TransactionID;
					saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveArg, parkType, charge, MPass, boc, bocn910, scanPayment);
					chargeContext.CommunicationChannel.Disconnect();
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				if (MPass != null || bocn910 != null || scanPayment != null)
				{
					DBHelper.Insert(charge.CardCode, charge, MPass, boc, saveArg, bocn910, scanPayment);
					chargeRecord = DBHelper.SelectChargeRecord(charge.CardCode);
					if (MPass != null)
					{
						mPass_POS_Transaction_Detail = DBHelper.SelectMPass_POS_Transaction_Detail(charge.TimeChargeID);
					}
					else if (bocn910 != null)
					{
						bOC_N910_POS_Card_Payment_DetailEX = DBHelper.SelectBOC_N910_POS_Card_Payment_DetailEX(charge.TimeChargeID);
					}
					else if (scanPayment != null)
					{
						scanPayment2 = DBHelper.SelectScan_Payment_DetailEX(charge.TimeChargeID);
					}
					saveChargeRecordReturn = new SaveChargeRecordReturn();
					saveChargeRecordReturn.ISOK = true;
					Syn = true;
				}
				else
				{
					Logger.Error(ex);
					btnCancel_Click(null, null);
					Global.ShowMessage(ex.Message);
				}
			}
			if (saveChargeRecordReturn.ISOK)
			{
				if (m_ChargeRecord.ParkTypeID == 5 || m_ChargeRecord.ParkTypeID == 4)
				{
					m_ChargeRecord.ParkTypeID = 1;
				}
				FeeInfo feeInfo = new FeeInfo
				{
					InTime = feeArg.InTime,
					ParkType = (EnumParkType)m_ChargeRecord.ParkTypeID,
					TicketNumber = m_ChargeRecord.CardCode,
					TicketAction = EnumTicketAction.Normal
				};
				FeeInfo feeInfo2 = feeInfo;
				feeInfo2.Fee = m_ChargeRecord.TotalCharge;
				feeInfo2.FeeTime = m_ChargeRecord.ChargeTime;
				feeInfo2.NeedPrint = true;
				feeInfo2.TicketAction = EnumTicketAction.Normal;
				feeInfo2.TicketType = EnumTicketType.Lost;
				feeInfo2.FieldStr = comboParkArea.Text;
				feeInfo2.CarParkSerialNo = DataBuffer2018.GetCarParkSerial((EnumParkType)m_ChargeRecord.ParkTypeID, (int)comboParkArea.SelectedValue);
				try
				{
					DeviceManager.FeeCenterModule.MakeTicket(feeInfo2);
				}
				catch (OperationCanceledException)
				{
					Global.ShowMessage(LangManager.GetLangString("Alert.TicketFail"));
					DeviceManager.FeeCenterModule.EjectTicket();
					return;
				}
				if (m_ChargeRecord.TotalCharge != 0m && mPass_POS_Transaction_Detail == null && bOC_Gate_TransactionExtend == null)
				{
					try
					{
						DeviceManager.FeeCenterModule.OpenCash();
					}
					catch (Exception message)
					{
						Logger.Error(message);
					}
				}
				if (Syn)
				{
					Global.ShowMessage("有一條數據沒有同步，現在馬上同步");
					try
					{
						if (mPass_POS_Transaction_Detail != null)
						{
							saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveArg, parkType, chargeRecord, mPass_POS_Transaction_Detail, null);
							chargeContext.CommunicationChannel.Disconnect();
						}
						else
						{
							saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveArg, parkType, chargeRecord, null, bOC_Gate_TransactionExtend, bOC_N910_POS_Card_Payment_DetailEX, scanPayment2);
							chargeContext.CommunicationChannel.Disconnect();
						}
						if (!saveChargeRecordReturn.ISOK)
						{
							Global.ShowMessage("同步失敗，請聯繫技術人員");
						}
						else
						{
							DBHelper.ExecuteNonQuery($"update ChargeRecord set isupload='1' where timechargeid={chargeRecord.TimeChargeID}", CommandType.Text, (IDbDataParameter[])null);
						}
					}
					catch (Exception)
					{
						Global.ShowMessage("同步失敗，請聯繫技術人員");
					}
				}
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				Global.ShowMessage(saveChargeRecordReturn.ErrCode);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
			btnCancel_Click(null, null);
		}
		catch (Exception message2)
		{
			Logger.Error(message2);
			btnCancel_Click(null, null);
		}
	}

	private void FormTimeChargeLost_QRCodeScanEvent(string code)
	{
		try
		{
			Invoke((MethodInvoker)delegate
			{
				if (!(txtQRCode.Text == code))
				{
					feeArg.CustomFreeID = 0;
					feeArg.CustomFreeTenatID = 0;
					FreeImagePath = null;
					Remark = "";
					Logger.Debug(code);
					txtQRCode.Text = code;
					CalcAmount();
				}
			});
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void btnCheck_Click(object sender, EventArgs e)
	{
		using FormLostLP formLostLP = new FormLostLP();
		if (formLostLP.ShowDialog() == DialogResult.OK)
		{
			view = formLostLP.view;
			dtDate.Value = formLostLP.view.InTime;
			dtTime.Value = formLostLP.view.InTime;
		}
	}

	private void labInTime_CheckedChanged(object sender, EventArgs e)
	{
		if (labInTime.Checked)
		{
			dtDate.Enabled = true;
			dtTime.Enabled = true;
			btnCheck.Enabled = false;
		}
		else
		{
			dtDate.Enabled = false;
			dtTime.Enabled = false;
			btnCheck.Enabled = true;
		}
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
		this.labTitle = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnOther = new System.Windows.Forms.Button();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.labInTime = new System.Windows.Forms.CheckBox();
		this.btnCheck = new System.Windows.Forms.Button();
		this.txtQRCode = new System.Windows.Forms.TextBox();
		this.labQRCode = new System.Windows.Forms.Label();
		this.btnFine = new System.Windows.Forms.Button();
		this.btnFree = new System.Windows.Forms.Button();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.btnCancelFree = new System.Windows.Forms.ToolStripMenuItem();
		this.txtFree = new System.Windows.Forms.TextBox();
		this.labFreeTime = new System.Windows.Forms.Label();
		this.comboParkArea = new System.Windows.Forms.ComboBox();
		this.comboParkType = new System.Windows.Forms.ComboBox();
		this.labTimeSplit = new System.Windows.Forms.Label();
		this.txtParkMin = new System.Windows.Forms.TextBox();
		this.txtTotalCharge = new System.Windows.Forms.TextBox();
		this.txtParkHour = new System.Windows.Forms.TextBox();
		this.txtChargeTime = new System.Windows.Forms.TextBox();
		this.dtDate = new System.Windows.Forms.DateTimePicker();
		this.dtTime = new System.Windows.Forms.DateTimePicker();
		this.labParkArea = new System.Windows.Forms.Label();
		this.labTotalCharge = new System.Windows.Forms.Label();
		this.labParkType = new System.Windows.Forms.Label();
		this.labParkTime = new System.Windows.Forms.Label();
		this.labChargeTime = new System.Windows.Forms.Label();
		this.panFill = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		this.panFill.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(593, 60);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "失票處理";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.panel1.Controls.Add(this.btnOther);
		this.panel1.Controls.Add(this.btnOK);
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 623);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(593, 75);
		this.panel1.TabIndex = 1;
		this.btnOther.Enabled = false;
		this.btnOther.ForeColor = System.Drawing.Color.Navy;
		this.btnOther.Location = new System.Drawing.Point(211, 13);
		this.btnOther.Name = "btnOther";
		this.btnOther.Size = new System.Drawing.Size(120, 48);
		this.btnOther.TabIndex = 3;
		this.btnOther.Text = "其他";
		this.btnOther.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOther.UseVisualStyleBackColor = true;
		this.btnOther.Click += new System.EventHandler(btnOther_Click);
		this.btnOK.Enabled = false;
		this.btnOK.ForeColor = System.Drawing.Color.Navy;
		this.btnOK.Location = new System.Drawing.Point(337, 13);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(120, 48);
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "確認";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(463, 13);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(120, 48);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.labInTime);
		this.panel2.Controls.Add(this.btnCheck);
		this.panel2.Controls.Add(this.txtQRCode);
		this.panel2.Controls.Add(this.labQRCode);
		this.panel2.Controls.Add(this.btnFine);
		this.panel2.Controls.Add(this.btnFree);
		this.panel2.Controls.Add(this.txtFree);
		this.panel2.Controls.Add(this.labFreeTime);
		this.panel2.Controls.Add(this.comboParkArea);
		this.panel2.Controls.Add(this.comboParkType);
		this.panel2.Controls.Add(this.labTimeSplit);
		this.panel2.Controls.Add(this.txtParkMin);
		this.panel2.Controls.Add(this.txtTotalCharge);
		this.panel2.Controls.Add(this.txtParkHour);
		this.panel2.Controls.Add(this.txtChargeTime);
		this.panel2.Controls.Add(this.dtDate);
		this.panel2.Controls.Add(this.dtTime);
		this.panel2.Controls.Add(this.labParkArea);
		this.panel2.Controls.Add(this.labTotalCharge);
		this.panel2.Controls.Add(this.labParkType);
		this.panel2.Controls.Add(this.labParkTime);
		this.panel2.Controls.Add(this.labChargeTime);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.panel2.ForeColor = System.Drawing.Color.Navy;
		this.panel2.Location = new System.Drawing.Point(0, 60);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(593, 563);
		this.panel2.TabIndex = 2;
		this.labInTime.Checked = true;
		this.labInTime.CheckState = System.Windows.Forms.CheckState.Checked;
		this.labInTime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labInTime.Location = new System.Drawing.Point(80, 16);
		this.labInTime.Name = "labInTime";
		this.labInTime.Size = new System.Drawing.Size(144, 43);
		this.labInTime.TabIndex = 99;
		this.labInTime.Text = "入場時間";
		this.labInTime.UseVisualStyleBackColor = true;
		this.labInTime.CheckedChanged += new System.EventHandler(labInTime_CheckedChanged);
		this.btnCheck.Enabled = false;
		this.btnCheck.Location = new System.Drawing.Point(276, 485);
		this.btnCheck.Name = "btnCheck";
		this.btnCheck.Size = new System.Drawing.Size(144, 53);
		this.btnCheck.TabIndex = 5;
		this.btnCheck.Text = "查詢";
		this.btnCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCheck.UseVisualStyleBackColor = true;
		this.btnCheck.Click += new System.EventHandler(btnCheck_Click);
		this.txtQRCode.Enabled = false;
		this.txtQRCode.Location = new System.Drawing.Point(230, 261);
		this.txtQRCode.Name = "txtQRCode";
		this.txtQRCode.Size = new System.Drawing.Size(339, 43);
		this.txtQRCode.TabIndex = 97;
		this.labQRCode.Location = new System.Drawing.Point(32, 261);
		this.labQRCode.Name = "labQRCode";
		this.labQRCode.Size = new System.Drawing.Size(192, 43);
		this.labQRCode.TabIndex = 96;
		this.labQRCode.Text = "優惠券";
		this.labQRCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnFine.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnFine.Location = new System.Drawing.Point(426, 485);
		this.btnFine.Name = "btnFine";
		this.btnFine.Size = new System.Drawing.Size(144, 53);
		this.btnFine.TabIndex = 6;
		this.btnFine.Text = "罰款";
		this.btnFine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnFine.UseVisualStyleBackColor = true;
		this.btnFine.Click += new System.EventHandler(btnFine_Click);
		this.btnFree.ContextMenuStrip = this.contextMenuStrip1;
		this.btnFree.Location = new System.Drawing.Point(442, 212);
		this.btnFree.Name = "btnFree";
		this.btnFree.Size = new System.Drawing.Size(127, 43);
		this.btnFree.TabIndex = 4;
		this.btnFree.Text = "免費";
		this.btnFree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnFree.UseVisualStyleBackColor = true;
		this.btnFree.Click += new System.EventHandler(btnFree_Click);
		this.contextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.btnCancelFree });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(165, 36);
		this.btnCancelFree.Name = "btnCancelFree";
		this.btnCancelFree.Size = new System.Drawing.Size(164, 32);
		this.btnCancelFree.Text = "取消免費";
		this.btnCancelFree.Click += new System.EventHandler(btnCancelFree_Click);
		this.txtFree.Enabled = false;
		this.txtFree.Location = new System.Drawing.Point(231, 212);
		this.txtFree.Name = "txtFree";
		this.txtFree.Size = new System.Drawing.Size(139, 43);
		this.txtFree.TabIndex = 90;
		this.labFreeTime.Location = new System.Drawing.Point(33, 212);
		this.labFreeTime.Name = "labFreeTime";
		this.labFreeTime.Size = new System.Drawing.Size(192, 43);
		this.labFreeTime.TabIndex = 89;
		this.labFreeTime.Text = "免费時間";
		this.labFreeTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.comboParkArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkArea.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.comboParkArea.FormattingEnabled = true;
		this.comboParkArea.Location = new System.Drawing.Point(230, 359);
		this.comboParkArea.Name = "comboParkArea";
		this.comboParkArea.Size = new System.Drawing.Size(339, 43);
		this.comboParkArea.TabIndex = 88;
		this.comboParkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkType.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.comboParkType.FormattingEnabled = true;
		this.comboParkType.Location = new System.Drawing.Point(230, 310);
		this.comboParkType.Name = "comboParkType";
		this.comboParkType.Size = new System.Drawing.Size(339, 43);
		this.comboParkType.TabIndex = 88;
		this.labTimeSplit.AutoSize = true;
		this.labTimeSplit.Font = new System.Drawing.Font("微軟正黑體", 20.25f);
		this.labTimeSplit.Location = new System.Drawing.Point(387, 167);
		this.labTimeSplit.Name = "labTimeSplit";
		this.labTimeSplit.Size = new System.Drawing.Size(42, 34);
		this.labTimeSplit.TabIndex = 87;
		this.labTimeSplit.Text = "：";
		this.txtParkMin.Enabled = false;
		this.txtParkMin.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtParkMin.Location = new System.Drawing.Point(442, 163);
		this.txtParkMin.Name = "txtParkMin";
		this.txtParkMin.Size = new System.Drawing.Size(127, 43);
		this.txtParkMin.TabIndex = 84;
		this.txtTotalCharge.BackColor = System.Drawing.Color.White;
		this.txtTotalCharge.Font = new System.Drawing.Font("微软雅黑", 36f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.txtTotalCharge.Location = new System.Drawing.Point(230, 408);
		this.txtTotalCharge.Name = "txtTotalCharge";
		this.txtTotalCharge.ReadOnly = true;
		this.txtTotalCharge.Size = new System.Drawing.Size(339, 71);
		this.txtTotalCharge.TabIndex = 83;
		this.txtTotalCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtTotalCharge.TextChanged += new System.EventHandler(txtTotalCharge_TextChanged);
		this.txtParkHour.Enabled = false;
		this.txtParkHour.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtParkHour.Location = new System.Drawing.Point(231, 163);
		this.txtParkHour.Name = "txtParkHour";
		this.txtParkHour.Size = new System.Drawing.Size(140, 43);
		this.txtParkHour.TabIndex = 86;
		this.txtChargeTime.Enabled = false;
		this.txtChargeTime.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtChargeTime.Location = new System.Drawing.Point(230, 114);
		this.txtChargeTime.Name = "txtChargeTime";
		this.txtChargeTime.Size = new System.Drawing.Size(339, 43);
		this.txtChargeTime.TabIndex = 85;
		this.dtDate.CalendarFont = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.dtDate.CustomFormat = "yyyy / MM / dd";
		this.dtDate.Font = new System.Drawing.Font("微軟正黑體", 20.25f);
		this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtDate.Location = new System.Drawing.Point(230, 16);
		this.dtDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
		this.dtDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtDate.Name = "dtDate";
		this.dtDate.ShowUpDown = true;
		this.dtDate.Size = new System.Drawing.Size(339, 43);
		this.dtDate.TabIndex = 81;
		this.dtTime.CustomFormat = "       HH : mm";
		this.dtTime.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtTime.Location = new System.Drawing.Point(230, 65);
		this.dtTime.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
		this.dtTime.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtTime.Name = "dtTime";
		this.dtTime.ShowUpDown = true;
		this.dtTime.Size = new System.Drawing.Size(339, 43);
		this.dtTime.TabIndex = 82;
		this.labParkArea.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labParkArea.Location = new System.Drawing.Point(32, 359);
		this.labParkArea.Name = "labParkArea";
		this.labParkArea.Size = new System.Drawing.Size(192, 43);
		this.labParkArea.TabIndex = 76;
		this.labParkArea.Text = "區域";
		this.labParkArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTotalCharge.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labTotalCharge.Location = new System.Drawing.Point(32, 408);
		this.labTotalCharge.Name = "labTotalCharge";
		this.labTotalCharge.Size = new System.Drawing.Size(192, 71);
		this.labTotalCharge.TabIndex = 77;
		this.labTotalCharge.Text = "金額";
		this.labTotalCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkType.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labParkType.Location = new System.Drawing.Point(32, 310);
		this.labParkType.Name = "labParkType";
		this.labParkType.Size = new System.Drawing.Size(192, 43);
		this.labParkType.TabIndex = 76;
		this.labParkType.Text = "車型";
		this.labParkType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkTime.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labParkTime.Location = new System.Drawing.Point(33, 163);
		this.labParkTime.Name = "labParkTime";
		this.labParkTime.Size = new System.Drawing.Size(192, 43);
		this.labParkTime.TabIndex = 78;
		this.labParkTime.Text = "停泊時間";
		this.labParkTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labChargeTime.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labChargeTime.Location = new System.Drawing.Point(32, 114);
		this.labChargeTime.Name = "labChargeTime";
		this.labChargeTime.Size = new System.Drawing.Size(192, 43);
		this.labChargeTime.TabIndex = 80;
		this.labChargeTime.Text = "收費時間";
		this.labChargeTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.panel2);
		this.panFill.Controls.Add(this.panel1);
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(595, 700);
		this.panFill.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(595, 700);
		base.Controls.Add(this.panFill);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "FormTimeChargeLost";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormTimeChargeLost";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormTimeChargeLost_FormClosing);
		base.Load += new System.EventHandler(FormTimeChargeLost_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.contextMenuStrip1.ResumeLayout(false);
		this.panFill.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
