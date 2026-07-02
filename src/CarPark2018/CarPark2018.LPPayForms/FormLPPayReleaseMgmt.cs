using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.LPPayForms;

public class FormLPPayReleaseMgmt : Form
{
	private static ILog Logger;

	public List<UCReleaseMgmtItem> m_GateItems = null;

	private IContainer components = null;

	private Panel panFill;

	private Label labTitle;

	private Panel panMiddle;

	private Panel panBottom;

	private Button btnCancel;

	static FormLPPayReleaseMgmt()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public FormLPPayReleaseMgmt()
	{
		InitializeComponent();
		m_GateItems = new List<UCReleaseMgmtItem>();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void FormLPPayReleaseMgmt_Load(object sender, EventArgs e)
	{
		try
		{
			int num = 14;
			int num2 = 3;
			int num3 = 0;
			Font font = new Font("黑体", 10f);
			foreach (ParkGate parkGate in DataBuffer2018.ParkGates)
			{
				UCReleaseMgmtItem uCReleaseMgmtItem = new UCReleaseMgmtItem(parkGate);
				uCReleaseMgmtItem.Location = new Point(num, num2);
				panMiddle.Controls.Add(uCReleaseMgmtItem);
				num += uCReleaseMgmtItem.Width + 10;
				if (panMiddle.Controls.Count % 3 == 0)
				{
					num3++;
					num = 14;
					num2 = uCReleaseMgmtItem.Height * num3 + 5 * num3 + 3;
				}
				m_GateItems.Add(uCReleaseMgmtItem);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
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
		this.panFill = new System.Windows.Forms.Panel();
		this.labTitle = new System.Windows.Forms.Label();
		this.panMiddle = new System.Windows.Forms.Panel();
		this.panBottom = new System.Windows.Forms.Panel();
		this.btnCancel = new System.Windows.Forms.Button();
		this.panFill.SuspendLayout();
		this.panBottom.SuspendLayout();
		base.SuspendLayout();
		this.panFill.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.panBottom);
		this.panFill.Controls.Add(this.panMiddle);
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(500, 600);
		this.panFill.TabIndex = 0;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(500, 60);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "放行管理";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panMiddle.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panMiddle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panMiddle.Location = new System.Drawing.Point(0, 60);
		this.panMiddle.Name = "panMiddle";
		this.panMiddle.Size = new System.Drawing.Size(500, 470);
		this.panMiddle.TabIndex = 1;
		this.panBottom.Controls.Add(this.btnCancel);
		this.panBottom.Location = new System.Drawing.Point(0, 529);
		this.panBottom.Name = "panBottom";
		this.panBottom.Size = new System.Drawing.Size(500, 70);
		this.panBottom.TabIndex = 2;
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 18f);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(178, 7);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(116, 54);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 600);
		base.Controls.Add(this.panFill);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormLPPayReleaseMgmt";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormLPPayReleaseMgmt";
		base.Load += new System.EventHandler(FormLPPayReleaseMgmt_Load);
		this.panFill.ResumeLayout(false);
		this.panBottom.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
