using System;
using System.Collections.Generic;
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
using CarPark2018.Forms;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.LPPayForms;

public class FormLPPayLost : Form
{
	private readonly DateTime initTime;

	private ILog Logger;

	private ChargeRecord m_ChargeRecord;

	private CalcTicketFeeArgs feeArg = new CalcTicketFeeArgs();

	private SaveChargeRecordArgs saveArg = new SaveChargeRecordArgs();

	private string TicketNumber = "";

	private EnumParkType parkType = EnumParkType.None;

	private DateTime inTime;

	private bool Fine = false;

	private string FreeImagePath = null;

	public string Remark = "";

	private bool Syn = false;

	private ChargeContext chargeContext = new ChargeContext();

	private IContainer components = null;

	private Panel panFill;

	private Label labTitle;

	private Panel panBottom;

	private Button btnOther;

	private Button btnOK;

	private Button btnCancel;

	private TextBox txtQRCode;

	private Label labQRCode;

	private Button btnFine;

	private Button btnFree;

	private TextBox txtFree;

	private Label labFreeTime;

	private ComboBox comboParkArea;

	private ComboBox comboParkType;

	private Label labTimeSplit;

	private TextBox txtParkMin;

	private TextBox txtTotalCharge;

	private TextBox txtParkHour;

	private TextBox txtChargeTime;

	private DateTimePicker dtDate;

	private DateTimePicker dtTime;

	private Label labParkArea;

	private Label labTotalCharge;

	private Label labParkType;

	private Label labParkTime;

	private Label labChargeTime;

	private Label labLP;

	private TextBox txtLP;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem btnCancelFree;

	private Label labInTime;

	public FormLPPayLost()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		initTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
		InitializeComponent();
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
		LoadImage();
		try
		{
			BindingHelper.BindComboBox<EnumParkTypeSource>(comboParkType);
			comboParkArea.DataSource = DataBuffer2018.AllParkAreas;
			comboParkArea.DisplayMember = "AreaName";
			comboParkArea.ValueMember = "AreaID";
			comboParkArea.SelectedValue = Convert.ToInt32(Config.AreaCode);
			comboParkType.SelectedValue = -1;
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
		btnCancel.Focus();
		dtDate.Value = initTime;
		dtTime.Value = initTime;
		comboParkType.SelectedIndexChanged += dtDate_ValueChanged;
		comboParkArea.SelectedIndexChanged += dtDate_ValueChanged;
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

	private void btnCancel_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		base.DialogResult = DialogResult.Cancel;
		Close();
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
		btnOther.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.btnOther");
		labLP.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeLost.labLP");
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
			CalcTicketFeeReturn calcTicketFeeReturn = chargeContext.CommunicationChannel.CalcTicketFee(feeArg, parkType, EnumBillType.TimeChargeLost, out m_ChargeRecord);
			chargeContext.CommunicationChannel.Disconnect();
			if (calcTicketFeeReturn.ISValid)
			{
				txtChargeTime.Text = initTime.ToString(SystemParm.LongTimeFormat);
				txtParkHour.Text = (m_ChargeRecord.ParkMin / 60).ToString();
				txtParkMin.Text = (m_ChargeRecord.ParkMin % 60).ToString();
				txtFree.Text = m_ChargeRecord.FreeMin.ToString();
				comboParkType.SelectedValue = m_ChargeRecord.ParkTypeID;
				txtTotalCharge.Text = m_ChargeRecord.TotalCharge.ToString("f2");
				btnOK.Enabled = true;
				btnOther.Enabled = true;
				FormFee.Self().SetTicket(saveArg.InTime.ToString(SystemParm.LongTimeFormat), txtChargeTime.Text, $"{txtParkHour.Text}小時{txtParkMin.Text}分", txtTotalCharge.Text);
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
				btnOK.Enabled = false;
				btnOther.Enabled = false;
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

	private void dtDate_ValueChanged(object sender, EventArgs e)
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
			if (comboParkType.SelectedIndex >= 0 && !txtLP.Enabled)
			{
				try
				{
					List<view_transactionandlp> _view_transactionandlp = new List<view_transactionandlp>();
					GetView_TransactionAndLPArgs getView_TransactionAndLPArgs = new GetView_TransactionAndLPArgs();
					getView_TransactionAndLPArgs.LicensePlate = txtLP.Text.ToString();
					GetView_TransactionAndLPReturn view_TransactionAndLP = chargeContext.CommunicationChannel.GetView_TransactionAndLP(getView_TransactionAndLPArgs, out _view_transactionandlp);
					chargeContext.CommunicationChannel.Disconnect();
					if (_view_transactionandlp.Count > 0)
					{
						Global.ShowMessage("該車已在場");
						txtLP.Text = "";
						btnOK.Enabled = false;
						btnOther.Enabled = false;
						return;
					}
					TicketNumber = txtLP.Text.ToString();
				}
				catch (Exception message)
				{
					Logger.Error(message);
				}
				CalcAmount();
				btnCancel.Focus();
			}
			else
			{
				btnOK.Enabled = false;
				btnOther.Enabled = false;
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
		}
	}

	private void FormLPPayLost_QRCodeScanEvent(string code)
	{
		btnCancel.Focus();
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
					if (comboParkType.SelectedIndex >= 0 && txtLP.Text.Length >= 6)
					{
						CalcAmount();
					}
				}
			});
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void btnFree_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
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
				if (comboParkType.SelectedIndex >= 0 && txtLP.Text.Length >= 6)
				{
					CalcAmount();
				}
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void btnCancelFree_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		feeArg.CustomFreeID = 0;
		feeArg.CustomFreeTenatID = 0;
		FreeImagePath = null;
		Remark = "";
		txtQRCode.Text = "";
		if (comboParkType.SelectedIndex >= 0 && txtLP.Text.Length >= 6)
		{
			CalcAmount();
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		m_ChargeRecord.BillType = 18;
		m_ChargeRecord.CardCode = TicketNumber;
		m_ChargeRecord.PayType = 0;
		SaveChargeRecord(m_ChargeRecord, null, null, null, null);
		try
		{
			if (m_ChargeRecord.TotalCharge > 0m)
			{
				DeviceManager.FeeCenterModule.OpenCash();
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
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
				saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveArg, parkType, charge, MPass, boc);
				chargeContext.CommunicationChannel.Disconnect();
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

	private void btnFine_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		Fine = !Fine;
		feeArg.ISFine = Fine;
		txtTotalCharge.ForeColor = (Fine ? Color.Red : Color.Black);
		if (comboParkType.SelectedIndex >= 0 && txtLP.Text.Length >= 6)
		{
			CalcAmount();
		}
	}

	private void btnOther_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		FormEpaySale formEpaySale = new FormEpaySale
		{
			ChargeRecord = m_ChargeRecord
		};
		using FormEpaySale formEpaySale2 = formEpaySale;
		if (formEpaySale2.ShowDialog() == DialogResult.OK)
		{
			formEpaySale2.ChargeRecord.BillType = 18;
			formEpaySale2.ChargeRecord.CardCode = TicketNumber;
			formEpaySale2.ChargeRecord.PayType = (int)formEpaySale2.PayTypeFlag;
			SaveChargeRecord(m_ChargeRecord, formEpaySale2.MPass, formEpaySale2.BOC, formEpaySale2.BOC_N910, formEpaySale2.SPay);
		}
	}

	private void FormLPPayLost_FormClosing(object sender, FormClosingEventArgs e)
	{
		FormFee.Self2();
	}

	private void txtLP_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtLP.Enabled = false;
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
		this.panFill = new System.Windows.Forms.Panel();
		this.labInTime = new System.Windows.Forms.Label();
		this.labLP = new System.Windows.Forms.Label();
		this.txtLP = new System.Windows.Forms.TextBox();
		this.txtQRCode = new System.Windows.Forms.TextBox();
		this.labQRCode = new System.Windows.Forms.Label();
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
		this.panBottom = new System.Windows.Forms.Panel();
		this.btnOther = new System.Windows.Forms.Button();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.btnFine = new System.Windows.Forms.Button();
		this.labTitle = new System.Windows.Forms.Label();
		this.panFill.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		this.panBottom.SuspendLayout();
		base.SuspendLayout();
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.labInTime);
		this.panFill.Controls.Add(this.labLP);
		this.panFill.Controls.Add(this.txtLP);
		this.panFill.Controls.Add(this.txtQRCode);
		this.panFill.Controls.Add(this.labQRCode);
		this.panFill.Controls.Add(this.btnFree);
		this.panFill.Controls.Add(this.txtFree);
		this.panFill.Controls.Add(this.labFreeTime);
		this.panFill.Controls.Add(this.comboParkArea);
		this.panFill.Controls.Add(this.comboParkType);
		this.panFill.Controls.Add(this.labTimeSplit);
		this.panFill.Controls.Add(this.txtParkMin);
		this.panFill.Controls.Add(this.txtTotalCharge);
		this.panFill.Controls.Add(this.txtParkHour);
		this.panFill.Controls.Add(this.txtChargeTime);
		this.panFill.Controls.Add(this.dtDate);
		this.panFill.Controls.Add(this.dtTime);
		this.panFill.Controls.Add(this.labParkArea);
		this.panFill.Controls.Add(this.labTotalCharge);
		this.panFill.Controls.Add(this.labParkType);
		this.panFill.Controls.Add(this.labParkTime);
		this.panFill.Controls.Add(this.labChargeTime);
		this.panFill.Controls.Add(this.panBottom);
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(595, 740);
		this.panFill.TabIndex = 1;
		this.labInTime.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labInTime.ForeColor = System.Drawing.Color.Navy;
		this.labInTime.Location = new System.Drawing.Point(46, 122);
		this.labInTime.Name = "labInTime";
		this.labInTime.Size = new System.Drawing.Size(192, 43);
		this.labInTime.TabIndex = 124;
		this.labInTime.Text = "入場時間";
		this.labInTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labLP.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labLP.ForeColor = System.Drawing.Color.Navy;
		this.labLP.Location = new System.Drawing.Point(46, 71);
		this.labLP.Name = "labLP";
		this.labLP.Size = new System.Drawing.Size(192, 43);
		this.labLP.TabIndex = 123;
		this.labLP.Text = "車牌";
		this.labLP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtLP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtLP.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLP.Location = new System.Drawing.Point(243, 72);
		this.txtLP.MaxLength = 7;
		this.txtLP.Name = "txtLP";
		this.txtLP.Size = new System.Drawing.Size(339, 43);
		this.txtLP.TabIndex = 0;
		this.txtLP.TabStop = false;
		this.txtLP.KeyDown += new System.Windows.Forms.KeyEventHandler(txtLP_KeyDown);
		this.txtQRCode.Enabled = false;
		this.txtQRCode.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.txtQRCode.Location = new System.Drawing.Point(243, 541);
		this.txtQRCode.Name = "txtQRCode";
		this.txtQRCode.Size = new System.Drawing.Size(339, 43);
		this.txtQRCode.TabIndex = 0;
		this.txtQRCode.TabStop = false;
		this.txtQRCode.Visible = false;
		this.labQRCode.Font = new System.Drawing.Font("微软雅黑", 20.25f);
		this.labQRCode.ForeColor = System.Drawing.Color.Navy;
		this.labQRCode.Location = new System.Drawing.Point(45, 541);
		this.labQRCode.Name = "labQRCode";
		this.labQRCode.Size = new System.Drawing.Size(192, 43);
		this.labQRCode.TabIndex = 119;
		this.labQRCode.Text = "優惠券";
		this.labQRCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labQRCode.Visible = false;
		this.btnFree.ContextMenuStrip = this.contextMenuStrip1;
		this.btnFree.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnFree.ForeColor = System.Drawing.Color.Navy;
		this.btnFree.Location = new System.Drawing.Point(455, 317);
		this.btnFree.Name = "btnFree";
		this.btnFree.Size = new System.Drawing.Size(127, 43);
		this.btnFree.TabIndex = 5;
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
		this.txtFree.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.txtFree.Location = new System.Drawing.Point(244, 317);
		this.txtFree.Name = "txtFree";
		this.txtFree.Size = new System.Drawing.Size(139, 43);
		this.txtFree.TabIndex = 0;
		this.txtFree.TabStop = false;
		this.labFreeTime.Font = new System.Drawing.Font("微软雅黑", 20.25f);
		this.labFreeTime.ForeColor = System.Drawing.Color.Navy;
		this.labFreeTime.Location = new System.Drawing.Point(46, 317);
		this.labFreeTime.Name = "labFreeTime";
		this.labFreeTime.Size = new System.Drawing.Size(192, 43);
		this.labFreeTime.TabIndex = 117;
		this.labFreeTime.Text = "免费時間";
		this.labFreeTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.comboParkArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkArea.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.comboParkArea.FormattingEnabled = true;
		this.comboParkArea.Location = new System.Drawing.Point(244, 416);
		this.comboParkArea.Name = "comboParkArea";
		this.comboParkArea.Size = new System.Drawing.Size(339, 43);
		this.comboParkArea.TabIndex = 0;
		this.comboParkArea.TabStop = false;
		this.comboParkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkType.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.comboParkType.FormattingEnabled = true;
		this.comboParkType.Location = new System.Drawing.Point(244, 367);
		this.comboParkType.Name = "comboParkType";
		this.comboParkType.Size = new System.Drawing.Size(339, 43);
		this.comboParkType.TabIndex = 0;
		this.comboParkType.TabStop = false;
		this.labTimeSplit.AutoSize = true;
		this.labTimeSplit.Font = new System.Drawing.Font("微軟正黑體", 20.25f);
		this.labTimeSplit.Location = new System.Drawing.Point(400, 272);
		this.labTimeSplit.Name = "labTimeSplit";
		this.labTimeSplit.Size = new System.Drawing.Size(42, 34);
		this.labTimeSplit.TabIndex = 114;
		this.labTimeSplit.Text = "：";
		this.txtParkMin.Enabled = false;
		this.txtParkMin.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtParkMin.Location = new System.Drawing.Point(455, 268);
		this.txtParkMin.Name = "txtParkMin";
		this.txtParkMin.Size = new System.Drawing.Size(127, 43);
		this.txtParkMin.TabIndex = 0;
		this.txtParkMin.TabStop = false;
		this.txtTotalCharge.BackColor = System.Drawing.Color.White;
		this.txtTotalCharge.Font = new System.Drawing.Font("微软雅黑", 36f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.txtTotalCharge.Location = new System.Drawing.Point(244, 465);
		this.txtTotalCharge.Name = "txtTotalCharge";
		this.txtTotalCharge.ReadOnly = true;
		this.txtTotalCharge.Size = new System.Drawing.Size(339, 71);
		this.txtTotalCharge.TabIndex = 0;
		this.txtTotalCharge.TabStop = false;
		this.txtTotalCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtParkHour.Enabled = false;
		this.txtParkHour.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtParkHour.Location = new System.Drawing.Point(244, 268);
		this.txtParkHour.Name = "txtParkHour";
		this.txtParkHour.Size = new System.Drawing.Size(140, 43);
		this.txtParkHour.TabIndex = 0;
		this.txtParkHour.TabStop = false;
		this.txtChargeTime.Enabled = false;
		this.txtChargeTime.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtChargeTime.Location = new System.Drawing.Point(243, 219);
		this.txtChargeTime.Name = "txtChargeTime";
		this.txtChargeTime.Size = new System.Drawing.Size(339, 43);
		this.txtChargeTime.TabIndex = 0;
		this.txtChargeTime.TabStop = false;
		this.dtDate.CalendarFont = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.dtDate.CustomFormat = "yyyy / MM / dd";
		this.dtDate.Font = new System.Drawing.Font("微軟正黑體", 20.25f);
		this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtDate.Location = new System.Drawing.Point(243, 121);
		this.dtDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
		this.dtDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtDate.Name = "dtDate";
		this.dtDate.ShowUpDown = true;
		this.dtDate.Size = new System.Drawing.Size(339, 43);
		this.dtDate.TabIndex = 0;
		this.dtDate.TabStop = false;
		this.dtDate.ValueChanged += new System.EventHandler(dtDate_ValueChanged);
		this.dtTime.CustomFormat = "       HH : mm";
		this.dtTime.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtTime.Location = new System.Drawing.Point(243, 170);
		this.dtTime.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
		this.dtTime.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtTime.Name = "dtTime";
		this.dtTime.ShowUpDown = true;
		this.dtTime.Size = new System.Drawing.Size(339, 43);
		this.dtTime.TabIndex = 0;
		this.dtTime.TabStop = false;
		this.dtTime.ValueChanged += new System.EventHandler(dtDate_ValueChanged);
		this.labParkArea.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labParkArea.ForeColor = System.Drawing.Color.Navy;
		this.labParkArea.Location = new System.Drawing.Point(46, 416);
		this.labParkArea.Name = "labParkArea";
		this.labParkArea.Size = new System.Drawing.Size(192, 43);
		this.labParkArea.TabIndex = 103;
		this.labParkArea.Text = "區域";
		this.labParkArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTotalCharge.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labTotalCharge.ForeColor = System.Drawing.Color.Navy;
		this.labTotalCharge.Location = new System.Drawing.Point(46, 465);
		this.labTotalCharge.Name = "labTotalCharge";
		this.labTotalCharge.Size = new System.Drawing.Size(192, 71);
		this.labTotalCharge.TabIndex = 105;
		this.labTotalCharge.Text = "金額";
		this.labTotalCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkType.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labParkType.ForeColor = System.Drawing.Color.Navy;
		this.labParkType.Location = new System.Drawing.Point(46, 367);
		this.labParkType.Name = "labParkType";
		this.labParkType.Size = new System.Drawing.Size(192, 43);
		this.labParkType.TabIndex = 104;
		this.labParkType.Text = "車型";
		this.labParkType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkTime.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labParkTime.ForeColor = System.Drawing.Color.Navy;
		this.labParkTime.Location = new System.Drawing.Point(46, 268);
		this.labParkTime.Name = "labParkTime";
		this.labParkTime.Size = new System.Drawing.Size(192, 43);
		this.labParkTime.TabIndex = 106;
		this.labParkTime.Text = "停泊時間";
		this.labParkTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labChargeTime.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labChargeTime.ForeColor = System.Drawing.Color.Navy;
		this.labChargeTime.Location = new System.Drawing.Point(45, 219);
		this.labChargeTime.Name = "labChargeTime";
		this.labChargeTime.Size = new System.Drawing.Size(192, 43);
		this.labChargeTime.TabIndex = 107;
		this.labChargeTime.Text = "收費時間";
		this.labChargeTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panBottom.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.panBottom.Controls.Add(this.btnOther);
		this.panBottom.Controls.Add(this.btnOK);
		this.panBottom.Controls.Add(this.btnCancel);
		this.panBottom.Controls.Add(this.btnFine);
		this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panBottom.Location = new System.Drawing.Point(0, 590);
		this.panBottom.Name = "panBottom";
		this.panBottom.Size = new System.Drawing.Size(593, 148);
		this.panBottom.TabIndex = 2;
		this.btnOther.Enabled = false;
		this.btnOther.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.btnOther.ForeColor = System.Drawing.Color.Navy;
		this.btnOther.Location = new System.Drawing.Point(166, 13);
		this.btnOther.Name = "btnOther";
		this.btnOther.Size = new System.Drawing.Size(136, 48);
		this.btnOther.TabIndex = 3;
		this.btnOther.Text = "其他";
		this.btnOther.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOther.UseVisualStyleBackColor = true;
		this.btnOther.Click += new System.EventHandler(btnOther_Click);
		this.btnOK.Enabled = false;
		this.btnOK.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.btnOK.ForeColor = System.Drawing.Color.Navy;
		this.btnOK.Location = new System.Drawing.Point(309, 13);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(136, 48);
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "確認";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(450, 13);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(136, 48);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.btnFine.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.btnFine.ForeColor = System.Drawing.Color.Navy;
		this.btnFine.Location = new System.Drawing.Point(24, 13);
		this.btnFine.Name = "btnFine";
		this.btnFine.Size = new System.Drawing.Size(136, 48);
		this.btnFine.TabIndex = 4;
		this.btnFine.Text = "罰款";
		this.btnFine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnFine.UseVisualStyleBackColor = true;
		this.btnFine.Click += new System.EventHandler(btnFine_Click);
		this.labTitle.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(593, 60);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "失票處理";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(595, 661);
		base.Controls.Add(this.panFill);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormLPPayLost";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormLPPayLost";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormLPPayLost_FormClosing);
		this.panFill.ResumeLayout(false);
		this.panFill.PerformLayout();
		this.contextMenuStrip1.ResumeLayout(false);
		this.panBottom.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
