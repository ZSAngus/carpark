using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Device;
using CarPark.Lib;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.Forms;

public class FormCheckRental : Form
{
	private ILog Logger;

	private List<Card> list;

	private BindingSource bsCard;

	private IContainer components;

	private Label labTitle;

	private Panel panel1;

	private Panel panel2;

	private Button btnCancel;

	private Button btnCheck;

	private Panel panel3;

	private TextBox bindLP;

	private TextBox bindCardCode;

	private RadioButton labLP;

	private RadioButton labCardCode;

	private DataGridView dataMain;

	private Button btnPay;

	private Button btnWeb;

	public FormCheckRental()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		SetDGVStyle(dataMain);
		bsCard = new BindingSource();
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
		BindingHelper.BindDataGridView<Card>(bsCard, dataMain, new DataGridBindingAttr[6]
		{
			new DataGridBindingAttr(PropertyHelper<Card>.GetProperty((Card m) => m.CardCode), 160),
			new DataGridBindingAttr(PropertyHelper<Card>.GetProperty((Card m) => m.CardCodeExt), 180),
			new DataGridBindingAttr(PropertyHelper<Card>.GetProperty((Card m) => m.LicensePlate), 158),
			new DataGridBindingAttr(PropertyHelper<Card>.GetProperty((Card m) => m.StartDate), 150),
			new DataGridBindingAttr(PropertyHelper<Card>.GetProperty((Card m) => m.ExpireDate), 150),
			new DataGridBindingAttr(PropertyHelper<Card>.GetProperty((Card m) => m.Status), 100)
		});
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.SmartCardReadEvent += FeeCenterModule_SmartCardReadEvent;
		}
		base.Shown += FormCheckRental_Shown;
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labCardCode.Text = LangManager.GetLangString("CarPark.Forms.FormCheckRental.labCardCode");
		labLP.Text = LangManager.GetLangString("CarPark.Forms.FormCheckRental.labLP");
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormCheckRental.labTitle");
		btnCheck.Text = LangManager.GetLangString("CarPark.Forms.FormCheckRental.btnCheck");
		btnPay.Text = LangManager.GetLangString("CarPark.Forms.FormCheckRental.btnPay");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormCheckRental.btnCancel");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void labCardCode_CheckedChanged(object sender, EventArgs e)
	{
		if (labCardCode.Checked)
		{
			bindCardCode.Enabled = true;
			bindLP.Enabled = false;
			bindLP.Text = "";
		}
		else if (labLP.Checked)
		{
			bindCardCode.Enabled = false;
			bindLP.Enabled = true;
			bindCardCode.Text = "";
		}
	}

	private void btnCheck_Click(object sender, EventArgs e)
	{
		try
		{
			this.list = new List<Card>();
			bsCard.Clear();
			GetCardInfoArgs getCardInfoArgs = new GetCardInfoArgs();
			if (labCardCode.Checked)
			{
				getCardInfoArgs.CardNumber = bindCardCode.Text;
				getCardInfoArgs.LicensePlate = bindCardCode.Text;
			}
			else
			{
				getCardInfoArgs.LicensePlate = bindLP.Text;
			}
			ChargeContext chargeContext = new ChargeContext();
			chargeContext.CommunicationChannel.GetCardInfo(getCardInfoArgs, out this.list);
			chargeContext.CommunicationChannel.Disconnect();
			List<Card> list = this.list.Where((Card card) => card.Status == 1).ToList();
			if (list.Count > 0)
			{
				foreach (Card item in list)
				{
					bsCard.Add(item);
				}
				return;
			}
			Global.ShowMessage("未查詢到記錄");
		}
		catch (TimeoutException message)
		{
			Logger.Error(message);
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
		}
		catch (Exception message2)
		{
			Logger.Error(message2);
		}
	}

	private void SetDGVStyle(DataGridView dgv)
	{
		DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
		dgv.AllowUserToAddRows = false;
		dgv.AllowUserToDeleteRows = false;
		dgv.AllowUserToResizeColumns = false;
		dgv.AllowUserToResizeRows = false;
		dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = SystemColors.Window;
		dataGridViewCellStyle.Font = new Font("Times New Roman", 15f);
		dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
		dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = SystemColors.ControlText;
		dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;
		dgv.DefaultCellStyle = dataGridViewCellStyle;
		dgv.GridColor = Color.FromArgb(208, 215, 229);
		dgv.ReadOnly = true;
		dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
		dgv.RowTemplate.Height = 50;
		dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
	}

	private void FormCheckRental_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.SmartCardReadEvent -= FeeCenterModule_SmartCardReadEvent;
		}
	}

	private void FeeCenterModule_SmartCardReadEvent(string CardCode)
	{
		try
		{
			Invoke((MethodInvoker)delegate
			{
				bindCardCode.Text = CardCode;
			});
		}
		catch (Exception ex)
		{
			Global.ShowMessage(ex.Message);
		}
	}

	private void btnPay_Click(object sender, EventArgs e)
	{
		try
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			int rowIndex = dataMain.CurrentCell.RowIndex;
			if (rowIndex >= 0)
			{
				_ = ((Card)bsCard[rowIndex]).CardID;
				string cardCode = ((Card)bsCard[rowIndex]).CardCode;
				if (labCardCode.Checked)
				{
					empty = bindCardCode.Text;
				}
				else
				{
					empty2 = bindLP.Text;
				}
				if (string.IsNullOrWhiteSpace(empty) && string.IsNullOrWhiteSpace(empty2))
				{
					MessageBox.Show("請輸入卡號或車牌");
					return;
				}
				int? num = 0;
				foreach (StaffType staffType in DataBuffer2018.StaffTypes)
				{
					if (DataBuffer2018.CurrentStaff.StaffTypeId == staffType.StaffTypeID)
					{
						num = staffType.SystemCode;
						break;
					}
				}
				if (num.HasValue)
				{
					Close();
					FormRentalCharge formRentalCharge = new FormRentalCharge();
					formRentalCharge.CardCode = cardCode;
					formRentalCharge.ShowDialog();
				}
				else
				{
					MessageBox.Show("該員工沒有系統權限");
				}
			}
			else
			{
				MessageBox.Show("請選擇一條記錄");
			}
		}
		catch (TimeoutException ex)
		{
			Logger.Error(ex);
			Global.ShowMessage(LangManager.GetLangString(ex.Message));
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Global.ShowMessage("發生錯誤：" + ex2.Message);
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
		this.labTitle = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnCancel = new System.Windows.Forms.Button();
		this.btnCheck = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.dataMain = new System.Windows.Forms.DataGridView();
		this.labLP = new System.Windows.Forms.RadioButton();
		this.labCardCode = new System.Windows.Forms.RadioButton();
		this.bindLP = new System.Windows.Forms.TextBox();
		this.bindCardCode = new System.Windows.Forms.TextBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.btnPay = new System.Windows.Forms.Button();
		this.btnWeb = new System.Windows.Forms.Button();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataMain).BeginInit();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(998, 60);
		this.labTitle.TabIndex = 1;
		this.labTitle.Text = "月票查詢";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.Controls.Add(this.btnCheck);
		this.panel1.Controls.Add(this.btnPay);
		this.panel1.Controls.Add(this.btnWeb);
		this.panel1.Controls.Add(this.btnCancel);
		int num = 116;
		int num2 = 54;
		int num3 = 20;
		int num4 = 230;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 573);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(998, 75);
		this.panel1.TabIndex = 2;
		this.btnCheck.ForeColor = System.Drawing.Color.Navy;
		this.btnCheck.Location = new System.Drawing.Point(num4, 10);
		this.btnCheck.Size = new System.Drawing.Size(num, num2);
		this.btnCheck.TabIndex = 0;
		this.btnCheck.Text = "查詢";
		this.btnCheck.Click += new System.EventHandler(btnCheck_Click);
		this.btnPay.ForeColor = System.Drawing.Color.Navy;
		this.btnPay.Location = new System.Drawing.Point(num4 + num + num3, 10);
		this.btnPay.Size = new System.Drawing.Size(num, num2);
		this.btnPay.TabIndex = 1;
		this.btnPay.Text = "續租";
		this.btnPay.Enabled = true;
		this.btnPay.Click += new System.EventHandler(btnPay_Click);
		this.btnWeb.ForeColor = System.Drawing.Color.Navy;
		this.btnWeb.Location = new System.Drawing.Point(num4 + (num + num3) * 2, 10);
		this.btnWeb.Size = new System.Drawing.Size(num, num2);
		this.btnWeb.TabIndex = 2;
		this.btnWeb.Text = "WEB續";
		this.btnWeb.Click += new System.EventHandler(btnweb_Click);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(num4 + (num + num3) * 3, 10);
		this.btnCancel.Size = new System.Drawing.Size(num, num2);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "關閉";
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.dataMain);
		this.panel2.Controls.Add(this.labLP);
		this.panel2.Controls.Add(this.labCardCode);
		this.panel2.Controls.Add(this.bindLP);
		this.panel2.Controls.Add(this.bindCardCode);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 60);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(998, 513);
		this.panel2.TabIndex = 3;
		this.dataMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataMain.Location = new System.Drawing.Point(38, 84);
		this.dataMain.Name = "dataMain";
		this.dataMain.RowTemplate.Height = 24;
		this.dataMain.Size = new System.Drawing.Size(918, 397);
		this.dataMain.TabIndex = 3;
		this.labLP.ForeColor = System.Drawing.Color.Navy;
		this.labLP.Location = new System.Drawing.Point(500, 23);
		this.labLP.Name = "labLP";
		this.labLP.Size = new System.Drawing.Size(150, 39);
		this.labLP.TabIndex = 2;
		this.labLP.Text = "車牌";
		this.labLP.UseVisualStyleBackColor = true;
		this.labLP.CheckedChanged += new System.EventHandler(labCardCode_CheckedChanged);
		this.labCardCode.Checked = true;
		this.labCardCode.ForeColor = System.Drawing.Color.Navy;
		this.labCardCode.Location = new System.Drawing.Point(38, 23);
		this.labCardCode.Name = "labCardCode";
		this.labCardCode.Size = new System.Drawing.Size(150, 39);
		this.labCardCode.TabIndex = 2;
		this.labCardCode.TabStop = true;
		this.labCardCode.Text = "卡號";
		this.labCardCode.UseVisualStyleBackColor = true;
		this.labCardCode.CheckedChanged += new System.EventHandler(labCardCode_CheckedChanged);
		this.bindLP.Enabled = false;
		this.bindLP.Location = new System.Drawing.Point(656, 22);
		this.bindLP.Name = "bindLP";
		this.bindLP.Size = new System.Drawing.Size(300, 39);
		this.bindLP.TabIndex = 2;
		this.bindLP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.bindCardCode.Location = new System.Drawing.Point(194, 23);
		this.bindCardCode.Name = "bindCardCode";
		this.bindCardCode.Size = new System.Drawing.Size(300, 39);
		this.bindCardCode.TabIndex = 1;
		this.bindCardCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.panel2);
		this.panel3.Controls.Add(this.panel1);
		this.panel3.Controls.Add(this.labTitle);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(1000, 650);
		this.panel3.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(14f, 31f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(1000, 650);
		base.Controls.Add(this.panel3);
		this.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
		base.Name = "FormCheckRental";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormLostLP";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormCheckRental_FormClosing);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataMain).EndInit();
		this.panel3.ResumeLayout(false);
		base.ResumeLayout(false);
		base.KeyPreview = true;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormCheckRental_KeyDown);
	}

	private void FormCheckRental_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && (bindCardCode.Focused || bindLP.Focused))
		{
			btnCheck_Click(sender, e);
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void btnweb_Click(object sender, EventArgs e)
	{
		try
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			int rowIndex = dataMain.CurrentCell.RowIndex;
			if (rowIndex >= 0)
			{
				int cardID = ((Card)bsCard[rowIndex]).CardID;
				if (labCardCode.Checked)
				{
					empty = bindCardCode.Text;
				}
				else
				{
					empty2 = bindLP.Text;
				}
				if (string.IsNullOrWhiteSpace(empty) && string.IsNullOrWhiteSpace(empty2))
				{
					MessageBox.Show("請輸入卡號或車牌");
					return;
				}
				int? num = 0;
				foreach (StaffType staffType in DataBuffer2018.StaffTypes)
				{
					if (DataBuffer2018.CurrentStaff.StaffTypeId == staffType.StaffTypeID)
					{
						num = staffType.SystemCode;
						break;
					}
				}
				if (num.HasValue)
				{
					Process.Start(string.Concat(new object[12]
					{
						Settings.Default.ReportPath,
						"park/payr.html?StaffCode=",
						DataBuffer2018.CurrentStaff.StaffCode,
						"&StaffPwd=",
						DataBuffer2018.CurrentStaff.StaffPwd,
						"&StaffId=",
						DataBuffer2018.CurrentStaff.StaffId.ToString(),
						"&StaffName=",
						DataBuffer2018.CurrentStaff.StaffName,
						"&StaffTypeId=",
						DataBuffer2018.CurrentStaff.StaffTypeId.ToString(),
						"&CardID=" + cardID
					}));
					Close();
				}
				else
				{
					MessageBox.Show("該員工沒有系統權限");
				}
			}
			else
			{
				MessageBox.Show("請選擇一條記錄");
			}
		}
		catch (TimeoutException ex)
		{
			Logger.Error(ex);
			Global.ShowMessage(LangManager.GetLangString(ex.Message));
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void FormCheckRental_Shown(object sender, EventArgs e)
	{
		bindCardCode.Focus();
	}
}
