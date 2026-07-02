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

public class UCParkAreaEX : UserControl
{
	private Point m_itemLocation = new Point(5, 2);

	public List<UCParkAreaEX_Item> m_AreaItems = null;

	private string cn = "";

	private string en = "";

	private IContainer components = null;

	private Panel panel_context;

	private Label labelSP;

	private Label labRemain;

	private Panel panel1;

	private Label labFull;

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.Style &= -33554433;
			return createParams;
		}
	}

	public UCParkAreaEX()
	{
		InitializeComponent();
		BackgroundImage = ImageManager.GetImage("Main", "PasstraceBox");
	}

	private void UCParkAreaEX2_Load(object sender, EventArgs e)
	{
		m_AreaItems = new List<UCParkAreaEX_Item>();
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
		UCParkAreaEX_Item uCParkAreaEX_Item = new UCParkAreaEX_Item(pae);
		string areaName = DataBuffer2018.ParkAreas.First((ParkArea m) => m.AreaID == pae.AreaID).AreaName;
		string areaNameCn = DataBuffer2018.ParkAreas.First((ParkArea m) => m.AreaID == pae.AreaID).AreaNameCn;
		string areaNamePt = DataBuffer2018.ParkAreas.First((ParkArea m) => m.AreaID == pae.AreaID).AreaNamePt;
		switch (pae.ParkType)
		{
		case EnumParkType.MCycle:
			uCParkAreaEX_Item.parkTypeInfo.Image = ImageManager.GetImage("UCParkAreaEX", "Motorcycle1");
			cn = areaNameCn + " 電單車";
			en = areaNamePt + " MCycle";
			break;
		case EnumParkType.Other:
			uCParkAreaEX_Item.parkTypeInfo.Image = ImageManager.GetImage("UCParkAreaEX", "DisabilityCar1");
			cn = areaNameCn + " 傷殘車";
			en = areaNamePt + " Disability";
			break;
		case EnumParkType.Private:
			uCParkAreaEX_Item.parkTypeInfo.Image = ImageManager.GetImage("UCParkAreaEX", "PrivateCar1");
			cn = areaNameCn + " 私家車";
			en = areaNamePt + " Private";
			break;
		case EnumParkType.Van:
			uCParkAreaEX_Item.parkTypeInfo.Image = ImageManager.GetImage("UCParkAreaEX", "PrivateCar1");
			cn = areaNameCn + " 貨車";
			en = areaNamePt + " Van";
			break;
		}
		uCParkAreaEX_Item.parkTypeInfo.Text = cn;
		uCParkAreaEX_Item.cn = cn;
		uCParkAreaEX_Item.en = en;
		uCParkAreaEX_Item.lab_SpCount.Text = pae.TimeChargeSupply.ToString();
		uCParkAreaEX_Item.labExCount.Text = pae.TimeChargRemain.ToString();
		if (pae.TimeChargRemain <= 0)
		{
			uCParkAreaEX_Item.labExCount.ForeColor = Color.Red;
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
		labelSP.Text = LangManager.GetLangString("CarPark.UserControls.UCParkAreaEX2.labelSP");
		labFull.Text = LangManager.GetLangString("CarPark.UserControls.UCParkAreaEX2.labFull");
		labRemain.Text = LangManager.GetLangString("CarPark.UserControls.UCParkAreaEX2.labRemain");
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
		this.panel_context = new System.Windows.Forms.Panel();
		this.labelSP = new System.Windows.Forms.Label();
		this.labRemain = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.labFull = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.panel_context.AutoScroll = true;
		this.panel_context.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel_context.Location = new System.Drawing.Point(0, 39);
		this.panel_context.Name = "panel_context";
		this.panel_context.Size = new System.Drawing.Size(360, 270);
		this.panel_context.TabIndex = 1;
		this.labelSP.BackColor = System.Drawing.Color.Transparent;
		this.labelSP.Font = new System.Drawing.Font("微软雅黑", 16f);
		this.labelSP.ForeColor = System.Drawing.Color.Navy;
		this.labelSP.Location = new System.Drawing.Point(98, 2);
		this.labelSP.Name = "labelSP";
		this.labelSP.Size = new System.Drawing.Size(92, 31);
		this.labelSP.TabIndex = 8;
		this.labelSP.Text = "Remain";
		this.labelSP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labRemain.BackColor = System.Drawing.Color.Transparent;
		this.labRemain.Font = new System.Drawing.Font("微软雅黑", 16f);
		this.labRemain.ForeColor = System.Drawing.Color.White;
		this.labRemain.Location = new System.Drawing.Point(184, 2);
		this.labRemain.Name = "labRemain";
		this.labRemain.Size = new System.Drawing.Size(92, 31);
		this.labRemain.TabIndex = 8;
		this.labRemain.Text = "Remain";
		this.labRemain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.Controls.Add(this.labelSP);
		this.panel1.Controls.Add(this.labFull);
		this.panel1.Controls.Add(this.labRemain);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(360, 39);
		this.panel1.TabIndex = 9;
		this.labFull.BackColor = System.Drawing.Color.Transparent;
		this.labFull.Font = new System.Drawing.Font("微软雅黑", 16f);
		this.labFull.ForeColor = System.Drawing.Color.Navy;
		this.labFull.Location = new System.Drawing.Point(267, 2);
		this.labFull.Name = "labFull";
		this.labFull.Size = new System.Drawing.Size(92, 31);
		this.labFull.TabIndex = 8;
		this.labFull.Text = "Remain";
		this.labFull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Transparent;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.Controls.Add(this.panel_context);
		base.Controls.Add(this.panel1);
		base.Name = "UCParkAreaEX";
		base.Size = new System.Drawing.Size(360, 342);
		base.Load += new System.EventHandler(UCParkAreaEX2_Load);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
