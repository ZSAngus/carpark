using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.Device;
using CarPark.Lib;
using CarPark2018.Properties;
using MacauPass.POSCom.Package;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.Forms;

public class FormMPassCharge : Form
{
	private ILog Logger;

	private readonly DateTime initTime;

	private ShiftRecord m_ShiftRecord = null;

	private ChargeContext chargeContext = new ChargeContext();

	private bool Syn = false;

	private IContainer components = null;

	private Label labTitle;

	private TextBox txtCardCode;

	private Label labInTime;

	private TextBox txtOragBalance;

	private Label labChargeTime;

	private TextBox txtRealAmt;

	private Label labelX4;

	private Label labelX3;

	private Label labelX2;

	private Label labParkTime;

	private TextBox txtBalance;

	private Label labelX1;

	private Button btnClose;

	private Button btnOk;

	private ComboBox comboValetType;

	private ComboBox comboCashType;

	private NumericUpDown valReload;

	private Panel panel1;

	private Panel panel3;

	private Panel panel2;

	private ComboBox comboReloadType;

	private Label labReloadType;

	public FormMPassCharge()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		components = null;
		initTime = DateTime.Now;
		comboCashType.SelectedIndex = 0;
		comboCashType_SelectedIndexChanged(null, null);
		comboReloadType.SelectedIndex = 0;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
		try
		{
			GetCurrShiftRecordArgs getCurrShiftRecordArgs = new GetCurrShiftRecordArgs
			{
				PayStationName = Settings.Default.OnlyID
			};
			m_ShiftRecord = new ShiftRecord();
			Logger.Debug("[FormMPassCharge]:StartTime=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
			chargeContext.CommunicationChannel.GetCurrShiftRecord(getCurrShiftRecordArgs, out m_ShiftRecord);
			Logger.Debug("[FormMPassCharge]:GetCurrShiftRecordTime=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
			chargeContext.CommunicationChannel.Disconnect();
			Logger.Debug("[FormMPassCharge]:EndTime=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
		}
		catch (Exception message)
		{
			Logger.Error(message);
			Global.ShowMessage("初始化界面失败，请重新开启");
			Close();
		}
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labChargeTime.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.labChargeTime");
		labelX1.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.labelX1");
		labelX2.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.labelX2");
		labelX3.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.labelX3");
		labelX4.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.labelX4");
		labInTime.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.labInTime");
		labParkTime.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.labParkTime");
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.labTitle");
		btnClose.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.btnClose");
		btnOk.Text = LangManager.GetLangString("CarPark.Forms.MPassCharge.btnOK");
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		MPassChargeDialog mPassChargeDialog = new MPassChargeDialog
		{
			BillType = EnumBillType.MacauPassDecal,
			ReloadAmt = valReload.Value,
			CashType = comboValetType.SelectedItem.ToString(),
			ValType = comboValetType.SelectedItem.ToString(),
			CardType = comboReloadType.SelectedItem.ToString()
		};
		using MPassChargeDialog mPassChargeDialog2 = mPassChargeDialog;
		if (mPassChargeDialog2.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		ReloadResult reloadResult = null;
		try
		{
			reloadResult = mPassChargeDialog2.ReloadResult;
			txtBalance.Text = reloadResult.BALANCE.ToString("f2");
			txtCardCode.Text = (string.IsNullOrWhiteSpace(reloadResult.PAN) ? reloadResult.PAY_ACCOUNT : reloadResult.PAN);
			txtOragBalance.Text = reloadResult.ORIGBALANCE.ToString("f2");
			btnOk.Visible = false;
			Application.DoEvents();
		}
		catch (Exception ex)
		{
			Logger.Error("[ReloadResult]=" + ex);
		}
		ChargeRecord chargeRecord = new ChargeRecord();
		try
		{
			chargeRecord.BillType = 12;
			chargeRecord.CardCode = (string.IsNullOrWhiteSpace(reloadResult.PAN) ? reloadResult.PAY_ACCOUNT : reloadResult.PAN);
			chargeRecord.ChargeTime = reloadResult.TransactionTime;
			chargeRecord.ChargeMin = 0;
			chargeRecord.FreeMin = 0;
			chargeRecord.FreeCharge = 0m;
			chargeRecord.ParkMin = 0;
			chargeRecord.FromStation = Settings.Default.OnlyID;
			chargeRecord.ShiftID = ((m_ShiftRecord != null) ? m_ShiftRecord.ShiftID : 0);
			chargeRecord.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			chargeRecord.TotalCharge = reloadResult.TOTALAMT;
			chargeRecord.ParkTypeID = 0;
			chargeRecord.PayType = 0;
		}
		catch (Exception ex2)
		{
			Logger.Error("[ChargeRecord]=" + ex2);
		}
		MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = null;
		try
		{
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail2 = new MPass_POS_Transaction_Detail
			{
				ChargeTransactionID = chargeRecord.TimeChargeID
			};
			mPass_POS_Transaction_Detail = mPass_POS_Transaction_Detail2;
			EntityHelper.CopyEntity(reloadResult, mPass_POS_Transaction_Detail);
			mPass_POS_Transaction_Detail.CashType = mPassChargeDialog2.CashType;
		}
		catch (Exception ex3)
		{
			Logger.Error("[MPass_POS_Transaction_Detail]=" + ex3);
		}
		Console.WriteLine(DateTime.Now.ToString());
		try
		{
			SaveMPassChargeReturn saveMPassChargeReturn = null;
			ChargeRecord chargeRecord2 = null;
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail3 = null;
			BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend = null;
			try
			{
				Logger.Error("[datetime]" + DateTime.Now.ToString());
				if (chargeRecord != null)
				{
					Logger.Debug("[FormMPassCharge]:Begin BillType=" + chargeRecord.BillType + ",BufferTime=" + chargeRecord.BufferTime + ",CardCode=" + chargeRecord.CardCode + ",ChargeMin=" + chargeRecord.ChargeMin + ",ChargeTime=" + chargeRecord.ChargeTime.ToString() + ",Currency=" + chargeRecord.Currency + ",Fine=" + chargeRecord.Fine + ",FirstTime=" + chargeRecord.FirstTime + ",FreeCharge=" + chargeRecord.FreeCharge + ",FreeMin=" + chargeRecord.FreeMin + ",FromStation=" + chargeRecord.FromStation + ",GetPeriodofTime=" + chargeRecord.GetPeriodofTime + ",IsDelete=" + chargeRecord.IsDelete + ".,IsOverTime=" + chargeRecord.IsOverTime + ",ParkMin=" + chargeRecord.ParkMin + ",ParkTimeSpan=" + chargeRecord.ParkTimeSpan.ToString() + ",ParkTypeID=" + chargeRecord.ParkTypeID + ",PayType=" + chargeRecord.PayType + ",PeriodofTime=" + chargeRecord.PeriodofTime + ",PeriodofTimeInfoList=" + chargeRecord.PeriodofTimeInfoList?.ToString() + ",Remark=" + chargeRecord.Remark + ",ShiftID=" + chargeRecord.ShiftID + ",StaffCode=" + chargeRecord.StaffCode + ",TimeChargeID=" + chargeRecord.TimeChargeID + ",TotalCharge=" + chargeRecord.TotalCharge + ",TransactionID=" + chargeRecord.TransactionID);
				}
				saveMPassChargeReturn = chargeContext.CommunicationChannel.SaveMPassCharge(null, chargeRecord, mPass_POS_Transaction_Detail, null);
				chargeContext.CommunicationChannel.Disconnect();
				Logger.Debug("[FormMPassCharge]:Begin ErrCode=" + saveMPassChargeReturn.ErrCode + ",ISValid=" + saveMPassChargeReturn.ISValid);
				Logger.Error("[datetime]" + DateTime.Now.ToString());
			}
			catch (Exception message)
			{
				Logger.Error(message);
				if (mPass_POS_Transaction_Detail != null)
				{
					DBHelper.Insert(chargeRecord.CardCode, chargeRecord, mPass_POS_Transaction_Detail, null, null, null, null);
					chargeRecord2 = DBHelper.SelectChargeRecord(chargeRecord.CardCode);
					if (mPass_POS_Transaction_Detail != null)
					{
						mPass_POS_Transaction_Detail3 = DBHelper.SelectMPass_POS_Transaction_Detail(chargeRecord.TimeChargeID);
					}
					saveMPassChargeReturn = new SaveMPassChargeReturn();
					saveMPassChargeReturn.ISValid = true;
					Syn = true;
				}
				else
				{
					Logger.Error(message);
				}
			}
			if (saveMPassChargeReturn.ISValid)
			{
				if (Syn)
				{
					Global.ShowMessage("有一條數據沒有同步，現在馬上同步");
					try
					{
						if (mPass_POS_Transaction_Detail3 != null)
						{
							saveMPassChargeReturn = chargeContext.CommunicationChannel.SaveMPassCharge(null, chargeRecord2, mPass_POS_Transaction_Detail3, null);
							chargeContext.CommunicationChannel.Disconnect();
							Logger.Debug("[FormMPassCharge]:Begin SynData ErrCode=" + saveMPassChargeReturn.ErrCode + ",ISValid=" + saveMPassChargeReturn.ISValid);
						}
						if (!saveMPassChargeReturn.ISValid)
						{
							Global.ShowMessage("同步失敗，請聯繫技術人員");
						}
						else
						{
							DBHelper.ExecuteNonQuery($"update ChargeRecord set isupload='1' where timechargeid={chargeRecord2.TimeChargeID}", CommandType.Text, (IDbDataParameter[])null);
						}
					}
					catch (Exception)
					{
						Global.ShowMessage("同步失敗，請聯繫技術人員");
					}
				}
				if (chargeRecord.TotalCharge != 0m)
				{
					try
					{
						DeviceManager.FeeCenterModule.OpenCash();
					}
					catch (Exception message2)
					{
						Logger.Error(message2);
					}
				}
				Global.ShowMessage("增值成功");
				Close();
			}
			else
			{
				Global.ShowMessage(saveMPassChargeReturn.ErrCode);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
		}
		catch (Exception ex6)
		{
			Global.ShowMessage(ex6.ToString());
		}
		PrintUtils.PrintMPReload(chargeRecord, mPass_POS_Transaction_Detail);
		Close();
	}

	private void CalcRealCount()
	{
		try
		{
			txtRealAmt.Text = ((IMPPOSTranscation)DeviceManager.FeeCenterModule).CalcPayment(valReload.Value, (EnumPaymentRate)Enum.Parse(typeof(EnumPaymentRate), comboValetType.SelectedItem.ToString()), (EnumPaymentRate)Enum.Parse(typeof(EnumPaymentRate), comboValetType.SelectedItem.ToString()));
			FormFee.Self().SetMPass(txtRealAmt.Text);
		}
		catch (NotSupportedException)
		{
			Global.ShowMessage(LangManager.GetLangString("Not Supported Rate"));
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void comboValetType_SelectedIndexChanged(object sender, EventArgs e)
	{
		CalcRealCount();
	}

	private void comboCashType_SelectedIndexChanged(object sender, EventArgs e)
	{
		comboValetType.Items.Clear();
		if (comboCashType.SelectedIndex == 0)
		{
			comboValetType.Items.Add("MOP");
			comboValetType.Items.Add("RMB");
			comboValetType.Items.Add("HKD");
			comboValetType.SelectedIndex = 0;
		}
		else if (comboCashType.SelectedIndex == 1)
		{
			comboValetType.Items.Add("RMB");
			comboValetType.SelectedIndex = 0;
		}
		else if (comboCashType.SelectedIndex == 2)
		{
			comboValetType.Items.Add("HKD");
			comboValetType.SelectedIndex = 0;
		}
		CalcRealCount();
	}

	private void valReload_ValueChanged(object sender, EventArgs e)
	{
		CalcRealCount();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void FormMPassCharge_FormClosing(object sender, FormClosingEventArgs e)
	{
		FormFee.Self2();
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
		this.valReload = new System.Windows.Forms.NumericUpDown();
		this.comboValetType = new System.Windows.Forms.ComboBox();
		this.comboCashType = new System.Windows.Forms.ComboBox();
		this.btnClose = new System.Windows.Forms.Button();
		this.btnOk = new System.Windows.Forms.Button();
		this.txtOragBalance = new System.Windows.Forms.TextBox();
		this.labChargeTime = new System.Windows.Forms.Label();
		this.txtRealAmt = new System.Windows.Forms.TextBox();
		this.labelX4 = new System.Windows.Forms.Label();
		this.labelX3 = new System.Windows.Forms.Label();
		this.labelX2 = new System.Windows.Forms.Label();
		this.labParkTime = new System.Windows.Forms.Label();
		this.txtBalance = new System.Windows.Forms.TextBox();
		this.labelX1 = new System.Windows.Forms.Label();
		this.txtCardCode = new System.Windows.Forms.TextBox();
		this.labInTime = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.comboReloadType = new System.Windows.Forms.ComboBox();
		this.labReloadType = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		((System.ComponentModel.ISupportInitialize)this.valReload).BeginInit();
		this.panel1.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 30f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(593, 68);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "澳門通增值";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.valReload.Increment = new decimal(new int[4] { 50, 0, 0, 0 });
		this.valReload.Location = new System.Drawing.Point(286, 147);
		this.valReload.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.valReload.Minimum = new decimal(new int[4] { 50, 0, 0, 0 });
		this.valReload.Name = "valReload";
		this.valReload.Size = new System.Drawing.Size(221, 51);
		this.valReload.TabIndex = 4;
		this.valReload.Value = new decimal(new int[4] { 50, 0, 0, 0 });
		this.valReload.ValueChanged += new System.EventHandler(valReload_ValueChanged);
		this.comboValetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboValetType.FormattingEnabled = true;
		this.comboValetType.Location = new System.Drawing.Point(286, 359);
		this.comboValetType.Name = "comboValetType";
		this.comboValetType.Size = new System.Drawing.Size(221, 51);
		this.comboValetType.TabIndex = 3;
		this.comboValetType.SelectedIndexChanged += new System.EventHandler(comboValetType_SelectedIndexChanged);
		this.comboCashType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboCashType.FormattingEnabled = true;
		this.comboCashType.Items.AddRange(new object[3] { "MOP", "RMB", "HKB" });
		this.comboCashType.Location = new System.Drawing.Point(286, 288);
		this.comboCashType.Name = "comboCashType";
		this.comboCashType.Size = new System.Drawing.Size(221, 51);
		this.comboCashType.TabIndex = 3;
		this.comboCashType.SelectedIndexChanged += new System.EventHandler(comboCashType_SelectedIndexChanged);
		this.btnClose.ForeColor = System.Drawing.Color.Navy;
		this.btnClose.Location = new System.Drawing.Point(466, 12);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(111, 44);
		this.btnClose.TabIndex = 2;
		this.btnClose.Text = "取消";
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnOk.ForeColor = System.Drawing.Color.Navy;
		this.btnOk.Location = new System.Drawing.Point(317, 12);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(111, 44);
		this.btnOk.TabIndex = 2;
		this.btnOk.Text = "確認";
		this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOk.UseVisualStyleBackColor = true;
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.txtOragBalance.Location = new System.Drawing.Point(286, 78);
		this.txtOragBalance.Name = "txtOragBalance";
		this.txtOragBalance.ReadOnly = true;
		this.txtOragBalance.Size = new System.Drawing.Size(221, 51);
		this.txtOragBalance.TabIndex = 1;
		this.labChargeTime.ForeColor = System.Drawing.Color.Navy;
		this.labChargeTime.Location = new System.Drawing.Point(81, 495);
		this.labChargeTime.Name = "labChargeTime";
		this.labChargeTime.Size = new System.Drawing.Size(199, 51);
		this.labChargeTime.TabIndex = 0;
		this.labChargeTime.Text = "增值後餘額";
		this.labChargeTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtRealAmt.Location = new System.Drawing.Point(286, 427);
		this.txtRealAmt.Name = "txtRealAmt";
		this.txtRealAmt.ReadOnly = true;
		this.txtRealAmt.Size = new System.Drawing.Size(221, 51);
		this.txtRealAmt.TabIndex = 1;
		this.labelX4.ForeColor = System.Drawing.Color.Navy;
		this.labelX4.Location = new System.Drawing.Point(81, 427);
		this.labelX4.Name = "labelX4";
		this.labelX4.Size = new System.Drawing.Size(199, 51);
		this.labelX4.TabIndex = 0;
		this.labelX4.Text = "實際金額";
		this.labelX4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelX3.ForeColor = System.Drawing.Color.Navy;
		this.labelX3.Location = new System.Drawing.Point(81, 359);
		this.labelX3.Name = "labelX3";
		this.labelX3.Size = new System.Drawing.Size(199, 51);
		this.labelX3.TabIndex = 0;
		this.labelX3.Text = "增值幣種";
		this.labelX3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labelX2.ForeColor = System.Drawing.Color.Navy;
		this.labelX2.Location = new System.Drawing.Point(81, 287);
		this.labelX2.Name = "labelX2";
		this.labelX2.Size = new System.Drawing.Size(199, 51);
		this.labelX2.TabIndex = 0;
		this.labelX2.Text = "接收幣種";
		this.labelX2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labParkTime.ForeColor = System.Drawing.Color.Navy;
		this.labParkTime.Location = new System.Drawing.Point(81, 147);
		this.labParkTime.Name = "labParkTime";
		this.labParkTime.Size = new System.Drawing.Size(199, 51);
		this.labParkTime.TabIndex = 0;
		this.labParkTime.Text = "增值金額";
		this.labParkTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtBalance.Location = new System.Drawing.Point(286, 495);
		this.txtBalance.Name = "txtBalance";
		this.txtBalance.ReadOnly = true;
		this.txtBalance.Size = new System.Drawing.Size(221, 51);
		this.txtBalance.TabIndex = 1;
		this.labelX1.ForeColor = System.Drawing.Color.Navy;
		this.labelX1.Location = new System.Drawing.Point(81, 78);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(199, 51);
		this.labelX1.TabIndex = 0;
		this.labelX1.Text = "餘額";
		this.labelX1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtCardCode.Location = new System.Drawing.Point(286, 10);
		this.txtCardCode.Name = "txtCardCode";
		this.txtCardCode.ReadOnly = true;
		this.txtCardCode.Size = new System.Drawing.Size(221, 51);
		this.txtCardCode.TabIndex = 1;
		this.labInTime.ForeColor = System.Drawing.Color.Navy;
		this.labInTime.Location = new System.Drawing.Point(81, 10);
		this.labInTime.Name = "labInTime";
		this.labInTime.Size = new System.Drawing.Size(199, 51);
		this.labInTime.TabIndex = 0;
		this.labInTime.Text = "卡號";
		this.labInTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.panel3);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Controls.Add(this.labTitle);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(595, 700);
		this.panel1.TabIndex = 1;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel3.Controls.Add(this.comboReloadType);
		this.panel3.Controls.Add(this.labReloadType);
		this.panel3.Controls.Add(this.valReload);
		this.panel3.Controls.Add(this.txtCardCode);
		this.panel3.Controls.Add(this.comboValetType);
		this.panel3.Controls.Add(this.labInTime);
		this.panel3.Controls.Add(this.comboCashType);
		this.panel3.Controls.Add(this.labelX1);
		this.panel3.Controls.Add(this.txtOragBalance);
		this.panel3.Controls.Add(this.txtBalance);
		this.panel3.Controls.Add(this.labChargeTime);
		this.panel3.Controls.Add(this.labParkTime);
		this.panel3.Controls.Add(this.txtRealAmt);
		this.panel3.Controls.Add(this.labelX2);
		this.panel3.Controls.Add(this.labelX4);
		this.panel3.Controls.Add(this.labelX3);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Font = new System.Drawing.Font("微软雅黑", 25f);
		this.panel3.Location = new System.Drawing.Point(0, 68);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(593, 562);
		this.panel3.TabIndex = 4;
		this.comboReloadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboReloadType.FormattingEnabled = true;
		this.comboReloadType.Items.AddRange(new object[2] { "MPCARD", "MPAY" });
		this.comboReloadType.Location = new System.Drawing.Point(286, 218);
		this.comboReloadType.Name = "comboReloadType";
		this.comboReloadType.Size = new System.Drawing.Size(221, 51);
		this.comboReloadType.TabIndex = 6;
		this.labReloadType.ForeColor = System.Drawing.Color.Navy;
		this.labReloadType.Location = new System.Drawing.Point(81, 217);
		this.labReloadType.Name = "labReloadType";
		this.labReloadType.Size = new System.Drawing.Size(199, 51);
		this.labReloadType.TabIndex = 5;
		this.labReloadType.Text = "充值類型";
		this.labReloadType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.panel2.Controls.Add(this.btnClose);
		this.panel2.Controls.Add(this.btnOk);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 630);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(593, 68);
		this.panel2.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(14f, 31f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(595, 700);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
		base.Name = "FormMPassCharge";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormMPassCharge";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMPassCharge_FormClosing);
		((System.ComponentModel.ISupportInitialize)this.valReload).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
