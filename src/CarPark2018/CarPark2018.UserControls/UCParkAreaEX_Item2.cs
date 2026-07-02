using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.UserControls;

public class UCParkAreaEX_Item2 : UserControl
{
	private bool isFull = false;

	private ParkAreaExtend m_parkAreaExtend;

	private static ILog Logger;

	public string cn = "";

	public string en = "";

	private IContainer components = null;

	public Label parkTypeInfo;

	public Label labTimeRemain;

	public Label labStaffRemain;

	public PictureBox pbPrivateState;

	public Label labStudentRemain;

	public UCParkAreaEX_Item2()
	{
		InitializeComponent();
	}

	static UCParkAreaEX_Item2()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public UCParkAreaEX_Item2(ParkAreaExtend pae)
	{
		InitializeComponent();
		m_parkAreaExtend = pae;
		base.Name = "Area" + pae.AreaID + pae.ParkTypeID;
		base.Tag = pae.CustomFunnSigh;
		pbPrivateState.Image = ImageManager.GetImage("UCParkAreaEX", "Full2");
		parkTypeInfo.BackgroundImageLayout = ImageLayout.Stretch;
		LangManager.LanguageChangedEvent += LangManager_LanguageChangedEvent;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void pbPrivateState_Click(object sender, EventArgs e)
	{
		try
		{
			DataBuffer2018.CheckRole(MethodBase.GetCurrentMethod());
			if (!(bool)base.Tag)
			{
				if (Global.ShowDialog(LangManager.GetLangString("ShowMessage.FullOk"), OkFocus: false) == DialogResult.Cancel)
				{
					return;
				}
			}
			else if (Global.ShowDialog(LangManager.GetLangString("ShowMessage.FullCancel"), OkFocus: false) == DialogResult.Cancel)
			{
				return;
			}
			ManualChangeArgs manualChangeArgs = new ManualChangeArgs(Settings.Default.OnlyID);
			manualChangeArgs.parkType = m_parkAreaExtend.ParkType;
			manualChangeArgs.ParkAreaExtendID = m_parkAreaExtend.AreaID;
			manualChangeArgs.ShiffCode = ((DataBuffer2018.CurrentStaff == null) ? "Auto" : DataBuffer2018.CurrentStaff.StaffCode);
			manualChangeArgs.OperationPC = Settings.Default.OnlyID;
			if (!(bool)base.Tag)
			{
				manualChangeArgs.ManualFull = true;
			}
			else
			{
				manualChangeArgs.ManualFull = false;
			}
			try
			{
				ChargeContext chargeContext = new ChargeContext();
				if (chargeContext.CommunicationChannel.ManualChange(manualChangeArgs))
				{
					Console.WriteLine("true");
				}
				else
				{
					Console.WriteLine("false");
				}
				chargeContext.CommunicationChannel.Disconnect();
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				MessageBox.Show(ex.Message);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception ex3)
		{
			Global.ShowMessage(ex3.Message);
		}
	}

	public void setFull_callBack(bool isFull)
	{
		this.isFull = isFull;
		base.Tag = isFull;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		if ((bool)base.Tag && currentLang == SysLanguage.CHT)
		{
			pbPrivateState.Image = ImageManager.GetImage("UCParkAreaEX", "Full1");
		}
		else if ((bool)base.Tag && currentLang == SysLanguage.ENG)
		{
			pbPrivateState.Image = ImageManager.GetImage("UCParkAreaEX", "Full1En");
		}
		else if (!(bool)base.Tag && currentLang == SysLanguage.CHT)
		{
			pbPrivateState.Image = ImageManager.GetImage("UCParkAreaEX", "Full2");
		}
		else if (!(bool)base.Tag && currentLang == SysLanguage.ENG)
		{
			pbPrivateState.Image = ImageManager.GetImage("UCParkAreaEX", "Full2En");
		}
		if (currentLang == SysLanguage.CHT)
		{
			parkTypeInfo.Text = cn;
		}
		else
		{
			parkTypeInfo.Text = en;
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
		this.parkTypeInfo = new System.Windows.Forms.Label();
		this.labTimeRemain = new System.Windows.Forms.Label();
		this.pbPrivateState = new System.Windows.Forms.PictureBox();
		this.labStaffRemain = new System.Windows.Forms.Label();
		this.labStudentRemain = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.pbPrivateState).BeginInit();
		base.SuspendLayout();
		this.parkTypeInfo.BackColor = System.Drawing.Color.Transparent;
		this.parkTypeInfo.Font = new System.Drawing.Font("微软雅黑", 8f);
		this.parkTypeInfo.ForeColor = System.Drawing.SystemColors.Window;
		this.parkTypeInfo.Location = new System.Drawing.Point(3, 2);
		this.parkTypeInfo.Name = "parkTypeInfo";
		this.parkTypeInfo.Size = new System.Drawing.Size(80, 50);
		this.parkTypeInfo.TabIndex = 1;
		this.parkTypeInfo.Text = "一二三四五六七";
		this.parkTypeInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.labTimeRemain.BackColor = System.Drawing.Color.Transparent;
		this.labTimeRemain.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.labTimeRemain.ForeColor = System.Drawing.Color.White;
		this.labTimeRemain.Location = new System.Drawing.Point(89, 12);
		this.labTimeRemain.Name = "labTimeRemain";
		this.labTimeRemain.Size = new System.Drawing.Size(55, 30);
		this.labTimeRemain.TabIndex = 7;
		this.labTimeRemain.Text = "1001";
		this.labTimeRemain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.pbPrivateState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pbPrivateState.Cursor = System.Windows.Forms.Cursors.Hand;
		this.pbPrivateState.Location = new System.Drawing.Point(287, 4);
		this.pbPrivateState.Name = "pbPrivateState";
		this.pbPrivateState.Size = new System.Drawing.Size(47, 47);
		this.pbPrivateState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pbPrivateState.TabIndex = 10;
		this.pbPrivateState.TabStop = false;
		this.pbPrivateState.Click += new System.EventHandler(pbPrivateState_Click);
		this.labStaffRemain.BackColor = System.Drawing.Color.Transparent;
		this.labStaffRemain.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.labStaffRemain.ForeColor = System.Drawing.Color.White;
		this.labStaffRemain.Location = new System.Drawing.Point(150, 12);
		this.labStaffRemain.Name = "labStaffRemain";
		this.labStaffRemain.Size = new System.Drawing.Size(55, 30);
		this.labStaffRemain.TabIndex = 7;
		this.labStaffRemain.Text = "1001";
		this.labStaffRemain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labStudentRemain.BackColor = System.Drawing.Color.Transparent;
		this.labStudentRemain.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.labStudentRemain.ForeColor = System.Drawing.Color.White;
		this.labStudentRemain.Location = new System.Drawing.Point(211, 12);
		this.labStudentRemain.Name = "labStudentRemain";
		this.labStudentRemain.Size = new System.Drawing.Size(55, 30);
		this.labStudentRemain.TabIndex = 7;
		this.labStudentRemain.Text = "1001";
		this.labStudentRemain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Transparent;
		base.Controls.Add(this.labStudentRemain);
		base.Controls.Add(this.labStaffRemain);
		base.Controls.Add(this.pbPrivateState);
		base.Controls.Add(this.parkTypeInfo);
		base.Controls.Add(this.labTimeRemain);
		this.DoubleBuffered = true;
		base.Name = "UCParkAreaEX_Item2";
		base.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
		base.Size = new System.Drawing.Size(350, 55);
		((System.ComponentModel.ISupportInitialize)this.pbPrivateState).EndInit();
		base.ResumeLayout(false);
	}
}
