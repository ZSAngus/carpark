using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.Device;
using log4net;

namespace CarPark2018.UserControls;

public class UCGatesEX : UserControl
{
	private static ILog Logger;

	public List<UCGatesEX_Item> m_GateItems = null;

	private IContainer components = null;

	public UCGatesEX()
	{
		InitializeComponent();
		BackgroundImage = ImageManager.GetImage("Main", "TwoBox");
		m_GateItems = new List<UCGatesEX_Item>();
	}

	static UCGatesEX()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	private void UCGatesEX_Load(object sender, EventArgs e)
	{
		Init();
		Test();
	}

	private void Init()
	{
		try
		{
			int num = 5;
			int num2 = 7;
			int num3 = 0;
			int num4 = 0;
			Font font = new Font("黑体", 10f);
			foreach (ParkGate parkGate in DataBuffer2018.ParkGates)
			{
				UCGatesEX_Item uCGatesEX_Item = new UCGatesEX_Item(parkGate);
				uCGatesEX_Item.Location = new Point(num, num2);
				base.Controls.Add(uCGatesEX_Item);
				num += uCGatesEX_Item.Width + 5;
				if (base.Controls.Count % 4 == 0)
				{
					num3++;
					num = 6;
					num2 = uCGatesEX_Item.Height * num3 + 10;
				}
				m_GateItems.Add(uCGatesEX_Item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public void Test()
	{
		try
		{
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void IFeeCenterCommunication_GateStatusChangeEvent(DeviceStatusInfo deviceStatusInfo)
	{
		string deviceCode = deviceStatusInfo.DeviceCode;
		EnumDeviceType deviceType = deviceStatusInfo.DeviceType;
		int gateID = deviceStatusInfo.GateID;
	}

	private new void MouseDown(object sender, MouseEventArgs e)
	{
	}

	private new void MouseUp(object sender, MouseEventArgs e)
	{
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
		base.SuspendLayout();
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Transparent;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("黑体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.Name = "UCGatesEX";
		base.Size = new System.Drawing.Size(526, 320);
		base.Load += new System.EventHandler(UCGatesEX_Load);
		base.ResumeLayout(false);
	}
}
