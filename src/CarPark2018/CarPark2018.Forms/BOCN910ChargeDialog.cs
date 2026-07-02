using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.Device;
using CarPark.Lib;
using CarPark2018.Properties;
using DAT.Entity;
using N910POSDll;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.Forms;

public class BOCN910ChargeDialog : Form
{
	private static ILog Logger;

	private IContainer components = null;

	private Button btnClose;

	private Label txtLog;

	private TextBox txtVal;

	private Label labInTime;

	private Panel panel1;

	public EnumBillType BillType { get; set; }

	public string CashType { get; set; }

	public decimal ReloadAmt { get; set; }

	public decimal SaleAmt { get; set; }

	public BOC_N910_POS_Card_Payment_DetailEX SaleResult { get; set; }

	public string ValType { get; set; }

	public BOCN910ChargeDialog()
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

	private void btnClose_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void QuickPassChargeDialog_Load(object sender, EventArgs e)
	{
		EnumBillType billType = BillType;
		EnumBillType enumBillType = billType;
		if (enumBillType == EnumBillType.PBOC)
		{
			txtVal.Text = SaleAmt.ToString("f2");
			Thread thread = new Thread(QuickPassCharge);
			thread.IsBackground = true;
			thread.Start();
		}
	}

	private void QuickPassCharge()
	{
		Action action = null;
		DateTime now = DateTime.Now;
		try
		{
			CheckLineResult checkLineResult = ((IBOC_N910)DeviceManager.FeeCenterModule).CHECKLINE();
			if (checkLineResult.RCODE != null && checkLineResult.STATUS != null && checkLineResult.RCODE == "0000000" && checkLineResult.STATUS == "A")
			{
				SaleResult retSale = ((IBOC_N910)DeviceManager.FeeCenterModule).SALE(SaleAmt);
				Invoke((Action)delegate
				{
					if (retSale.RCODE != null && retSale.STATUS != null && retSale.RCODE == "0000000" && retSale.STATUS == "A")
					{
						base.DialogResult = DialogResult.OK;
						BOC_N910_POS_Card_Payment_DetailEX saleResult = new BOC_N910_POS_Card_Payment_DetailEX
						{
							VERSION = retSale.VERSION,
							CMD = retSale.CMD,
							STATUS = retSale.STATUS,
							TXNDATE = retSale.TXNDATE,
							TXNTIME = retSale.TXNTIME,
							RCODE = retSale.RCODE,
							MESSAGE = retSale.MESSAGE,
							AMOUNT = (string.IsNullOrWhiteSpace(retSale.AMOUNT) ? 0m : decimal.Parse(retSale.AMOUNT)),
							AUTHCODE = retSale.AUTHCODE,
							PAN = retSale.PAN,
							EXPIRYDATE = retSale.EXPIRYDATE,
							ENTERMODE = retSale.ENTERMODE,
							TRACEID = retSale.TRACEID,
							TERMINALID = retSale.TERMINALID,
							MERCHANTID = retSale.MERCHANTID,
							CARDTYPE = retSale.CARDTYPE,
							CARDHOLDERNAME = retSale.CARDHOLDERNAME,
							BATCHNO = retSale.BATCHNO,
							REFERENCENO = retSale.REFERENCENO,
							ISSIGNATURE = retSale.ISSIGNATURE,
							REQUESTID = retSale.REQUESTID,
							ChargeRecordID = 0,
							TransactionID = 0
						};
						SaleResult = saleResult;
					}
					else
					{
						Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed"));
						base.DialogResult = DialogResult.Cancel;
					}
					Close();
				});
			}
			else
			{
				Invoke((Action)delegate
				{
					btnClose.Enabled = true;
					Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed"));
					base.DialogResult = DialogResult.Cancel;
					Close();
				});
			}
		}
		catch (Exception message)
		{
			Global.ShowMessage(LangManager.GetLangString("Alert.POSNotFound"));
			Logger.Error(message);
			Invoke((Action)delegate
			{
				btnClose.Enabled = true;
			});
		}
	}

	private bool CheckQuick(PBOCReadCardData readCard, ref string msg)
	{
		if (readCard.ReplyCode == "00")
		{
			if (readCard.CardNumber.Length > 10 && readCard.CardNumber.Substring(0, 6).Equals(Settings.Default.ShieldNum))
			{
				msg = "GateErrorCodes.ContactPBOC";
				return false;
			}
			if (readCard.OffLineRemain_MOP < SaleAmt)
			{
				msg = "GateErrorCodes.Not_Enough_Balance";
				return false;
			}
		}
		return true;
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
		this.btnClose.Enabled = false;
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
		this.txtLog.Font = new System.Drawing.Font("微软雅黑", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.txtLog.ForeColor = System.Drawing.Color.Navy;
		this.txtLog.Location = new System.Drawing.Point(130, 91);
		this.txtLog.Name = "txtLog";
		this.txtLog.Size = new System.Drawing.Size(345, 54);
		this.txtLog.TabIndex = 6;
		this.txtLog.Text = "請拍卡";
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
		this.Font = new System.Drawing.Font("微软雅黑", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(7);
		base.Name = "BOCN910ChargeDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "BOCN910ChargeDialog";
		base.TopMost = true;
		base.Load += new System.EventHandler(QuickPassChargeDialog_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
