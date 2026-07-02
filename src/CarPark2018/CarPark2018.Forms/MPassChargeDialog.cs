using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.Device;
using CarPark.Lib;
using CarPark2018.Properties;
using MacauPass.POSCom.Package;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.Forms;

public class MPassChargeDialog : Form
{
	private static ILog Logger;

	private IContainer components = null;

	private Label labInTime;

	private TextBox txtVal;

	private Label txtLog;

	private Button btnClose;

	private Panel panel1;

	public EnumBillType BillType { get; set; }

	public string CashType { get; set; }

	public decimal ReloadAmt { get; set; }

	public ReloadResult ReloadResult { get; set; }

	public decimal SaleAmt { get; set; }

	public SaleResult SaleResult { get; set; }

	public string ValType { get; set; }

	public string CardType { get; set; }

	public MPassChargeDialog()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labInTime.Text = LangManager.GetLangString("CarPark.Forms.MPassChargeDialog.labInTime");
		btnClose.Text = LangManager.GetLangString("CarPark.Forms.MPassChargeDialog.btnClose");
		txtLog.Text = LangManager.GetLangString("CarPark.Forms.MPassChargeDialog.txtLog");
	}

	private void MPassChargeDialog_Load(object sender, EventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			((IFeeCenterV5)DeviceManager.FeeCenterModule).QRCodeScanPayEvent += FormLPPayLost_QRCodeScanEvent;
		}
		switch (BillType)
		{
		case EnumBillType.MacauPass:
			method_0();
			break;
		case EnumBillType.MacauPassDecal:
			DealMPReload();
			break;
		}
	}

	private void method_0()
	{
		txtVal.Text = SaleAmt.ToString("f2");
		Thread thread = new Thread((ThreadStart)delegate
		{
			Action action = null;
			try
			{
				if (CardType == "MPCARD")
				{
					SaleResult = ((IMPPOSTranscation)DeviceManager.FeeCenterModule).Sale(SaleAmt);
				}
				else
				{
					if (!Settings.Default.IsMPayByPAX)
					{
						if (action == null)
						{
							action = delegate
							{
								btnClose.Enabled = true;
							};
						}
						Invoke(action);
						return;
					}
					SaleResult = ((IMPPOSMPay)DeviceManager.FeeCenterModule).SaleMPay(SaleAmt, null);
				}
				if (action == null)
				{
					action = delegate
					{
						if (SaleResult.CommandResult == CommandResult.Success)
						{
							base.DialogResult = DialogResult.OK;
						}
						else
						{
							Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed") + Environment.NewLine + SaleResult.ErrDescription);
							base.DialogResult = DialogResult.Cancel;
						}
						Close();
					};
				}
				Invoke(action);
			}
			catch (Exception message)
			{
				Global.ShowMessage(LangManager.GetLangString("Alert.POSNotFound"));
				Logger.Error(message);
				try
				{
					Invoke((Action)delegate
					{
						base.DialogResult = DialogResult.Cancel;
						Close();
					});
				}
				catch (Exception)
				{
				}
			}
		});
		thread.IsBackground = true;
		thread.Start();
	}

	private void DealMPReload()
	{
		txtVal.Text = ReloadAmt.ToString("f2");
		Thread thread = new Thread((ThreadStart)delegate
		{
			Action action = null;
			try
			{
				if (CardType == "MPCARD")
				{
					ReloadResult = ((IMPPOSTranscation)DeviceManager.FeeCenterModule).Reload(ReloadAmt, CashType, ValType);
				}
				else
				{
					if (!Settings.Default.IsMPayByPAX)
					{
						if (action == null)
						{
							action = delegate
							{
								btnClose.Enabled = true;
							};
						}
						Invoke(action);
						return;
					}
					ReloadResult = ((IMPPOSMPay)DeviceManager.FeeCenterModule).ReloadMPay(ReloadAmt, CashType, ValType, null);
				}
				if (action == null)
				{
					action = delegate
					{
						if (ReloadResult.CommandResult == CommandResult.Success)
						{
							base.DialogResult = DialogResult.OK;
						}
						else
						{
							Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed") + Environment.NewLine + ReloadResult.ErrDescription);
							base.DialogResult = DialogResult.Cancel;
						}
						Close();
					};
				}
				Invoke(action);
			}
			catch (Exception message)
			{
				Global.ShowMessage(LangManager.GetLangString("Alert.POSNotFound"));
				Logger.Error(message);
				try
				{
					Invoke((Action)delegate
					{
						base.DialogResult = DialogResult.Cancel;
						Close();
					});
				}
				catch (Exception)
				{
				}
			}
		});
		thread.IsBackground = true;
		thread.Start();
	}

	[CompilerGenerated]
	private void DealMPSaleb__1()
	{
		Action action = null;
		try
		{
			if (CardType == "MPCARD")
			{
				SaleResult = ((IMPPOSTranscation)DeviceManager.FeeCenterModule).Sale(SaleAmt);
			}
			else
			{
				if (!Settings.Default.IsMPayByPAX)
				{
					if (action == null)
					{
						action = delegate
						{
							btnClose.Enabled = true;
						};
					}
					Invoke(action);
					return;
				}
				SaleResult = ((IMPPOSMPay)DeviceManager.FeeCenterModule).SaleMPay(SaleAmt, null);
			}
			if (action == null)
			{
				action = delegate
				{
					if (SaleResult.CommandResult == CommandResult.Success)
					{
						base.DialogResult = DialogResult.OK;
					}
					else
					{
						Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed") + Environment.NewLine + SaleResult.ErrDescription);
						base.DialogResult = DialogResult.Cancel;
					}
					Close();
				};
			}
			Invoke(action);
		}
		catch (Exception message)
		{
			Global.ShowMessage(LangManager.GetLangString("Alert.POSNotFound"));
			Logger.Error(message);
			try
			{
				Invoke((Action)delegate
				{
					base.DialogResult = DialogResult.Cancel;
					Close();
				});
			}
			catch (Exception)
			{
			}
		}
	}

	[CompilerGenerated]
	private void DealMPSaleb__2()
	{
		if (SaleResult.CommandResult == CommandResult.Success)
		{
			base.DialogResult = DialogResult.OK;
		}
		else
		{
			Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed") + Environment.NewLine + SaleResult.ErrDescription);
			base.DialogResult = DialogResult.Cancel;
		}
		Close();
	}

	[CompilerGenerated]
	private void DealMPSaleb__3()
	{
	}

	private void button1_Click(object sender, EventArgs e)
	{
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void MPassChargeDialog_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			((IFeeCenterV5)DeviceManager.FeeCenterModule).QRCodeScanPayEvent -= FormLPPayLost_QRCodeScanEvent;
		}
	}

	private void FormLPPayLost_QRCodeScanEvent(string code)
	{
		try
		{
			Invoke((MethodInvoker)delegate
			{
				if (CardType == "MPAY" && BillType == EnumBillType.MacauPass)
				{
					SaleResult = ((IMPPOSMPay)DeviceManager.FeeCenterModule).SaleMPay(SaleAmt, code);
					if (SaleResult.CommandResult == CommandResult.Success)
					{
						base.DialogResult = DialogResult.OK;
					}
					else
					{
						Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed") + Environment.NewLine + SaleResult.ErrDescription);
						base.DialogResult = DialogResult.Cancel;
					}
					Close();
				}
				else if (CardType == "MPAY" && BillType == EnumBillType.MacauPassDecal)
				{
					ReloadResult = ((IMPPOSMPay)DeviceManager.FeeCenterModule).ReloadMPay(ReloadAmt, CashType, ValType, code);
					if (ReloadResult.CommandResult == CommandResult.Success)
					{
						base.DialogResult = DialogResult.OK;
					}
					else
					{
						Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed") + Environment.NewLine + ReloadResult.ErrDescription);
						base.DialogResult = DialogResult.Cancel;
					}
					Close();
				}
			});
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
		this.labInTime = new System.Windows.Forms.Label();
		this.txtVal = new System.Windows.Forms.TextBox();
		this.txtLog = new System.Windows.Forms.Label();
		this.btnClose = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.labInTime.ForeColor = System.Drawing.Color.Navy;
		this.labInTime.Location = new System.Drawing.Point(127, 31);
		this.labInTime.Name = "labInTime";
		this.labInTime.Size = new System.Drawing.Size(149, 28);
		this.labInTime.TabIndex = 0;
		this.labInTime.Text = "交易金額";
		this.labInTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtVal.Location = new System.Drawing.Point(282, 28);
		this.txtVal.Name = "txtVal";
		this.txtVal.ReadOnly = true;
		this.txtVal.Size = new System.Drawing.Size(193, 35);
		this.txtVal.TabIndex = 1;
		this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtLog.Font = new System.Drawing.Font("微软雅黑", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.txtLog.ForeColor = System.Drawing.Color.Navy;
		this.txtLog.Location = new System.Drawing.Point(129, 90);
		this.txtLog.Name = "txtLog";
		this.txtLog.Size = new System.Drawing.Size(345, 54);
		this.txtLog.TabIndex = 2;
		this.txtLog.Text = "請拍卡";
		this.txtLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnClose.Enabled = false;
		this.btnClose.ForeColor = System.Drawing.Color.Navy;
		this.btnClose.Location = new System.Drawing.Point(240, 171);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(122, 43);
		this.btnClose.TabIndex = 3;
		this.btnClose.Text = "取消";
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.txtVal);
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Controls.Add(this.labInTime);
		this.panel1.Controls.Add(this.txtLog);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(605, 244);
		this.panel1.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(13f, 28f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		base.ClientSize = new System.Drawing.Size(605, 244);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("微软雅黑", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(7);
		base.Name = "MPassChargeDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "MPassChargeDialog";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MPassChargeDialog_FormClosing);
		base.Load += new System.EventHandler(MPassChargeDialog_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
