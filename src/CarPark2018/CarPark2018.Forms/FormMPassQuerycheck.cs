using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CarPark.Device;
using CarPark.Lib;
using MacauPass.POSCom.Package;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.Forms;

public class FormMPassQuerycheck : Form
{
	public ILog Logger;

	private BindingSource bs;

	private IContainer components;

	private Label labTitle;

	private Panel panMain;

	private TextBox txtCardCode;

	private Label labInTime;

	private TextBox textBoxX_0;

	private TextBox txtPur2;

	private Label labelX6;

	private Label labelX5;

	private TextBox textBoxX_1;

	private TextBox txtPur1;

	private Label labParkTime;

	private Label labelX4;

	private TextBox txtLogicNo;

	private Label labelX1;

	private Label labelX2;

	private DataGridView dataMain;

	private Label labelX3;

	private ComboBox comboParkType;

	private Button btnClose;

	private Button btnCheck;

	private Panel panel1;

	public FormMPassQuerycheck()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		bs = new BindingSource();
		SetDGVStyle(dataMain);
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
		BindingHelper.BindDataGridView<SaleBrifInfo>(bs, dataMain, new DataGridBindingAttr[4]
		{
			new DataGridBindingAttr(PropertyHelper<SaleBrifInfo>.GetProperty((SaleBrifInfo m) => m.SaleTime), 300),
			new DataGridBindingAttr(PropertyHelper<SaleBrifInfo>.GetProperty((SaleBrifInfo m) => m.TerminalID), 250),
			new DataGridBindingAttr(PropertyHelper<SaleBrifInfo>.GetProperty((SaleBrifInfo m) => m.TotalAMT), 250),
			new DataGridBindingAttr(PropertyHelper<SaleBrifInfo>.GetProperty((SaleBrifInfo m) => m.OragBalance), 200)
		});
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labelX1.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.labelX1");
		labelX2.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.labelX2");
		labelX3.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.labelX3");
		labelX4.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.labelX4");
		labelX5.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.labelX5");
		labelX6.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.labelX6");
		labInTime.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.labInTime");
		labParkTime.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.labParkTime");
		labTitle.Text = LangManager.GetLangString("CarPark.FormMain.btnMpassQuery");
		btnCheck.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.btnOK");
		btnClose.Text = LangManager.GetLangString("CarPark.Forms.MPassQuery.btnClose");
	}

	private void btnCheck_Click(object sender, EventArgs e)
	{
		try
		{
			if (comboParkType.SelectedIndex < 0)
			{
				comboParkType.SelectedIndex = 0;
			}
			bs.Clear();
			btnCheck.Enabled = false;
			btnClose.Enabled = false;
			int selectedIndex = comboParkType.SelectedIndex;
			txtCardCode.Text = string.Empty;
			txtLogicNo.Text = string.Empty;
			txtPur1.Text = string.Empty;
			textBoxX_1.Text = string.Empty;
			txtPur2.Text = string.Empty;
			textBoxX_0.Text = string.Empty;
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				try
				{
					QueryResult queryResult = ((IMPPOSTranscation)DeviceManager.FeeCenterModule).QueryCard(int.Parse(state.ToString()));
					if (queryResult.CommandResult == CommandResult.Fail)
					{
						Global.ShowMessage(LangManager.GetLangString("Alert.TransactionFailed") + Environment.NewLine + queryResult.ErrDescription);
					}
					else
					{
						Invoke((Action)delegate
						{
							txtCardCode.Text = queryResult.CardNo;
							txtLogicNo.Text = queryResult.CardLogicNo;
							txtPur1.Text = queryResult.Balance1Type;
							textBoxX_1.Text = queryResult.Balance1.ToString("f2");
							txtPur2.Text = queryResult.Balance2Type;
							textBoxX_0.Text = queryResult.Balance2.ToString("f2");
							if (queryResult.TransactionCount > 0)
							{
								foreach (SaleBrifInfo item in queryResult.TransactionInfo)
								{
									bs.Add(item);
								}
							}
						});
					}
				}
				catch (Exception ex2)
				{
					Global.ShowMessage(ex2.Message);
				}
				finally
				{
					Invoke((Action)delegate
					{
						btnCheck.Enabled = true;
						btnClose.Enabled = true;
					});
				}
			}, selectedIndex);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			MessageBox.Show(ex.Message);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
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

	private void dataMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
		this.labTitle = new System.Windows.Forms.Label();
		this.panMain = new System.Windows.Forms.Panel();
		this.btnClose = new System.Windows.Forms.Button();
		this.btnCheck = new System.Windows.Forms.Button();
		this.comboParkType = new System.Windows.Forms.ComboBox();
		this.dataMain = new System.Windows.Forms.DataGridView();
		this.textBoxX_0 = new System.Windows.Forms.TextBox();
		this.labelX3 = new System.Windows.Forms.Label();
		this.txtPur2 = new System.Windows.Forms.TextBox();
		this.labelX6 = new System.Windows.Forms.Label();
		this.labelX2 = new System.Windows.Forms.Label();
		this.labelX5 = new System.Windows.Forms.Label();
		this.textBoxX_1 = new System.Windows.Forms.TextBox();
		this.txtPur1 = new System.Windows.Forms.TextBox();
		this.labParkTime = new System.Windows.Forms.Label();
		this.labelX4 = new System.Windows.Forms.Label();
		this.txtLogicNo = new System.Windows.Forms.TextBox();
		this.txtCardCode = new System.Windows.Forms.TextBox();
		this.labelX1 = new System.Windows.Forms.Label();
		this.labInTime = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataMain).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 30f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(917, 60);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "餘額查詢";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panMain.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panMain.Controls.Add(this.btnClose);
		this.panMain.Controls.Add(this.btnCheck);
		this.panMain.Controls.Add(this.comboParkType);
		this.panMain.Controls.Add(this.dataMain);
		this.panMain.Controls.Add(this.textBoxX_0);
		this.panMain.Controls.Add(this.labelX3);
		this.panMain.Controls.Add(this.txtPur2);
		this.panMain.Controls.Add(this.labelX6);
		this.panMain.Controls.Add(this.labelX2);
		this.panMain.Controls.Add(this.labelX5);
		this.panMain.Controls.Add(this.textBoxX_1);
		this.panMain.Controls.Add(this.txtPur1);
		this.panMain.Controls.Add(this.labParkTime);
		this.panMain.Controls.Add(this.labelX4);
		this.panMain.Controls.Add(this.txtLogicNo);
		this.panMain.Controls.Add(this.txtCardCode);
		this.panMain.Controls.Add(this.labelX1);
		this.panMain.Controls.Add(this.labInTime);
		this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panMain.Location = new System.Drawing.Point(0, 60);
		this.panMain.Name = "panMain";
		this.panMain.Size = new System.Drawing.Size(917, 628);
		this.panMain.TabIndex = 1;
		this.btnClose.ForeColor = System.Drawing.Color.Navy;
		this.btnClose.Location = new System.Drawing.Point(595, 498);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(121, 53);
		this.btnClose.TabIndex = 4;
		this.btnClose.Text = "關閉";
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnCheck.ForeColor = System.Drawing.Color.Navy;
		this.btnCheck.Location = new System.Drawing.Point(468, 498);
		this.btnCheck.Name = "btnCheck";
		this.btnCheck.Size = new System.Drawing.Size(121, 53);
		this.btnCheck.TabIndex = 4;
		this.btnCheck.Text = "查詢";
		this.btnCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCheck.UseVisualStyleBackColor = true;
		this.btnCheck.Click += new System.EventHandler(btnCheck_Click);
		this.comboParkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkType.FormattingEnabled = true;
		this.comboParkType.Items.AddRange(new object[2] { "全部", "最後一條" });
		this.comboParkType.Location = new System.Drawing.Point(251, 506);
		this.comboParkType.Name = "comboParkType";
		this.comboParkType.Size = new System.Drawing.Size(205, 39);
		this.comboParkType.TabIndex = 3;
		this.dataMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataMain.Location = new System.Drawing.Point(251, 310);
		this.dataMain.Name = "dataMain";
		this.dataMain.RowTemplate.Height = 24;
		this.dataMain.Size = new System.Drawing.Size(652, 150);
		this.dataMain.TabIndex = 2;
		this.dataMain.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataMain_DataError);
		this.textBoxX_0.Location = new System.Drawing.Point(698, 225);
		this.textBoxX_0.Name = "textBoxX_0";
		this.textBoxX_0.ReadOnly = true;
		this.textBoxX_0.Size = new System.Drawing.Size(205, 39);
		this.textBoxX_0.TabIndex = 1;
		this.labelX3.ForeColor = System.Drawing.Color.Navy;
		this.labelX3.Location = new System.Drawing.Point(15, 506);
		this.labelX3.Name = "labelX3";
		this.labelX3.Size = new System.Drawing.Size(230, 39);
		this.labelX3.TabIndex = 0;
		this.labelX3.Text = "記錄數";
		this.labelX3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtPur2.Location = new System.Drawing.Point(251, 225);
		this.txtPur2.Name = "txtPur2";
		this.txtPur2.ReadOnly = true;
		this.txtPur2.Size = new System.Drawing.Size(205, 39);
		this.txtPur2.TabIndex = 1;
		this.labelX6.ForeColor = System.Drawing.Color.Navy;
		this.labelX6.Location = new System.Drawing.Point(462, 225);
		this.labelX6.Name = "labelX6";
		this.labelX6.Size = new System.Drawing.Size(230, 39);
		this.labelX6.TabIndex = 0;
		this.labelX6.Text = "錢包2餘額";
		this.labelX6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelX2.ForeColor = System.Drawing.Color.Navy;
		this.labelX2.Location = new System.Drawing.Point(15, 310);
		this.labelX2.Name = "labelX2";
		this.labelX2.Size = new System.Drawing.Size(230, 39);
		this.labelX2.TabIndex = 0;
		this.labelX2.Text = "交易記錄";
		this.labelX2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelX5.ForeColor = System.Drawing.Color.Navy;
		this.labelX5.Location = new System.Drawing.Point(15, 225);
		this.labelX5.Name = "labelX5";
		this.labelX5.Size = new System.Drawing.Size(230, 39);
		this.labelX5.TabIndex = 0;
		this.labelX5.Text = "錢包2幣種";
		this.labelX5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.textBoxX_1.Location = new System.Drawing.Point(698, 140);
		this.textBoxX_1.Name = "textBoxX_1";
		this.textBoxX_1.ReadOnly = true;
		this.textBoxX_1.Size = new System.Drawing.Size(205, 39);
		this.textBoxX_1.TabIndex = 1;
		this.txtPur1.Location = new System.Drawing.Point(251, 140);
		this.txtPur1.Name = "txtPur1";
		this.txtPur1.ReadOnly = true;
		this.txtPur1.Size = new System.Drawing.Size(205, 39);
		this.txtPur1.TabIndex = 1;
		this.labParkTime.ForeColor = System.Drawing.Color.Navy;
		this.labParkTime.Location = new System.Drawing.Point(462, 140);
		this.labParkTime.Name = "labParkTime";
		this.labParkTime.Size = new System.Drawing.Size(230, 39);
		this.labParkTime.TabIndex = 0;
		this.labParkTime.Text = "錢包1餘額";
		this.labParkTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelX4.ForeColor = System.Drawing.Color.Navy;
		this.labelX4.Location = new System.Drawing.Point(15, 140);
		this.labelX4.Name = "labelX4";
		this.labelX4.Size = new System.Drawing.Size(230, 39);
		this.labelX4.TabIndex = 0;
		this.labelX4.Text = "錢包1幣種";
		this.labelX4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtLogicNo.Location = new System.Drawing.Point(698, 55);
		this.txtLogicNo.Name = "txtLogicNo";
		this.txtLogicNo.ReadOnly = true;
		this.txtLogicNo.Size = new System.Drawing.Size(205, 39);
		this.txtLogicNo.TabIndex = 1;
		this.txtCardCode.Location = new System.Drawing.Point(251, 55);
		this.txtCardCode.Name = "txtCardCode";
		this.txtCardCode.ReadOnly = true;
		this.txtCardCode.Size = new System.Drawing.Size(205, 39);
		this.txtCardCode.TabIndex = 1;
		this.labelX1.ForeColor = System.Drawing.Color.Navy;
		this.labelX1.Location = new System.Drawing.Point(462, 55);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(230, 39);
		this.labelX1.TabIndex = 0;
		this.labelX1.Text = "邏輯卡號";
		this.labelX1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labInTime.ForeColor = System.Drawing.Color.Navy;
		this.labInTime.Location = new System.Drawing.Point(15, 55);
		this.labInTime.Name = "labInTime";
		this.labInTime.Size = new System.Drawing.Size(230, 39);
		this.labInTime.TabIndex = 0;
		this.labInTime.Text = "卡面號";
		this.labInTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.panMain);
		this.panel1.Controls.Add(this.labTitle);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(919, 690);
		this.panel1.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(14f, 31f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(919, 690);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
		base.Name = "FormMPassQuerycheck";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormMPassQuerycheck";
		this.panMain.ResumeLayout(false);
		this.panMain.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataMain).EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
