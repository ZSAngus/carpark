using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark2018.Properties;
using MacauPass.POSCom.Package;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.Forms;

public class FormEpaySale : Form
{
	private static ILog Logger;

	private ChargeRecord m_ChargeRecord;

	private IContainer components = null;

	private Button btnMPass;

	private Button btnQPass;

	private Button btnCancel;

	private Label label1;

	private Panel panel1;

	private Panel panel2;

	private Button btnScan;

	public MPass_POS_Transaction_Detail MPass { get; set; }

	public BOC_Gate_TransactionExtend BOC { get; set; }

	public BOC_N910_POS_Card_Payment_DetailEX BOC_N910 { get; set; }

	public ScanPayment SPay { get; set; }

	public EnumPayType PayTypeFlag { get; set; }

	public EnumBillType BillType { get; set; }

	public ChargeRecord ChargeRecord
	{
		get
		{
			return m_ChargeRecord;
		}
		set
		{
			m_ChargeRecord = value;
		}
	}

	public FormEpaySale()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		m_ChargeRecord = null;
		btnMPass.BackgroundImage = ImageManager.GetImage("", "macaupass");
		btnMPass.BackgroundImageLayout = ImageLayout.Stretch;
		btnQPass.BackgroundImage = ImageManager.GetImage("", "QPass");
		btnQPass.BackgroundImageLayout = ImageLayout.Stretch;
		btnScan.BackgroundImage = ImageManager.GetImage("", "ScanPay");
		btnScan.BackgroundImageLayout = ImageLayout.Stretch;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		label1.Text = LangManager.GetLangString("CarPark.Forms.FormEpaySale.label1");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormEpaySale.btnCancel");
	}

	private void btnMPass_Click(object sender, EventArgs e)
	{
		PayTypeFlag = EnumPayType.MacauPass;
		BillType = EnumBillType.MacauPass;
		MPassChargeDialog mPassChargeDialog = new MPassChargeDialog
		{
			BillType = BillType,
			SaleAmt = m_ChargeRecord.TotalCharge,
			CardType = "MPCARD"
		};
		using MPassChargeDialog mPassChargeDialog2 = mPassChargeDialog;
		if (mPassChargeDialog2.ShowDialog() == DialogResult.OK)
		{
			SaleResult saleResult = mPassChargeDialog2.SaleResult;
			Application.DoEvents();
			m_ChargeRecord.BillType = 10;
			m_ChargeRecord.CardCode = (string.IsNullOrWhiteSpace(saleResult.PAN) ? saleResult.PAY_ACCOUNT : saleResult.PAN);
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = new MPass_POS_Transaction_Detail
			{
				ChargeTransactionID = m_ChargeRecord.TimeChargeID
			};
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail2 = mPass_POS_Transaction_Detail;
			EntityHelper.CopyEntity(saleResult, mPass_POS_Transaction_Detail2);
			mPass_POS_Transaction_Detail2.CashType = "MOP";
			MPass = mPass_POS_Transaction_Detail2;
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void btnQPass_Click(object sender, EventArgs e)
	{
		PayTypeFlag = EnumPayType.QuickPass;
		BillType = EnumBillType.PBOC;
		BOCN910ChargeDialog bOCN910ChargeDialog = new BOCN910ChargeDialog
		{
			BillType = BillType,
			SaleAmt = m_ChargeRecord.TotalCharge
		};
		using BOCN910ChargeDialog bOCN910ChargeDialog2 = bOCN910ChargeDialog;
		if (bOCN910ChargeDialog2.ShowDialog() == DialogResult.OK)
		{
			BOC_N910_POS_Card_Payment_DetailEX saleResult = bOCN910ChargeDialog2.SaleResult;
			Application.DoEvents();
			m_ChargeRecord.BillType = 11;
			m_ChargeRecord.CardCode = Common.EncryptedCardNumber(saleResult.PAN);
			saleResult.ChargeRecordID = m_ChargeRecord.TimeChargeID;
			BOC_N910 = saleResult;
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnScan_Click(object sender, EventArgs e)
	{
		if (Settings.Default.IsMPayByPAX)
		{
			PayTypeFlag = EnumPayType.MacauPass;
			BillType = EnumBillType.MacauPass;
			MPassChargeDialog mPassChargeDialog = new MPassChargeDialog
			{
				BillType = BillType,
				SaleAmt = m_ChargeRecord.TotalCharge,
				CardType = "MPay"
			};
			using MPassChargeDialog mPassChargeDialog2 = mPassChargeDialog;
			if (mPassChargeDialog2.ShowDialog() == DialogResult.OK)
			{
				SaleResult saleResult = mPassChargeDialog2.SaleResult;
				Application.DoEvents();
				m_ChargeRecord.subPayType = 2;
				m_ChargeRecord.BillType = 10;
				m_ChargeRecord.CardCode = (string.IsNullOrWhiteSpace(saleResult.PAN) ? saleResult.PAY_ACCOUNT : saleResult.PAN);
				MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = new MPass_POS_Transaction_Detail
				{
					ChargeTransactionID = m_ChargeRecord.TimeChargeID
				};
				MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail2 = mPass_POS_Transaction_Detail;
				EntityHelper.CopyEntity(saleResult, mPass_POS_Transaction_Detail2);
				mPass_POS_Transaction_Detail2.CashType = "MOP";
				MPass = mPass_POS_Transaction_Detail2;
				base.DialogResult = DialogResult.OK;
				Close();
			}
			return;
		}
		ScanPaymentChargeDialog scanPaymentChargeDialog = new ScanPaymentChargeDialog
		{
			BillType = BillType,
			SaleAmt = m_ChargeRecord.TotalCharge
		};
		using ScanPaymentChargeDialog scanPaymentChargeDialog2 = scanPaymentChargeDialog;
		if (scanPaymentChargeDialog2.ShowDialog() == DialogResult.OK)
		{
			if (scanPaymentChargeDialog2.SaleResultMPass != null)
			{
				SaleResult saleResultMPass = scanPaymentChargeDialog2.SaleResultMPass;
				Application.DoEvents();
				m_ChargeRecord.CardCode = (string.IsNullOrWhiteSpace(saleResultMPass.PAN) ? saleResultMPass.PAY_ACCOUNT : saleResultMPass.PAN);
				MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail3 = new MPass_POS_Transaction_Detail
				{
					ChargeTransactionID = m_ChargeRecord.TimeChargeID
				};
				MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail4 = mPass_POS_Transaction_Detail3;
				EntityHelper.CopyEntity(saleResultMPass, mPass_POS_Transaction_Detail4);
				mPass_POS_Transaction_Detail4.CashType = "MOP";
				MPass = mPass_POS_Transaction_Detail4;
				PayTypeFlag = EnumPayType.MacauPass;
				m_ChargeRecord.AuthCode = scanPaymentChargeDialog2.AuthCode;
			}
			else
			{
				SPay = scanPaymentChargeDialog2.SaleResultScanPay;
				PayTypeFlag = EnumPayType.BankQRCode;
				m_ChargeRecord.AuthCode = scanPaymentChargeDialog2.AuthCode;
			}
			base.DialogResult = DialogResult.OK;
			Close();
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
		this.btnMPass = new System.Windows.Forms.Button();
		this.btnQPass = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnScan = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.btnMPass.Location = new System.Drawing.Point(30, 17);
		this.btnMPass.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
		this.btnMPass.Name = "btnMPass";
		this.btnMPass.Size = new System.Drawing.Size(162, 67);
		this.btnMPass.TabIndex = 0;
		this.btnMPass.UseVisualStyleBackColor = true;
		this.btnMPass.Click += new System.EventHandler(btnMPass_Click);
		this.btnQPass.Location = new System.Drawing.Point(216, 17);
		this.btnQPass.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
		this.btnQPass.Name = "btnQPass";
		this.btnQPass.Size = new System.Drawing.Size(162, 67);
		this.btnQPass.TabIndex = 0;
		this.btnQPass.UseVisualStyleBackColor = true;
		this.btnQPass.Visible = false;
		this.btnQPass.Click += new System.EventHandler(btnQPass_Click);
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(216, 94);
		this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(162, 67);
		this.btnCancel.TabIndex = 0;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.label1.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.label1.Dock = System.Windows.Forms.DockStyle.Top;
		this.label1.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Navy;
		this.label1.Location = new System.Drawing.Point(0, 0);
		this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(593, 69);
		this.label1.TabIndex = 1;
		this.label1.Text = "選擇支付方式";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.Controls.Add(this.btnScan);
		this.panel1.Controls.Add(this.btnQPass);
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Controls.Add(this.btnMPass);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 69);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(593, 176);
		this.panel1.TabIndex = 2;
		this.btnScan.Location = new System.Drawing.Point(400, 17);
		this.btnScan.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
		this.btnScan.Name = "btnScan";
		this.btnScan.Size = new System.Drawing.Size(162, 67);
		this.btnScan.TabIndex = 1;
		this.btnScan.UseVisualStyleBackColor = true;
		this.btnScan.Click += new System.EventHandler(btnScan_Click);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.panel1);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(595, 247);
		this.panel2.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(11f, 21f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		base.ClientSize = new System.Drawing.Size(595, 247);
		base.Controls.Add(this.panel2);
		this.Font = new System.Drawing.Font("新細明體", 16f);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
		base.Name = "FormEpaySale";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormEpaySale";
		base.TopMost = true;
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
