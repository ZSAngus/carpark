using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.DB.AdditionalDataSource;
using CarPark.Device;
using CarPark.Lib;
using CarPark2018.Properties;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.Forms;

public class FormTimeCompensationFare : Form
{
	private ILog Logger;

	private TransactionData m_TransactionData = new TransactionData();

	private IContainer components = null;

	private Label labTitle;

	private Panel panel1;

	private Panel panel2;

	private Label labParkType;

	private Label labInTime;

	private ComboBox comboParkType;

	private DateTimePicker dtDate;

	private DateTimePicker dtTime;

	private Button btnOK;

	private Button btnCancel;

	private Panel panFill;

	public FormTimeCompensationFare()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labInTime.Text = LangManager.GetLangString("CarPark.Forms.FormTimeCompensationFare.labInTime");
		labParkType.Text = LangManager.GetLangString("CarPark.Forms.FormTimeCompensationFare.labParkType");
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormTimeCompensationFare.labTitle");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormTimeCompensationFare.btnClose");
		btnOK.Text = LangManager.GetLangString("CarPark.Forms.FormTimeCompensationFare.btnOK");
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
		Close();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		TimeCompensationFareArgs timeCompensationFareArgs = new TimeCompensationFareArgs();
		timeCompensationFareArgs.InTime = new DateTime(dtDate.Value.Year, dtDate.Value.Month, dtDate.Value.Day, dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second);
		timeCompensationFareArgs.parkType = (EnumParkType)comboParkType.SelectedValue;
		timeCompensationFareArgs.PayStationName = Settings.Default.OnlyID;
		timeCompensationFareArgs.ShiftID = DataBuffer.CurrentShiftRecord.ShiftID;
		timeCompensationFareArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
		TimeCompensationFareReturn timeCompensationFareReturn = Common._Carpark2018ServiceContext.CommunicationChannel.TimeCompensationFare(timeCompensationFareArgs, out m_TransactionData);
		if (timeCompensationFareReturn.ISOK)
		{
			try
			{
				FeeInfo feeInfo = new FeeInfo();
				feeInfo.InTime = m_TransactionData.InTime;
				feeInfo.ParkType = (EnumParkType)m_TransactionData.ParkTypeID;
				feeInfo.TicketNumber = m_TransactionData.InCardCode;
				feeInfo.TicketAction = EnumTicketAction.Normal;
				feeInfo.TicketType = EnumTicketType.Compensation;
				FeeInfo feeInfo2 = feeInfo;
				feeInfo2.NeedPrint = true;
				feeInfo2.TicketAction = EnumTicketAction.Normal;
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
			}
			catch (Exception ex2)
			{
				Global.ShowMessage(ex2.Message);
			}
			DeviceManager.FeeCenterModule.EjectTicket();
			Close();
		}
		else
		{
			Global.ShowMessage(timeCompensationFareReturn.ErrCode);
		}
	}

	private void FormTimeCompensationFare_Load(object sender, EventArgs e)
	{
		dtDate.Value = DateTime.Now;
		dtTime.Value = DateTime.Now;
		try
		{
			BindingHelper.BindComboBox<EnumParkTypeSource>(comboParkType);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
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
				btnOK.Focus();
			});
			return feeInfo;
		}
		Global.ShowMessage(LangManager.GetLangString("Alert.Not_Empty_Ticket"));
		feeInfo.TicketAction = EnumTicketAction.Reject;
		Invoke((Action)delegate
		{
			btnOK.Enabled = false;
		});
		return feeInfo;
	}

	private void FormTimeCompensationFare_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent -= FeeCenterModule_TicketScanEvent;
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.labParkType = new System.Windows.Forms.Label();
		this.labInTime = new System.Windows.Forms.Label();
		this.comboParkType = new System.Windows.Forms.ComboBox();
		this.dtDate = new System.Windows.Forms.DateTimePicker();
		this.dtTime = new System.Windows.Forms.DateTimePicker();
		this.panFill = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
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
		this.labTitle.Text = "補票處理";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.panel1.Controls.Add(this.btnOK);
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 582);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(593, 75);
		this.panel1.TabIndex = 1;
		this.btnOK.Enabled = false;
		this.btnOK.ForeColor = System.Drawing.Color.Navy;
		this.btnOK.Location = new System.Drawing.Point(337, 13);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(120, 48);
		this.btnOK.TabIndex = 2;
		this.btnOK.Text = "確認";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(463, 13);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(120, 48);
		this.btnCancel.TabIndex = 1;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.labParkType);
		this.panel2.Controls.Add(this.labInTime);
		this.panel2.Controls.Add(this.comboParkType);
		this.panel2.Controls.Add(this.dtDate);
		this.panel2.Controls.Add(this.dtTime);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 60);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(593, 522);
		this.panel2.TabIndex = 2;
		this.labParkType.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.labParkType.ForeColor = System.Drawing.Color.Navy;
		this.labParkType.Location = new System.Drawing.Point(44, 133);
		this.labParkType.Name = "labParkType";
		this.labParkType.Size = new System.Drawing.Size(192, 35);
		this.labParkType.TabIndex = 34;
		this.labParkType.Text = "車型";
		this.labParkType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labInTime.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.labInTime.ForeColor = System.Drawing.Color.Navy;
		this.labInTime.Location = new System.Drawing.Point(38, 27);
		this.labInTime.Name = "labInTime";
		this.labInTime.Size = new System.Drawing.Size(198, 35);
		this.labInTime.TabIndex = 33;
		this.labInTime.Text = "入場時間";
		this.labInTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.comboParkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkType.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.comboParkType.FormattingEnabled = true;
		this.comboParkType.Location = new System.Drawing.Point(251, 130);
		this.comboParkType.Name = "comboParkType";
		this.comboParkType.Size = new System.Drawing.Size(305, 42);
		this.comboParkType.TabIndex = 32;
		this.dtDate.CalendarFont = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.dtDate.CustomFormat = "yyyy / MM / dd";
		this.dtDate.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtDate.Location = new System.Drawing.Point(251, 21);
		this.dtDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtDate.Name = "dtDate";
		this.dtDate.ShowUpDown = true;
		this.dtDate.Size = new System.Drawing.Size(305, 43);
		this.dtDate.TabIndex = 30;
		this.dtTime.CustomFormat = "       HH : mm";
		this.dtTime.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.dtTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtTime.Location = new System.Drawing.Point(251, 75);
		this.dtTime.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
		this.dtTime.Name = "dtTime";
		this.dtTime.ShowUpDown = true;
		this.dtTime.Size = new System.Drawing.Size(305, 43);
		this.dtTime.TabIndex = 31;
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.panel2);
		this.panFill.Controls.Add(this.panel1);
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(595, 659);
		this.panFill.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(595, 659);
		base.Controls.Add(this.panFill);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "FormTimeCompensationFare";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormTimeCompensationFare";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormTimeCompensationFare_FormClosing);
		base.Load += new System.EventHandler(FormTimeCompensationFare_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panFill.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
