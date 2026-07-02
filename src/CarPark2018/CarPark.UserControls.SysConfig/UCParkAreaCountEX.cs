using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using CarPark2018;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark.UserControls.SysConfig;

public class UCParkAreaCountEX : UserControl, IMessageFilter
{
	private ILog Logger;

	private GetParkAreaArgs getArg = new GetParkAreaArgs();

	private ParkAreaExtend parkAreaExtend = new ParkAreaExtend();

	private List<ParkAreaExtend> m_ParkAreaExtend = new List<ParkAreaExtend>();

	private List<ParkArea> m_ParkArea = new List<ParkArea>();

	private IContainer components = null;

	private Label labnameCn;

	private TextBox bindExtendNameCn;

	private Label labNamePt;

	private TextBox bindExtendNamePt;

	private Label labTotalSupply;

	private Label labTotalUse;

	private Label labFixSupply;

	private Label labFixUse;

	private Label labFloatSupply;

	private Label labFloatUse;

	private Label labTimeChargeSupply;

	private Label labTimeChargeUse;

	private Label labTimeChargRemain;

	private Label labExtendCount;

	private Button btnRefresh;

	private Button btnSave;

	private NumericUpDown bindTotalSupply;

	private NumericUpDown bindCurrentUse;

	private NumericUpDown bindFixParkSupply;

	private NumericUpDown bindFixParkUse;

	private NumericUpDown bindFloatParkSupply;

	private NumericUpDown bindFloatParkUse;

	private NumericUpDown bindTimeChargeSupply;

	private NumericUpDown bindTimeChargeUse;

	private NumericUpDown bindTimeChargRemain;

	private NumericUpDown bindExtendCount;

	private Label labFloatSupply5;

	private Label labFloatUse5;

	private NumericUpDown bindFloatParkSupply5;

	private NumericUpDown bindFloatParkUse5;

	public UCParkAreaCountEX()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		InitializeComponent();
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labExtendCount.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labExtendCount");
		labnameCn.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labnameCn");
		labNamePt.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labNamePt");
		labTimeChargeSupply.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labTimeChargeSupply");
		labTimeChargeUse.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labTimeChargeUse");
		labTimeChargRemain.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labTimeChargRemain");
		labTotalSupply.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labTotalSupply");
		labTotalUse.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labTotalUse");
		btnRefresh.Text = LangManager.GetLangString("UcParkAreaCount.panMain.btnRefresh");
		btnSave.Text = LangManager.GetLangString("UcParkAreaCount.panMain.btnSave");
		labFixSupply.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labFixSupply");
		labFixUse.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labFixUse");
		labFloatSupply.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labFloatSupply");
		labFloatUse.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labFloatUse");
		labFloatSupply5.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labFloatSupply5");
		labFloatUse5.Text = LangManager.GetLangString("UcParkAreaCount.panMain.labFloatUse5");
	}

	private void btnRefresh_Click(object sender, EventArgs e)
	{
		try
		{
			ChargeContext chargeContext = new ChargeContext();
			GetParkAreaReturn getParkAreaReturn = chargeContext.CommunicationChannel.GetParkAreaExtend(getArg, out m_ParkAreaExtend, out m_ParkArea);
			chargeContext.CommunicationChannel.Disconnect();
			if (getParkAreaReturn.ISOK)
			{
				ParkAreaExtend extend1 = this.parkAreaExtend;
				ParkAreaExtend parkAreaExtend = m_ParkAreaExtend.Where((ParkAreaExtend m) => m.ParkTypeID == extend1.ParkTypeID && m.AreaID == extend1.AreaID).FirstOrDefault();
				if (parkAreaExtend != null)
				{
					LoadData(parkAreaExtend);
				}
			}
			else
			{
				Console.WriteLine(getParkAreaReturn.ErrCode);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			DataBuffer2018.CheckRole(MethodBase.GetCurrentMethod());
			if (bindTotalSupply.Value < bindFixParkSupply.Value + bindFloatParkSupply.Value)
			{
				throw new Exception("供應數計算錯誤");
			}
			ParkAreaExtend parkAreaExtend = this.parkAreaExtend;
			parkAreaExtend.ExtendCount = Convert.ToInt32(bindExtendCount.Value);
			parkAreaExtend.ExtendNameCn = bindExtendNameCn.Text;
			parkAreaExtend.ExtendNamePt = bindExtendNamePt.Text;
			parkAreaExtend.TotalSupply = Convert.ToInt32(bindTotalSupply.Value);
			parkAreaExtend.TimeChargeUse = Convert.ToInt32(bindTimeChargeUse.Value);
			parkAreaExtend.FixParkSupply = Convert.ToInt32(bindFixParkSupply.Value);
			parkAreaExtend.FloatParkSupply = Convert.ToInt32(bindFloatParkSupply.Value);
			parkAreaExtend.FloatParkSupply5 = Convert.ToInt32(bindFloatParkSupply5.Value);
			parkAreaExtend.FixParkUse = Convert.ToInt32(bindFixParkUse.Value);
			parkAreaExtend.FloatParkUse = Convert.ToInt32(bindFloatParkUse.Value);
			parkAreaExtend.FloatParkUse5 = Convert.ToInt32(bindFloatParkUse5.Value);
			ModifyParkAreaExtendArgs modifyParkAreaExtendArgs = new ModifyParkAreaExtendArgs();
			modifyParkAreaExtendArgs.PayStationName = Settings.Default.OnlyID;
			modifyParkAreaExtendArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			ChargeContext chargeContext = new ChargeContext();
			ModifyParkAreaExtendReturn modifyParkAreaExtendReturn = chargeContext.CommunicationChannel.ModifyParkAreaExtend(modifyParkAreaExtendArgs, parkAreaExtend);
			chargeContext.CommunicationChannel.Disconnect();
			if (modifyParkAreaExtendReturn.ISOK)
			{
				MessageBox.Show("保存成功");
				btnRefresh_Click(null, null);
			}
			else
			{
				MessageBox.Show(modifyParkAreaExtendReturn.ErrCode);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			MessageBox.Show(LangManager.GetLangString(ex2.Message));
		}
	}

	public void LoadData(ParkAreaExtend extend)
	{
		try
		{
			parkAreaExtend = extend;
			bindExtendNameCn.Text = extend.ExtendNameCn;
			bindExtendNamePt.Text = extend.ExtendNamePt;
			bindTotalSupply.Value = extend.TotalSupply;
			bindCurrentUse.Value = extend.CurrentUse;
			bindFixParkSupply.Value = extend.FixParkSupply;
			bindFixParkUse.Value = extend.FixParkUse;
			bindFloatParkSupply.Value = extend.FloatParkSupply;
			bindFloatParkUse.Value = extend.FloatParkUse;
			bindTimeChargeSupply.Value = extend.TimeChargeSupply;
			bindTimeChargeUse.Value = extend.TimeChargeUse;
			bindTimeChargRemain.Value = extend.TimeChargRemain;
			bindExtendCount.Value = extend.ExtendCount;
			bindFloatParkUse5.Value = (extend.FloatParkUse5.HasValue ? Convert.ToInt32(extend.FloatParkUse5) : 0);
			bindFloatParkSupply5.Value = (extend.FloatParkSupply5.HasValue ? Convert.ToInt32(extend.FloatParkSupply5) : 0);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
	}

	public bool PreFilterMessage(ref Message m)
	{
		if (m.Msg == 522)
		{
			return true;
		}
		return false;
	}

	private void UCParkAreaCountEX_Load(object sender, EventArgs e)
	{
		Application.AddMessageFilter(this);
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
		this.labnameCn = new System.Windows.Forms.Label();
		this.bindExtendNameCn = new System.Windows.Forms.TextBox();
		this.labNamePt = new System.Windows.Forms.Label();
		this.bindExtendNamePt = new System.Windows.Forms.TextBox();
		this.labTotalSupply = new System.Windows.Forms.Label();
		this.labTotalUse = new System.Windows.Forms.Label();
		this.labFixSupply = new System.Windows.Forms.Label();
		this.labFixUse = new System.Windows.Forms.Label();
		this.labFloatSupply = new System.Windows.Forms.Label();
		this.labFloatUse = new System.Windows.Forms.Label();
		this.labTimeChargeSupply = new System.Windows.Forms.Label();
		this.labTimeChargeUse = new System.Windows.Forms.Label();
		this.labTimeChargRemain = new System.Windows.Forms.Label();
		this.labExtendCount = new System.Windows.Forms.Label();
		this.btnRefresh = new System.Windows.Forms.Button();
		this.btnSave = new System.Windows.Forms.Button();
		this.bindTotalSupply = new System.Windows.Forms.NumericUpDown();
		this.bindCurrentUse = new System.Windows.Forms.NumericUpDown();
		this.bindFixParkSupply = new System.Windows.Forms.NumericUpDown();
		this.bindFixParkUse = new System.Windows.Forms.NumericUpDown();
		this.bindFloatParkSupply = new System.Windows.Forms.NumericUpDown();
		this.bindFloatParkUse = new System.Windows.Forms.NumericUpDown();
		this.bindTimeChargeSupply = new System.Windows.Forms.NumericUpDown();
		this.bindTimeChargeUse = new System.Windows.Forms.NumericUpDown();
		this.bindTimeChargRemain = new System.Windows.Forms.NumericUpDown();
		this.bindExtendCount = new System.Windows.Forms.NumericUpDown();
		this.labFloatSupply5 = new System.Windows.Forms.Label();
		this.labFloatUse5 = new System.Windows.Forms.Label();
		this.bindFloatParkSupply5 = new System.Windows.Forms.NumericUpDown();
		this.bindFloatParkUse5 = new System.Windows.Forms.NumericUpDown();
		((System.ComponentModel.ISupportInitialize)this.bindTotalSupply).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindCurrentUse).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFixParkSupply).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFixParkUse).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFloatParkSupply).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFloatParkUse).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindTimeChargeSupply).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindTimeChargeUse).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindTimeChargRemain).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindExtendCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFloatParkSupply5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.bindFloatParkUse5).BeginInit();
		base.SuspendLayout();
		this.labnameCn.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labnameCn.Location = new System.Drawing.Point(10, 13);
		this.labnameCn.Name = "labnameCn";
		this.labnameCn.Size = new System.Drawing.Size(200, 34);
		this.labnameCn.TabIndex = 0;
		this.labnameCn.Text = "中文名稱";
		this.labnameCn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindExtendNameCn.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindExtendNameCn.Location = new System.Drawing.Point(216, 13);
		this.bindExtendNameCn.Name = "bindExtendNameCn";
		this.bindExtendNameCn.Size = new System.Drawing.Size(231, 34);
		this.bindExtendNameCn.TabIndex = 1;
		this.labNamePt.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labNamePt.Location = new System.Drawing.Point(453, 12);
		this.labNamePt.Name = "labNamePt";
		this.labNamePt.Size = new System.Drawing.Size(200, 34);
		this.labNamePt.TabIndex = 0;
		this.labNamePt.Text = "英文名稱";
		this.labNamePt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindExtendNamePt.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindExtendNamePt.Location = new System.Drawing.Point(659, 12);
		this.bindExtendNamePt.Name = "bindExtendNamePt";
		this.bindExtendNamePt.Size = new System.Drawing.Size(231, 34);
		this.bindExtendNamePt.TabIndex = 1;
		this.labTotalSupply.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labTotalSupply.Location = new System.Drawing.Point(10, 53);
		this.labTotalSupply.Name = "labTotalSupply";
		this.labTotalSupply.Size = new System.Drawing.Size(200, 34);
		this.labTotalSupply.TabIndex = 0;
		this.labTotalSupply.Text = "總供應";
		this.labTotalSupply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTotalUse.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labTotalUse.Location = new System.Drawing.Point(453, 53);
		this.labTotalUse.Name = "labTotalUse";
		this.labTotalUse.Size = new System.Drawing.Size(200, 34);
		this.labTotalUse.TabIndex = 0;
		this.labTotalUse.Text = "總佔用";
		this.labTotalUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFixSupply.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labFixSupply.Location = new System.Drawing.Point(10, 93);
		this.labFixSupply.Name = "labFixSupply";
		this.labFixSupply.Size = new System.Drawing.Size(200, 34);
		this.labFixSupply.TabIndex = 0;
		this.labFixSupply.Text = "留用車位供應";
		this.labFixSupply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFixUse.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labFixUse.Location = new System.Drawing.Point(453, 93);
		this.labFixUse.Name = "labFixUse";
		this.labFixUse.Size = new System.Drawing.Size(200, 34);
		this.labFixUse.TabIndex = 0;
		this.labFixUse.Text = "留用車位佔用";
		this.labFixUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFloatSupply.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labFloatSupply.Location = new System.Drawing.Point(10, 133);
		this.labFloatSupply.Name = "labFloatSupply";
		this.labFloatSupply.Size = new System.Drawing.Size(200, 34);
		this.labFloatSupply.TabIndex = 0;
		this.labFloatSupply.Text = "職員車位供應";
		this.labFloatSupply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFloatUse.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labFloatUse.Location = new System.Drawing.Point(453, 133);
		this.labFloatUse.Name = "labFloatUse";
		this.labFloatUse.Size = new System.Drawing.Size(200, 34);
		this.labFloatUse.TabIndex = 0;
		this.labFloatUse.Text = "職員車位佔用";
		this.labFloatUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTimeChargeSupply.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labTimeChargeSupply.Location = new System.Drawing.Point(10, 218);
		this.labTimeChargeSupply.Name = "labTimeChargeSupply";
		this.labTimeChargeSupply.Size = new System.Drawing.Size(200, 34);
		this.labTimeChargeSupply.TabIndex = 0;
		this.labTimeChargeSupply.Text = "時租車位供應";
		this.labTimeChargeSupply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTimeChargeUse.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labTimeChargeUse.Location = new System.Drawing.Point(453, 218);
		this.labTimeChargeUse.Name = "labTimeChargeUse";
		this.labTimeChargeUse.Size = new System.Drawing.Size(200, 34);
		this.labTimeChargeUse.TabIndex = 0;
		this.labTimeChargeUse.Text = "時租車位佔用";
		this.labTimeChargeUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTimeChargRemain.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labTimeChargRemain.Location = new System.Drawing.Point(10, 258);
		this.labTimeChargRemain.Name = "labTimeChargRemain";
		this.labTimeChargRemain.Size = new System.Drawing.Size(200, 34);
		this.labTimeChargRemain.TabIndex = 0;
		this.labTimeChargRemain.Text = "時租剩餘車位";
		this.labTimeChargRemain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labExtendCount.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labExtendCount.Location = new System.Drawing.Point(453, 258);
		this.labExtendCount.Name = "labExtendCount";
		this.labExtendCount.Size = new System.Drawing.Size(200, 34);
		this.labExtendCount.TabIndex = 0;
		this.labExtendCount.Text = "擴展車位";
		this.labExtendCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.btnRefresh.Location = new System.Drawing.Point(249, 333);
		this.btnRefresh.Name = "btnRefresh";
		this.btnRefresh.Size = new System.Drawing.Size(120, 55);
		this.btnRefresh.TabIndex = 2;
		this.btnRefresh.Text = "刷新";
		this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnRefresh.UseVisualStyleBackColor = true;
		this.btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
		this.btnSave.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.btnSave.Location = new System.Drawing.Point(532, 333);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(120, 55);
		this.btnSave.TabIndex = 2;
		this.btnSave.Text = "保存";
		this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.bindTotalSupply.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindTotalSupply.Location = new System.Drawing.Point(216, 53);
		this.bindTotalSupply.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindTotalSupply.Name = "bindTotalSupply";
		this.bindTotalSupply.Size = new System.Drawing.Size(231, 34);
		this.bindTotalSupply.TabIndex = 3;
		this.bindCurrentUse.Enabled = false;
		this.bindCurrentUse.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindCurrentUse.Location = new System.Drawing.Point(659, 53);
		this.bindCurrentUse.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindCurrentUse.Name = "bindCurrentUse";
		this.bindCurrentUse.Size = new System.Drawing.Size(231, 34);
		this.bindCurrentUse.TabIndex = 3;
		this.bindFixParkSupply.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindFixParkSupply.Location = new System.Drawing.Point(216, 93);
		this.bindFixParkSupply.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindFixParkSupply.Name = "bindFixParkSupply";
		this.bindFixParkSupply.Size = new System.Drawing.Size(231, 34);
		this.bindFixParkSupply.TabIndex = 3;
		this.bindFixParkUse.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindFixParkUse.Location = new System.Drawing.Point(659, 93);
		this.bindFixParkUse.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindFixParkUse.Name = "bindFixParkUse";
		this.bindFixParkUse.Size = new System.Drawing.Size(231, 34);
		this.bindFixParkUse.TabIndex = 3;
		this.bindFloatParkSupply.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindFloatParkSupply.Location = new System.Drawing.Point(216, 133);
		this.bindFloatParkSupply.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindFloatParkSupply.Name = "bindFloatParkSupply";
		this.bindFloatParkSupply.Size = new System.Drawing.Size(231, 34);
		this.bindFloatParkSupply.TabIndex = 3;
		this.bindFloatParkUse.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindFloatParkUse.Location = new System.Drawing.Point(659, 133);
		this.bindFloatParkUse.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindFloatParkUse.Name = "bindFloatParkUse";
		this.bindFloatParkUse.Size = new System.Drawing.Size(231, 34);
		this.bindFloatParkUse.TabIndex = 3;
		this.bindTimeChargeSupply.Enabled = false;
		this.bindTimeChargeSupply.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindTimeChargeSupply.Location = new System.Drawing.Point(216, 218);
		this.bindTimeChargeSupply.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindTimeChargeSupply.Name = "bindTimeChargeSupply";
		this.bindTimeChargeSupply.Size = new System.Drawing.Size(231, 34);
		this.bindTimeChargeSupply.TabIndex = 3;
		this.bindTimeChargeUse.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindTimeChargeUse.Location = new System.Drawing.Point(659, 218);
		this.bindTimeChargeUse.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindTimeChargeUse.Name = "bindTimeChargeUse";
		this.bindTimeChargeUse.Size = new System.Drawing.Size(231, 34);
		this.bindTimeChargeUse.TabIndex = 3;
		this.bindTimeChargRemain.Enabled = false;
		this.bindTimeChargRemain.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindTimeChargRemain.Location = new System.Drawing.Point(216, 258);
		this.bindTimeChargRemain.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindTimeChargRemain.Name = "bindTimeChargRemain";
		this.bindTimeChargRemain.Size = new System.Drawing.Size(231, 34);
		this.bindTimeChargRemain.TabIndex = 3;
		this.bindExtendCount.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindExtendCount.Location = new System.Drawing.Point(659, 258);
		this.bindExtendCount.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindExtendCount.Minimum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		this.bindExtendCount.Name = "bindExtendCount";
		this.bindExtendCount.Size = new System.Drawing.Size(231, 34);
		this.bindExtendCount.TabIndex = 3;
		this.labFloatSupply5.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labFloatSupply5.Location = new System.Drawing.Point(10, 176);
		this.labFloatSupply5.Name = "labFloatSupply5";
		this.labFloatSupply5.Size = new System.Drawing.Size(200, 34);
		this.labFloatSupply5.TabIndex = 0;
		this.labFloatSupply5.Text = "學生車位供應";
		this.labFloatSupply5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFloatUse5.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.labFloatUse5.Location = new System.Drawing.Point(453, 176);
		this.labFloatUse5.Name = "labFloatUse5";
		this.labFloatUse5.Size = new System.Drawing.Size(200, 34);
		this.labFloatUse5.TabIndex = 0;
		this.labFloatUse5.Text = "學生車位佔用";
		this.labFloatUse5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindFloatParkSupply5.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindFloatParkSupply5.Location = new System.Drawing.Point(216, 176);
		this.bindFloatParkSupply5.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindFloatParkSupply5.Name = "bindFloatParkSupply5";
		this.bindFloatParkSupply5.Size = new System.Drawing.Size(231, 34);
		this.bindFloatParkSupply5.TabIndex = 3;
		this.bindFloatParkUse5.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.bindFloatParkUse5.Location = new System.Drawing.Point(659, 176);
		this.bindFloatParkUse5.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.bindFloatParkUse5.Name = "bindFloatParkUse5";
		this.bindFloatParkUse5.Size = new System.Drawing.Size(231, 34);
		this.bindFloatParkUse5.TabIndex = 3;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.bindExtendCount);
		base.Controls.Add(this.bindTimeChargeUse);
		base.Controls.Add(this.bindFloatParkUse5);
		base.Controls.Add(this.bindFloatParkUse);
		base.Controls.Add(this.bindFixParkUse);
		base.Controls.Add(this.bindCurrentUse);
		base.Controls.Add(this.bindTimeChargRemain);
		base.Controls.Add(this.bindTimeChargeSupply);
		base.Controls.Add(this.bindFloatParkSupply5);
		base.Controls.Add(this.bindFloatParkSupply);
		base.Controls.Add(this.bindFixParkSupply);
		base.Controls.Add(this.bindTotalSupply);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.labFloatUse5);
		base.Controls.Add(this.btnRefresh);
		base.Controls.Add(this.labExtendCount);
		base.Controls.Add(this.labTimeChargeUse);
		base.Controls.Add(this.labFloatUse);
		base.Controls.Add(this.labTimeChargRemain);
		base.Controls.Add(this.labFloatSupply5);
		base.Controls.Add(this.labFixUse);
		base.Controls.Add(this.labTimeChargeSupply);
		base.Controls.Add(this.bindExtendNamePt);
		base.Controls.Add(this.labFloatSupply);
		base.Controls.Add(this.labTotalUse);
		base.Controls.Add(this.labFixSupply);
		base.Controls.Add(this.bindExtendNameCn);
		base.Controls.Add(this.labTotalSupply);
		base.Controls.Add(this.labNamePt);
		base.Controls.Add(this.labnameCn);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ForeColor = System.Drawing.Color.Navy;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "UCParkAreaCountEX";
		base.Size = new System.Drawing.Size(900, 620);
		base.Load += new System.EventHandler(UCParkAreaCountEX_Load);
		((System.ComponentModel.ISupportInitialize)this.bindTotalSupply).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindCurrentUse).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFixParkSupply).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFixParkUse).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFloatParkSupply).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFloatParkUse).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindTimeChargeSupply).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindTimeChargeUse).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindTimeChargRemain).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindExtendCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFloatParkSupply5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.bindFloatParkUse5).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
