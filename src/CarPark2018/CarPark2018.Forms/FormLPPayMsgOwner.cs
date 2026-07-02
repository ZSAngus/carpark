using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using log4net;

namespace CarPark2018.Forms;

public class FormLPPayMsgOwner : Form
{
	private ILog Logger;

	private static FormLPPayMsgOwner frmUser;

	private IContainer components = null;

	private Label labLP;

	private Label labTips;

	private Timer timer1;

	public static FormLPPayMsgOwner Self()
	{
		if (frmUser == null)
		{
			frmUser = new FormLPPayMsgOwner();
			int num = Screen.GetWorkingArea(frmUser).Width;
			frmUser.Location = new Point(num, 0);
		}
		return frmUser;
	}

	public FormLPPayMsgOwner()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		InitializeComponent();
	}

	public void ShowMsg(string lp, string msg, bool isCancel = false)
	{
		labLP.Text = lp;
		labTips.Text = msg;
		if (!isCancel)
		{
			timer1.Stop();
			timer1.Start();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Hide();
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		timer1.Stop();
		Hide();
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
		this.labLP = new System.Windows.Forms.Label();
		this.labTips = new System.Windows.Forms.Label();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.labLP.Font = new System.Drawing.Font("微软雅黑", 40f);
		this.labLP.ForeColor = System.Drawing.Color.White;
		this.labLP.Location = new System.Drawing.Point(0, 99);
		this.labLP.Name = "labLP";
		this.labLP.Size = new System.Drawing.Size(1366, 70);
		this.labLP.TabIndex = 0;
		this.labLP.Text = "MM3214";
		this.labLP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labTips.Font = new System.Drawing.Font("微软雅黑", 40f);
		this.labTips.ForeColor = System.Drawing.Color.White;
		this.labTips.Location = new System.Drawing.Point(0, 193);
		this.labTips.Name = "labTips";
		this.labTips.Size = new System.Drawing.Size(1366, 250);
		this.labTips.TabIndex = 1;
		this.labTips.Text = "沒有符合的車輛信息 請確認輸入車牌是否正確";
		this.labTips.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.timer1.Interval = 3000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Highlight;
		base.ClientSize = new System.Drawing.Size(1366, 768);
		base.Controls.Add(this.labTips);
		base.Controls.Add(this.labLP);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormLPPayMsgOwner";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.Text = "FormLPPayMsg";
		base.TopMost = true;
		base.ResumeLayout(false);
	}
}
