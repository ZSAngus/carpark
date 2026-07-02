using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using CarPark2018;
using CarPark2018.Properties;
using CarPark2018.UserControls;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark.UserControls.SysConfig;

public class UcTimeCharge : Form
{
	private ILog Logger;

	private BindingSource bs;

	private List<TimeCharge> m_TimeCharge = new List<TimeCharge>();

	private List<TimeChargeExt> m_TimeChargeEXT = new List<TimeChargeExt>();

	private bool isNew = false;

	private IContainer components = null;

	private Label labTitle;

	private Panel panBottom;

	private Panel panFill;

	private DataGridView dataMain;

	private UcChargeRange ucChargeRange6;

	private UcChargeRange ucChargeRange3;

	private UcChargeRange ucChargeRange5;

	private UcChargeRange ucChargeRange2;

	private UcChargeRange ucChargeRange4;

	private UcChargeRange ucChargeRange1;

	private NumericUpDown bindFirstNormalChargeA;

	private NumericUpDown bindFirstNormalChargeB;

	private NumericUpDown bindNormalChargeA;

	private NumericUpDown bindNormalChargeB;

	private NumericUpDown bindFineChargeA;

	private NumericUpDown bindFineChargeB;

	private NumericUpDown bindFirstMinA;

	private NumericUpDown bindFirstMinB;

	private DateTimePicker bindStartDate;

	private ComboBox bindParkTypeID;

	private TextBox bindTimeChargeNamePt;

	private TextBox bindTimeChargeNameCn;

	private Label labTimeChargeNamePt;

	private Label labStartDate;

	private Label labNormalChargeA;

	private Label labNormalChargeB;

	private Label labelX7;

	private Label labStartTimeA;

	private Label labFineChargeA;

	private Label labFineChargeB;

	private Label labFirstNormalChargeA;

	private Label labFirstNormalChargeB;

	private Label labFirstMinA;

	private Label labFirstMinB;

	private Label labStartTimeB;

	private Label labParkTypeID;

	private Label labTimeChargeNameCn;

	private Button btnSave;

	private Button btnClose;

	private Button btnDelete;

	private Button btnNew;

	private Panel panel1;

	public UcTimeCharge()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		bs = new BindingSource();
		dataMain.DataError += dataMain_DataError;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labelX7.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labelX7");
		labFineChargeA.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labelX1");
		labFineChargeB.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labelX1");
		labFirstMinA.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labFirstMinA");
		labFirstMinB.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labFirstMinB");
		labFirstNormalChargeA.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labFirstNormalChargeA");
		labFirstNormalChargeB.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labFirstNormalChargeB");
		labNormalChargeA.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labAddress");
		labNormalChargeB.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labAddress");
		labParkTypeID.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labEmailAddress");
		labStartDate.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labelX2");
		labStartTimeA.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labelX6");
		labStartTimeB.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labelX5");
		labTimeChargeNameCn.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labStaffCode");
		labTimeChargeNamePt.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcTimeCharge.labStaffName");
		labTitle.Text = LangManager.GetLangString("CarPark2018.Forms.FormTimeChargeEx.labTitle");
		btnClose.Text = LangManager.GetLangString("CarPark.Forms.FormSystemConfig.btnClose");
		btnDelete.Text = LangManager.GetLangString("CarPark.Forms.FormSystemConfig.btnDelete");
		btnNew.Text = LangManager.GetLangString("CarPark.Forms.FormSystemConfig.btnAdd");
		btnSave.Text = LangManager.GetLangString("CarPark.Forms.FormSystemConfig.btnSave");
	}

	private void dataMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	private void FormTimeChargeEx_Load(object sender, EventArgs e)
	{
		try
		{
			SetDGVStyle(dataMain);
			try
			{
				GetTimeChargeReturn timeCharge = Common._Carpark2018ServiceContext.CommunicationChannel.GetTimeCharge(new GetTimeChargeArgs(), out m_TimeCharge, out m_TimeChargeEXT);
				if (timeCharge.ISOK)
				{
					LoadInfo();
					InitBinding();
				}
				else
				{
					Global.ShowMessage(timeCharge.ErrCode);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				Console.WriteLine(ex.Message);
				Global.ShowMessage(ex.Message);
				Close();
			}
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
			BindingHelper.BindControls<TimeCharge>(bs, panFill, "bind");
			BindingHelper.BindDataGridView<TimeCharge>(bs, dataMain, new DataGridBindingAttr[7]
			{
				new DataGridBindingAttr(PropertyHelper<TimeCharge>.GetProperty((TimeCharge m) => m.TimeChargeNameCn), 200),
				new DataGridBindingAttr(PropertyHelper<TimeCharge>.GetProperty((TimeCharge m) => m.TimeChargeNamePt), 200),
				new DataGridBindingAttr(PropertyHelper<TimeCharge>.GetProperty((TimeCharge m) => m.ParkTypeID), 150),
				new DataGridBindingAttr(PropertyHelper<TimeCharge>.GetProperty((TimeCharge m) => m.NormalChargeA), 250),
				new DataGridBindingAttr(PropertyHelper<TimeCharge>.GetProperty((TimeCharge m) => m.NormalChargeB), 250),
				new DataGridBindingAttr(PropertyHelper<TimeCharge>.GetProperty((TimeCharge m) => m.FineChargeA), 250),
				new DataGridBindingAttr(PropertyHelper<TimeCharge>.GetProperty((TimeCharge m) => m.FineChargeB), 250)
			});
			bs.CurrentChanged += bs_CurrentChanged;
			bs_CurrentChanged(null, null);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void bs_CurrentChanged(object sender, EventArgs e)
	{
		TimeCharge charge = bs.Current as TimeCharge;
		bindParkTypeID.SelectedValue = charge.ParkTypeID;
		if (charge == null)
		{
			return;
		}
		UcChargeRange ucChargeRange = ucChargeRange1;
		UcChargeRange ucChargeRange2 = this.ucChargeRange2;
		UcChargeRange ucChargeRange3 = this.ucChargeRange3;
		UcChargeRange ucChargeRange4 = this.ucChargeRange4;
		UcChargeRange ucChargeRange5 = this.ucChargeRange5;
		TimeChargeExt timeChargeExt = (ucChargeRange6.ChargeExt = null);
		TimeChargeExt timeChargeExt3 = (ucChargeRange5.ChargeExt = timeChargeExt);
		TimeChargeExt timeChargeExt5 = (ucChargeRange4.ChargeExt = timeChargeExt3);
		TimeChargeExt timeChargeExt7 = (ucChargeRange3.ChargeExt = timeChargeExt5);
		TimeChargeExt chargeExt = (ucChargeRange2.ChargeExt = timeChargeExt7);
		ucChargeRange.ChargeExt = chargeExt;
		List<TimeChargeExt> source = m_TimeChargeEXT.Where((TimeChargeExt m) => m.ParkTypeID == charge.ParkTypeID).ToList();
		List<TimeChargeExt> list = (from m in source
			where !m.AfterFlag
			orderby m.ExtID
			select m).ToList();
		int num = 0;
		foreach (TimeChargeExt item in list)
		{
			switch (num)
			{
			case 0:
				ucChargeRange1.ChargeExt = item;
				break;
			case 1:
				this.ucChargeRange5.ChargeExt = item;
				break;
			case 2:
				this.ucChargeRange3.ChargeExt = item;
				break;
			}
			num++;
		}
		List<TimeChargeExt> list2 = (from m in source
			where m.AfterFlag
			orderby m.ExtID
			select m).ToList();
		num = 0;
		foreach (TimeChargeExt item2 in list2)
		{
			switch (num)
			{
			case 0:
				ucChargeRange6.ChargeExt = item2;
				break;
			case 1:
				this.ucChargeRange4.ChargeExt = item2;
				break;
			case 2:
				this.ucChargeRange2.ChargeExt = item2;
				break;
			}
			num++;
		}
	}

	private void Delete(object sender, EventArgs e)
	{
		try
		{
			DataBuffer2018.CheckRole(MethodBase.GetCurrentMethod());
			if (Global.ShowDialog("是否要刪除該規則", OkFocus: true) != DialogResult.Cancel && bs.Current is TimeCharge timeCharge)
			{
				DeleteTimeChargeArgs deleteTimeChargeArgs = new DeleteTimeChargeArgs();
				deleteTimeChargeArgs.PayStationName = Settings.Default.OnlyID;
				deleteTimeChargeArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
				DeleteTimeChargeReturn deleteTimeChargeReturn = Common._Carpark2018ServiceContext.CommunicationChannel.DeleteTimeCharge(deleteTimeChargeArgs, timeCharge);
				if (deleteTimeChargeReturn.ISOK)
				{
					Global.ShowMessage("刪除成功");
					GetTimeCharge();
				}
				else
				{
					Global.ShowMessage(deleteTimeChargeReturn.ErrCode);
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Global.ShowMessage(LangManager.GetLangString(ex.Message));
		}
	}

	private void New(object sender, EventArgs e)
	{
		try
		{
			DataBuffer2018.CheckRole(MethodBase.GetCurrentMethod());
			isNew = true;
			for (int i = 0; i < bs.Count; i++)
			{
				TimeCharge timeCharge = bs[i] as TimeCharge;
			}
			bs.Add(new TimeCharge());
			bs.MoveLast();
		}
		catch (Exception ex)
		{
			Global.ShowMessage(LangManager.GetLangString(ex.Message));
			Logger.Error(ex);
		}
	}

	private void Save(object sender, EventArgs e)
	{
		try
		{
			DataBuffer2018.CheckRole(MethodBase.GetCurrentMethod());
			if (!(bs.Current is TimeCharge timeCharge))
			{
				return;
			}
			if (isNew)
			{
				AddTimeChargeArgs addTimeChargeArgs = new AddTimeChargeArgs();
				addTimeChargeArgs.PayStationName = Settings.Default.OnlyID;
				addTimeChargeArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
				AddTimeChargeReturn addTimeChargeReturn = Common._Carpark2018ServiceContext.CommunicationChannel.AddTimeCharge(addTimeChargeArgs, timeCharge);
				if (addTimeChargeReturn.ISOK)
				{
					Global.ShowMessage(LangManager.GetLangString("SaveSucceed"));
					GetTimeCharge();
				}
				else
				{
					Global.ShowMessage(addTimeChargeReturn.ErrCode);
				}
				isNew = false;
				return;
			}
			UpdateTimeChargeArgs updateTimeChargeArgs = new UpdateTimeChargeArgs();
			updateTimeChargeArgs.PayStationName = Settings.Default.OnlyID;
			updateTimeChargeArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			List<TimeChargeExt> list = new List<TimeChargeExt>();
			list.Add(ucChargeRange1.ChargeExt);
			list.Add(ucChargeRange2.ChargeExt);
			list.Add(ucChargeRange3.ChargeExt);
			list.Add(ucChargeRange4.ChargeExt);
			list.Add(ucChargeRange5.ChargeExt);
			list.Add(ucChargeRange6.ChargeExt);
			UpdateTimeChargeReturn updateTimeChargeReturn = Common._Carpark2018ServiceContext.CommunicationChannel.UpdateTimeCharge(updateTimeChargeArgs, timeCharge, list);
			if (updateTimeChargeReturn.ISOK)
			{
				Global.ShowMessage(LangManager.GetLangString("SaveSucceed"));
				GetTimeCharge();
			}
			else
			{
				Global.ShowMessage(updateTimeChargeReturn.ErrCode);
			}
		}
		catch (Exception ex)
		{
			Global.ShowMessage(LangManager.GetLangString(ex.Message));
			Logger.Error(ex);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void LoadInfo()
	{
		try
		{
			bs.Clear();
			foreach (TimeCharge item in m_TimeCharge)
			{
				bs.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void GetTimeCharge()
	{
		try
		{
			GetTimeChargeReturn timeCharge = Common._Carpark2018ServiceContext.CommunicationChannel.GetTimeCharge(new GetTimeChargeArgs(), out m_TimeCharge, out m_TimeChargeEXT);
			if (timeCharge.ISOK)
			{
				LoadInfo();
			}
			else
			{
				Global.ShowMessage(timeCharge.ErrCode);
			}
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
			Global.ShowMessage(ex.Message);
			Close();
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
		this.btnDelete = new System.Windows.Forms.Button();
		this.btnNew = new System.Windows.Forms.Button();
		this.btnSave = new System.Windows.Forms.Button();
		this.btnClose = new System.Windows.Forms.Button();
		this.panFill = new System.Windows.Forms.Panel();
		this.ucChargeRange6 = new CarPark2018.UserControls.UcChargeRange();
		this.ucChargeRange3 = new CarPark2018.UserControls.UcChargeRange();
		this.ucChargeRange5 = new CarPark2018.UserControls.UcChargeRange();
		this.ucChargeRange2 = new CarPark2018.UserControls.UcChargeRange();
		this.ucChargeRange4 = new CarPark2018.UserControls.UcChargeRange();
		this.ucChargeRange1 = new CarPark2018.UserControls.UcChargeRange();
		this.bindFirstNormalChargeA = new System.Windows.Forms.NumericUpDown();
		this.bindFirstNormalChargeB = new System.Windows.Forms.NumericUpDown();
		this.bindNormalChargeA = new System.Windows.Forms.NumericUpDown();
		this.bindNormalChargeB = new System.Windows.Forms.NumericUpDown();
		this.bindFineChargeA = new System.Windows.Forms.NumericUpDown();
		this.bindFineChargeB = new System.Windows.Forms.NumericUpDown();
		this.bindFirstMinA = new System.Windows.Forms.NumericUpDown();
		this.bindFirstMinB = new System.Windows.Forms.NumericUpDown();
		this.bindStartDate = new System.Windows.Forms.DateTimePicker();
		this.bindParkTypeID = new System.Windows.Forms.ComboBox();
		this.bindTimeChargeNamePt = new System.Windows.Forms.TextBox();
		this.bindTimeChargeNameCn = new System.Windows.Forms.TextBox();
		this.labTimeChargeNamePt = new System.Windows.Forms.Label();
		this.labStartDate = new System.Windows.Forms.Label();
		this.labNormalChargeA = new System.Windows.Forms.Label();
		this.labNormalChargeB = new System.Windows.Forms.Label();
		this.labelX7 = new System.Windows.Forms.Label();
		this.labStartTimeA = new System.Windows.Forms.Label();
		this.labFineChargeA = new System.Windows.Forms.Label();
		this.labFineChargeB = new System.Windows.Forms.Label();
		this.labFirstNormalChargeA = new System.Windows.Forms.Label();
		this.labFirstNormalChargeB = new System.Windows.Forms.Label();
		this.labFirstMinA = new System.Windows.Forms.Label();
		this.labFirstMinB = new System.Windows.Forms.Label();
		this.labStartTimeB = new System.Windows.Forms.Label();
		this.labParkTypeID = new System.Windows.Forms.Label();
		this.labTimeChargeNameCn = new System.Windows.Forms.Label();
		this.dataMain = new System.Windows.Forms.DataGridView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panBottom.SuspendLayout();
		this.panFill.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.bindFirstNormalChargeA).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFirstNormalChargeB).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindNormalChargeA).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindNormalChargeB).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFineChargeA).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFineChargeB).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFirstMinA).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFirstMinB).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataMain).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold);
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(1170, 69);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "收費規則";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panBottom.Controls.Add(this.btnDelete);
		this.panBottom.Controls.Add(this.btnNew);
		this.panBottom.Controls.Add(this.btnSave);
		this.panBottom.Controls.Add(this.btnClose);
		this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panBottom.Location = new System.Drawing.Point(0, 620);
		this.panBottom.Name = "panBottom";
		this.panBottom.Size = new System.Drawing.Size(1170, 78);
		this.panBottom.TabIndex = 1;
		this.btnDelete.Location = new System.Drawing.Point(922, 6);
		this.btnDelete.Name = "btnDelete";
		this.btnDelete.Size = new System.Drawing.Size(116, 60);
		this.btnDelete.TabIndex = 0;
		this.btnDelete.Text = "刪除";
		this.btnDelete.UseVisualStyleBackColor = true;
		this.btnDelete.Click += new System.EventHandler(Delete);
		this.btnNew.Location = new System.Drawing.Point(678, 6);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(116, 60);
		this.btnNew.TabIndex = 0;
		this.btnNew.Text = "新增";
		this.btnNew.UseVisualStyleBackColor = true;
		this.btnNew.Click += new System.EventHandler(New);
		this.btnSave.Location = new System.Drawing.Point(800, 6);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(116, 60);
		this.btnSave.TabIndex = 0;
		this.btnSave.Text = "保存";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(Save);
		this.btnClose.Location = new System.Drawing.Point(1044, 6);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(116, 60);
		this.btnClose.TabIndex = 0;
		this.btnClose.Text = "關閉";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.panFill.BackColor = System.Drawing.Color.White;
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panFill.Controls.Add(this.ucChargeRange6);
		this.panFill.Controls.Add(this.ucChargeRange3);
		this.panFill.Controls.Add(this.ucChargeRange5);
		this.panFill.Controls.Add(this.ucChargeRange2);
		this.panFill.Controls.Add(this.ucChargeRange4);
		this.panFill.Controls.Add(this.ucChargeRange1);
		this.panFill.Controls.Add(this.bindFirstNormalChargeA);
		this.panFill.Controls.Add(this.bindFirstNormalChargeB);
		this.panFill.Controls.Add(this.bindNormalChargeA);
		this.panFill.Controls.Add(this.bindNormalChargeB);
		this.panFill.Controls.Add(this.bindFineChargeA);
		this.panFill.Controls.Add(this.bindFineChargeB);
		this.panFill.Controls.Add(this.bindFirstMinA);
		this.panFill.Controls.Add(this.bindFirstMinB);
		this.panFill.Controls.Add(this.bindStartDate);
		this.panFill.Controls.Add(this.bindParkTypeID);
		this.panFill.Controls.Add(this.bindTimeChargeNamePt);
		this.panFill.Controls.Add(this.bindTimeChargeNameCn);
		this.panFill.Controls.Add(this.labTimeChargeNamePt);
		this.panFill.Controls.Add(this.labStartDate);
		this.panFill.Controls.Add(this.labNormalChargeA);
		this.panFill.Controls.Add(this.labNormalChargeB);
		this.panFill.Controls.Add(this.labelX7);
		this.panFill.Controls.Add(this.labStartTimeA);
		this.panFill.Controls.Add(this.labFineChargeA);
		this.panFill.Controls.Add(this.labFineChargeB);
		this.panFill.Controls.Add(this.labFirstNormalChargeA);
		this.panFill.Controls.Add(this.labFirstNormalChargeB);
		this.panFill.Controls.Add(this.labFirstMinA);
		this.panFill.Controls.Add(this.labFirstMinB);
		this.panFill.Controls.Add(this.labStartTimeB);
		this.panFill.Controls.Add(this.labParkTypeID);
		this.panFill.Controls.Add(this.labTimeChargeNameCn);
		this.panFill.Controls.Add(this.dataMain);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Location = new System.Drawing.Point(0, 69);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(1170, 551);
		this.panFill.TabIndex = 2;
		this.ucChargeRange6.ChargeExt = null;
		this.ucChargeRange6.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucChargeRange6.Location = new System.Drawing.Point(592, 504);
		this.ucChargeRange6.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		this.ucChargeRange6.Name = "ucChargeRange6";
		this.ucChargeRange6.Size = new System.Drawing.Size(443, 43);
		this.ucChargeRange6.TabIndex = 38;
		this.ucChargeRange3.ChargeExt = null;
		this.ucChargeRange3.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucChargeRange3.Location = new System.Drawing.Point(137, 504);
		this.ucChargeRange3.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		this.ucChargeRange3.Name = "ucChargeRange3";
		this.ucChargeRange3.Size = new System.Drawing.Size(443, 43);
		this.ucChargeRange3.TabIndex = 37;
		this.ucChargeRange5.ChargeExt = null;
		this.ucChargeRange5.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucChargeRange5.Location = new System.Drawing.Point(592, 460);
		this.ucChargeRange5.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		this.ucChargeRange5.Name = "ucChargeRange5";
		this.ucChargeRange5.Size = new System.Drawing.Size(443, 43);
		this.ucChargeRange5.TabIndex = 36;
		this.ucChargeRange2.ChargeExt = null;
		this.ucChargeRange2.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucChargeRange2.Location = new System.Drawing.Point(137, 460);
		this.ucChargeRange2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		this.ucChargeRange2.Name = "ucChargeRange2";
		this.ucChargeRange2.Size = new System.Drawing.Size(443, 43);
		this.ucChargeRange2.TabIndex = 41;
		this.ucChargeRange4.ChargeExt = null;
		this.ucChargeRange4.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucChargeRange4.Location = new System.Drawing.Point(592, 416);
		this.ucChargeRange4.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		this.ucChargeRange4.Name = "ucChargeRange4";
		this.ucChargeRange4.Size = new System.Drawing.Size(443, 43);
		this.ucChargeRange4.TabIndex = 40;
		this.ucChargeRange1.ChargeExt = null;
		this.ucChargeRange1.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucChargeRange1.Location = new System.Drawing.Point(137, 416);
		this.ucChargeRange1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		this.ucChargeRange1.Name = "ucChargeRange1";
		this.ucChargeRange1.Size = new System.Drawing.Size(443, 43);
		this.ucChargeRange1.TabIndex = 39;
		this.bindFirstNormalChargeA.Location = new System.Drawing.Point(475, 267);
		this.bindFirstNormalChargeA.Name = "bindFirstNormalChargeA";
		this.bindFirstNormalChargeA.Size = new System.Drawing.Size(58, 34);
		this.bindFirstNormalChargeA.TabIndex = 30;
		this.bindFirstNormalChargeB.Location = new System.Drawing.Point(962, 269);
		this.bindFirstNormalChargeB.Name = "bindFirstNormalChargeB";
		this.bindFirstNormalChargeB.Size = new System.Drawing.Size(58, 34);
		this.bindFirstNormalChargeB.TabIndex = 27;
		this.bindNormalChargeA.Location = new System.Drawing.Point(304, 341);
		this.bindNormalChargeA.Name = "bindNormalChargeA";
		this.bindNormalChargeA.Size = new System.Drawing.Size(229, 34);
		this.bindNormalChargeA.TabIndex = 28;
		this.bindNormalChargeB.Location = new System.Drawing.Point(791, 343);
		this.bindNormalChargeB.Name = "bindNormalChargeB";
		this.bindNormalChargeB.Size = new System.Drawing.Size(229, 34);
		this.bindNormalChargeB.TabIndex = 31;
		this.bindFineChargeA.Location = new System.Drawing.Point(304, 304);
		this.bindFineChargeA.Name = "bindFineChargeA";
		this.bindFineChargeA.Size = new System.Drawing.Size(229, 34);
		this.bindFineChargeA.TabIndex = 34;
		this.bindFineChargeB.Location = new System.Drawing.Point(791, 306);
		this.bindFineChargeB.Name = "bindFineChargeB";
		this.bindFineChargeB.Size = new System.Drawing.Size(229, 34);
		this.bindFineChargeB.TabIndex = 35;
		this.bindFirstMinA.Location = new System.Drawing.Point(304, 267);
		this.bindFirstMinA.Name = "bindFirstMinA";
		this.bindFirstMinA.Size = new System.Drawing.Size(58, 34);
		this.bindFirstMinA.TabIndex = 32;
		this.bindFirstMinB.Location = new System.Drawing.Point(791, 269);
		this.bindFirstMinB.Name = "bindFirstMinB";
		this.bindFirstMinB.Size = new System.Drawing.Size(58, 34);
		this.bindFirstMinB.TabIndex = 33;
		this.bindStartDate.CustomFormat = "yyyy-MM-dd";
		this.bindStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindStartDate.Location = new System.Drawing.Point(791, 201);
		this.bindStartDate.Name = "bindStartDate";
		this.bindStartDate.Size = new System.Drawing.Size(229, 34);
		this.bindStartDate.TabIndex = 26;
		this.bindParkTypeID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.bindParkTypeID.FormattingEnabled = true;
		this.bindParkTypeID.Location = new System.Drawing.Point(304, 200);
		this.bindParkTypeID.Name = "bindParkTypeID";
		this.bindParkTypeID.Size = new System.Drawing.Size(229, 35);
		this.bindParkTypeID.TabIndex = 25;
		this.bindTimeChargeNamePt.Location = new System.Drawing.Point(791, 160);
		this.bindTimeChargeNamePt.Name = "bindTimeChargeNamePt";
		this.bindTimeChargeNamePt.Size = new System.Drawing.Size(229, 34);
		this.bindTimeChargeNamePt.TabIndex = 24;
		this.bindTimeChargeNameCn.Location = new System.Drawing.Point(304, 161);
		this.bindTimeChargeNameCn.Name = "bindTimeChargeNameCn";
		this.bindTimeChargeNameCn.Size = new System.Drawing.Size(229, 34);
		this.bindTimeChargeNameCn.TabIndex = 23;
		this.labTimeChargeNamePt.Location = new System.Drawing.Point(639, 160);
		this.labTimeChargeNamePt.Name = "labTimeChargeNamePt";
		this.labTimeChargeNamePt.Size = new System.Drawing.Size(146, 34);
		this.labTimeChargeNamePt.TabIndex = 12;
		this.labTimeChargeNamePt.Text = "英文名稱";
		this.labTimeChargeNamePt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labStartDate.Location = new System.Drawing.Point(639, 201);
		this.labStartDate.Name = "labStartDate";
		this.labStartDate.Size = new System.Drawing.Size(146, 34);
		this.labStartDate.TabIndex = 11;
		this.labStartDate.Text = "開始日期";
		this.labStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labNormalChargeA.Location = new System.Drawing.Point(152, 341);
		this.labNormalChargeA.Name = "labNormalChargeA";
		this.labNormalChargeA.Size = new System.Drawing.Size(146, 34);
		this.labNormalChargeA.TabIndex = 14;
		this.labNormalChargeA.Text = "標準收費";
		this.labNormalChargeA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labNormalChargeB.Location = new System.Drawing.Point(639, 343);
		this.labNormalChargeB.Name = "labNormalChargeB";
		this.labNormalChargeB.Size = new System.Drawing.Size(146, 34);
		this.labNormalChargeB.TabIndex = 13;
		this.labNormalChargeB.Text = "標準收費";
		this.labNormalChargeB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelX7.Location = new System.Drawing.Point(137, 379);
		this.labelX7.Name = "labelX7";
		this.labelX7.Size = new System.Drawing.Size(898, 34);
		this.labelX7.TabIndex = 8;
		this.labelX7.Text = "分時段收費";
		this.labelX7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labStartTimeA.Location = new System.Drawing.Point(639, 232);
		this.labStartTimeA.Name = "labStartTimeA";
		this.labStartTimeA.Size = new System.Drawing.Size(381, 34);
		this.labStartTimeA.TabIndex = 7;
		this.labStartTimeA.Text = "開始日期之後";
		this.labStartTimeA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labFineChargeA.Location = new System.Drawing.Point(152, 304);
		this.labFineChargeA.Name = "labFineChargeA";
		this.labFineChargeA.Size = new System.Drawing.Size(146, 34);
		this.labFineChargeA.TabIndex = 10;
		this.labFineChargeA.Text = "標準罰款";
		this.labFineChargeA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFineChargeB.Location = new System.Drawing.Point(639, 306);
		this.labFineChargeB.Name = "labFineChargeB";
		this.labFineChargeB.Size = new System.Drawing.Size(146, 34);
		this.labFineChargeB.TabIndex = 9;
		this.labFineChargeB.Text = "標準罰款";
		this.labFineChargeB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFirstNormalChargeA.Location = new System.Drawing.Point(368, 267);
		this.labFirstNormalChargeA.Name = "labFirstNormalChargeA";
		this.labFirstNormalChargeA.Size = new System.Drawing.Size(101, 34);
		this.labFirstNormalChargeA.TabIndex = 19;
		this.labFirstNormalChargeA.Text = "收費(H)";
		this.labFirstNormalChargeA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFirstNormalChargeB.Location = new System.Drawing.Point(855, 269);
		this.labFirstNormalChargeB.Name = "labFirstNormalChargeB";
		this.labFirstNormalChargeB.Size = new System.Drawing.Size(101, 34);
		this.labFirstNormalChargeB.TabIndex = 22;
		this.labFirstNormalChargeB.Text = "收費(H)";
		this.labFirstNormalChargeB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFirstMinA.Location = new System.Drawing.Point(152, 267);
		this.labFirstMinA.Name = "labFirstMinA";
		this.labFirstMinA.Size = new System.Drawing.Size(146, 34);
		this.labFirstMinA.TabIndex = 21;
		this.labFirstMinA.Text = "首(分鐘)";
		this.labFirstMinA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFirstMinB.Location = new System.Drawing.Point(639, 269);
		this.labFirstMinB.Name = "labFirstMinB";
		this.labFirstMinB.Size = new System.Drawing.Size(146, 34);
		this.labFirstMinB.TabIndex = 16;
		this.labFirstMinB.Text = "首(分鐘)";
		this.labFirstMinB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labStartTimeB.Location = new System.Drawing.Point(157, 230);
		this.labStartTimeB.Name = "labStartTimeB";
		this.labStartTimeB.Size = new System.Drawing.Size(376, 34);
		this.labStartTimeB.TabIndex = 15;
		this.labStartTimeB.Text = "開始日期之前";
		this.labStartTimeB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labParkTypeID.Location = new System.Drawing.Point(152, 201);
		this.labParkTypeID.Name = "labParkTypeID";
		this.labParkTypeID.Size = new System.Drawing.Size(146, 34);
		this.labParkTypeID.TabIndex = 18;
		this.labParkTypeID.Text = "車類型";
		this.labParkTypeID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTimeChargeNameCn.Location = new System.Drawing.Point(152, 161);
		this.labTimeChargeNameCn.Name = "labTimeChargeNameCn";
		this.labTimeChargeNameCn.Size = new System.Drawing.Size(146, 34);
		this.labTimeChargeNameCn.TabIndex = 17;
		this.labTimeChargeNameCn.Text = "中文名稱";
		this.labTimeChargeNameCn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dataMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataMain.Dock = System.Windows.Forms.DockStyle.Top;
		this.dataMain.Location = new System.Drawing.Point(0, 0);
		this.dataMain.MultiSelect = false;
		this.dataMain.Name = "dataMain";
		this.dataMain.RowTemplate.Height = 24;
		this.dataMain.Size = new System.Drawing.Size(1166, 154);
		this.dataMain.TabIndex = 1;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.panFill);
		this.panel1.Controls.Add(this.panBottom);
		this.panel1.Controls.Add(this.labTitle);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1172, 700);
		this.panel1.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(1172, 700);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ForeColor = System.Drawing.Color.Navy;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "UcTimeCharge";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormTimeChargeEx";
		base.Load += new System.EventHandler(FormTimeChargeEx_Load);
		this.panBottom.ResumeLayout(false);
		this.panFill.ResumeLayout(false);
		this.panFill.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.bindFirstNormalChargeA).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFirstNormalChargeB).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindNormalChargeA).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindNormalChargeB).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFineChargeA).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFineChargeB).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFirstMinA).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFirstMinB).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataMain).EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
