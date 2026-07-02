using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using BOC_SmartPay;
using CarPark.Core;
using CarPark.DB;
using CarPark.Device;
using CarPark.Lib;
using CarPark2018.Properties;
using MacauPass.POSCom.Package;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.Forms;

public class ScanPaymentChargeDialog : Form
{
	private static ILog Logger;

	private IContainer components = null;

	private Button btnClose;

	private Label txtLog;

	private TextBox txtVal;

	private Label labInTime;

	private Panel panel1;

	public string CardType { get; set; }

	public string AuthCode { get; set; }

	public EnumBillType BillType { get; set; }

	public string CashType { get; set; }

	public decimal ReloadAmt { get; set; }

	public decimal SaleAmt { get; set; }

	public SaleResult SaleResultMPass { get; set; }

	public ScanPayment SaleResultScanPay { get; set; }

	public string ValType { get; set; }

	public ScanPaymentChargeDialog()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labInTime.Text = LangManager.GetLangString("CarPark.Forms.MPassChargeDialog.labInTime");
		btnClose.Text = LangManager.GetLangString("CarPark.Forms.MPassChargeDialog.btnClose");
		txtLog.Text = "請掃碼\r\n(PleaseScan)";
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void QuickPassChargeDialog_Load(object sender, EventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			((IFeeCenterV5)DeviceManager.FeeCenterModule).QRCodeScanPayEvent += FormLPPayLost_QRCodeScanEvent;
		}
		txtVal.Text = SaleAmt.ToString("f2");
	}

	private void FormLPPayLost_QRCodeScanEvent(string code)
	{
		try
		{
			if (code.Length < 10)
			{
				Logger.Error("code.Length < 10");
				Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed"));
				base.DialogResult = DialogResult.Cancel;
				Close();
			}
		}
		catch (Exception ex)
		{
			Logger.Error("code.Exception:" + ex.Message);
			Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed"));
			base.DialogResult = DialogResult.Cancel;
			Close();
		}
		try
		{
			Invoke((MethodInvoker)delegate
			{
				try
				{
					txtLog.Text = "掃碼成功(" + code + ")，請在手機上確認是否需要輸入密碼\r\nScan OK,Please check the mobile APP to confirm the inputting of password";
					if (DeviceManager.FeeCenterModule != null)
					{
						((IFeeCenterV5)DeviceManager.FeeCenterModule).QRCodeScanPayEvent -= FormLPPayLost_QRCodeScanEvent;
					}
					btnClose.Enabled = false;
				}
				catch (Exception ex3)
				{
					Logger.Error("QRCodeScanPayEvent:" + ex3.Message);
					Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed"));
					base.DialogResult = DialogResult.Cancel;
					Close();
				}
				CardType = CheckPayType(code);
				if (CardType == "MPAY")
				{
					Application.DoEvents();
					SaleResultMPass = ((IMPPOSMPay)DeviceManager.FeeCenterModule).SaleMPay(SaleAmt, AuthCode);
					if (SaleResultMPass.CommandResult == CommandResult.Success)
					{
						base.DialogResult = DialogResult.OK;
					}
					else
					{
						Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed") + Environment.NewLine + SaleResultMPass.ErrDescription);
						base.DialogResult = DialogResult.Cancel;
					}
				}
				else if (CardType == "BOC")
				{
					Application.DoEvents();
					string text = BOCSmartPayHandler(AuthCode);
					if (text == "000")
					{
						base.DialogResult = DialogResult.OK;
					}
					else
					{
						Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed"));
						base.DialogResult = DialogResult.Cancel;
					}
				}
				else
				{
					Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed"));
					base.DialogResult = DialogResult.Cancel;
				}
				Close();
			});
		}
		catch (Exception message)
		{
			Logger.Error(message);
			Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed"));
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

	private string CheckPayType(string qrcode)
	{
		string text = "";
		AuthCode = "";
		string text2 = qrcode.Substring(0, 2);
		if (string.IsNullOrEmpty(text2))
		{
			text = "";
		}
		else
		{
			switch (text2)
			{
			case "88":
				text = "MPAY";
				AuthCode = qrcode;
				break;
			case "99":
				text = "BOC";
				AuthCode = qrcode;
				break;
			case "13":
				text = "BOC";
				AuthCode = qrcode;
				break;
			case "28":
				text = "BOC";
				AuthCode = qrcode;
				break;
			default:
				text = "BOC";
				AuthCode = qrcode;
				break;
			}
		}
		return text;
	}

	private void SmartPayChargeDialog_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			((IFeeCenterV5)DeviceManager.FeeCenterModule).QRCodeScanPayEvent -= FormLPPayLost_QRCodeScanEvent;
		}
	}

	private string BOCSmartPayHandler(string qrcode)
	{
		BOC_SmartPayManager bOC_SmartPayManager = new BOC_SmartPayManager();
		bOC_SmartPayManager.ParkingLotNo = Settings.Default.ParkingLotNo;
		bOC_SmartPayManager.PaymentServerUrl = Settings.Default.PaymentServerUrl;
		bOC_SmartPayManager.OnlyID = Settings.Default.OnlyID;
		B2CPayArgs b2CPayArgs = new B2CPayArgs
		{
			ChargeTime = DateTime.Now,
			TimeOutNum = Settings.Default.TimeOutNum,
			Subject = "ParkingFee",
			Amount = SaleAmt,
			AuthCode = qrcode
		};
		ReturnBase returnBase = bOC_SmartPayManager.B2CPay(b2CPayArgs);
		if (returnBase != null && returnBase.ReturnCode == "000")
		{
			ScanPayment scanPayment = new ScanPayment();
			scanPayment.MerchantId = returnBase.MerchantId;
			scanPayment.TrmNo = returnBase.TrmNo;
			scanPayment.PayOrderNo = returnBase.PayOrderNo;
			scanPayment.LogNo = returnBase.PayLogNo;
			scanPayment.ChargeTime = b2CPayArgs.ChargeTime.ToString("yyyy-MM-dd hh:mm:ss");
			scanPayment.Subject = b2CPayArgs.Subject;
			scanPayment.Amount = SaleAmt;
			scanPayment.AuthCode = b2CPayArgs.AuthCode;
			scanPayment.PayType = returnBase.PayType;
			scanPayment.UserData = returnBase.UserID;
			scanPayment.ParkingLotNo = Settings.Default.ParkingLotNo;
			scanPayment.Result = returnBase.ReturnCode;
			scanPayment.ResultMessage = returnBase.ReturnMessage;
			SaleResultScanPay = scanPayment;
			return returnBase.ReturnCode;
		}
		if (returnBase != null && returnBase.ReturnCode == "001")
		{
			ScanPayment scanPayment2 = new ScanPayment();
			scanPayment2.MerchantId = returnBase.MerchantId;
			scanPayment2.TrmNo = returnBase.TrmNo;
			scanPayment2.PayOrderNo = returnBase.PayOrderNo;
			scanPayment2.LogNo = returnBase.PayLogNo;
			scanPayment2.ChargeTime = b2CPayArgs.ChargeTime.ToString("yyyy-MM-dd hh:mm:ss");
			scanPayment2.Subject = b2CPayArgs.Subject;
			scanPayment2.Amount = SaleAmt;
			scanPayment2.AuthCode = b2CPayArgs.AuthCode;
			scanPayment2.PayType = returnBase.PayType;
			scanPayment2.UserData = returnBase.UserID;
			scanPayment2.ParkingLotNo = Settings.Default.ParkingLotNo;
			scanPayment2.Result = returnBase.ReturnCode;
			scanPayment2.ResultMessage = returnBase.ReturnMessage;
			SaleResultScanPay = scanPayment2;
			return returnBase.ReturnCode;
		}
		int num = b2CPayArgs.TimeOutNum / 5 + 1;
		ReturnBase returnBase2 = null;
		string result = "001";
		while (num > 0)
		{
			try
			{
				num--;
				Thread thread = new Thread((ParameterizedThreadStart)delegate
				{
					Thread.Sleep(5000);
				});
				thread.Start(this);
				while (thread.IsAlive)
				{
					Application.DoEvents();
				}
				returnBase2 = bOC_SmartPayManager.OrderQuery(bOC_SmartPayManager.PayOrderNoNow, string.IsNullOrWhiteSpace(bOC_SmartPayManager.PayLogNo) ? null : bOC_SmartPayManager.PayLogNo);
				if (returnBase2.ReturnCode == "000" || returnBase2.ReturnCode == "001")
				{
					ScanPayment scanPayment3 = new ScanPayment();
					scanPayment3.MerchantId = returnBase2.MerchantId;
					scanPayment3.TrmNo = returnBase2.TrmNo;
					scanPayment3.PayOrderNo = returnBase2.PayOrderNo;
					scanPayment3.LogNo = returnBase2.PayLogNo;
					scanPayment3.ChargeTime = b2CPayArgs.ChargeTime.ToString("yyyy-MM-dd hh:mm:ss");
					scanPayment3.Subject = b2CPayArgs.Subject;
					scanPayment3.Amount = SaleAmt;
					scanPayment3.AuthCode = b2CPayArgs.AuthCode;
					scanPayment3.PayType = returnBase2.PayType;
					scanPayment3.UserData = returnBase2.UserID;
					scanPayment3.ParkingLotNo = Settings.Default.ParkingLotNo;
					scanPayment3.Result = returnBase2.ReturnCode;
					scanPayment3.ResultMessage = returnBase2.ReturnMessage;
					SaleResultScanPay = scanPayment3;
					result = returnBase2.ReturnCode;
					break;
				}
			}
			catch (Exception message)
			{
				Logger.Error(message);
			}
		}
		return result;
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
		this.btnClose = new System.Windows.Forms.Button();
		this.txtLog = new System.Windows.Forms.Label();
		this.txtVal = new System.Windows.Forms.TextBox();
		this.labInTime = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.btnClose.ForeColor = System.Drawing.Color.Navy;
		this.btnClose.Location = new System.Drawing.Point(241, 172);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(122, 43);
		this.btnClose.TabIndex = 7;
		this.btnClose.Text = "取消";
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtLog.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.txtLog.ForeColor = System.Drawing.Color.Navy;
		this.txtLog.Location = new System.Drawing.Point(12, 81);
		this.txtLog.Name = "txtLog";
		this.txtLog.Size = new System.Drawing.Size(581, 77);
		this.txtLog.TabIndex = 6;
		this.txtLog.Text = "請掃碼";
		this.txtLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtVal.Location = new System.Drawing.Point(283, 29);
		this.txtVal.Name = "txtVal";
		this.txtVal.ReadOnly = true;
		this.txtVal.Size = new System.Drawing.Size(193, 35);
		this.txtVal.TabIndex = 5;
		this.labInTime.ForeColor = System.Drawing.Color.Navy;
		this.labInTime.Location = new System.Drawing.Point(128, 32);
		this.labInTime.Name = "labInTime";
		this.labInTime.Size = new System.Drawing.Size(149, 28);
		this.labInTime.TabIndex = 4;
		this.labInTime.Text = "交易金額";
		this.labInTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panel1.Controls.Add(this.txtVal);
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Controls.Add(this.labInTime);
		this.panel1.Controls.Add(this.txtLog);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(605, 244);
		this.panel1.TabIndex = 8;
		base.AutoScaleDimensions = new System.Drawing.SizeF(13f, 28f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		base.ClientSize = new System.Drawing.Size(605, 244);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Microsoft YaHei", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(7);
		base.Name = "ScanPaymentChargeDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "ScanPayment";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SmartPayChargeDialog_FormClosing);
		base.Load += new System.EventHandler(QuickPassChargeDialog_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
