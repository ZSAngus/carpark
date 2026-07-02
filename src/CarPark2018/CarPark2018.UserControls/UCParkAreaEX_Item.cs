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

public class UCParkAreaEX_Item : UserControl
{
	private bool isFull = false;

	private ParkAreaExtend m_parkAreaExtend;

	private static ILog Logger;

	public string cn = "";

	public string en = "";

	private IContainer components = null;

	public Label parkTypeInfo;

	public Label lab_SpCount;

	public Label labExCount;

	public PictureBox pbPrivateState;

	public UCParkAreaEX_Item()
	{
		InitializeComponent();
	}

	static UCParkAreaEX_Item()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public UCParkAreaEX_Item(ParkAreaExtend pae)
	{
		InitializeComponent();
		m_parkAreaExtend = pae;
		base.Name = "Area" + pae.AreaID + pae.ParkTypeID;
		base.Tag = pae.CustomFunnSigh;
		pbPrivateState.Image = ImageManager.GetImage("UCParkAreaEX", "Full2");
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
		catch (Exception ex2)
		{
			Global.ShowMessage(ex2.Message);
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
		this.lab_SpCount = new System.Windows.Forms.Label();
		this.pbPrivateState = new System.Windows.Forms.PictureBox();
		this.labExCount = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.pbPrivateState).BeginInit();
		base.SuspendLayout();
		this.parkTypeInfo.BackColor = System.Drawing.Color.Transparent;
		this.parkTypeInfo.Font = new System.Drawing.Font("微软雅黑", 8f);
		this.parkTypeInfo.ForeColor = System.Drawing.SystemColors.Window;
		this.parkTypeInfo.Location = new System.Drawing.Point(3, 2);
		this.parkTypeInfo.Name = "parkTypeInfo";
		this.parkTypeInfo.Size = new System.Drawing.Size(90, 60);
		this.parkTypeInfo.TabIndex = 1;
		this.parkTypeInfo.Text = "一二三四五六七";
		this.parkTypeInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.lab_SpCount.BackColor = System.Drawing.Color.Transparent;
		this.lab_SpCount.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.lab_SpCount.ForeColor = System.Drawing.Color.Navy;
		this.lab_SpCount.Location = new System.Drawing.Point(107, 15);
		this.lab_SpCount.Name = "lab_SpCount";
		this.lab_SpCount.Size = new System.Drawing.Size(60, 34);
		this.lab_SpCount.TabIndex = 7;
		this.lab_SpCount.Text = "1001";
		this.lab_SpCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.pbPrivateState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pbPrivateState.Cursor = System.Windows.Forms.Cursors.Hand;
		this.pbPrivateState.Location = new System.Drawing.Point(277, 4);
		this.pbPrivateState.Name = "pbPrivateState";
		this.pbPrivateState.Size = new System.Drawing.Size(57, 57);
		this.pbPrivateState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pbPrivateState.TabIndex = 10;
		this.pbPrivateState.TabStop = false;
		this.pbPrivateState.Click += new System.EventHandler(pbPrivateState_Click);
		this.labExCount.BackColor = System.Drawing.Color.Transparent;
		this.labExCount.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labExCount.ForeColor = System.Drawing.Color.White;
		this.labExCount.Location = new System.Drawing.Point(192, 15);
		this.labExCount.Name = "labExCount";
		this.labExCount.Size = new System.Drawing.Size(60, 34);
		this.labExCount.TabIndex = 7;
		this.labExCount.Text = "1001";
		this.labExCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Transparent;
		base.Controls.Add(this.labExCount);
		base.Controls.Add(this.pbPrivateState);
		base.Controls.Add(this.parkTypeInfo);
		base.Controls.Add(this.lab_SpCount);
		base.Name = "UCParkAreaEX_Item";
		base.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
		base.Size = new System.Drawing.Size(350, 65);
		((System.ComponentModel.ISupportInitialize)this.pbPrivateState).EndInit();
		base.ResumeLayout(false);
	}
}
