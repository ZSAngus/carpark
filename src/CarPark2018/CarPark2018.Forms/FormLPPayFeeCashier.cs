using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.DB.AdditionalDataSource;
using CarPark.Device;
using CarPark.Lib;
using CarPark2018.LPPayForms;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using Newtonsoft.Json;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.Forms;

public class FormLPPayFeeCashier : Form
{
	public class AutoCloseMessageBox : IDisposable
	{
		private Timer _timer;

		private Form _form;

		private DialogResult _result;

		public AutoCloseMessageBox(string text, string caption, int timeoutMs, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			_form = new Form
			{
				Width = 500,
				Height = 250,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				StartPosition = FormStartPosition.CenterScreen,
				Text = caption,
				TopMost = true,
				Font = new Font("微软雅黑", 12f)
			};
			Label value = new Label
			{
				Text = text,
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleCenter,
				Font = new Font("微软雅黑", 20f, FontStyle.Bold),
				ForeColor = Color.DarkBlue,
				Padding = new Padding(20)
			};
			Button button = new Button
			{
				Text = "是",
				DialogResult = DialogResult.Yes,
				Font = new Font("微软雅黑", 16f),
				Size = new Size(120, 50),
				Margin = new Padding(10)
			};
			Button button2 = new Button
			{
				Text = "否",
				DialogResult = DialogResult.No,
				Font = new Font("微软雅黑", 16f),
				Size = new Size(120, 50),
				Margin = new Padding(10)
			};
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
			{
				Dock = DockStyle.Bottom,
				FlowDirection = FlowDirection.RightToLeft,
				Padding = new Padding(20),
				AutoSize = true
			};
			flowLayoutPanel.Controls.AddRange(new Control[2] { button2, button });
			_form.Controls.Add(value);
			_form.Controls.Add(flowLayoutPanel);
			_timer = new Timer
			{
				Interval = timeoutMs
			};
			_timer.Tick += delegate
			{
				_result = DialogResult.No;
				_form.Close();
			};
		}

		public DialogResult ShowDialog()
		{
			_timer.Start();
			_result = _form.ShowDialog();
			return _result;
		}

		public void Dispose()
		{
			_timer?.Dispose();
			_form?.Dispose();
		}
	}

	public CalcTicketFeeArgsV2 FeeArgs;

	public view_transactionandlp Transactionandlp;

	public bool IsImageActivate;

	private ChargeRecord m_ChargeRecord;

	private static ILog Logger;

	public string FreeImagePath;

	public string Remark = "";

	private MPass_POS_Transaction_Detail m_mpass;

	private BOC_Gate_TransactionExtend m_boc;

	private EnumParkType parkType;

	private bool Syn;

	private CalcTicketFeeReturnV2 m_Calcreturn;

	private IContainer components;

	private Panel panFill;

	private Label labTitle;

	private Panel panMiddle;

	private Label bindLP;

	private PictureBox picLP;

	private Label bindIntime;

	private Label bindFeetime;

	private Label bindParkmin;

	private Label bindFreemin;

	private Label btnParktype;

	private Label bindTotal;

	private Label btnDiscount;

	private Label btnLastFeetime;

	private TextBox txtLP;

	private Label txtIntime;

	private Label txtParkmin;

	private Label txtFeetime;

	private Label txtFreemin;

	private ComboBox comParktype;

	private Label txtTotal;

	private Label txtLastFeetime;

	private Panel panBottom;

	private Button btnOther;

	private Button btnCancel;

	private Button btnOK;

	private Button btnFixLP;

	private Label label2;

	private Button btnSave;

	private Button btnFixParktype;

	private Button btnSaveParktype;

	private Button btnFree;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem btnCancelFree;

	private Button btnSetFree;

	static FormLPPayFeeCashier()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public FormLPPayFeeCashier()
	{
		InitializeComponent();
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		bindIntime.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.bindIntime");
		bindFeetime.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.bindFeetime");
		bindParkmin.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.bindParkmin");
		bindFreemin.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.bindFreemin");
		btnParktype.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnParktype");
		bindTotal.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.bindTotal");
		btnDiscount.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnDiscount");
		btnLastFeetime.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnLastFeetime");
		bindLP.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.bindLP");
		btnSave.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnSave");
		btnFixParktype.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnFixParktype");
		btnSaveParktype.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnSaveParktype");
		btnFree.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnFree");
		btnCancelFree.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnCancelFree");
		btnOther.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnOther");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnCancel");
		btnOK.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnOK");
		btnFixLP.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnFixLP");
		labTitle.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.labTitle");
		btnCancelFree.Text = LangManager.GetLangString("CarPark.Forms.FormLPPayFeeCashier.btnCancelFree");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		Action<RequestArgs> action = EQM_Fee;
		RequestArgs obj = new RequestArgs
		{
			Extend1 = "FEE_CANCEL",
			Extend3 = Settings.Default.ServerEQM
		};
		action.BeginInvoke(obj, EndAsync, null);
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void btnFixLP_Click(object sender, EventArgs e)
	{
		txtLP.Enabled = true;
		btnSave.Visible = true;
		btnFixLP.Visible = false;
	}

	private void txtLP_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b')
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		txtLP.Enabled = false;
		btnSave.Visible = false;
		btnFixLP.Visible = true;
		try
		{
			if (string.IsNullOrWhiteSpace(txtLP.Text))
			{
				Global.ShowMessage(LangManager.GetLangString("LP_Input_NULL"));
				return;
			}
			CorrectLicensePlateArgs correctLicensePlateArgs = new CorrectLicensePlateArgs();
			correctLicensePlateArgs.TransactionDataID = Transactionandlp.TransactionID;
			correctLicensePlateArgs.NewLicensePlate = txtLP.Text.ToString();
			correctLicensePlateArgs.PayStationName = Settings.Default.OnlyID;
			correctLicensePlateArgs.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
			CorrectLicensePlateReturn correctLicensePlateReturn = LPDBHelper.CorrectLicensePlate(correctLicensePlateArgs);
			if (correctLicensePlateReturn.ISOK)
			{
				CalcTicketFeeArgsV2 feeArgs = FeeArgs;
				string ticketNumber = (m_ChargeRecord.CardCode = correctLicensePlateArgs.NewLicensePlate);
				feeArgs.TicketNumber = ticketNumber;
				Global.ShowMessage(LangManager.GetLangString("SaveSucceed"));
				Action<RequestArgs> action = EQM_Fee;
				List<string> value = new List<string>
				{
					JsonConvert.SerializeObject(m_ChargeRecord),
					JsonConvert.SerializeObject(m_Calcreturn)
				};
				RequestArgs obj = new RequestArgs
				{
					Extend1 = "FEE",
					Extend2 = JsonConvert.SerializeObject(value),
					Extend3 = Settings.Default.ServerEQM
				};
				action.BeginInvoke(obj, EndAsync, null);
			}
			else
			{
				Global.ShowMessage(LangManager.GetLangString(correctLicensePlateReturn.ErrCode));
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void btnFixParktype_Click(object sender, EventArgs e)
	{
		comParktype.Enabled = true;
		btnSaveParktype.Visible = true;
		btnFixParktype.Visible = false;
	}

	private void btnSaveParktype_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		comParktype.Enabled = false;
		btnSaveParktype.Visible = false;
		btnFixParktype.Visible = true;
		CorrectParkTypeArgs correctParkTypeArgs = new CorrectParkTypeArgs();
		correctParkTypeArgs.ParkTypeIDCorrect = (int)comParktype.SelectedValue;
		correctParkTypeArgs.TransactionID = Transactionandlp.TransactionID;
		Logger.Info($"CorrectParkType Begin ParkTypeIDCorrect:{correctParkTypeArgs.ParkTypeIDCorrect},TransactionID:{correctParkTypeArgs.TransactionID}");
		CorrectParkTypeReturn correctParkTypeReturn = LPDBHelper.CorrectParkType(correctParkTypeArgs);
		if (correctParkTypeReturn.IsOK)
		{
			Logger.Info($"CorrectParkType SaveSucceed");
			Global.ShowMessage(LangManager.GetLangString("SaveSucceed"));
			CalcAmount();
		}
		else
		{
			Logger.Info($"CorrectParkType ErrCode:{correctParkTypeReturn.ErrCode}");
			Global.ShowMessage(LangManager.GetLangString(correctParkTypeReturn.ErrCode));
		}
	}

	private void CalcAmount()
	{
		try
		{
			ChargeContext chargeContext = new ChargeContext();
			CalcTicketFeeReturnV2 calcTicketFeeReturnV = chargeContext.CommunicationChannel.CalcTicketFeeV2(FeeArgs, EnumParkType.None, EnumBillType.TimeChargeLicensePlate, out m_ChargeRecord);
			chargeContext.CommunicationChannel.Disconnect();
			if (calcTicketFeeReturnV.ISValid)
			{
				FeeArgs.InTime = calcTicketFeeReturnV.InTime;
				txtLP.Text = m_ChargeRecord.CardCode;
				txtIntime.Text = calcTicketFeeReturnV.InTime.ToString(SystemParm.LongTimeFormat);
				txtFeetime.Text = m_ChargeRecord.ChargeTime.ToString(SystemParm.LongTimeFormat);
				txtParkmin.Text = $"{m_ChargeRecord.ParkMin / 60} H {m_ChargeRecord.ParkMin % 60} M";
				txtFreemin.Text = $"{m_ChargeRecord.FreeMin.ToString()} M";
				txtTotal.Text = m_ChargeRecord.TotalCharge.ToString("f2");
				comParktype.SelectedValue = m_ChargeRecord.ParkTypeID;
				string text = string.Empty;
				Logger.Info("Transactionandlp.ImagePath" + Transactionandlp.ImagePath + ",Transactionandlp.TransactionID" + Transactionandlp.TransactionID + ",calcReturn.InLicensePlatePath" + calcTicketFeeReturnV.InLicensePlatePath);
				if (!string.IsNullOrEmpty(Transactionandlp.ImagePath))
				{
					text = Transactionandlp.ImagePath;
				}
				else if (Transactionandlp.TransactionID != 0)
				{
					try
					{
						text = LPDBHelper.GetView_TransactionAndLP_IMAGE(Transactionandlp.TransactionID);
					}
					catch (Exception ex)
					{
						Logger.Error(ex.Message ?? "");
					}
				}
				else if (!string.IsNullOrEmpty(calcTicketFeeReturnV.InLicensePlatePath))
				{
					text = calcTicketFeeReturnV.InLicensePlatePath;
				}
				Logger.Info("TimagePath" + text + ",IsImageActivate" + IsImageActivate);
				if (IsImageActivate && !string.IsNullOrEmpty(text))
				{
					try
					{
						picLP.Image = Image.FromFile(Config.LicensePlatePath + text);
					}
					catch (Exception)
					{
						picLP.Image = ImageManager.GetImage("", "cancel");
					}
				}
				else
				{
					picLP.Image = ImageManager.GetImage("", "cancel");
				}
				if (calcTicketFeeReturnV.HasLastTimeCharge)
				{
					btnLastFeetime.Visible = true;
					txtLastFeetime.Text = calcTicketFeeReturnV.LastTimeCharge.ToString(SystemParm.LongTimeFormat);
					if (!calcTicketFeeReturnV.ISTimeOut)
					{
						Global.ShowMessage("未超時,無需收費");
						base.DialogResult = DialogResult.Cancel;
						Close();
						return;
					}
				}
				m_Calcreturn = calcTicketFeeReturnV;
				FormFee.Self().SetTicket(txtIntime.Text, txtFeetime.Text, $"{m_ChargeRecord.ParkMin / 60}小時{m_ChargeRecord.ParkMin % 60}分", txtTotal.Text);
				Action<RequestArgs> action = EQM_Fee;
				List<string> value = new List<string>
				{
					JsonConvert.SerializeObject(m_ChargeRecord),
					JsonConvert.SerializeObject(calcTicketFeeReturnV)
				};
				RequestArgs obj = new RequestArgs
				{
					Extend1 = "FEE",
					Extend2 = JsonConvert.SerializeObject(value),
					Extend3 = Settings.Default.ServerEQM
				};
				action.BeginInvoke(obj, EndAsync, null);
				btnCancel.Focus();
			}
			else
			{
				Logger.Info($"CalcTicketFeeV2 Err:{calcTicketFeeReturnV.ErrCode}");
				HideAllControls();
				FormSetFree formSetFree = new FormSetFree();
				formSetFree.LicensePlate = FeeArgs.TicketNumber;
				formSetFree.TopMost = true;
				formSetFree.ShowDialog();
				base.DialogResult = DialogResult.Cancel;
				Close();
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void FormLPPayFeeCashier_Shown(object sender, EventArgs e)
	{
		try
		{
			BindingHelper.BindComboBox<EnumParkTypeSource>(comParktype);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
		CalcAmount();
		btnCancel.Focus();
	}

	private void btnFree_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		using FormTimeChargeFree formTimeChargeFree = new FormTimeChargeFree();
		if (formTimeChargeFree.ShowDialog() == DialogResult.OK)
		{
			FeeArgs.CustomFreeID = formTimeChargeFree.m_CustomFreeType.CustomFreeTypeID;
			FeeArgs.CustomFreeTenatID = formTimeChargeFree.m_CustomFreeTenat.TenatID;
			FreeImagePath = formTimeChargeFree.FreeImagePath;
			Remark = formTimeChargeFree.Remark;
			CalcAmount();
		}
	}

	private void btnCancelFree_Click(object sender, EventArgs e)
	{
		FeeArgs.CustomFreeTenatID = 0;
		FeeArgs.CustomFreeID = 0;
		FreeImagePath = null;
		Remark = "";
		LPDBHelper.DelFreeRecord(Transactionandlp.TransactionID);
		CalcAmount();
		btnCancel.Focus();
	}

	private void SaveChargeRecord(ChargeRecord charge, MPass_POS_Transaction_Detail mpass, BOC_Gate_TransactionExtend boc, BOC_N910_POS_Card_Payment_DetailEX bocn910, ScanPayment scanPayment)
	{
		try
		{
			Logger.Debug("Start FormLPPayFeeCashier SaveChargeRecord");
			m_mpass = mpass;
			m_boc = boc;
			SaveChargeRecordArgs saveChargeRecordArgs = new SaveChargeRecordArgs();
			saveChargeRecordArgs.CustomFreeID = FeeArgs.CustomFreeID;
			saveChargeRecordArgs.CustomFreeTenatID = FeeArgs.CustomFreeTenatID;
			saveChargeRecordArgs.InTime = FeeArgs.InTime;
			saveChargeRecordArgs.TicketNumber = FeeArgs.TicketNumber;
			saveChargeRecordArgs.FreeImagePath = FreeImagePath;
			saveChargeRecordArgs.CustomFreeRecordRemark = Remark;
			saveChargeRecordArgs.BarCode = "";
			SaveChargeRecordReturn saveChargeRecordReturn = null;
			ChargeRecord chargeRecord = null;
			MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = null;
			BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend = null;
			BOC_N910_POS_Card_Payment_DetailEX bOC_N910_POS_Card_Payment_DetailEX = null;
			ScanPayment scanPayment2 = null;
			try
			{
				if (mpass != null)
				{
					if (mpass.PAY_MODE == "mpay")
					{
						charge.subPayType = 2;
					}
					else
					{
						charge.subPayType = 1;
					}
				}
				else if (bocn910 != null)
				{
					charge.subPayType = 8;
				}
				else if (scanPayment != null)
				{
					charge.subPayType = Convert.ToInt32(scanPayment.PayType);
				}
			}
			catch (Exception arg)
			{
				Logger.Error($"[subPayType]{arg}");
			}
			try
			{
				ChargeContext chargeContext = new ChargeContext();
				Logger.Debug("Start FormLPPayFeeCashier SaveChargeRecord SaveElectronicChargeRecord");
				saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveChargeRecordArgs, (EnumParkType)m_ChargeRecord.ParkTypeID, charge, mpass, boc, bocn910, scanPayment);
				chargeContext.CommunicationChannel.Disconnect();
				Logger.Debug("End FormLPPayFeeCashier SaveChargeRecord SaveElectronicChargeRecord");
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				if (mpass != null || bocn910 != null || scanPayment != null)
				{
					DBHelper.Insert(charge.CardCode, charge, mpass, boc, saveChargeRecordArgs, bocn910, scanPayment);
					chargeRecord = DBHelper.SelectChargeRecord(charge.CardCode);
					if (mpass != null)
					{
						mPass_POS_Transaction_Detail = DBHelper.SelectMPass_POS_Transaction_Detail(charge.TimeChargeID);
					}
					else if (bocn910 != null)
					{
						bOC_N910_POS_Card_Payment_DetailEX = DBHelper.SelectBOC_N910_POS_Card_Payment_DetailEX(charge.TimeChargeID);
					}
					else if (scanPayment != null)
					{
						scanPayment2 = DBHelper.SelectScan_Payment_DetailEX(charge.TimeChargeID);
					}
					saveChargeRecordReturn = new SaveChargeRecordReturn();
					saveChargeRecordReturn.ISOK = true;
					Syn = true;
				}
				else
				{
					Logger.Error(ex);
					btnCancel_Click(null, null);
					Global.ShowMessage(ex.Message);
				}
			}
			if (saveChargeRecordReturn.ISOK)
			{
				if (m_ChargeRecord.TotalCharge != 0m && mPass_POS_Transaction_Detail == null && bOC_Gate_TransactionExtend == null)
				{
					try
					{
						DeviceManager.FeeCenterModule.OpenCash();
					}
					catch (Exception message)
					{
						Logger.Error(message);
					}
				}
				if (Syn)
				{
					Global.ShowMessage("有一條數據沒有同步，現在馬上同步");
					try
					{
						ChargeContext chargeContext2 = new ChargeContext();
						Logger.Debug("Start FormLPPayFeeCashier SaveChargeRecord SaveElectronicChargeRecord Offline");
						if (mPass_POS_Transaction_Detail != null)
						{
							saveChargeRecordReturn = chargeContext2.CommunicationChannel.SaveElectronicChargeRecord(saveChargeRecordArgs, parkType, chargeRecord, mPass_POS_Transaction_Detail, null);
							chargeContext2.CommunicationChannel.Disconnect();
						}
						else
						{
							saveChargeRecordReturn = chargeContext2.CommunicationChannel.SaveElectronicChargeRecord(saveChargeRecordArgs, parkType, chargeRecord, null, bOC_Gate_TransactionExtend, bOC_N910_POS_Card_Payment_DetailEX, scanPayment2);
							chargeContext2.CommunicationChannel.Disconnect();
						}
						Logger.Debug("End FormLPPayFeeCashier SaveChargeRecord SaveElectronicChargeRecord Offline");
						if (!saveChargeRecordReturn.ISOK)
						{
							Global.ShowMessage("同步失敗，請聯繫技術人員");
						}
						else
						{
							DBHelper.ExecuteNonQuery($"update ChargeRecord set isupload='1' where timechargeid={chargeRecord.TimeChargeID}", CommandType.Text, (IDbDataParameter[])null);
						}
					}
					catch (Exception message2)
					{
						Logger.Error(message2);
						Global.ShowMessage("同步失敗，請聯繫技術人員");
					}
				}
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				Global.ShowMessage(LangManager.GetLangString(saveChargeRecordReturn.ErrCode));
			}
		}
		catch (TimeoutException message3)
		{
			Logger.Error(message3);
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception message4)
		{
			Logger.Error(message4);
			Global.ShowMessage("收費失敗，請聯繫技術人員");
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			if (m_Calcreturn != null && m_Calcreturn.HasLastTimeCharge)
			{
				m_ChargeRecord.BillType = 5;
			}
			else
			{
				m_ChargeRecord.BillType = 17;
			}
			m_ChargeRecord.PayType = 0;
			SaveChargeRecord(m_ChargeRecord, null, null, null, null);
			Action<RequestArgs> action = EQM_Fee;
			RequestArgs obj = new RequestArgs
			{
				Extend1 = "FEE_FINISHED",
				Extend3 = Settings.Default.ServerEQM
			};
			action.BeginInvoke(obj, EndAsync, null);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void btnOther_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		using FormEpaySale formEpaySale = new FormEpaySale
		{
			ChargeRecord = m_ChargeRecord
		};
		if (formEpaySale.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		try
		{
			if (m_Calcreturn != null && m_Calcreturn.HasLastTimeCharge)
			{
				formEpaySale.ChargeRecord.BillType = 5;
			}
			else
			{
				formEpaySale.ChargeRecord.BillType = 17;
			}
			formEpaySale.ChargeRecord.CardCode = FeeArgs.TicketNumber;
			formEpaySale.ChargeRecord.PayType = (int)formEpaySale.PayTypeFlag;
			SaveChargeRecord(formEpaySale.ChargeRecord, formEpaySale.MPass, formEpaySale.BOC, formEpaySale.BOC_N910, formEpaySale.SPay);
			Action<RequestArgs> action = EQM_Fee;
			RequestArgs obj = new RequestArgs
			{
				Extend1 = "FEE_FINISHED",
				Extend3 = Settings.Default.ServerEQM
			};
			action.BeginInvoke(obj, EndAsync, null);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public void EQM_Fee(RequestArgs args)
	{
		try
		{
			Common._Carpark2018ServiceContext.CommunicationChannel.ExtendRequestInterface(args);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private static void EndAsync(IAsyncResult ar)
	{
		try
		{
			((Action<int>)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
		}
		catch (Exception)
		{
		}
	}

	private void FormLPPayFeeCashier_FormClosing(object sender, FormClosingEventArgs e)
	{
		FormFee.Self2();
	}

	private void btnSetFree_Click(object sender, EventArgs e)
	{
		try
		{
			if (FeeArgs.CustomFreeTenatID != 0 && FeeArgs.CustomFreeID != 0 && LPDBHelper.SetFreeRecord(FeeArgs.CustomFreeTenatID, FeeArgs.CustomFreeID, Transactionandlp.TransactionID))
			{
				Action<RequestArgs> action = EQM_Fee;
				RequestArgs obj = new RequestArgs
				{
					Extend1 = "FEE_CANCEL",
					Extend3 = Settings.Default.ServerEQM
				};
				action.BeginInvoke(obj, EndAsync, null);
				Global.ShowMessage("優惠成功");
				Close();
			}
			else
			{
				Global.ShowMessage("優惠失敗，請重新操作");
			}
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
		this.components = new System.ComponentModel.Container();
		this.panFill = new System.Windows.Forms.Panel();
		this.panBottom = new System.Windows.Forms.Panel();
		this.btnSetFree = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnOther = new System.Windows.Forms.Button();
		this.panMiddle = new System.Windows.Forms.Panel();
		this.btnFree = new System.Windows.Forms.Button();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.btnCancelFree = new System.Windows.Forms.ToolStripMenuItem();
		this.label2 = new System.Windows.Forms.Label();
		this.txtTotal = new System.Windows.Forms.Label();
		this.txtLastFeetime = new System.Windows.Forms.Label();
		this.comParktype = new System.Windows.Forms.ComboBox();
		this.txtFreemin = new System.Windows.Forms.Label();
		this.txtParkmin = new System.Windows.Forms.Label();
		this.txtFeetime = new System.Windows.Forms.Label();
		this.txtIntime = new System.Windows.Forms.Label();
		this.txtLP = new System.Windows.Forms.TextBox();
		this.btnDiscount = new System.Windows.Forms.Label();
		this.btnLastFeetime = new System.Windows.Forms.Label();
		this.bindTotal = new System.Windows.Forms.Label();
		this.btnParktype = new System.Windows.Forms.Label();
		this.bindFreemin = new System.Windows.Forms.Label();
		this.bindParkmin = new System.Windows.Forms.Label();
		this.bindFeetime = new System.Windows.Forms.Label();
		this.bindIntime = new System.Windows.Forms.Label();
		this.bindLP = new System.Windows.Forms.Label();
		this.picLP = new System.Windows.Forms.PictureBox();
		this.btnSave = new System.Windows.Forms.Button();
		this.btnSaveParktype = new System.Windows.Forms.Button();
		this.btnFixLP = new System.Windows.Forms.Button();
		this.btnFixParktype = new System.Windows.Forms.Button();
		this.labTitle = new System.Windows.Forms.Label();
		this.panFill.SuspendLayout();
		this.panBottom.SuspendLayout();
		this.panMiddle.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLP).BeginInit();
		base.SuspendLayout();
		this.panFill.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.panFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panFill.Controls.Add(this.panBottom);
		this.panFill.Controls.Add(this.panMiddle);
		this.panFill.Controls.Add(this.labTitle);
		this.panFill.Location = new System.Drawing.Point(0, 0);
		this.panFill.Name = "panFill";
		this.panFill.Size = new System.Drawing.Size(1200, 720);
		this.panFill.TabIndex = 0;
		this.panBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panBottom.Controls.Add(this.btnSetFree);
		this.panBottom.Controls.Add(this.btnCancel);
		this.panBottom.Controls.Add(this.btnOK);
		this.panBottom.Controls.Add(this.btnOther);
		this.panBottom.Location = new System.Drawing.Point(0, 648);
		this.panBottom.Name = "panBottom";
		this.panBottom.Size = new System.Drawing.Size(1200, 70);
		this.panBottom.TabIndex = 2;
		this.btnSetFree.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnSetFree.ForeColor = System.Drawing.Color.Navy;
		this.btnSetFree.Location = new System.Drawing.Point(613, 10);
		this.btnSetFree.Name = "btnSetFree";
		this.btnSetFree.Size = new System.Drawing.Size(120, 48);
		this.btnSetFree.TabIndex = 7;
		this.btnSetFree.Text = "優惠";
		this.btnSetFree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSetFree.UseVisualStyleBackColor = true;
		this.btnSetFree.Click += new System.EventHandler(btnSetFree_Click);
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(1044, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(120, 48);
		this.btnCancel.TabIndex = 6;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.btnOK.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnOK.ForeColor = System.Drawing.Color.Navy;
		this.btnOK.Location = new System.Drawing.Point(899, 11);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(120, 48);
		this.btnOK.TabIndex = 5;
		this.btnOK.Text = "確定";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnOther.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnOther.ForeColor = System.Drawing.Color.Navy;
		this.btnOther.Location = new System.Drawing.Point(756, 11);
		this.btnOther.Name = "btnOther";
		this.btnOther.Size = new System.Drawing.Size(120, 48);
		this.btnOther.TabIndex = 4;
		this.btnOther.Text = "其他";
		this.btnOther.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOther.UseVisualStyleBackColor = true;
		this.btnOther.Click += new System.EventHandler(btnOther_Click);
		this.panMiddle.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panMiddle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panMiddle.Controls.Add(this.btnFree);
		this.panMiddle.Controls.Add(this.label2);
		this.panMiddle.Controls.Add(this.txtTotal);
		this.panMiddle.Controls.Add(this.txtLastFeetime);
		this.panMiddle.Controls.Add(this.comParktype);
		this.panMiddle.Controls.Add(this.txtFreemin);
		this.panMiddle.Controls.Add(this.txtParkmin);
		this.panMiddle.Controls.Add(this.txtFeetime);
		this.panMiddle.Controls.Add(this.txtIntime);
		this.panMiddle.Controls.Add(this.txtLP);
		this.panMiddle.Controls.Add(this.btnDiscount);
		this.panMiddle.Controls.Add(this.btnLastFeetime);
		this.panMiddle.Controls.Add(this.bindTotal);
		this.panMiddle.Controls.Add(this.btnParktype);
		this.panMiddle.Controls.Add(this.bindFreemin);
		this.panMiddle.Controls.Add(this.bindParkmin);
		this.panMiddle.Controls.Add(this.bindFeetime);
		this.panMiddle.Controls.Add(this.bindIntime);
		this.panMiddle.Controls.Add(this.bindLP);
		this.panMiddle.Controls.Add(this.picLP);
		this.panMiddle.Controls.Add(this.btnSave);
		this.panMiddle.Controls.Add(this.btnSaveParktype);
		this.panMiddle.Controls.Add(this.btnFixLP);
		this.panMiddle.Controls.Add(this.btnFixParktype);
		this.panMiddle.Location = new System.Drawing.Point(0, 60);
		this.panMiddle.Name = "panMiddle";
		this.panMiddle.Size = new System.Drawing.Size(1200, 594);
		this.panMiddle.TabIndex = 1;
		this.btnFree.ContextMenuStrip = this.contextMenuStrip1;
		this.btnFree.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnFree.Location = new System.Drawing.Point(1034, 249);
		this.btnFree.Name = "btnFree";
		this.btnFree.Size = new System.Drawing.Size(129, 43);
		this.btnFree.TabIndex = 23;
		this.btnFree.Text = "免費";
		this.btnFree.UseVisualStyleBackColor = true;
		this.btnFree.Click += new System.EventHandler(btnFree_Click);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.btnCancelFree });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(160, 36);
		this.btnCancelFree.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.btnCancelFree.Name = "btnCancelFree";
		this.btnCancelFree.Size = new System.Drawing.Size(159, 32);
		this.btnCancelFree.Text = "取消免費";
		this.btnCancelFree.Click += new System.EventHandler(btnCancelFree_Click);
		this.label2.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.label2.ForeColor = System.Drawing.Color.Black;
		this.label2.Location = new System.Drawing.Point(183, 371);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(336, 50);
		this.label2.TabIndex = 19;
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtTotal.Font = new System.Drawing.Font("微软雅黑", 35f);
		this.txtTotal.ForeColor = System.Drawing.Color.Red;
		this.txtTotal.Location = new System.Drawing.Point(721, 436);
		this.txtTotal.Name = "txtTotal";
		this.txtTotal.Size = new System.Drawing.Size(276, 100);
		this.txtTotal.TabIndex = 17;
		this.txtTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtLastFeetime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.txtLastFeetime.ForeColor = System.Drawing.Color.Black;
		this.txtLastFeetime.Location = new System.Drawing.Point(721, 371);
		this.txtLastFeetime.Name = "txtLastFeetime";
		this.txtLastFeetime.Size = new System.Drawing.Size(450, 50);
		this.txtLastFeetime.TabIndex = 16;
		this.txtLastFeetime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.comParktype.Enabled = false;
		this.comParktype.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.comParktype.FormattingEnabled = true;
		this.comParktype.Location = new System.Drawing.Point(721, 313);
		this.comParktype.Name = "comParktype";
		this.comParktype.Size = new System.Drawing.Size(276, 43);
		this.comParktype.TabIndex = 15;
		this.txtFreemin.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.txtFreemin.ForeColor = System.Drawing.Color.Black;
		this.txtFreemin.Location = new System.Drawing.Point(721, 245);
		this.txtFreemin.Name = "txtFreemin";
		this.txtFreemin.Size = new System.Drawing.Size(276, 50);
		this.txtFreemin.TabIndex = 14;
		this.txtFreemin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtParkmin.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.txtParkmin.ForeColor = System.Drawing.Color.Black;
		this.txtParkmin.Location = new System.Drawing.Point(721, 183);
		this.txtParkmin.Name = "txtParkmin";
		this.txtParkmin.Size = new System.Drawing.Size(276, 50);
		this.txtParkmin.TabIndex = 13;
		this.txtParkmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtFeetime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.txtFeetime.ForeColor = System.Drawing.Color.Black;
		this.txtFeetime.Location = new System.Drawing.Point(721, 121);
		this.txtFeetime.Name = "txtFeetime";
		this.txtFeetime.Size = new System.Drawing.Size(450, 50);
		this.txtFeetime.TabIndex = 12;
		this.txtFeetime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtIntime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.txtIntime.ForeColor = System.Drawing.Color.Black;
		this.txtIntime.Location = new System.Drawing.Point(721, 62);
		this.txtIntime.Name = "txtIntime";
		this.txtIntime.Size = new System.Drawing.Size(450, 50);
		this.txtIntime.TabIndex = 11;
		this.txtIntime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtLP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtLP.Enabled = false;
		this.txtLP.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.txtLP.Location = new System.Drawing.Point(721, 10);
		this.txtLP.MaxLength = 10;
		this.txtLP.Name = "txtLP";
		this.txtLP.Size = new System.Drawing.Size(276, 43);
		this.txtLP.TabIndex = 10;
		this.txtLP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtLP_KeyPress);
		this.btnDiscount.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnDiscount.ForeColor = System.Drawing.Color.Black;
		this.btnDiscount.Location = new System.Drawing.Point(9, 371);
		this.btnDiscount.Name = "btnDiscount";
		this.btnDiscount.Size = new System.Drawing.Size(150, 50);
		this.btnDiscount.TabIndex = 9;
		this.btnDiscount.Text = "優惠券";
		this.btnDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnDiscount.Visible = false;
		this.btnLastFeetime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnLastFeetime.ForeColor = System.Drawing.Color.Black;
		this.btnLastFeetime.Location = new System.Drawing.Point(535, 371);
		this.btnLastFeetime.Name = "btnLastFeetime";
		this.btnLastFeetime.Size = new System.Drawing.Size(150, 50);
		this.btnLastFeetime.TabIndex = 8;
		this.btnLastFeetime.Text = "上次收費";
		this.btnLastFeetime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnLastFeetime.Visible = false;
		this.bindTotal.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindTotal.ForeColor = System.Drawing.Color.Black;
		this.bindTotal.Location = new System.Drawing.Point(535, 457);
		this.bindTotal.Name = "bindTotal";
		this.bindTotal.Size = new System.Drawing.Size(150, 50);
		this.bindTotal.TabIndex = 7;
		this.bindTotal.Text = "金額";
		this.bindTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnParktype.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnParktype.ForeColor = System.Drawing.Color.Black;
		this.btnParktype.Location = new System.Drawing.Point(535, 308);
		this.btnParktype.Name = "btnParktype";
		this.btnParktype.Size = new System.Drawing.Size(150, 50);
		this.btnParktype.TabIndex = 6;
		this.btnParktype.Text = "車型";
		this.btnParktype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindFreemin.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindFreemin.ForeColor = System.Drawing.Color.Black;
		this.bindFreemin.Location = new System.Drawing.Point(535, 245);
		this.bindFreemin.Name = "bindFreemin";
		this.bindFreemin.Size = new System.Drawing.Size(150, 50);
		this.bindFreemin.TabIndex = 5;
		this.bindFreemin.Text = "免費時間";
		this.bindFreemin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindParkmin.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindParkmin.ForeColor = System.Drawing.Color.Black;
		this.bindParkmin.Location = new System.Drawing.Point(535, 183);
		this.bindParkmin.Name = "bindParkmin";
		this.bindParkmin.Size = new System.Drawing.Size(150, 50);
		this.bindParkmin.TabIndex = 4;
		this.bindParkmin.Text = "泊車時間";
		this.bindParkmin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindFeetime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindFeetime.ForeColor = System.Drawing.Color.Black;
		this.bindFeetime.Location = new System.Drawing.Point(535, 121);
		this.bindFeetime.Name = "bindFeetime";
		this.bindFeetime.Size = new System.Drawing.Size(150, 50);
		this.bindFeetime.TabIndex = 3;
		this.bindFeetime.Text = "收費時間";
		this.bindFeetime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindIntime.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindIntime.ForeColor = System.Drawing.Color.Black;
		this.bindIntime.Location = new System.Drawing.Point(535, 62);
		this.bindIntime.Name = "bindIntime";
		this.bindIntime.Size = new System.Drawing.Size(150, 50);
		this.bindIntime.TabIndex = 2;
		this.bindIntime.Text = "入場時間";
		this.bindIntime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.bindLP.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.bindLP.ForeColor = System.Drawing.Color.Red;
		this.bindLP.Location = new System.Drawing.Point(535, 3);
		this.bindLP.Name = "bindLP";
		this.bindLP.Size = new System.Drawing.Size(150, 50);
		this.bindLP.TabIndex = 1;
		this.bindLP.Text = "車牌號碼";
		this.bindLP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.picLP.BackColor = System.Drawing.Color.Black;
		this.picLP.Location = new System.Drawing.Point(1, 3);
		this.picLP.Name = "picLP";
		this.picLP.Size = new System.Drawing.Size(518, 331);
		this.picLP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.picLP.TabIndex = 0;
		this.picLP.TabStop = false;
		this.btnSave.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnSave.Location = new System.Drawing.Point(1034, 9);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(129, 43);
		this.btnSave.TabIndex = 20;
		this.btnSave.Text = "保存";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Visible = false;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.btnSaveParktype.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnSaveParktype.Location = new System.Drawing.Point(1034, 313);
		this.btnSaveParktype.Name = "btnSaveParktype";
		this.btnSaveParktype.Size = new System.Drawing.Size(129, 43);
		this.btnSaveParktype.TabIndex = 21;
		this.btnSaveParktype.Text = "保存";
		this.btnSaveParktype.UseVisualStyleBackColor = true;
		this.btnSaveParktype.Visible = false;
		this.btnSaveParktype.Click += new System.EventHandler(btnSaveParktype_Click);
		this.btnFixLP.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnFixLP.Location = new System.Drawing.Point(1034, 10);
		this.btnFixLP.Name = "btnFixLP";
		this.btnFixLP.Size = new System.Drawing.Size(129, 43);
		this.btnFixLP.TabIndex = 18;
		this.btnFixLP.Text = "糾正";
		this.btnFixLP.UseVisualStyleBackColor = true;
		this.btnFixLP.Click += new System.EventHandler(btnFixLP_Click);
		this.btnFixParktype.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.btnFixParktype.Location = new System.Drawing.Point(1034, 312);
		this.btnFixParktype.Name = "btnFixParktype";
		this.btnFixParktype.Size = new System.Drawing.Size(129, 43);
		this.btnFixParktype.TabIndex = 22;
		this.btnFixParktype.Text = "糾正";
		this.btnFixParktype.UseVisualStyleBackColor = true;
		this.btnFixParktype.Click += new System.EventHandler(btnFixParktype_Click);
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(1200, 60);
		this.labTitle.TabIndex = 0;
		this.labTitle.Text = "車牌收費";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1200, 720);
		base.Controls.Add(this.panFill);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormLPPayFeeCashier";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormLPPayFeeCashier";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormLPPayFeeCashier_FormClosing);
		base.Shown += new System.EventHandler(FormLPPayFeeCashier_Shown);
		this.panFill.ResumeLayout(false);
		this.panBottom.ResumeLayout(false);
		this.panMiddle.ResumeLayout(false);
		this.panMiddle.PerformLayout();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.picLP).EndInit();
		base.ResumeLayout(false);
	}

	private void HideAllControls()
	{
		foreach (Control control in base.Controls)
		{
			control.Visible = false;
		}
	}

	private void ManualOpenGate()
	{
		try
		{
			ManualUpBarArgs manualUpBarArgs = new ManualUpBarArgs(Settings.Default.OnlyID)
			{
				GateID = 2,
				OperationPC = Settings.Default.OnlyID,
				ShiffCode = DataBuffer2018.CurrentStaff.StaffCode,
				Extend1 = (Transactionandlp.AnalysisResult?.ToUpper().Trim() ?? "未输入車牌")
			};
			Common._Carpark2018ServiceContext.CommunicationChannel.ManualUpBar(manualUpBarArgs);
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2);
			Global.ShowMessage(ex2.Message);
		}
	}
}
