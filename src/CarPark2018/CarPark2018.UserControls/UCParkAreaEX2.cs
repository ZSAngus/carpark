using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using SkyInno.Lang;

namespace CarPark2018.UserControls;

public class UCParkAreaEX2 : UserControl
{
	private Point m_itemLocation = new Point(5, 2);

	public List<UCParkAreaEX_Item2> m_AreaItems = null;

	private string cn = "";

	private string en = "";

	private IContainer components = null;

	private MyPanel panel_context;

	private Label labelTime;

	private Label labStaff;

	private Panel panel1;

	private Label labFull;

	private Label labelStudent;

	public UCParkAreaEX2()
	{
		InitializeComponent();
		BackgroundImage = ImageManager.GetImage("Main", "PasstraceBox");
	}

	private void UCParkAreaEX2_Load(object sender, EventArgs e)
	{
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		m_AreaItems = new List<UCParkAreaEX_Item2>();
		if (DataBuffer2018.ParkAreaExtends != null)
		{
			foreach (ParkAreaExtend parkAreaExtend in DataBuffer2018.ParkAreaExtends)
			{
				AddItem(parkAreaExtend);
			}
		}
		LangManager.LanguageChangedEvent += LangManager_LanguageChangedEvent;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	public void AddItem(ParkAreaExtend pae)
	{
		UCParkAreaEX_Item2 uCParkAreaEX_Item = new UCParkAreaEX_Item2(pae);
		string areaName = DataBuffer2018.ParkAreas.First((ParkArea m) => m.AreaID == pae.AreaID).AreaName;
		string areaNameCn = DataBuffer2018.ParkAreas.First((ParkArea m) => m.AreaID == pae.AreaID).AreaNameCn;
		string areaNamePt = DataBuffer2018.ParkAreas.First((ParkArea m) => m.AreaID == pae.AreaID).AreaNamePt;
		switch (pae.ParkType)
		{
		case EnumParkType.MCycle:
			uCParkAreaEX_Item.parkTypeInfo.BackgroundImage = ImageManager.GetImage("UCParkAreaEX", "Motorcycle1");
			cn = areaNameCn + " 電單車";
			en = areaNamePt + " MCycle";
			break;
		case EnumParkType.Other:
			uCParkAreaEX_Item.parkTypeInfo.BackgroundImage = ImageManager.GetImage("UCParkAreaEX", "DisabilityCar1");
			cn = areaNameCn + " 傷殘車";
			en = areaNamePt + " Disability";
			break;
		case EnumParkType.Private:
			uCParkAreaEX_Item.parkTypeInfo.BackgroundImage = ImageManager.GetImage("UCParkAreaEX", "PrivateCar1");
			cn = areaNameCn + " 私家車";
			en = areaNamePt + " Private";
			break;
		case EnumParkType.Van:
			uCParkAreaEX_Item.parkTypeInfo.BackgroundImage = ImageManager.GetImage("UCParkAreaEX", "PrivateCar1");
			cn = areaNameCn + " 貨車";
			en = areaNamePt + " Van";
			break;
		case EnumParkType.Charging:
			uCParkAreaEX_Item.parkTypeInfo.BackgroundImage = ImageManager.GetImage("UCParkAreaEX", "ElectricCar1");
			cn = areaNameCn + " 電動車";
			en = areaNamePt + " Electric";
			break;
		}
		uCParkAreaEX_Item.parkTypeInfo.Text = cn;
		uCParkAreaEX_Item.cn = cn;
		uCParkAreaEX_Item.en = en;
		if (LangManager.CurLanguage == SysLanguage.CHT)
		{
			uCParkAreaEX_Item.parkTypeInfo.Text = cn;
		}
		else
		{
			uCParkAreaEX_Item.parkTypeInfo.Text = en;
		}
		uCParkAreaEX_Item.labTimeRemain.Text = pae.TimeChargRemain.ToString();
		_ = pae.FloatParkSupply;
		int num = Convert.ToInt32(pae.FloatParkSupply.ToString());
		_ = pae.FloatParkUse;
		int num2 = num - Convert.ToInt32(pae.FloatParkUse.ToString());
		uCParkAreaEX_Item.labStaffRemain.Text = num2.ToString();
		int num3 = Convert.ToInt32((!pae.FloatParkSupply5.HasValue) ? "0" : pae.FloatParkSupply5.ToString()) - Convert.ToInt32((!pae.FloatParkUse5.HasValue) ? "0" : pae.FloatParkUse5.ToString());
		uCParkAreaEX_Item.labStudentRemain.Text = num3.ToString();
		if (pae.TimeChargRemain <= 0)
		{
			uCParkAreaEX_Item.labTimeRemain.ForeColor = Color.Red;
		}
		else
		{
			uCParkAreaEX_Item.labTimeRemain.ForeColor = Color.White;
		}
		if (num2 <= 0)
		{
			uCParkAreaEX_Item.labStaffRemain.ForeColor = Color.Red;
		}
		else
		{
			uCParkAreaEX_Item.labStaffRemain.ForeColor = Color.White;
		}
		if (num3 <= 0)
		{
			uCParkAreaEX_Item.labStudentRemain.ForeColor = Color.Red;
		}
		else
		{
			uCParkAreaEX_Item.labStudentRemain.ForeColor = Color.White;
		}
		if (pae.CustomFunnSigh)
		{
			uCParkAreaEX_Item.Tag = true;
			uCParkAreaEX_Item.pbPrivateState.Image = ImageManager.GetImage("UCParkAreaEX", "Full1");
		}
		if (panel_context.Controls.Count % 2 == 0)
		{
			uCParkAreaEX_Item.BackColor = Color.FromArgb(150, 178, 208);
		}
		else
		{
			uCParkAreaEX_Item.BackColor = Color.FromArgb(157, 196, 241);
		}
		uCParkAreaEX_Item.Location = m_itemLocation;
		m_itemLocation.Y += uCParkAreaEX_Item.Height;
		panel_context.Controls.Add(uCParkAreaEX_Item);
		m_AreaItems.Add(uCParkAreaEX_Item);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labFull.Text = LangManager.GetLangString("CarPark.UserControls.UCParkAreaEX2.labFull");
		labelTime.Text = LangManager.GetLangString("CarPark.UserControls.UCParkAreaEX2.labelTime");
		labStaff.Text = LangManager.GetLangString("CarPark.UserControls.UCParkAreaEX2.labStaff");
		labelStudent.Text = LangManager.GetLangString("CarPark.UserControls.UCParkAreaEX2.labelStudent");
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
		this.panel_context = new CarPark2018.UserControls.MyPanel();
		this.labelTime = new System.Windows.Forms.Label();
		this.labStaff = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.labFull = new System.Windows.Forms.Label();
		this.labelStudent = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.panel_context.AutoScroll = true;
		this.panel_context.Location = new System.Drawing.Point(0, 39);
		this.panel_context.Name = "panel_context";
		this.panel_context.Size = new System.Drawing.Size(360, 270);
		this.panel_context.TabIndex = 1;
		this.labelTime.BackColor = System.Drawing.Color.Transparent;
		this.labelTime.Font = new System.Drawing.Font("微软雅黑", 11f);
		this.labelTime.ForeColor = System.Drawing.Color.White;
		this.labelTime.Location = new System.Drawing.Point(95, 2);
		this.labelTime.Name = "labelTime";
		this.labelTime.Size = new System.Drawing.Size(55, 31);
		this.labelTime.TabIndex = 8;
		this.labelTime.Text = "訪客";
		this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labStaff.BackColor = System.Drawing.Color.Transparent;
		this.labStaff.Font = new System.Drawing.Font("微软雅黑", 11f);
		this.labStaff.ForeColor = System.Drawing.Color.White;
		this.labStaff.Location = new System.Drawing.Point(156, 2);
		this.labStaff.Name = "labStaff";
		this.labStaff.Size = new System.Drawing.Size(55, 31);
		this.labStaff.TabIndex = 8;
		this.labStaff.Text = "職員";
		this.labStaff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(360, 39);
		this.panel1.TabIndex = 9;
		this.panel1.Visible = true;
		this.labFull.BackColor = System.Drawing.Color.Transparent;
		this.labFull.Font = new System.Drawing.Font("微软雅黑", 11f);
		this.labFull.ForeColor = System.Drawing.Color.Navy;
		this.labFull.Location = new System.Drawing.Point(290, 2);
		this.labFull.Name = "labFull";
		this.labFull.Size = new System.Drawing.Size(60, 31);
		this.labFull.TabIndex = 8;
		this.labFull.Text = "人手滿";
		this.labFull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labelStudent.BackColor = System.Drawing.Color.Transparent;
		this.labelStudent.Font = new System.Drawing.Font("微软雅黑", 11f);
		this.labelStudent.ForeColor = System.Drawing.Color.White;
		this.labelStudent.Location = new System.Drawing.Point(217, 2);
		this.labelStudent.Name = "labelStudent";
		this.labelStudent.Size = new System.Drawing.Size(55, 31);
		this.labelStudent.TabIndex = 8;
		this.labelStudent.Text = "學生";
		this.labelStudent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Transparent;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.Controls.Add(this.labelTime);
		base.Controls.Add(this.labFull);
		base.Controls.Add(this.labelStudent);
		base.Controls.Add(this.labStaff);
		base.Controls.Add(this.panel_context);
		this.DoubleBuffered = true;
		base.Name = "UCParkAreaEX2";
		base.Size = new System.Drawing.Size(360, 342);
		base.Load += new System.EventHandler(UCParkAreaEX2_Load);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
