using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using CarPark.UserControls.SysConfig;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.Forms;

public class FormParkAreaExtend : Form
{
	private ILog Logger;

	private GetParkAreaArgs getArg = new GetParkAreaArgs();

	private List<ParkAreaExtend> m_ParkAreaExtend;

	private List<ParkArea> m_ParkArea;

	private IContainer components = null;

	private Label label1;

	private Panel panBottom;

	private Panel panFill;

	private TabControl tabControl1;

	private Button btnClose;

	private Panel panel1;

	public FormParkAreaExtend()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		btnClose.Image = ImageManager.GetImage("", "cancel");
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		label1.Text = LangManager.GetLangString("CarPark2018.Forms.FormParkAreaExtend.labTitle");
		btnClose.Text = LangManager.GetLangString("CarPark2018.Forms.FormParkAreaExtend.btnClose");
	}

	private void FormParkAreaExtend_Load(object sender, EventArgs e)
	{
		try
		{
			m_ParkAreaExtend = new List<ParkAreaExtend>();
			m_ParkArea = new List<ParkArea>();
			ChargeContext chargeContext = new ChargeContext();
			GetParkAreaReturn parkAreaExtend = chargeContext.CommunicationChannel.GetParkAreaExtend(getArg, out m_ParkAreaExtend, out m_ParkArea);
			chargeContext.CommunicationChannel.Disconnect();
			if (parkAreaExtend.ISOK)
			{
				Init(m_ParkAreaExtend, m_ParkArea);
				return;
			}
			Global.ShowMessage(parkAreaExtend.ErrCode);
			Close();
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Console.WriteLine(ex2.Message);
		}
	}

	private void Init(List<ParkAreaExtend> extendList, List<ParkArea> parkAreaList)
	{
		if (parkAreaList.Count > 0)
		{
			int areaID = Config.AreaCode;
			ParkArea parkArea = parkAreaList.Where((ParkArea m) => m.AreaID == areaID).FirstOrDefault();
			if (parkArea != null)
			{
				TabPage tabPage = new TabPage();
				tabPage.Name = "ParkAreaExtend" + areaID;
				tabPage.Padding = new Padding(3);
				tabPage.Size = new Size(871, 282);
				tabPage.TabIndex = 0;
				tabPage.Text = parkArea.AreaName;
				tabPage.UseVisualStyleBackColor = true;
				tabPage.Font = new Font("微軟雅黑", 25f);
				tabControl1.Controls.Add(tabPage);
				List<ParkAreaExtend> list = extendList.Where((ParkAreaExtend m) => m.AreaID == areaID).ToList();
				if (list != null)
				{
					TabControl tabControl = new TabControl();
					tabControl.Dock = DockStyle.Fill;
					tabControl.Location = new Point(0, 0);
					tabControl.Name = "tabControl" + areaID;
					tabControl.SelectedIndex = 0;
					foreach (ParkAreaExtend item in list)
					{
						TabPage tabPage2 = new TabPage();
						tabPage2.Name = "ParkAreaExtend" + item.AreaID + item.ParkTypeID;
						tabPage2.Padding = new Padding(3);
						tabPage2.Size = new Size(871, 282);
						tabPage2.TabIndex = 0;
						tabPage2.Text = item.ExtendName;
						tabPage2.UseVisualStyleBackColor = true;
						tabPage2.AutoScroll = true;
						UCParkAreaCountEX uCParkAreaCountEX = new UCParkAreaCountEX();
						uCParkAreaCountEX.LoadData(item);
						tabPage2.Controls.Add(uCParkAreaCountEX);
						tabControl.Controls.Add(tabPage2);
					}
					tabPage.Controls.Add(tabControl);
				}
				else
				{
					Logger.Error("List<ParkAreaExtend> is null");
					Console.WriteLine("List<ParkAreaExtend> is null");
				}
			}
			else
			{
				Logger.Error("ParkArea is null");
				Console.WriteLine("ParkArea is null");
			}
		}
		else
		{
			Logger.Error("List<ParkArea> is null");
			Console.WriteLine("List<ParkArea> is null");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
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
		this.label1 = new System.Windows.Forms.Label();
		this.panBottom = new System.Windows.Forms.Panel();
		this.btnClose = new System.Windows.Forms.Button();
		this.panFill = new System.Windows.Forms.Panel();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panBottom.SuspendLayout();
		this.panFill.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.label1.Dock = System.Windows.Forms.DockStyle.Top;
		this.label1.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold);
		this.label1.Location = new System.Drawing.Point(0, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(1077, 52);
		this.label1.TabIndex = 0;
		this.label1.Text = "車位設定";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panBottom.Controls.Add(this.btnClose);
		this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panBottom.Location = new System.Drawing.Point(0, 594);
		this.panBottom.Name = "panBottom";
		this.panBottom.Size = new System.Drawing.Size(1077, 78);
		this.panBottom.TabIndex = 1;
		this.btnClose.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnClose.Location = new System.Drawing.Point(914, 4);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(153, 69);
		this.btnClose.TabIndex = 0;
		this.btnClose.Text = "關閉";
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panFill.Controls.Add(this.tabControl1);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Location = new System.Drawing.Point(0, 52);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(1077, 542);
		this.panFill.TabIndex = 2;
		this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.tabControl1.Location = new System.Drawing.Point(0, 0);
		this.tabControl1.Multiline = true;
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(1073, 538);
		this.tabControl1.TabIndex = 0;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.panFill);
		this.panel1.Controls.Add(this.panBottom);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1079, 674);
		this.panel1.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(1079, 674);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ForeColor = System.Drawing.Color.Navy;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "FormParkAreaExtend";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormParkAreaExtend";
		base.Load += new System.EventHandler(FormParkAreaExtend_Load);
		this.panBottom.ResumeLayout(false);
		this.panFill.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
