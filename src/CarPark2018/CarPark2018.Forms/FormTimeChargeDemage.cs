using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

public class FormTimeChargeDemage : Form
{
	private readonly DateTime initTime;

	private ILog Logger;

	private ChargeRecord m_ChargeRecord;

	private TransactionData m_TransactionData;

	private SaveChargeRecordArgs saveArg = new SaveChargeRecordArgs();

	private CalcTicketFeeArgs feeArg = new CalcTicketFeeArgs();

	private EnumParkType parkType = EnumParkType.None;

	public string FreeImagePath = null;

	public string Remark = "";

	private bool IsCharge = false;

	private FormTimeChargeTimeOut form = null;

	private bool Syn = false;

	private ChargeContext chargeContext = new ChargeContext();

	private IContainer components = null;

	private Label labTitle;

	private Panel panel1;

	private Button btnOK;

	private Button btnCancel;

	private Panel panel2;

	private Button btnFree;

	private TextBox txtTotalCharge;

	private ComboBox comboParkType;

	private TextBox txtFree;

	private TextBox txtParkMin;

	private TextBox txtParkHour;

	private TextBox txtChargeTime;

	private TextBox txtInTime;

	private TextBox txtTicketNo;

	private Label labTotalCharge;

	private Label labParkType;

	private Label labFreeTime;

	private Label labTimeSplit;

	private Label labParkTime;

	private Label labChargeTime;

	private Label labLastTime;

	private Label labTicketNo;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem btnCancelFree;

	private Panel panFill;

	private ComboBox comboParkArea;

	private Label labArea;

	private Button btnOther;

	private TextBox txtQRCode;

	private Label labQRCode;

	public FormTimeChargeDemage()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		initTime = DateTime.Now;
		m_ChargeRecord = null;
		m_TransactionData = null;
		InitializeComponent();
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
		try
		{
			BindingHelper.BindComboBox<EnumParkTypeSource>(comboParkType);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labChargeTime.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.labChargeTime");
		labFreeTime.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.labelX1");
		labLastTime.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.labLastTime");
		labParkTime.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.labParkTime");
		labParkType.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.labParkType");
		labTicketNo.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.labTicketNo");
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.labTitle");
		labTotalCharge.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.labTotalCharge");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.btnClose");
		btnCancelFree.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.cancelFree");
		btnFree.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.btnFree");
		btnOK.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeDemage.btnOK");
		labQRCode.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labQRCode");
		labArea.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labParkArea");
		btnOther.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.btnOther");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		try
		{
			DeviceManager.FeeCenterModule.EjectTicket();
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void btnFree_Click(object sender, EventArgs e)
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

	private void btnOK_Click(object sender, EventArgs e)
	{
		m_ChargeRecord.BillType = 1;
		m_ChargeRecord.CardCode = m_TransactionData.InCardCode;
		m_ChargeRecord.PayType = 0;
		SaveChargeRecord(m_ChargeRecord, null, null, null, null);
	}

	private void CalcAmount()
	{
		try
		{
			if (m_TransactionData != null)
			{
				feeArg.TicketNumber = m_TransactionData.InCardCode;
				feeArg.PayStationName = Settings.Default.OnlyID;
				feeArg.SerialNumber = "";
				feeArg.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
				feeArg.ISFine = false;
				feeArg.ChargeTime = initTime;
				feeArg.InTime = m_TransactionData.InTime;
				parkType = (EnumParkType)m_TransactionData.ParkTypeID;
				feeArg.BarCode = txtQRCode.Text;
				CalcTicketFeeReturn calcTicketFeeReturn = chargeContext.CommunicationChannel.CalcTicketFee(feeArg, parkType, EnumBillType.TimeChargeDemage, out m_ChargeRecord);
				chargeContext.CommunicationChannel.Disconnect();
				saveArg.ISTimeOut = calcTicketFeeReturn.ISTimeOut;
				if (calcTicketFeeReturn.ISValid)
				{
					if (!calcTicketFeeReturn.HasLastTimeCharge)
					{
						txtTicketNo.Text = m_TransactionData.InCardCode;
						txtInTime.Text = m_TransactionData.InTime.ToString(SystemParm.LongTimeFormat);
						txtChargeTime.Text = m_ChargeRecord.ChargeTime.ToString(SystemParm.LongTimeFormat);
						comboParkType.SelectedValue = m_TransactionData.ParkTypeID;
						txtParkHour.Text = (m_ChargeRecord.ParkMin / 60).ToString();
						txtParkMin.Text = (m_ChargeRecord.ParkMin % 60).ToString();
						txtFree.Text = m_ChargeRecord.FreeMin.ToString();
						txtTotalCharge.Text = m_ChargeRecord.TotalCharge.ToString("f2");
						if (!string.IsNullOrEmpty(m_TransactionData.Remark))
						{
							try
							{
								comboParkArea.SelectedValue = Convert.ToInt32(m_TransactionData.Remark);
								return;
							}
							catch (Exception message)
							{
								Logger.Error(message);
								return;
							}
						}
						comboParkArea.SelectedValue = (from m in DataBuffer2018.AllParkGates
							where m.GateID == m_TransactionData.InGateID
							select m.GateAreaID).FirstOrDefault();
						return;
					}
					if (calcTicketFeeReturn.ISTimeOut)
					{
						Global.ShowMessage("已超時");
						using (form = new FormTimeChargeTimeOut(m_ChargeRecord, m_TransactionData, calcTicketFeeReturn.LastTimeCharge, IsCharge))
						{
							form.ShowDialog();
							Close();
							return;
						}
					}
					txtTicketNo.Text = m_TransactionData.InCardCode;
					txtInTime.Text = m_TransactionData.InTime.ToString(SystemParm.LongTimeFormat);
					txtChargeTime.Text = m_ChargeRecord.ChargeTime.ToString(SystemParm.LongTimeFormat);
					comboParkType.SelectedValue = m_TransactionData.ParkTypeID;
					txtParkHour.Text = (m_ChargeRecord.ParkMin / 60).ToString();
					txtParkMin.Text = (m_ChargeRecord.ParkMin % 60).ToString();
					txtFree.Text = m_ChargeRecord.FreeMin.ToString();
					comboParkType.SelectedValue = m_TransactionData.ParkTypeID;
					txtTotalCharge.Text = m_ChargeRecord.TotalCharge.ToString("f2");
					if (!string.IsNullOrEmpty(m_TransactionData.Remark))
					{
						try
						{
							comboParkArea.SelectedValue = Convert.ToInt32(m_TransactionData.Remark);
						}
						catch (Exception message2)
						{
							Logger.Error(message2);
						}
					}
					else
					{
						comboParkArea.SelectedValue = (from m in DataBuffer2018.AllParkGates
							where m.GateID == m_TransactionData.InGateID
							select m.GateAreaID).FirstOrDefault();
					}
					Global.ShowMessage("未超時");
				}
				else
				{
					if (calcTicketFeeReturn.ErrCode == "Coupon_Invalid")
					{
						txtQRCode.Text = "";
					}
					Global.ShowMessage(calcTicketFeeReturn.ErrCode);
				}
			}
			else
			{
				Console.WriteLine("m_TransactionData is Null");
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請超新操作");
			btnCancel_Click(null, null);
		}
		catch (Exception message3)
		{
			Logger.Error(message3);
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

	private void txtTicketNo_TextChanged(object sender, EventArgs e)
	{
		if (txtTicketNo.Text.Length != 7)
		{
			return;
		}
		try
		{
			GetTransactionDataArgs getTransactionDataArgs = new GetTransactionDataArgs();
			getTransactionDataArgs.TicketNumber = txtTicketNo.Text;
			GetTransactionDataReturn transactionData = chargeContext.CommunicationChannel.GetTransactionData(getTransactionDataArgs, out m_TransactionData);
			chargeContext.CommunicationChannel.Disconnect();
			if (transactionData.ISOK)
			{
				CalcAmount();
			}
			else
			{
				Global.ShowMessage(LangManager.GetLangString(transactionData.ErrCode));
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

	private void FormTimeChargeDemage_Activated(object sender, EventArgs e)
	{
		txtTicketNo.Focus();
	}

	private void CheckValid(bool status)
	{
		if (status)
		{
			btnOK.Enabled = true;
			btnOther.Enabled = true;
			btnOK.Focus();
		}
		else
		{
			btnOK.Enabled = false;
			btnOther.Enabled = false;
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
				btnOK.Focus();
				IsCharge = true;
				btnOther.Enabled = true;
				if (form != null)
				{
					form.btnOK.Enabled = true;
					form.btnOther.Enabled = true;
				}
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

	private void FormTimeChargeDemage_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent -= FeeCenterModule_TicketScanEvent;
		}
	}

	private void FormTimeChargeDemage_Load(object sender, EventArgs e)
	{
		comboParkArea.DataSource = DataBuffer2018.AllParkAreas;
		comboParkArea.DisplayMember = "AreaName";
		comboParkArea.ValueMember = "AreaID";
		comboParkArea.SelectedValue = Convert.ToInt32(Config.AreaCode);
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent += FeeCenterModule_TicketScanEvent;
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
			formEpaySale2.ChargeRecord.BillType = 1;
			formEpaySale2.ChargeRecord.CardCode = m_TransactionData.InCardCode;
			formEpaySale2.ChargeRecord.PayType = (int)formEpaySale2.PayTypeFlag;
			SaveChargeRecord(formEpaySale2.ChargeRecord, formEpaySale2.MPass, formEpaySale2.BOC, formEpaySale2.BOC_N910, formEpaySale2.SPay);
		}
	}

	private void SaveChargeRecord(ChargeRecord charge, MPass_POS_Transaction_Detail mpass, BOC_Gate_TransactionExtend boc, BOC_N910_POS_Card_Payment_DetailEX bocn910, ScanPayment scanPayment)
	{
		if (charge == null)
		{
			Global.ShowMessage(LangManager.GetLangString("Alert.Input"));
			return;
		}
		try
		{
			saveArg.CustomFreeID = feeArg.CustomFreeID;
			saveArg.CustomFreeTenatID = feeArg.CustomFreeTenatID;
			saveArg.InTime = feeArg.InTime;
			saveArg.TicketNumber = feeArg.TicketNumber;
			saveArg.FreeImagePath = FreeImagePath;
			saveArg.CustomFreeRecordRemark = Remark;
			saveArg.BarCode = txtQRCode.Text;
			SaveChargeRecordReturn saveChargeRecordReturn = null;
			ChargeRecord chargeRecord = null;
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = null;
			BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend = null;
			BOC_N910_POS_Card_Payment_DetailEX bOC_N910_POS_Card_Payment_DetailEX = null;
			ScanPayment scanPayment2 = null;
			try
			{
				if (mpass != null)
				{
					if (mpass.PAY_MODE == "mpay")
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
				saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveArg, parkType, charge, mpass, boc);
				chargeContext.CommunicationChannel.Disconnect();
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				if (mpass != null || bocn910 != null || scanPayment != null)
				{
					DBHelper.Insert(charge.CardCode, charge, mpass, boc, saveArg, bocn910, scanPayment);
					chargeRecord = DBHelper.SelectChargeRecord(charge.CardCode);
					if (mpass != null)
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
			if (saveChargeRecordReturn != null)
			{
				if (saveChargeRecordReturn.ISOK)
				{
					if (m_ChargeRecord.ParkTypeID == 5 || m_ChargeRecord.ParkTypeID == 4)
					{
						m_TransactionData.ParkTypeID = 1;
					}
					FeeInfo feeInfo = new FeeInfo
					{
						InTime = m_TransactionData.InTime,
						ParkType = (EnumParkType)m_TransactionData.ParkTypeID,
						TicketNumber = m_TransactionData.InCardCode,
						TicketAction = EnumTicketAction.Normal
					};
					FeeInfo feeInfo2 = feeInfo;
					feeInfo2.Fee = charge.TotalCharge;
					feeInfo2.FeeTime = charge.ChargeTime;
					feeInfo2.NeedPrint = true;
					feeInfo2.TicketAction = EnumTicketAction.Normal;
					feeInfo2.TicketType = EnumTicketType.Lost;
					feeInfo2.FieldStr = comboParkArea.Text;
					Logger.Debug(m_TransactionData.InGateID.ToString());
					if (m_TransactionData.InGateID != 0)
					{
						feeInfo2.CarParkSerialNo = DataBuffer2018.GetCarParkSerialEx((EnumParkType)m_TransactionData.ParkTypeID, m_TransactionData.InGateID);
					}
					else
					{
						feeInfo2.CarParkSerialNo = DataBuffer2018.GetCarParkSerial((EnumParkType)m_TransactionData.ParkTypeID, Convert.ToInt32(m_TransactionData.Remark));
					}
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
					DeviceManager.FeeCenterModule.EjectTicket();
					if (m_ChargeRecord.TotalCharge != 0m && mpass == null && boc == null)
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
				}
				else
				{
					Global.ShowMessage(saveChargeRecordReturn.ErrCode);
				}
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
			btnCancel_Click(null, null);
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
			Logger.Error(ex5);
			Console.WriteLine(ex5.Message);
			MessageBox.Show(ex5.Message);
		}
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void FormTimeChargeLost_QRCodeScanEvent(string code)
	{
		try
		{
			Invoke((MethodInvoker)delegate
			{
				if (m_ChargeRecord != null && m_ChargeRecord.TotalCharge > 0m && !(txtQRCode.Text == code))
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
		this.txtQRCode = new System.Windows.Forms.TextBox();
		this.labQRCode = new System.Windows.Forms.Label();
		this.btnFree = new System.Windows.Forms.Button();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.btnCancelFree = new System.Windows.Forms.ToolStripMenuItem();
		this.txtTotalCharge = new System.Windows.Forms.TextBox();
		this.comboParkArea = new System.Windows.Forms.ComboBox();
		this.comboParkType = new System.Windows.Forms.ComboBox();
		this.txtFree = new System.Windows.Forms.TextBox();
		this.txtParkMin = new System.Windows.Forms.TextBox();
		this.txtParkHour = new System.Windows.Forms.TextBox();
		this.txtChargeTime = new System.Windows.Forms.TextBox();
		this.txtInTime = new System.Windows.Forms.TextBox();
		this.txtTicketNo = new System.Windows.Forms.TextBox();
		this.labArea = new System.Windows.Forms.Label();
		this.labTotalCharge = new System.Windows.Forms.Label();
		this.labParkType = new System.Windows.Forms.Label();
		this.labFreeTime = new System.Windows.Forms.Label();
		this.labTimeSplit = new System.Windows.Forms.Label();
		this.labParkTime = new System.Windows.Forms.Label();
		this.labChargeTime = new System.Windows.Forms.Label();
		this.labLastTime = new System.Windows.Forms.Label();
		this.labTicketNo = new System.Windows.Forms.Label();
		this.panFill = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		this.panFill.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(593, 60);
		this.labTitle.TabIndex = 4;
		this.labTitle.Text = "壞票處理";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.Controls.Add(this.btnOther);
		this.panel1.Controls.Add(this.btnOK);
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.panel1.Location = new System.Drawing.Point(0, 622);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(593, 76);
		this.panel1.TabIndex = 5;
		this.btnOther.Enabled = false;
		this.btnOther.ForeColor = System.Drawing.Color.Navy;
		this.btnOther.Location = new System.Drawing.Point(211, 14);
		this.btnOther.Name = "btnOther";
		this.btnOther.Size = new System.Drawing.Size(120, 48);
		this.btnOther.TabIndex = 3;
		this.btnOther.Text = "其他";
		this.btnOther.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOther.UseVisualStyleBackColor = true;
		this.btnOther.Click += new System.EventHandler(btnOther_Click);
		this.btnOK.Enabled = false;
		this.btnOK.ForeColor = System.Drawing.Color.Navy;
		this.btnOK.Location = new System.Drawing.Point(337, 14);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(120, 48);
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "確認";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(463, 14);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(120, 48);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.txtQRCode);
		this.panel2.Controls.Add(this.labQRCode);
		this.panel2.Controls.Add(this.btnFree);
		this.panel2.Controls.Add(this.txtTotalCharge);
		this.panel2.Controls.Add(this.comboParkArea);
		this.panel2.Controls.Add(this.comboParkType);
		this.panel2.Controls.Add(this.txtFree);
		this.panel2.Controls.Add(this.txtParkMin);
		this.panel2.Controls.Add(this.txtParkHour);
		this.panel2.Controls.Add(this.txtChargeTime);
		this.panel2.Controls.Add(this.txtInTime);
		this.panel2.Controls.Add(this.txtTicketNo);
		this.panel2.Controls.Add(this.labArea);
		this.panel2.Controls.Add(this.labTotalCharge);
		this.panel2.Controls.Add(this.labParkType);
		this.panel2.Controls.Add(this.labFreeTime);
		this.panel2.Controls.Add(this.labTimeSplit);
		this.panel2.Controls.Add(this.labParkTime);
		this.panel2.Controls.Add(this.labChargeTime);
		this.panel2.Controls.Add(this.labLastTime);
		this.panel2.Controls.Add(this.labTicketNo);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.panel2.ForeColor = System.Drawing.Color.Navy;
		this.panel2.Location = new System.Drawing.Point(0, 60);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(593, 562);
		this.panel2.TabIndex = 6;
		this.txtQRCode.Enabled = false;
		this.txtQRCode.Location = new System.Drawing.Point(248, 295);
		this.txtQRCode.Name = "txtQRCode";
		this.txtQRCode.Size = new System.Drawing.Size(328, 43);
		this.txtQRCode.TabIndex = 99;
		this.labQRCode.Location = new System.Drawing.Point(14, 299);
		this.labQRCode.Name = "labQRCode";
		this.labQRCode.Size = new System.Drawing.Size(228, 34);
		this.labQRCode.TabIndex = 98;
		this.labQRCode.Text = "優惠券";
		this.labQRCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnFree.ContextMenuStrip = this.contextMenuStrip1;
		this.btnFree.Location = new System.Drawing.Point(449, 236);
		this.btnFree.Name = "btnFree";
		this.btnFree.Size = new System.Drawing.Size(127, 44);
		this.btnFree.TabIndex = 4;
		this.btnFree.Text = "免費";
		this.btnFree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnFree.UseVisualStyleBackColor = true;
		this.btnFree.Click += new System.EventHandler(btnFree_Click);
		this.contextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.btnCancelFree });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(199, 44);
		this.btnCancelFree.Name = "btnCancelFree";
		this.btnCancelFree.Size = new System.Drawing.Size(198, 40);
		this.btnCancelFree.Text = "取消免費";
		this.btnCancelFree.Click += new System.EventHandler(btnCancelFree_Click);
		this.txtTotalCharge.BackColor = System.Drawing.Color.White;
		this.txtTotalCharge.Font = new System.Drawing.Font("微軟正黑體", 36f, System.Drawing.FontStyle.Bold);
		this.txtTotalCharge.Location = new System.Drawing.Point(248, 464);
		this.txtTotalCharge.Name = "txtTotalCharge";
		this.txtTotalCharge.ReadOnly = true;
		this.txtTotalCharge.Size = new System.Drawing.Size(328, 71);
		this.txtTotalCharge.TabIndex = 92;
		this.txtTotalCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtTotalCharge.TextChanged += new System.EventHandler(txtTotalCharge_TextChanged);
		this.comboParkArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkArea.Enabled = false;
		this.comboParkArea.Font = new System.Drawing.Font("微軟正黑體", 20.25f);
		this.comboParkArea.FormattingEnabled = true;
		this.comboParkArea.Location = new System.Drawing.Point(248, 408);
		this.comboParkArea.Name = "comboParkArea";
		this.comboParkArea.Size = new System.Drawing.Size(328, 42);
		this.comboParkArea.TabIndex = 91;
		this.comboParkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkType.Enabled = false;
		this.comboParkType.Font = new System.Drawing.Font("微軟正黑體", 20.25f);
		this.comboParkType.FormattingEnabled = true;
		this.comboParkType.Location = new System.Drawing.Point(248, 352);
		this.comboParkType.Name = "comboParkType";
		this.comboParkType.Size = new System.Drawing.Size(328, 42);
		this.comboParkType.TabIndex = 91;
		this.txtFree.Enabled = false;
		this.txtFree.Location = new System.Drawing.Point(248, 238);
		this.txtFree.Name = "txtFree";
		this.txtFree.Size = new System.Drawing.Size(145, 43);
		this.txtFree.TabIndex = 87;
		this.txtParkMin.Enabled = false;
		this.txtParkMin.Location = new System.Drawing.Point(445, 181);
		this.txtParkMin.Name = "txtParkMin";
		this.txtParkMin.Size = new System.Drawing.Size(131, 43);
		this.txtParkMin.TabIndex = 86;
		this.txtParkHour.Enabled = false;
		this.txtParkHour.Location = new System.Drawing.Point(248, 181);
		this.txtParkHour.Name = "txtParkHour";
		this.txtParkHour.Size = new System.Drawing.Size(131, 43);
		this.txtParkHour.TabIndex = 88;
		this.txtChargeTime.Enabled = false;
		this.txtChargeTime.Location = new System.Drawing.Point(248, 124);
		this.txtChargeTime.Name = "txtChargeTime";
		this.txtChargeTime.Size = new System.Drawing.Size(328, 43);
		this.txtChargeTime.TabIndex = 90;
		this.txtInTime.Enabled = false;
		this.txtInTime.Location = new System.Drawing.Point(248, 67);
		this.txtInTime.Name = "txtInTime";
		this.txtInTime.Size = new System.Drawing.Size(328, 43);
		this.txtInTime.TabIndex = 89;
		this.txtTicketNo.Location = new System.Drawing.Point(248, 10);
		this.txtTicketNo.Name = "txtTicketNo";
		this.txtTicketNo.Size = new System.Drawing.Size(328, 43);
		this.txtTicketNo.TabIndex = 85;
		this.txtTicketNo.TextChanged += new System.EventHandler(txtTicketNo_TextChanged);
		this.labArea.Location = new System.Drawing.Point(14, 411);
		this.labArea.Name = "labArea";
		this.labArea.Size = new System.Drawing.Size(228, 34);
		this.labArea.TabIndex = 83;
		this.labArea.Text = "區域";
		this.labArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTotalCharge.Location = new System.Drawing.Point(14, 464);
		this.labTotalCharge.Name = "labTotalCharge";
		this.labTotalCharge.Size = new System.Drawing.Size(228, 71);
		this.labTotalCharge.TabIndex = 84;
		this.labTotalCharge.Text = "金額";
		this.labTotalCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkType.Location = new System.Drawing.Point(14, 355);
		this.labParkType.Name = "labParkType";
		this.labParkType.Size = new System.Drawing.Size(228, 34);
		this.labParkType.TabIndex = 83;
		this.labParkType.Text = "車型";
		this.labParkType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFreeTime.Location = new System.Drawing.Point(14, 242);
		this.labFreeTime.Name = "labFreeTime";
		this.labFreeTime.Size = new System.Drawing.Size(228, 34);
		this.labFreeTime.TabIndex = 82;
		this.labFreeTime.Text = "免费時間";
		this.labFreeTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTimeSplit.AutoSize = true;
		this.labTimeSplit.Location = new System.Drawing.Point(391, 185);
		this.labTimeSplit.Name = "labTimeSplit";
		this.labTimeSplit.Size = new System.Drawing.Size(42, 35);
		this.labTimeSplit.TabIndex = 80;
		this.labTimeSplit.Text = "：";
		this.labParkTime.Location = new System.Drawing.Point(14, 185);
		this.labParkTime.Name = "labParkTime";
		this.labParkTime.Size = new System.Drawing.Size(228, 34);
		this.labParkTime.TabIndex = 81;
		this.labParkTime.Text = "泊车時間";
		this.labParkTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labChargeTime.Location = new System.Drawing.Point(14, 128);
		this.labChargeTime.Name = "labChargeTime";
		this.labChargeTime.Size = new System.Drawing.Size(228, 34);
		this.labChargeTime.TabIndex = 79;
		this.labChargeTime.Text = "收費時間";
		this.labChargeTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labLastTime.Location = new System.Drawing.Point(14, 71);
		this.labLastTime.Name = "labLastTime";
		this.labLastTime.Size = new System.Drawing.Size(228, 34);
		this.labLastTime.TabIndex = 78;
		this.labLastTime.Text = "入場時間";
		this.labLastTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTicketNo.Location = new System.Drawing.Point(14, 14);
		this.labTicketNo.Name = "labTicketNo";
		this.labTicketNo.Size = new System.Drawing.Size(228, 34);
		this.labTicketNo.TabIndex = 77;
		this.labTicketNo.Text = "票號";
		this.labTicketNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.panel2);
		this.panFill.Controls.Add(this.panel1);
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(595, 700);
		this.panFill.TabIndex = 5;
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(595, 700);
		base.Controls.Add(this.panFill);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "FormTimeChargeDemage";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormTimeChargeDemage";
		base.Activated += new System.EventHandler(FormTimeChargeDemage_Activated);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormTimeChargeDemage_FormClosing);
		base.Load += new System.EventHandler(FormTimeChargeDemage_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.contextMenuStrip1.ResumeLayout(false);
		this.panFill.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
