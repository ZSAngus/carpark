using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CarPark2018.Properties;

namespace CarPark2018;

public class FormFee : Form
{
	private static FormFee frmFee;

	private Thread thread = null;

	private static bool hide;

	private IContainer components = null;

	private Panel panTicket;

	private Label labCharge_T;

	private Label labBerthTime;

	private Label labFeeTime_T;

	private Label labInTime_T;

	private Panel panMonth;

	private Label labTotal_M;

	private Label labExpire_M;

	private Panel panMpass;

	private Label labTotal_Mpass;

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.ExStyle |= 33554432;
			return createParams;
		}
	}

	public static FormFee Self()
	{
		if (frmFee == null)
		{
			frmFee = new FormFee();
			int num = Screen.GetWorkingArea(frmFee).Width;
			frmFee.Location = new Point(num, 0);
			frmFee.Show();
		}
		return frmFee;
	}

	public FormFee()
	{
		InitializeComponent();
		panTicket.Visible = false;
		panMonth.Visible = false;
		panMpass.Visible = false;
	}

	public void SetTicket(string inTime, string feeTime, string berthTime, string charge)
	{
		panTicket.Dock = DockStyle.Fill;
		labInTime_T.Text = inTime;
		labFeeTime_T.Text = feeTime;
		labBerthTime.Text = berthTime;
		labCharge_T.Text = "$ " + charge;
		ShowHide();
		panTicket.Visible = true;
		hide = false;
	}

	public void SetMonth(string expire, string total)
	{
		panMonth.Dock = DockStyle.Fill;
		labExpire_M.Text = expire;
		labTotal_M.Text = "$ " + total;
		ShowHide();
		panMonth.Visible = true;
		hide = false;
	}

	public void SetMPass(string total)
	{
		panMpass.Dock = DockStyle.Fill;
		labTotal_Mpass.Text = "$ " + total;
		ShowHide();
		panMpass.Visible = true;
		hide = false;
	}

	public void ShowHide()
	{
		panTicket.Visible = false;
		panMonth.Visible = false;
		panMpass.Visible = false;
	}

	private void LoadImage()
	{
		try
		{
			string filename = Application.StartupPath + "\\Image\\Master.png";
			Image backgroundImage = Image.FromFile(filename);
			BackgroundImage = backgroundImage;
		}
		catch (Exception)
		{
		}
	}

	public Image GetImage(string path)
	{
		Image result = null;
		if (File.Exists(path))
		{
			using FileStream fileStream = File.OpenRead(path);
			int num = 0;
			num = (int)fileStream.Length;
			byte[] buffer = new byte[num];
			fileStream.Read(buffer, 0, num);
			result = Image.FromStream(fileStream);
		}
		return result;
	}

	private void FormFee_Load(object sender, EventArgs e)
	{
		LoadImage();
		thread = new Thread(send)
		{
			IsBackground = true
		};
		thread.Start();
	}

	public void send()
	{
		int num = 0;
		while (true)
		{
			num = (hide ? (num + 1) : 0);
			if (num >= 100 && frmFee != null)
			{
				hide = false;
				Invoke((MethodInvoker)delegate
				{
					ShowHide();
				});
			}
			Thread.Sleep(100);
		}
	}

	public static void Self2()
	{
		hide = true;
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
		this.panTicket = new System.Windows.Forms.Panel();
		this.labCharge_T = new System.Windows.Forms.Label();
		this.labBerthTime = new System.Windows.Forms.Label();
		this.labFeeTime_T = new System.Windows.Forms.Label();
		this.labInTime_T = new System.Windows.Forms.Label();
		this.panMonth = new System.Windows.Forms.Panel();
		this.labTotal_M = new System.Windows.Forms.Label();
		this.labExpire_M = new System.Windows.Forms.Label();
		this.panMpass = new System.Windows.Forms.Panel();
		this.labTotal_Mpass = new System.Windows.Forms.Label();
		this.panTicket.SuspendLayout();
		this.panMonth.SuspendLayout();
		this.panMpass.SuspendLayout();
		base.SuspendLayout();
		this.panTicket.BackColor = System.Drawing.Color.Transparent;
		this.panTicket.BackgroundImage = CarPark2018.Properties.Resources.beijing800_600;
		this.panTicket.Controls.Add(this.labCharge_T);
		this.panTicket.Controls.Add(this.labBerthTime);
		this.panTicket.Controls.Add(this.labFeeTime_T);
		this.panTicket.Controls.Add(this.labInTime_T);
		this.panTicket.Location = new System.Drawing.Point(386, 342);
		this.panTicket.Name = "panTicket";
		this.panTicket.Size = new System.Drawing.Size(213, 98);
		this.panTicket.TabIndex = 0;
		this.labCharge_T.BackColor = System.Drawing.Color.Transparent;
		this.labCharge_T.Font = new System.Drawing.Font("新細明體", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.labCharge_T.ForeColor = System.Drawing.Color.White;
		this.labCharge_T.Location = new System.Drawing.Point(302, 445);
		this.labCharge_T.Name = "labCharge_T";
		this.labCharge_T.Size = new System.Drawing.Size(446, 59);
		this.labCharge_T.TabIndex = 3;
		this.labCharge_T.Text = "$ 20";
		this.labCharge_T.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labBerthTime.BackColor = System.Drawing.Color.Transparent;
		this.labBerthTime.Font = new System.Drawing.Font("新細明體", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.labBerthTime.ForeColor = System.Drawing.Color.White;
		this.labBerthTime.Location = new System.Drawing.Point(302, 347);
		this.labBerthTime.Name = "labBerthTime";
		this.labBerthTime.Size = new System.Drawing.Size(446, 59);
		this.labBerthTime.TabIndex = 2;
		this.labBerthTime.Text = "1小時27分31秒";
		this.labBerthTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labFeeTime_T.BackColor = System.Drawing.Color.Transparent;
		this.labFeeTime_T.Font = new System.Drawing.Font("新細明體", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.labFeeTime_T.ForeColor = System.Drawing.Color.White;
		this.labFeeTime_T.Location = new System.Drawing.Point(301, 246);
		this.labFeeTime_T.Name = "labFeeTime_T";
		this.labFeeTime_T.Size = new System.Drawing.Size(446, 59);
		this.labFeeTime_T.TabIndex = 1;
		this.labFeeTime_T.Text = "2017-01-18 04:06:30";
		this.labFeeTime_T.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labInTime_T.BackColor = System.Drawing.Color.Transparent;
		this.labInTime_T.Font = new System.Drawing.Font("新細明體", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.labInTime_T.ForeColor = System.Drawing.Color.White;
		this.labInTime_T.Location = new System.Drawing.Point(302, 144);
		this.labInTime_T.Name = "labInTime_T";
		this.labInTime_T.Size = new System.Drawing.Size(446, 59);
		this.labInTime_T.TabIndex = 0;
		this.labInTime_T.Text = "2017-01-18 04:06:30";
		this.labInTime_T.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panMonth.BackColor = System.Drawing.Color.Transparent;
		this.panMonth.BackgroundImage = CarPark2018.Properties.Resources.Month800_600;
		this.panMonth.Controls.Add(this.labTotal_M);
		this.panMonth.Controls.Add(this.labExpire_M);
		this.panMonth.Location = new System.Drawing.Point(12, 342);
		this.panMonth.Name = "panMonth";
		this.panMonth.Size = new System.Drawing.Size(213, 117);
		this.panMonth.TabIndex = 1;
		this.labTotal_M.BackColor = System.Drawing.Color.Transparent;
		this.labTotal_M.Font = new System.Drawing.Font("新細明體", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.labTotal_M.ForeColor = System.Drawing.Color.White;
		this.labTotal_M.Location = new System.Drawing.Point(302, 379);
		this.labTotal_M.Name = "labTotal_M";
		this.labTotal_M.Size = new System.Drawing.Size(446, 59);
		this.labTotal_M.TabIndex = 2;
		this.labTotal_M.Text = "$300";
		this.labTotal_M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labExpire_M.BackColor = System.Drawing.Color.Transparent;
		this.labExpire_M.Font = new System.Drawing.Font("新細明體", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.labExpire_M.ForeColor = System.Drawing.Color.White;
		this.labExpire_M.Location = new System.Drawing.Point(302, 203);
		this.labExpire_M.Name = "labExpire_M";
		this.labExpire_M.Size = new System.Drawing.Size(446, 59);
		this.labExpire_M.TabIndex = 1;
		this.labExpire_M.Text = "2017-01-18 ";
		this.labExpire_M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panMpass.BackColor = System.Drawing.Color.Transparent;
		this.panMpass.BackgroundImage = CarPark2018.Properties.Resources.MPasss800_600;
		this.panMpass.Controls.Add(this.labTotal_Mpass);
		this.panMpass.Location = new System.Drawing.Point(252, 167);
		this.panMpass.Name = "panMpass";
		this.panMpass.Size = new System.Drawing.Size(202, 144);
		this.panMpass.TabIndex = 2;
		this.labTotal_Mpass.BackColor = System.Drawing.Color.Transparent;
		this.labTotal_Mpass.Font = new System.Drawing.Font("新細明體", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.labTotal_Mpass.ForeColor = System.Drawing.Color.White;
		this.labTotal_Mpass.Location = new System.Drawing.Point(281, 324);
		this.labTotal_Mpass.Name = "labTotal_Mpass";
		this.labTotal_Mpass.Size = new System.Drawing.Size(446, 59);
		this.labTotal_Mpass.TabIndex = 2;
		this.labTotal_Mpass.Text = "$300";
		this.labTotal_Mpass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(800, 600);
		base.Controls.Add(this.panMpass);
		base.Controls.Add(this.panMonth);
		base.Controls.Add(this.panTicket);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormFee";
		base.ShowInTaskbar = false;
		this.Text = "FormFee";
		base.Load += new System.EventHandler(FormFee_Load);
		this.panTicket.ResumeLayout(false);
		this.panMonth.ResumeLayout(false);
		this.panMpass.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
