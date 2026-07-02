using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.Lib;
using CarPark2018.Properties;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.Forms;

public class FormDisabilityDialog : Form
{
	public ParkGate m_ParkGate;

	private static ILog Logger;

	private EnumParkType m_ParkType;

	private DisabilityPressArgs m_PressArg;

	private IContainer components = null;

	private Panel panMain;

	private Label labTitle;

	private Panel panMain2;

	private Button btnCancel;

	private Button btnOther;

	private Button btnTime;

	public FormDisabilityDialog()
	{
		InitializeComponent();
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	static FormDisabilityDialog()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public FormDisabilityDialog(string title, DisabilityPressArgs pressArg)
	{
		InitializeComponent();
		m_PressArg = pressArg;
		if (pressArg.PressParkType == EnumParkType.Other)
		{
			m_ParkType = EnumParkType.Other;
			labTitle.Text = title + "  " + LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.btnDisability");
		}
		else if (pressArg.PressParkType == EnumParkType.Charging)
		{
			m_ParkType = EnumParkType.Charging;
			labTitle.Text = title + "  " + LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.btnElectric");
		}
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormDisabilityDialog.btnCancel");
		btnOther.Text = LangManager.GetLangString("CarPark.Forms.FormDisabilityDialog.btnOther");
		btnTime.Text = LangManager.GetLangString("CarPark.Forms.FormDisabilityDialog.btnTime");
	}

	private void btnTime_Click(object sender, EventArgs e)
	{
		if (m_ParkGate != null)
		{
			Console.WriteLine(m_PressArg.PrintParkType.ToString() + "\t" + m_PressArg.PressParkType);
			DisabilityPressArgs disabilityPressArgs = new DisabilityPressArgs(m_ParkGate.GateID.ToString());
			disabilityPressArgs.GateID = m_ParkGate.GateID;
			disabilityPressArgs.PrintParkType = m_PressArg.PrintParkType;
			disabilityPressArgs.PressParkType = m_PressArg.PressParkType;
			disabilityPressArgs.ShiffCode = DataBuffer2018.CurrentStaff.StaffCode;
			disabilityPressArgs.OperationPC = Settings.Default.OnlyID;
			disabilityPressArgs.IsCancel = false;
			disabilityPressArgs.Extend1 = "Time";
			try
			{
				Common._Carpark2018ServiceContext.CommunicationChannel.DisabilityPress(disabilityPressArgs);
			}
			catch (TimeoutException message)
			{
				Logger.Error(message);
				Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				Global.ShowMessage(ex.Message);
			}
		}
		Close();
	}

	private void btnOther_Click(object sender, EventArgs e)
	{
		if (m_ParkGate != null)
		{
			DisabilityPressArgs disabilityPressArgs = new DisabilityPressArgs(m_ParkGate.GateID.ToString());
			disabilityPressArgs.GateID = m_ParkGate.GateID;
			disabilityPressArgs.PrintParkType = m_PressArg.PrintParkType;
			disabilityPressArgs.PressParkType = m_PressArg.PrintParkType;
			disabilityPressArgs.ShiffCode = DataBuffer2018.CurrentStaff.StaffCode;
			disabilityPressArgs.OperationPC = Settings.Default.OnlyID;
			disabilityPressArgs.IsCancel = false;
			disabilityPressArgs.Extend1 = "Other";
			try
			{
				Common._Carpark2018ServiceContext.CommunicationChannel.DisabilityPress(disabilityPressArgs);
			}
			catch (TimeoutException message)
			{
				Logger.Error(message);
				Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				Global.ShowMessage(ex.Message);
			}
		}
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		if (m_ParkGate != null)
		{
			DisabilityPressArgs disabilityPressArgs = new DisabilityPressArgs(m_ParkGate.GateID.ToString());
			disabilityPressArgs.GateID = m_ParkGate.GateID;
			disabilityPressArgs.PressParkType = m_PressArg.PressParkType;
			disabilityPressArgs.PrintParkType = m_PressArg.PrintParkType;
			disabilityPressArgs.ShiffCode = DataBuffer2018.CurrentStaff.StaffCode;
			disabilityPressArgs.OperationPC = Settings.Default.OnlyID;
			disabilityPressArgs.IsCancel = true;
			disabilityPressArgs.Extend1 = "Other";
			try
			{
				Common._Carpark2018ServiceContext.CommunicationChannel.DisabilityPress(disabilityPressArgs);
			}
			catch (TimeoutException message)
			{
				Logger.Error(message);
				Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				Global.ShowMessage(ex.Message);
			}
		}
		Close();
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
		this.panMain = new System.Windows.Forms.Panel();
		this.panMain2 = new System.Windows.Forms.Panel();
		this.btnCancel = new System.Windows.Forms.Button();
		this.btnOther = new System.Windows.Forms.Button();
		this.btnTime = new System.Windows.Forms.Button();
		this.labTitle = new System.Windows.Forms.Label();
		this.panMain.SuspendLayout();
		this.panMain2.SuspendLayout();
		base.SuspendLayout();
		this.panMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panMain.Controls.Add(this.panMain2);
		this.panMain.Controls.Add(this.labTitle);
		this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panMain.Location = new System.Drawing.Point(0, 0);
		this.panMain.Name = "panMain";
		this.panMain.Size = new System.Drawing.Size(700, 362);
		this.panMain.TabIndex = 0;
		this.panMain2.Controls.Add(this.btnCancel);
		this.panMain2.Controls.Add(this.btnOther);
		this.panMain2.Controls.Add(this.btnTime);
		this.panMain2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panMain2.Location = new System.Drawing.Point(0, 60);
		this.panMain2.Name = "panMain2";
		this.panMain2.Size = new System.Drawing.Size(698, 300);
		this.panMain2.TabIndex = 1;
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnCancel.Location = new System.Drawing.Point(425, 115);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(140, 70);
		this.btnCancel.TabIndex = 0;
		this.btnCancel.Text = "取消";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.btnOther.Enabled = false;
		this.btnOther.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnOther.Location = new System.Drawing.Point(279, 115);
		this.btnOther.Name = "btnOther";
		this.btnOther.Size = new System.Drawing.Size(140, 70);
		this.btnOther.TabIndex = 0;
		this.btnOther.Text = "電子支付";
		this.btnOther.UseVisualStyleBackColor = true;
		this.btnOther.Click += new System.EventHandler(btnOther_Click);
		this.btnTime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnTime.Location = new System.Drawing.Point(133, 115);
		this.btnTime.Name = "btnTime";
		this.btnTime.Size = new System.Drawing.Size(140, 70);
		this.btnTime.TabIndex = 0;
		this.btnTime.Text = "時租";
		this.btnTime.UseVisualStyleBackColor = true;
		this.btnTime.Click += new System.EventHandler(btnTime_Click);
		this.labTitle.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 30f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(698, 60);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "傷殘車";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 17f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		base.ClientSize = new System.Drawing.Size(700, 362);
		base.Controls.Add(this.panMain);
		this.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "FormDisabilityDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormDisabilityDialog";
		this.panMain.ResumeLayout(false);
		this.panMain2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
