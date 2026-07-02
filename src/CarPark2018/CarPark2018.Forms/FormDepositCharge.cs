using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
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

public class FormDepositCharge : Form
{
	private ILog Logger;

	private ChargeRecord m_ChargeRecord;

	private Card m_Card;

	private RentalType m_RentalType;

	private readonly DateTime initTime;

	private ChargeContext chargeContext = new ChargeContext();

	private IContainer components = null;

	private Label labTitle;

	private TextBox bindCardNumber;

	private Label labCardNumber;

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

	private Label labStartDate;

	private DateTimePicker bindExpireDate;

	private DateTimePicker bindStartDate;

	private Panel panMain2;

	private Panel panel2;

	private Panel panel1;

	public FormDepositCharge()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		components = null;
		initTime = DateTime.Now;
		m_ChargeRecord = null;
		m_Card = null;
		m_RentalType = null;
		BindingHelper.BindComboBox<EnumParkTypeSource>(bindParkType);
		bindParkType.SelectedIndex = -1;
		bindStartDate.Value = bindStartDate.MaxDate;
		bindExpireDate.Value = bindExpireDate.MaxDate;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labCardNumber.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labCardNumber");
		labExpireDate.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labExpireDate");
		labParkType.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labParkType");
		labRentalType.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labRentalType");
		labStartDate.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labStartDate");
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labTitle");
		labTotalCharge.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.labTotalCharge");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.btnCancel");
		btnOk.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.btnOk");
		btnOther.Text = LangManager.GetLangString("CarPark.Forms.FormRentalCharge.btnOther");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void bindCardNumber_TextChanged(object sender, EventArgs e)
	{
		try
		{
			GetRentalChargeArgs getRentalChargeArgs = new GetRentalChargeArgs();
			getRentalChargeArgs.CardNumber = bindCardNumber.Text;
			GetRentalChargeReturn rentalCharge = chargeContext.CommunicationChannel.GetRentalCharge(getRentalChargeArgs, out m_Card, out m_RentalType);
			chargeContext.CommunicationChannel.Disconnect();
			if (m_Card == null)
			{
				bindRentalType.Text = "";
				bindParkType.SelectedValue = 0;
				bindStartDate.Value = bindStartDate.MaxDate;
				bindExpireDate.Value = bindExpireDate.MaxDate;
				bindTotalCharge.Text = "";
				btnOk.Enabled = false;
				btnOther.Enabled = false;
			}
			else if (m_RentalType == null)
			{
				bindRentalType.Text = "";
				bindParkType.SelectedValue = 0;
				bindStartDate.Value = bindStartDate.MaxDate;
				bindExpireDate.Value = bindExpireDate.MaxDate;
				bindTotalCharge.Text = "";
				btnOk.Enabled = false;
				btnOther.Enabled = false;
			}
			else
			{
				btnOk.Enabled = true;
				btnOther.Enabled = true;
				bindRentalType.Text = m_RentalType.RentalName;
				bindParkType.SelectedValue = m_RentalType.ParkTypeID;
				bindStartDate.Value = m_Card.StartDate;
				bindExpireDate.Value = ((!m_Card.ExpireDate.HasValue) ? DateTime.Now : Convert.ToDateTime(m_Card.ExpireDate));
				CalcAmount();
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
		}
		catch (Exception message)
		{
			bindRentalType.Text = "";
			bindTotalCharge.Text = "0.0";
			bindParkType.SelectedValue = 1;
			bindStartDate.Value = bindStartDate.MaxDate;
			bindExpireDate.Value = bindExpireDate.MaxDate;
			Logger.Error(message);
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
			m_ChargeRecord.BillType = 6;
			m_ChargeRecord.CardCode = m_Card.CardCode;
			m_ChargeRecord.ChargeTime = initTime;
			m_ChargeRecord.ParkTypeID = m_RentalType.ParkTypeID;
			m_ChargeRecord.ShiftID = DataBuffer.CurrentShiftRecord.ShiftID;
			m_ChargeRecord.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			m_ChargeRecord.FromStation = Settings.Default.OnlyID;
			decimal deposit = m_RentalType.Deposit;
			bindTotalCharge.Text = deposit.ToString("f2");
			m_ChargeRecord.TotalCharge = deposit;
		}
		catch (Exception message)
		{
			m_ChargeRecord = null;
			Logger.Error(message);
		}
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		if (m_Card == null)
		{
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.Show1"));
			return;
		}
		decimal num = ((!m_Card.Deposit.HasValue) ? 0m : Convert.ToDecimal(m_Card.Deposit));
		if (num > 0m)
		{
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.Show2"));
			return;
		}
		m_ChargeRecord.PayType = 0;
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
			charge.BillType = 6;
			charge.CardCode = m_Card.CardCode;
			SaveMPassChargeArgs saveMPassChargeArgs = new SaveMPassChargeArgs();
			saveMPassChargeArgs.CardCode = m_Card.CardCode;
			saveMPassChargeArgs.ExpireDate = bindExpireDate.Value;
			saveMPassChargeArgs.StartDate = bindStartDate.Value;
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
				Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.Show3"));
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
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.Show1"));
			return;
		}
		decimal num = ((!m_Card.Deposit.HasValue) ? 0m : Convert.ToDecimal(m_Card.Deposit));
		if (num > 0m)
		{
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.Show2"));
			return;
		}
		FormEpaySale formEpaySale = new FormEpaySale
		{
			ChargeRecord = m_ChargeRecord
		};
		using FormEpaySale formEpaySale2 = formEpaySale;
		if (formEpaySale2.ShowDialog() == DialogResult.OK)
		{
			formEpaySale2.ChargeRecord.PayType = (int)formEpaySale2.PayTypeFlag;
			SaveChargeRecord(formEpaySale2.ChargeRecord, formEpaySale2.MPass, formEpaySale2.BOC, formEpaySale2.BOC_N910, formEpaySale2.SPay);
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
		this.bindParkType = new System.Windows.Forms.ComboBox();
		this.btnOther = new System.Windows.Forms.Button();
		this.btnOk = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.bindTotalCharge = new System.Windows.Forms.TextBox();
		this.labTotalCharge = new System.Windows.Forms.Label();
		this.labParkType = new System.Windows.Forms.Label();
		this.labStartDate = new System.Windows.Forms.Label();
		this.labExpireDate = new System.Windows.Forms.Label();
		this.bindRentalType = new System.Windows.Forms.TextBox();
		this.labRentalType = new System.Windows.Forms.Label();
		this.bindCardNumber = new System.Windows.Forms.TextBox();
		this.labCardNumber = new System.Windows.Forms.Label();
		this.panMain2 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panMain2.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 30f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(593, 60);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "月租按金";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.bindExpireDate.CustomFormat = "yyyy-MM-dd";
		this.bindExpireDate.Enabled = false;
		this.bindExpireDate.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindExpireDate.Location = new System.Drawing.Point(272, 262);
		this.bindExpireDate.Name = "bindExpireDate";
		this.bindExpireDate.Size = new System.Drawing.Size(250, 51);
		this.bindExpireDate.TabIndex = 6;
		this.bindStartDate.CustomFormat = "yyyy-MM-dd";
		this.bindStartDate.Enabled = false;
		this.bindStartDate.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindStartDate.Location = new System.Drawing.Point(272, 181);
		this.bindStartDate.Name = "bindStartDate";
		this.bindStartDate.Size = new System.Drawing.Size(250, 51);
		this.bindStartDate.TabIndex = 6;
		this.bindParkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.bindParkType.Enabled = false;
		this.bindParkType.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindParkType.FormattingEnabled = true;
		this.bindParkType.Location = new System.Drawing.Point(272, 343);
		this.bindParkType.Name = "bindParkType";
		this.bindParkType.Size = new System.Drawing.Size(250, 51);
		this.bindParkType.TabIndex = 4;
		this.btnOther.Enabled = false;
		this.btnOther.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnOther.ForeColor = System.Drawing.Color.Navy;
		this.btnOther.Location = new System.Drawing.Point(172, 8);
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
		this.btnOk.Location = new System.Drawing.Point(317, 8);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(120, 48);
		this.btnOk.TabIndex = 3;
		this.btnOk.Text = "確認";
		this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOk.UseVisualStyleBackColor = true;
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(462, 8);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(120, 48);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.bindTotalCharge.Font = new System.Drawing.Font("微软雅黑", 60f);
		this.bindTotalCharge.Location = new System.Drawing.Point(272, 424);
		this.bindTotalCharge.Name = "bindTotalCharge";
		this.bindTotalCharge.ReadOnly = true;
		this.bindTotalCharge.Size = new System.Drawing.Size(250, 113);
		this.bindTotalCharge.TabIndex = 1;
		this.bindTotalCharge.TextChanged += new System.EventHandler(bindTotalCharge_TextChanged);
		this.labTotalCharge.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labTotalCharge.ForeColor = System.Drawing.Color.Navy;
		this.labTotalCharge.Location = new System.Drawing.Point(66, 424);
		this.labTotalCharge.Name = "labTotalCharge";
		this.labTotalCharge.Size = new System.Drawing.Size(200, 113);
		this.labTotalCharge.TabIndex = 0;
		this.labTotalCharge.Text = "金額";
		this.labTotalCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkType.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labParkType.ForeColor = System.Drawing.Color.Navy;
		this.labParkType.Location = new System.Drawing.Point(66, 343);
		this.labParkType.Name = "labParkType";
		this.labParkType.Size = new System.Drawing.Size(200, 51);
		this.labParkType.TabIndex = 0;
		this.labParkType.Text = "車型";
		this.labParkType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labStartDate.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labStartDate.ForeColor = System.Drawing.Color.Navy;
		this.labStartDate.Location = new System.Drawing.Point(66, 181);
		this.labStartDate.Name = "labStartDate";
		this.labStartDate.Size = new System.Drawing.Size(200, 51);
		this.labStartDate.TabIndex = 0;
		this.labStartDate.Text = "開始日期";
		this.labStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labExpireDate.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labExpireDate.ForeColor = System.Drawing.Color.Navy;
		this.labExpireDate.Location = new System.Drawing.Point(66, 262);
		this.labExpireDate.Name = "labExpireDate";
		this.labExpireDate.Size = new System.Drawing.Size(200, 51);
		this.labExpireDate.TabIndex = 0;
		this.labExpireDate.Text = "失效日期";
		this.labExpireDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindRentalType.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindRentalType.Location = new System.Drawing.Point(272, 100);
		this.bindRentalType.Name = "bindRentalType";
		this.bindRentalType.ReadOnly = true;
		this.bindRentalType.Size = new System.Drawing.Size(250, 51);
		this.bindRentalType.TabIndex = 1;
		this.labRentalType.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labRentalType.ForeColor = System.Drawing.Color.Navy;
		this.labRentalType.Location = new System.Drawing.Point(66, 100);
		this.labRentalType.Name = "labRentalType";
		this.labRentalType.Size = new System.Drawing.Size(200, 51);
		this.labRentalType.TabIndex = 0;
		this.labRentalType.Text = "租賃方式";
		this.labRentalType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindCardNumber.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.bindCardNumber.Location = new System.Drawing.Point(272, 19);
		this.bindCardNumber.Name = "bindCardNumber";
		this.bindCardNumber.Size = new System.Drawing.Size(250, 51);
		this.bindCardNumber.TabIndex = 1;
		this.bindCardNumber.TextChanged += new System.EventHandler(bindCardNumber_TextChanged);
		this.labCardNumber.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.labCardNumber.ForeColor = System.Drawing.Color.Navy;
		this.labCardNumber.Location = new System.Drawing.Point(66, 19);
		this.labCardNumber.Name = "labCardNumber";
		this.labCardNumber.Size = new System.Drawing.Size(200, 51);
		this.labCardNumber.TabIndex = 0;
		this.labCardNumber.Text = "卡號";
		this.labCardNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panMain2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panMain2.Controls.Add(this.panel2);
		this.panMain2.Controls.Add(this.panel1);
		this.panMain2.Controls.Add(this.labTitle);
		this.panMain2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panMain2.Location = new System.Drawing.Point(0, 0);
		this.panMain2.Name = "panMain2";
		this.panMain2.Size = new System.Drawing.Size(595, 700);
		this.panMain2.TabIndex = 1;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.bindExpireDate);
		this.panel2.Controls.Add(this.labCardNumber);
		this.panel2.Controls.Add(this.bindStartDate);
		this.panel2.Controls.Add(this.bindCardNumber);
		this.panel2.Controls.Add(this.bindParkType);
		this.panel2.Controls.Add(this.labRentalType);
		this.panel2.Controls.Add(this.bindRentalType);
		this.panel2.Controls.Add(this.labExpireDate);
		this.panel2.Controls.Add(this.labStartDate);
		this.panel2.Controls.Add(this.bindTotalCharge);
		this.panel2.Controls.Add(this.labParkType);
		this.panel2.Controls.Add(this.labTotalCharge);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 60);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(593, 574);
		this.panel2.TabIndex = 3;
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Controls.Add(this.btnOk);
		this.panel1.Controls.Add(this.btnOther);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 634);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(593, 64);
		this.panel1.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(13f, 28f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(595, 700);
		base.Controls.Add(this.panMain2);
		this.Font = new System.Drawing.Font("微软雅黑", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(7);
		base.Name = "FormDepositCharge";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormRentalCharge";
		this.panMain2.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
