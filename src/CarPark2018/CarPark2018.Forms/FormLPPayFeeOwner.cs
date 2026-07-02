using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using Master.SystemCommunication.Lib;
using log4net;

namespace CarPark2018.Forms;

public class FormLPPayFeeOwner : Form
{
	private ILog Logger;

	private static FormLPPayFeeOwner frmUser;

	private IContainer components = null;

	private PictureBox picCar;

	private Label labLP;

	private Label labIntime;

	private Label labParkMin;

	private Label labAmount;

	private Label labParkMinIF;

	private Label labAmountIF;

	private Label labLPIF;

	private Label labIntimeIF;

	private Label labLasttimeIF;

	private Label labLasttime;

	public static FormLPPayFeeOwner Self()
	{
		if (frmUser == null)
		{
			frmUser = new FormLPPayFeeOwner();
			int num = Screen.GetWorkingArea(frmUser).Width;
			frmUser.Location = new Point(num, 0);
			frmUser.Show();
		}
		return frmUser;
	}

	public FormLPPayFeeOwner()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		InitializeComponent();
	}

	public void SetInfo(ChargeRecord info, CalcTicketFeeReturnV2 calcReturn, bool isOK)
	{
		labLPIF.Text = info.CardCode;
		labIntimeIF.Text = calcReturn.InTime.ToString(SystemParm.LongTimeFormat);
		labParkMinIF.Text = $"{info.ParkMin / 60}:{info.ParkMin % 60}";
		labAmountIF.Text = info.TotalCharge.ToString("f2");
		if (calcReturn.HasLastTimeCharge)
		{
			labLasttime.Visible = true;
			labLasttimeIF.Visible = true;
			labLasttimeIF.Text = calcReturn.LastTimeCharge.ToString(SystemParm.LongTimeFormat);
		}
		else
		{
			labLasttime.Visible = false;
			labLasttimeIF.Visible = false;
		}
		if (isOK && !string.IsNullOrEmpty(calcReturn.InLicensePlatePath))
		{
			try
			{
				picCar.Image = Image.FromFile(Config.LicensePlatePath + calcReturn.InLicensePlatePath);
				return;
			}
			catch (Exception)
			{
				picCar.Image = ImageManager.GetImage("", "cancel");
				return;
			}
		}
		picCar.Image = ImageManager.GetImage("", "cancel");
	}

	public void SetInfo(ChargeRecord info, CalcTicketFeeReturn calcReturn, DateTime intime)
	{
		labLPIF.Text = info.CardCode;
		labIntimeIF.Text = intime.ToString(SystemParm.LongTimeFormat);
		labParkMinIF.Text = $"{info.ParkMin / 60}H{info.ParkMin % 60}M";
		labAmountIF.Text = info.TotalCharge.ToString("f2");
		if (calcReturn.HasLastTimeCharge)
		{
			labLasttime.Visible = true;
			labLasttimeIF.Visible = true;
			labLasttimeIF.Text = calcReturn.LastTimeCharge.ToString(SystemParm.LongTimeFormat);
		}
		else
		{
			labLasttime.Visible = false;
			labLasttimeIF.Visible = false;
		}
		picCar.Image = ImageManager.GetImage("", "cancel");
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
		this.picCar = new System.Windows.Forms.PictureBox();
		this.labLP = new System.Windows.Forms.Label();
		this.labIntime = new System.Windows.Forms.Label();
		this.labParkMin = new System.Windows.Forms.Label();
		this.labAmount = new System.Windows.Forms.Label();
		this.labParkMinIF = new System.Windows.Forms.Label();
		this.labAmountIF = new System.Windows.Forms.Label();
		this.labLPIF = new System.Windows.Forms.Label();
		this.labIntimeIF = new System.Windows.Forms.Label();
		this.labLasttimeIF = new System.Windows.Forms.Label();
		this.labLasttime = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.picCar).BeginInit();
		base.SuspendLayout();
		this.picCar.BackColor = System.Drawing.Color.Black;
		this.picCar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.picCar.Location = new System.Drawing.Point(11, 12);
		this.picCar.Name = "picCar";
		this.picCar.Size = new System.Drawing.Size(588, 342);
		this.picCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picCar.TabIndex = 0;
		this.picCar.TabStop = false;
		this.labLP.Font = new System.Drawing.Font("微软雅黑", 30f);
		this.labLP.ForeColor = System.Drawing.Color.White;
		this.labLP.Location = new System.Drawing.Point(702, 36);
		this.labLP.Name = "labLP";
		this.labLP.Size = new System.Drawing.Size(500, 50);
		this.labLP.TabIndex = 2;
		this.labLP.Text = "車牌";
		this.labLP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labIntime.Font = new System.Drawing.Font("微软雅黑", 30f);
		this.labIntime.ForeColor = System.Drawing.Color.White;
		this.labIntime.Location = new System.Drawing.Point(12, 407);
		this.labIntime.Name = "labIntime";
		this.labIntime.Size = new System.Drawing.Size(200, 50);
		this.labIntime.TabIndex = 3;
		this.labIntime.Text = "入場時間";
		this.labIntime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkMin.Font = new System.Drawing.Font("微软雅黑", 30f);
		this.labParkMin.ForeColor = System.Drawing.Color.White;
		this.labParkMin.Location = new System.Drawing.Point(12, 490);
		this.labParkMin.Name = "labParkMin";
		this.labParkMin.Size = new System.Drawing.Size(200, 50);
		this.labParkMin.TabIndex = 4;
		this.labParkMin.Text = "泊車時間";
		this.labParkMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labAmount.Font = new System.Drawing.Font("微软雅黑", 30f);
		this.labAmount.ForeColor = System.Drawing.Color.White;
		this.labAmount.Location = new System.Drawing.Point(649, 220);
		this.labAmount.Name = "labAmount";
		this.labAmount.Size = new System.Drawing.Size(169, 50);
		this.labAmount.TabIndex = 5;
		this.labAmount.Text = "金額";
		this.labAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkMinIF.Font = new System.Drawing.Font("微软雅黑", 30f);
		this.labParkMinIF.ForeColor = System.Drawing.Color.White;
		this.labParkMinIF.Location = new System.Drawing.Point(239, 490);
		this.labParkMinIF.Name = "labParkMinIF";
		this.labParkMinIF.Size = new System.Drawing.Size(360, 50);
		this.labParkMinIF.TabIndex = 6;
		this.labParkMinIF.Text = "75:29";
		this.labParkMinIF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labAmountIF.Font = new System.Drawing.Font("微软雅黑", 50f);
		this.labAmountIF.ForeColor = System.Drawing.Color.Crimson;
		this.labAmountIF.Location = new System.Drawing.Point(842, 189);
		this.labAmountIF.Name = "labAmountIF";
		this.labAmountIF.Size = new System.Drawing.Size(360, 123);
		this.labAmountIF.TabIndex = 7;
		this.labAmountIF.Text = "1000.0";
		this.labAmountIF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labLPIF.Font = new System.Drawing.Font("微软雅黑", 30f);
		this.labLPIF.ForeColor = System.Drawing.Color.White;
		this.labLPIF.Location = new System.Drawing.Point(702, 100);
		this.labLPIF.Name = "labLPIF";
		this.labLPIF.Size = new System.Drawing.Size(500, 50);
		this.labLPIF.TabIndex = 8;
		this.labLPIF.Text = "MM1234";
		this.labLPIF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labIntimeIF.Font = new System.Drawing.Font("微软雅黑", 30f);
		this.labIntimeIF.ForeColor = System.Drawing.Color.White;
		this.labIntimeIF.Location = new System.Drawing.Point(218, 407);
		this.labIntimeIF.Name = "labIntimeIF";
		this.labIntimeIF.Size = new System.Drawing.Size(420, 50);
		this.labIntimeIF.TabIndex = 9;
		this.labIntimeIF.Text = "2018-12-09 12:22";
		this.labIntimeIF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labLasttimeIF.Font = new System.Drawing.Font("微软雅黑", 30f);
		this.labLasttimeIF.ForeColor = System.Drawing.Color.White;
		this.labLasttimeIF.Location = new System.Drawing.Point(218, 579);
		this.labLasttimeIF.Name = "labLasttimeIF";
		this.labLasttimeIF.Size = new System.Drawing.Size(420, 50);
		this.labLasttimeIF.TabIndex = 11;
		this.labLasttimeIF.Text = "2018-12-09 12:22";
		this.labLasttimeIF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labLasttime.Font = new System.Drawing.Font("微软雅黑", 30f);
		this.labLasttime.ForeColor = System.Drawing.Color.White;
		this.labLasttime.Location = new System.Drawing.Point(12, 579);
		this.labLasttime.Name = "labLasttime";
		this.labLasttime.Size = new System.Drawing.Size(200, 50);
		this.labLasttime.TabIndex = 10;
		this.labLasttime.Text = "上次收費";
		this.labLasttime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Highlight;
		base.ClientSize = new System.Drawing.Size(1366, 768);
		base.Controls.Add(this.labLasttimeIF);
		base.Controls.Add(this.labLasttime);
		base.Controls.Add(this.labLPIF);
		base.Controls.Add(this.labAmountIF);
		base.Controls.Add(this.labParkMinIF);
		base.Controls.Add(this.labAmount);
		base.Controls.Add(this.labParkMin);
		base.Controls.Add(this.labIntime);
		base.Controls.Add(this.labLP);
		base.Controls.Add(this.picCar);
		base.Controls.Add(this.labIntimeIF);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormLPPayFeeOwner";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.Text = "FormLPPayFee";
		base.TopMost = true;
		((System.ComponentModel.ISupportInitialize)this.picCar).EndInit();
		base.ResumeLayout(false);
	}
}
