using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.DB.Context;
using CarPark2018.LPPayForms;
using Master.SystemCommunication.Lib;
using log4net;

namespace CarPark2018;

public class LPDBHelper
{
	private static ILog Logger;

	public LPDBHelper()
	{
		if (Logger == null)
		{
			Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}
	}

	public static GetView_TransactionAndLPReturn GetView_TransactionAndLP(GetView_TransactionAndLPArgs getView_TransactionAndLPArgs, out List<view_transactionandlp> _view_transactionandlp)
	{
		GetView_TransactionAndLPReturn result = new GetView_TransactionAndLPReturn();
		_view_transactionandlp = null;
		try
		{
			using Entities entities = DBContext.NewContext;
			IQueryable<view_transactionandlp> source = ((!string.IsNullOrWhiteSpace(getView_TransactionAndLPArgs.LicensePlate)) ? entities.view_transactionandlp.Where((view_transactionandlp list) => list.TransactionBillType == 12 && list.InCardCode.Contains(getView_TransactionAndLPArgs.LicensePlate)) : entities.view_transactionandlp.Where((view_transactionandlp list) => list.TransactionBillType == 12));
			if (getView_TransactionAndLPArgs.InStartTime.Year > 1990 && getView_TransactionAndLPArgs.InEndTime.Year > 1990)
			{
				source = source.Where((view_transactionandlp list) => list.InTime >= getView_TransactionAndLPArgs.InStartTime && list.InTime <= getView_TransactionAndLPArgs.InEndTime);
			}
			Console.Write("mListAll " + source.Count());
			IQueryable<view_transactionandlp> queryable = null;
			IQueryable<view_transactionandlp> queryable2 = null;
			if (!string.IsNullOrWhiteSpace(getView_TransactionAndLPArgs.LicensePlate) && getView_TransactionAndLPArgs.LicensePlate.Length > 3)
			{
				string lpLeft = getView_TransactionAndLPArgs.LicensePlate.Substring(0, 3);
				queryable = entities.view_transactionandlp.Where((view_transactionandlp list) => list.TransactionBillType == 12 && list.InCardCode.Contains(lpLeft));
				Console.Write("mListLeft " + queryable.Count());
				queryable = queryable.Where((view_transactionandlp m) => !m.InCardCode.Contains(getView_TransactionAndLPArgs.LicensePlate));
				if (getView_TransactionAndLPArgs.InStartTime.Year > 1990 && getView_TransactionAndLPArgs.InEndTime.Year > 1990)
				{
					queryable = queryable.Where((view_transactionandlp list) => list.InTime >= getView_TransactionAndLPArgs.InStartTime && list.InTime <= getView_TransactionAndLPArgs.InEndTime);
				}
				Console.WriteLine("mListLeft " + queryable.Count());
				string lpRight = getView_TransactionAndLPArgs.LicensePlate.Substring(getView_TransactionAndLPArgs.LicensePlate.Length - 3, 3);
				queryable2 = entities.view_transactionandlp.Where((view_transactionandlp list) => list.TransactionBillType == 12 && list.InCardCode.Contains(lpRight));
				Console.Write("mListRight " + queryable2.Count());
				queryable2 = queryable2.Where((view_transactionandlp m) => !m.InCardCode.Contains(getView_TransactionAndLPArgs.LicensePlate));
				if (getView_TransactionAndLPArgs.InStartTime.Year > 1990 && getView_TransactionAndLPArgs.InEndTime.Year > 1990)
				{
					queryable2 = queryable2.Where((view_transactionandlp list) => list.InTime >= getView_TransactionAndLPArgs.InStartTime && list.InTime <= getView_TransactionAndLPArgs.InEndTime);
				}
				Console.WriteLine("mListRight " + queryable2.Count());
			}
			if (source.Count() > 0)
			{
				_view_transactionandlp = source.ToList();
			}
			if (_view_transactionandlp == null)
			{
				_view_transactionandlp = new List<view_transactionandlp>();
			}
			if (queryable != null)
			{
				_view_transactionandlp.AddRange(queryable.ToList());
			}
			if (queryable2 != null)
			{
				_view_transactionandlp.AddRange(queryable2.ToList());
			}
			Console.WriteLine(" total  " + _view_transactionandlp.Count());
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		return result;
	}

	public static CorrectParkTypeReturn CorrectParkType(CorrectParkTypeArgs correctParkTypeArgs)
	{
		CorrectParkTypeReturn correctParkTypeReturn = new CorrectParkTypeReturn();
		correctParkTypeReturn.IsOK = false;
		DbTransaction dbTransaction = null;
		try
		{
			using Entities entities = DBContext.NewContext;
			DbConnection connection = entities.Connection;
			connection.Open();
			dbTransaction = connection.BeginTransaction();
			TransactionData transactionData = entities.TransactionData.FirstOrDefault((TransactionData m) => m.TransactionID == correctParkTypeArgs.TransactionID);
			if (transactionData != null)
			{
				transactionData.ParkTypeID = correctParkTypeArgs.ParkTypeIDCorrect;
				entities.SaveChanges();
				PassTrace passTrace = entities.PassTrace.FirstOrDefault((PassTrace m) => m.TransactionID == (int?)correctParkTypeArgs.TransactionID);
				if (passTrace != null)
				{
					passTrace.ParkTypeID = correctParkTypeArgs.ParkTypeIDCorrect;
					entities.SaveChanges();
				}
				ShiftRecord shiftRecord = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => m.IsComplete == false && m.FromStation == DataBuffer.APPOnlyID);
				StaffOperat entity = new StaffOperat
				{
					OperationCode = 52,
					OperationTime = DateTime.Now,
					Remark = $"CardCode={transactionData.InCardCode} ParkTypeID={transactionData.ParkTypeID}",
					StaffCode = DataBuffer2018.CurrentStaff.StaffCode,
					ShiftID = (shiftRecord?.ShiftID ?? 0)
				};
				entities.StaffOperat.AddObject(entity);
				entities.SaveChanges();
			}
			dbTransaction.Commit();
			correctParkTypeReturn.IsOK = true;
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			correctParkTypeReturn.ErrCode = ex.Message;
			try
			{
				dbTransaction.Rollback();
			}
			catch (Exception ex2)
			{
				ILog logger = Logger;
				string text = "Rollback Err:";
				logger.Error(text + ex2);
			}
		}
		return correctParkTypeReturn;
	}

	public static CheckFeeReturn CheckFeeInfo(CheckFeeArgs checkFeeArgs)
	{
		CheckFeeReturn checkFeeReturn = new CheckFeeReturn();
		checkFeeReturn.IsPaid = false;
		checkFeeReturn.IsTimeout = false;
		DateTime now = DateTime.Now;
		try
		{
			using Entities entities = DBContext.NewContext;
			ChargeRecord chargeRecord = entities.ChargeRecord.OrderByDescending((ChargeRecord m) => m.TimeChargeID).FirstOrDefault((ChargeRecord m) => m.TransactionID == (int?)checkFeeArgs.TransactionID && m.IsDelete == false);
			if (chargeRecord != null)
			{
				checkFeeReturn.IsPaid = true;
				CompanyInfo companyInfo = entities.CompanyInfo.FirstOrDefault();
				if (chargeRecord.ChargeTime.AddMinutes(companyInfo.ExitTimeOutMin) <= now)
				{
					checkFeeReturn.IsTimeout = true;
				}
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		return checkFeeReturn;
	}

	public static CorrectLicensePlateReturn CorrectLicensePlate(CorrectLicensePlateArgs args)
	{
		CorrectLicensePlateReturn correctLicensePlateReturn = new CorrectLicensePlateReturn();
		correctLicensePlateReturn.ISOK = false;
		string empty = string.Empty;
		DbTransaction dbTransaction = null;
		try
		{
			using Entities entities = DBContext.NewContext;
			DbConnection connection = entities.Connection;
			connection.Open();
			dbTransaction = connection.BeginTransaction();
			if (args.TransactionDataID <= 0)
			{
				throw new Exception("TransactionDataID_NULL");
			}
			if (string.IsNullOrWhiteSpace(args.NewLicensePlate))
			{
				throw new Exception("New_LicensePlate_NULL");
			}
			TransactionData transactionData = entities.TransactionData.FirstOrDefault((TransactionData m) => m.TransactionID == args.TransactionDataID);
			TransactionData transactionData2 = entities.TransactionData.FirstOrDefault((TransactionData m) => m.InCardCode == args.NewLicensePlate && m.TransactionStatus == 1);
			if (transactionData == null)
			{
				throw new Exception("Vehicle_Not_In");
			}
			if (transactionData.InCardCode == args.NewLicensePlate || transactionData2 != null)
			{
				throw new Exception("Already_In");
			}
			empty = transactionData.InCardCode;
			transactionData.InCardCode = args.NewLicensePlate;
			entities.SaveChanges();
			PassTrace enterPass = entities.PassTrace.FirstOrDefault((PassTrace m) => m.TransactionID == (int?)args.TransactionDataID && m.PassDirection == 0);
			if (enterPass == null)
			{
				throw new Exception("Vehicle_Not_In");
			}
			enterPass.PassCardCode = args.NewLicensePlate;
			entities.SaveChanges();
			entities.LicensePlate_PassTrace.FirstOrDefault((LicensePlate_PassTrace m) => m.PassTraceID == enterPass.PassTraceID).AnalysisResult = args.NewLicensePlate;
			entities.SaveChanges();
			ShiftRecord shiftRecord = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => m.IsComplete == false && m.FromStation == args.PayStationName);
			StaffOperat entity = new StaffOperat
			{
				OperationCode = 52,
				OperationTime = DateTime.Now,
				Remark = $"Old LicensePlate={empty}  New LicensePlate={args.NewLicensePlate}",
				StaffCode = args.StaffCode,
				ShiftID = (shiftRecord?.ShiftID ?? 0)
			};
			entities.StaffOperat.AddObject(entity);
			entities.SaveChanges();
			dbTransaction.Commit();
			correctLicensePlateReturn.ISOK = true;
		}
		catch (Exception ex)
		{
			correctLicensePlateReturn.ErrCode = ex.Message;
			Logger.Error(ex);
			try
			{
				dbTransaction.Rollback();
			}
			catch (Exception ex2)
			{
				ILog logger = Logger;
				string text = "Rollback Err:";
				logger.Error(text + ex2);
			}
		}
		return correctLicensePlateReturn;
	}

	public static InParkReturn IsInPark(InParkArgs args)
	{
		InParkReturn inParkReturn = new InParkReturn();
		inParkReturn.IsOK = false;
		try
		{
			using Entities entities = DBContext.NewContext;
			if (string.IsNullOrWhiteSpace(args.Cardcode))
			{
				throw new Exception("Cardcode_NULL");
			}
			if (entities.TransactionData.FirstOrDefault((TransactionData m) => m.InCardCode == args.Cardcode && m.TransactionStatus == 1) != null)
			{
				throw new Exception("Already_In");
			}
			inParkReturn.IsOK = true;
		}
		catch (Exception ex)
		{
			inParkReturn.ErrCode = ex.Message;
			Logger.Error(ex);
		}
		return inParkReturn;
	}

	public static bool Ping(string arg)
	{
		bool result = false;
		try
		{
			string empty = string.Empty;
			arg = arg.Replace("\\", "");
			arg = arg.Replace("Pic", "");
			arg = arg.Replace("PIC", "");
			arg = arg.Replace("pic", "");
			empty = arg;
			if (new Ping().Send(empty).Status == IPStatus.Success)
			{
				Console.WriteLine("OK");
				result = true;
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		return result;
	}

	public static InParkReturn CheckMonthInpark(string plate)
	{
		InParkReturn inParkReturn = new InParkReturn();
		inParkReturn.IsOK = false;
		try
		{
			using Entities entities = DBContext.NewContext;
			if (string.IsNullOrWhiteSpace(plate))
			{
				throw new Exception("Cardcode_NULL");
			}
			if (entities.TransactionData.FirstOrDefault((TransactionData m) => m.InCardCode == plate && m.TransactionStatus == 1 && m.RentalType != (int?)null) != null)
			{
				throw new Exception("Already_In");
			}
			inParkReturn.IsOK = true;
		}
		catch (Exception ex)
		{
			inParkReturn.ErrCode = ex.Message;
			Logger.Error(ex);
		}
		return inParkReturn;
	}

	public static bool SetFreeRecord(int tenatID, int freeTypeID, int transactionID)
	{
		bool result = false;
		DateTime now = DateTime.Now;
		try
		{
			using Entities entities = DBContext.NewContext;
			CloudDiscount cloudDiscount = entities.CloudDiscount.OrderByDescending((CloudDiscount m) => m.ID).FirstOrDefault((CloudDiscount m) => m.TransactionDataID == (int?)transactionID && m.DiscountStatus == (int?)0);
			if (cloudDiscount != null)
			{
				cloudDiscount.CreateTime = now;
				cloudDiscount.CustomFreeTenatID = tenatID;
				cloudDiscount.CustomFreeTypeID = freeTypeID;
				entities.SaveChanges();
			}
			else
			{
				cloudDiscount = new CloudDiscount
				{
					CreateTime = now,
					CustomFreeTenatID = tenatID,
					CustomFreeTypeID = freeTypeID,
					DiscountID = 0,
					TransactionDataID = transactionID,
					DiscountStatus = 0
				};
				entities.CloudDiscount.AddObject(cloudDiscount);
				entities.SaveChanges();
			}
			result = true;
		}
		catch (Exception message)
		{
			Logger.Error(message);
			result = false;
		}
		return result;
	}

	public static void DelFreeRecord(int transactionID)
	{
		_ = DateTime.Now;
		try
		{
			using Entities entities = DBContext.NewContext;
			CloudDiscount cloudDiscount = entities.CloudDiscount.OrderByDescending((CloudDiscount m) => m.ID).FirstOrDefault((CloudDiscount m) => m.TransactionDataID == (int?)transactionID && m.DiscountStatus == (int?)0);
			if (cloudDiscount != null)
			{
				entities.DeleteObject(cloudDiscount);
				entities.SaveChanges();
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void LogOperation(int operatType, string remark, string staffcode, string paystation)
	{
		DbTransaction dbTransaction = null;
		DateTime startTime = default(DateTime);
		try
		{
			using Entities entities = DBContext.NewContext;
			DbConnection connection = entities.Connection;
			connection.Open();
			dbTransaction = connection.BeginTransaction();
			ShiftRecord shiftRecord = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => m.FromStation == paystation && m.IsComplete == false);
			if (shiftRecord == null)
			{
				shiftRecord = new ShiftRecord
				{
					IsComplete = false,
					StartTime = startTime,
					FromStation = paystation,
					StartStaffCode = staffcode
				};
				entities.ShiftRecord.AddObject(shiftRecord);
				entities.SaveChanges();
			}
			StaffOperat entity = new StaffOperat
			{
				OperationCode = operatType,
				OperationTime = DateTime.Now,
				Remark = remark,
				StaffCode = staffcode,
				ShiftID = shiftRecord.ShiftID
			};
			entities.StaffOperat.AddObject(entity);
			entities.SaveChanges();
			dbTransaction.Commit();
		}
		catch (Exception message)
		{
			Logger.Error(message);
			try
			{
				dbTransaction?.Rollback();
			}
			catch (Exception)
			{
			}
		}
	}

	public static string GetView_TransactionAndLP_IMAGE(int transactionID)
	{
		string result = string.Empty;
		try
		{
			using Entities entities = DBContext.NewContext;
			view_transactionandlp view_transactionandlp2 = entities.view_transactionandlp.Where((view_transactionandlp list) => list.TransactionBillType == 12 && list.TransactionID == transactionID).FirstOrDefault();
			if (view_transactionandlp2 != null)
			{
				result = view_transactionandlp2.ImagePath;
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
		return result;
	}

	static LPDBHelper()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public static view_transactionandlp GetViewTran(int tranID)
	{
		try
		{
			using Entities entities = DBContext.NewContext;
			view_transactionandlp view_transactionandlp2 = entities.view_transactionandlp.FirstOrDefault((view_transactionandlp m) => m.TransactionID == tranID);
			if (view_transactionandlp2 != null && view_transactionandlp2.InCardCode != "")
			{
				return view_transactionandlp2;
			}
			return null;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static bool AddChargerecord(ChargeRecord m_charge, MPass_POS_Transaction_Detail m_mpass, string staffcode, string paystation)
	{
		bool result = false;
		DbTransaction dbTransaction = null;
		DateTime startTime = default(DateTime);
		try
		{
			using Entities entities = DBContext.NewContext;
			DbConnection connection = entities.Connection;
			connection.Open();
			dbTransaction = connection.BeginTransaction();
			ShiftRecord shiftRecord = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => m.FromStation == paystation && m.IsComplete == false);
			if (shiftRecord == null)
			{
				shiftRecord = new ShiftRecord
				{
					IsComplete = false,
					StartTime = startTime,
					FromStation = paystation,
					StartStaffCode = staffcode
				};
				entities.ShiftRecord.AddObject(shiftRecord);
				entities.SaveChanges();
			}
			string remark = ((m_charge.PayType == 1) ? $"MPCARD扣費 {m_charge.TotalCharge}" : $"MPAY扣費 {m_charge.TotalCharge}");
			StaffOperat entity = new StaffOperat
			{
				OperationCode = 998,
				OperationTime = DateTime.Now,
				Remark = remark,
				StaffCode = staffcode,
				ShiftID = shiftRecord.ShiftID
			};
			entities.StaffOperat.AddObject(entity);
			entities.SaveChanges();
			m_charge.StaffCode = staffcode;
			m_charge.ShiftID = shiftRecord.ShiftID;
			entities.ChargeRecord.AddObject(m_charge);
			entities.SaveChanges();
			m_mpass.ChargeTransactionID = m_charge.TimeChargeID;
			entities.MPass_POS_Transaction_Detail.AddObject(m_mpass);
			entities.SaveChanges();
			dbTransaction.Commit();
			result = true;
		}
		catch (Exception message)
		{
			Logger.Error(message);
			try
			{
				dbTransaction?.Rollback();
			}
			catch (Exception)
			{
			}
		}
		return result;
	}

	public static string GetTransactionIDByLastPassGateID2()
	{
		string result = string.Empty;
		try
		{
			using Entities entities = DBContext.NewContext;
			PassTrace passTrace = (from l in entities.PassTrace
				where l.PassGateID == 2
				orderby l.PassTraceID descending
				select l).FirstOrDefault();
			if (passTrace != null && !passTrace.TransactionID.HasValue)
			{
				string text = (from p in entities.PassTrace
					where p.PassCardCode == passTrace.PassCardCode && p.ParkTypeID == passTrace.ParkTypeID && p.PassStatus == 2 && p.PassGateID == 1
					orderby p.PassTraceID descending
					select p).FirstOrDefault()?.TransactionID?.ToString();
				result = text ?? string.Empty;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("错误：" + ex.Message + "\n" + ex.StackTrace, "系统异常", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return result;
	}

	public static Tuple<string, string> GetAnalysisResultByPassTraceID(int passTraceID, string passCardcode, int parkTypeId)
	{
		string item = string.Empty;
		string item2;
		try
		{
			using Entities entities = DBContext.NewContext;
			LicensePlate_PassTrace licensePlate_PassTrace = entities.LicensePlate_PassTrace.FirstOrDefault((LicensePlate_PassTrace l) => l.PassTraceID == passTraceID);
			if (licensePlate_PassTrace != null)
			{
				item2 = licensePlate_PassTrace.AnalysisResult ?? string.Empty;
				item = licensePlate_PassTrace.ImagePath;
			}
			else
			{
				PassTrace passTrace = (from p in entities.PassTrace
					where p.PassCardCode == passCardcode && p.ParkTypeID == parkTypeId && p.PassStatus == 2
					orderby p.PassTraceID descending
					select p).FirstOrDefault();
				if (passTrace != null)
				{
					licensePlate_PassTrace = entities.LicensePlate_PassTrace.FirstOrDefault((LicensePlate_PassTrace l) => l.PassTraceID == passTrace.PassTraceID);
					item2 = licensePlate_PassTrace?.AnalysisResult ?? string.Empty;
					item = licensePlate_PassTrace?.ImagePath ?? string.Empty;
				}
				else
				{
					item2 = string.Empty;
				}
			}
		}
		catch
		{
			item2 = string.Empty;
		}
		return Tuple.Create(item2, item);
	}

	public static Tuple<int?, int?, string> CheckFreeRegisterExists(string carNumber, int parkTypeId, DateTime passTime)
	{
		int? item = null;
		int? customFreeID = null;
		string item2 = null;
		try
		{
			using Entities entities = DBContext.NewContext;
			string parkTypeIdString = parkTypeId.ToString();
			FreeRegister freeRegister = entities.FreeRegister.Where((FreeRegister fr) => fr.CarNumber == carNumber && fr.CreateStaff == parkTypeIdString && passTime > fr.StartDate && passTime < fr.ExpireDate).FirstOrDefault();
			if (freeRegister != null)
			{
				item = freeRegister.TenatID;
				customFreeID = freeRegister.CustomFreeTypeID;
				CustomFreeType customFreeType = entities.CustomFreeType.FirstOrDefault((CustomFreeType cft) => (int?)cft.CustomFreeTypeID == customFreeID);
				if (customFreeType != null)
				{
					item2 = customFreeType.CustomFreeNameCn;
				}
			}
		}
		catch (Exception exception)
		{
			Logger.Error("Error checking FreeRegister existence", exception);
		}
		return Tuple.Create(item, customFreeID, item2);
	}

	public static bool CheckRent(string carNumber, int parkTypeId, DateTime passTime)
	{
		try
		{
			using Entities entities = DBContext.NewContext;
			return (from card in entities.Card
				join rentalType in entities.RentalType on card.RentalTypeID equals rentalType.RentalTypeID
				where card.LicensePlate.Contains(carNumber) && rentalType.ParkTypeID == parkTypeId && card.StartDate < passTime && card.ExpireDate > passTime && !card.IsDelete
				select card).Any();
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static bool IsRecentPassMatched(string passCardcode, int parkTypeId, DateTime passtime, int passGateId)
	{
		try
		{
			using Entities entities = DBContext.NewContext;
			foreach (PassTrace trace in entities.PassTrace.OrderByDescending((PassTrace p) => p.PassTime).Take(20).ToList())
			{
				if (trace.ParkTypeID == parkTypeId && trace.PassStatus == 2 && trace.PassGateID == passGateId && entities.LicensePlate_PassTrace.FirstOrDefault((LicensePlate_PassTrace lp) => lp.PassTraceID == trace.PassTraceID && lp.AnalysisResult == passCardcode) != null)
				{
					double num = Math.Abs((passtime - trace.PassTime).TotalSeconds);
					if (num < 180.0 && num > 12.0)
					{
						return true;
					}
				}
			}
		}
		catch (Exception)
		{
		}
		return false;
	}

	public static string GetTotalChargeByTransactionID(int transactionID)
	{
		string text = string.Empty;
		try
		{
			using Entities entities = DBContext.NewContext;
			ChargeRecord chargeRecord = (from c in entities.ChargeRecord
				where c.TransactionID == (int?)transactionID
				orderby c.TimeChargeID descending
				select c).FirstOrDefault();
			if (chargeRecord != null)
			{
				decimal? num = chargeRecord.TotalCharge;
				decimal? num2 = chargeRecord.FreeCharge;
				if (num.HasValue)
				{
					text = num.Value.ToString("F2");
					if (num2.HasValue && num2.Value > 0m)
					{
						text = text + "；減免:" + num2.Value.ToString("F2");
					}
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("错误：" + ex.Message + "\n" + ex.StackTrace, "系统异常", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return text;
	}

	public static bool CheckMonthIn(string plate, int parkTypeId)
	{
		try
		{
			using Entities entities = DBContext.NewContext;
			if (string.IsNullOrWhiteSpace(plate))
			{
				throw new Exception("Cardcode_NULL");
			}
			Card card = entities.Card.FirstOrDefault((Card c) => c.Status == 1 && c.LicensePlate.Contains(plate));
			if (card == null)
			{
				return false;
			}
			int rentalTypeId = card.RentalTypeID;
			RentalType rentalType = entities.RentalType.FirstOrDefault((RentalType rt) => rt.RentalTypeID == rentalTypeId);
			if (rentalType == null)
			{
				return false;
			}
			if (rentalType.ParkTypeID != parkTypeId)
			{
				return false;
			}
			TransactionData transaction = entities.TransactionData.FirstOrDefault((TransactionData td) => td.InCardCode == card.CardCode && td.TransactionStatus == 1);
			if (transaction == null)
			{
				return false;
			}
			PassTrace passTrace = entities.PassTrace.FirstOrDefault((PassTrace pt) => pt.TransactionID == (int?)transaction.TransactionID);
			if (passTrace == null)
			{
				return false;
			}
			LicensePlate_PassTrace licensePlate_PassTrace = entities.LicensePlate_PassTrace.FirstOrDefault((LicensePlate_PassTrace lp) => lp.PassTraceID == passTrace.PassTraceID);
			return licensePlate_PassTrace != null && licensePlate_PassTrace.AnalysisResult == plate;
		}
		catch (Exception exception)
		{
			Logger.Error("An error occurred in CheckMonthIn.", exception);
			return false;
		}
	}
}
