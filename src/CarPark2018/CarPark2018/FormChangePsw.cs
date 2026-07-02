using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;

namespace CarPark2018;

public class FormChangePsw : Form
{
	private StaffInfo m_StaffInfo;

	private UpdatePasswordArgs updateArg;

	private IContainer components = null;

	private TextBox txtNew;

	private Label labPwd;

	private TextBox txtOld;

	private Label labNumber;

	private Button btnCancel;

	private Button btnOK;

	private Label labConfirm;

	private TextBox txtConfirm;

	private Panel panel1;

	public FormChangePsw()
	{
		m_StaffInfo = null;
		updateArg = new UpdatePasswordArgs();
		InitializeComponent();
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labNumber.Text = LangManager.GetLangString("CarPark.FormChangePsw.labNumber");
		labPwd.Text = LangManager.GetLangString("CarPark.FormChangePsw.labPwd");
		labConfirm.Text = LangManager.GetLangString("CarPark.FormChangePsw.labConfirm");
		btnOK.Text = LangManager.GetLangString("CarPark.FormChangePsw.btnOk");
		btnCancel.Text = LangManager.GetLangString("CarPark.FormChangePsw.btnCancel");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		try
		{
			if (DataBuffer2018.CurrentStaff.StaffPwd != txtOld.Text)
			{
				Global.ShowMessage(LangManager.GetLangString("ERR_WRONG_PWD"));
				return;
			}
			if (string.IsNullOrEmpty(txtNew.Text) || string.IsNullOrEmpty(txtConfirm.Text) || txtConfirm.Text != txtNew.Text)
			{
				Global.ShowMessage(LangManager.GetLangString("ERR_CHECK_PWD"));
				return;
			}
			updateArg.NewPassword = txtNew.Text;
			updateArg.OldPassword = txtOld.Text;
			updateArg.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			ChargeContext chargeContext = new ChargeContext();
			UpdatePasswordReturn updatePasswordReturn = chargeContext.CommunicationChannel.UpdatePassword(updateArg);
			chargeContext.CommunicationChannel.Disconnect();
			if (updatePasswordReturn.ISOK)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				Global.ShowMessage(updatePasswordReturn.ErrCode);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
		}
		catch (Exception ex2)
		{
			Global.ShowMessage(LangManager.GetLangString(ex2.Message));
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
		this.txtNew = new System.Windows.Forms.TextBox();
		this.labPwd = new System.Windows.Forms.Label();
		this.txtOld = new System.Windows.Forms.TextBox();
		this.labNumber = new System.Windows.Forms.Label();
		this.btnCancel = new System.Windows.Forms.Button();
		this.btnOK = new System.Windows.Forms.Button();
		this.labConfirm = new System.Windows.Forms.Label();
		this.txtConfirm = new System.Windows.Forms.TextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.txtNew.Location = new System.Drawing.Point(192, 78);
		this.txtNew.Name = "txtNew";
		this.txtNew.PasswordChar = '*';
		this.txtNew.Size = new System.Drawing.Size(209, 34);
		this.txtNew.TabIndex = 2;
		this.txtNew.UseSystemPasswordChar = true;
		this.labPwd.Location = new System.Drawing.Point(66, 78);
		this.labPwd.Name = "labPwd";
		this.labPwd.Size = new System.Drawing.Size(120, 34);
		this.labPwd.TabIndex = 6;
		this.labPwd.Text = "新密碼";
		this.labPwd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtOld.Location = new System.Drawing.Point(192, 30);
		this.txtOld.Name = "txtOld";
		this.txtOld.PasswordChar = '*';
		this.txtOld.Size = new System.Drawing.Size(209, 34);
		this.txtOld.TabIndex = 1;
		this.txtOld.UseSystemPasswordChar = true;
		this.labNumber.Location = new System.Drawing.Point(66, 30);
		this.labNumber.Name = "labNumber";
		this.labNumber.Size = new System.Drawing.Size(120, 34);
		this.labNumber.TabIndex = 5;
		this.labNumber.Text = "原密碼";
		this.labNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(259, 177);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(100, 40);
		this.btnCancel.TabIndex = 5;
		this.btnCancel.TabStop = false;
		this.btnCancel.Text = "取消";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.btnOK.Location = new System.Drawing.Point(105, 177);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(100, 40);
		this.btnOK.TabIndex = 4;
		this.btnOK.TabStop = false;
		this.btnOK.Text = "確認";
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOk_Click);
		this.labConfirm.Location = new System.Drawing.Point(66, 126);
		this.labConfirm.Name = "labConfirm";
		this.labConfirm.Size = new System.Drawing.Size(120, 34);
		this.labConfirm.TabIndex = 6;
		this.labConfirm.Text = "確認密碼";
		this.labConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtConfirm.Location = new System.Drawing.Point(192, 126);
		this.txtConfirm.Name = "txtConfirm";
		this.txtConfirm.PasswordChar = '*';
		this.txtConfirm.Size = new System.Drawing.Size(209, 34);
		this.txtConfirm.TabIndex = 3;
		this.txtConfirm.UseSystemPasswordChar = true;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Controls.Add(this.txtConfirm);
		this.panel1.Controls.Add(this.btnOK);
		this.panel1.Controls.Add(this.labConfirm);
		this.panel1.Controls.Add(this.txtOld);
		this.panel1.Controls.Add(this.txtNew);
		this.panel1.Controls.Add(this.labNumber);
		this.panel1.Controls.Add(this.labPwd);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(469, 248);
		this.panel1.TabIndex = 7;
		base.AcceptButton = this.btnOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(469, 248);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ForeColor = System.Drawing.Color.Navy;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "FormChangePsw";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormChangePsw";
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
