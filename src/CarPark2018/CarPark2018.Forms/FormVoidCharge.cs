using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using CarPark2018.Properties;
using CarPark2018.UserControls;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.Forms;

public class FormVoidCharge : Form
{
	private ILog Logger;

	private GetCurrChargeRecordArgs m_GetCurrChargeRecordArgs = new GetCurrChargeRecordArgs();

	private List<ChargeRecord> m_ChargeRecordList = new List<ChargeRecord>();

	private ChargeRecord m_ChargeRecord = new ChargeRecord();

	private BindingSource bs;

	private DateTime initTime;

	private IContainer components = null;

	private Label labTitle;

	private Panel panBottom;

	private Button btnCancel;

	private Button btnOK;

	private Panel panFill;

	private NumericUpDown bindVoidCharge;

	private Label labRemark;

	private Label labAmt;

	private RichTextBox bindRemark;

	private UCVideoPlayerEX ucVideoPlayerEX1;

	private TextBox bindCardCode;

	private Label labCardCode;

	private Panel panel1;

	public FormVoidCharge()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		bs = new BindingSource();
		initTime = DateTime.Now;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labAmt.Text = LangManager.GetLangString("CarPark2018.Forms.FormVoidCharge.labAmt");
		labCardCode.Text = LangManager.GetLangString("CarPark2018.Forms.FormVoidCharge.labCardCode");
		labRemark.Text = LangManager.GetLangString("CarPark2018.Forms.FormVoidCharge.labRemark");
		labTitle.Text = LangManager.GetLangString("CarPark2018.Forms.FormVoidCharge.labTitle");
		btnCancel.Text = LangManager.GetLangString("CarPark2018.Forms.FormVoidCharge.btnCancel");
		btnOK.Text = LangManager.GetLangString("CarPark2018.Forms.FormVoidCharge.btnOK");
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
		dataGridViewCellStyle.Font = new Font("Times New Roman", 22.5f);
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

	private void FormChargeFree_Load(object sender, EventArgs e)
	{
		try
		{
			Console.WriteLine(DateTime.Now.ToString());
			m_GetCurrChargeRecordArgs.PayStationName = Settings.Default.OnlyID;
			ChargeContext chargeContext = new ChargeContext();
			GetCurrChargeRecordReturn currChargeRecord = chargeContext.CommunicationChannel.GetCurrChargeRecord(m_GetCurrChargeRecordArgs, out m_ChargeRecordList);
			chargeContext.CommunicationChannel.Disconnect();
			if (currChargeRecord == null)
			{
				Logger.Error("GetCurrChargeRecordReturn is null");
				Console.WriteLine("GetCurrChargeRecordReturn is null");
				Global.ShowMessage("GetCurrChargeRecordReturn is null");
				return;
			}
			Console.WriteLine(DateTime.Now.ToString());
			if (currChargeRecord.ISOK)
			{
				try
				{
					m_ChargeRecord = m_ChargeRecordList.OrderByDescending((ChargeRecord chargeRecords) => chargeRecords.TimeChargeID).FirstOrDefault();
					bindCardCode.Text = m_ChargeRecord.CardCode;
				}
				catch (Exception ex)
				{
					Logger.Error(ex);
					Console.WriteLine(ex.Message);
				}
			}
			else
			{
				Logger.Error(currChargeRecord.ErrCode);
				Console.WriteLine(currChargeRecord.ErrCode);
				Global.ShowMessage(currChargeRecord.ErrCode);
			}
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Console.WriteLine(ex2.Message);
			Global.ShowMessage(ex2.Message);
		}
		try
		{
			if (ucVideoPlayerEX1.LoadVideoDevices() && ucVideoPlayerEX1.LoadVideoFBL())
			{
				if (!ucVideoPlayerEX1.OpenVideo())
				{
					Logger.Error("打開視頻流失敗");
					Console.WriteLine("打開視頻流失敗");
				}
				else
				{
					ucVideoPlayerEX1.Refresh();
				}
			}
			else
			{
				Logger.Error("加載失敗");
			}
		}
		catch (Exception ex3)
		{
			Logger.Error(ex3);
			Console.WriteLine(ex3.Message);
		}
	}

	private void InitBinding()
	{
		try
		{
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
	}

	private void FormChargeFree_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			ucVideoPlayerEX1.CloseVideo();
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			DataBuffer2018.CheckRole(MethodBase.GetCurrentMethod());
			if (m_ChargeRecord == null)
			{
				Global.ShowMessage("沒有收費記錄");
				return;
			}
			string imagePath = "";
			try
			{
				imagePath = ucVideoPlayerEX1.TakePhoto();
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				Console.WriteLine(ex.Message);
			}
			try
			{
				VoidCharge voidCharge = new VoidCharge();
				voidCharge.ChargeRecordID = m_ChargeRecord.TimeChargeID;
				voidCharge.CreateTime = initTime;
				voidCharge.ImagePath = imagePath;
				voidCharge.Remark = bindRemark.Text;
				voidCharge.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
				voidCharge.Status = 0;
				voidCharge.VoidChargeAmt = bindVoidCharge.Value;
				ChargeContext chargeContext = new ChargeContext();
				if (chargeContext.CommunicationChannel.SaveVoidCharge(voidCharge))
				{
					Global.ShowMessage("保存成功");
					Close();
				}
				else
				{
					Global.ShowMessage("保存失敗");
				}
				chargeContext.CommunicationChannel.Disconnect();
			}
			catch (TimeoutException)
			{
				Global.ShowMessage("操作超時，請重新操作");
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3);
				Console.WriteLine(ex3.Message);
				Global.ShowMessage(ex3.Message);
			}
		}
		catch (Exception ex4)
		{
			Global.ShowMessage(LangManager.GetLangString(ex4.Message));
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
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
		this.panBottom = new System.Windows.Forms.Panel();
		this.btnCancel = new System.Windows.Forms.Button();
		this.btnOK = new System.Windows.Forms.Button();
		this.panFill = new System.Windows.Forms.Panel();
		this.bindCardCode = new System.Windows.Forms.TextBox();
		this.ucVideoPlayerEX1 = new CarPark2018.UserControls.UCVideoPlayerEX();
		this.bindRemark = new System.Windows.Forms.RichTextBox();
		this.bindVoidCharge = new System.Windows.Forms.NumericUpDown();
		this.labRemark = new System.Windows.Forms.Label();
		this.labCardCode = new System.Windows.Forms.Label();
		this.labAmt = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panBottom.SuspendLayout();
		this.panFill.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.bindVoidCharge).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微軟正黑體", 25f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(500, 54);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "優惠審核";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panBottom.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.panBottom.Controls.Add(this.btnCancel);
		this.panBottom.Controls.Add(this.btnOK);
		this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panBottom.Location = new System.Drawing.Point(0, 483);
		this.panBottom.Name = "panBottom";
		this.panBottom.Size = new System.Drawing.Size(500, 65);
		this.panBottom.TabIndex = 1;
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(262, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(99, 44);
		this.btnCancel.TabIndex = 0;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.btnOK.ForeColor = System.Drawing.Color.Navy;
		this.btnOK.Location = new System.Drawing.Point(141, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(99, 44);
		this.btnOK.TabIndex = 0;
		this.btnOK.Text = "確認";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.panFill.Controls.Add(this.bindCardCode);
		this.panFill.Controls.Add(this.ucVideoPlayerEX1);
		this.panFill.Controls.Add(this.bindRemark);
		this.panFill.Controls.Add(this.bindVoidCharge);
		this.panFill.Controls.Add(this.labRemark);
		this.panFill.Controls.Add(this.labCardCode);
		this.panFill.Controls.Add(this.labAmt);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Location = new System.Drawing.Point(0, 54);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(500, 429);
		this.panFill.TabIndex = 2;
		this.bindCardCode.Location = new System.Drawing.Point(148, 208);
		this.bindCardCode.Name = "bindCardCode";
		this.bindCardCode.ReadOnly = true;
		this.bindCardCode.Size = new System.Drawing.Size(277, 34);
		this.bindCardCode.TabIndex = 6;
		this.ucVideoPlayerEX1.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucVideoPlayerEX1.Location = new System.Drawing.Point(148, 31);
		this.ucVideoPlayerEX1.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
		this.ucVideoPlayerEX1.Name = "ucVideoPlayerEX1";
		this.ucVideoPlayerEX1.Size = new System.Drawing.Size(277, 165);
		this.ucVideoPlayerEX1.TabIndex = 5;
		this.bindRemark.Location = new System.Drawing.Point(148, 288);
		this.bindRemark.Name = "bindRemark";
		this.bindRemark.Size = new System.Drawing.Size(277, 112);
		this.bindRemark.TabIndex = 4;
		this.bindRemark.Text = "";
		this.bindVoidCharge.Location = new System.Drawing.Point(148, 248);
		this.bindVoidCharge.Name = "bindVoidCharge";
		this.bindVoidCharge.Size = new System.Drawing.Size(277, 34);
		this.bindVoidCharge.TabIndex = 3;
		this.labRemark.ForeColor = System.Drawing.Color.Navy;
		this.labRemark.Location = new System.Drawing.Point(12, 288);
		this.labRemark.Name = "labRemark";
		this.labRemark.Size = new System.Drawing.Size(130, 34);
		this.labRemark.TabIndex = 1;
		this.labRemark.Text = "備註";
		this.labRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labCardCode.ForeColor = System.Drawing.Color.Navy;
		this.labCardCode.Location = new System.Drawing.Point(12, 208);
		this.labCardCode.Name = "labCardCode";
		this.labCardCode.Size = new System.Drawing.Size(130, 34);
		this.labCardCode.TabIndex = 1;
		this.labCardCode.Text = "卡號";
		this.labCardCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labAmt.ForeColor = System.Drawing.Color.Navy;
		this.labAmt.Location = new System.Drawing.Point(12, 248);
		this.labAmt.Name = "labAmt";
		this.labAmt.Size = new System.Drawing.Size(130, 34);
		this.labAmt.TabIndex = 1;
		this.labAmt.Text = "金額";
		this.labAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.panFill);
		this.panel1.Controls.Add(this.panBottom);
		this.panel1.Controls.Add(this.labTitle);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(502, 550);
		this.panel1.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 25f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(502, 550);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("微軟正黑體", 15f);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
		base.Name = "FormVoidCharge";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormChargeFree";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormChargeFree_FormClosing);
		base.Load += new System.EventHandler(FormChargeFree_Load);
		this.panBottom.ResumeLayout(false);
		this.panFill.ResumeLayout(false);
		this.panFill.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.bindVoidCharge).EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
