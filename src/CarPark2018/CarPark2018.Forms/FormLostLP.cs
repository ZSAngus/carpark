using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.Forms;

public class FormLostLP : Form
{
	private ILog Logger;

	public List<view_transactionandlp> list;

	public view_transactionandlp view;

	private BindingSource bsView;

	private IContainer components = null;

	private Label labTitle;

	private Panel panel1;

	private Panel panel2;

	private Button btnOK;

	private Button btnCancel;

	private Button btnCheck;

	private TextBox bindLPText;

	private DateTimePicker bindEndTime;

	private DateTimePicker bindStartTime;

	private RadioButton rbTime;

	private RadioButton rbLP;

	private PictureBox picLP;

	private Panel panel3;

	private DataGridView dataMain;

	public FormLostLP()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		SetDGVStyle(dataMain);
		view = new view_transactionandlp();
		bsView = new BindingSource();
		bsView.CurrentChanged += bsView_CurrentChanged;
		BindingHelper.BindDataGridView<view_transactionandlp>(bsView, dataMain, new DataGridBindingAttr[1]
		{
			new DataGridBindingAttr(PropertyHelper<view_transactionandlp>.GetProperty((view_transactionandlp m) => m.InCardCode), 272)
		});
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormLostLP.labTitle");
		rbLP.Text = LangManager.GetLangString("CarPark.Forms.FormLostLP.rbLP");
		rbTime.Text = LangManager.GetLangString("CarPark.Forms.FormLostLP.rbTime");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormLostLP.btnCancel");
		btnCheck.Text = LangManager.GetLangString("CarPark.Forms.FormLostLP.btnCheck");
		btnOK.Text = LangManager.GetLangString("CarPark.Forms.FormLostLP.btnOK");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void rbLP_CheckedChanged(object sender, EventArgs e)
	{
		if (rbLP.Checked)
		{
			bindLPText.Enabled = true;
			bindStartTime.Enabled = false;
			bindEndTime.Enabled = false;
		}
		else
		{
			bindLPText.Enabled = false;
			bindStartTime.Enabled = true;
			bindEndTime.Enabled = true;
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

	private void btnCheck_Click(object sender, EventArgs e)
	{
		try
		{
			bsView.Clear();
			GetView_TransactionAndLPArgs getView_TransactionAndLPArgs = new GetView_TransactionAndLPArgs();
			if (rbLP.Checked)
			{
				getView_TransactionAndLPArgs.LicensePlate = bindLPText.Text;
			}
			else
			{
				getView_TransactionAndLPArgs.InStartTime = bindStartTime.Value;
				getView_TransactionAndLPArgs.InEndTime = bindEndTime.Value;
			}
			list = new List<view_transactionandlp>();
			GetView_TransactionAndLPReturn view_TransactionAndLP = Common._Carpark2018ServiceContext.CommunicationChannel.GetView_TransactionAndLP(getView_TransactionAndLPArgs, out list);
			if (list.Count > 0)
			{
				foreach (view_transactionandlp item in list)
				{
					bsView.Add(item);
				}
				return;
			}
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.Show4"));
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

	private void bsView_CurrentChanged(object sender, EventArgs e)
	{
		try
		{
			view = bsView.Current as view_transactionandlp;
			if (string.IsNullOrEmpty(view.ImagePath))
			{
				picLP.Image = ImageManager.GetImage("", "cancel");
				return;
			}
			try
			{
				picLP.Image = Image.FromFile(Config.LicensePlatePath + view.ImagePath);
			}
			catch (Exception)
			{
				picLP.Image = ImageManager.GetImage("", "cancel");
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void dataMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			if (view == null)
			{
				base.DialogResult = DialogResult.Cancel;
			}
			DateTime inTime = view.InTime;
			base.DialogResult = DialogResult.OK;
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.dataMain = new System.Windows.Forms.DataGridView();
		this.picLP = new System.Windows.Forms.PictureBox();
		this.rbTime = new System.Windows.Forms.RadioButton();
		this.rbLP = new System.Windows.Forms.RadioButton();
		this.bindEndTime = new System.Windows.Forms.DateTimePicker();
		this.bindStartTime = new System.Windows.Forms.DateTimePicker();
		this.bindLPText = new System.Windows.Forms.TextBox();
		this.btnCheck = new System.Windows.Forms.Button();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataMain).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLP).BeginInit();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(593, 60);
		this.labTitle.TabIndex = 1;
		this.labTitle.Text = "失票查詢";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.Controls.Add(this.btnOK);
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 623);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(593, 75);
		this.panel1.TabIndex = 2;
		this.btnOK.ForeColor = System.Drawing.Color.Navy;
		this.btnOK.Location = new System.Drawing.Point(303, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(116, 54);
		this.btnOK.TabIndex = 0;
		this.btnOK.Text = "確認";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(465, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(116, 54);
		this.btnCancel.TabIndex = 0;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.dataMain);
		this.panel2.Controls.Add(this.picLP);
		this.panel2.Controls.Add(this.rbTime);
		this.panel2.Controls.Add(this.rbLP);
		this.panel2.Controls.Add(this.bindEndTime);
		this.panel2.Controls.Add(this.bindStartTime);
		this.panel2.Controls.Add(this.bindLPText);
		this.panel2.Controls.Add(this.btnCheck);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 60);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(593, 563);
		this.panel2.TabIndex = 3;
		this.dataMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataMain.Location = new System.Drawing.Point(9, 299);
		this.dataMain.Name = "dataMain";
		this.dataMain.RowTemplate.Height = 24;
		this.dataMain.Size = new System.Drawing.Size(275, 252);
		this.dataMain.TabIndex = 7;
		this.dataMain.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataMain_DataError);
		this.picLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.picLP.Location = new System.Drawing.Point(306, 299);
		this.picLP.Name = "picLP";
		this.picLP.Size = new System.Drawing.Size(275, 252);
		this.picLP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picLP.TabIndex = 6;
		this.picLP.TabStop = false;
		this.rbTime.ForeColor = System.Drawing.Color.Navy;
		this.rbTime.Location = new System.Drawing.Point(57, 90);
		this.rbTime.Name = "rbTime";
		this.rbTime.Size = new System.Drawing.Size(133, 39);
		this.rbTime.TabIndex = 4;
		this.rbTime.Text = "進場時間";
		this.rbTime.UseVisualStyleBackColor = true;
		this.rbLP.Checked = true;
		this.rbLP.ForeColor = System.Drawing.Color.Navy;
		this.rbLP.Location = new System.Drawing.Point(57, 21);
		this.rbLP.Name = "rbLP";
		this.rbLP.Size = new System.Drawing.Size(133, 39);
		this.rbLP.TabIndex = 4;
		this.rbLP.TabStop = true;
		this.rbLP.Text = "車牌";
		this.rbLP.UseVisualStyleBackColor = true;
		this.rbLP.CheckedChanged += new System.EventHandler(rbLP_CheckedChanged);
		this.bindEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
		this.bindEndTime.Enabled = false;
		this.bindEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindEndTime.Location = new System.Drawing.Point(196, 159);
		this.bindEndTime.Name = "bindEndTime";
		this.bindEndTime.Size = new System.Drawing.Size(287, 39);
		this.bindEndTime.TabIndex = 3;
		this.bindStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
		this.bindStartTime.Enabled = false;
		this.bindStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.bindStartTime.Location = new System.Drawing.Point(196, 90);
		this.bindStartTime.Name = "bindStartTime";
		this.bindStartTime.Size = new System.Drawing.Size(287, 39);
		this.bindStartTime.TabIndex = 3;
		this.bindLPText.Location = new System.Drawing.Point(196, 21);
		this.bindLPText.Name = "bindLPText";
		this.bindLPText.Size = new System.Drawing.Size(287, 39);
		this.bindLPText.TabIndex = 2;
		this.btnCheck.ForeColor = System.Drawing.Color.Navy;
		this.btnCheck.Location = new System.Drawing.Point(248, 228);
		this.btnCheck.Name = "btnCheck";
		this.btnCheck.Size = new System.Drawing.Size(96, 41);
		this.btnCheck.TabIndex = 0;
		this.btnCheck.Text = "查詢";
		this.btnCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCheck.UseVisualStyleBackColor = true;
		this.btnCheck.Click += new System.EventHandler(btnCheck_Click);
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.panel2);
		this.panel3.Controls.Add(this.panel1);
		this.panel3.Controls.Add(this.labTitle);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(595, 700);
		this.panel3.TabIndex = 2;
		base.AutoScaleDimensions = new System.Drawing.SizeF(14f, 31f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(595, 700);
		base.Controls.Add(this.panel3);
		this.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
		base.Name = "FormLostLP";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormLostLP";
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataMain).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLP).EndInit();
		this.panel3.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
