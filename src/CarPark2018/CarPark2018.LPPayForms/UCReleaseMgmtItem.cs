using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using CarPark2018.Properties;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.LPPayForms;

public class UCReleaseMgmtItem : UserControl
{
	public ParkGate mParkGate = null;

	private static ILog Logger;

	private IContainer components = null;

	public Label lab_GateName;

	public TextBox txt_LicensePlate;

	public Label lab_lp;

	private Button btnBarUp;

	public UCReleaseMgmtItem()
	{
		InitializeComponent();
	}

	static UCReleaseMgmtItem()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public UCReleaseMgmtItem(ParkGate mGate)
	{
		InitializeComponent();
		base.Name = "Gate" + mGate.GateID;
		lab_GateName.Text = mGate.GateName;
		btnBarUp.Image = ImageManager.GetImage("UCGatesEX", "up");
		mParkGate = mGate;
		BackgroundImage = ImageManager.GetImage("UCGatesEX", "GateBackground");
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
	}

	private void btnBarUp_Click(object sender, EventArgs e)
	{
		try
		{
			if (Global.ShowDialog(LangManager.GetLangString("ShowMessage.UpBar"), OkFocus: false) == DialogResult.Cancel)
			{
				return;
			}
			ManualUpBarArgs manualUpBarArgs = new ManualUpBarArgs(Settings.Default.OnlyID);
			manualUpBarArgs.GateID = mParkGate.GateID;
			manualUpBarArgs.OperationPC = Settings.Default.OnlyID;
			manualUpBarArgs.ShiffCode = DataBuffer2018.CurrentStaff.StaffCode;
			manualUpBarArgs.Extend1 = txt_LicensePlate.Text.ToUpper().Trim();
			try
			{
				bool flag = Common._Carpark2018ServiceContext.CommunicationChannel.ManualUpBar(manualUpBarArgs);
			}
			catch (TimeoutException)
			{
				Global.ShowMessage("操作超時，請重新操作");
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2);
				Global.ShowMessage(ex2.Message);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception ex4)
		{
			Global.ShowMessage(ex4.Message);
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
		this.lab_GateName = new System.Windows.Forms.Label();
		this.txt_LicensePlate = new System.Windows.Forms.TextBox();
		this.lab_lp = new System.Windows.Forms.Label();
		this.btnBarUp = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.lab_GateName.BackColor = System.Drawing.Color.Transparent;
		this.lab_GateName.Font = new System.Drawing.Font("新細明體", 10f, System.Drawing.FontStyle.Bold);
		this.lab_GateName.ForeColor = System.Drawing.Color.Navy;
		this.lab_GateName.Location = new System.Drawing.Point(3, 10);
		this.lab_GateName.Name = "lab_GateName";
		this.lab_GateName.Size = new System.Drawing.Size(144, 30);
		this.lab_GateName.TabIndex = 8;
		this.lab_GateName.Text = "label1";
		this.lab_GateName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txt_LicensePlate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txt_LicensePlate.Font = new System.Drawing.Font("黑体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.txt_LicensePlate.Location = new System.Drawing.Point(24, 84);
		this.txt_LicensePlate.MaxLength = 7;
		this.txt_LicensePlate.Name = "txt_LicensePlate";
		this.txt_LicensePlate.Size = new System.Drawing.Size(101, 26);
		this.txt_LicensePlate.TabIndex = 9;
		this.txt_LicensePlate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lab_lp.BackColor = System.Drawing.Color.Transparent;
		this.lab_lp.Font = new System.Drawing.Font("新細明體", 10f, System.Drawing.FontStyle.Bold);
		this.lab_lp.ForeColor = System.Drawing.Color.Navy;
		this.lab_lp.Location = new System.Drawing.Point(3, 46);
		this.lab_lp.Name = "lab_lp";
		this.lab_lp.Size = new System.Drawing.Size(144, 30);
		this.lab_lp.TabIndex = 10;
		this.lab_lp.Text = "車牌";
		this.lab_lp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnBarUp.BackColor = System.Drawing.Color.Transparent;
		this.btnBarUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btnBarUp.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnBarUp.FlatAppearance.BorderSize = 0;
		this.btnBarUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btnBarUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btnBarUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnBarUp.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.btnBarUp.ForeColor = System.Drawing.Color.White;
		this.btnBarUp.Location = new System.Drawing.Point(58, 119);
		this.btnBarUp.Name = "btnBarUp";
		this.btnBarUp.Size = new System.Drawing.Size(30, 20);
		this.btnBarUp.TabIndex = 11;
		this.btnBarUp.UseVisualStyleBackColor = false;
		this.btnBarUp.Click += new System.EventHandler(btnBarUp_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.btnBarUp);
		base.Controls.Add(this.lab_lp);
		base.Controls.Add(this.txt_LicensePlate);
		base.Controls.Add(this.lab_GateName);
		base.Name = "UCReleaseMgmtItem";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
