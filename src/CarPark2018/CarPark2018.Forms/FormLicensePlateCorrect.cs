using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.DB.AdditionalDataSource;
using CarPark.Lib;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.Forms;

public class FormLicensePlateCorrect : Form
{
	private ILog Logger;

	public List<view_transactionandlp> list;

	public view_transactionandlp view;

	private BindingSource bsView;

	private IContainer components = null;

	private Button btnSave;

	private Button btnCancel;

	private Panel panFill;

	private Panel panMiddle;

	private Panel panBottom;

	private Label labTitle;

	private TextBox bindLPText;

	private DateTimePicker bindEndTime;

	private DateTimePicker bindStartTime;

	private Button btnCheck;

	private DataGridView dataMain;

	private TextBox txtCorretLP;

	private Label labCorrectPlate;

	private PictureBox picLP;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private Panel panMiddle2;

	private Label labLPAdd;

	private Label labParktypeAdd;

	private Label labIntimeAdd;

	private DateTimePicker bindAddIntime;

	private TextBox bindAddLP;

	private ComboBox bindAddParkType;

	private Button btnAdd;

	private ComboBox bindAreaAdd;

	private Label labAreaAdd;

	private CheckBox ckcIntime;

	private Label labLP;

	public FormLicensePlateCorrect()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		SetDGVStyle(dataMain);
		view = new view_transactionandlp();
		bsView = new BindingSource();
		bsView.CurrentChanged += bsView_CurrentChanged;
		BindingHelper.BindDataGridView<view_transactionandlp>(bsView, dataMain, new DataGridBindingAttr[2]
		{
			new DataGridBindingAttr(PropertyHelper<view_transactionandlp>.GetProperty((view_transactionandlp m) => m.AnalysisResult), 115),
			new DataGridBindingAttr(PropertyHelper<view_transactionandlp>.GetProperty((view_transactionandlp m) => m.InTime), 189)
		});
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.labTitle");
		labLP.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.rbLP");
		ckcIntime.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.rbTime");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.btnCancel");
		btnCheck.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.btnCheck");
		btnSave.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.btnSave");
		labCorrectPlate.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.labCorrectPlate");
		tabPage1.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.tabPage1");
		tabPage2.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.tabPage2");
		labLPAdd.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.labLPAdd");
		labIntimeAdd.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.labIntimeAdd");
		labParktypeAdd.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.labParktypeAdd");
		labAreaAdd.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.labAreaAdd");
		btnAdd.Text = LangManager.GetLangString("CarPark.Forms.FormLicensePlateCorrect.btnAdd");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

	private void bindLPText_KeyPress(object sender, KeyPressEventArgs e)
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

	private void rbLP_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void btnCheck_Click(object sender, EventArgs e)
	{
		try
		{
			bsView.Clear();
			GetView_TransactionAndLPArgs getView_TransactionAndLPArgs = new GetView_TransactionAndLPArgs();
			getView_TransactionAndLPArgs.LicensePlate = bindLPText.Text;
			if (ckcIntime.Checked)
			{
				getView_TransactionAndLPArgs.InStartTime = bindStartTime.Value;
				getView_TransactionAndLPArgs.InEndTime = bindEndTime.Value;
			}
			list = new List<view_transactionandlp>();
			GetView_TransactionAndLPReturn view_TransactionAndLP = LPDBHelper.GetView_TransactionAndLP(getView_TransactionAndLPArgs, out list);
			if (list.Count > 0)
			{
				foreach (view_transactionandlp item in list)
				{
					bsView.Add(item);
				}
				return;
			}
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.Show4"));
		}
		catch (TimeoutException message)
		{
			Logger.Error(message);
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
		}
		catch (Exception message2)
		{
			Logger.Error(message2);
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
		dataGridViewCellStyle.Font = new Font("Times New Roman", 15f);
		dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
		dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = SystemColors.ControlText;
		dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;
		dgv.DefaultCellStyle = dataGridViewCellStyle;
		dgv.GridColor = Color.FromArgb(208, 215, 229);
		dgv.ReadOnly = true;
		dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
		dgv.RowTemplate.Height = 30;
		dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 15f);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			if (tabControl1.SelectedIndex == 0)
			{
				if (string.IsNullOrWhiteSpace(txtCorretLP.Text))
				{
					Global.ShowMessage(LangManager.GetLangString("LP_Input_NULL"));
					return;
				}
				view = bsView.Current as view_transactionandlp;
				if (view == null)
				{
					Global.ShowMessage(LangManager.GetLangString("LP_SELECT_NULL"));
					return;
				}
				CorrectLicensePlateArgs correctLicensePlateArgs = new CorrectLicensePlateArgs();
				correctLicensePlateArgs.TransactionDataID = view.TransactionID;
				correctLicensePlateArgs.NewLicensePlate = txtCorretLP.Text.ToString();
				correctLicensePlateArgs.PayStationName = Settings.Default.OnlyID;
				correctLicensePlateArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
				CorrectLicensePlateReturn correctLicensePlateReturn = LPDBHelper.CorrectLicensePlate(correctLicensePlateArgs);
				if (correctLicensePlateReturn.ISOK)
				{
					Global.ShowMessage(LangManager.GetLangString("SaveSucceed"));
					bsView.Clear();
					txtCorretLP.Text = "";
					bindLPText.Text = "";
				}
				else
				{
					Global.ShowMessage(LangManager.GetLangString(correctLicensePlateReturn.ErrCode));
				}
				return;
			}
			if (string.IsNullOrWhiteSpace(bindAddLP.Text))
			{
				Global.ShowMessage(LangManager.GetLangString("LP_Input_NULL"));
				return;
			}
			ManualEntryLicensePlateArgs manualEntryLicensePlateArgs = new ManualEntryLicensePlateArgs();
			manualEntryLicensePlateArgs.LicensePlate = bindAddLP.Text.ToString();
			manualEntryLicensePlateArgs.PayStationName = Settings.Default.OnlyID;
			manualEntryLicensePlateArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			manualEntryLicensePlateArgs.AreaID = (int)bindAreaAdd.SelectedValue;
			manualEntryLicensePlateArgs.InTime = new DateTime(bindAddIntime.Value.Year, bindAddIntime.Value.Month, bindAddIntime.Value.Day, bindAddIntime.Value.Hour, bindAddIntime.Value.Minute, bindAddIntime.Value.Second);
			ChargeContext chargeContext = new ChargeContext();
			ManualEntryLicensePlateReturn manualEntryLicensePlateReturn = chargeContext.CommunicationChannel.ManualEntryLicensePlate(manualEntryLicensePlateArgs, (EnumParkType)bindAddParkType.SelectedValue, EnumBillType.TimeChargeLostLicensePlate);
			chargeContext.CommunicationChannel.Disconnect();
			if (manualEntryLicensePlateReturn.ISOK)
			{
				Global.ShowMessage(LangManager.GetLangString("SaveSucceed"));
				bindAddLP.Text = "";
				btnAdd.Enabled = true;
				btnSave.Enabled = false;
				bindAreaAdd.Enabled = false;
				bindAddLP.Enabled = false;
				bindAddIntime.Enabled = false;
				bindAddParkType.Enabled = false;
			}
			else
			{
				Global.ShowMessage(LangManager.GetLangString(manualEntryLicensePlateReturn.ErrCode));
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void bsView_CurrentChanged(object sender, EventArgs e)
	{
		try
		{
			view = bsView.Current as view_transactionandlp;
			if (LPDBHelper.Ping(Config.LicensePlatePath) && !string.IsNullOrEmpty(view.ImagePath))
			{
				try
				{
					picLP.Image = Image.FromFile(Config.LicensePlatePath + view.ImagePath);
					return;
				}
				catch (Exception)
				{
					picLP.Image = ImageManager.GetImage("", "cancel");
					return;
				}
			}
			picLP.Image = ImageManager.GetImage("", "cancel");
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tabControl1.SelectedIndex == 0)
		{
			btnAdd.Visible = false;
			btnSave.Enabled = true;
			bindLPText.Focus();
		}
		else
		{
			btnAdd.Visible = true;
			btnSave.Enabled = false;
		}
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		bindAreaAdd.Enabled = true;
		bindAddLP.Enabled = true;
		bindAddIntime.Enabled = true;
		bindAddParkType.Enabled = true;
		btnAdd.Enabled = false;
		btnSave.Enabled = true;
	}

	private void FormLicensePlateCorrect_Load(object sender, EventArgs e)
	{
		try
		{
			BindingHelper.BindComboBox<EnumParkTypeSource>(bindAddParkType);
			bindAreaAdd.DataSource = DataBuffer2018.AllParkAreas;
			bindAreaAdd.DisplayMember = "AreaName";
			bindAreaAdd.ValueMember = "AreaID";
			bindAreaAdd.SelectedValue = Convert.ToInt32(Config.AreaCode);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
	}

	private void bindAddLP_KeyPress(object sender, KeyPressEventArgs e)
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

	private void ckcIntime_CheckedChanged(object sender, EventArgs e)
	{
		if (ckcIntime.Checked)
		{
			bindStartTime.Enabled = true;
			bindEndTime.Enabled = true;
		}
		else
		{
			bindStartTime.Enabled = false;
			bindEndTime.Enabled = false;
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
		this.btnSave = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.panFill = new System.Windows.Forms.Panel();
		this.labTitle = new System.Windows.Forms.Label();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.panMiddle = new System.Windows.Forms.Panel();
		this.labLP = new System.Windows.Forms.Label();
		this.ckcIntime = new System.Windows.Forms.CheckBox();
		this.txtCorretLP = new System.Windows.Forms.TextBox();
		this.labCorrectPlate = new System.Windows.Forms.Label();
		this.picLP = new System.Windows.Forms.PictureBox();
		this.dataMain = new System.Windows.Forms.DataGridView();
		this.bindEndTime = new System.Windows.Forms.DateTimePicker();
		this.bindStartTime = new System.Windows.Forms.DateTimePicker();
		this.btnCheck = new System.Windows.Forms.Button();
		this.bindLPText = new System.Windows.Forms.TextBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.panMiddle2 = new System.Windows.Forms.Panel();
		this.bindAreaAdd = new System.Windows.Forms.ComboBox();
		this.labAreaAdd = new System.Windows.Forms.Label();
		this.bindAddParkType = new System.Windows.Forms.ComboBox();
		this.bindAddIntime = new System.Windows.Forms.DateTimePicker();
		this.bindAddLP = new System.Windows.Forms.TextBox();
		this.labParktypeAdd = new System.Windows.Forms.Label();
		this.labIntimeAdd = new System.Windows.Forms.Label();
		this.labLPAdd = new System.Windows.Forms.Label();
		this.panBottom = new System.Windows.Forms.Panel();
		this.btnAdd = new System.Windows.Forms.Button();
		this.panFill.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.panMiddle.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLP).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataMain).BeginInit();
		this.tabPage2.SuspendLayout();
		this.panMiddle2.SuspendLayout();
		this.panBottom.SuspendLayout();
		base.SuspendLayout();
		this.btnSave.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.btnSave.ForeColor = System.Drawing.Color.Navy;
		this.btnSave.Location = new System.Drawing.Point(401, 10);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(116, 54);
		this.btnSave.TabIndex = 1;
		this.btnSave.Text = "保存";
		this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(571, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(116, 54);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Controls.Add(this.tabControl1);
		this.panFill.Controls.Add(this.panBottom);
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(700, 740);
		this.panFill.TabIndex = 3;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(700, 60);
		this.labTitle.TabIndex = 4;
		this.labTitle.Text = "車牌查詢糾正";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.tabControl1.Location = new System.Drawing.Point(0, 60);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(700, 610);
		this.tabControl1.TabIndex = 0;
		this.tabControl1.SelectedIndexChanged += new System.EventHandler(tabControl1_SelectedIndexChanged);
		this.tabPage1.Controls.Add(this.panMiddle);
		this.tabPage1.Location = new System.Drawing.Point(4, 44);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Size = new System.Drawing.Size(692, 562);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "車牌查詢糾正";
		this.panMiddle.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panMiddle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panMiddle.Controls.Add(this.labLP);
		this.panMiddle.Controls.Add(this.ckcIntime);
		this.panMiddle.Controls.Add(this.txtCorretLP);
		this.panMiddle.Controls.Add(this.labCorrectPlate);
		this.panMiddle.Controls.Add(this.picLP);
		this.panMiddle.Controls.Add(this.dataMain);
		this.panMiddle.Controls.Add(this.bindEndTime);
		this.panMiddle.Controls.Add(this.bindStartTime);
		this.panMiddle.Controls.Add(this.btnCheck);
		this.panMiddle.Controls.Add(this.bindLPText);
		this.panMiddle.Location = new System.Drawing.Point(0, 0);
		this.panMiddle.Name = "panMiddle";
		this.panMiddle.Size = new System.Drawing.Size(700, 563);
		this.panMiddle.TabIndex = 3;
		this.labLP.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.labLP.ForeColor = System.Drawing.Color.Navy;
		this.labLP.Location = new System.Drawing.Point(142, 23);
		this.labLP.Name = "labLP";
		this.labLP.Size = new System.Drawing.Size(117, 36);
		this.labLP.TabIndex = 16;
		this.labLP.Text = "車牌";
		this.labLP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ckcIntime.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.ckcIntime.ForeColor = System.Drawing.Color.Navy;
		this.ckcIntime.Location = new System.Drawing.Point(126, 95);
		this.ckcIntime.Name = "ckcIntime";
		this.ckcIntime.Size = new System.Drawing.Size(133, 39);
		this.ckcIntime.TabIndex = 15;
		this.ckcIntime.Text = "進場時間";
		this.ckcIntime.UseVisualStyleBackColor = true;
		this.ckcIntime.CheckedChanged += new System.EventHandler(ckcIntime_CheckedChanged);
		this.txtCorretLP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCorretLP.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.txtCorretLP.ForeColor = System.Drawing.Color.Red;
		this.txtCorretLP.Location = new System.Drawing.Point(366, 506);
		this.txtCorretLP.MaxLength = 7;
		this.txtCorretLP.Name = "txtCorretLP";
		this.txtCorretLP.Size = new System.Drawing.Size(273, 39);
		this.txtCorretLP.TabIndex = 14;
		this.txtCorretLP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox1_KeyPress);
		this.labCorrectPlate.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.labCorrectPlate.ForeColor = System.Drawing.Color.Navy;
		this.labCorrectPlate.Location = new System.Drawing.Point(360, 457);
		this.labCorrectPlate.Name = "labCorrectPlate";
		this.labCorrectPlate.Size = new System.Drawing.Size(271, 33);
		this.labCorrectPlate.TabIndex = 13;
		this.labCorrectPlate.Text = "糾正車牌";
		this.labCorrectPlate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.picLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.picLP.Location = new System.Drawing.Point(366, 224);
		this.picLP.Name = "picLP";
		this.picLP.Size = new System.Drawing.Size(319, 200);
		this.picLP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picLP.TabIndex = 12;
		this.picLP.TabStop = false;
		this.dataMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataMain.Location = new System.Drawing.Point(18, 224);
		this.dataMain.Name = "dataMain";
		this.dataMain.RowTemplate.Height = 24;
		this.dataMain.Size = new System.Drawing.Size(324, 322);
		this.dataMain.TabIndex = 11;
		this.bindEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
		this.bindEndTime.Enabled = false;
		this.bindEndTime.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.bindEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindEndTime.Location = new System.Drawing.Point(265, 164);
		this.bindEndTime.Name = "bindEndTime";
		this.bindEndTime.Size = new System.Drawing.Size(287, 39);
		this.bindEndTime.TabIndex = 9;
		this.bindStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
		this.bindStartTime.Enabled = false;
		this.bindStartTime.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.bindStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindStartTime.Location = new System.Drawing.Point(265, 95);
		this.bindStartTime.Name = "bindStartTime";
		this.bindStartTime.Size = new System.Drawing.Size(287, 39);
		this.bindStartTime.TabIndex = 8;
		this.btnCheck.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.btnCheck.ForeColor = System.Drawing.Color.Navy;
		this.btnCheck.Location = new System.Drawing.Point(569, 23);
		this.btnCheck.Name = "btnCheck";
		this.btnCheck.Size = new System.Drawing.Size(96, 41);
		this.btnCheck.TabIndex = 7;
		this.btnCheck.Text = "查詢";
		this.btnCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCheck.UseVisualStyleBackColor = true;
		this.btnCheck.Click += new System.EventHandler(btnCheck_Click);
		this.bindLPText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.bindLPText.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.bindLPText.Location = new System.Drawing.Point(265, 23);
		this.bindLPText.MaxLength = 10;
		this.bindLPText.Name = "bindLPText";
		this.bindLPText.Size = new System.Drawing.Size(287, 39);
		this.bindLPText.TabIndex = 6;
		this.bindLPText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(bindLPText_KeyPress);
		this.tabPage2.Controls.Add(this.panMiddle2);
		this.tabPage2.Location = new System.Drawing.Point(4, 44);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Size = new System.Drawing.Size(692, 562);
		this.tabPage2.TabIndex = 0;
		this.tabPage2.Text = "車牌錄入";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.panMiddle2.Controls.Add(this.bindAreaAdd);
		this.panMiddle2.Controls.Add(this.labAreaAdd);
		this.panMiddle2.Controls.Add(this.bindAddParkType);
		this.panMiddle2.Controls.Add(this.bindAddIntime);
		this.panMiddle2.Controls.Add(this.bindAddLP);
		this.panMiddle2.Controls.Add(this.labParktypeAdd);
		this.panMiddle2.Controls.Add(this.labIntimeAdd);
		this.panMiddle2.Controls.Add(this.labLPAdd);
		this.panMiddle2.Location = new System.Drawing.Point(0, 0);
		this.panMiddle2.Name = "panMiddle2";
		this.panMiddle2.Size = new System.Drawing.Size(700, 563);
		this.panMiddle2.TabIndex = 0;
		this.bindAreaAdd.Enabled = false;
		this.bindAreaAdd.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.bindAreaAdd.FormattingEnabled = true;
		this.bindAreaAdd.Location = new System.Drawing.Point(257, 265);
		this.bindAreaAdd.Name = "bindAreaAdd";
		this.bindAreaAdd.Size = new System.Drawing.Size(287, 39);
		this.bindAreaAdd.TabIndex = 14;
		this.labAreaAdd.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.labAreaAdd.ForeColor = System.Drawing.Color.Navy;
		this.labAreaAdd.Location = new System.Drawing.Point(7, 261);
		this.labAreaAdd.Name = "labAreaAdd";
		this.labAreaAdd.Size = new System.Drawing.Size(244, 42);
		this.labAreaAdd.TabIndex = 13;
		this.labAreaAdd.Text = "區域";
		this.labAreaAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindAddParkType.Enabled = false;
		this.bindAddParkType.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.bindAddParkType.FormattingEnabled = true;
		this.bindAddParkType.Location = new System.Drawing.Point(257, 193);
		this.bindAddParkType.Name = "bindAddParkType";
		this.bindAddParkType.Size = new System.Drawing.Size(287, 39);
		this.bindAddParkType.TabIndex = 10;
		this.bindAddIntime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
		this.bindAddIntime.Enabled = false;
		this.bindAddIntime.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.bindAddIntime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindAddIntime.Location = new System.Drawing.Point(257, 121);
		this.bindAddIntime.Name = "bindAddIntime";
		this.bindAddIntime.Size = new System.Drawing.Size(287, 39);
		this.bindAddIntime.TabIndex = 9;
		this.bindAddLP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.bindAddLP.Enabled = false;
		this.bindAddLP.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.bindAddLP.Location = new System.Drawing.Point(257, 52);
		this.bindAddLP.MaxLength = 10;
		this.bindAddLP.Name = "bindAddLP";
		this.bindAddLP.Size = new System.Drawing.Size(287, 39);
		this.bindAddLP.TabIndex = 7;
		this.bindAddLP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(bindAddLP_KeyPress);
		this.labParktypeAdd.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.labParktypeAdd.ForeColor = System.Drawing.Color.Navy;
		this.labParktypeAdd.Location = new System.Drawing.Point(7, 189);
		this.labParktypeAdd.Name = "labParktypeAdd";
		this.labParktypeAdd.Size = new System.Drawing.Size(244, 42);
		this.labParktypeAdd.TabIndex = 2;
		this.labParktypeAdd.Text = "車型";
		this.labParktypeAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labIntimeAdd.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.labIntimeAdd.ForeColor = System.Drawing.Color.Navy;
		this.labIntimeAdd.Location = new System.Drawing.Point(7, 117);
		this.labIntimeAdd.Name = "labIntimeAdd";
		this.labIntimeAdd.Size = new System.Drawing.Size(244, 42);
		this.labIntimeAdd.TabIndex = 1;
		this.labIntimeAdd.Text = "入場時間";
		this.labIntimeAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labLPAdd.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.labLPAdd.ForeColor = System.Drawing.Color.Navy;
		this.labLPAdd.Location = new System.Drawing.Point(7, 49);
		this.labLPAdd.Name = "labLPAdd";
		this.labLPAdd.Size = new System.Drawing.Size(244, 42);
		this.labLPAdd.TabIndex = 0;
		this.labLPAdd.Text = "車牌";
		this.labLPAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panBottom.Controls.Add(this.btnAdd);
		this.panBottom.Controls.Add(this.btnCancel);
		this.panBottom.Controls.Add(this.btnSave);
		this.panBottom.Location = new System.Drawing.Point(0, 670);
		this.panBottom.Name = "panBottom";
		this.panBottom.Size = new System.Drawing.Size(700, 70);
		this.panBottom.TabIndex = 3;
		this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.btnAdd.ForeColor = System.Drawing.Color.Navy;
		this.btnAdd.Location = new System.Drawing.Point(233, 10);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(116, 54);
		this.btnAdd.TabIndex = 3;
		this.btnAdd.Text = "增加";
		this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAdd.UseVisualStyleBackColor = true;
		this.btnAdd.Visible = false;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(700, 740);
		base.Controls.Add(this.panFill);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormLicensePlateCorrect";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormLicensePlateCorrect";
		base.Load += new System.EventHandler(FormLicensePlateCorrect_Load);
		this.panFill.ResumeLayout(false);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.panMiddle.ResumeLayout(false);
		this.panMiddle.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picLP).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataMain).EndInit();
		this.tabPage2.ResumeLayout(false);
		this.panMiddle2.ResumeLayout(false);
		this.panMiddle2.PerformLayout();
		this.panBottom.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
