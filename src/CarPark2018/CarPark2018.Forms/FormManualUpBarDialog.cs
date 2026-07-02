using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CarPark.Lib;

namespace CarPark2018.Forms;

public class FormManualUpBarDialog : Form
{
	private IContainer components;

	private Button btnCancel;

	private Button btnOk;

	private Label labNumber;

	public TextBox txtLP;

	private Panel panel1;

	private Label label1;

	private Label labWarning;

	public string MSGText { get; set; }

	public static event Action<string> m_FormManualUpBarDialog_Event;

	public FormManualUpBarDialog()
	{
		InitializeComponent();
		LangHelper.ApplyLang(this);
		base.Shown += delegate
		{
			base.ActiveControl = txtLP;
			txtLP.Focus();
		};
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

	private void btnOk_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		try
		{
			labNumber.Text = MSGText;
			txtLP.Focus();
		}
		catch (Exception)
		{
		}
	}

	public DialogResult ShowDialog(string message, bool OkFocus)
	{
		FormManualUpBarDialog formManualUpBarDialog = new FormManualUpBarDialog();
		formManualUpBarDialog.MSGText = message;
		formManualUpBarDialog.SetFocus(OkFocus);
		using (formManualUpBarDialog)
		{
			return formManualUpBarDialog.ShowDialog();
		}
	}

	private void FormManualUpBarDialog_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void txtLP_TextChanged(object sender, EventArgs e)
	{
		FormManualUpBarDialog.m_FormManualUpBarDialog_Event?.Invoke(txtLP.Text);
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
		this.labNumber = new System.Windows.Forms.Label();
		this.labWarning = new System.Windows.Forms.Label();
		this.btnOk = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.txtLP = new System.Windows.Forms.TextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.labNumber.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.labNumber.ForeColor = System.Drawing.Color.Navy;
		this.labNumber.Location = new System.Drawing.Point(3, 3);
		this.labNumber.Name = "labNumber";
		this.labNumber.Size = new System.Drawing.Size(342, 80);
		this.labNumber.TabIndex = 2;
		this.labNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labWarning.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Bold);
		this.labWarning.ForeColor = System.Drawing.Color.White;
		this.labWarning.BackColor = System.Drawing.Color.Red;
		this.labWarning.Text = "除系統故障外,必須輸入車牌";
		this.labWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labWarning.Size = new System.Drawing.Size(342, 38);
		this.labWarning.Location = new System.Drawing.Point(3, 88);
		this.labWarning.Padding = new System.Windows.Forms.Padding(3);
		this.labWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.labWarning.Name = "labWarning";
		this.txtLP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtLP.Font = new System.Drawing.Font("微软雅黑", 22f);
		this.txtLP.Location = new System.Drawing.Point(23, 130);
		this.txtLP.MaxLength = 7;
		this.txtLP.Name = "txtLP";
		this.txtLP.Size = new System.Drawing.Size(306, 44);
		this.txtLP.TabIndex = 0;
		this.txtLP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtLP.TextChanged += new System.EventHandler(txtLP_TextChanged);
		this.label1.Visible = false;
		this.btnOk.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.btnOk.ForeColor = System.Drawing.Color.Navy;
		this.btnOk.Location = new System.Drawing.Point(53, 190);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(96, 34);
		this.btnOk.TabIndex = 1;
		this.btnOk.Text = "確定";
		this.btnOk.UseVisualStyleBackColor = true;
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(204, 190);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(96, 34);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel1.Controls.Add(this.labNumber);
		this.panel1.Controls.Add(this.labWarning);
		this.panel1.Controls.Add(this.txtLP);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(352, 190);
		this.panel1.TabIndex = 6;
		base.AcceptButton = this.btnOk;
		this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(352, 235);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.btnOk);
		base.Controls.Add(this.btnCancel);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormManualUpBarDialog";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormManualUpBarDialog_FormClosing);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
