using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
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

public class FormRentalCharge : Form
{
	private ILog Logger;

	private ChargeRecord m_ChargeRecord;

	private Card m_Card;

	private RentalType m_RentalType;

	private readonly DateTime initTime;

	private ChargeContext chargeContext = new ChargeContext();

	private IContainer components;

	private Label labTitle;

	private TextBox bindCardNumber;

	private Label labCardNumber;

	private Label labParkTime;

	private Label labParkType;

	private Label labExpireDate;

	private TextBox bindRentalType;

	private Label labRentalType;

	private TextBox bindTotalCharge;

	private Label labTotalCharge;

	private Button btnOther;

	private Button btnOk;

	private Button btnCancel;

	private ComboBox bindParkType;

	private Label labTimeSplit;

	private NumericUpDown bindMonth;

	private Label labStartDate;

	private DateTimePicker bindExpireDate;

	private DateTimePicker bindStartDate;

	private Panel panel1;

	private Panel panel2;

	private Panel panel3;

	private string[] payTypeSums = new string[6] { "掃碼支付", "POS機支付", "轉賬", "支票", "現金", "澳門通POS機" };

	private ComboBox comboxCurrency;

	private Label labCurrency;

	private ComboBox comboBoxPayType;

	private Label labpaytype;

	private TextBox textBoxHKCharge;

	private Label labelTotalHK;

	private RadioButton rdbMOP;

	private RadioButton rdbHKD;

	private RadioButton rdbScan;

	private RadioButton rdbPOS;

	private RadioButton rdbTransfer;

	private RadioButton rdbCheque;

	private RadioButton rdbCash;

	private RadioButton rdbMPassPOS;

	private Panel panelCurrency;

	private Panel panelPayType;

	public int CardID { get; set; }

	public string CardCode { get; set; }

	public FormRentalCharge()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		components = null;
		initTime = DateTime.Now;
		m_ChargeRecord = null;
		m_Card = null;
		m_RentalType = null;
		base.Load += FormRentalCharge_Load;
		BindingHelper.BindComboBox<EnumParkTypeSource>(bindParkType);
		bindParkType.SelectedIndex = -1;
		bindStartDate.Value = bindStartDate.MaxDate;
		bindExpireDate.Value = bindExpireDate.MaxDate;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
		InitializeRadioButtons();
		SetupEventHandlers();
	}

	private void FeeCenterModule_SmartCardReadEvent(string CardCode)
	{
		MessageBox.Show(CardCode);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labCardNumber.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labCardNumber");
		labExpireDate.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labExpireDate");
		labParkTime.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labParkTime");
		labParkType.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labParkType");
		labRentalType.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labRentalType");
		labStartDate.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labStartDate");
		labTimeSplit.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labTimeSplit");
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labTitle");
		labTotalCharge.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labTotalCharge");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.btnCancel");
		btnOk.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.btnOk");
		btnOther.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.btnOther");
		if (LangManager.CurLanguage == SysLanguage.ENG)
		{
			labCurrency.Text = "Currency";
			labpaytype.Text = "Payment";
			labelTotalHK.Text = "Total(HKD)";
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void bindCardNumber_TextChanged(object sender, EventArgs e)
	{
	}

	private void bindMonth_ValueChanged(object sender, EventArgs e)
	{
		if (m_RentalType != null)
		{
			CalcAmount();
		}
	}

	private void CalcAmount()
	{
		try
		{
			if (m_ChargeRecord == null)
			{
				m_ChargeRecord = new ChargeRecord();
			}
			m_ChargeRecord.BillType = 7;
			m_ChargeRecord.CardCode = m_Card.CardCode;
			m_ChargeRecord.ChargeTime = initTime;
			m_ChargeRecord.ParkTypeID = m_RentalType.ParkTypeID;
			GetCurrShiftRecordArgs getCurrShiftRecordArgs = new GetCurrShiftRecordArgs();
			getCurrShiftRecordArgs.PayStationName = Settings.Default.OnlyID;
			ShiftRecord shiftRecord = new ShiftRecord();
			ChargeContext obj = new ChargeContext();
			obj.CommunicationChannel.GetCurrShiftRecord(getCurrShiftRecordArgs, out shiftRecord);
			obj.CommunicationChannel.Disconnect();
			m_ChargeRecord.ShiftID = shiftRecord.ShiftID;
			m_ChargeRecord.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			m_ChargeRecord.FromStation = Settings.Default.OnlyID;
			decimal totalCharge = bindMonth.Value * m_RentalType.NormalCharge;
			textBoxHKCharge.Text = "";
			m_ChargeRecord.Remark2 = null;
			bool flag;
			if (m_RentalType.LimitCharge.HasValue)
			{
				decimal? limitCharge = m_RentalType.LimitCharge;
				flag = (limitCharge.GetValueOrDefault() > default(decimal)) & limitCharge.HasValue;
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				m_ChargeRecord.Remark2 = totalCharge.ToString("f2");
				textBoxHKCharge.Text = totalCharge.ToString("f2");
				totalCharge = bindMonth.Value * m_RentalType.LimitCharge.Value;
			}
			m_ChargeRecord.TotalCharge = totalCharge;
			bindTotalCharge.Text = totalCharge.ToString("f2");
		}
		catch (Exception message)
		{
			m_ChargeRecord = null;
			Logger.Error(message);
		}
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		string currency = (rdbMOP.Checked ? "澳門幣" : "港幣");
		string selectedPaymentMethod = GetSelectedPaymentMethod();
		int payTypeIndex = GetPayTypeIndex();
		bool isMOPChecked = rdbMOP.Checked;
		Card card = m_Card;
		string text = ((card == null) ? null : (card.Deposit.HasValue ? card.Deposit.GetValueOrDefault().ToString("0.00") : null));
		if (text == "0.00")
		{
			text = null;
		}
		using (ConfirmDialog confirmDialog = new ConfirmDialog(currency, selectedPaymentMethod, payTypeIndex, isMOPChecked, text))
		{
			if (confirmDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
		}
		if (m_Card == null)
		{
			Global.ShowMessage("請輸入正確的卡信息");
			return;
		}
		m_ChargeRecord.Currency = (rdbMOP.Checked ? "1" : "2");
		m_ChargeRecord.PayType = GetPayTypeIndex();
		if (m_ChargeRecord.Currency == "2")
		{
			decimal totalCharge = m_ChargeRecord.TotalCharge;
			string remark = m_ChargeRecord.Remark2;
			m_ChargeRecord.TotalCharge = decimal.Parse(remark);
			m_ChargeRecord.Remark2 = totalCharge.ToString();
		}
		SaveChargeRecord(m_ChargeRecord, null, null, null, null);
	}

	private void SaveChargeRecord(ChargeRecord charge, MPass_POS_Transaction_Detail mpass, BOC_Gate_TransactionExtend boc, BOC_N910_POS_Card_Payment_DetailEX bocn910, ScanPayment scanPayment)
	{
		try
		{
			if (m_ChargeRecord == null || m_Card == null || m_RentalType == null)
			{
				Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
				return;
			}
			charge.BillType = 7;
			charge.CardCode = m_Card.CardCode;
			SaveMPassChargeArgs saveMPassChargeArgs = new SaveMPassChargeArgs();
			saveMPassChargeArgs.CardCode = m_Card.CardCode;
			saveMPassChargeArgs.ExpireDate = bindExpireDate.Value;
			saveMPassChargeArgs.StartDate = m_Card.StartDate;
			charge.StartDate = bindStartDate.Value;
			charge.EndDate = saveMPassChargeArgs.ExpireDate;
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
			ChargeContext chargeContext = new ChargeContext();
			SaveMPassChargeReturn saveMPassChargeReturn = chargeContext.CommunicationChannel.SaveMPassCharge(saveMPassChargeArgs, charge, mpass, boc, bocn910, scanPayment);
			chargeContext.CommunicationChannel.Disconnect();
			if (saveMPassChargeReturn.ISValid)
			{
				if (charge.TotalCharge != 0m && mpass == null && boc == null)
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
				int? num = 0;
				foreach (StaffType staffType in DataBuffer2018.StaffTypes)
				{
					if (DataBuffer2018.CurrentStaff.StaffTypeId == staffType.StaffTypeID)
					{
						num = staffType.SystemCode;
						break;
					}
				}
				GetLastChargeRecordArgs getLastChargeRecordArgs = new GetLastChargeRecordArgs
				{
					PayStationName = Settings.Default.OnlyID,
					StaffCode = DataBuffer2018.CurrentStaff.StaffCode
				};
				ChargeRecord chargeRecord = new ChargeRecord();
				MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = new MPass_POS_Transaction_Detail();
				TransactionData transactionData = new TransactionData();
				if (Common._Carpark2018ServiceContext.CommunicationChannel.GetLastChargeRecord(getLastChargeRecordArgs, out chargeRecord, out mPass_POS_Transaction_Detail, out transactionData).ISOK && chargeRecord != null)
				{
					int timeChargeID = chargeRecord.TimeChargeID;
					if (num.HasValue)
					{
						Process.Start(string.Concat(new object[12]
						{
							Settings.Default.ReportPath,
							"park/sj.html?StaffCode=",
							DataBuffer2018.CurrentStaff.StaffCode,
							"&StaffPwd=",
							DataBuffer2018.CurrentStaff.StaffPwd,
							"&StaffId=",
							DataBuffer2018.CurrentStaff.StaffId.ToString(),
							"&StaffName=",
							DataBuffer2018.CurrentStaff.StaffName,
							"&StaffTypeId=",
							DataBuffer2018.CurrentStaff.StaffTypeId.ToString(),
							"&TimeChargeID=" + timeChargeID
						}));
						string text = (rdbMOP.Checked ? "澳門幣" : "港幣");
						string text2 = (rdbMOP.Checked ? m_ChargeRecord.TotalCharge.ToString() : m_ChargeRecord.Remark2);
						string param = "卡號：" + charge.CardCode + "；\n支付方式：" + GetSelectedPaymentMethod() + text + "；\n支付金額：" + text2 + "；\n操作員：" + DataBuffer2018.CurrentStaff.StaffCode;
						if (Settings.Default.rent == "1")
						{
							ExecuteHttpRequest("銀座月租續期", param);
						}
					}
				}
				else if (num.HasValue)
				{
					Process.Start(Settings.Default.ReportPath + "park/rent.html?StaffCode=" + DataBuffer2018.CurrentStaff.StaffCode + "&StaffPwd=" + DataBuffer2018.CurrentStaff.StaffPwd + "&StaffId=" + DataBuffer2018.CurrentStaff.StaffId.ToString() + "&StaffName=" + DataBuffer2018.CurrentStaff.StaffName + "&StaffTypeId=" + DataBuffer2018.CurrentStaff.StaffTypeId.ToString());
				}
				else
				{
					MessageBox.Show("該員工沒有系統權限");
				}
				Close();
			}
			else
			{
				Global.ShowMessage(saveMPassChargeReturn.ErrCode);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
		}
		catch (Exception message2)
		{
			Logger.Error(message2);
		}
	}

	private void btnOther_Click(object sender, EventArgs e)
	{
		if (m_Card == null)
		{
			Global.ShowMessage("請輸入正確的卡信息");
			return;
		}
		using FormEpaySale formEpaySale = new FormEpaySale
		{
			ChargeRecord = m_ChargeRecord
		};
		if (formEpaySale.ShowDialog() == DialogResult.OK)
		{
			formEpaySale.ChargeRecord.Currency = "1";
			formEpaySale.ChargeRecord.PayType = (int)formEpaySale.PayTypeFlag;
			SaveChargeRecord(formEpaySale.ChargeRecord, formEpaySale.MPass, formEpaySale.BOC, formEpaySale.BOC_N910, formEpaySale.SPay);
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void bindTotalCharge_TextChanged(object sender, EventArgs e)
	{
		try
		{
			DeviceManager.FeeCenterModule.DisplayFee(bindTotalCharge.Text);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
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
		this.labTitle = new System.Windows.Forms.Label();
		this.bindExpireDate = new System.Windows.Forms.DateTimePicker();
		this.bindStartDate = new System.Windows.Forms.DateTimePicker();
		this.bindMonth = new System.Windows.Forms.NumericUpDown();
		this.bindParkType = new System.Windows.Forms.ComboBox();
		this.btnOther = new System.Windows.Forms.Button();
		this.btnOk = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.bindTotalCharge = new System.Windows.Forms.TextBox();
		this.labTotalCharge = new System.Windows.Forms.Label();
		this.labTimeSplit = new System.Windows.Forms.Label();
		this.labParkTime = new System.Windows.Forms.Label();
		this.labParkType = new System.Windows.Forms.Label();
		this.labStartDate = new System.Windows.Forms.Label();
		this.labExpireDate = new System.Windows.Forms.Label();
		this.bindRentalType = new System.Windows.Forms.TextBox();
		this.labRentalType = new System.Windows.Forms.Label();
		this.bindCardNumber = new System.Windows.Forms.TextBox();
		this.labCardNumber = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.comboxCurrency = new System.Windows.Forms.ComboBox();
		this.labCurrency = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.comboBoxPayType = new System.Windows.Forms.ComboBox();
		this.labpaytype = new System.Windows.Forms.Label();
		this.textBoxHKCharge = new System.Windows.Forms.TextBox();
		this.labelTotalHK = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.bindMonth).BeginInit();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 30f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(1298, 60);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "月租續費";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.bindExpireDate.CustomFormat = "yyyy-MM-dd";
		this.bindExpireDate.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindExpireDate.Location = new System.Drawing.Point(244, 228);
		this.bindExpireDate.Name = "bindExpireDate";
		this.bindExpireDate.Size = new System.Drawing.Size(398, 62);
		this.bindExpireDate.TabIndex = 6;
		this.bindStartDate.CustomFormat = "yyyy-MM-dd";
		this.bindStartDate.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindStartDate.Location = new System.Drawing.Point(244, 155);
		this.bindStartDate.Name = "bindStartDate";
		this.bindStartDate.Size = new System.Drawing.Size(398, 62);
		this.bindStartDate.TabIndex = 6;
		this.bindMonth.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindMonth.Location = new System.Drawing.Point(244, 374);
		this.bindMonth.Maximum = new decimal(new int[4] { 99, 0, 0, 0 });
		this.bindMonth.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.bindMonth.Name = "bindMonth";
		this.bindMonth.Size = new System.Drawing.Size(115, 62);
		this.bindMonth.TabIndex = 5;
		this.bindMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.bindMonth.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.bindMonth.ValueChanged += new System.EventHandler(bindMonth_ValueChanged);
		this.bindParkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.bindParkType.Enabled = false;
		this.bindParkType.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindParkType.FormattingEnabled = true;
		this.bindParkType.Location = new System.Drawing.Point(244, 301);
		this.bindParkType.Name = "bindParkType";
		this.bindParkType.Size = new System.Drawing.Size(398, 63);
		this.bindParkType.TabIndex = 4;
		this.btnOther.Enabled = false;
		this.btnOther.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnOther.ForeColor = System.Drawing.Color.Navy;
		this.btnOther.Location = new System.Drawing.Point(163, 8);
		this.btnOther.Name = "btnOther";
		this.btnOther.Size = new System.Drawing.Size(120, 48);
		this.btnOther.TabIndex = 3;
		this.btnOther.Text = "其他";
		this.btnOther.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOther.UseVisualStyleBackColor = true;
		this.btnOther.Click += new System.EventHandler(btnOther_Click);
		this.btnOk.Enabled = false;
		this.btnOk.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnOk.ForeColor = System.Drawing.Color.Navy;
		this.btnOk.Location = new System.Drawing.Point(313, 8);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(120, 48);
		this.btnOk.TabIndex = 3;
		this.btnOk.Text = "確認";
		this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOk.UseVisualStyleBackColor = true;
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(463, 8);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(120, 48);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.bindTotalCharge.Font = new System.Drawing.Font("微软雅黑", 60f);
		this.bindTotalCharge.Location = new System.Drawing.Point(244, 447);
		this.bindTotalCharge.Name = "bindTotalCharge";
		this.bindTotalCharge.ReadOnly = true;
		this.bindTotalCharge.Size = new System.Drawing.Size(398, 139);
		this.bindTotalCharge.TabIndex = 1;
		this.bindTotalCharge.TextChanged += new System.EventHandler(bindTotalCharge_TextChanged);
		this.labTotalCharge.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labTotalCharge.ForeColor = System.Drawing.Color.Navy;
		this.labTotalCharge.Location = new System.Drawing.Point(38, 447);
		this.labTotalCharge.Name = "labTotalCharge";
		this.labTotalCharge.Size = new System.Drawing.Size(200, 113);
		this.labTotalCharge.TabIndex = 0;
		this.labTotalCharge.Text = "實收金額";
		this.labTotalCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTimeSplit.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labTimeSplit.ForeColor = System.Drawing.Color.Navy;
		this.labTimeSplit.Location = new System.Drawing.Point(365, 374);
		this.labTimeSplit.Name = "labTimeSplit";
		this.labTimeSplit.Size = new System.Drawing.Size(129, 51);
		this.labTimeSplit.TabIndex = 0;
		this.labTimeSplit.Text = "個月";
		this.labTimeSplit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labParkTime.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labParkTime.ForeColor = System.Drawing.Color.Navy;
		this.labParkTime.Location = new System.Drawing.Point(38, 374);
		this.labParkTime.Name = "labParkTime";
		this.labParkTime.Size = new System.Drawing.Size(200, 51);
		this.labParkTime.TabIndex = 0;
		this.labParkTime.Text = "續費時間";
		this.labParkTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkType.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labParkType.ForeColor = System.Drawing.Color.Navy;
		this.labParkType.Location = new System.Drawing.Point(38, 301);
		this.labParkType.Name = "labParkType";
		this.labParkType.Size = new System.Drawing.Size(200, 51);
		this.labParkType.TabIndex = 0;
		this.labParkType.Text = "車型";
		this.labParkType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labStartDate.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labStartDate.ForeColor = System.Drawing.Color.Navy;
		this.labStartDate.Location = new System.Drawing.Point(38, 155);
		this.labStartDate.Name = "labStartDate";
		this.labStartDate.Size = new System.Drawing.Size(200, 51);
		this.labStartDate.TabIndex = 0;
		this.labStartDate.Text = "開始日期";
		this.labStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labExpireDate.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labExpireDate.ForeColor = System.Drawing.Color.Navy;
		this.labExpireDate.Location = new System.Drawing.Point(38, 228);
		this.labExpireDate.Name = "labExpireDate";
		this.labExpireDate.Size = new System.Drawing.Size(200, 51);
		this.labExpireDate.TabIndex = 0;
		this.labExpireDate.Text = "失效日期";
		this.labExpireDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindRentalType.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindRentalType.Location = new System.Drawing.Point(244, 82);
		this.bindRentalType.Name = "bindRentalType";
		this.bindRentalType.ReadOnly = true;
		this.bindRentalType.Size = new System.Drawing.Size(398, 62);
		this.bindRentalType.TabIndex = 1;
		this.labRentalType.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labRentalType.ForeColor = System.Drawing.Color.Navy;
		this.labRentalType.Location = new System.Drawing.Point(38, 82);
		this.labRentalType.Name = "labRentalType";
		this.labRentalType.Size = new System.Drawing.Size(200, 51);
		this.labRentalType.TabIndex = 0;
		this.labRentalType.Text = "租賃方式";
		this.labRentalType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindCardNumber.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindCardNumber.Location = new System.Drawing.Point(244, 9);
		this.bindCardNumber.Name = "bindCardNumber";
		this.bindCardNumber.Size = new System.Drawing.Size(398, 62);
		this.bindCardNumber.TabIndex = 1;
		this.bindCardNumber.TextChanged += new System.EventHandler(bindCardNumber_TextChanged);
		this.bindCardNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(bindCardNumber_KeyDown);
		this.labCardNumber.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labCardNumber.ForeColor = System.Drawing.Color.Navy;
		this.labCardNumber.Location = new System.Drawing.Point(38, 9);
		this.labCardNumber.Name = "labCardNumber";
		this.labCardNumber.Size = new System.Drawing.Size(200, 51);
		this.labCardNumber.TabIndex = 0;
		this.labCardNumber.Text = "卡號";
		this.labCardNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Controls.Add(this.btnOther);
		this.panel1.Controls.Add(this.btnOk);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 633);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1298, 65);
		this.panel1.TabIndex = 4;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.textBoxHKCharge);
		this.panel2.Controls.Add(this.labelTotalHK);
		this.panel2.Controls.Add(this.labpaytype);
		this.panel2.Controls.Add(this.labCurrency);
		this.panel2.Controls.Add(this.bindExpireDate);
		this.panel2.Controls.Add(this.bindCardNumber);
		this.panel2.Controls.Add(this.bindStartDate);
		this.panel2.Controls.Add(this.labCardNumber);
		this.panel2.Controls.Add(this.bindMonth);
		this.panel2.Controls.Add(this.labRentalType);
		this.panel2.Controls.Add(this.bindParkType);
		this.panel2.Controls.Add(this.bindRentalType);
		this.panel2.Controls.Add(this.bindTotalCharge);
		this.panel2.Controls.Add(this.labExpireDate);
		this.panel2.Controls.Add(this.labTotalCharge);
		this.panel2.Controls.Add(this.labStartDate);
		this.panel2.Controls.Add(this.labTimeSplit);
		this.panel2.Controls.Add(this.labParkType);
		this.panel2.Controls.Add(this.labParkTime);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 60);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(1298, 573);
		this.panel2.TabIndex = 5;
		this.labCurrency.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labCurrency.ForeColor = System.Drawing.Color.Navy;
		this.labCurrency.Location = new System.Drawing.Point(800, 9);
		this.labCurrency.Name = "labCurrency";
		this.labCurrency.Size = new System.Drawing.Size(200, 51);
		this.labCurrency.TabIndex = 7;
		this.labCurrency.Text = "幣種";
		this.labCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.panel2);
		this.panel3.Controls.Add(this.panel1);
		this.panel3.Controls.Add(this.labTitle);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(1300, 700);
		this.panel3.TabIndex = 4;
		this.labpaytype.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labpaytype.ForeColor = System.Drawing.Color.Navy;
		this.labpaytype.Location = new System.Drawing.Point(840, 166);
		this.labpaytype.Name = "labpaytype";
		this.labpaytype.Size = new System.Drawing.Size(200, 51);
		this.labpaytype.TabIndex = 9;
		this.labpaytype.Text = "支付方式";
		this.labpaytype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.textBoxHKCharge.Font = new System.Drawing.Font("微软雅黑", 60f);
		this.textBoxHKCharge.Location = new System.Drawing.Point(863, 447);
		this.textBoxHKCharge.Name = "textBoxHKCharge";
		this.textBoxHKCharge.ReadOnly = true;
		this.textBoxHKCharge.Size = new System.Drawing.Size(398, 139);
		this.textBoxHKCharge.TabIndex = 12;
		this.labelTotalHK.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labelTotalHK.ForeColor = System.Drawing.Color.Navy;
		this.labelTotalHK.Location = new System.Drawing.Point(657, 447);
		this.labelTotalHK.Name = "labelTotalHK";
		this.labelTotalHK.Size = new System.Drawing.Size(200, 113);
		this.labelTotalHK.TabIndex = 11;
		this.labelTotalHK.Text = "港幣金額";
		this.labelTotalHK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(16f, 35f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(1300, 700);
		base.Controls.Add(this.panel3);
		this.Font = new System.Drawing.Font("微软雅黑", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(7);
		base.Name = "FormRentalCharge";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormRentalCharge";
		((System.ComponentModel.ISupportInitialize)this.bindMonth).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel3.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void bindCardNumber_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		try
		{
			GetRentalChargeArgs getRentalChargeArgs = new GetRentalChargeArgs();
			getRentalChargeArgs.CardNumber = bindCardNumber.Text;
			ChargeContext obj = new ChargeContext();
			obj.CommunicationChannel.GetRentalCharge(getRentalChargeArgs, out m_Card, out m_RentalType);
			obj.CommunicationChannel.Disconnect();
			if (m_Card == null || m_RentalType == null)
			{
				bindRentalType.Text = "";
				bindTotalCharge.Text = "0.0";
				bindMonth.Value = 1m;
				bindParkType.SelectedValue = 1;
				bindStartDate.Value = bindStartDate.MaxDate;
				bindExpireDate.Value = bindExpireDate.MaxDate;
				btnOk.Enabled = false;
				btnOther.Enabled = false;
				comboBoxPayType.Enabled = false;
				return;
			}
			btnOk.Enabled = true;
			comboBoxPayType.Enabled = true;
			bindRentalType.Text = m_RentalType.RentalName;
			if (m_RentalType.RentalName.Contains("年"))
			{
				labTimeSplit.Text = "年";
			}
			else
			{
				labTimeSplit.Text = "個月";
			}
			bindParkType.SelectedValue = m_RentalType.ParkTypeID;
			DateTime baseDate = m_Card.ExpireDate ?? DateTime.Now;
			bindStartDate.Value = baseDate.AddDays(1.0);
			bindExpireDate.Value = ((!m_Card.ExpireDate.HasValue) ? DateTime.Now : Convert.ToDateTime(m_Card.ExpireDate));
			UpdateExpireDate(baseDate, bindMonth.Value);
			bindTotalCharge.Text = (bindMonth.Value * m_RentalType.NormalCharge).ToString();
			CalcAmount();
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception message)
		{
			bindRentalType.Text = "";
			bindTotalCharge.Text = "0.0";
			bindMonth.Value = 1m;
			bindParkType.SelectedValue = 1;
			bindStartDate.Value = bindStartDate.MaxDate;
			bindExpireDate.Value = bindExpireDate.MaxDate;
			comboBoxPayType.Enabled = false;
			Logger.Error(message);
		}
	}

	private void comboxCurrency_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void comboBoxPayType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (comboBoxPayType.SelectedIndex == 5)
		{
			btnOther.Enabled = true;
			btnOk.Enabled = false;
		}
		else
		{
			btnOther.Enabled = false;
			btnOk.Enabled = true;
		}
	}

	private void FormRentalCharge_Load(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(CardCode))
		{
			bindCardNumber.Text = CardCode;
			bindCardNumber_KeyDown(bindCardNumber, new KeyEventArgs(Keys.Return));
		}
		bindMonth.ValueChanged += BindMonth_ValueChanged;
	}

	private void UpdateExpireDate(DateTime baseDate, decimal monthIncrement)
	{
		int num = (int)monthIncrement;
		if (m_RentalType != null && m_RentalType.RentalName.Contains("年"))
		{
			num *= 12;
		}
		DateTime dateTime = baseDate.AddMonths(num);
		int day = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
		bindExpireDate.Value = new DateTime(dateTime.Year, dateTime.Month, day);
	}

	private void BindMonth_ValueChanged(object sender, EventArgs e)
	{
		if (m_Card != null && m_Card.ExpireDate.HasValue)
		{
			DateTime value = m_Card.ExpireDate.Value;
			UpdateExpireDate(value, bindMonth.Value);
		}
		TextBox textBox = bindTotalCharge;
		decimal value2 = bindMonth.Value;
		RentalType rentalType = m_RentalType;
		decimal value3 = value2;
		decimal? obj = rentalType?.NormalCharge;
		textBox.Text = ((decimal?)value3 * obj).GetValueOrDefault().ToString();
		CalcAmount();
	}

	private void InitializeRadioButtons()
	{
		panelCurrency = new Panel();
		rdbMOP = new RadioButton();
		rdbHKD = new RadioButton();
		rdbMOP.Text = "澳門幣";
		rdbMOP.Font = new Font("微软雅黑", 20f);
		rdbMOP.Appearance = Appearance.Button;
		rdbMOP.Size = new Size(180, 60);
		rdbMOP.Location = new Point(0, 0);
		rdbMOP.FlatStyle = FlatStyle.Standard;
		rdbMOP.FlatAppearance.BorderSize = 2;
		rdbMOP.BackColor = Color.FromArgb(0, 120, 215);
		rdbMOP.TextAlign = ContentAlignment.MiddleCenter;
		rdbHKD.Text = "港\u3000幣";
		rdbHKD.Font = new Font("微软雅黑", 20f);
		rdbHKD.Appearance = Appearance.Button;
		rdbHKD.Size = new Size(180, 60);
		rdbHKD.Location = new Point(190, 0);
		rdbHKD.FlatStyle = FlatStyle.Standard;
		rdbHKD.FlatAppearance.BorderSize = 2;
		rdbHKD.BackColor = Color.White;
		rdbHKD.TextAlign = ContentAlignment.MiddleCenter;
		panelCurrency.Controls.Add(rdbMOP);
		panelCurrency.Controls.Add(rdbHKD);
		panelCurrency.Location = new Point(770, 70);
		panelCurrency.Size = new Size(370, 70);
		panelPayType = new Panel();
		rdbScan = new RadioButton();
		rdbPOS = new RadioButton();
		rdbTransfer = new RadioButton();
		rdbCheque = new RadioButton();
		rdbCash = new RadioButton();
		rdbMPassPOS = new RadioButton();
		rdbScan.Text = "掃\u3000\u3000碼";
		rdbScan.Location = new Point(0, 0);
		rdbPOS.Text = "手持POS機";
		rdbPOS.Location = new Point(190, 0);
		rdbTransfer.Text = "銀行轉賬";
		rdbTransfer.Location = new Point(0, 70);
		rdbCheque.Text = "支\u3000\u3000票";
		rdbCheque.Location = new Point(190, 70);
		rdbCash.Text = "現\u3000\u3000金";
		rdbCash.Location = new Point(0, 140);
		rdbMPassPOS.Text = "澳門通POS機";
		rdbMPassPOS.Location = new Point(190, 140);
		RadioButton[] array = new RadioButton[6] { rdbScan, rdbPOS, rdbTransfer, rdbCheque, rdbCash, rdbMPassPOS };
		foreach (RadioButton radioButton in array)
		{
			radioButton.Font = new Font("微软雅黑", 18f);
			radioButton.Size = new Size(180, 60);
			radioButton.Appearance = Appearance.Button;
			radioButton.FlatStyle = FlatStyle.Standard;
			radioButton.FlatAppearance.BorderSize = 2;
			radioButton.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 120, 215);
			radioButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 84, 152);
			radioButton.BackColor = Color.White;
			radioButton.TextAlign = ContentAlignment.MiddleCenter;
			panelPayType.Controls.Add(radioButton);
		}
		rdbMOP.Checked = true;
		rdbPOS.Checked = true;
		rdbPOS.BackColor = Color.FromArgb(0, 120, 215);
		panelPayType.Location = new Point(770, 220);
		panelPayType.Size = new Size(370, 210);
		panel2.Controls.Add(panelCurrency);
		panel2.Controls.Add(panelPayType);
	}

	private void SetupEventHandlers()
	{
		rdbHKD.CheckedChanged += Currency_CheckedChanged;
		rdbMOP.CheckedChanged += Currency_CheckedChanged;
		RadioButton[] array = new RadioButton[6] { rdbScan, rdbPOS, rdbTransfer, rdbCheque, rdbCash, rdbMPassPOS };
		for (int i = 0; i < array.Length; i++)
		{
			array[i].CheckedChanged += PayType_CheckedChanged;
		}
	}

	private void Currency_CheckedChanged(object sender, EventArgs e)
	{
		bool flag = rdbHKD.Checked;
		rdbPOS.Enabled = !flag;
		rdbScan.Enabled = !flag;
		rdbMPassPOS.Enabled = !flag;
		rdbMOP.BackColor = (rdbMOP.Checked ? Color.FromArgb(0, 120, 215) : Color.White);
		rdbHKD.BackColor = (rdbHKD.Checked ? Color.FromArgb(0, 120, 215) : Color.White);
		if (flag && (rdbPOS.Checked || rdbScan.Checked || rdbMPassPOS.Checked))
		{
			rdbCash.Checked = true;
		}
	}

	private int GetPayTypeIndex()
	{
		if (rdbScan.Checked)
		{
			return 8;
		}
		if (rdbPOS.Checked)
		{
			return 7;
		}
		if (rdbTransfer.Checked)
		{
			return 5;
		}
		if (rdbCheque.Checked)
		{
			return 6;
		}
		if (rdbCash.Checked)
		{
			return 0;
		}
		if (rdbMPassPOS.Checked)
		{
			return 2;
		}
		return -1;
	}

	private void PayType_CheckedChanged(object sender, EventArgs e)
	{
		RadioButton[] array = new RadioButton[6] { rdbScan, rdbPOS, rdbTransfer, rdbCheque, rdbCash, rdbMPassPOS };
		foreach (RadioButton obj in array)
		{
			obj.BackColor = (obj.Checked ? Color.FromArgb(0, 120, 215) : Color.White);
		}
		if (!rdbMPassPOS.Checked)
		{
			btnOther.Enabled = false;
			btnOk.Enabled = true;
		}
		else
		{
			btnOther.Enabled = true;
			btnOk.Enabled = false;
		}
	}

	private string GetSelectedPaymentMethod()
	{
		if (rdbScan.Checked)
		{
			return "掃碼";
		}
		if (rdbPOS.Checked)
		{
			return "手持POS機";
		}
		if (rdbTransfer.Checked)
		{
			return "銀行轉賬";
		}
		if (rdbCheque.Checked)
		{
			return "支票";
		}
		if (rdbCash.Checked)
		{
			return "現金";
		}
		if (rdbMPassPOS.Checked)
		{
			return "澳門通POS機";
		}
		return "未知方式";
	}

		private void ExecuteHttpRequest(string param1, string param2)
		{
			System.Net.WebClient client = new System.Net.WebClient();
			string requestUri = Settings.Default.ReportPath + "park/php/angushw.php?param1=" + param1 + "&param2=" + param2;
			try
			{
				client.DownloadString(requestUri);
			}
			catch (Exception message)
			{
				Logger.Error(message);
			}
		}
}
