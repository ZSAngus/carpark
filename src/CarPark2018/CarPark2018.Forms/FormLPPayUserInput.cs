using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CarPark2018.UserControls;
using log4net;

namespace CarPark2018.Forms;

public class FormLPPayUserInput : Form
{
	private ILog Logger;

	private static FormLPPayUserInput frmUser;

	public GlobalFormObj.LPInputEventHandler m_lpInputEvent;

	private IContainer components = null;

	private Label labLP;

	private TextBox txtLPInput;

	private MyKeyBoard myKeyBoard1;

	public event GlobalFormObj.LPInputEventHandler LPInputEvent
	{
		add
		{
			GlobalFormObj.LPInputEventHandler lPInputEventHandler = m_lpInputEvent;
			GlobalFormObj.LPInputEventHandler lPInputEventHandler2;
			do
			{
				lPInputEventHandler2 = lPInputEventHandler;
				GlobalFormObj.LPInputEventHandler value2 = (GlobalFormObj.LPInputEventHandler)Delegate.Combine(lPInputEventHandler2, value);
				lPInputEventHandler = Interlocked.CompareExchange(ref m_lpInputEvent, value2, lPInputEventHandler2);
			}
			while (lPInputEventHandler != lPInputEventHandler2);
		}
		remove
		{
			GlobalFormObj.LPInputEventHandler lPInputEventHandler = m_lpInputEvent;
			GlobalFormObj.LPInputEventHandler lPInputEventHandler2;
			do
			{
				lPInputEventHandler2 = lPInputEventHandler;
				GlobalFormObj.LPInputEventHandler value2 = (GlobalFormObj.LPInputEventHandler)Delegate.Remove(lPInputEventHandler2, value);
				lPInputEventHandler = Interlocked.CompareExchange(ref m_lpInputEvent, value2, lPInputEventHandler2);
			}
			while (lPInputEventHandler != lPInputEventHandler2);
		}
	}

	public static FormLPPayUserInput Self()
	{
		if (frmUser == null)
		{
			frmUser = new FormLPPayUserInput();
			int num = Screen.GetWorkingArea(frmUser).Width;
			frmUser.Location = new Point(num, 0);
			frmUser.Show();
		}
		return frmUser;
	}

	public FormLPPayUserInput()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		InitializeComponent();
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		Console.WriteLine("ProcessCmdKey : " + keyData);
		base.ProcessCmdKey(ref msg, keyData);
		switch (keyData)
		{
		case Keys.D0:
		case Keys.D1:
		case Keys.D2:
		case Keys.D3:
		case Keys.D4:
		case Keys.D5:
		case Keys.D6:
		case Keys.D7:
		case Keys.D8:
		case Keys.D9:
		case Keys.A:
		case Keys.B:
		case Keys.C:
		case Keys.D:
		case Keys.E:
		case Keys.F:
		case Keys.G:
		case Keys.H:
		case Keys.I:
		case Keys.J:
		case Keys.K:
		case Keys.L:
		case Keys.M:
		case Keys.N:
		case Keys.O:
		case Keys.P:
		case Keys.Q:
		case Keys.R:
		case Keys.S:
		case Keys.T:
		case Keys.U:
		case Keys.V:
		case Keys.W:
		case Keys.X:
		case Keys.Y:
		case Keys.Z:
			SetInput(keyData.ToString());
			break;
		case Keys.Space:
			SetInput(" ");
			break;
		case Keys.Return:
			EnterKey();
			return true;
		case Keys.Escape:
			return true;
		}
		return true;
	}

	public void SetInput(string keyValue)
	{
		if (keyValue.Length > 1)
		{
			keyValue = keyValue.Substring(1, keyValue.Length - 1);
		}
		if (GlobalFormObj.LicenseplateEdit.Length < 6)
		{
			GlobalFormObj.LicenseplateEdit += keyValue;
			txtLPInput.Text = GlobalFormObj.LicenseplateEdit;
		}
	}

	public void EnterKey()
	{
		try
		{
			if (m_lpInputEvent != null)
			{
				Logger.Info("EnterKey " + GlobalFormObj.LicenseplateEdit);
				m_lpInputEvent(GlobalFormObj.LicenseplateEdit);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public void SetLP(string value)
	{
		try
		{
			txtLPInput.Text = value;
		}
		catch (Exception message)
		{
			Logger.Error(message);
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
		this.labLP = new System.Windows.Forms.Label();
		this.txtLPInput = new System.Windows.Forms.TextBox();
		this.myKeyBoard1 = new CarPark2018.UserControls.MyKeyBoard();
		base.SuspendLayout();
		this.labLP.Font = new System.Drawing.Font("微软雅黑", 35f, System.Drawing.FontStyle.Bold);
		this.labLP.ForeColor = System.Drawing.Color.Navy;
		this.labLP.Location = new System.Drawing.Point(0, 68);
		this.labLP.Name = "labLP";
		this.labLP.Size = new System.Drawing.Size(400, 60);
		this.labLP.TabIndex = 0;
		this.labLP.Text = "請輸入車牌";
		this.labLP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtLPInput.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtLPInput.Font = new System.Drawing.Font("微软雅黑", 40f, System.Drawing.FontStyle.Bold);
		this.txtLPInput.Location = new System.Drawing.Point(406, 58);
		this.txtLPInput.MaxLength = 6;
		this.txtLPInput.Name = "txtLPInput";
		this.txtLPInput.Size = new System.Drawing.Size(570, 78);
		this.txtLPInput.TabIndex = 1;
		this.myKeyBoard1.BackColor = System.Drawing.Color.FromArgb(30, 44, 55);
		this.myKeyBoard1.Location = new System.Drawing.Point(0, 154);
		this.myKeyBoard1.Name = "myKeyBoard1";
		this.myKeyBoard1.Size = new System.Drawing.Size(1180, 480);
		this.myKeyBoard1.TabIndex = 16;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(1186, 768);
		base.Controls.Add(this.txtLPInput);
		base.Controls.Add(this.labLP);
		base.Controls.Add(this.myKeyBoard1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormLPPayUser";
		this.Text = "FormLPPayUser";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
