using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using CarPark.DB;
using Master.SystemCommunication.Lib;

namespace CarPark2018;

public class DBHelper
{
	private static DBType conType = DBType.SQLite;

	public static string constr = "Data Source =127.0.0.1;Initial Catalog = CarparkData;User Id = sa;Password = 123456;";

	private static string assemblyName = "CarPark.DB";

	private static string AppPath = "Data Source=" + Application.StartupPath + "\\LocalCarparkData.DB";

	private static IDbConnection GetConnection()
	{
		IDbConnection dbConnection = null;
		return conType switch
		{
			DBType.SQLServer => new SqlConnection(constr), 
			DBType.Oracle => new OracleConnection(constr), 
			DBType.OleDb => new OleDbConnection(constr), 
			DBType.ODBC => new OdbcConnection(constr), 
			DBType.SQLite => new SQLiteConnection(AppPath), 
			_ => new SqlConnection(constr), 
		};
	}

	private static IDbCommand GetCommand(string commandText, CommandType commandType, IDbConnection con, params IDbDataParameter[] param)
	{
		IDbCommand dbCommand = null;
		dbCommand = conType switch
		{
			DBType.SQLServer => new SqlCommand(commandText, con as SqlConnection), 
			DBType.Oracle => new OracleCommand(commandText, con as OracleConnection), 
			DBType.OleDb => new OleDbCommand(commandText, con as OleDbConnection), 
			DBType.ODBC => new OdbcCommand(commandText, con as OdbcConnection), 
			DBType.SQLite => new SQLiteCommand(commandText, con as SQLiteConnection), 
			_ => new SqlCommand(commandText, con as SqlConnection), 
		};
		dbCommand.CommandType = commandType;
		if (param != null)
		{
			dbCommand.Parameters.Add(param);
		}
		return dbCommand;
	}

	private static T ExexuteDataReader<T>(IDataReader reader)
	{
		string text = string.Empty;
		T val = default(T);
		try
		{
			Type typeFromHandle = typeof(T);
			val = (T)Assembly.Load(assemblyName).CreateInstance(assemblyName + "." + typeFromHandle.Name);
			PropertyInfo[] properties = typeFromHandle.GetProperties();
			PropertyInfo[] array = properties;
			foreach (PropertyInfo propertyInfo in array)
			{
				for (int j = 0; j < reader.FieldCount; j++)
				{
					string name = reader.GetName(j);
					if (!(name.ToLower() == propertyInfo.Name.ToLower()))
					{
						continue;
					}
					object obj = reader[propertyInfo.Name];
					if (obj != null && obj != DBNull.Value)
					{
						string fullName = propertyInfo.GetGetMethod().ReturnParameter.ParameterType.FullName;
						text = propertyInfo.Name;
						string text2 = "Description";
						if (text == text2)
						{
							Console.WriteLine("");
						}
						if (fullName.Contains("Int32"))
						{
							obj = Convert.ToInt32(obj);
						}
						else if (fullName.Contains("Decimal"))
						{
							obj = decimal.Parse(obj.ToString());
						}
						else if (fullName.Contains("Boolean"))
						{
							obj = Convert.ToBoolean(obj);
						}
						else if (fullName.Contains("DateTime"))
						{
							obj = Convert.ToDateTime(obj);
						}
						propertyInfo.SetValue(val, obj, null);
						break;
					}
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(text);
			throw ex;
		}
		return val;
	}

	public static int ExecuteScalar(string commandText, CommandType commandType, params IDbDataParameter[] param)
	{
		int result = 0;
		try
		{
			IDbConnection connection = GetConnection();
			IDbCommand command = GetCommand(commandText, commandType, connection, param);
			using (connection)
			{
				using (command)
				{
					connection.Open();
					result = Convert.ToInt32(command.ExecuteScalar());
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
		return result;
	}

	public static int ExecuteNonQuery(string commandText, CommandType commandType, params IDbDataParameter[] param)
	{
		int result = 0;
		try
		{
			IDbConnection connection = GetConnection();
			IDbCommand command = GetCommand(commandText, commandType, connection, param);
			using (connection)
			{
				using (command)
				{
					connection.Open();
					IDbTransaction dbTransaction = (command.Transaction = connection.BeginTransaction());
					try
					{
						result = Convert.ToInt32(command.ExecuteNonQuery());
						dbTransaction.Commit();
					}
					catch (Exception ex)
					{
						dbTransaction.Rollback();
						throw ex;
					}
				}
			}
		}
		catch (Exception ex2)
		{
			throw ex2;
		}
		return result;
	}

	public static int ExecuteNonQuery(string[] commandText, CommandType commandType, params IDbDataParameter[] param)
	{
		int num = 0;
		try
		{
			IDbConnection connection = GetConnection();
			IDbCommand command = GetCommand(commandText[0], commandType, connection, param);
			using (connection)
			{
				using (command)
				{
					connection.Open();
					IDbTransaction dbTransaction = (command.Transaction = connection.BeginTransaction());
					try
					{
						foreach (string commandText2 in commandText)
						{
							command.CommandText = commandText2;
							num += Convert.ToInt32(command.ExecuteNonQuery());
						}
						dbTransaction.Commit();
					}
					catch (Exception ex)
					{
						dbTransaction.Rollback();
						throw ex;
					}
				}
			}
		}
		catch (Exception ex2)
		{
			throw ex2;
		}
		return num;
	}

	public static T ExexuteEntity<T>(string commandText, CommandType commandType, params IDbDataParameter[] param)
	{
		T result = default(T);
		try
		{
			IDbConnection connection = GetConnection();
			IDbCommand command = GetCommand(commandText, commandType, connection, param);
			using (connection)
			{
				using (command)
				{
					connection.Open();
					IDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
					while (dataReader.Read())
					{
						result = ExexuteDataReader<T>(dataReader);
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
		return result;
	}

	public static List<T> ExecuteList<T>(string commandText, CommandType commandType, params IDbDataParameter[] param)
	{
		List<T> list = new List<T>();
		try
		{
			IDbConnection connection = GetConnection();
			IDbCommand command = GetCommand(commandText, commandType, connection, param);
			using (connection)
			{
				using (command)
				{
					connection.Open();
					IDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
					while (dataReader.Read())
					{
						T item = ExexuteDataReader<T>(dataReader);
						list.Add(item);
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
		return list;
	}

	public static void Insert(string CardCode, ChargeRecord m_c, MPass_POS_Transaction_Detail m_m, BOC_Gate_TransactionExtend m_b, SaveChargeRecordArgs m_s, BOC_N910_POS_Card_Payment_DetailEX m_b9, ScanPayment m_sp)
	{
		try
		{
			if (InsertChargeRecord(CardCode, m_c) <= 0)
			{
				return;
			}
			ChargeRecord chargeRecord = SelectChargeRecord(CardCode);
			if (chargeRecord != null)
			{
				if (m_s != null)
				{
					SaveChargeRecordArgsEX saveChargeRecordArgsEX = new SaveChargeRecordArgsEX();
					saveChargeRecordArgsEX.CustomFreeID = m_s.CustomFreeID;
					saveChargeRecordArgsEX.CustomFreeRecordRemark = m_s.CustomFreeRecordRemark;
					saveChargeRecordArgsEX.CustomFreeTenatID = m_s.CustomFreeTenatID;
					saveChargeRecordArgsEX.FreeImagePath = m_s.FreeImagePath;
					saveChargeRecordArgsEX.InTime = m_s.InTime;
					saveChargeRecordArgsEX.ISTimeOut = m_s.ISTimeOut;
					saveChargeRecordArgsEX.TicketNumber = m_s.TicketNumber;
					saveChargeRecordArgsEX.TimeChargeID = chargeRecord.TimeChargeID;
					saveChargeRecordArgsEX.BarCode = m_s.BarCode;
					InsertSaveChargeRecordArgsEX(saveChargeRecordArgsEX);
				}
				if (m_m != null)
				{
					InsertMPass_POS_Transaction_Detail(m_m, chargeRecord.TimeChargeID);
				}
				else if (m_b != null)
				{
					InsertBOC_Gate_TransactionExtend(m_b, chargeRecord.TimeChargeID);
				}
				else if (m_b9 != null)
				{
					InsertBOC_N910_POS_Card_Payment_DetailEX(m_b9, chargeRecord.TimeChargeID);
				}
				else if (m_sp != null)
				{
					Insert_Scan_Payment_Detail(m_sp, chargeRecord.TimeChargeID);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public static void Delete(string CardCode)
	{
		try
		{
			ChargeRecord chargeRecord = SelectChargeRecord(CardCode);
			if (chargeRecord != null)
			{
				DeleteChargeRecord(chargeRecord.CardCode);
				DeleteMPass_POS_Transaction_Detail(chargeRecord.TimeChargeID);
				DeleteBOC_Gate_TransactionExtend(chargeRecord.TimeChargeID);
				DeleteScan_Payment_Detail(chargeRecord.TimeChargeID);
				DeleteBOC_N910_POS_Card_Payment_DetailEX(chargeRecord.TimeChargeID);
				DeleteSaveChargeRecordArgsEX(chargeRecord.TimeChargeID);
			}
		}
		catch (Exception)
		{
		}
	}

	public static int InsertChargeRecord(string CardCode, ChargeRecord m_ChargeRecordEX)
	{
		try
		{
			string commandText = "INSERT INTO chargerecord (ChargeTime, StaffCode, TotalCharge, FreeMin, FreeCharge, TransactionID, BillType, ShiftID, ChargeMin, ParkMin, CardCode, ParkTypeID, Remark, FromStation, PeriodofTime, BufferTime, Currency, FirstTime, Fine, IsDelete,PayType,subPayType,AuthCode) VALUES ( '" + m_ChargeRecordEX.ChargeTime.ToString() + "',  '" + m_ChargeRecordEX.StaffCode + "',  " + m_ChargeRecordEX.TotalCharge + ",  " + m_ChargeRecordEX.FreeMin + ",  " + m_ChargeRecordEX.FreeCharge + ",  " + m_ChargeRecordEX.TransactionID + ",  " + m_ChargeRecordEX.BillType + ",  " + m_ChargeRecordEX.ShiftID + ",  " + m_ChargeRecordEX.ChargeMin + ",  " + m_ChargeRecordEX.ParkMin + ",  '" + CardCode + "',  " + m_ChargeRecordEX.ParkTypeID + ",  '" + m_ChargeRecordEX.Remark + "',  '" + m_ChargeRecordEX.FromStation + "',  '" + m_ChargeRecordEX.PeriodofTime + "', " + m_ChargeRecordEX.BufferTime + ",  '" + m_ChargeRecordEX.Currency + "',  '" + m_ChargeRecordEX.FirstTime + "',  " + m_ChargeRecordEX.Fine + ", '" + m_ChargeRecordEX.IsDelete + "'," + m_ChargeRecordEX.PayType + "," + m_ChargeRecordEX.subPayType + ",'" + m_ChargeRecordEX.AuthCode + "' );";
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public static int InsertMPass_POS_Transaction_Detail(MPass_POS_Transaction_Detail m_MPass_POS_Transaction_DetailEX, int TimeChargeID)
	{
		try
		{
			string commandText = "INSERT INTO mpass_pos_transaction_detail (TransactionID, ChargeTransactionID, TransactionTime, STATUS, TERMINALID, MERCHANTID, INVOICENO, TXNDATE, TXNTIME, TXNTYPE, CARDTYPE, MPTXNTYPE, PAN, TOTALAMT, BALANCE,LOGNO, M1CARDTYPE, ORIGBALANCE, DEPOSIT, CHARGE, TXNTAC, TXNNO, PURSETYPE, EXPDATE,EPA, EPAAMT, BATCHNO, VOCNO, REFNO, AUTH, AID, TC, TVR, TSI, ATC, APPLAB, TransactionType, CommandResult, ErrDescription, RETCODE, TransactionSEQ, CashType, PAY_MODE,PAY_ACCOUNT,PAY_PURSETYPE,PAY_TOTALAMT,PAY_INVOICENO,DISCOUNTAMT,ORIGTXNAMT,DISCOUNTTYPE, COUPONTYPE,COUPONCODE,COUPONDEDAMT,ACTUALAMT,MERCHANTORDERNO) VALUES (" + m_MPass_POS_Transaction_DetailEX.TransactionID + ", " + TimeChargeID + ", '" + m_MPass_POS_Transaction_DetailEX.TransactionTime.ToString() + "', '" + m_MPass_POS_Transaction_DetailEX.STATUS + "', '" + m_MPass_POS_Transaction_DetailEX.TERMINALID + "', '" + m_MPass_POS_Transaction_DetailEX.MERCHANTID + "','" + m_MPass_POS_Transaction_DetailEX.INVOICENO + "', '" + m_MPass_POS_Transaction_DetailEX.TXNDATE + "', '" + m_MPass_POS_Transaction_DetailEX.TXNTIME + "', '" + m_MPass_POS_Transaction_DetailEX.TXNTYPE + "', '" + m_MPass_POS_Transaction_DetailEX.CARDTYPE + "', '" + m_MPass_POS_Transaction_DetailEX.MPTXNTYPE + "', '" + m_MPass_POS_Transaction_DetailEX.PAN + "', " + m_MPass_POS_Transaction_DetailEX.TOTALAMT + ", " + m_MPass_POS_Transaction_DetailEX.BALANCE + ", '" + m_MPass_POS_Transaction_DetailEX.LOGNO + "', '" + m_MPass_POS_Transaction_DetailEX.M1CARDTYPE + "', " + m_MPass_POS_Transaction_DetailEX.ORIGBALANCE + ", " + m_MPass_POS_Transaction_DetailEX.DEPOSIT + ", " + m_MPass_POS_Transaction_DetailEX.CHARGE + ", '" + m_MPass_POS_Transaction_DetailEX.TXNTAC + "', '" + m_MPass_POS_Transaction_DetailEX.TXNNO + "', '" + m_MPass_POS_Transaction_DetailEX.PURSETYPE + "', '" + m_MPass_POS_Transaction_DetailEX.EXPDATE + "', " + m_MPass_POS_Transaction_DetailEX.EPA + ", " + m_MPass_POS_Transaction_DetailEX.EPAAMT + ", '" + m_MPass_POS_Transaction_DetailEX.BATCHNO + "', '" + m_MPass_POS_Transaction_DetailEX.VOCNO + "', '" + m_MPass_POS_Transaction_DetailEX.REFNO + "', '" + m_MPass_POS_Transaction_DetailEX.AUTH + "', '" + m_MPass_POS_Transaction_DetailEX.AID + "', '" + m_MPass_POS_Transaction_DetailEX.TC + "', '" + m_MPass_POS_Transaction_DetailEX.TVR + "', '" + m_MPass_POS_Transaction_DetailEX.TSI + "', '" + m_MPass_POS_Transaction_DetailEX.ATC + "', '" + m_MPass_POS_Transaction_DetailEX.APPLAB + "', " + m_MPass_POS_Transaction_DetailEX.TransactionType + ", " + m_MPass_POS_Transaction_DetailEX.CommandResult + ", '" + m_MPass_POS_Transaction_DetailEX.ErrDescription + "', '" + m_MPass_POS_Transaction_DetailEX.RETCODE + "', '" + m_MPass_POS_Transaction_DetailEX.TransactionSEQ + "', '" + m_MPass_POS_Transaction_DetailEX.CashType + "', '" + m_MPass_POS_Transaction_DetailEX.PAY_MODE + "', '" + m_MPass_POS_Transaction_DetailEX.PAY_ACCOUNT + "', '" + m_MPass_POS_Transaction_DetailEX.PAY_PURSETYPE + "', " + m_MPass_POS_Transaction_DetailEX.PAY_TOTALAMT + ", '" + m_MPass_POS_Transaction_DetailEX.PAY_INVOICENO + "', " + m_MPass_POS_Transaction_DetailEX.DISCOUNTAMT + "," + m_MPass_POS_Transaction_DetailEX.ORIGTXNAMT + ", '" + m_MPass_POS_Transaction_DetailEX.DISCOUNTTYPE + "', '" + m_MPass_POS_Transaction_DetailEX.COUPONTYPE + "', '" + m_MPass_POS_Transaction_DetailEX.COUPONCODE + "', " + m_MPass_POS_Transaction_DetailEX.COUPONDEDAMT + ", " + m_MPass_POS_Transaction_DetailEX.ACTUALAMT + ", '" + m_MPass_POS_Transaction_DetailEX.MERCHANTORDERNO + "');";
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public static int InsertBOC_Gate_TransactionExtend(BOC_Gate_TransactionExtend m_BOC_Gate_TransactionExtendEX, int TimeChargeID)
	{
		try
		{
			string commandText = "INSERT INTO boc_gate_transactionextend (TransactionID, CardBillAmount, CardRemain, CardNumber, BillArea, TxnNo, BillDate, BillTime, ServerCode, LogicNo, DeviceCode, ReceiverCode, IC_Data, AlternateData, MD5, ReplyCode, ErrorCode, REQUEST_CARD_State, PURCHASE_CARD_State, CardPhyType, CardAppType, BillAreaB, OffLineRemain_MOP, OffLineRemain_RMB, Purchase_FullData, EncryptedCardNumber, Description, CardReaderNumber, StaffInfo, Valid, IsBlack, FromGateID, SysTransacionID, TransactionTime, ISUploaded, ChargeTransactionID) VALUES (" + m_BOC_Gate_TransactionExtendEX.TransactionID + ", " + m_BOC_Gate_TransactionExtendEX.CardBillAmount + ", " + m_BOC_Gate_TransactionExtendEX.CardRemain + ", '" + m_BOC_Gate_TransactionExtendEX.CardNumber + "', '" + m_BOC_Gate_TransactionExtendEX.BillArea + "', '" + m_BOC_Gate_TransactionExtendEX.TxnNo + "', '" + m_BOC_Gate_TransactionExtendEX.BillDate + "', '" + m_BOC_Gate_TransactionExtendEX.BillTime + "', '" + m_BOC_Gate_TransactionExtendEX.ServerCode + "', '" + m_BOC_Gate_TransactionExtendEX.LogicNo + "', '" + m_BOC_Gate_TransactionExtendEX.DeviceCode + "', '" + m_BOC_Gate_TransactionExtendEX.ReceiverCode + "', '" + m_BOC_Gate_TransactionExtendEX.IC_Data + "', '" + m_BOC_Gate_TransactionExtendEX.AlternateData + "', '" + m_BOC_Gate_TransactionExtendEX.MD5 + "', '" + m_BOC_Gate_TransactionExtendEX.ReplyCode + "', '" + m_BOC_Gate_TransactionExtendEX.ErrorCode + "', '" + m_BOC_Gate_TransactionExtendEX.REQUEST_CARD_State + "', '" + m_BOC_Gate_TransactionExtendEX.PURCHASE_CARD_State + "', '" + m_BOC_Gate_TransactionExtendEX.CardPhyType + ", '" + m_BOC_Gate_TransactionExtendEX.CardAppType + "', '" + m_BOC_Gate_TransactionExtendEX.BillAreaB + "', '" + m_BOC_Gate_TransactionExtendEX.OffLineRemain_MOP + "', '" + m_BOC_Gate_TransactionExtendEX.OffLineRemain_RMB + "', '" + m_BOC_Gate_TransactionExtendEX.Purchase_FullData + "', '" + m_BOC_Gate_TransactionExtendEX.EncryptedCardNumber + "', '" + m_BOC_Gate_TransactionExtendEX.Description + "', '" + m_BOC_Gate_TransactionExtendEX.CardReaderNumber + "', '" + m_BOC_Gate_TransactionExtendEX.StaffInfo + "', '" + m_BOC_Gate_TransactionExtendEX.Valid + "', '" + m_BOC_Gate_TransactionExtendEX.IsBlack + "', " + m_BOC_Gate_TransactionExtendEX.FromGateID + ", " + m_BOC_Gate_TransactionExtendEX.SysTransacionID + ", '" + m_BOC_Gate_TransactionExtendEX.TransactionTime.ToString() + "', '" + m_BOC_Gate_TransactionExtendEX.ISUploaded + "', '" + TimeChargeID + "');";
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static int InsertBOC_N910_POS_Card_Payment_DetailEX(BOC_N910_POS_Card_Payment_DetailEX m_BOC_N910_POS_Card_Payment_DetailEX, int TimeChargeID)
	{
		try
		{
			string commandText = "INSERT INTO BOC_N910_POS_Card_Payment_DetailEX (VERSION, CMD, STATUS, TXNDATE, TXNTIME, RCODE, MESSAGE, AMOUNT, AUTHCODE, PAN, EXPIRYDATE, ENTERMODE, TRACEID, TERMINALID, MERCHANTID, CARDTYPE, CARDHOLDERNAME, BATCHNO, REFERENCENO, ISSIGNATURE, REQUESTID, ChargeRecordID, TransactionID) VALUES ('" + m_BOC_N910_POS_Card_Payment_DetailEX.VERSION + "',' " + m_BOC_N910_POS_Card_Payment_DetailEX.CMD + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.STATUS + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.TXNDATE + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.TXNTIME + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.RCODE + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.MESSAGE + "', " + m_BOC_N910_POS_Card_Payment_DetailEX.AMOUNT + ", '" + m_BOC_N910_POS_Card_Payment_DetailEX.AUTHCODE + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.PAN + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.EXPIRYDATE + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.ENTERMODE + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.TRACEID + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.TERMINALID + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.MERCHANTID + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.CARDTYPE + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.CARDHOLDERNAME + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.BATCHNO + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.REFERENCENO + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.ISSIGNATURE + "', '" + m_BOC_N910_POS_Card_Payment_DetailEX.REQUESTID + "', " + TimeChargeID + ", " + m_BOC_N910_POS_Card_Payment_DetailEX.TransactionID + ");";
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static int Insert_Scan_Payment_Detail(ScanPayment m_Scan_Payment_DetailEX, int TimeChargeID)
	{
		try
		{
			string commandText = "INSERT INTO ScanPayment (merchantId, trmNo, payOrderNo,logNo,ChargeTime,subject,amount,authCode,payType,userData,ParkingLotNo,result, resultMessage,LastUpdateTime,ChargeRecordID, TransactionID) VALUES ('" + m_Scan_Payment_DetailEX.MerchantId + "','" + m_Scan_Payment_DetailEX.TrmNo + "','" + m_Scan_Payment_DetailEX.PayOrderNo + "', '" + m_Scan_Payment_DetailEX.LogNo + "', '" + m_Scan_Payment_DetailEX.ChargeTime + "', '" + m_Scan_Payment_DetailEX.Subject + "', " + m_Scan_Payment_DetailEX.Amount + ", '" + m_Scan_Payment_DetailEX.AuthCode + "', '" + m_Scan_Payment_DetailEX.PayType + "', '" + m_Scan_Payment_DetailEX.UserData + "', '" + m_Scan_Payment_DetailEX.ParkingLotNo + "', '" + m_Scan_Payment_DetailEX.Result + "', '" + m_Scan_Payment_DetailEX.ResultMessage + "', '" + m_Scan_Payment_DetailEX.LastUpdateTime + "', " + TimeChargeID + ", " + m_Scan_Payment_DetailEX.TransactionID + ");";
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static int InsertSaveChargeRecordArgsEX(SaveChargeRecordArgsEX m_SaveChargeRecordArgsEX)
	{
		try
		{
			string commandText = "INSERT INTO SaveChargeRecordArgs (TimeChargeID, CustomFreeID, CustomFreeRecordRemark, CustomFreeTenatID, FreeImagePath, ISTimeOut, InTime, TicketNumber, TransactionDataRemark,BarCode) VALUES (" + m_SaveChargeRecordArgsEX.TimeChargeID + "," + m_SaveChargeRecordArgsEX.CustomFreeID + ", '" + m_SaveChargeRecordArgsEX.CustomFreeRecordRemark + "', " + m_SaveChargeRecordArgsEX.CustomFreeTenatID + ", '" + m_SaveChargeRecordArgsEX.FreeImagePath + "', '" + m_SaveChargeRecordArgsEX.ISTimeOut + "', '" + m_SaveChargeRecordArgsEX.InTime.ToString() + "', '" + m_SaveChargeRecordArgsEX.TicketNumber + "', '" + m_SaveChargeRecordArgsEX.TransactionDataRemark + "','" + m_SaveChargeRecordArgsEX.BarCode + "');";
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static int DeleteMPass_POS_Transaction_Detail(int TimeChargeID)
	{
		try
		{
			string commandText = "delete from mpass_pos_transaction_detail where ChargeTransactionID = " + TimeChargeID;
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static int DeleteBOC_Gate_TransactionExtend(int TimeChargeID)
	{
		try
		{
			string commandText = "delete from boc_gate_transactionextend where ChargeTransactionID =  " + TimeChargeID;
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static int DeleteBOC_N910_POS_Card_Payment_DetailEX(int TimeChargeID)
	{
		try
		{
			string commandText = "delete from BOC_N910_POS_Card_Payment_DetailEX where ChargeRecordID =  " + TimeChargeID;
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static int DeleteScan_Payment_Detail(int TimeChargeID)
	{
		try
		{
			string commandText = "delete from ScanPayment where ChargeRecordID =  " + TimeChargeID;
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static int DeleteChargeRecord(string CardCode)
	{
		try
		{
			string commandText = "delete from chargerecord where cardcode =  '" + CardCode + "'";
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static int DeleteSaveChargeRecordArgsEX(int TimeChargeID)
	{
		try
		{
			string commandText = "delete from SaveChargeRecordArgs where timechargeid =  " + TimeChargeID;
			return ExecuteNonQuery(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			throw;
		}
	}

	public static ChargeRecord SelectChargeRecord(string CardCode)
	{
		try
		{
			string commandText = "select * from chargerecord where CardCode = '" + CardCode + "'";
			return ExexuteEntity<ChargeRecord>(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			return null;
		}
	}

	public static MPass_POS_Transaction_Detail SelectMPass_POS_Transaction_Detail(int TimeChargeID)
	{
		try
		{
			string commandText = "select * from mpass_pos_transaction_detail where ChargeTransactionID = " + TimeChargeID;
			return ExexuteEntity<MPass_POS_Transaction_Detail>(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			return null;
		}
	}

	public static BOC_Gate_TransactionExtend SelectBOC_Gate_TransactionExtend(int TimeChargeID)
	{
		try
		{
			string commandText = "select * from boc_gate_transactionextend where ChargeTransactionID = " + TimeChargeID;
			return ExexuteEntity<BOC_Gate_TransactionExtend>(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			return null;
		}
	}

	public static BOC_N910_POS_Card_Payment_DetailEX SelectBOC_N910_POS_Card_Payment_DetailEX(int TimeChargeID)
	{
		try
		{
			string commandText = "select * from BOC_N910_POS_Card_Payment_DetailEX where ChargeRecordID = " + TimeChargeID;
			return ExexuteEntity<BOC_N910_POS_Card_Payment_DetailEX>(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			return null;
		}
	}

	public static ScanPayment SelectScan_Payment_DetailEX(int TimeChargeID)
	{
		try
		{
			string commandText = "select * from ScanPayment where ChargeRecordID = " + TimeChargeID;
			return ExexuteEntity<ScanPayment>(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			return null;
		}
	}

	public static SaveChargeRecordArgsEX SelectSaveChargeRecordArgsEX(int TimeChargeID)
	{
		try
		{
			string commandText = "select * from SaveChargeRecordArgs where timechargeid =  " + TimeChargeID;
			return ExexuteEntity<SaveChargeRecordArgsEX>(commandText, CommandType.Text, (IDbDataParameter[])null);
		}
		catch
		{
			return null;
		}
	}

	public static SaveChargeRecordArgs SelectSaveChargeRecordArgs(int TimeChargeID)
	{
		try
		{
			string commandText = "select * from SaveChargeRecordArgs where timechargeid =  " + TimeChargeID;
			SaveChargeRecordArgsEX saveChargeRecordArgsEX = ExexuteSaveChargeRecordArgsEX(commandText, CommandType.Text, (IDbDataParameter[])null);
			SaveChargeRecordArgs saveChargeRecordArgs = new SaveChargeRecordArgs();
			saveChargeRecordArgs.CustomFreeID = saveChargeRecordArgsEX.CustomFreeID;
			saveChargeRecordArgs.CustomFreeRecordRemark = saveChargeRecordArgsEX.CustomFreeRecordRemark;
			saveChargeRecordArgs.CustomFreeTenatID = saveChargeRecordArgsEX.CustomFreeTenatID;
			saveChargeRecordArgs.FreeImagePath = saveChargeRecordArgsEX.FreeImagePath;
			saveChargeRecordArgs.InTime = saveChargeRecordArgsEX.InTime;
			saveChargeRecordArgs.ISTimeOut = saveChargeRecordArgsEX.ISTimeOut;
			saveChargeRecordArgs.TicketNumber = saveChargeRecordArgsEX.TicketNumber;
			saveChargeRecordArgs.TransactionDataRemark = saveChargeRecordArgsEX.TransactionDataRemark;
			saveChargeRecordArgs.BarCode = saveChargeRecordArgsEX.BarCode;
			return saveChargeRecordArgs;
		}
		catch
		{
			return null;
		}
	}

	public static SaveChargeRecordArgsEX ExexuteSaveChargeRecordArgsEX(string commandText, CommandType commandType, params IDbDataParameter[] param)
	{
		SaveChargeRecordArgsEX saveChargeRecordArgsEX = null;
		try
		{
			IDbConnection connection = GetConnection();
			IDbCommand command = GetCommand(commandText, commandType, connection, param);
			using (connection)
			{
				using (command)
				{
					connection.Open();
					IDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
					while (dataReader.Read())
					{
						if (saveChargeRecordArgsEX == null)
						{
							saveChargeRecordArgsEX = new SaveChargeRecordArgsEX();
						}
						saveChargeRecordArgsEX.CustomFreeID = Convert.ToInt32(dataReader["CustomFreeID"]);
						saveChargeRecordArgsEX.CustomFreeRecordRemark = dataReader["CustomFreeRecordRemark"].ToString();
						saveChargeRecordArgsEX.CustomFreeTenatID = Convert.ToInt32(dataReader["CustomFreeTenatID"]);
						saveChargeRecordArgsEX.FreeImagePath = dataReader["FreeImagePath"].ToString();
						saveChargeRecordArgsEX.InTime = Convert.ToDateTime(dataReader["InTime"]);
						saveChargeRecordArgsEX.SaveChargeRecordArgsID = Convert.ToInt32(dataReader["SaveChargeRecordArgsID"]);
						saveChargeRecordArgsEX.TicketNumber = dataReader["TicketNumber"].ToString();
						saveChargeRecordArgsEX.TimeChargeID = Convert.ToInt32(dataReader["TimeChargeID"]);
						saveChargeRecordArgsEX.TransactionDataRemark = dataReader["TransactionDataRemark"].ToString();
						saveChargeRecordArgsEX.BarCode = dataReader["BarCode"].ToString();
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
		return saveChargeRecordArgsEX;
	}
}
