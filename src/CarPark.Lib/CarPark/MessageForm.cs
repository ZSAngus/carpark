using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CarPark.Lib;
using DevComponents.DotNetBar;

namespace CarPark;

public class MessageForm : Office2007Form
{
	private IContainer components;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarPark.MessageForm));
		this.labNumber = new DevComponents.DotNetBar.LabelX();
		this.btnOk = new DevComponents.DotNetBar.ButtonX();
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
		this.btnOk.Location = new System.Drawing.Point(133, 126);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(96, 34);
		this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnOk.TabIndex = 3;
		this.btnOk.Text = "確定";
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		base.AcceptButton = this.btnOk;
		this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
		base.ClientSize = new System.Drawing.Size(352, 162);
		base.Controls.Add(this.btnOk);
		base.Controls.Add(this.labNumber);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "MessageForm";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.TopMost = true;
		base.ResumeLayout(false);
	}

	public MessageForm()
	{
		Class2.sKBPqdpzNwCBA();
		InitializeComponent();
		LangHelper.ApplyLang(this);
		base.TopMost = true;
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
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
