using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.Lib;
using CarPark2018.Properties;
using MacauPass.POSCom.Package;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.Forms;

public class FormMPassQuery : Form
{
	private ILog Logger;

	private IContainer components;

	private Label labTitle;

	private Panel panel1;

	private readonly DateTime initTime;

	private ChargeRecord m_ChargeRecord;

	private decimal charge_value;

	private Button btnCancel;

	private Panel panel2;

	private TextBox txtTicketNo;

	private Label labTicketNo;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem btnCancelFree;

	private Panel panFill;

	private Label label1;

	private Button btnMPass;

	private Button btnMpay;

	private Button btnBalanceCheck;

	public EnumBillType BillType { get; set; }

	public FormMPassQuery()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		initTime = DateTime.Now;
		m_ChargeRecord = null;
		InitializeComponent();
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
		this.components = new System.ComponentModel.Container();
		this.labTitle = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnMpay = new System.Windows.Forms.Button();
		this.btnMPass = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.txtTicketNo = new System.Windows.Forms.TextBox();
		this.labTicketNo = new System.Windows.Forms.Label();
		this.btnBalanceCheck = new System.Windows.Forms.Button();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.panFill = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panFill.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(593, 60);
		this.labTitle.TabIndex = 4;
		this.labTitle.Text = "澳门通设备";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.Controls.Add(this.btnMpay);
		this.panel1.Controls.Add(this.btnMPass);
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.panel1.Location = new System.Drawing.Point(0, 622);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(593, 76);
		this.panel1.TabIndex = 5;
		this.btnMpay.ForeColor = System.Drawing.Color.Navy;
		this.btnMpay.Location = new System.Drawing.Point(223, 14);
		this.btnMpay.Name = "btnMpay";
		this.btnMpay.Size = new System.Drawing.Size(153, 48);
		this.btnMpay.TabIndex = 4;
		this.btnMpay.Text = "MPay";
		this.btnMpay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnMpay.UseVisualStyleBackColor = true;
		this.btnMpay.Click += new System.EventHandler(btnMpay_Click);
		this.btnMPass.ForeColor = System.Drawing.Color.Navy;
		this.btnMPass.Location = new System.Drawing.Point(33, 14);
		this.btnMPass.Name = "btnMPass";
		this.btnMPass.Size = new System.Drawing.Size(153, 48);
		this.btnMPass.TabIndex = 3;
		this.btnMPass.Text = "澳門通";
		this.btnMPass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnMPass.UseVisualStyleBackColor = true;
		this.btnMPass.Click += new System.EventHandler(btnRead_Click);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(420, 14);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(141, 48);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.txtTicketNo);
		this.panel2.Controls.Add(this.labTicketNo);
		this.panel2.Controls.Add(this.btnBalanceCheck);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.panel2.ForeColor = System.Drawing.Color.Navy;
		this.panel2.Location = new System.Drawing.Point(0, 60);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(593, 562);
		this.panel2.TabIndex = 6;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("微软雅黑", 10f);
		this.label1.ForeColor = System.Drawing.Color.Red;
		this.label1.Location = new System.Drawing.Point(252, 56);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(214, 23);
		this.label1.TabIndex = 0;
		this.label1.Text = "*  請輸入扣费金额(单位:元)";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtTicketNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTicketNo.Location = new System.Drawing.Point(248, 10);
		this.txtTicketNo.Name = "txtTicketNo";
		this.txtTicketNo.Size = new System.Drawing.Size(239, 51);
		this.txtTicketNo.TabIndex = 0;
		this.txtTicketNo.TabStop = false;
		this.txtTicketNo.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTicketNo_KeyDown);
		this.labTicketNo.Location = new System.Drawing.Point(14, 14);
		this.labTicketNo.Name = "labTicketNo";
		this.labTicketNo.Size = new System.Drawing.Size(228, 47);
		this.labTicketNo.TabIndex = 0;
		this.labTicketNo.Text = "扣费金额";
		this.labTicketNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBalanceCheck.ForeColor = System.Drawing.Color.Navy;
		this.btnBalanceCheck.Location = new System.Drawing.Point(400, 200);
		this.btnBalanceCheck.Name = "btnBalanceCheck";
		this.btnBalanceCheck.Size = new System.Drawing.Size(153, 48);
		this.btnBalanceCheck.TabIndex = 5;
		this.btnBalanceCheck.Text = "余额查询";
		this.btnBalanceCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBalanceCheck.UseVisualStyleBackColor = true;
		this.btnBalanceCheck.Click += new System.EventHandler(btnBalanceCheck_Click);
		this.contextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.panel2);
		this.panFill.Controls.Add(this.panel1);
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(595, 700);
		this.panFill.TabIndex = 5;
		base.AutoScaleDimensions = new System.Drawing.SizeF(15f, 32f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(595, 700);
		base.Controls.Add(this.panFill);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "FormMPassQuery";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormMPassQuery";
		base.Activated += new System.EventHandler(FormTimeChargeDemage_Activated);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormTimeChargeDemage_FormClosing);
		base.Load += new System.EventHandler(FormTimeChargeDemage_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panFill.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
	}

	private void txtTotalCharge_TextChanged(object sender, EventArgs e)
	{
	}

	private void FormTimeChargeDemage_Activated(object sender, EventArgs e)
	{
		txtTicketNo.Focus();
	}

	private void FormTimeChargeDemage_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void FormTimeChargeDemage_Load(object sender, EventArgs e)
	{
	}

	private void btnOther_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		_ = m_ChargeRecord.CardCode;
		using (new FormEpaySale
		{
			ChargeRecord = m_ChargeRecord
		})
		{
		}
	}

	private void txtTicketNo_TextChanged(object sender, EventArgs e)
	{
	}

	private void rbMPASS_CheckedChanged(object sender, EventArgs e)
	{
		txtTicketNo.Text = "";
	}

	private void DisableRB()
	{
	}

	private void txtTicketNo_KeyDown(object sender, KeyEventArgs e)
	{
		_ = e.KeyCode;
	}

	private void btnRead_Click(object sender, EventArgs e)
	{
		try
		{
			charge_value = decimal.Parse(txtTicketNo.Text);
		}
		catch (Exception)
		{
			charge_value = default(decimal);
		}
		if (charge_value > 0m)
		{
			BillType = EnumBillType.MacauPass;
			using MPassChargeDialog mPassChargeDialog = new MPassChargeDialog
			{
				BillType = BillType,
				SaleAmt = charge_value,
				CardType = "MPCARD"
			};
			if (mPassChargeDialog.ShowDialog() == DialogResult.OK)
			{
				SaleResult saleResult = mPassChargeDialog.SaleResult;
				Application.DoEvents();
				MakeCharge(charge_value, string.IsNullOrEmpty(saleResult.PAN) ? saleResult.PAY_ACCOUNT : saleResult.PAN, 10, out m_ChargeRecord);
				if (saleResult.PAY_MODE == "mpay")
				{
					m_ChargeRecord.subPayType = 2;
				}
				else
				{
					m_ChargeRecord.subPayType = 1;
				}
				MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = new MPass_POS_Transaction_Detail
				{
					ChargeTransactionID = m_ChargeRecord.TimeChargeID
				};
				EntityHelper.CopyEntity(saleResult, mPass_POS_Transaction_Detail);
				mPass_POS_Transaction_Detail.CashType = "MOP";
				if (LPDBHelper.AddChargerecord(m_ChargeRecord, mPass_POS_Transaction_Detail, (DataBuffer2018.CurrentStaff != null) ? DataBuffer2018.CurrentStaff.StaffCode : "Auto", Settings.Default.OnlyID))
				{
					Global.ShowMessage("扣費成功");
				}
			}
			return;
		}
		Global.ShowMessage("請輸入正確金額");
	}

	private void btnMpay_Click(object sender, EventArgs e)
	{
		try
		{
			charge_value = decimal.Parse(txtTicketNo.Text);
		}
		catch (Exception)
		{
			charge_value = default(decimal);
		}
		if (!(charge_value > 0m))
		{
			return;
		}
		BillType = EnumBillType.MacauPass;
		using MPassChargeDialog mPassChargeDialog = new MPassChargeDialog
		{
			BillType = BillType,
			SaleAmt = charge_value,
			CardType = "MPay"
		};
		if (mPassChargeDialog.ShowDialog() == DialogResult.OK)
		{
			SaleResult saleResult = mPassChargeDialog.SaleResult;
			Application.DoEvents();
			MakeCharge(charge_value, string.IsNullOrEmpty(saleResult.PAN) ? saleResult.PAY_ACCOUNT : saleResult.PAN, 10, out m_ChargeRecord);
			if (saleResult.PAY_MODE == "mpay")
			{
				m_ChargeRecord.subPayType = 2;
			}
			else
			{
				m_ChargeRecord.subPayType = 1;
			}
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = new MPass_POS_Transaction_Detail
			{
				ChargeTransactionID = m_ChargeRecord.TimeChargeID
			};
			EntityHelper.CopyEntity(saleResult, mPass_POS_Transaction_Detail);
			mPass_POS_Transaction_Detail.CashType = "MOP";
			if (LPDBHelper.AddChargerecord(m_ChargeRecord, mPass_POS_Transaction_Detail, (DataBuffer2018.CurrentStaff != null) ? DataBuffer2018.CurrentStaff.StaffCode : "Auto", Settings.Default.OnlyID))
			{
				Global.ShowMessage("扣費成功");
			}
		}
	}

	private void MakeCharge(decimal totalcharge, string cardcode, int billtype, out ChargeRecord record)
	{
		record = new ChargeRecord
		{
			BillType = billtype,
			TotalCharge = totalcharge,
			CardCode = cardcode,
			ChargeTime = DateTime.Now,
			StaffCode = "ex",
			FreeMin = 0,
			FreeCharge = 0m,
			ShiftID = 0,
			ChargeMin = 0,
			ParkMin = 0,
			ParkTypeID = 0,
			FromStation = Settings.Default.OnlyID,
			PayType = 2
		};
	}

	private void btnBalanceCheck_Click(object sender, EventArgs e)
	{
		new FormMPassQuerycheck().Show();
	}
}
