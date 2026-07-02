using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CarPark;
using CarPark.Core;
using CarPark.DB;
using CarPark.DB.Context;
using CarPark.Device;
using Newtonsoft.Json;
using SkyInno.Lang;
using log4net;

namespace CarPark2018;

public class PrintUtils
{
	private static ILog Logger;

	private static string ShiftInfoOld;

	private static string ShiftInfo;

	static PrintUtils()
	{
		ShiftInfoOld = "Carpark Shift Receipt\n\n更次編號.            {0}\n站臺                 {1}\n開始時間             {2}\n結束時間             {3}\n\n\n-------Transaction-----\n澳門通增值   :   {8}\n增值金額     :   {9}\n時租數量     :   {10}\n時租金額     :   {11}\n失票數量     :   {12}\n失票金額     :   {13}\n坏票數量     :   {14}\n坏票金額     :   {15}\n超時數量     :   {16}\n超時金額     :   {17}\n月租數量     :   {18}\n月租金額     :   {19}\n月租按金     :   {20}\n按金退回     :   {21}\n          -------------\n閃付數量     :   {4}\n閃付金額     :   {5}\n澳門通數量   :   {6}\n澳門通金額   :   {7}\n現金金額     :   {23}\n          -------------\n交易總數     :   {22}\n交易金額     :   {24}\n\n---------免費及全免----\n免費數量     :   {25}\n免費金額     :   {26}\n\n\n---------手工起杆-----\n手工起杆     :   {27}\n\n\nUser ID      :   {27}\n\n---------END-------------\n";
		ShiftInfo = "Carpark Shift Receipt\n\n更次編號.            {0}\n站臺                 {1}\n開始時間             {2}\n結束時間             {3}\n\n\n-----------交易類型-----------\n時租收入          :   {54}\n月租收入          :   {17}\n澳門通增值        :   {5}\n\n-------------現金-------------\n增值數量          :   {4}\n增值金額          :   {5}\n時租數量          :   {6}\n時租金額          :   {7}\n超時數量          :   {12}\n超時金額          :   {13}\n轉紙票數量        :   {14}\n轉紙票金額        :   {15}\n------------------------------\n現金小計          :   {18}\n\n-------------月租-------------\n月租數量          :   {16}\n月租金額          :   {17}\n------------------------------\n月租小計          :   {17}\n\n---------澳門通和MPay---------\n時票數量          :   {19}\n時票金額          :   {20}\n超時數量          :   {25}\n超時金額          :   {26}\n轉紙票數量        :   {27}\n轉紙票金額        :   {28}\n------------------------------\n澳門通和MPay小計  :   {31}\n\n-------------無感-------------\n無感數量          :   {45}\n無感金額          :   {46}\n------------------------------\n無感小計          :   {47}\n\n-------------臨時收費-------------\n臨時數量          :   {29}\n臨時金額          :   {30}\n收據數量          :   {55}\n收據金額          :   {56}\n\n\n------------------------------\n交易總數          :   {48}\n交易金額          :   {49}\n\n----------免費及全免----------\n免費數量          :   {50}\n免費金額          :   {51}\n-----------手工起桿-----------\n起桿數量          :   {52}\nUser ID           :   {53}\n--------------END-------------\n";
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	private static string GetToString(IQueryable<ChargeRecord> records)
	{
		string text = "0";
		try
		{
			text = records.Sum((ChargeRecord m) => m.TotalCharge).ToString();
		}
		catch (Exception)
		{
		}
		return "$" + text;
	}

	private static string GetToString(List<ChargeRecord> records)
	{
		string text = "0";
		try
		{
			decimal num = default(decimal);
			foreach (ChargeRecord record in records)
			{
				num += record.TotalCharge;
			}
			text = num.ToString();
		}
		catch (Exception)
		{
		}
		return "$" + text;
	}

	private static string GetToString(IQueryable<CustomFreeRecord> records)
	{
		string text = "0";
		try
		{
			text = records.Sum((CustomFreeRecord m) => m.FreeCharge).ToString();
		}
		catch (Exception)
		{
		}
		return "$" + text;
	}

	private static string GetTransactionDesc(int transactionType)
	{
		string empty = string.Empty;
		return transactionType switch
		{
			0 => "消費 Sale", 
			1 => "充值 Reload", 
			2 => "售卡 Active", 
			_ => empty, 
		};
	}

	internal static void PrintDeposit(ChargeRecord dispRec)
	{
		try
		{
			string format = new string(PrintEtc.DepositChargePrint.ToCharArray());
			format = string.Format(format, dispRec.CardCode, dispRec.ChargeTime, dispRec.TotalCharge, dispRec.StaffCode, DataBuffer.CompanyInfo.CompanyNameCn, DataBuffer.CompanyInfo.CompanyNamePt);
			File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\DepositChargePrint.dat", format.Replace("\n", Environment.NewLine), Encoding.UTF8);
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	internal static void PrintDepositVoid(ChargeRecord dispRec)
	{
		try
		{
			string format = new string(PrintEtc.DepositVoidChargePrint.ToCharArray());
			format = string.Format(format, dispRec.CardCode, dispRec.ChargeTime, Math.Ceiling(dispRec.TotalCharge), dispRec.StaffCode, DataBuffer.CompanyInfo.CompanyNameCn, DataBuffer.CompanyInfo.CompanyNamePt);
			File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\DepositChargePrint.dat", format.Replace("\n", Environment.NewLine), Encoding.UTF8);
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	internal static void PrintMPReload(ChargeRecord record, MPass_POS_Transaction_Detail detai)
	{
		try
		{
			string format = new string(PrintEtc.MPReloadString.ToCharArray());
			format = string.Format(format, detai.INVOICENO, detai.MERCHANTID, detai.TERMINALID, $"{detai.PAN}/{detai.LOGNO}", detai.TransactionTime.ToString("dd/MM/yyyy HH:mm:ss"), (string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.ORIGBALANCE, detai.CashType + detai.TOTALAMT, (detai.EPA == 0m) ? "1.000" : detai.EPA.ToString(), (detai.EPAAMT == 0m) ? ((string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.TOTALAMT) : ((string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.EPAAMT), (string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.BALANCE);
			File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\mppos.dat", format.Replace("\n", Environment.NewLine), Encoding.UTF8);
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	internal static void PrintPBOCSale(ChargeRecord chargeRecord, BOC_Gate_TransactionExtend detai, TransactionData data)
	{
		try
		{
			EnumParkType parkTypeID = (EnumParkType)data.ParkTypeID;
			string format = new string(PrintEtc.PBOCSaleString.ToCharArray());
			format = string.Format(format, detai.EncryptedCardNumber, data.InTime.ToString("dd/MM/yyyy HH:mm:ss"), data.OutTime.Value.ToString("dd/MM/yyyy HH:mm:ss"), new TimeSpan(0, data.ParkMin, 0), LangManager.GetLangString($"{parkTypeID.GetType().FullName}.{parkTypeID}"), chargeRecord.TotalCharge, detai.CardRemain);
			File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\pbocsale.dat", format.Replace("\n", Environment.NewLine), Encoding.UTF8);
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	internal static void PrintRentalCharge(ChargeRecord record, Card card, RentalType renttype)
	{
		try
		{
			EnumParkType parkTypeID = (EnumParkType)renttype.ParkTypeID;
			string format = new string(PrintEtc.CardChargePrint.ToCharArray());
			format = string.Format(format, card.CardCode, record.ChargeTime, card.StartDate.ToString("dd/MM/yyyy"), card.ExpireDate.Value.ToString("dd/MM/yyyy"), LangManager.GetLangString($"{parkTypeID.GetType().FullName}.{parkTypeID}"), record.TotalCharge, record.StaffCode, DataBuffer.CompanyInfo.CompanyNameCn, DataBuffer.CompanyInfo.CompanyNamePt);
			File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\mppos.dat", format.Replace("\n", Environment.NewLine), Encoding.UTF8);
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	internal static void PrintShift(ShiftRecord record, List<ChargeRecord> ChargeRecord)
	{
		try
		{
			string format = new string(PrintEtc.ShiftInfo.ToCharArray());
			using Entities entities = DBContext.NewContext;
			List<ChargeRecord> list = ChargeRecord.Where((ChargeRecord m) => m.ShiftID == record.ShiftID).ToList();
			List<ChargeRecord> list2 = list.Where((ChargeRecord m) => m.BillType == 0).ToList();
			List<ChargeRecord> list3 = list.Where((ChargeRecord m) => m.BillType == 3 || m.BillType == 4).ToList();
			List<ChargeRecord> list4 = list.Where((ChargeRecord m) => m.BillType == 1).ToList();
			List<ChargeRecord> list5 = list.Where((ChargeRecord m) => m.BillType == 2 || m.BillType == 5).ToList();
			List<ChargeRecord> list6 = list.Where((ChargeRecord m) => m.BillType == 7).ToList();
			List<ChargeRecord> records = list.Where((ChargeRecord m) => m.BillType == 6).ToList();
			List<ChargeRecord> records2 = list.Where((ChargeRecord m) => m.BillType == 8).ToList();
			IQueryable<CustomFreeRecord> queryable = entities.CustomFreeRecord.Where((CustomFreeRecord m) => m.ShiftID == record.ShiftID);
			IQueryable<StaffOperat> source = entities.StaffOperat.Where((StaffOperat m) => m.ShiftID == record.ShiftID && m.OperationCode == 1 && !m.Remark.Contains("MANUAL"));
			List<ChargeRecord> list7 = list.Where((ChargeRecord m) => m.BillType == 11).ToList();
			List<ChargeRecord> list8 = list.Where((ChargeRecord m) => m.BillType == 10).ToList();
			List<ChargeRecord> list9 = list.Where((ChargeRecord m) => m.BillType == 12).ToList();
			List<ChargeRecord> records3 = list.Where((ChargeRecord m) => m.BillType != 10 && m.BillType != 12 && m.BillType != 11 && m.BillType != 9).ToList();
			format = string.Format(format, record.ShiftID.ToString().PadLeft(8, '0'), Environment.MachineName, record.StartTime.ToString("dd/MM/yyyy HH:mm"), record.IsComplete ? record.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : DateTime.Now.ToString("dd/MM/yyyy HH:mm"), list7.Count(), GetToString(list7), list8.Count(), GetToString(list8), list9.Count(), GetToString(list9), list2.Count(), GetToString(list2), list3.Count(), GetToString(list3), list4.Count(), GetToString(list4), list5.Count(), GetToString(list5), list6.Count(), GetToString(list6), GetToString(records), GetToString(records2), list.Count(), GetToString(records3), GetToString(list), queryable.Count(), GetToString(queryable), source.Count(), DataBuffer2018.CurrentStaff.StaffCode);
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	internal static void PrintShift(ShiftRecord record)
	{
		string text = new string(string.IsNullOrWhiteSpace(record.ReceiptObj) ? ShiftInfoOld.ToCharArray() : ShiftInfo.ToCharArray());
		string text2 = "";
		if (record.ManualChargeCharge.HasValue)
		{
			text2 = GetRemark("\n人手收費原因：" + record.ManualChargeRemark, 30) + "\n人手收費金額：$" + record.ManualChargeCharge;
		}
		ReceiptObj receiptObj = (string.IsNullOrWhiteSpace(record.ReceiptObj) ? null : JsonConvert.DeserializeObject<ReceiptObj>(record.ReceiptObj));
		text = ((receiptObj == null) ? string.Format(text + text2, record.ShiftID.ToString().PadLeft(8, '0'), Environment.MachineName, record.StartTime.ToString("dd/MM/yyyy HH:mm"), record.IsComplete ? record.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : DateTime.Now.ToString("dd/MM/yyyy HH:mm"), record.QPassCount, "$" + record.QPassCharge, record.MPassCount, "$" + record.MPassCharge, record.MPassDecalCount, "$" + record.MPassDecalCharge, record.TimeChargeCount, "$" + record.TimeCharge, record.LostCount, "$" + record.LostCharge, record.DamageCount, "$" + record.DamageCharge, record.TimeoutCount, "$" + record.TimeoutCharge, record.RentalCount, "$" + record.RentalCharge, "$" + record.RentalDepositCharge, "$" + record.VoidRentalCharge, record.TotalCount, "$" + record.CashCharge, "$" + record.TotalCharge, record.FeeCount, "$" + record.FeeCharge, record.OpenGateCount, DataBuffer2018.CurrentStaff.StaffCode, record.PaymentConversionCount, "$" + record.PaymentConversionCharge, record.MPassPosCount, "$" + record.MPassPosCharge, record.MPassGateCount, "$" + record.MPassGateCharge, record.NoSenseCount, "$" + record.NoSenseCharge) : string.Format(text + text2, record.ShiftID.ToString().PadLeft(8, '0'), Environment.MachineName, record.StartTime.ToString("dd/MM/yyyy HH:mm"), record.IsComplete ? record.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : DateTime.Now.ToString("dd/MM/yyyy HH:mm"), receiptObj.mpDecalCount, "$" + receiptObj.mpDecalAmt, receiptObj.cashTotalTimeChargeCnt, "$" + receiptObj.cashTotalTimeChargeAmt, receiptObj.cashLostCnt, "$" + receiptObj.cashLostAmt, receiptObj.cashDamageCnt, "$" + receiptObj.cashDamageAmt, receiptObj.cashTimeoutCnt, "$" + receiptObj.cashTimeoutAmt, receiptObj.cashPaymentConversionCnt, "$" + receiptObj.cashPaymentConversionAmt, receiptObj.cashRentalCnt, "$" + receiptObj.cashRentalAmt, "$" + (receiptObj.cashAmt - receiptObj.cashLostAmt), receiptObj.macauPassTotalTimeChargeCnt, "$" + receiptObj.macauPassTotalTimeChargeAmt, receiptObj.macauPassLostCnt, "$" + receiptObj.macauPassLostAmt, receiptObj.macauPassDamageCnt, "$" + receiptObj.macauPassDamageAmt, receiptObj.macauPassTimeoutCnt, "$" + receiptObj.macauPassTimeoutAmt, receiptObj.macauPassPaymentConversionCnt, "$" + receiptObj.macauPassPaymentConversionAmt, receiptObj.macauPassOutCnt, "$" + receiptObj.macauPassOutAmt, "$" + (receiptObj.mpassAmt - receiptObj.macauPassOutAmt), receiptObj.quickPassTotalTimeChargeCnt, "$" + receiptObj.quickPassTotalTimeChargeAmt, receiptObj.quickPassLostCnt, "$" + receiptObj.quickPassLostAmt, receiptObj.quickPassDamageCnt, "$" + receiptObj.quickPassDamageAmt, receiptObj.quickPassTimeoutCnt, "$" + receiptObj.quickPassTimeoutAmt, receiptObj.quickPassPaymentConversionCnt, "$" + receiptObj.quickPassPaymentConversionAmt, receiptObj.quickPassOutCnt, "$" + receiptObj.quickPassOutAmt, "$" + receiptObj.quickAmt, receiptObj.NoSenseCnt, "$" + receiptObj.NoSenseAmt, "$" + receiptObj.NoSenseAmt, record.TotalCount, "$" + record.TotalCharge, record.FeeCount, "$" + record.FeeCharge, record.OpenGateCount, DataBuffer2018.CurrentStaff.StaffCode, "$" + (receiptObj.cashAmt + receiptObj.mpassAmt + receiptObj.NoSenseAmt - receiptObj.mpDecalAmt - receiptObj.cashLostAmt - receiptObj.macauPassOutAmt), record.RentalDepositCount, "$" + record.RentalDepositCharge));
		if (DeviceManager.FeeCenterModule != null)
		{
			DeviceManager.FeeCenterModule.Print(text);
		}
	}

	internal static void PrintTimechargeRecord(ChargeRecord record, TransactionData passtrace)
	{
		try
		{
			if (record.BillType == 12)
			{
				using (Entities entities = DBContext.NewContext)
				{
					MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail = entities.MPass_POS_Transaction_Detail.FirstOrDefault((MPass_POS_Transaction_Detail m) => m.ChargeTransactionID == record.TimeChargeID);
					if (mPass_POS_Transaction_Detail == null)
					{
						Logger.Error($"找不到交易編號為{record.TimeChargeID}的POS交易記錄");
					}
					else
					{
						PrintMPReload(record, mPass_POS_Transaction_Detail);
					}
					return;
				}
			}
			if (record.BillType == 7)
			{
				using (Entities entities2 = DBContext.NewContext)
				{
					Card card = entities2.Card.FirstOrDefault((Card m) => m.CardCode == record.CardCode);
					RentalType renttype = DataBuffer.RentalTypes.FirstOrDefault((RentalType m) => m.RentalTypeID == card.RentalTypeID);
					PrintRentalCharge(record, card, renttype);
					return;
				}
			}
			if (record.BillType == 11)
			{
				using (Entities entities3 = DBContext.NewContext)
				{
					BOC_Gate_TransactionExtend detai = entities3.BOC_Gate_TransactionExtend.FirstOrDefault((BOC_Gate_TransactionExtend m) => m.SysTransacionID == passtrace.TransactionID);
					PrintPBOCSale(record, detai, passtrace);
					return;
				}
			}
			if (record.BillType == 10)
			{
				using (Entities entities4 = DBContext.NewContext)
				{
					MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail2 = entities4.MPass_POS_Transaction_Detail.FirstOrDefault((MPass_POS_Transaction_Detail m) => m.ChargeTransactionID == record.TimeChargeID);
					if (mPass_POS_Transaction_Detail2 != null)
					{
						smethod_0(record, mPass_POS_Transaction_Detail2);
					}
					else
					{
						Logger.Error($"找不到交易編號為{record.TimeChargeID}的POS交易記錄");
						MPass_Gate_Transaction mPass_Gate_Transaction = (from m in entities4.MPass_Gate_Transaction
							where m.SysTransacionID == passtrace.TransactionID
							orderby m.TransactionID descending
							select m).FirstOrDefault();
						MPass_POS_Transaction_Detail detai2 = new MPass_POS_Transaction_Detail
						{
							INVOICENO = "",
							MERCHANTID = "",
							TERMINALID = mPass_Gate_Transaction.CardReaderNo,
							PAN = mPass_Gate_Transaction.CardNumber,
							LOGNO = mPass_Gate_Transaction.LogicalCardNumber,
							TransactionTime = mPass_Gate_Transaction.TransactionTime,
							PURSETYPE = "",
							ORIGBALANCE = mPass_Gate_Transaction.CardRemain.Value + mPass_Gate_Transaction.CardBillAmount.Value,
							EPA = 0m,
							EPAAMT = mPass_Gate_Transaction.CardBillAmount.Value,
							TOTALAMT = mPass_Gate_Transaction.CardBillAmount.Value,
							BALANCE = mPass_Gate_Transaction.CardRemain.Value
						};
						smethod_0(record, detai2);
					}
					return;
				}
			}
			string format = new string(PrintEtc.TimeChargePrint.ToCharArray());
			EnumParkType parkTypeID = (EnumParkType)passtrace.ParkTypeID;
			string text = (record.ChargeTime - passtrace.InTime).ToString("c");
			if (text.LastIndexOf(".") > 7)
			{
				text = text.Substring(0, text.LastIndexOf("."));
			}
			string text2 = ((record.Remark == null) ? "" : record.Remark);
			format = string.Format(format, record.CardCode, passtrace.InTime.ToString("yyyy/MM/dd HH:mm"), record.ChargeTime.ToString("yyyy/MM/dd HH:mm"), text, record.FreeMin, LangManager.GetLangString($"{parkTypeID.GetType().FullName}.{parkTypeID}"), record.TotalCharge, record.StaffCode, DataBuffer.CompanyInfo.CompanyNameCn, DataBuffer.CompanyInfo.CompanyNamePt, text2);
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	internal static void smethod_0(ChargeRecord chargeRecord, MPass_POS_Transaction_Detail detai)
	{
		try
		{
			string format = new string(PrintEtc.MPSaleString.ToCharArray());
			format = string.Format(format, detai.INVOICENO, detai.MERCHANTID, detai.TERMINALID, $"{detai.PAN}/{detai.LOGNO}", detai.TransactionTime.ToString("dd/MM/yyyy HH:mm:ss"), (string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.ORIGBALANCE, detai.CashType + detai.TOTALAMT, (detai.EPA == 0m) ? "1.000" : detai.EPA.ToString(), (detai.EPAAMT == 0m) ? ((string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.TOTALAMT) : ((string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.EPAAMT), (string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.BALANCE);
			File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\mppossale.dat", format.Replace("\n", Environment.NewLine), Encoding.UTF8);
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
			using Entities entities = DBContext.NewContext;
			TransactionData transactionData = entities.TransactionData.FirstOrDefault((TransactionData m) => (int?)m.TransactionID == chargeRecord.TransactionID);
			if (transactionData != null && transactionData.InCardCode != chargeRecord.CardCode)
			{
				ChargeRecord chargeRecord2 = new ChargeRecord();
				EntityHelper.CopyEntity(chargeRecord, chargeRecord2);
				chargeRecord2.BillType = transactionData.TransactionBillType;
				chargeRecord2.CardCode = transactionData.InCardCode;
				PrintTimechargeRecord(chargeRecord2, transactionData);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	internal static void smethod_0(ChargeRecord chargeRecord, MPass_POS_Transaction_Detail detai, TransactionData passtrace)
	{
		try
		{
			string format = new string(PrintEtc.MPSaleString.ToCharArray());
			format = string.Format(format, detai.INVOICENO, detai.MERCHANTID, detai.TERMINALID, $"{detai.PAN}/{detai.LOGNO}", detai.TransactionTime.ToString("dd/MM/yyyy HH:mm:ss"), (string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.ORIGBALANCE, detai.CashType + detai.TOTALAMT, (detai.EPA == 0m) ? "1.000" : detai.EPA.ToString(), (detai.EPAAMT == 0m) ? ((string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.TOTALAMT) : ((string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.EPAAMT), (string.IsNullOrEmpty(detai.PURSETYPE) ? "MOP" : detai.PURSETYPE) + detai.BALANCE);
			File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\mppossale.dat", format.Replace("\n", Environment.NewLine), Encoding.UTF8);
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	internal static void PrintTimechargeTimeOutRecord(ChargeRecord record, TransactionData passtrace)
	{
		try
		{
			ChargeRecord chargeRecord;
			using (Entities entities = DBContext.NewContext)
			{
				chargeRecord = entities.ChargeRecord.OrderByDescending((ChargeRecord m) => m.TimeChargeID).FirstOrDefault((ChargeRecord m) => m.CardCode == record.CardCode && m.ChargeTime < record.ChargeTime);
				if (chargeRecord == null)
				{
					Logger.Error("[HYC] {PrintUtils} no lastCharge record");
				}
			}
			string format = new string(PrintEtc.TimeChargeTimeOutPrint.ToCharArray());
			EnumParkType parkTypeID = (EnumParkType)passtrace.ParkTypeID;
			string text = (record.ChargeTime - chargeRecord.ChargeTime).ToString("c");
			if (text.LastIndexOf(".") > 7)
			{
				text = text.Substring(0, text.LastIndexOf("."));
			}
			string text2 = ((record.Remark == null) ? "" : record.Remark);
			format = string.Format(format, record.CardCode, passtrace.InTime.ToString("yyyy/MM/dd HH:mm"), record.ChargeTime.ToString("yyyy/MM/dd HH:mm"), text, record.FreeMin, LangManager.GetLangString($"{parkTypeID.GetType().FullName}.{parkTypeID}"), record.TotalCharge, record.StaffCode, DataBuffer.CompanyInfo.CompanyNameCn, DataBuffer.CompanyInfo.CompanyNamePt, text2, chargeRecord.ChargeTime.ToString("yyyy/MM/dd HH:mm"));
			if (DeviceManager.FeeCenterModule != null)
			{
				DeviceManager.FeeCenterModule.Print(format);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private static string GetRemark(string str, int length)
	{
		if (str.Length == 0)
		{
			return "";
		}
		ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
		int num = 0;
		byte[] bytes = aSCIIEncoding.GetBytes(str);
		string text = "";
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < bytes.Length; i++)
		{
			if (bytes[i] == 63)
			{
				num += 2;
				num3 += 2;
			}
			else
			{
				num++;
				num3++;
			}
			num4++;
			if (num3 == length || num3 == length + 1)
			{
				text = text + str.Substring(num2, num4) + "\n";
				num2 = i;
				num3 = 0;
				num4 = 0;
			}
		}
		return text + str.Substring(num2, str.Length - num2);
	}
}
