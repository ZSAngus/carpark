using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.UserControls;

public class UCLicensePlate : UserControl
{
	private ILog Logger;

	private int TimeOutNum;

	public bool IsPass = false;

	private ExitContrastArgs m_ExitContrastInfo = null;

	private RecordContrastArgs m_RecordContrastInfo = null;

	private IContainer components = null;

	private Label labTimeOut;

	private Label labVS;

	private Button btnNoPass;

	private Button btnYesPass;

	private PictureBox picExit;

	private PictureBox picEnter;

	private Timer timer1;

	private Label labInLPRS;

	private Label labOutLPRS;

	private Label labInLPRSTest;

	private Label labOutLPRSTest;

	public UCLicensePlate()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		LangManager.LanguageChangedEvent += LangManager_LanguageChangedEvent;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		btnNoPass.Text = LangManager.GetLangString("CarPark2018.Forms.UCLicensePlate.btnNoPass");
		btnYesPass.Text = LangManager.GetLangString("CarPark2018.Forms.UCLicensePlate.btnYesPass");
		labInLPRSTest.Text = LangManager.GetLangString("CarPark2018.Forms.UCLicensePlate.labInLPRSTest");
		labOutLPRSTest.Text = LangManager.GetLangString("CarPark2018.Forms.UCLicensePlate.labOutLPRSTest");
	}

	private void UCLicensePlate_Load(object sender, EventArgs e)
	{
		picEnter.Image = ImageManager.GetImage("NanYi", "Private");
		picExit.Image = ImageManager.GetImage("NanYi", "Private");
		labTimeOut.Text = "";
	}

	public void ExitStart(int TimeOut, string EnterImagePath, string ExitImagePath)
	{
		if (File.Exists(EnterImagePath))
		{
			picEnter.Image = Image.FromFile(EnterImagePath);
		}
		else
		{
			picEnter.Image = ImageManager.GetImage("", "cancel");
		}
		if (File.Exists(ExitImagePath))
		{
			picExit.Image = Image.FromFile(ExitImagePath);
		}
		else
		{
			picExit.Image = ImageManager.GetImage("", "cancel");
		}
		labTimeOut.Text = TimeOut.ToString();
		TimeOutNum = TimeOut;
		ControlsVisible(status: true);
		timer1.Start();
	}

	public void ExitStart(ExitContrastArgs exitContrastInfo)
	{
		if (timer1.Enabled)
		{
			Common._Carpark2018ServiceContext.CommunicationChannel.ExitContrast(exitContrastInfo);
			return;
		}
		m_RecordContrastInfo = null;
		m_ExitContrastInfo = exitContrastInfo;
		m_ExitContrastInfo.ShiffCode = DataBuffer2018.CurrentStaff.StaffCode;
		Console.WriteLine(Config.LicensePlatePath + exitContrastInfo.EnterImagePath);
		Console.WriteLine(Config.LicensePlatePath + exitContrastInfo.ExitImagePath);
		if (File.Exists(Config.LicensePlatePath + exitContrastInfo.EnterImagePath))
		{
			picEnter.Image = Image.FromFile(Config.LicensePlatePath + exitContrastInfo.EnterImagePath);
		}
		else
		{
			picEnter.Image = ImageManager.GetImage("", "cancel");
		}
		if (File.Exists(Config.LicensePlatePath + exitContrastInfo.ExitImagePath))
		{
			picExit.Image = Image.FromFile(Config.LicensePlatePath + exitContrastInfo.ExitImagePath);
		}
		else
		{
			picExit.Image = ImageManager.GetImage("", "cancel");
		}
		labTimeOut.Text = exitContrastInfo.ShowTime.ToString();
		TimeOutNum = exitContrastInfo.ShowTime;
		labInLPRS.Text = exitContrastInfo.EnterValue;
		labOutLPRS.Text = exitContrastInfo.ExitValue;
		ControlsVisible(status: true);
		timer1.Start();
		timer1.Enabled = true;
	}

	public void EnterStart(RecordContrastArgs recordContrastInfo)
	{
		if (timer1.Enabled)
		{
			Common._Carpark2018ServiceContext.CommunicationChannel.RecordContrast(recordContrastInfo);
			return;
		}
		m_ExitContrastInfo = null;
		m_RecordContrastInfo = recordContrastInfo;
		m_RecordContrastInfo.ShiffCode = DataBuffer2018.CurrentStaff.StaffCode;
		if (File.Exists(Config.LicensePlatePath + recordContrastInfo.ImagePath))
		{
			picEnter.Image = Image.FromFile(Config.LicensePlatePath + recordContrastInfo.ImagePath);
		}
		else
		{
			picEnter.Image = ImageManager.GetImage("", "cancel");
		}
		labTimeOut.Text = recordContrastInfo.ShowTime.ToString();
		TimeOutNum = recordContrastInfo.ShowTime;
		labInLPRS.Text = recordContrastInfo.Registration;
		ControlsVisible(status: true);
		timer1.Start();
		timer1.Enabled = true;
	}

	public void EnterStart(int timeOut, string EnterImagePath)
	{
		if (File.Exists(EnterImagePath))
		{
			picEnter.Image = Image.FromFile(EnterImagePath);
		}
		else
		{
			picEnter.Image = ImageManager.GetImage("", "cancel");
		}
		labTimeOut.Text = timeOut.ToString();
		TimeOutNum = timeOut;
		ControlsVisible(status: true);
		timer1.Start();
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		try
		{
			if (TimeOutNum > 0)
			{
				TimeOutNum--;
				labTimeOut.Text = TimeOutNum.ToString();
				return;
			}
			ControlsVisible(status: false);
			timer1.Stop();
			picExit.Image = ImageManager.GetImage("NanYi", "Private");
			picEnter.Image = ImageManager.GetImage("NanYi", "Private");
			labInLPRS.Text = "";
			labOutLPRS.Text = "";
			btnIsPass(status: false);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
	}

	private void Button_Click(object sender, EventArgs a)
	{
		Button button = (Button)sender;
		if ((string)button.Tag == "true")
		{
			IsPass = true;
		}
		else
		{
			IsPass = false;
		}
		ControlsVisible(status: false);
		timer1.Stop();
		btnIsPass(IsPass);
		picExit.Image = ImageManager.GetImage("NanYi", "Private");
		picEnter.Image = ImageManager.GetImage("NanYi", "Private");
		labInLPRS.Text = "";
		labOutLPRS.Text = "";
	}

	private void btnIsPass(bool status)
	{
		try
		{
			if (m_ExitContrastInfo == null)
			{
				m_RecordContrastInfo.IsPass = status;
				Common._Carpark2018ServiceContext.CommunicationChannel.RecordContrast(m_RecordContrastInfo);
			}
			else
			{
				m_ExitContrastInfo.IsPass = status;
				Common._Carpark2018ServiceContext.CommunicationChannel.ExitContrast(m_ExitContrastInfo);
			}
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
	}

	private void ControlsVisible(bool status)
	{
		labTimeOut.Visible = status;
		btnYesPass.Visible = status;
		btnNoPass.Visible = status;
		labVS.Visible = status;
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
		this.labTimeOut = new System.Windows.Forms.Label();
		this.labVS = new System.Windows.Forms.Label();
		this.btnNoPass = new System.Windows.Forms.Button();
		this.btnYesPass = new System.Windows.Forms.Button();
		this.picExit = new System.Windows.Forms.PictureBox();
		this.picEnter = new System.Windows.Forms.PictureBox();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.labInLPRS = new System.Windows.Forms.Label();
		this.labOutLPRS = new System.Windows.Forms.Label();
		this.labInLPRSTest = new System.Windows.Forms.Label();
		this.labOutLPRSTest = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.picExit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picEnter).BeginInit();
		base.SuspendLayout();
		this.labTimeOut.BackColor = System.Drawing.Color.Transparent;
		this.labTimeOut.Font = new System.Drawing.Font("微软雅黑", 50.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labTimeOut.ForeColor = System.Drawing.Color.Red;
		this.labTimeOut.Location = new System.Drawing.Point(607, 98);
		this.labTimeOut.Name = "labTimeOut";
		this.labTimeOut.Size = new System.Drawing.Size(130, 90);
		this.labTimeOut.TabIndex = 11;
		this.labTimeOut.Text = "15";
		this.labTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labTimeOut.Visible = false;
		this.labVS.BackColor = System.Drawing.Color.Transparent;
		this.labVS.Font = new System.Drawing.Font("微软雅黑", 50.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labVS.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.labVS.Location = new System.Drawing.Point(607, 8);
		this.labVS.Name = "labVS";
		this.labVS.Size = new System.Drawing.Size(130, 90);
		this.labVS.TabIndex = 12;
		this.labVS.Text = "VS";
		this.labVS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labVS.Visible = false;
		this.btnNoPass.Font = new System.Drawing.Font("微软雅黑", 30f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnNoPass.Location = new System.Drawing.Point(607, 277);
		this.btnNoPass.Name = "btnNoPass";
		this.btnNoPass.Size = new System.Drawing.Size(130, 80);
		this.btnNoPass.TabIndex = 10;
		this.btnNoPass.Tag = "false";
		this.btnNoPass.Text = "禁行";
		this.btnNoPass.UseVisualStyleBackColor = true;
		this.btnNoPass.Visible = false;
		this.btnNoPass.Click += new System.EventHandler(Button_Click);
		this.btnYesPass.Font = new System.Drawing.Font("微软雅黑", 30f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnYesPass.Location = new System.Drawing.Point(607, 191);
		this.btnYesPass.Name = "btnYesPass";
		this.btnYesPass.Size = new System.Drawing.Size(130, 80);
		this.btnYesPass.TabIndex = 9;
		this.btnYesPass.Tag = "true";
		this.btnYesPass.Text = "通過";
		this.btnYesPass.UseVisualStyleBackColor = true;
		this.btnYesPass.Visible = false;
		this.btnYesPass.Click += new System.EventHandler(Button_Click);
		this.picExit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.picExit.Location = new System.Drawing.Point(750, 40);
		this.picExit.Name = "picExit";
		this.picExit.Size = new System.Drawing.Size(590, 325);
		this.picExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picExit.TabIndex = 7;
		this.picExit.TabStop = false;
		this.picEnter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.picEnter.Location = new System.Drawing.Point(3, 40);
		this.picEnter.Name = "picEnter";
		this.picEnter.Size = new System.Drawing.Size(590, 325);
		this.picEnter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picEnter.TabIndex = 8;
		this.picEnter.TabStop = false;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.labInLPRS.Location = new System.Drawing.Point(185, 4);
		this.labInLPRS.Name = "labInLPRS";
		this.labInLPRS.Size = new System.Drawing.Size(408, 35);
		this.labInLPRS.TabIndex = 13;
		this.labInLPRS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labOutLPRS.Location = new System.Drawing.Point(932, 2);
		this.labOutLPRS.Name = "labOutLPRS";
		this.labOutLPRS.Size = new System.Drawing.Size(408, 35);
		this.labOutLPRS.TabIndex = 13;
		this.labOutLPRS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labInLPRSTest.Location = new System.Drawing.Point(3, 4);
		this.labInLPRSTest.Name = "labInLPRSTest";
		this.labInLPRSTest.Size = new System.Drawing.Size(176, 35);
		this.labInLPRSTest.TabIndex = 13;
		this.labInLPRSTest.Text = "入口結果：";
		this.labInLPRSTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labOutLPRSTest.Location = new System.Drawing.Point(750, 2);
		this.labOutLPRSTest.Name = "labOutLPRSTest";
		this.labOutLPRSTest.Size = new System.Drawing.Size(176, 35);
		this.labOutLPRSTest.TabIndex = 13;
		this.labOutLPRSTest.Text = "出口結果：";
		this.labOutLPRSTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(16f, 35f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.labOutLPRS);
		base.Controls.Add(this.labOutLPRSTest);
		base.Controls.Add(this.labInLPRSTest);
		base.Controls.Add(this.labInLPRS);
		base.Controls.Add(this.labTimeOut);
		base.Controls.Add(this.labVS);
		base.Controls.Add(this.btnNoPass);
		base.Controls.Add(this.btnYesPass);
		base.Controls.Add(this.picExit);
		base.Controls.Add(this.picEnter);
		this.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
		base.Name = "UCLicensePlate";
		base.Size = new System.Drawing.Size(1340, 365);
		base.Load += new System.EventHandler(UCLicensePlate_Load);
		((System.ComponentModel.ISupportInitialize)this.picExit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picEnter).EndInit();
		base.ResumeLayout(false);
	}
}
