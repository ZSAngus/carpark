using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using CarPark2018.UserControls;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.Forms;

public class FormTimeChargeFree : Form
{
	private List<CustomFreeType> m_CustomFreeTypeList;

	private List<CustomFreeTenat> m_CustomFreeTenatList;

	private ILog Logger;

	private BindingSource bsFreeRolw;

	private BindingSource bsTenat;

	private ChargeContext chargeContext = new ChargeContext();

	private IContainer components = null;

	private UCVideoPlayerEX ucVideoPlayerEX1;

	protected Label labTitle;

	private TextBox txtRemark;

	private Label labInfo;

	private Label labRemark;

	private RadioButton labSearchBarCode;

	private RadioButton labSearch;

	private TextBox searchFreeTenat;

	private TextBox txtComp;

	protected Button btnClose;

	protected Button btnOK;

	private DataGridView dataFreeRule;

	private DataGridView dataFreeTenat;

	private Label labfreeRule;

	private Label labFreeTenat;

	private Panel panFill;

	public CustomFreeType m_CustomFreeType { get; set; }

	public CustomFreeTenat m_CustomFreeTenat { get; set; }

	public string Remark { get; set; }

	public string FreeImagePath { get; set; }

	public FormTimeChargeFree()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		bsTenat = new BindingSource();
		bsFreeRolw = new BindingSource();
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labfreeRule.Text = LangManager.GetLangString("CarPark2018.Forms.FormTimeChargeFree.labfreeRule");
		labFreeTenat.Text = LangManager.GetLangString("CarPark2018.Forms.FormTimeChargeFree.labFreeTenat");
		labRemark.Text = LangManager.GetLangString("UcSmartCard.panRight.labRemark");
		labSearch.Text = LangManager.GetLangString("CarPark2018.Forms.FormTimeChargeFree.labSearch");
		labSearchBarCode.Text = LangManager.GetLangString("CarPark2018.Forms.FormTimeChargeFree.labSearchBarCode");
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeFree.labTitle");
		btnClose.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeFree.btnClose");
		btnOK.Text = LangManager.GetLangString("CarPark.Forms.FormTimeChargeFree.btnOK");
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		try
		{
			GetFreeTypeAndFreeTenatArgs getFreeTypeAndFreeTenatArgs = new GetFreeTypeAndFreeTenatArgs();
			m_CustomFreeTenatList = new List<CustomFreeTenat>();
			m_CustomFreeTypeList = new List<CustomFreeType>();
			GetFreeTypeAndFreeTenatReturn freeTypeAndFreeTenat = chargeContext.CommunicationChannel.GetFreeTypeAndFreeTenat(getFreeTypeAndFreeTenatArgs, out m_CustomFreeTypeList, out m_CustomFreeTenatList);
			chargeContext.CommunicationChannel.Disconnect();
			if (freeTypeAndFreeTenat.ISOK)
			{
				LoadTenats(m_CustomFreeTenatList);
			}
			else
			{
				Global.ShowMessage(freeTypeAndFreeTenat.ErrCode);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Console.WriteLine(ex2.Message);
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
		try
		{
			SetDGVStyle(dataFreeTenat);
			SetDGVStyle(dataFreeRule);
			BindDataView();
			bsTenat_CurrentChanged(null, null);
			bsTenat.CurrentChanged += bsTenat_CurrentChanged;
			RadioButton_CheckedCharge(null, null);
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception ex5)
		{
			Logger.Error(ex5);
			Console.WriteLine(ex5.Message);
		}
	}

	private void BindDataView()
	{
		BindingHelper.BindDataGridView<CustomFreeTenat>(bsTenat, dataFreeTenat, new DataGridBindingAttr[2]
		{
			new DataGridBindingAttr(PropertyHelper<CustomFreeTenat>.GetProperty((CustomFreeTenat m) => m.TenatNo), 250),
			new DataGridBindingAttr(PropertyHelper<CustomFreeTenat>.GetProperty((CustomFreeTenat m) => m.TenatName), 300)
		});
		BindingHelper.BindDataGridView<CustomFreeType>(bsFreeRolw, dataFreeRule, new DataGridBindingAttr[1]
		{
			new DataGridBindingAttr(PropertyHelper<CustomFreeType>.GetProperty((CustomFreeType m) => m.CustomFreeName), 450)
		});
	}

	private void LoadFreeTypes()
	{
		bsFreeRolw.Clear();
		CustomFreeTenat customFreeTenat = bsTenat.Current as CustomFreeTenat;
		foreach (CustomFreeType customFreeType in m_CustomFreeTypeList)
		{
			if (customFreeTenat != null && customFreeTenat.AllowFreeType(customFreeType))
			{
				bsFreeRolw.Add(customFreeType);
			}
		}
	}

	private void LoadTenats(List<CustomFreeTenat> tenats)
	{
		bsTenat.Clear();
		tenats = tenats.OrderBy((CustomFreeTenat ten) => ten.TenatNo).ToList();
		foreach (CustomFreeTenat tenat in tenats)
		{
			bsTenat.Add(tenat);
		}
		bsTenat.MoveFirst();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		ucVideoPlayerEX1.CloseVideo();
		Close();
	}

	private void searchFreeTenat_TextChanged(object sender, EventArgs e)
	{
		if (m_CustomFreeTenatList != null)
		{
			if (!string.IsNullOrEmpty(searchFreeTenat.Text))
			{
				LoadTenats(m_CustomFreeTenatList.Where((CustomFreeTenat m) => !m.IsDelete && (m.TenatNo.Contains(searchFreeTenat.Text) || m.TenatNameCn.Contains(searchFreeTenat.Text) || m.TenatNamePt.Contains(searchFreeTenat.Text))).ToList());
			}
			else
			{
				LoadTenats(m_CustomFreeTenatList.Select((CustomFreeTenat m) => m).ToList());
			}
		}
		else
		{
			Logger.Debug("m_CustomFreeTenats is null");
		}
	}

	private void dataFreeTenat_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	private void dataFreeRule_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	private void RadioButton_CheckedCharge(object sender, EventArgs e)
	{
		if (labSearchBarCode.Checked)
		{
			txtComp.Enabled = true;
			txtComp.Focus();
			searchFreeTenat.Enabled = false;
			dataFreeRule.Enabled = false;
			dataFreeTenat.Enabled = false;
		}
		else
		{
			txtComp.Enabled = false;
			searchFreeTenat.Enabled = true;
			searchFreeTenat.Focus();
			dataFreeRule.Enabled = true;
			dataFreeTenat.Enabled = true;
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			if (!(bsFreeRolw.Current is CustomFreeType customFreeType))
			{
				return;
			}
			try
			{
				Console.WriteLine("1");
				string text = ucVideoPlayerEX1.TakePhoto();
				Console.WriteLine("2");
				if (text != null)
				{
					FreeImagePath = text;
				}
			}
			catch (Exception ex)
			{
				FreeImagePath = null;
				Logger.Error(ex);
				Console.WriteLine(ex.Message);
			}
			Remark = txtRemark.Text.Trim();
			m_CustomFreeType = customFreeType;
			m_CustomFreeTenat = bsTenat.Current as CustomFreeTenat;
			base.DialogResult = DialogResult.OK;
			Close();
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Console.WriteLine(ex2.Message);
			base.DialogResult = DialogResult.Cancel;
			Close();
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

	private void bsTenat_CurrentChanged(object sender, EventArgs e)
	{
		LoadFreeTypes();
	}

	private void FormTimeChargeFree_FormClosed(object sender, FormClosedEventArgs e)
	{
	}

	private void FormTimeChargeFree_FormClosing(object sender, FormClosingEventArgs e)
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
		this.ucVideoPlayerEX1 = new CarPark2018.UserControls.UCVideoPlayerEX();
		this.labTitle = new System.Windows.Forms.Label();
		this.txtRemark = new System.Windows.Forms.TextBox();
		this.labInfo = new System.Windows.Forms.Label();
		this.labRemark = new System.Windows.Forms.Label();
		this.labSearchBarCode = new System.Windows.Forms.RadioButton();
		this.labSearch = new System.Windows.Forms.RadioButton();
		this.searchFreeTenat = new System.Windows.Forms.TextBox();
		this.txtComp = new System.Windows.Forms.TextBox();
		this.btnClose = new System.Windows.Forms.Button();
		this.btnOK = new System.Windows.Forms.Button();
		this.dataFreeRule = new System.Windows.Forms.DataGridView();
		this.dataFreeTenat = new System.Windows.Forms.DataGridView();
		this.labfreeRule = new System.Windows.Forms.Label();
		this.labFreeTenat = new System.Windows.Forms.Label();
		this.panFill = new System.Windows.Forms.Panel();
		((System.ComponentModel.ISupportInitialize)this.dataFreeRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataFreeTenat).BeginInit();
		this.panFill.SuspendLayout();
		base.SuspendLayout();
		this.ucVideoPlayerEX1.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucVideoPlayerEX1.Location = new System.Drawing.Point(535, 92);
		this.ucVideoPlayerEX1.Margin = new System.Windows.Forms.Padding(21, 26, 21, 26);
		this.ucVideoPlayerEX1.Name = "ucVideoPlayerEX1";
		this.ucVideoPlayerEX1.Size = new System.Drawing.Size(310, 165);
		this.ucVideoPlayerEX1.TabIndex = 0;
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微軟正黑體", 26.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(960, 55);
		this.labTitle.TabIndex = 26;
		this.labTitle.Text = "優惠處理";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtRemark.Location = new System.Drawing.Point(206, 214);
		this.txtRemark.MaxLength = 100;
		this.txtRemark.Name = "txtRemark";
		this.txtRemark.Size = new System.Drawing.Size(269, 43);
		this.txtRemark.TabIndex = 42;
		this.labInfo.ForeColor = System.Drawing.Color.Red;
		this.labInfo.Location = new System.Drawing.Point(200, 57);
		this.labInfo.Name = "labInfo";
		this.labInfo.Size = new System.Drawing.Size(202, 40);
		this.labInfo.TabIndex = 44;
		this.labInfo.Text = "* Invalidation";
		this.labInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labRemark.Location = new System.Drawing.Point(79, 214);
		this.labRemark.Name = "labRemark";
		this.labRemark.Size = new System.Drawing.Size(121, 40);
		this.labRemark.TabIndex = 43;
		this.labRemark.Text = "備註";
		this.labRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labSearchBarCode.Location = new System.Drawing.Point(59, 101);
		this.labSearchBarCode.Name = "labSearchBarCode";
		this.labSearchBarCode.Size = new System.Drawing.Size(141, 39);
		this.labSearchBarCode.TabIndex = 39;
		this.labSearchBarCode.Text = "查找優惠";
		this.labSearchBarCode.UseVisualStyleBackColor = true;
		this.labSearchBarCode.CheckedChanged += new System.EventHandler(RadioButton_CheckedCharge);
		this.labSearch.Checked = true;
		this.labSearch.Location = new System.Drawing.Point(59, 158);
		this.labSearch.Name = "labSearch";
		this.labSearch.Size = new System.Drawing.Size(141, 39);
		this.labSearch.TabIndex = 40;
		this.labSearch.TabStop = true;
		this.labSearch.Text = "查找客戶";
		this.labSearch.UseVisualStyleBackColor = true;
		this.labSearch.CheckedChanged += new System.EventHandler(RadioButton_CheckedCharge);
		this.searchFreeTenat.Enabled = false;
		this.searchFreeTenat.Location = new System.Drawing.Point(206, 157);
		this.searchFreeTenat.MaxLength = 20;
		this.searchFreeTenat.Name = "searchFreeTenat";
		this.searchFreeTenat.Size = new System.Drawing.Size(269, 43);
		this.searchFreeTenat.TabIndex = 41;
		this.searchFreeTenat.TextChanged += new System.EventHandler(searchFreeTenat_TextChanged);
		this.txtComp.Location = new System.Drawing.Point(206, 100);
		this.txtComp.MaxLength = 13;
		this.txtComp.Name = "txtComp";
		this.txtComp.Size = new System.Drawing.Size(269, 43);
		this.txtComp.TabIndex = 38;
		this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnClose.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.Location = new System.Drawing.Point(511, 543);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(120, 48);
		this.btnClose.TabIndex = 48;
		this.btnClose.Text = "取消";
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnOK.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.Location = new System.Drawing.Point(382, 543);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(120, 48);
		this.btnOK.TabIndex = 47;
		this.btnOK.Text = "確認";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.dataFreeRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataFreeRule.Location = new System.Drawing.Point(535, 311);
		this.dataFreeRule.MultiSelect = false;
		this.dataFreeRule.Name = "dataFreeRule";
		this.dataFreeRule.RowTemplate.Height = 24;
		this.dataFreeRule.Size = new System.Drawing.Size(417, 222);
		this.dataFreeRule.TabIndex = 46;
		this.dataFreeRule.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataFreeRule_DataError);
		this.dataFreeTenat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataFreeTenat.Location = new System.Drawing.Point(10, 311);
		this.dataFreeTenat.MultiSelect = false;
		this.dataFreeTenat.Name = "dataFreeTenat";
		this.dataFreeTenat.RowTemplate.Height = 24;
		this.dataFreeTenat.Size = new System.Drawing.Size(481, 222);
		this.dataFreeTenat.TabIndex = 45;
		this.dataFreeTenat.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataFreeTenat_DataError);
		this.labfreeRule.Location = new System.Drawing.Point(529, 268);
		this.labfreeRule.Name = "labfreeRule";
		this.labfreeRule.Size = new System.Drawing.Size(156, 40);
		this.labfreeRule.TabIndex = 50;
		this.labfreeRule.Text = "免费规则";
		this.labfreeRule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFreeTenat.Location = new System.Drawing.Point(12, 268);
		this.labFreeTenat.Name = "labFreeTenat";
		this.labFreeTenat.Size = new System.Drawing.Size(188, 40);
		this.labFreeTenat.TabIndex = 49;
		this.labFreeTenat.Text = "免費客戶";
		this.labFreeTenat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.btnClose);
		this.panFill.Controls.Add(this.btnOK);
		this.panFill.Controls.Add(this.dataFreeRule);
		this.panFill.Controls.Add(this.dataFreeTenat);
		this.panFill.Controls.Add(this.labfreeRule);
		this.panFill.Controls.Add(this.labFreeTenat);
		this.panFill.Controls.Add(this.txtRemark);
		this.panFill.Controls.Add(this.labInfo);
		this.panFill.Controls.Add(this.labRemark);
		this.panFill.Controls.Add(this.labSearchBarCode);
		this.panFill.Controls.Add(this.labSearch);
		this.panFill.Controls.Add(this.searchFreeTenat);
		this.panFill.Controls.Add(this.txtComp);
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Controls.Add(this.ucVideoPlayerEX1);
		this.panFill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(962, 600);
		this.panFill.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(16f, 35f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		base.ClientSize = new System.Drawing.Size(962, 600);
		base.Controls.Add(this.panFill);
		this.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ForeColor = System.Drawing.Color.Navy;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
		base.Name = "FormTimeChargeFree";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Form1";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormTimeChargeFree_FormClosing);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormTimeChargeFree_FormClosed);
		base.Load += new System.EventHandler(Form1_Load);
		((System.ComponentModel.ISupportInitialize)this.dataFreeRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataFreeTenat).EndInit();
		this.panFill.ResumeLayout(false);
		this.panFill.PerformLayout();
		base.ResumeLayout(false);
	}
}
