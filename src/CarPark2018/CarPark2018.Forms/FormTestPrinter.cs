using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.Device;
using CarPark.Lib;
using SkyInno.Lang;

namespace CarPark2018.Forms;

public class FormTestPrinter : Form
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnPrint;

	private Button button1;

	private TextBox txtFreeminute;

	private Label label1;

	private Label label2;

	private TextBox txtGateID;

	private GroupBox groupBox3;

	public FormTestPrinter()
	{
		InitializeComponent();
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent += FeeCenterModule_TicketScanEvent;
		}
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.PrintTicket("0000000", "1|0");
			btnPrint.Enabled = false;
		}
	}

	private void FormTestPrinter_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.TicketScanEvent -= FeeCenterModule_TicketScanEvent;
			if (btnPrint.Enabled)
			{
				DeviceManager.FeeCenterModule.EjectTicket();
			}
		}
	}

	private FeeInfo FeeCenterModule_TicketScanEvent(TicketInfo ticketInfo)
	{
		if (ticketInfo.IsEmptyOrInValid)
		{
			FeeInfo feeInfo = new FeeInfo();
			feeInfo.TicketAction = EnumTicketAction.Keep;
			btnPrint.Invoke((Action)delegate
			{
				btnPrint.Enabled = true;
			}, null);
			return feeInfo;
		}
		FeeInfo feeInfo2 = new FeeInfo();
		Global.ShowMessage(LangManager.GetLangString("Alert.Not_Empty_Ticket"));
		feeInfo2.TicketAction = EnumTicketAction.Reject;
		Action method = delegate
		{
			btnPrint.Enabled = false;
		};
		Invoke(method);
		return feeInfo2;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
			int freeminute = Convert.ToInt32(txtFreeminute.Text.Trim());
			int gateID = Convert.ToInt32(txtGateID.Text.Trim());
			DeviceManager.EnterGateModule.SetFreeExitTime(gateID, freeminute);
			MessageBox.Show("设定成功");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnPrint = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.txtFreeminute = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtGateID = new System.Windows.Forms.TextBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.btnPrint);
		this.groupBox1.Location = new System.Drawing.Point(3, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(285, 119);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "票機測試";
		this.btnPrint.Enabled = false;
		this.btnPrint.Location = new System.Drawing.Point(191, 21);
		this.btnPrint.Name = "btnPrint";
		this.btnPrint.Size = new System.Drawing.Size(75, 23);
		this.btnPrint.TabIndex = 0;
		this.btnPrint.Text = "打印測試";
		this.btnPrint.UseVisualStyleBackColor = true;
		this.btnPrint.Click += new System.EventHandler(btnPrint_Click);
		this.button1.Location = new System.Drawing.Point(227, 49);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 1;
		this.button1.Text = "设定";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.txtFreeminute.Location = new System.Drawing.Point(106, 49);
		this.txtFreeminute.MaxLength = 3;
		this.txtFreeminute.Name = "txtFreeminute";
		this.txtFreeminute.Size = new System.Drawing.Size(100, 22);
		this.txtFreeminute.TabIndex = 2;
		this.txtFreeminute.Text = "000";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(23, 54);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(77, 12);
		this.label1.TabIndex = 3;
		this.label1.Text = "免费离场时间";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(23, 26);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 12);
		this.label2.TabIndex = 3;
		this.label2.Text = "闸门ID";
		this.txtGateID.Location = new System.Drawing.Point(106, 21);
		this.txtGateID.Name = "txtGateID";
		this.txtGateID.Size = new System.Drawing.Size(100, 22);
		this.txtGateID.TabIndex = 2;
		this.txtGateID.Text = "1";
		this.groupBox3.Controls.Add(this.txtGateID);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Controls.Add(this.button1);
		this.groupBox3.Controls.Add(this.label1);
		this.groupBox3.Controls.Add(this.txtFreeminute);
		this.groupBox3.Location = new System.Drawing.Point(370, 12);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(331, 129);
		this.groupBox3.TabIndex = 5;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "闸机设置";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(773, 378);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormTestPrinter";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Setting";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormTestPrinter_FormClosing);
		this.groupBox1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		base.ResumeLayout(false);
	}
}
