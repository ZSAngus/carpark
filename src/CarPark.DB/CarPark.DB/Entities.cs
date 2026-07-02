using System.Data.EntityClient;
using System.Data.Objects;

namespace CarPark.DB;

public class Entities : ObjectContext
{
	private ObjectSet<AD_ExtCardExpiryDate> _AD_ExtCardExpiryDate;

	private ObjectSet<AD_SCSCP_CARD_DATA> _AD_SCSCP_CARD_DATA;

	private ObjectSet<AdminCharge> _AdminCharge;

	private ObjectSet<AppSetting> _AppSetting;

	private ObjectSet<APS_Alarm> _APS_Alarm;

	private ObjectSet<APS_CashBoxAcceptor> _APS_CashBoxAcceptor;

	private ObjectSet<APS_CashBoxDispenser> _APS_CashBoxDispenser;

	private ObjectSet<APS_CashBoxType> _APS_CashBoxType;

	private ObjectSet<APS_Certificate> _APS_Certificate;

	private ObjectSet<APS_CertificateType> _APS_CertificateType;

	private ObjectSet<APS_CertificateUseRecord> _APS_CertificateUseRecord;

	private ObjectSet<APS_CouponSetting> _APS_CouponSetting;

	private ObjectSet<APS_CurrencyInfo> _APS_CurrencyInfo;

	private ObjectSet<APS_FinishApsAcceptLog> _APS_FinishApsAcceptLog;

	private ObjectSet<APS_FinishApsDispenseLog> _APS_FinishApsDispenseLog;

	private ObjectSet<APS_MxtraSyslog> _APS_MxtraSyslog;

	private ObjectSet<APS_RedoTicket> _APS_RedoTicket;

	private ObjectSet<APS_ShiftRecordDetailed> _APS_ShiftRecordDetailed;

	private ObjectSet<APS_Station> _APS_Station;

	private ObjectSet<APS_Syslog> _APS_Syslog;

	private ObjectSet<APS_SystemSetting> _APS_SystemSetting;

	private ObjectSet<APS_TimeChargeRecord> _APS_TimeChargeRecord;

	private ObjectSet<barsys_Discount> _barsys_Discount;

	private ObjectSet<barsys_Discount_Batch> _barsys_Discount_Batch;

	private ObjectSet<barsys_Discount_Record> _barsys_Discount_Record;

	private ObjectSet<BatteryChargeRecord> _BatteryChargeRecord;

	private ObjectSet<BlackList> _BlackList;

	private ObjectSet<BOC_Gate_TransactionExtend> _BOC_Gate_TransactionExtend;

	private ObjectSet<BOC_Gate_TransactionExtend_PackData_File> _BOC_Gate_TransactionExtend_PackData_File;

	private ObjectSet<BOC_N910_POS_Card_Payment_DetailEX> _BOC_N910_POS_Card_Payment_DetailEX;

	private ObjectSet<BOC_Smart_Payment_DetailEX> _BOC_Smart_Payment_DetailEX;

	private ObjectSet<Card> _Card;

	private ObjectSet<CardType> _CardType;

	private ObjectSet<ChargeRecord> _ChargeRecord;

	private ObjectSet<CloudDiscount> _CloudDiscount;

	private ObjectSet<CompanyInfo> _CompanyInfo;

	private ObjectSet<Customer> _Customer;

	private ObjectSet<CustomFreeRecord> _CustomFreeRecord;

	private ObjectSet<CustomFreeTenat> _CustomFreeTenat;

	private ObjectSet<CustomFreeType> _CustomFreeType;

	private ObjectSet<FreeRegister> _FreeRegister;

	private ObjectSet<ICBC_Gate_TransactionExtend> _ICBC_Gate_TransactionExtend;

	private ObjectSet<ICBC_Gate_TransactionExtend_PackData_File> _ICBC_Gate_TransactionExtend_PackData_File;

	private ObjectSet<LicensePlate_PassTrace> _LicensePlate_PassTrace;

	private ObjectSet<MPass_Gate_Transaction> _MPass_Gate_Transaction;

	private ObjectSet<MPass_Gate_Transaction_PackData> _MPass_Gate_Transaction_PackData;

	private ObjectSet<MPass_Gate_Transaction_PackData_File> _MPass_Gate_Transaction_PackData_File;

	private ObjectSet<MPass_Gate_Transaction_SIFiles> _MPass_Gate_Transaction_SIFiles;

	private ObjectSet<MPass_Gate_Transaction_SIStrings> _MPass_Gate_Transaction_SIStrings;

	private ObjectSet<MPass_POS_Signin> _MPass_POS_Signin;

	private ObjectSet<MPass_POS_Signin_Detail> _MPass_POS_Signin_Detail;

	private ObjectSet<MPass_POS_Transaction_Detail> _MPass_POS_Transaction_Detail;

	private ObjectSet<NoFeelPayment> _NoFeelPayment;

	private ObjectSet<Park> _Park;

	private ObjectSet<ParkArea> _ParkArea;

	private ObjectSet<ParkAreaExtend> _ParkAreaExtend;

	private ObjectSet<ParkGate> _ParkGate;

	private ObjectSet<PassTrace> _PassTrace;

	private ObjectSet<PreOffer> _PreOffer;

	private ObjectSet<RentalType> _RentalType;

	private ObjectSet<RuleControl> _RuleControl;

	private ObjectSet<SalaryBlock> _SalaryBlock;

	private ObjectSet<ShiftRecord> _ShiftRecord;

	private ObjectSet<StaffInfo> _StaffInfo;

	private ObjectSet<StaffOperat> _StaffOperat;

	private ObjectSet<StaffType> _StaffType;

	private ObjectSet<Statistics_NotExitData> _Statistics_NotExitData;

	private ObjectSet<Statistics_UpdateUtil> _Statistics_UpdateUtil;

	private ObjectSet<Statistics_UseTime> _Statistics_UseTime;

	private ObjectSet<SysRole> _SysRole;

	private ObjectSet<SysRoleGroup> _SysRoleGroup;

	private ObjectSet<SysStaffRole> _SysStaffRole;

	private ObjectSet<TempStop> _TempStop;

	private ObjectSet<TempStopList> _TempStopList;

	private ObjectSet<TicketSeq> _TicketSeq;

	private ObjectSet<TimeCharge> _TimeCharge;

	private ObjectSet<TimeChargeExt> _TimeChargeExt;

	private ObjectSet<TransactionData> _TransactionData;

	private ObjectSet<TransactionDataFinished> _TransactionDataFinished;

	private ObjectSet<TypeDictionary> _TypeDictionary;

	private ObjectSet<UMRefNum> _UMRefNum;

	private ObjectSet<VoidCharge> _VoidCharge;

	private ObjectSet<view_parkintime2018> _view_parkintime2018;

	private ObjectSet<view_transactionandlp> _view_transactionandlp;

	private ObjectSet<view_transactiondata> _view_transactiondata;

	private ObjectSet<ScanPayment> _ScanPayment;

	public ObjectSet<AD_ExtCardExpiryDate> AD_ExtCardExpiryDate
	{
		get
		{
			if (_AD_ExtCardExpiryDate == null)
			{
				_AD_ExtCardExpiryDate = CreateObjectSet<AD_ExtCardExpiryDate>("AD_ExtCardExpiryDate");
			}
			return _AD_ExtCardExpiryDate;
		}
	}

	public ObjectSet<AD_SCSCP_CARD_DATA> AD_SCSCP_CARD_DATA
	{
		get
		{
			if (_AD_SCSCP_CARD_DATA == null)
			{
				_AD_SCSCP_CARD_DATA = CreateObjectSet<AD_SCSCP_CARD_DATA>("AD_SCSCP_CARD_DATA");
			}
			return _AD_SCSCP_CARD_DATA;
		}
	}

	public ObjectSet<AdminCharge> AdminCharge
	{
		get
		{
			if (_AdminCharge == null)
			{
				_AdminCharge = CreateObjectSet<AdminCharge>("AdminCharge");
			}
			return _AdminCharge;
		}
	}

	public ObjectSet<AppSetting> AppSetting
	{
		get
		{
			if (_AppSetting == null)
			{
				_AppSetting = CreateObjectSet<AppSetting>("AppSetting");
			}
			return _AppSetting;
		}
	}

	public ObjectSet<APS_Alarm> APS_Alarm
	{
		get
		{
			if (_APS_Alarm == null)
			{
				_APS_Alarm = CreateObjectSet<APS_Alarm>("APS_Alarm");
			}
			return _APS_Alarm;
		}
	}

	public ObjectSet<APS_CashBoxAcceptor> APS_CashBoxAcceptor
	{
		get
		{
			if (_APS_CashBoxAcceptor == null)
			{
				_APS_CashBoxAcceptor = CreateObjectSet<APS_CashBoxAcceptor>("APS_CashBoxAcceptor");
			}
			return _APS_CashBoxAcceptor;
		}
	}

	public ObjectSet<APS_CashBoxDispenser> APS_CashBoxDispenser
	{
		get
		{
			if (_APS_CashBoxDispenser == null)
			{
				_APS_CashBoxDispenser = CreateObjectSet<APS_CashBoxDispenser>("APS_CashBoxDispenser");
			}
			return _APS_CashBoxDispenser;
		}
	}

	public ObjectSet<APS_CashBoxType> APS_CashBoxType
	{
		get
		{
			if (_APS_CashBoxType == null)
			{
				_APS_CashBoxType = CreateObjectSet<APS_CashBoxType>("APS_CashBoxType");
			}
			return _APS_CashBoxType;
		}
	}

	public ObjectSet<APS_Certificate> APS_Certificate
	{
		get
		{
			if (_APS_Certificate == null)
			{
				_APS_Certificate = CreateObjectSet<APS_Certificate>("APS_Certificate");
			}
			return _APS_Certificate;
		}
	}

	public ObjectSet<APS_CertificateType> APS_CertificateType
	{
		get
		{
			if (_APS_CertificateType == null)
			{
				_APS_CertificateType = CreateObjectSet<APS_CertificateType>("APS_CertificateType");
			}
			return _APS_CertificateType;
		}
	}

	public ObjectSet<APS_CertificateUseRecord> APS_CertificateUseRecord
	{
		get
		{
			if (_APS_CertificateUseRecord == null)
			{
				_APS_CertificateUseRecord = CreateObjectSet<APS_CertificateUseRecord>("APS_CertificateUseRecord");
			}
			return _APS_CertificateUseRecord;
		}
	}

	public ObjectSet<APS_CouponSetting> APS_CouponSetting
	{
		get
		{
			if (_APS_CouponSetting == null)
			{
				_APS_CouponSetting = CreateObjectSet<APS_CouponSetting>("APS_CouponSetting");
			}
			return _APS_CouponSetting;
		}
	}

	public ObjectSet<APS_CurrencyInfo> APS_CurrencyInfo
	{
		get
		{
			if (_APS_CurrencyInfo == null)
			{
				_APS_CurrencyInfo = CreateObjectSet<APS_CurrencyInfo>("APS_CurrencyInfo");
			}
			return _APS_CurrencyInfo;
		}
	}

	public ObjectSet<APS_FinishApsAcceptLog> APS_FinishApsAcceptLog
	{
		get
		{
			if (_APS_FinishApsAcceptLog == null)
			{
				_APS_FinishApsAcceptLog = CreateObjectSet<APS_FinishApsAcceptLog>("APS_FinishApsAcceptLog");
			}
			return _APS_FinishApsAcceptLog;
		}
	}

	public ObjectSet<APS_FinishApsDispenseLog> APS_FinishApsDispenseLog
	{
		get
		{
			if (_APS_FinishApsDispenseLog == null)
			{
				_APS_FinishApsDispenseLog = CreateObjectSet<APS_FinishApsDispenseLog>("APS_FinishApsDispenseLog");
			}
			return _APS_FinishApsDispenseLog;
		}
	}

	public ObjectSet<APS_MxtraSyslog> APS_MxtraSyslog
	{
		get
		{
			if (_APS_MxtraSyslog == null)
			{
				_APS_MxtraSyslog = CreateObjectSet<APS_MxtraSyslog>("APS_MxtraSyslog");
			}
			return _APS_MxtraSyslog;
		}
	}

	public ObjectSet<APS_RedoTicket> APS_RedoTicket
	{
		get
		{
			if (_APS_RedoTicket == null)
			{
				_APS_RedoTicket = CreateObjectSet<APS_RedoTicket>("APS_RedoTicket");
			}
			return _APS_RedoTicket;
		}
	}

	public ObjectSet<APS_ShiftRecordDetailed> APS_ShiftRecordDetailed
	{
		get
		{
			if (_APS_ShiftRecordDetailed == null)
			{
				_APS_ShiftRecordDetailed = CreateObjectSet<APS_ShiftRecordDetailed>("APS_ShiftRecordDetailed");
			}
			return _APS_ShiftRecordDetailed;
		}
	}

	public ObjectSet<APS_Station> APS_Station
	{
		get
		{
			if (_APS_Station == null)
			{
				_APS_Station = CreateObjectSet<APS_Station>("APS_Station");
			}
			return _APS_Station;
		}
	}

	public ObjectSet<APS_Syslog> APS_Syslog
	{
		get
		{
			if (_APS_Syslog == null)
			{
				_APS_Syslog = CreateObjectSet<APS_Syslog>("APS_Syslog");
			}
			return _APS_Syslog;
		}
	}

	public ObjectSet<APS_SystemSetting> APS_SystemSetting
	{
		get
		{
			if (_APS_SystemSetting == null)
			{
				_APS_SystemSetting = CreateObjectSet<APS_SystemSetting>("APS_SystemSetting");
			}
			return _APS_SystemSetting;
		}
	}

	public ObjectSet<APS_TimeChargeRecord> APS_TimeChargeRecord
	{
		get
		{
			if (_APS_TimeChargeRecord == null)
			{
				_APS_TimeChargeRecord = CreateObjectSet<APS_TimeChargeRecord>("APS_TimeChargeRecord");
			}
			return _APS_TimeChargeRecord;
		}
	}

	public ObjectSet<barsys_Discount> barsys_Discount
	{
		get
		{
			if (_barsys_Discount == null)
			{
				_barsys_Discount = CreateObjectSet<barsys_Discount>("barsys_Discount");
			}
			return _barsys_Discount;
		}
	}

	public ObjectSet<barsys_Discount_Batch> barsys_Discount_Batch
	{
		get
		{
			if (_barsys_Discount_Batch == null)
			{
				_barsys_Discount_Batch = CreateObjectSet<barsys_Discount_Batch>("barsys_Discount_Batch");
			}
			return _barsys_Discount_Batch;
		}
	}

	public ObjectSet<barsys_Discount_Record> barsys_Discount_Record
	{
		get
		{
			if (_barsys_Discount_Record == null)
			{
				_barsys_Discount_Record = CreateObjectSet<barsys_Discount_Record>("barsys_Discount_Record");
			}
			return _barsys_Discount_Record;
		}
	}

	public ObjectSet<BatteryChargeRecord> BatteryChargeRecord
	{
		get
		{
			if (_BatteryChargeRecord == null)
			{
				_BatteryChargeRecord = CreateObjectSet<BatteryChargeRecord>("BatteryChargeRecord");
			}
			return _BatteryChargeRecord;
		}
	}

	public ObjectSet<BlackList> BlackList
	{
		get
		{
			if (_BlackList == null)
			{
				_BlackList = CreateObjectSet<BlackList>("BlackList");
			}
			return _BlackList;
		}
	}

	public ObjectSet<BOC_Gate_TransactionExtend> BOC_Gate_TransactionExtend
	{
		get
		{
			if (_BOC_Gate_TransactionExtend == null)
			{
				_BOC_Gate_TransactionExtend = CreateObjectSet<BOC_Gate_TransactionExtend>("BOC_Gate_TransactionExtend");
			}
			return _BOC_Gate_TransactionExtend;
		}
	}

	public ObjectSet<BOC_Gate_TransactionExtend_PackData_File> BOC_Gate_TransactionExtend_PackData_File
	{
		get
		{
			if (_BOC_Gate_TransactionExtend_PackData_File == null)
			{
				_BOC_Gate_TransactionExtend_PackData_File = CreateObjectSet<BOC_Gate_TransactionExtend_PackData_File>("BOC_Gate_TransactionExtend_PackData_File");
			}
			return _BOC_Gate_TransactionExtend_PackData_File;
		}
	}

	public ObjectSet<BOC_N910_POS_Card_Payment_DetailEX> BOC_N910_POS_Card_Payment_DetailEX
	{
		get
		{
			if (_BOC_N910_POS_Card_Payment_DetailEX == null)
			{
				_BOC_N910_POS_Card_Payment_DetailEX = CreateObjectSet<BOC_N910_POS_Card_Payment_DetailEX>("BOC_N910_POS_Card_Payment_DetailEX");
			}
			return _BOC_N910_POS_Card_Payment_DetailEX;
		}
	}

	public ObjectSet<BOC_Smart_Payment_DetailEX> BOC_Smart_Payment_DetailEX
	{
		get
		{
			if (_BOC_Smart_Payment_DetailEX == null)
			{
				_BOC_Smart_Payment_DetailEX = CreateObjectSet<BOC_Smart_Payment_DetailEX>("BOC_Smart_Payment_DetailEX");
			}
			return _BOC_Smart_Payment_DetailEX;
		}
	}

	public ObjectSet<Card> Card
	{
		get
		{
			if (_Card == null)
			{
				_Card = CreateObjectSet<Card>("Card");
			}
			return _Card;
		}
	}

	public ObjectSet<CardType> CardType
	{
		get
		{
			if (_CardType == null)
			{
				_CardType = CreateObjectSet<CardType>("CardType");
			}
			return _CardType;
		}
	}

	public ObjectSet<ChargeRecord> ChargeRecord
	{
		get
		{
			if (_ChargeRecord == null)
			{
				_ChargeRecord = CreateObjectSet<ChargeRecord>("ChargeRecord");
			}
			return _ChargeRecord;
		}
	}

	public ObjectSet<CloudDiscount> CloudDiscount
	{
		get
		{
			if (_CloudDiscount == null)
			{
				_CloudDiscount = CreateObjectSet<CloudDiscount>("CloudDiscount");
			}
			return _CloudDiscount;
		}
	}

	public ObjectSet<CompanyInfo> CompanyInfo
	{
		get
		{
			if (_CompanyInfo == null)
			{
				_CompanyInfo = CreateObjectSet<CompanyInfo>("CompanyInfo");
			}
			return _CompanyInfo;
		}
	}

	public ObjectSet<Customer> Customer
	{
		get
		{
			if (_Customer == null)
			{
				_Customer = CreateObjectSet<Customer>("Customer");
			}
			return _Customer;
		}
	}

	public ObjectSet<CustomFreeRecord> CustomFreeRecord
	{
		get
		{
			if (_CustomFreeRecord == null)
			{
				_CustomFreeRecord = CreateObjectSet<CustomFreeRecord>("CustomFreeRecord");
			}
			return _CustomFreeRecord;
		}
	}

	public ObjectSet<CustomFreeTenat> CustomFreeTenat
	{
		get
		{
			if (_CustomFreeTenat == null)
			{
				_CustomFreeTenat = CreateObjectSet<CustomFreeTenat>("CustomFreeTenat");
			}
			return _CustomFreeTenat;
		}
	}

	public ObjectSet<CustomFreeType> CustomFreeType
	{
		get
		{
			if (_CustomFreeType == null)
			{
				_CustomFreeType = CreateObjectSet<CustomFreeType>("CustomFreeType");
			}
			return _CustomFreeType;
		}
	}

	public ObjectSet<FreeRegister> FreeRegister
	{
		get
		{
			if (_FreeRegister == null)
			{
				_FreeRegister = CreateObjectSet<FreeRegister>("FreeRegister");
			}
			return _FreeRegister;
		}
	}

	public ObjectSet<ICBC_Gate_TransactionExtend> ICBC_Gate_TransactionExtend
	{
		get
		{
			if (_ICBC_Gate_TransactionExtend == null)
			{
				_ICBC_Gate_TransactionExtend = CreateObjectSet<ICBC_Gate_TransactionExtend>("ICBC_Gate_TransactionExtend");
			}
			return _ICBC_Gate_TransactionExtend;
		}
	}

	public ObjectSet<ICBC_Gate_TransactionExtend_PackData_File> ICBC_Gate_TransactionExtend_PackData_File
	{
		get
		{
			if (_ICBC_Gate_TransactionExtend_PackData_File == null)
			{
				_ICBC_Gate_TransactionExtend_PackData_File = CreateObjectSet<ICBC_Gate_TransactionExtend_PackData_File>("ICBC_Gate_TransactionExtend_PackData_File");
			}
			return _ICBC_Gate_TransactionExtend_PackData_File;
		}
	}

	public ObjectSet<LicensePlate_PassTrace> LicensePlate_PassTrace
	{
		get
		{
			if (_LicensePlate_PassTrace == null)
			{
				_LicensePlate_PassTrace = CreateObjectSet<LicensePlate_PassTrace>("LicensePlate_PassTrace");
			}
			return _LicensePlate_PassTrace;
		}
	}

	public ObjectSet<MPass_Gate_Transaction> MPass_Gate_Transaction
	{
		get
		{
			if (_MPass_Gate_Transaction == null)
			{
				_MPass_Gate_Transaction = CreateObjectSet<MPass_Gate_Transaction>("MPass_Gate_Transaction");
			}
			return _MPass_Gate_Transaction;
		}
	}

	public ObjectSet<MPass_Gate_Transaction_PackData> MPass_Gate_Transaction_PackData
	{
		get
		{
			if (_MPass_Gate_Transaction_PackData == null)
			{
				_MPass_Gate_Transaction_PackData = CreateObjectSet<MPass_Gate_Transaction_PackData>("MPass_Gate_Transaction_PackData");
			}
			return _MPass_Gate_Transaction_PackData;
		}
	}

	public ObjectSet<MPass_Gate_Transaction_PackData_File> MPass_Gate_Transaction_PackData_File
	{
		get
		{
			if (_MPass_Gate_Transaction_PackData_File == null)
			{
				_MPass_Gate_Transaction_PackData_File = CreateObjectSet<MPass_Gate_Transaction_PackData_File>("MPass_Gate_Transaction_PackData_File");
			}
			return _MPass_Gate_Transaction_PackData_File;
		}
	}

	public ObjectSet<MPass_Gate_Transaction_SIFiles> MPass_Gate_Transaction_SIFiles
	{
		get
		{
			if (_MPass_Gate_Transaction_SIFiles == null)
			{
				_MPass_Gate_Transaction_SIFiles = CreateObjectSet<MPass_Gate_Transaction_SIFiles>("MPass_Gate_Transaction_SIFiles");
			}
			return _MPass_Gate_Transaction_SIFiles;
		}
	}

	public ObjectSet<MPass_Gate_Transaction_SIStrings> MPass_Gate_Transaction_SIStrings
	{
		get
		{
			if (_MPass_Gate_Transaction_SIStrings == null)
			{
				_MPass_Gate_Transaction_SIStrings = CreateObjectSet<MPass_Gate_Transaction_SIStrings>("MPass_Gate_Transaction_SIStrings");
			}
			return _MPass_Gate_Transaction_SIStrings;
		}
	}

	public ObjectSet<MPass_POS_Signin> MPass_POS_Signin
	{
		get
		{
			if (_MPass_POS_Signin == null)
			{
				_MPass_POS_Signin = CreateObjectSet<MPass_POS_Signin>("MPass_POS_Signin");
			}
			return _MPass_POS_Signin;
		}
	}

	public ObjectSet<MPass_POS_Signin_Detail> MPass_POS_Signin_Detail
	{
		get
		{
			if (_MPass_POS_Signin_Detail == null)
			{
				_MPass_POS_Signin_Detail = CreateObjectSet<MPass_POS_Signin_Detail>("MPass_POS_Signin_Detail");
			}
			return _MPass_POS_Signin_Detail;
		}
	}

	public ObjectSet<MPass_POS_Transaction_Detail> MPass_POS_Transaction_Detail
	{
		get
		{
			if (_MPass_POS_Transaction_Detail == null)
			{
				_MPass_POS_Transaction_Detail = CreateObjectSet<MPass_POS_Transaction_Detail>("MPass_POS_Transaction_Detail");
			}
			return _MPass_POS_Transaction_Detail;
		}
	}

	public ObjectSet<NoFeelPayment> NoFeelPayment
	{
		get
		{
			if (_NoFeelPayment == null)
			{
				_NoFeelPayment = CreateObjectSet<NoFeelPayment>("NoFeelPayment");
			}
			return _NoFeelPayment;
		}
	}

	public ObjectSet<Park> Park
	{
		get
		{
			if (_Park == null)
			{
				_Park = CreateObjectSet<Park>("Park");
			}
			return _Park;
		}
	}

	public ObjectSet<ParkArea> ParkArea
	{
		get
		{
			if (_ParkArea == null)
			{
				_ParkArea = CreateObjectSet<ParkArea>("ParkArea");
			}
			return _ParkArea;
		}
	}

	public ObjectSet<ParkAreaExtend> ParkAreaExtend
	{
		get
		{
			if (_ParkAreaExtend == null)
			{
				_ParkAreaExtend = CreateObjectSet<ParkAreaExtend>("ParkAreaExtend");
			}
			return _ParkAreaExtend;
		}
	}

	public ObjectSet<ParkGate> ParkGate
	{
		get
		{
			if (_ParkGate == null)
			{
				_ParkGate = CreateObjectSet<ParkGate>("ParkGate");
			}
			return _ParkGate;
		}
	}

	public ObjectSet<PassTrace> PassTrace
	{
		get
		{
			if (_PassTrace == null)
			{
				_PassTrace = CreateObjectSet<PassTrace>("PassTrace");
			}
			return _PassTrace;
		}
	}

	public ObjectSet<PreOffer> PreOffer
	{
		get
		{
			if (_PreOffer == null)
			{
				_PreOffer = CreateObjectSet<PreOffer>("PreOffer");
			}
			return _PreOffer;
		}
	}

	public ObjectSet<RentalType> RentalType
	{
		get
		{
			if (_RentalType == null)
			{
				_RentalType = CreateObjectSet<RentalType>("RentalType");
			}
			return _RentalType;
		}
	}

	public ObjectSet<RuleControl> RuleControl
	{
		get
		{
			if (_RuleControl == null)
			{
				_RuleControl = CreateObjectSet<RuleControl>("RuleControl");
			}
			return _RuleControl;
		}
	}

	public ObjectSet<SalaryBlock> SalaryBlock
	{
		get
		{
			if (_SalaryBlock == null)
			{
				_SalaryBlock = CreateObjectSet<SalaryBlock>("SalaryBlock");
			}
			return _SalaryBlock;
		}
	}

	public ObjectSet<ShiftRecord> ShiftRecord
	{
		get
		{
			if (_ShiftRecord == null)
			{
				_ShiftRecord = CreateObjectSet<ShiftRecord>("ShiftRecord");
			}
			return _ShiftRecord;
		}
	}

	public ObjectSet<StaffInfo> StaffInfo
	{
		get
		{
			if (_StaffInfo == null)
			{
				_StaffInfo = CreateObjectSet<StaffInfo>("StaffInfo");
			}
			return _StaffInfo;
		}
	}

	public ObjectSet<StaffOperat> StaffOperat
	{
		get
		{
			if (_StaffOperat == null)
			{
				_StaffOperat = CreateObjectSet<StaffOperat>("StaffOperat");
			}
			return _StaffOperat;
		}
	}

	public ObjectSet<StaffType> StaffType
	{
		get
		{
			if (_StaffType == null)
			{
				_StaffType = CreateObjectSet<StaffType>("StaffType");
			}
			return _StaffType;
		}
	}

	public ObjectSet<Statistics_NotExitData> Statistics_NotExitData
	{
		get
		{
			if (_Statistics_NotExitData == null)
			{
				_Statistics_NotExitData = CreateObjectSet<Statistics_NotExitData>("Statistics_NotExitData");
			}
			return _Statistics_NotExitData;
		}
	}

	public ObjectSet<Statistics_UpdateUtil> Statistics_UpdateUtil
	{
		get
		{
			if (_Statistics_UpdateUtil == null)
			{
				_Statistics_UpdateUtil = CreateObjectSet<Statistics_UpdateUtil>("Statistics_UpdateUtil");
			}
			return _Statistics_UpdateUtil;
		}
	}

	public ObjectSet<Statistics_UseTime> Statistics_UseTime
	{
		get
		{
			if (_Statistics_UseTime == null)
			{
				_Statistics_UseTime = CreateObjectSet<Statistics_UseTime>("Statistics_UseTime");
			}
			return _Statistics_UseTime;
		}
	}

	public ObjectSet<SysRole> SysRole
	{
		get
		{
			if (_SysRole == null)
			{
				_SysRole = CreateObjectSet<SysRole>("SysRole");
			}
			return _SysRole;
		}
	}

	public ObjectSet<SysRoleGroup> SysRoleGroup
	{
		get
		{
			if (_SysRoleGroup == null)
			{
				_SysRoleGroup = CreateObjectSet<SysRoleGroup>("SysRoleGroup");
			}
			return _SysRoleGroup;
		}
	}

	public ObjectSet<SysStaffRole> SysStaffRole
	{
		get
		{
			if (_SysStaffRole == null)
			{
				_SysStaffRole = CreateObjectSet<SysStaffRole>("SysStaffRole");
			}
			return _SysStaffRole;
		}
	}

	public ObjectSet<TempStop> TempStop
	{
		get
		{
			if (_TempStop == null)
			{
				_TempStop = CreateObjectSet<TempStop>("TempStop");
			}
			return _TempStop;
		}
	}

	public ObjectSet<TempStopList> TempStopList
	{
		get
		{
			if (_TempStopList == null)
			{
				_TempStopList = CreateObjectSet<TempStopList>("TempStopList");
			}
			return _TempStopList;
		}
	}

	public ObjectSet<TicketSeq> TicketSeq
	{
		get
		{
			if (_TicketSeq == null)
			{
				_TicketSeq = CreateObjectSet<TicketSeq>("TicketSeq");
			}
			return _TicketSeq;
		}
	}

	public ObjectSet<TimeCharge> TimeCharge
	{
		get
		{
			if (_TimeCharge == null)
			{
				_TimeCharge = CreateObjectSet<TimeCharge>("TimeCharge");
			}
			return _TimeCharge;
		}
	}

	public ObjectSet<TimeChargeExt> TimeChargeExt
	{
		get
		{
			if (_TimeChargeExt == null)
			{
				_TimeChargeExt = CreateObjectSet<TimeChargeExt>("TimeChargeExt");
			}
			return _TimeChargeExt;
		}
	}

	public ObjectSet<TransactionData> TransactionData
	{
		get
		{
			if (_TransactionData == null)
			{
				_TransactionData = CreateObjectSet<TransactionData>("TransactionData");
			}
			return _TransactionData;
		}
	}

	public ObjectSet<TransactionDataFinished> TransactionDataFinished
	{
		get
		{
			if (_TransactionDataFinished == null)
			{
				_TransactionDataFinished = CreateObjectSet<TransactionDataFinished>("TransactionDataFinished");
			}
			return _TransactionDataFinished;
		}
	}

	public ObjectSet<TypeDictionary> TypeDictionary
	{
		get
		{
			if (_TypeDictionary == null)
			{
				_TypeDictionary = CreateObjectSet<TypeDictionary>("TypeDictionary");
			}
			return _TypeDictionary;
		}
	}

	public ObjectSet<UMRefNum> UMRefNum
	{
		get
		{
			if (_UMRefNum == null)
			{
				_UMRefNum = CreateObjectSet<UMRefNum>("UMRefNum");
			}
			return _UMRefNum;
		}
	}

	public ObjectSet<VoidCharge> VoidCharge
	{
		get
		{
			if (_VoidCharge == null)
			{
				_VoidCharge = CreateObjectSet<VoidCharge>("VoidCharge");
			}
			return _VoidCharge;
		}
	}

	public ObjectSet<view_parkintime2018> view_parkintime2018
	{
		get
		{
			if (_view_parkintime2018 == null)
			{
				_view_parkintime2018 = CreateObjectSet<view_parkintime2018>("view_parkintime2018");
			}
			return _view_parkintime2018;
		}
	}

	public ObjectSet<view_transactionandlp> view_transactionandlp
	{
		get
		{
			if (_view_transactionandlp == null)
			{
				_view_transactionandlp = CreateObjectSet<view_transactionandlp>("view_transactionandlp");
			}
			return _view_transactionandlp;
		}
	}

	public ObjectSet<view_transactiondata> view_transactiondata
	{
		get
		{
			if (_view_transactiondata == null)
			{
				_view_transactiondata = CreateObjectSet<view_transactiondata>("view_transactiondata");
			}
			return _view_transactiondata;
		}
	}

	public ObjectSet<ScanPayment> ScanPayment
	{
		get
		{
			if (_ScanPayment == null)
			{
				_ScanPayment = CreateObjectSet<ScanPayment>("ScanPayment");
			}
			return _ScanPayment;
		}
	}

	public Entities()
		: base("name=Entities", "Entities")
	{
		base.ContextOptions.LazyLoadingEnabled = true;
	}

	public Entities(string connectionString)
		: base(connectionString, "Entities")
	{
		base.ContextOptions.LazyLoadingEnabled = true;
	}

	public Entities(EntityConnection connection)
		: base(connection, "Entities")
	{
		base.ContextOptions.LazyLoadingEnabled = true;
	}

	public void AddToAD_ExtCardExpiryDate(AD_ExtCardExpiryDate aD_ExtCardExpiryDate)
	{
		AddObject("AD_ExtCardExpiryDate", aD_ExtCardExpiryDate);
	}

	public void AddToAD_SCSCP_CARD_DATA(AD_SCSCP_CARD_DATA aD_SCSCP_CARD_DATA)
	{
		AddObject("AD_SCSCP_CARD_DATA", aD_SCSCP_CARD_DATA);
	}

	public void AddToAdminCharge(AdminCharge adminCharge)
	{
		AddObject("AdminCharge", adminCharge);
	}

	public void AddToAppSetting(AppSetting appSetting)
	{
		AddObject("AppSetting", appSetting);
	}

	public void AddToAPS_Alarm(APS_Alarm aPS_Alarm)
	{
		AddObject("APS_Alarm", aPS_Alarm);
	}

	public void AddToAPS_CashBoxAcceptor(APS_CashBoxAcceptor aPS_CashBoxAcceptor)
	{
		AddObject("APS_CashBoxAcceptor", aPS_CashBoxAcceptor);
	}

	public void AddToAPS_CashBoxDispenser(APS_CashBoxDispenser aPS_CashBoxDispenser)
	{
		AddObject("APS_CashBoxDispenser", aPS_CashBoxDispenser);
	}

	public void AddToAPS_CashBoxType(APS_CashBoxType aPS_CashBoxType)
	{
		AddObject("APS_CashBoxType", aPS_CashBoxType);
	}

	public void AddToAPS_Certificate(APS_Certificate aPS_Certificate)
	{
		AddObject("APS_Certificate", aPS_Certificate);
	}

	public void AddToAPS_CertificateType(APS_CertificateType aPS_CertificateType)
	{
		AddObject("APS_CertificateType", aPS_CertificateType);
	}

	public void AddToAPS_CertificateUseRecord(APS_CertificateUseRecord aPS_CertificateUseRecord)
	{
		AddObject("APS_CertificateUseRecord", aPS_CertificateUseRecord);
	}

	public void AddToAPS_CouponSetting(APS_CouponSetting aPS_CouponSetting)
	{
		AddObject("APS_CouponSetting", aPS_CouponSetting);
	}

	public void AddToAPS_CurrencyInfo(APS_CurrencyInfo aPS_CurrencyInfo)
	{
		AddObject("APS_CurrencyInfo", aPS_CurrencyInfo);
	}

	public void AddToAPS_FinishApsAcceptLog(APS_FinishApsAcceptLog aPS_FinishApsAcceptLog)
	{
		AddObject("APS_FinishApsAcceptLog", aPS_FinishApsAcceptLog);
	}

	public void AddToAPS_FinishApsDispenseLog(APS_FinishApsDispenseLog aPS_FinishApsDispenseLog)
	{
		AddObject("APS_FinishApsDispenseLog", aPS_FinishApsDispenseLog);
	}

	public void AddToAPS_MxtraSyslog(APS_MxtraSyslog aPS_MxtraSyslog)
	{
		AddObject("APS_MxtraSyslog", aPS_MxtraSyslog);
	}

	public void AddToAPS_RedoTicket(APS_RedoTicket aPS_RedoTicket)
	{
		AddObject("APS_RedoTicket", aPS_RedoTicket);
	}

	public void AddToAPS_ShiftRecordDetailed(APS_ShiftRecordDetailed aPS_ShiftRecordDetailed)
	{
		AddObject("APS_ShiftRecordDetailed", aPS_ShiftRecordDetailed);
	}

	public void AddToAPS_Station(APS_Station aPS_Station)
	{
		AddObject("APS_Station", aPS_Station);
	}

	public void AddToAPS_Syslog(APS_Syslog aPS_Syslog)
	{
		AddObject("APS_Syslog", aPS_Syslog);
	}

	public void AddToAPS_SystemSetting(APS_SystemSetting aPS_SystemSetting)
	{
		AddObject("APS_SystemSetting", aPS_SystemSetting);
	}

	public void AddToAPS_TimeChargeRecord(APS_TimeChargeRecord aPS_TimeChargeRecord)
	{
		AddObject("APS_TimeChargeRecord", aPS_TimeChargeRecord);
	}

	public void AddTobarsys_Discount(barsys_Discount barsys_Discount)
	{
		AddObject("barsys_Discount", barsys_Discount);
	}

	public void AddTobarsys_Discount_Batch(barsys_Discount_Batch barsys_Discount_Batch)
	{
		AddObject("barsys_Discount_Batch", barsys_Discount_Batch);
	}

	public void AddTobarsys_Discount_Record(barsys_Discount_Record barsys_Discount_Record)
	{
		AddObject("barsys_Discount_Record", barsys_Discount_Record);
	}

	public void AddToBatteryChargeRecord(BatteryChargeRecord batteryChargeRecord)
	{
		AddObject("BatteryChargeRecord", batteryChargeRecord);
	}

	public void AddToBlackList(BlackList blackList)
	{
		AddObject("BlackList", blackList);
	}

	public void AddToBOC_Gate_TransactionExtend(BOC_Gate_TransactionExtend bOC_Gate_TransactionExtend)
	{
		AddObject("BOC_Gate_TransactionExtend", bOC_Gate_TransactionExtend);
	}

	public void AddToBOC_Gate_TransactionExtend_PackData_File(BOC_Gate_TransactionExtend_PackData_File bOC_Gate_TransactionExtend_PackData_File)
	{
		AddObject("BOC_Gate_TransactionExtend_PackData_File", bOC_Gate_TransactionExtend_PackData_File);
	}

	public void AddToBOC_N910_POS_Card_Payment_DetailEX(BOC_N910_POS_Card_Payment_DetailEX bOC_N910_POS_Card_Payment_DetailEX)
	{
		AddObject("BOC_N910_POS_Card_Payment_DetailEX", bOC_N910_POS_Card_Payment_DetailEX);
	}

	public void AddToBOC_Smart_Payment_DetailEX(BOC_Smart_Payment_DetailEX bOC_Smart_Payment_DetailEX)
	{
		AddObject("BOC_Smart_Payment_DetailEX", bOC_Smart_Payment_DetailEX);
	}

	public void AddToCard(Card card)
	{
		AddObject("Card", card);
	}

	public void AddToCardType(CardType cardType)
	{
		AddObject("CardType", cardType);
	}

	public void AddToChargeRecord(ChargeRecord chargeRecord)
	{
		AddObject("ChargeRecord", chargeRecord);
	}

	public void AddToCloudDiscount(CloudDiscount cloudDiscount)
	{
		AddObject("CloudDiscount", cloudDiscount);
	}

	public void AddToCompanyInfo(CompanyInfo companyInfo)
	{
		AddObject("CompanyInfo", companyInfo);
	}

	public void AddToCustomer(Customer customer)
	{
		AddObject("Customer", customer);
	}

	public void AddToCustomFreeRecord(CustomFreeRecord customFreeRecord)
	{
		AddObject("CustomFreeRecord", customFreeRecord);
	}

	public void AddToCustomFreeTenat(CustomFreeTenat customFreeTenat)
	{
		AddObject("CustomFreeTenat", customFreeTenat);
	}

	public void AddToCustomFreeType(CustomFreeType customFreeType)
	{
		AddObject("CustomFreeType", customFreeType);
	}

	public void AddToFreeRegister(FreeRegister freeRegister)
	{
		AddObject("FreeRegister", freeRegister);
	}

	public void AddToICBC_Gate_TransactionExtend(ICBC_Gate_TransactionExtend iCBC_Gate_TransactionExtend)
	{
		AddObject("ICBC_Gate_TransactionExtend", iCBC_Gate_TransactionExtend);
	}

	public void AddToICBC_Gate_TransactionExtend_PackData_File(ICBC_Gate_TransactionExtend_PackData_File iCBC_Gate_TransactionExtend_PackData_File)
	{
		AddObject("ICBC_Gate_TransactionExtend_PackData_File", iCBC_Gate_TransactionExtend_PackData_File);
	}

	public void AddToLicensePlate_PassTrace(LicensePlate_PassTrace licensePlate_PassTrace)
	{
		AddObject("LicensePlate_PassTrace", licensePlate_PassTrace);
	}

	public void AddToMPass_Gate_Transaction(MPass_Gate_Transaction mPass_Gate_Transaction)
	{
		AddObject("MPass_Gate_Transaction", mPass_Gate_Transaction);
	}

	public void AddToMPass_Gate_Transaction_PackData(MPass_Gate_Transaction_PackData mPass_Gate_Transaction_PackData)
	{
		AddObject("MPass_Gate_Transaction_PackData", mPass_Gate_Transaction_PackData);
	}

	public void AddToMPass_Gate_Transaction_PackData_File(MPass_Gate_Transaction_PackData_File mPass_Gate_Transaction_PackData_File)
	{
		AddObject("MPass_Gate_Transaction_PackData_File", mPass_Gate_Transaction_PackData_File);
	}

	public void AddToMPass_Gate_Transaction_SIFiles(MPass_Gate_Transaction_SIFiles mPass_Gate_Transaction_SIFiles)
	{
		AddObject("MPass_Gate_Transaction_SIFiles", mPass_Gate_Transaction_SIFiles);
	}

	public void AddToMPass_Gate_Transaction_SIStrings(MPass_Gate_Transaction_SIStrings mPass_Gate_Transaction_SIStrings)
	{
		AddObject("MPass_Gate_Transaction_SIStrings", mPass_Gate_Transaction_SIStrings);
	}

	public void AddToMPass_POS_Signin(MPass_POS_Signin mPass_POS_Signin)
	{
		AddObject("MPass_POS_Signin", mPass_POS_Signin);
	}

	public void AddToMPass_POS_Signin_Detail(MPass_POS_Signin_Detail mPass_POS_Signin_Detail)
	{
		AddObject("MPass_POS_Signin_Detail", mPass_POS_Signin_Detail);
	}

	public void AddToMPass_POS_Transaction_Detail(MPass_POS_Transaction_Detail mPass_POS_Transaction_Detail)
	{
		AddObject("MPass_POS_Transaction_Detail", mPass_POS_Transaction_Detail);
	}

	public void AddToNoFeelPayment(NoFeelPayment noFeelPayment)
	{
		AddObject("NoFeelPayment", noFeelPayment);
	}

	public void AddToPark(Park park)
	{
		AddObject("Park", park);
	}

	public void AddToParkArea(ParkArea parkArea)
	{
		AddObject("ParkArea", parkArea);
	}

	public void AddToParkAreaExtend(ParkAreaExtend parkAreaExtend)
	{
		AddObject("ParkAreaExtend", parkAreaExtend);
	}

	public void AddToParkGate(ParkGate parkGate)
	{
		AddObject("ParkGate", parkGate);
	}

	public void AddToPassTrace(PassTrace passTrace)
	{
		AddObject("PassTrace", passTrace);
	}

	public void AddToPreOffer(PreOffer preOffer)
	{
		AddObject("PreOffer", preOffer);
	}

	public void AddToRentalType(RentalType rentalType)
	{
		AddObject("RentalType", rentalType);
	}

	public void AddToRuleControl(RuleControl ruleControl)
	{
		AddObject("RuleControl", ruleControl);
	}

	public void AddToSalaryBlock(SalaryBlock salaryBlock)
	{
		AddObject("SalaryBlock", salaryBlock);
	}

	public void AddToShiftRecord(ShiftRecord shiftRecord)
	{
		AddObject("ShiftRecord", shiftRecord);
	}

	public void AddToStaffInfo(StaffInfo staffInfo)
	{
		AddObject("StaffInfo", staffInfo);
	}

	public void AddToStaffOperat(StaffOperat staffOperat)
	{
		AddObject("StaffOperat", staffOperat);
	}

	public void AddToStaffType(StaffType staffType)
	{
		AddObject("StaffType", staffType);
	}

	public void AddToStatistics_NotExitData(Statistics_NotExitData statistics_NotExitData)
	{
		AddObject("Statistics_NotExitData", statistics_NotExitData);
	}

	public void AddToStatistics_UpdateUtil(Statistics_UpdateUtil statistics_UpdateUtil)
	{
		AddObject("Statistics_UpdateUtil", statistics_UpdateUtil);
	}

	public void AddToStatistics_UseTime(Statistics_UseTime statistics_UseTime)
	{
		AddObject("Statistics_UseTime", statistics_UseTime);
	}

	public void AddToSysRole(SysRole sysRole)
	{
		AddObject("SysRole", sysRole);
	}

	public void AddToSysRoleGroup(SysRoleGroup sysRoleGroup)
	{
		AddObject("SysRoleGroup", sysRoleGroup);
	}

	public void AddToSysStaffRole(SysStaffRole sysStaffRole)
	{
		AddObject("SysStaffRole", sysStaffRole);
	}

	public void AddToTempStop(TempStop tempStop)
	{
		AddObject("TempStop", tempStop);
	}

	public void AddToTempStopList(TempStopList tempStopList)
	{
		AddObject("TempStopList", tempStopList);
	}

	public void AddToTicketSeq(TicketSeq ticketSeq)
	{
		AddObject("TicketSeq", ticketSeq);
	}

	public void AddToTimeCharge(TimeCharge timeCharge)
	{
		AddObject("TimeCharge", timeCharge);
	}

	public void AddToTimeChargeExt(TimeChargeExt timeChargeExt)
	{
		AddObject("TimeChargeExt", timeChargeExt);
	}

	public void AddToTransactionData(TransactionData transactionData)
	{
		AddObject("TransactionData", transactionData);
	}

	public void AddToTransactionDataFinished(TransactionDataFinished transactionDataFinished)
	{
		AddObject("TransactionDataFinished", transactionDataFinished);
	}

	public void AddToTypeDictionary(TypeDictionary typeDictionary)
	{
		AddObject("TypeDictionary", typeDictionary);
	}

	public void AddToUMRefNum(UMRefNum uMRefNum)
	{
		AddObject("UMRefNum", uMRefNum);
	}

	public void AddToVoidCharge(VoidCharge voidCharge)
	{
		AddObject("VoidCharge", voidCharge);
	}

	public void AddToview_parkintime2018(view_parkintime2018 view_parkintime2018)
	{
		AddObject("view_parkintime2018", view_parkintime2018);
	}

	public void AddToview_transactionandlp(view_transactionandlp view_transactionandlp)
	{
		AddObject("view_transactionandlp", view_transactionandlp);
	}

	public void AddToview_transactiondata(view_transactiondata view_transactiondata)
	{
		AddObject("view_transactiondata", view_transactiondata);
	}

	public void AddToScanPayment(ScanPayment scanPayment)
	{
		AddObject("ScanPayment", scanPayment);
	}
}
