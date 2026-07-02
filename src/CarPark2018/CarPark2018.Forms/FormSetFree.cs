using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.DB.AdditionalDataSource;
using CarPark.Device;
using CarPark.Lib;
using CarPark2018.Properties;
using MacauPass.POSCom.Package;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.Forms;

public class FormSetFree : Form
{
	private readonly DateTime initTime;

	private ILog Logger;

	private ChargeRecord m_ChargeRecord;

	private TransactionData m_TransactionData;

	private SaveChargeRecordArgs saveArg = new SaveChargeRecordArgs();

	private CalcTicketFeeArgs feeArg = new CalcTicketFeeArgs();

	private EnumParkType parkType;

	public string FreeImagePath;

	public string Remark = "";

	private bool IsCharge;

	private FormTimeChargeTimeOut form;

	private bool Syn;

	private List<view_transactionandlp> list;

	private view_transactionandlp view;

	private ChargeContext chargeContext = new ChargeContext();

	private string Linceseplate = "";

	private IContainer components;

	private Label labTitle;

	private Panel panel1;

	private Button btnOK;

	private Button btnCancel;

	private Panel panel2;

	private Button btnFree;

	private TextBox txtTotalCharge;

	private ComboBox comboParkType;

	private TextBox txtFree;

	private TextBox txtParkMin;

	private TextBox txtParkHour;

	private TextBox txtInTime;

	private TextBox txtTicketNo;

	private Label labTotalCharge;

	private Label labParkType;

	private Label labFreeTime;

	private Label labTimeSplit;

	private Label labParkTime;

	private Label labLastTime;

	private Label labTicketNo;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem btnCancelFree;

	private Panel panFill;

	private ComboBox comboParkArea;

	private Label labArea;

	private RadioButton rbMPASS;

	private Label label1;

	private RadioButton rbAirPay;

	private Button btnRead;

	private Label label2;

	private Button btnTransferTicket;

	private string initialLicensePlate;

	public string LicensePlate { get; set; }

	public FormSetFree()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		initTime = DateTime.Now;
		m_ChargeRecord = null;
		m_TransactionData = null;
		InitializeComponent();
		base.Load += FormSetFree_Load;
		try
		{
			BindingHelper.BindComboBox<EnumParkTypeSource>(comboParkType);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		rbMPASS.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.rbMPASS");
		rbAirPay.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.rbAirPay2");
		labArea.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.labArea");
		label1.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.label1");
		labFreeTime.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.labFreeTime");
		labLastTime.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.labLastTime");
		labParkTime.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.labParkTime");
		labParkType.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.labParkType");
		labTicketNo.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.labTicketNo2");
		label1.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.label3");
		labTotalCharge.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.labTotalCharge");
		btnCancel.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.btnCancel");
		btnFree.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.btnFree");
		btnOK.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.btnOK");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void btnFree_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		using FormTimeChargeFree formTimeChargeFree = new FormTimeChargeFree();
		if (formTimeChargeFree.ShowDialog() == DialogResult.OK)
		{
			feeArg.CustomFreeID = formTimeChargeFree.m_CustomFreeType.CustomFreeTypeID;
			feeArg.CustomFreeTenatID = formTimeChargeFree.m_CustomFreeTenat.TenatID;
			FreeImagePath = formTimeChargeFree.FreeImagePath;
			Remark = formTimeChargeFree.Remark;
			CalcAmount();
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			if (feeArg.CustomFreeTenatID != 0 && feeArg.CustomFreeID != 0 && m_TransactionData != null && LPDBHelper.SetFreeRecord(feeArg.CustomFreeTenatID, feeArg.CustomFreeID, m_TransactionData.TransactionID))
			{
				Logger.Debug("SetFreeRecord ID:" + m_TransactionData.TransactionID + ",CustomFreeTenatID:" + feeArg.CustomFreeTenatID + ",CustomFreeID:" + feeArg.CustomFreeID);
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

	private void CalcAmount()
	{
		try
		{
			if (m_TransactionData != null)
			{
				feeArg.TicketNumber = txtTicketNo.Text;
				feeArg.PayStationName = Settings.Default.OnlyID;
				feeArg.SerialNumber = "";
				feeArg.StaffCode = DataBuffer2018.CurrentStaff.StaffCode;
				feeArg.ISFine = false;
				feeArg.ChargeTime = initTime;
				feeArg.InTime = m_TransactionData.InTime;
				parkType = (EnumParkType)m_TransactionData.ParkTypeID;
				EnumBillType billType = EnumBillType.TimeCharge;
				CalcTicketFeeReturn calcTicketFeeReturn = chargeContext.CommunicationChannel.CalcTicketFee(feeArg, parkType, billType, out m_ChargeRecord);
				chargeContext.CommunicationChannel.Disconnect();
				saveArg.ISTimeOut = calcTicketFeeReturn.ISTimeOut;
				if (calcTicketFeeReturn.ISValid)
				{
					if (!calcTicketFeeReturn.HasLastTimeCharge)
					{
						txtTicketNo.Text = view.InCardCode;
						txtInTime.Text = m_TransactionData.InTime.ToString(SystemParm.LongTimeFormat);
						comboParkType.SelectedValue = m_TransactionData.ParkTypeID;
						txtParkHour.Text = (m_ChargeRecord.ParkMin / 60).ToString();
						txtParkMin.Text = (m_ChargeRecord.ParkMin % 60).ToString();
						txtFree.Text = m_ChargeRecord.FreeMin.ToString();
						txtTotalCharge.Text = m_ChargeRecord.TotalCharge.ToString("f2");
						if (!string.IsNullOrEmpty(m_TransactionData.Remark))
						{
							try
							{
								comboParkArea.SelectedValue = Convert.ToInt32(m_TransactionData.Remark);
							}
							catch (Exception message)
							{
								Logger.Error(message);
							}
						}
						else
						{
							comboParkArea.SelectedValue = (from m in DataBuffer2018.AllParkGates
								where m.GateID == m_TransactionData.InGateID
								select m.GateAreaID).FirstOrDefault();
						}
					}
					else
					{
						txtTicketNo.Text = view.InCardCode;
						txtInTime.Text = m_TransactionData.InTime.ToString(SystemParm.LongTimeFormat);
						comboParkType.SelectedValue = m_TransactionData.ParkTypeID;
						txtParkHour.Text = (m_ChargeRecord.ParkMin / 60).ToString();
						txtParkMin.Text = (m_ChargeRecord.ParkMin % 60).ToString();
						txtFree.Text = m_ChargeRecord.FreeMin.ToString();
						comboParkType.SelectedValue = m_TransactionData.ParkTypeID;
						txtTotalCharge.Text = m_ChargeRecord.TotalCharge.ToString("f2");
						if (!string.IsNullOrEmpty(m_TransactionData.Remark))
						{
							try
							{
								comboParkArea.SelectedValue = Convert.ToInt32(m_TransactionData.Remark);
							}
							catch (Exception message2)
							{
								Logger.Error(message2);
							}
						}
						else
						{
							comboParkArea.SelectedValue = (from m in DataBuffer2018.AllParkGates
								where m.GateID == m_TransactionData.InGateID
								select m.GateAreaID).FirstOrDefault();
						}
					}
					btnOK.Enabled = true;
				}
				else
				{
					if (calcTicketFeeReturn.ErrCode == "Coupon_Invalid")
					{
						CalcAmount();
					}
					Global.ShowMessage(calcTicketFeeReturn.ErrCode);
					btnOK.Enabled = false;
				}
			}
			else
			{
				Console.WriteLine("m_TransactionData is Null");
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
			btnCancel_Click(null, null);
		}
		catch (Exception message3)
		{
			Logger.Error(message3);
		}
	}

	private void btnCancelFree_Click(object sender, EventArgs e)
	{
		feeArg.CustomFreeID = 0;
		feeArg.CustomFreeTenatID = 0;
		FreeImagePath = null;
		Remark = "";
		try
		{
			LPDBHelper.DelFreeRecord(m_TransactionData.TransactionID);
			Logger.Debug("DelFreeRecord ID:" + m_TransactionData.TransactionID);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		CalcAmount();
		btnCancel.Focus();
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		try
		{
			DeviceManager.FeeCenterModule.DisplayFee("READY.");
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
		base.OnClosing(e);
	}

	private void txtTotalCharge_TextChanged(object sender, EventArgs e)
	{
		try
		{
			DeviceManager.FeeCenterModule.DisplayFee(txtTotalCharge.Text);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Console.WriteLine(ex.Message);
		}
	}

	private void FormTimeChargeDemage_Activated(object sender, EventArgs e)
	{
		txtTicketNo.Focus();
	}

	private void CheckValid(bool status)
	{
		if (status)
		{
			btnOK.Enabled = true;
			btnOK.Focus();
		}
		else
		{
			btnOK.Enabled = false;
		}
	}

	private FeeInfo FeeCenterModule_TicketScanEvent(TicketInfo ticketInfo)
	{
		FeeInfo feeInfo = new FeeInfo();
		if (ticketInfo.IsEmptyOrInValid)
		{
			feeInfo.TicketAction = EnumTicketAction.Keep;
			Invoke((Action)delegate
			{
				btnOK.Enabled = true;
				IsCharge = true;
				if (form != null)
				{
					form.btnOK.Enabled = true;
					form.btnOther.Enabled = true;
				}
			});
			return feeInfo;
		}
		Global.ShowMessage(LangManager.GetLangString("Alert.Not_Empty_Ticket"));
		feeInfo.TicketAction = EnumTicketAction.Reject;
		Invoke((Action)delegate
		{
			btnOK.Enabled = false;
		});
		return feeInfo;
	}

	private void FormTimeChargeDemage_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void FormTimeChargeDemage_Load(object sender, EventArgs e)
	{
		comboParkArea.DataSource = DataBuffer2018.AllParkAreas;
		comboParkArea.DisplayMember = "AreaName";
		comboParkArea.ValueMember = "AreaID";
		comboParkArea.SelectedValue = Convert.ToInt32(Config.AreaCode);
	}

	private void btnOther_Click(object sender, EventArgs e)
	{
		btnCancel.Focus();
		string cardCode = m_ChargeRecord.CardCode;
		using FormEpaySale formEpaySale = new FormEpaySale
		{
			ChargeRecord = m_ChargeRecord
		};
		if (formEpaySale.ShowDialog() == DialogResult.OK)
		{
			if (rbAirPay.Checked)
			{
				formEpaySale.ChargeRecord.BillType = 14;
			}
			else
			{
				formEpaySale.ChargeRecord.BillType = 14;
			}
			m_ChargeRecord.CardCode = cardCode;
			formEpaySale.ChargeRecord.PayType = (int)formEpaySale.PayTypeFlag;
			SaveChargeRecord(m_ChargeRecord, formEpaySale.MPass, formEpaySale.BOC, formEpaySale.BOC_N910, formEpaySale.SPay);
		}
	}

	private void SaveChargeRecord(ChargeRecord charge, MPass_POS_Transaction_Detail mpass, BOC_Gate_TransactionExtend boc, BOC_N910_POS_Card_Payment_DetailEX bocn910, ScanPayment scanPayment)
	{
		if (charge == null)
		{
			Global.ShowMessage(LangManager.GetLangString("Alert.Input"));
			return;
		}
		try
		{
			saveArg.CustomFreeID = feeArg.CustomFreeID;
			saveArg.CustomFreeTenatID = feeArg.CustomFreeTenatID;
			saveArg.InTime = feeArg.InTime;
			saveArg.TicketNumber = m_ChargeRecord.CardCode;
			saveArg.FreeImagePath = FreeImagePath;
			saveArg.CustomFreeRecordRemark = Remark;
			saveArg.TransactionID = view.TransactionID;
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
				saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveArg, parkType, charge, mpass, boc, bocn910, scanPayment);
				chargeContext.CommunicationChannel.Disconnect();
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				if (mpass != null || bocn910 != null || scanPayment != null)
				{
					DBHelper.Insert(charge.CardCode, charge, mpass, boc, saveArg, bocn910, scanPayment);
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
			if (saveChargeRecordReturn != null)
			{
				if (saveChargeRecordReturn.ISOK)
				{
					FeeInfo feeInfo = new FeeInfo
					{
						InTime = m_TransactionData.InTime,
						ParkType = (EnumParkType)m_TransactionData.ParkTypeID,
						TicketNumber = m_ChargeRecord.CardCode,
						TicketAction = EnumTicketAction.Normal
					};
					feeInfo.Fee = charge.TotalCharge;
					feeInfo.FeeTime = charge.ChargeTime;
					feeInfo.NeedPrint = true;
					feeInfo.TicketAction = EnumTicketAction.Normal;
					feeInfo.TicketType = EnumTicketType.Lost;
					feeInfo.FieldStr = comboParkArea.Text;
					Logger.Debug(m_TransactionData.InGateID.ToString());
					if (m_TransactionData.InGateID != 0)
					{
						feeInfo.CarParkSerialNo = DataBuffer2018.GetCarParkSerialEx((EnumParkType)m_TransactionData.ParkTypeID, m_TransactionData.InGateID);
					}
					else
					{
						feeInfo.CarParkSerialNo = DataBuffer2018.GetCarParkSerial((EnumParkType)m_TransactionData.ParkTypeID, Convert.ToInt32(m_TransactionData.Remark));
					}
					try
					{
						DeviceManager.FeeCenterModule.MakeTicket(feeInfo);
					}
					catch (OperationCanceledException)
					{
						Global.ShowMessage(LangManager.GetLangString("Alert.TicketFail"));
						DeviceManager.FeeCenterModule.EjectTicket();
						return;
					}
					DeviceManager.FeeCenterModule.EjectTicket();
					if (m_ChargeRecord.TotalCharge != 0m && mpass == null && boc == null)
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
						Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TipSynchronization"));
						try
						{
							if (mPass_POS_Transaction_Detail != null)
							{
								saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveArg, parkType, chargeRecord, mPass_POS_Transaction_Detail, null);
								chargeContext.CommunicationChannel.Disconnect();
							}
							else
							{
								saveChargeRecordReturn = chargeContext.CommunicationChannel.SaveElectronicChargeRecord(saveArg, parkType, chargeRecord, null, bOC_Gate_TransactionExtend, bOC_N910_POS_Card_Payment_DetailEX, scanPayment2);
								chargeContext.CommunicationChannel.Disconnect();
							}
							if (!saveChargeRecordReturn.ISOK)
							{
								Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.SyncFailed"));
							}
							else
							{
								DBHelper.ExecuteNonQuery($"update ChargeRecord set isupload='1' where timechargeid={chargeRecord.TimeChargeID}", CommandType.Text, (IDbDataParameter[])null);
							}
						}
						catch (Exception)
						{
							Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.SyncFailed"));
						}
					}
				}
				else
				{
					Global.ShowMessage(saveChargeRecordReturn.ErrCode);
				}
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage(LangManager.GetLangString("CarPark.Forms.ShowMessage.TimeOut"));
			btnCancel_Click(null, null);
		}
		catch (Exception ex5)
		{
			try
			{
				DeviceManager.FeeCenterModule.EjectTicket();
			}
			catch (Exception ex6)
			{
				Logger.Error(ex6);
				Console.WriteLine(ex6.Message);
			}
			Logger.Error(ex5);
			Console.WriteLine(ex5.Message);
			MessageBox.Show(ex5.Message);
		}
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void txtTicketNo_TextChanged(object sender, EventArgs e)
	{
		try
		{
			GetView_TransactionAndLPArgs getView_TransactionAndLPArgs = new GetView_TransactionAndLPArgs();
			if (rbMPASS.Checked)
			{
				if (txtTicketNo.Text.Length < 10 || txtTicketNo.Text.Length > 12)
				{
					return;
				}
				list = new List<view_transactionandlp>();
				getView_TransactionAndLPArgs.MPassNumber = txtTicketNo.Text;
				chargeContext.CommunicationChannel.GetView_TransactionAndLP(getView_TransactionAndLPArgs, out list);
				chargeContext.CommunicationChannel.Disconnect();
				if (list.Count > 0)
				{
					using (List<view_transactionandlp>.Enumerator enumerator = list.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							view_transactionandlp current = enumerator.Current;
							view = current;
						}
					}
					DisableRB();
					if (!string.IsNullOrWhiteSpace(view.AnalysisResult))
					{
						Linceseplate = view.AnalysisResult;
					}
					m_TransactionData = new TransactionData();
					m_ChargeRecord = new ChargeRecord();
					m_TransactionData.InCardCode = view.InCardCode;
					m_TransactionData.InGateID = view.InGateID;
					m_TransactionData.InTime = view.InTime;
					m_TransactionData.ParkTypeID = view.ParkTypeID;
					m_TransactionData.TransactionBillType = view.TransactionBillType;
					m_TransactionData.TransactionID = view.TransactionID;
					txtTicketNo.Text = view.InCardCode;
					CalcAmount();
					btnOK.Focus();
				}
				else
				{
					Global.ShowMessage(LangManager.GetLangString("GateErrorCodes.Vehicle_Not_In"));
					txtTicketNo.Text = "";
					txtTicketNo.Enabled = true;
					txtTicketNo.Focus();
				}
			}
			else
			{
				if (txtTicketNo.Text.Length < 0 || txtTicketNo.Text.Length > 7)
				{
					return;
				}
				list = new List<view_transactionandlp>();
				getView_TransactionAndLPArgs.LicensePlate = txtTicketNo.Text;
				GetView_TransactionAndLPReturn view_TransactionAndLP = chargeContext.CommunicationChannel.GetView_TransactionAndLP(getView_TransactionAndLPArgs, out list);
				chargeContext.CommunicationChannel.Disconnect();
				if (list.Count > 0)
				{
					using (List<view_transactionandlp>.Enumerator enumerator2 = list.GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							view_transactionandlp current2 = enumerator2.Current;
							view = current2;
						}
					}
					DisableRB();
					if (!string.IsNullOrWhiteSpace(view.AnalysisResult))
					{
						Linceseplate = view.AnalysisResult;
					}
					m_TransactionData = new TransactionData();
					m_ChargeRecord = new ChargeRecord();
					m_TransactionData.InCardCode = view.InCardCode;
					m_TransactionData.InGateID = view.InGateID;
					m_TransactionData.InTime = view.InTime;
					m_TransactionData.ParkTypeID = view.ParkTypeID;
					m_TransactionData.TransactionBillType = view.TransactionBillType;
					m_TransactionData.TransactionID = view.TransactionID;
					txtTicketNo.Text = view.InCardCode;
					CalcAmount();
					btnOK.Focus();
				}
				else if (!string.IsNullOrEmpty(view_TransactionAndLP.Extend1))
				{
					Global.ShowMessage(LangManager.GetLangString(view_TransactionAndLP.Extend1));
				}
				else
				{
					Global.ShowMessage(LangManager.GetLangString("GateErrorCodes.Vehicle_Not_In"));
					txtTicketNo.Text = "";
					txtTicketNo.Enabled = true;
					txtTicketNo.Focus();
				}
			}
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

	private void rbMPASS_CheckedChanged(object sender, EventArgs e)
	{
		txtTicketNo.Text = "";
		if (rbMPASS.Checked)
		{
			btnRead.Enabled = true;
			labTicketNo.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.labTicketNo");
			label1.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.label1");
			txtTicketNo.Focus();
		}
		else
		{
			btnRead.Enabled = false;
			labTicketNo.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.labTicketNo2");
			label1.Text = LangManager.GetLangString("CarPark.Forms.FormTransferTicket.label3");
			txtTicketNo.Focus();
		}
	}

	private void DisableRB()
	{
		rbAirPay.Enabled = false;
		rbMPASS.Enabled = false;
	}

	private void txtTicketNo_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			initialLicensePlate = txtTicketNo.Text;
			txtTicketNo.Enabled = false;
			txtTicketNo_TextChanged(sender, e);
		}
	}

	private void btnRead_Click(object sender, EventArgs e)
	{
		try
		{
			txtTicketNo.Text = string.Empty;
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
							txtTicketNo.Text = queryResult.CardNo;
							txtTicketNo.Focus();
						});
					}
				}
				catch (Exception ex2)
				{
					Global.ShowMessage(ex2.Message);
				}
			}, 0);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			MessageBox.Show(ex.Message);
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
		this.labTitle = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnTransferTicket = new System.Windows.Forms.Button();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.rbAirPay = new System.Windows.Forms.RadioButton();
		this.label1 = new System.Windows.Forms.Label();
		this.rbMPASS = new System.Windows.Forms.RadioButton();
		this.btnFree = new System.Windows.Forms.Button();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.btnCancelFree = new System.Windows.Forms.ToolStripMenuItem();
		this.txtTotalCharge = new System.Windows.Forms.TextBox();
		this.comboParkArea = new System.Windows.Forms.ComboBox();
		this.comboParkType = new System.Windows.Forms.ComboBox();
		this.txtFree = new System.Windows.Forms.TextBox();
		this.txtParkMin = new System.Windows.Forms.TextBox();
		this.txtParkHour = new System.Windows.Forms.TextBox();
		this.txtInTime = new System.Windows.Forms.TextBox();
		this.txtTicketNo = new System.Windows.Forms.TextBox();
		this.labArea = new System.Windows.Forms.Label();
		this.labTotalCharge = new System.Windows.Forms.Label();
		this.labParkType = new System.Windows.Forms.Label();
		this.labFreeTime = new System.Windows.Forms.Label();
		this.labTimeSplit = new System.Windows.Forms.Label();
		this.labParkTime = new System.Windows.Forms.Label();
		this.labLastTime = new System.Windows.Forms.Label();
		this.labTicketNo = new System.Windows.Forms.Label();
		this.panFill = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		this.panFill.SuspendLayout();
		base.SuspendLayout();
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("微软雅黑", 25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labTitle.ForeColor = System.Drawing.Color.Navy;
		this.labTitle.Location = new System.Drawing.Point(0, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(593, 60);
		this.labTitle.TabIndex = 4;
		this.labTitle.Text = "優惠處理";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel1.Controls.Add(this.btnTransferTicket);
		this.panel1.Controls.Add(this.btnOK);
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.panel1.Location = new System.Drawing.Point(0, 622);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(593, 76);
		this.panel1.TabIndex = 5;
		this.btnTransferTicket.ForeColor = System.Drawing.Color.Navy;
		this.btnTransferTicket.Location = new System.Drawing.Point(180, 14);
		this.btnTransferTicket.Name = "btnTransferTicket";
		this.btnTransferTicket.Size = new System.Drawing.Size(120, 48);
		this.btnTransferTicket.TabIndex = 3;
		this.btnTransferTicket.Text = "轉時票";
		this.btnTransferTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnTransferTicket.UseVisualStyleBackColor = true;
		this.btnTransferTicket.Click += new System.EventHandler(btnTransferTicket_Click);
		this.btnOK.Enabled = false;
		this.btnOK.ForeColor = System.Drawing.Color.Navy;
		this.btnOK.Location = new System.Drawing.Point(337, 14);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(120, 48);
		this.btnOK.TabIndex = 0;
		this.btnOK.Text = "確認";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.ForeColor = System.Drawing.Color.Navy;
		this.btnCancel.Location = new System.Drawing.Point(463, 14);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(120, 48);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(239, 246, 253);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.label2);
		this.panel2.Controls.Add(this.rbAirPay);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.rbMPASS);
		this.panel2.Controls.Add(this.btnFree);
		this.panel2.Controls.Add(this.txtTotalCharge);
		this.panel2.Controls.Add(this.comboParkArea);
		this.panel2.Controls.Add(this.comboParkType);
		this.panel2.Controls.Add(this.txtFree);
		this.panel2.Controls.Add(this.txtParkMin);
		this.panel2.Controls.Add(this.txtParkHour);
		this.panel2.Controls.Add(this.txtInTime);
		this.panel2.Controls.Add(this.txtTicketNo);
		this.panel2.Controls.Add(this.labArea);
		this.panel2.Controls.Add(this.labTotalCharge);
		this.panel2.Controls.Add(this.labParkType);
		this.panel2.Controls.Add(this.labFreeTime);
		this.panel2.Controls.Add(this.labTimeSplit);
		this.panel2.Controls.Add(this.labParkTime);
		this.panel2.Controls.Add(this.labLastTime);
		this.panel2.Controls.Add(this.labTicketNo);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.panel2.ForeColor = System.Drawing.Color.Navy;
		this.panel2.Location = new System.Drawing.Point(0, 60);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(593, 562);
		this.panel2.TabIndex = 6;
		this.label2.Location = new System.Drawing.Point(385, 227);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(50, 35);
		this.label2.TabIndex = 5;
		this.label2.Text = "M";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.rbAirPay.Checked = true;
		this.rbAirPay.Font = new System.Drawing.Font("微软雅黑", 10f);
		this.rbAirPay.Location = new System.Drawing.Point(493, 29);
		this.rbAirPay.Name = "rbAirPay";
		this.rbAirPay.Size = new System.Drawing.Size(83, 24);
		this.rbAirPay.TabIndex = 0;
		this.rbAirPay.TabStop = true;
		this.rbAirPay.Text = "車牌";
		this.rbAirPay.UseVisualStyleBackColor = true;
		this.rbAirPay.CheckedChanged += new System.EventHandler(rbMPASS_CheckedChanged);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("微软雅黑", 10f);
		this.label1.ForeColor = System.Drawing.Color.Red;
		this.label1.Location = new System.Drawing.Point(252, 56);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(135, 20);
		this.label1.TabIndex = 0;
		this.label1.Text = "*  請輸入澳門通卡號";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.rbMPASS.Font = new System.Drawing.Font("微软雅黑", 10f);
		this.rbMPASS.Location = new System.Drawing.Point(493, 3);
		this.rbMPASS.Name = "rbMPASS";
		this.rbMPASS.Size = new System.Drawing.Size(83, 24);
		this.rbMPASS.TabIndex = 0;
		this.rbMPASS.Text = "澳門通";
		this.rbMPASS.UseVisualStyleBackColor = true;
		this.rbMPASS.CheckedChanged += new System.EventHandler(rbMPASS_CheckedChanged);
		this.btnFree.ContextMenuStrip = this.contextMenuStrip1;
		this.btnFree.Location = new System.Drawing.Point(445, 223);
		this.btnFree.Name = "btnFree";
		this.btnFree.Size = new System.Drawing.Size(131, 43);
		this.btnFree.TabIndex = 4;
		this.btnFree.Text = "免費";
		this.btnFree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnFree.UseVisualStyleBackColor = true;
		this.btnFree.Click += new System.EventHandler(btnFree_Click);
		this.contextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.btnCancelFree });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(199, 44);
		this.btnCancelFree.Name = "btnCancelFree";
		this.btnCancelFree.Size = new System.Drawing.Size(198, 40);
		this.btnCancelFree.Text = "取消免費";
		this.btnCancelFree.Click += new System.EventHandler(btnCancelFree_Click);
		this.txtTotalCharge.BackColor = System.Drawing.Color.White;
		this.txtTotalCharge.Font = new System.Drawing.Font("微軟正黑體", 36f, System.Drawing.FontStyle.Bold);
		this.txtTotalCharge.Location = new System.Drawing.Point(248, 421);
		this.txtTotalCharge.Name = "txtTotalCharge";
		this.txtTotalCharge.ReadOnly = true;
		this.txtTotalCharge.Size = new System.Drawing.Size(328, 71);
		this.txtTotalCharge.TabIndex = 0;
		this.txtTotalCharge.TabStop = false;
		this.txtTotalCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtTotalCharge.Visible = false;
		this.txtTotalCharge.TextChanged += new System.EventHandler(txtTotalCharge_TextChanged);
		this.comboParkArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkArea.Enabled = false;
		this.comboParkArea.Font = new System.Drawing.Font("微軟正黑體", 20.25f);
		this.comboParkArea.FormattingEnabled = true;
		this.comboParkArea.Location = new System.Drawing.Point(248, 355);
		this.comboParkArea.Name = "comboParkArea";
		this.comboParkArea.Size = new System.Drawing.Size(328, 42);
		this.comboParkArea.TabIndex = 0;
		this.comboParkArea.TabStop = false;
		this.comboParkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboParkType.Enabled = false;
		this.comboParkType.Font = new System.Drawing.Font("微軟正黑體", 20.25f);
		this.comboParkType.FormattingEnabled = true;
		this.comboParkType.IntegralHeight = false;
		this.comboParkType.Location = new System.Drawing.Point(248, 290);
		this.comboParkType.Name = "comboParkType";
		this.comboParkType.Size = new System.Drawing.Size(328, 42);
		this.comboParkType.TabIndex = 0;
		this.txtFree.Enabled = false;
		this.txtFree.Location = new System.Drawing.Point(248, 224);
		this.txtFree.Name = "txtFree";
		this.txtFree.Size = new System.Drawing.Size(131, 43);
		this.txtFree.TabIndex = 0;
		this.txtFree.TabStop = false;
		this.txtParkMin.Enabled = false;
		this.txtParkMin.Location = new System.Drawing.Point(445, 162);
		this.txtParkMin.Name = "txtParkMin";
		this.txtParkMin.Size = new System.Drawing.Size(131, 43);
		this.txtParkMin.TabIndex = 0;
		this.txtParkMin.TabStop = false;
		this.txtParkHour.Enabled = false;
		this.txtParkHour.Location = new System.Drawing.Point(248, 162);
		this.txtParkHour.Name = "txtParkHour";
		this.txtParkHour.Size = new System.Drawing.Size(131, 43);
		this.txtParkHour.TabIndex = 0;
		this.txtParkHour.TabStop = false;
		this.txtInTime.Enabled = false;
		this.txtInTime.Location = new System.Drawing.Point(248, 101);
		this.txtInTime.Name = "txtInTime";
		this.txtInTime.Size = new System.Drawing.Size(328, 43);
		this.txtInTime.TabIndex = 0;
		this.txtInTime.TabStop = false;
		this.txtTicketNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTicketNo.Location = new System.Drawing.Point(248, 10);
		this.txtTicketNo.Name = "txtTicketNo";
		this.txtTicketNo.Size = new System.Drawing.Size(239, 43);
		this.txtTicketNo.TabIndex = 0;
		this.txtTicketNo.TabStop = false;
		this.txtTicketNo.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTicketNo_KeyDown);
		this.labArea.Location = new System.Drawing.Point(14, 358);
		this.labArea.Name = "labArea";
		this.labArea.Size = new System.Drawing.Size(228, 34);
		this.labArea.TabIndex = 0;
		this.labArea.Text = "區域";
		this.labArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTotalCharge.Location = new System.Drawing.Point(14, 421);
		this.labTotalCharge.Name = "labTotalCharge";
		this.labTotalCharge.Size = new System.Drawing.Size(228, 71);
		this.labTotalCharge.TabIndex = 0;
		this.labTotalCharge.Text = "金額";
		this.labTotalCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTotalCharge.Visible = false;
		this.labParkType.Location = new System.Drawing.Point(14, 293);
		this.labParkType.Name = "labParkType";
		this.labParkType.Size = new System.Drawing.Size(228, 34);
		this.labParkType.TabIndex = 0;
		this.labParkType.Text = "車型";
		this.labParkType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labFreeTime.Location = new System.Drawing.Point(14, 228);
		this.labFreeTime.Name = "labFreeTime";
		this.labFreeTime.Size = new System.Drawing.Size(228, 34);
		this.labFreeTime.TabIndex = 0;
		this.labFreeTime.Text = "免费時間";
		this.labFreeTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTimeSplit.AutoSize = true;
		this.labTimeSplit.Location = new System.Drawing.Point(391, 166);
		this.labTimeSplit.Name = "labTimeSplit";
		this.labTimeSplit.Size = new System.Drawing.Size(42, 35);
		this.labTimeSplit.TabIndex = 0;
		this.labTimeSplit.Text = "：";
		this.labParkTime.Location = new System.Drawing.Point(14, 166);
		this.labParkTime.Name = "labParkTime";
		this.labParkTime.Size = new System.Drawing.Size(228, 34);
		this.labParkTime.TabIndex = 0;
		this.labParkTime.Text = "泊车時間";
		this.labParkTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labLastTime.Location = new System.Drawing.Point(14, 105);
		this.labLastTime.Name = "labLastTime";
		this.labLastTime.Size = new System.Drawing.Size(228, 34);
		this.labLastTime.TabIndex = 0;
		this.labLastTime.Text = "入場時間";
		this.labLastTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labTicketNo.Location = new System.Drawing.Point(14, 14);
		this.labTicketNo.Name = "labTicketNo";
		this.labTicketNo.Size = new System.Drawing.Size(228, 34);
		this.labTicketNo.TabIndex = 0;
		this.labTicketNo.Text = "卡號";
		this.labTicketNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(595, 700);
		base.Controls.Add(this.panFill);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "FormSetFree";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormTimeChargeDemage";
		base.Activated += new System.EventHandler(FormTimeChargeDemage_Activated);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormTimeChargeDemage_FormClosing);
		base.Load += new System.EventHandler(FormTimeChargeDemage_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.contextMenuStrip1.ResumeLayout(false);
		this.panFill.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormSetFree_Load(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(LicensePlate))
		{
			txtTicketNo.Text = LicensePlate;
			initialLicensePlate = LicensePlate;
			txtTicketNo.Enabled = false;
			txtTicketNo_TextChanged(sender, EventArgs.Empty);
		}
	}

	private void btnTransferTicket_Click(object sender, EventArgs e)
	{
		FormTransferTicket formTransferTicket = new FormTransferTicket();
		formTransferTicket.LicensePlate = initialLicensePlate;
		formTransferTicket.ShowDialog();
		Close();
	}
}
