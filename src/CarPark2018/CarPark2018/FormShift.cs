using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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

namespace CarPark2018;

public class FormShift : Form
{
	private ILog Logger;

	private ShiftRecord m_ShiftRecord = new ShiftRecord();

	private GetCurrShiftRecordArgs StaffReacrdArg = new GetCurrShiftRecordArgs();

	private List<ChargeRecord> list = new List<ChargeRecord>();

	private GetCurrChargeRecordArgs ChargeRecordArgs = new GetCurrChargeRecordArgs();

	private BindingSource bs;

	private ChargeContext chargeContext = new ChargeContext();

	private IContainer components;

	private Label labTitle;

	private Panel panBottom;

	private Panel panMain;

	private Button btnPrint;

	private Button btnSave;

	private Button btnClose;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private TextBox bindEndBalance;

	private TextBox bindStartBalance;

	private Label labCloseBalance;

	private Label labOpenBalance;

	private TextBox bindTotalCharge;

	private TextBox bindTotalRentalDeposit;

	private Label labAllAmt;

	private Label labDispostAmt;

	private TextBox bindTotalRentalCharge;

	private TextBox bindTotalTimeCharge;

	private Label labRentalChargeAmt;

	private Label labTimeChargeAmt;

	private TextBox bindEndStaffCode;

	private TextBox bindStartStaffCode;

	private Label labEndStaffCode;

	private Label labStartStaff;

	private TextBox bindEndTime;

	private TextBox bindStartTime;

	private Label labEndTime;

	private Label labStartTime;

	private TextBox bindShiftID;

	private Label labShiftID;

	private Panel panel1;

	private Label labParkType;

	private TextBox txtTotalCharge;

	private Label labTotalCharge;

	private ComboBox comboBillType;

	private ComboBox comboParkType;

	private Label labBillType;

	private DataGridView dataMain;

	private TextBox bindMPassDecalCharge;

	private TextBox bindCashCharge;

	private Label labCashCharge;

	private Label labMPassDecalCharge;

	private Panel panFill;

	private ComboBox comboPayType;

	private Label labPayType;

	public FormShift()
	{
		InitializeComponent();
		bs = new BindingSource();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		StaffReacrdArg.PayStationName = Settings.Default.OnlyID;
		ChargeRecordArgs.PayStationName = Settings.Default.OnlyID;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labTitle.Text = LangManager.GetLangString("CarPark.FormShift.labTitle");
		labAllAmt.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labAllAmt");
		labCashCharge.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labCashCharge");
		labCloseBalance.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labCloseBalance");
		labDispostAmt.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labDispostAmt");
		labEndStaffCode.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labEndStaffCode");
		labEndTime.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labEndTime");
		labMPassDecalCharge.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labMPassDecalCharge");
		labOpenBalance.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labOpenBalance");
		labParkType.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labParkType");
		labRentalChargeAmt.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labRentalChargeAmt");
		labShiftID.Text = LangManager.GetLangString("CarPark.UserControls.SysAnilize.UCChargeRecord.labelX6");
		labStartStaff.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labStartStaff");
		labStartTime.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labStartTime");
		labTimeChargeAmt.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labTimeChargeAmt");
		labTotalCharge.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labTotalCharge");
		btnClose.Text = LangManager.GetLangString("CarPark.FormShift.btnClose");
		btnPrint.Text = LangManager.GetLangString("CarPark.FormShift.btnPrint");
		btnSave.Text = LangManager.GetLangString("CarPark.FormShift.btnSave");
		tabPage1.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.tabBrif");
		tabPage2.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.tabDetail");
		labBillType.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labBillType");
		labPayType.Text = LangManager.GetLangString("CarPark.UserControls.UCShiftInfo.labPayType");
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void FormShift_Load(object sender, EventArgs e)
	{
		try
		{
			BindingHelper.BindComboBox<EnumBillTypeSource>(comboBillType, "====");
			BindingHelper.BindComboBox<EnumParkTypeSource>(comboParkType, "====");
			BindingHelper.BindComboBox<EnumPayTypeSource>(comboPayType, "====");
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		try
		{
			SetDGVStyle(dataMain);
			InitShiftInfo();
			InitDetailedInfo();
			InitBinding();
			InitStaffOperate();
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception message2)
		{
			Logger.Error(message2);
		}
	}

	private void InitShiftInfo()
	{
		try
		{
			chargeContext.CommunicationChannel.GetCurrShiftRecord(StaffReacrdArg, out m_ShiftRecord);
			chargeContext.CommunicationChannel.Disconnect();
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
		try
		{
			bindShiftID.Text = m_ShiftRecord.ShiftID.ToString();
			bindStartStaffCode.Text = m_ShiftRecord.StartStaffCode;
			bindEndStaffCode.Text = m_ShiftRecord.EndStaffCode;
			bindStartTime.Text = m_ShiftRecord.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
			bindEndTime.Text = ((!m_ShiftRecord.EndTime.HasValue) ? "" : Convert.ToDateTime(m_ShiftRecord.EndTime).ToString("yyyy-MM-dd HH:mm:ss"));
			bindTotalCharge.Text = m_ShiftRecord.TotalCharge.ToString();
			bindTotalRentalCharge.Text = m_ShiftRecord.TotalRentalCharge.ToString();
			TextBox textBox = bindTotalRentalDeposit;
			decimal totalRentalDeposit = m_ShiftRecord.TotalRentalDeposit;
			decimal? mPassCharge = m_ShiftRecord.MPassCharge;
			textBox.Text = ((decimal?)totalRentalDeposit + mPassCharge - m_ShiftRecord.MPassPosCharge).ToString();
			decimal valueOrDefault = m_ShiftRecord.MPassDecalCharge.GetValueOrDefault();
			decimal valueOrDefault2 = m_ShiftRecord.MPassCharge.GetValueOrDefault();
			decimal valueOrDefault3 = m_ShiftRecord.MPassPosCharge.GetValueOrDefault();
			decimal num = m_ShiftRecord.TotalCharge - m_ShiftRecord.TotalRentalCharge - valueOrDefault - valueOrDefault2 + valueOrDefault3;
			bindTotalTimeCharge.Text = num.ToString();
			bindStartBalance.Text = m_ShiftRecord.StartBalance.ToString();
			bindEndBalance.Text = m_ShiftRecord.EndBalance.ToString();
			bindMPassDecalCharge.Text = m_ShiftRecord.MPassDecalCharge.GetValueOrDefault().ToString();
			bindCashCharge.Text = m_ShiftRecord.CashCharge.ToString();
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Console.WriteLine(ex2.Message);
		}
	}

	private void InitStaffOperate()
	{
	}

	private void InitDetailedInfo()
	{
		try
		{
			chargeContext.CommunicationChannel.GetCurrChargeRecord(ChargeRecordArgs, out list);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
		try
		{
			bs.Clear();
			decimal num = default(decimal);
			for (int num2 = list.Count() - 1; num2 >= 0; num2--)
			{
				num += list[num2].TotalCharge;
				bs.Add(list[num2]);
			}
			txtTotalCharge.Text = num.ToString();
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Console.WriteLine(ex2.Message);
		}
	}

	private void InitBinding()
	{
		try
		{
			BindingHelper.BindDataGridView<ChargeRecord>(bs, dataMain, new DataGridBindingAttr[6]
			{
				new DataGridBindingAttr(PropertyHelper<ChargeRecord>.GetProperty((ChargeRecord m) => m.CardCode), 200),
				new DataGridBindingAttr(PropertyHelper<ChargeRecord>.GetProperty((ChargeRecord m) => m.BillType), 200),
				new DataGridBindingAttr(PropertyHelper<ChargeRecord>.GetProperty((ChargeRecord m) => m.PayType), 130),
				new DataGridBindingAttr(PropertyHelper<ChargeRecord>.GetProperty((ChargeRecord m) => m.ParkTypeID), 130),
				new DataGridBindingAttr(PropertyHelper<ChargeRecord>.GetProperty((ChargeRecord m) => m.TotalCharge), 130),
				new DataGridBindingAttr(PropertyHelper<ChargeRecord>.GetProperty((ChargeRecord m) => m.ChargeTime), 230)
			});
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			EndCurrShiftRecordArgs endCurrShiftRecordArgs = new EndCurrShiftRecordArgs();
			endCurrShiftRecordArgs.PayStationName = Settings.Default.OnlyID;
			endCurrShiftRecordArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			EndCurrShiftRecordReturn endCurrShiftRecordReturn = chargeContext.CommunicationChannel.EndCurrShiftRecord(endCurrShiftRecordArgs, out m_ShiftRecord);
			chargeContext.CommunicationChannel.Disconnect();
			if (endCurrShiftRecordReturn.ISOK)
			{
				PrintUtils.PrintShift(m_ShiftRecord);
				try
				{
					DeviceManager.FeeCenterModule.OpenCash();
				}
				catch (Exception message)
				{
					Logger.Error(message);
				}
				Close();
			}
			else
			{
				Global.ShowMessage(endCurrShiftRecordReturn.ErrCode);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Console.WriteLine(ex2.Message);
			Global.ShowMessage(ex2.Message);
		}
	}

	private void SetDGVStyle(DataGridView dgv)
	{
		DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
		dgv.AllowUserToAddRows = false;
		dgv.AllowUserToDeleteRows = false;
		dgv.AllowUserToResizeColumns = false;
		dgv.AllowUserToResizeRows = false;
		dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = SystemColors.Window;
		dataGridViewCellStyle.Font = new Font("Times New Roman", 22.5f);
		dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
		dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = SystemColors.ControlText;
		dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;
		dgv.DefaultCellStyle = dataGridViewCellStyle;
		dgv.GridColor = Color.FromArgb(208, 215, 229);
		dgv.ReadOnly = true;
		dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
		dgv.RowTemplate.Height = 50;
		dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		PrintUtils.PrintShift(m_ShiftRecord);
	}

	private void dataMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	private void Combobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (m_ShiftRecord == null)
		{
			return;
		}
		List<ChargeRecord> list = new List<ChargeRecord>();
		List<ChargeRecord> m_ListRecord = new List<ChargeRecord>();
		int num = -1;
		if (Convert.ToInt32(comboParkType.SelectedIndex) > 0)
		{
			num = Convert.ToInt32(comboParkType.SelectedValue);
		}
		if (num <= 0)
		{
			this.list.ForEach(delegate(ChargeRecord i)
			{
				m_ListRecord.Add(i);
			});
		}
		else
		{
			foreach (ChargeRecord item in this.list)
			{
				if (item.ParkTypeID == num)
				{
					list.Add(item);
				}
			}
			list.ForEach(delegate(ChargeRecord i)
			{
				m_ListRecord.Add(i);
			});
			list.Clear();
		}
		int num2 = -1;
		if (Convert.ToInt32(comboBillType.SelectedIndex) > 0)
		{
			num2 = Convert.ToInt32(comboBillType.SelectedValue);
		}
		if (num2 >= 0)
		{
			foreach (ChargeRecord item2 in m_ListRecord)
			{
				if (item2.BillType == num2)
				{
					list.Add(item2);
				}
			}
			m_ListRecord.Clear();
			list.ForEach(delegate(ChargeRecord i)
			{
				m_ListRecord.Add(i);
			});
			list.Clear();
		}
		int num3 = -1;
		if (Convert.ToInt32(comboPayType.SelectedIndex) > 0)
		{
			num3 = Convert.ToInt32(comboPayType.SelectedValue);
		}
		if (num3 >= 0)
		{
			foreach (ChargeRecord item3 in m_ListRecord)
			{
				if (item3.PayType == num3)
				{
					list.Add(item3);
				}
			}
			m_ListRecord.Clear();
			list.ForEach(delegate(ChargeRecord i)
			{
				m_ListRecord.Add(i);
			});
			list.Clear();
		}
		bs.Clear();
		decimal num4 = default(decimal);
		for (int num5 = m_ListRecord.Count() - 1; num5 >= 0; num5--)
		{
			num4 += m_ListRecord[num5].TotalCharge;
			bs.Add(m_ListRecord[num5]);
		}
		txtTotalCharge.Text = num4.ToString();
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
		this.panBottom = new System.Windows.Forms.Panel();
		this.btnPrint = new System.Windows.Forms.Button();
		this.btnSave = new System.Windows.Forms.Button();
		this.btnClose = new System.Windows.Forms.Button();
		this.panMain = new System.Windows.Forms.Panel();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.bindEndBalance = new System.Windows.Forms.TextBox();
		this.bindStartBalance = new System.Windows.Forms.TextBox();
		this.labCloseBalance = new System.Windows.Forms.Label();
		this.labOpenBalance = new System.Windows.Forms.Label();
		this.bindTotalCharge = new System.Windows.Forms.TextBox();
		this.bindTotalRentalDeposit = new System.Windows.Forms.TextBox();
		this.labAllAmt = new System.Windows.Forms.Label();
		this.labDispostAmt = new System.Windows.Forms.Label();
		this.bindTotalRentalCharge = new System.Windows.Forms.TextBox();
		this.bindMPassDecalCharge = new System.Windows.Forms.TextBox();
		this.bindCashCharge = new System.Windows.Forms.TextBox();
		this.bindTotalTimeCharge = new System.Windows.Forms.TextBox();
		this.labRentalChargeAmt = new System.Windows.Forms.Label();
		this.labCashCharge = new System.Windows.Forms.Label();
		this.labMPassDecalCharge = new System.Windows.Forms.Label();
		this.labTimeChargeAmt = new System.Windows.Forms.Label();
		this.bindEndStaffCode = new System.Windows.Forms.TextBox();
		this.bindStartStaffCode = new System.Windows.Forms.TextBox();
		this.labEndStaffCode = new System.Windows.Forms.Label();
		this.labStartStaff = new System.Windows.Forms.Label();
		this.bindEndTime = new System.Windows.Forms.TextBox();
		this.bindStartTime = new System.Windows.Forms.TextBox();
		this.labEndTime = new System.Windows.Forms.Label();
		this.labStartTime = new System.Windows.Forms.Label();
		this.bindShiftID = new System.Windows.Forms.TextBox();
		this.labShiftID = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.dataMain = new System.Windows.Forms.DataGridView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.labParkType = new System.Windows.Forms.Label();
		this.txtTotalCharge = new System.Windows.Forms.TextBox();
		this.labTotalCharge = new System.Windows.Forms.Label();
		this.comboPayType = new System.Windows.Forms.ComboBox();
		this.comboBillType = new System.Windows.Forms.ComboBox();
		this.labPayType = new System.Windows.Forms.Label();
		this.comboParkType = new System.Windows.Forms.ComboBox();
		this.labBillType = new System.Windows.Forms.Label();
		this.panFill = new System.Windows.Forms.Panel();
		this.panBottom.SuspendLayout();
		this.panMain.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataMain).BeginInit();
		this.panel1.SuspendLayout();
		this.panFill.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 30f, System.Drawing.FontStyle.Bold);
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(1198, 83);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "轉更結算";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panBottom.Controls.Add(this.btnPrint);
		this.panBottom.Controls.Add(this.btnSave);
		this.panBottom.Controls.Add(this.btnClose);
		this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panBottom.Location = new System.Drawing.Point(0, 598);
		this.panBottom.Name = "panBottom";
		this.panBottom.Size = new System.Drawing.Size(1198, 100);
		this.panBottom.TabIndex = 1;
		this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.btnPrint.Location = new System.Drawing.Point(765, 6);
		this.btnPrint.Name = "btnPrint";
		this.btnPrint.Size = new System.Drawing.Size(137, 82);
		this.btnPrint.TabIndex = 0;
		this.btnPrint.Text = "列印";
		this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnPrint.UseVisualStyleBackColor = true;
		this.btnPrint.Click += new System.EventHandler(btnPrint_Click);
		this.btnSave.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.btnSave.Location = new System.Drawing.Point(908, 6);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(137, 82);
		this.btnSave.TabIndex = 0;
		this.btnSave.Text = "轉更";
		this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.btnClose.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.btnClose.Location = new System.Drawing.Point(1051, 6);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(137, 82);
		this.btnClose.TabIndex = 0;
		this.btnClose.Text = "關閉";
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.panMain.Controls.Add(this.tabControl1);
		this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panMain.Location = new System.Drawing.Point(0, 83);
		this.panMain.Name = "panMain";
		this.panMain.Size = new System.Drawing.Size(1198, 515);
		this.panMain.TabIndex = 2;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.tabControl1.Location = new System.Drawing.Point(0, 0);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(1198, 515);
		this.tabControl1.TabIndex = 0;
		this.tabPage1.Controls.Add(this.bindEndBalance);
		this.tabPage1.Controls.Add(this.bindStartBalance);
		this.tabPage1.Controls.Add(this.labCloseBalance);
		this.tabPage1.Controls.Add(this.labOpenBalance);
		this.tabPage1.Controls.Add(this.bindTotalCharge);
		this.tabPage1.Controls.Add(this.bindTotalRentalDeposit);
		this.tabPage1.Controls.Add(this.labAllAmt);
		this.tabPage1.Controls.Add(this.labDispostAmt);
		this.tabPage1.Controls.Add(this.bindTotalRentalCharge);
		this.tabPage1.Controls.Add(this.bindMPassDecalCharge);
		this.tabPage1.Controls.Add(this.bindCashCharge);
		this.tabPage1.Controls.Add(this.bindTotalTimeCharge);
		this.tabPage1.Controls.Add(this.labRentalChargeAmt);
		this.tabPage1.Controls.Add(this.labCashCharge);
		this.tabPage1.Controls.Add(this.labMPassDecalCharge);
		this.tabPage1.Controls.Add(this.labTimeChargeAmt);
		this.tabPage1.Controls.Add(this.bindEndStaffCode);
		this.tabPage1.Controls.Add(this.bindStartStaffCode);
		this.tabPage1.Controls.Add(this.labEndStaffCode);
		this.tabPage1.Controls.Add(this.labStartStaff);
		this.tabPage1.Controls.Add(this.bindEndTime);
		this.tabPage1.Controls.Add(this.bindStartTime);
		this.tabPage1.Controls.Add(this.labEndTime);
		this.tabPage1.Controls.Add(this.labStartTime);
		this.tabPage1.Controls.Add(this.bindShiftID);
		this.tabPage1.Controls.Add(this.labShiftID);
		this.tabPage1.Location = new System.Drawing.Point(4, 44);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(1190, 467);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "更次資訊";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.bindEndBalance.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindEndBalance.Location = new System.Drawing.Point(1157, 7);
		this.bindEndBalance.Margin = new System.Windows.Forms.Padding(4);
		this.bindEndBalance.Name = "bindEndBalance";
		this.bindEndBalance.Size = new System.Drawing.Size(26, 43);
		this.bindEndBalance.TabIndex = 17;
		this.bindEndBalance.Visible = false;
		this.bindStartBalance.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindStartBalance.Location = new System.Drawing.Point(1157, 58);
		this.bindStartBalance.Margin = new System.Windows.Forms.Padding(4);
		this.bindStartBalance.Name = "bindStartBalance";
		this.bindStartBalance.Size = new System.Drawing.Size(26, 43);
		this.bindStartBalance.TabIndex = 15;
		this.bindStartBalance.Visible = false;
		this.labCloseBalance.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labCloseBalance.Location = new System.Drawing.Point(1108, 7);
		this.labCloseBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labCloseBalance.Name = "labCloseBalance";
		this.labCloseBalance.Size = new System.Drawing.Size(41, 43);
		this.labCloseBalance.TabIndex = 10;
		this.labCloseBalance.Text = "結餘";
		this.labCloseBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labCloseBalance.Visible = false;
		this.labOpenBalance.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labOpenBalance.Location = new System.Drawing.Point(1108, 58);
		this.labOpenBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labOpenBalance.Name = "labOpenBalance";
		this.labOpenBalance.Size = new System.Drawing.Size(41, 43);
		this.labOpenBalance.TabIndex = 9;
		this.labOpenBalance.Text = "開更結餘";
		this.labOpenBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labOpenBalance.Visible = false;
		this.bindTotalCharge.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindTotalCharge.Location = new System.Drawing.Point(824, 363);
		this.bindTotalCharge.Margin = new System.Windows.Forms.Padding(4);
		this.bindTotalCharge.Name = "bindTotalCharge";
		this.bindTotalCharge.ReadOnly = true;
		this.bindTotalCharge.Size = new System.Drawing.Size(217, 43);
		this.bindTotalCharge.TabIndex = 19;
		this.bindTotalRentalDeposit.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindTotalRentalDeposit.Location = new System.Drawing.Point(375, 212);
		this.bindTotalRentalDeposit.Margin = new System.Windows.Forms.Padding(4);
		this.bindTotalRentalDeposit.Name = "bindTotalRentalDeposit";
		this.bindTotalRentalDeposit.ReadOnly = true;
		this.bindTotalRentalDeposit.Size = new System.Drawing.Size(217, 43);
		this.bindTotalRentalDeposit.TabIndex = 20;
		this.labAllAmt.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labAllAmt.Location = new System.Drawing.Point(599, 363);
		this.labAllAmt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAllAmt.Name = "labAllAmt";
		this.labAllAmt.Size = new System.Drawing.Size(217, 43);
		this.labAllAmt.TabIndex = 12;
		this.labAllAmt.Text = "總收入";
		this.labAllAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labDispostAmt.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labDispostAmt.Location = new System.Drawing.Point(150, 212);
		this.labDispostAmt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDispostAmt.Name = "labDispostAmt";
		this.labDispostAmt.Size = new System.Drawing.Size(217, 43);
		this.labDispostAmt.TabIndex = 4;
		this.labDispostAmt.Text = "臨時收入";
		this.labDispostAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindTotalRentalCharge.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindTotalRentalCharge.Location = new System.Drawing.Point(824, 213);
		this.bindTotalRentalCharge.Margin = new System.Windows.Forms.Padding(4);
		this.bindTotalRentalCharge.Name = "bindTotalRentalCharge";
		this.bindTotalRentalCharge.ReadOnly = true;
		this.bindTotalRentalCharge.Size = new System.Drawing.Size(217, 43);
		this.bindTotalRentalCharge.TabIndex = 21;
		this.bindMPassDecalCharge.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindMPassDecalCharge.Location = new System.Drawing.Point(825, 288);
		this.bindMPassDecalCharge.Margin = new System.Windows.Forms.Padding(4);
		this.bindMPassDecalCharge.Name = "bindMPassDecalCharge";
		this.bindMPassDecalCharge.ReadOnly = true;
		this.bindMPassDecalCharge.Size = new System.Drawing.Size(217, 43);
		this.bindMPassDecalCharge.TabIndex = 22;
		this.bindCashCharge.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindCashCharge.Location = new System.Drawing.Point(375, 362);
		this.bindCashCharge.Margin = new System.Windows.Forms.Padding(4);
		this.bindCashCharge.Name = "bindCashCharge";
		this.bindCashCharge.ReadOnly = true;
		this.bindCashCharge.Size = new System.Drawing.Size(217, 43);
		this.bindCashCharge.TabIndex = 22;
		this.bindTotalTimeCharge.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindTotalTimeCharge.Location = new System.Drawing.Point(375, 287);
		this.bindTotalTimeCharge.Margin = new System.Windows.Forms.Padding(4);
		this.bindTotalTimeCharge.Name = "bindTotalTimeCharge";
		this.bindTotalTimeCharge.ReadOnly = true;
		this.bindTotalTimeCharge.Size = new System.Drawing.Size(217, 43);
		this.bindTotalTimeCharge.TabIndex = 22;
		this.labRentalChargeAmt.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labRentalChargeAmt.Location = new System.Drawing.Point(599, 213);
		this.labRentalChargeAmt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labRentalChargeAmt.Name = "labRentalChargeAmt";
		this.labRentalChargeAmt.Size = new System.Drawing.Size(217, 43);
		this.labRentalChargeAmt.TabIndex = 11;
		this.labRentalChargeAmt.Text = "月租收入";
		this.labRentalChargeAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labCashCharge.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labCashCharge.Location = new System.Drawing.Point(150, 362);
		this.labCashCharge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labCashCharge.Name = "labCashCharge";
		this.labCashCharge.Size = new System.Drawing.Size(217, 43);
		this.labCashCharge.TabIndex = 2;
		this.labCashCharge.Text = "現金收入";
		this.labCashCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labMPassDecalCharge.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labMPassDecalCharge.Location = new System.Drawing.Point(600, 288);
		this.labMPassDecalCharge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labMPassDecalCharge.Name = "labMPassDecalCharge";
		this.labMPassDecalCharge.Size = new System.Drawing.Size(217, 43);
		this.labMPassDecalCharge.TabIndex = 2;
		this.labMPassDecalCharge.Text = "澳門通增值";
		this.labMPassDecalCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTimeChargeAmt.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labTimeChargeAmt.Location = new System.Drawing.Point(150, 287);
		this.labTimeChargeAmt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labTimeChargeAmt.Name = "labTimeChargeAmt";
		this.labTimeChargeAmt.Size = new System.Drawing.Size(217, 43);
		this.labTimeChargeAmt.TabIndex = 2;
		this.labTimeChargeAmt.Text = "時租收入";
		this.labTimeChargeAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindEndStaffCode.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindEndStaffCode.Location = new System.Drawing.Point(1074, 58);
		this.bindEndStaffCode.Margin = new System.Windows.Forms.Padding(4);
		this.bindEndStaffCode.Name = "bindEndStaffCode";
		this.bindEndStaffCode.Size = new System.Drawing.Size(26, 43);
		this.bindEndStaffCode.TabIndex = 18;
		this.bindEndStaffCode.Visible = false;
		this.bindStartStaffCode.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindStartStaffCode.Location = new System.Drawing.Point(825, 137);
		this.bindStartStaffCode.Margin = new System.Windows.Forms.Padding(4);
		this.bindStartStaffCode.Name = "bindStartStaffCode";
		this.bindStartStaffCode.ReadOnly = true;
		this.bindStartStaffCode.Size = new System.Drawing.Size(217, 43);
		this.bindStartStaffCode.TabIndex = 23;
		this.labEndStaffCode.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labEndStaffCode.Location = new System.Drawing.Point(1025, 58);
		this.labEndStaffCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labEndStaffCode.Name = "labEndStaffCode";
		this.labEndStaffCode.Size = new System.Drawing.Size(41, 43);
		this.labEndStaffCode.TabIndex = 5;
		this.labEndStaffCode.Text = "結束員工";
		this.labEndStaffCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labEndStaffCode.Visible = false;
		this.labStartStaff.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labStartStaff.Location = new System.Drawing.Point(600, 137);
		this.labStartStaff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labStartStaff.Name = "labStartStaff";
		this.labStartStaff.Size = new System.Drawing.Size(217, 43);
		this.labStartStaff.TabIndex = 8;
		this.labStartStaff.Text = "開始員工";
		this.labStartStaff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindEndTime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindEndTime.Location = new System.Drawing.Point(1074, 7);
		this.bindEndTime.Margin = new System.Windows.Forms.Padding(4);
		this.bindEndTime.Name = "bindEndTime";
		this.bindEndTime.Size = new System.Drawing.Size(26, 43);
		this.bindEndTime.TabIndex = 14;
		this.bindEndTime.Visible = false;
		this.bindStartTime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindStartTime.Location = new System.Drawing.Point(375, 137);
		this.bindStartTime.Margin = new System.Windows.Forms.Padding(4);
		this.bindStartTime.Name = "bindStartTime";
		this.bindStartTime.ReadOnly = true;
		this.bindStartTime.Size = new System.Drawing.Size(217, 43);
		this.bindStartTime.TabIndex = 13;
		this.labEndTime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labEndTime.Location = new System.Drawing.Point(1025, 7);
		this.labEndTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labEndTime.Name = "labEndTime";
		this.labEndTime.Size = new System.Drawing.Size(41, 43);
		this.labEndTime.TabIndex = 7;
		this.labEndTime.Text = "結束時間";
		this.labEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labEndTime.Visible = false;
		this.labStartTime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labStartTime.Location = new System.Drawing.Point(150, 137);
		this.labStartTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labStartTime.Name = "labStartTime";
		this.labStartTime.Size = new System.Drawing.Size(217, 43);
		this.labStartTime.TabIndex = 6;
		this.labStartTime.Text = "開始時間";
		this.labStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindShiftID.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindShiftID.Location = new System.Drawing.Point(375, 62);
		this.bindShiftID.Margin = new System.Windows.Forms.Padding(4);
		this.bindShiftID.Name = "bindShiftID";
		this.bindShiftID.ReadOnly = true;
		this.bindShiftID.Size = new System.Drawing.Size(217, 43);
		this.bindShiftID.TabIndex = 16;
		this.labShiftID.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labShiftID.Location = new System.Drawing.Point(150, 62);
		this.labShiftID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labShiftID.Name = "labShiftID";
		this.labShiftID.Size = new System.Drawing.Size(217, 43);
		this.labShiftID.TabIndex = 3;
		this.labShiftID.Text = "更次編號";
		this.labShiftID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tabPage2.Controls.Add(this.dataMain);
		this.tabPage2.Controls.Add(this.panel1);
		this.tabPage2.Location = new System.Drawing.Point(4, 44);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(1190, 467);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "詳細資訊";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.dataMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataMain.Location = new System.Drawing.Point(3, 3);
		this.dataMain.MultiSelect = false;
		this.dataMain.Name = "dataMain";
		this.dataMain.RowTemplate.Height = 24;
		this.dataMain.Size = new System.Drawing.Size(809, 461);
		this.dataMain.TabIndex = 46;
		this.dataMain.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataMain_DataError);
		this.panel1.Controls.Add(this.labParkType);
		this.panel1.Controls.Add(this.txtTotalCharge);
		this.panel1.Controls.Add(this.labTotalCharge);
		this.panel1.Controls.Add(this.comboPayType);
		this.panel1.Controls.Add(this.comboBillType);
		this.panel1.Controls.Add(this.labPayType);
		this.panel1.Controls.Add(this.comboParkType);
		this.panel1.Controls.Add(this.labBillType);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(812, 3);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(375, 461);
		this.panel1.TabIndex = 6;
		this.labParkType.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labParkType.Location = new System.Drawing.Point(6, 24);
		this.labParkType.Name = "labParkType";
		this.labParkType.Size = new System.Drawing.Size(132, 35);
		this.labParkType.TabIndex = 1;
		this.labParkType.Text = "車型";
		this.labParkType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtTotalCharge.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.txtTotalCharge.Location = new System.Drawing.Point(144, 240);
		this.txtTotalCharge.Name = "txtTotalCharge";
		this.txtTotalCharge.ReadOnly = true;
		this.txtTotalCharge.Size = new System.Drawing.Size(220, 43);
		this.txtTotalCharge.TabIndex = 3;
		this.labTotalCharge.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labTotalCharge.Location = new System.Drawing.Point(3, 243);
		this.labTotalCharge.Name = "labTotalCharge";
		this.labTotalCharge.Size = new System.Drawing.Size(132, 35);
		this.labTotalCharge.TabIndex = 1;
		this.labTotalCharge.Text = "合計";
		this.labTotalCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.comboPayType.DropDownHeight = 250;
		this.comboPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboPayType.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.comboPayType.FormattingEnabled = true;
		this.comboPayType.IntegralHeight = false;
		this.comboPayType.Location = new System.Drawing.Point(144, 167);
		this.comboPayType.Name = "comboPayType";
		this.comboPayType.Size = new System.Drawing.Size(220, 43);
		this.comboPayType.TabIndex = 2;
		this.comboPayType.SelectedIndexChanged += new System.EventHandler(Combobox_SelectedIndexChanged);
		this.comboBillType.DropDownHeight = 250;
		this.comboBillType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBillType.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.comboBillType.FormattingEnabled = true;
		this.comboBillType.IntegralHeight = false;
		this.comboBillType.Location = new System.Drawing.Point(144, 94);
		this.comboBillType.Name = "comboBillType";
		this.comboBillType.Size = new System.Drawing.Size(220, 43);
		this.comboBillType.TabIndex = 2;
		this.comboBillType.SelectedIndexChanged += new System.EventHandler(Combobox_SelectedIndexChanged);
		this.labPayType.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labPayType.Location = new System.Drawing.Point(6, 170);
		this.labPayType.Name = "labPayType";
		this.labPayType.Size = new System.Drawing.Size(132, 35);
		this.labPayType.TabIndex = 1;
		this.labPayType.Text = "支付方式";
		this.labPayType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.comboParkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkType.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.comboParkType.FormattingEnabled = true;
		this.comboParkType.Location = new System.Drawing.Point(144, 21);
		this.comboParkType.Name = "comboParkType";
		this.comboParkType.Size = new System.Drawing.Size(220, 43);
		this.comboParkType.TabIndex = 2;
		this.comboParkType.SelectedIndexChanged += new System.EventHandler(Combobox_SelectedIndexChanged);
		this.labBillType.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labBillType.Location = new System.Drawing.Point(6, 97);
		this.labBillType.Name = "labBillType";
		this.labBillType.Size = new System.Drawing.Size(132, 35);
		this.labBillType.TabIndex = 1;
		this.labBillType.Text = "業務類型";
		this.labBillType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.panMain);
		this.panFill.Controls.Add(this.panBottom);
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(1200, 700);
		this.panFill.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		base.ClientSize = new System.Drawing.Size(1200, 700);
		base.Controls.Add(this.panFill);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ForeColor = System.Drawing.Color.Navy;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "FormShift";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormShift";
		base.Load += new System.EventHandler(FormShift_Load);
		this.panBottom.ResumeLayout(false);
		this.panMain.ResumeLayout(false);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataMain).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panFill.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
