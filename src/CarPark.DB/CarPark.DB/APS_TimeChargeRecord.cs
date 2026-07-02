using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_TimeChargeRecord")]
[DataContract(IsReference = true)]
public class APS_TimeChargeRecord : EntityObject
{
	private int _FirstTime;

	private int _TimeChargeRecordID;

	private int _StationID;

	private string _FromAPS;

	private int _TransactionID;

	private int? _CardID;

	private string _CardCode;

	private int _ParkTypeID;

	private string _LicensePlate;

	private int? _CouponID;

	private string _CouponCode;

	private DateTime _InTime;

	private DateTime _FeeTime;

	private int _OriginalFeeMin;

	private int _ChargeFreeMin;

	private int _ChargeMin;

	private decimal _TotalCharge;

	private decimal _ActualCharge;

	private decimal? _PenaltyAmount;

	private decimal? _FreeCharge;

	private decimal _HKDPaidAmount;

	private decimal _MOPPaidAmount;

	private decimal _OthPaidAmoun;

	private string _PaidCurrencyString;

	private string _ChangeCurrencyString;

	private decimal _HKDChangeAmount;

	private decimal _MOPChangeAmount;

	private decimal _OthChangeAmount;

	private bool? _IsLost;

	private bool? _IsDamage;

	private bool? _IsOverTime;

	private bool? _IsAllFree;

	private int? _StaffID;

	private string _StaffName;

	private int _ShiftID;

	private DateTime? _CreateTime;

	private int? _BufferTime;

	private int _BillType;

	private string _Remark;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FirstTime
	{
		get
		{
			return _FirstTime;
		}
		set
		{
			ReportPropertyChanging("FirstTime");
			_FirstTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FirstTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int TimeChargeRecordID
	{
		get
		{
			return _TimeChargeRecordID;
		}
		set
		{
			if (_TimeChargeRecordID != value)
			{
				ReportPropertyChanging("TimeChargeRecordID");
				_TimeChargeRecordID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TimeChargeRecordID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int StationID
	{
		get
		{
			return _StationID;
		}
		set
		{
			ReportPropertyChanging("StationID");
			_StationID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StationID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string FromAPS
	{
		get
		{
			return _FromAPS;
		}
		set
		{
			ReportPropertyChanging("FromAPS");
			_FromAPS = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("FromAPS");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int TransactionID
	{
		get
		{
			return _TransactionID;
		}
		set
		{
			ReportPropertyChanging("TransactionID");
			_TransactionID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TransactionID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? CardID
	{
		get
		{
			return _CardID;
		}
		set
		{
			ReportPropertyChanging("CardID");
			_CardID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CardID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CardCode
	{
		get
		{
			return _CardCode;
		}
		set
		{
			ReportPropertyChanging("CardCode");
			_CardCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CardCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ParkTypeID
	{
		get
		{
			return _ParkTypeID;
		}
		set
		{
			ReportPropertyChanging("ParkTypeID");
			_ParkTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string LicensePlate
	{
		get
		{
			return _LicensePlate;
		}
		set
		{
			ReportPropertyChanging("LicensePlate");
			_LicensePlate = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LicensePlate");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? CouponID
	{
		get
		{
			return _CouponID;
		}
		set
		{
			ReportPropertyChanging("CouponID");
			_CouponID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CouponID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CouponCode
	{
		get
		{
			return _CouponCode;
		}
		set
		{
			ReportPropertyChanging("CouponCode");
			_CouponCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CouponCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime InTime
	{
		get
		{
			return _InTime;
		}
		set
		{
			ReportPropertyChanging("InTime");
			_InTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("InTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime FeeTime
	{
		get
		{
			return _FeeTime;
		}
		set
		{
			ReportPropertyChanging("FeeTime");
			_FeeTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FeeTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int OriginalFeeMin
	{
		get
		{
			return _OriginalFeeMin;
		}
		set
		{
			ReportPropertyChanging("OriginalFeeMin");
			_OriginalFeeMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OriginalFeeMin");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ChargeFreeMin
	{
		get
		{
			return _ChargeFreeMin;
		}
		set
		{
			ReportPropertyChanging("ChargeFreeMin");
			_ChargeFreeMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeFreeMin");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ChargeMin
	{
		get
		{
			return _ChargeMin;
		}
		set
		{
			ReportPropertyChanging("ChargeMin");
			_ChargeMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeMin");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal TotalCharge
	{
		get
		{
			return _TotalCharge;
		}
		set
		{
			ReportPropertyChanging("TotalCharge");
			_TotalCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TotalCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal ActualCharge
	{
		get
		{
			return _ActualCharge;
		}
		set
		{
			ReportPropertyChanging("ActualCharge");
			_ActualCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ActualCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? PenaltyAmount
	{
		get
		{
			return _PenaltyAmount;
		}
		set
		{
			ReportPropertyChanging("PenaltyAmount");
			_PenaltyAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PenaltyAmount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? FreeCharge
	{
		get
		{
			return _FreeCharge;
		}
		set
		{
			ReportPropertyChanging("FreeCharge");
			_FreeCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal HKDPaidAmount
	{
		get
		{
			return _HKDPaidAmount;
		}
		set
		{
			ReportPropertyChanging("HKDPaidAmount");
			_HKDPaidAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("HKDPaidAmount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal MOPPaidAmount
	{
		get
		{
			return _MOPPaidAmount;
		}
		set
		{
			ReportPropertyChanging("MOPPaidAmount");
			_MOPPaidAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MOPPaidAmount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal OthPaidAmoun
	{
		get
		{
			return _OthPaidAmoun;
		}
		set
		{
			ReportPropertyChanging("OthPaidAmoun");
			_OthPaidAmoun = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OthPaidAmoun");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PaidCurrencyString
	{
		get
		{
			return _PaidCurrencyString;
		}
		set
		{
			ReportPropertyChanging("PaidCurrencyString");
			_PaidCurrencyString = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PaidCurrencyString");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ChangeCurrencyString
	{
		get
		{
			return _ChangeCurrencyString;
		}
		set
		{
			ReportPropertyChanging("ChangeCurrencyString");
			_ChangeCurrencyString = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ChangeCurrencyString");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal HKDChangeAmount
	{
		get
		{
			return _HKDChangeAmount;
		}
		set
		{
			ReportPropertyChanging("HKDChangeAmount");
			_HKDChangeAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("HKDChangeAmount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal MOPChangeAmount
	{
		get
		{
			return _MOPChangeAmount;
		}
		set
		{
			ReportPropertyChanging("MOPChangeAmount");
			_MOPChangeAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MOPChangeAmount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal OthChangeAmount
	{
		get
		{
			return _OthChangeAmount;
		}
		set
		{
			ReportPropertyChanging("OthChangeAmount");
			_OthChangeAmount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OthChangeAmount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public bool? IsLost
	{
		get
		{
			return _IsLost;
		}
		set
		{
			ReportPropertyChanging("IsLost");
			_IsLost = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsLost");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public bool? IsDamage
	{
		get
		{
			return _IsDamage;
		}
		set
		{
			ReportPropertyChanging("IsDamage");
			_IsDamage = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDamage");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? IsOverTime
	{
		get
		{
			return _IsOverTime;
		}
		set
		{
			ReportPropertyChanging("IsOverTime");
			_IsOverTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsOverTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? IsAllFree
	{
		get
		{
			return _IsAllFree;
		}
		set
		{
			ReportPropertyChanging("IsAllFree");
			_IsAllFree = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsAllFree");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? StaffID
	{
		get
		{
			return _StaffID;
		}
		set
		{
			ReportPropertyChanging("StaffID");
			_StaffID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StaffID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string StaffName
	{
		get
		{
			return _StaffName;
		}
		set
		{
			ReportPropertyChanging("StaffName");
			_StaffName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("StaffName");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ShiftID
	{
		get
		{
			return _ShiftID;
		}
		set
		{
			ReportPropertyChanging("ShiftID");
			_ShiftID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ShiftID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? CreateTime
	{
		get
		{
			return _CreateTime;
		}
		set
		{
			ReportPropertyChanging("CreateTime");
			_CreateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CreateTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? BufferTime
	{
		get
		{
			return _BufferTime;
		}
		set
		{
			ReportPropertyChanging("BufferTime");
			_BufferTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BufferTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int BillType
	{
		get
		{
			return _BillType;
		}
		set
		{
			ReportPropertyChanging("BillType");
			_BillType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BillType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Remark
	{
		get
		{
			return _Remark;
		}
		set
		{
			ReportPropertyChanging("Remark");
			_Remark = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Remark");
		}
	}

	public static APS_TimeChargeRecord CreateAPS_TimeChargeRecord(int timeChargeRecordID, int stationID, string fromAPS, int transactionID, string cardCode, int parkTypeID, DateTime inTime, DateTime feeTime, int originalFeeMin, int chargeFreeMin, int chargeMin, decimal totalCharge, decimal actualCharge, decimal hKDPaidAmount, decimal mOPPaidAmount, decimal othPaidAmoun, decimal hKDChangeAmount, decimal mOPChangeAmount, decimal othChangeAmount, int shiftID, int billType)
	{
		APS_TimeChargeRecord aPS_TimeChargeRecord = new APS_TimeChargeRecord();
		aPS_TimeChargeRecord.TimeChargeRecordID = timeChargeRecordID;
		aPS_TimeChargeRecord.StationID = stationID;
		aPS_TimeChargeRecord.FromAPS = fromAPS;
		aPS_TimeChargeRecord.TransactionID = transactionID;
		aPS_TimeChargeRecord.CardCode = cardCode;
		aPS_TimeChargeRecord.ParkTypeID = parkTypeID;
		aPS_TimeChargeRecord.InTime = inTime;
		aPS_TimeChargeRecord.FeeTime = feeTime;
		aPS_TimeChargeRecord.OriginalFeeMin = originalFeeMin;
		aPS_TimeChargeRecord.ChargeFreeMin = chargeFreeMin;
		aPS_TimeChargeRecord.ChargeMin = chargeMin;
		aPS_TimeChargeRecord.TotalCharge = totalCharge;
		aPS_TimeChargeRecord.ActualCharge = actualCharge;
		aPS_TimeChargeRecord.HKDPaidAmount = hKDPaidAmount;
		aPS_TimeChargeRecord.MOPPaidAmount = mOPPaidAmount;
		aPS_TimeChargeRecord.OthPaidAmoun = othPaidAmoun;
		aPS_TimeChargeRecord.HKDChangeAmount = hKDChangeAmount;
		aPS_TimeChargeRecord.MOPChangeAmount = mOPChangeAmount;
		aPS_TimeChargeRecord.OthChangeAmount = othChangeAmount;
		aPS_TimeChargeRecord.ShiftID = shiftID;
		aPS_TimeChargeRecord.BillType = billType;
		return aPS_TimeChargeRecord;
	}
}
