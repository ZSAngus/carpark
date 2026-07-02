using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CarPark.Lib;
using DevComponents.DotNetBar;

namespace CarPark;

public class Dialog : Office2007Form
{
	private IContainer components;

	private ButtonX btnCancel;

	private ButtonX btnOk;

	private LabelX labNumber;

	public string MSGText { get; set; }

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarPark.Dialog));
		this.labNumber = new DevComponents.DotNetBar.LabelX();
		this.btnOk = new DevComponents.DotNetBar.ButtonX();
		this.btnCancel = new DevComponents.DotNetBar.ButtonX();
		base.SuspendLayout();
		this.labNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labNumber.Dock = System.Windows.Forms.DockStyle.Top;
		this.labNumber.Font = new System.Drawing.Font("Times New Roman", 18f);
		this.labNumber.Location = new System.Drawing.Point(0, 0);
		this.labNumber.Name = "labNumber";
		this.labNumber.Size = new System.Drawing.Size(352, 120);
		this.labNumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.labNumber.TabIndex = 2;
		this.labNumber.TextAlignment = System.Drawing.StringAlignment.Center;
		this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnOk.Font = new System.Drawing.Font("Times New Roman", 18f);
		this.btnOk.Location = new System.Drawing.Point(47, 126);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(96, 34);
		this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnOk.TabIndex = 3;
		this.btnOk.Text = "確定";
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 18f);
		this.btnCancel.Location = new System.Drawing.Point(203, 126);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(96, 34);
		this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "取消";
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		base.AcceptButton = this.btnOk;
		this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
		base.ClientSize = new System.Drawing.Size(352, 162);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnOk);
		base.Controls.Add(this.labNumber);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Dialog";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.TopMost = true;
		base.ResumeLayout(false);
	}

	public Dialog()
	{
		Class2.sKBPqdpzNwCBA();
		InitializeComponent();
		LangHelper.ApplyLang(this);
		base.TopMost = true;
	}

	public void SetFocus(bool focus)
	{
		if (focus)
		{
			btnOk.TabIndex = 0;
		}
		else
		{
			btnCancel.TabIndex = 0;
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		Close();
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		try
		{
			Invoke((Action)delegate
			{
				labNumber.Text = MSGText;
			});
		}
		catch (Exception)
		{
		}
	}
}
