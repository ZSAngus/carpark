using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ShiftRecord")]
[DataContract(IsReference = true)]
public class ShiftRecord : EntityObject
{
	private string _ReceiptObj;

	private decimal? _NoSenseCharge;

	private int? _NoSenseCount;

	private int? _PaymentConversionCount;

	private decimal? _PaymentConversionCharge;

	private int? _MPassPosCount;

	private decimal? _MPassPosCharge;

	private int? _MPassGateCount;

	private decimal? _MPassGateCharge;

	private string _ManualChargeRemark;

	private decimal? _ManualChargeCharge;

	private int? _QPassCount;

	private decimal? _QPassCharge;

	private int? _MPassCount;

	private decimal? _MPassCharge;

	private int? _MPassDecalCount;

	private decimal? _MPassDecalCharge;

	private int? _TimeChargeCount;

	private decimal? _TimeCharge;

	private int? _LostCount;

	private decimal? _LostCharge;

	private int? _DamageCount;

	private decimal? _DamageCharge;

	private int? _TimeoutCount;

	private decimal? _TimeoutCharge;

	private int? _RentalCount;

	private decimal? _RentalCharge;

	private int? _RentalDepositCount;

	private decimal? _RentalDepositCharge;

	private int? _VoidDepositCount;

	private decimal? _VoidDepositCharge;

	private int? _VoidRentalCount;

	private decimal? _VoidRentalCharge;

	private int? _TotalCount;

	private decimal? _CashCharge;

	private int? _FeeCount;

	private decimal? _FeeCharge;

	private int? _OpenGateCount;

	private int _ShiftID;

	private DateTime _StartTime;

	private string _StartStaffCode;

	private DateTime? _EndTime;

	private string _EndStaffCode;

	private bool _IsComplete;

	private decimal _StartBalance;

	private decimal _EndBalance;

	private decimal _TotalTimeCharge;

	private decimal _TotalRentalCharge;

	private decimal _TotalRentalDeposit;

	private decimal _TotalCharge;

	private string _FromStation;

	private decimal? _MOP;

	private decimal? _HKD;

	private decimal? _CREDIT_CARD;

	private bool _IsDelete;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string ReceiptObj
	{
		get
		{
			return _ReceiptObj;
		}
		set
		{
			ReportPropertyChanging("ReceiptObj");
			_ReceiptObj = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ReceiptObj");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? NoSenseCharge
	{
		get
		{
			return _NoSenseCharge;
		}
		set
		{
			ReportPropertyChanging("NoSenseCharge");
			_NoSenseCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("NoSenseCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? NoSenseCount
	{
		get
		{
			return _NoSenseCount;
		}
		set
		{
			ReportPropertyChanging("NoSenseCount");
			_NoSenseCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("NoSenseCount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? PaymentConversionCount
	{
		get
		{
			return _PaymentConversionCount;
		}
		set
		{
			ReportPropertyChanging("PaymentConversionCount");
			_PaymentConversionCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PaymentConversionCount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? PaymentConversionCharge
	{
		get
		{
			return _PaymentConversionCharge;
		}
		set
		{
			ReportPropertyChanging("PaymentConversionCharge");
			_PaymentConversionCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PaymentConversionCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? MPassPosCount
	{
		get
		{
			return _MPassPosCount;
		}
		set
		{
			ReportPropertyChanging("MPassPosCount");
			_MPassPosCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MPassPosCount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? MPassPosCharge
	{
		get
		{
			return _MPassPosCharge;
		}
		set
		{
			ReportPropertyChanging("MPassPosCharge");
			_MPassPosCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MPassPosCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? MPassGateCount
	{
		get
		{
			return _MPassGateCount;
		}
		set
		{
			ReportPropertyChanging("MPassGateCount");
			_MPassGateCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MPassGateCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? MPassGateCharge
	{
		get
		{
			return _MPassGateCharge;
		}
		set
		{
			ReportPropertyChanging("MPassGateCharge");
			_MPassGateCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MPassGateCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ManualChargeRemark
	{
		get
		{
			return _ManualChargeRemark;
		}
		set
		{
			ReportPropertyChanging("ManualChargeRemark");
			_ManualChargeRemark = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ManualChargeRemark");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? ManualChargeCharge
	{
		get
		{
			return _ManualChargeCharge;
		}
		set
		{
			ReportPropertyChanging("ManualChargeCharge");
			_ManualChargeCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ManualChargeCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? QPassCount
	{
		get
		{
			return _QPassCount;
		}
		set
		{
			ReportPropertyChanging("QPassCount");
			_QPassCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("QPassCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? QPassCharge
	{
		get
		{
			return _QPassCharge;
		}
		set
		{
			ReportPropertyChanging("QPassCharge");
			_QPassCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("QPassCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? MPassCount
	{
		get
		{
			return _MPassCount;
		}
		set
		{
			ReportPropertyChanging("MPassCount");
			_MPassCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MPassCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? MPassCharge
	{
		get
		{
			return _MPassCharge;
		}
		set
		{
			ReportPropertyChanging("MPassCharge");
			_MPassCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MPassCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? MPassDecalCount
	{
		get
		{
			return _MPassDecalCount;
		}
		set
		{
			ReportPropertyChanging("MPassDecalCount");
			_MPassDecalCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MPassDecalCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? MPassDecalCharge
	{
		get
		{
			return _MPassDecalCharge;
		}
		set
		{
			ReportPropertyChanging("MPassDecalCharge");
			_MPassDecalCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MPassDecalCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? TimeChargeCount
	{
		get
		{
			return _TimeChargeCount;
		}
		set
		{
			ReportPropertyChanging("TimeChargeCount");
			_TimeChargeCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TimeChargeCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? TimeCharge
	{
		get
		{
			return _TimeCharge;
		}
		set
		{
			ReportPropertyChanging("TimeCharge");
			_TimeCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TimeCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? LostCount
	{
		get
		{
			return _LostCount;
		}
		set
		{
			ReportPropertyChanging("LostCount");
			_LostCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LostCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? LostCharge
	{
		get
		{
			return _LostCharge;
		}
		set
		{
			ReportPropertyChanging("LostCharge");
			_LostCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LostCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? DamageCount
	{
		get
		{
			return _DamageCount;
		}
		set
		{
			ReportPropertyChanging("DamageCount");
			_DamageCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DamageCount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? DamageCharge
	{
		get
		{
			return _DamageCharge;
		}
		set
		{
			ReportPropertyChanging("DamageCharge");
			_DamageCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DamageCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? TimeoutCount
	{
		get
		{
			return _TimeoutCount;
		}
		set
		{
			ReportPropertyChanging("TimeoutCount");
			_TimeoutCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TimeoutCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? TimeoutCharge
	{
		get
		{
			return _TimeoutCharge;
		}
		set
		{
			ReportPropertyChanging("TimeoutCharge");
			_TimeoutCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TimeoutCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? RentalCount
	{
		get
		{
			return _RentalCount;
		}
		set
		{
			ReportPropertyChanging("RentalCount");
			_RentalCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RentalCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? RentalCharge
	{
		get
		{
			return _RentalCharge;
		}
		set
		{
			ReportPropertyChanging("RentalCharge");
			_RentalCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RentalCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? RentalDepositCount
	{
		get
		{
			return _RentalDepositCount;
		}
		set
		{
			ReportPropertyChanging("RentalDepositCount");
			_RentalDepositCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RentalDepositCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? RentalDepositCharge
	{
		get
		{
			return _RentalDepositCharge;
		}
		set
		{
			ReportPropertyChanging("RentalDepositCharge");
			_RentalDepositCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RentalDepositCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? VoidDepositCount
	{
		get
		{
			return _VoidDepositCount;
		}
		set
		{
			ReportPropertyChanging("VoidDepositCount");
			_VoidDepositCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("VoidDepositCount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? VoidDepositCharge
	{
		get
		{
			return _VoidDepositCharge;
		}
		set
		{
			ReportPropertyChanging("VoidDepositCharge");
			_VoidDepositCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("VoidDepositCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? VoidRentalCount
	{
		get
		{
			return _VoidRentalCount;
		}
		set
		{
			ReportPropertyChanging("VoidRentalCount");
			_VoidRentalCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("VoidRentalCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? VoidRentalCharge
	{
		get
		{
			return _VoidRentalCharge;
		}
		set
		{
			ReportPropertyChanging("VoidRentalCharge");
			_VoidRentalCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("VoidRentalCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? TotalCount
	{
		get
		{
			return _TotalCount;
		}
		set
		{
			ReportPropertyChanging("TotalCount");
			_TotalCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TotalCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? CashCharge
	{
		get
		{
			return _CashCharge;
		}
		set
		{
			ReportPropertyChanging("CashCharge");
			_CashCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CashCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? FeeCount
	{
		get
		{
			return _FeeCount;
		}
		set
		{
			ReportPropertyChanging("FeeCount");
			_FeeCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FeeCount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? FeeCharge
	{
		get
		{
			return _FeeCharge;
		}
		set
		{
			ReportPropertyChanging("FeeCharge");
			_FeeCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FeeCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? OpenGateCount
	{
		get
		{
			return _OpenGateCount;
		}
		set
		{
			ReportPropertyChanging("OpenGateCount");
			_OpenGateCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OpenGateCount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ShiftID
	{
		get
		{
			return _ShiftID;
		}
		set
		{
			if (_ShiftID != value)
			{
				ReportPropertyChanging("ShiftID");
				_ShiftID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ShiftID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime StartTime
	{
		get
		{
			return _StartTime;
		}
		set
		{
			ReportPropertyChanging("StartTime");
			_StartTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string StartStaffCode
	{
		get
		{
			return _StartStaffCode;
		}
		set
		{
			ReportPropertyChanging("StartStaffCode");
			_StartStaffCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("StartStaffCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? EndTime
	{
		get
		{
			return _EndTime;
		}
		set
		{
			ReportPropertyChanging("EndTime");
			_EndTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string EndStaffCode
	{
		get
		{
			return _EndStaffCode;
		}
		set
		{
			ReportPropertyChanging("EndStaffCode");
			_EndStaffCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("EndStaffCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsComplete
	{
		get
		{
			return _IsComplete;
		}
		set
		{
			ReportPropertyChanging("IsComplete");
			_IsComplete = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsComplete");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal StartBalance
	{
		get
		{
			return _StartBalance;
		}
		set
		{
			ReportPropertyChanging("StartBalance");
			_StartBalance = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartBalance");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal EndBalance
	{
		get
		{
			return _EndBalance;
		}
		set
		{
			ReportPropertyChanging("EndBalance");
			_EndBalance = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndBalance");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal TotalTimeCharge
	{
		get
		{
			return _TotalTimeCharge;
		}
		set
		{
			ReportPropertyChanging("TotalTimeCharge");
			_TotalTimeCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TotalTimeCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal TotalRentalCharge
	{
		get
		{
			return _TotalRentalCharge;
		}
		set
		{
			ReportPropertyChanging("TotalRentalCharge");
			_TotalRentalCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TotalRentalCharge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal TotalRentalDeposit
	{
		get
		{
			return _TotalRentalDeposit;
		}
		set
		{
			ReportPropertyChanging("TotalRentalDeposit");
			_TotalRentalDeposit = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TotalRentalDeposit");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string FromStation
	{
		get
		{
			return _FromStation;
		}
		set
		{
			ReportPropertyChanging("FromStation");
			_FromStation = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("FromStation");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? MOP
	{
		get
		{
			return _MOP;
		}
		set
		{
			ReportPropertyChanging("MOP");
			_MOP = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("MOP");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? HKD
	{
		get
		{
			return _HKD;
		}
		set
		{
			ReportPropertyChanging("HKD");
			_HKD = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("HKD");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? CREDIT_CARD
	{
		get
		{
			return _CREDIT_CARD;
		}
		set
		{
			ReportPropertyChanging("CREDIT_CARD");
			_CREDIT_CARD = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CREDIT_CARD");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsDelete
	{
		get
		{
			return _IsDelete;
		}
		set
		{
			ReportPropertyChanging("IsDelete");
			_IsDelete = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDelete");
		}
	}

	public static ShiftRecord CreateShiftRecord(int shiftID, DateTime startTime, bool isComplete, decimal startBalance, decimal endBalance, decimal totalTimeCharge, decimal totalRentalCharge, decimal totalRentalDeposit, decimal totalCharge, string fromStation, bool isDelete)
	{
		ShiftRecord shiftRecord = new ShiftRecord();
		shiftRecord.ShiftID = shiftID;
		shiftRecord.StartTime = startTime;
		shiftRecord.IsComplete = isComplete;
		shiftRecord.StartBalance = startBalance;
		shiftRecord.EndBalance = endBalance;
		shiftRecord.TotalTimeCharge = totalTimeCharge;
		shiftRecord.TotalRentalCharge = totalRentalCharge;
		shiftRecord.TotalRentalDeposit = totalRentalDeposit;
		shiftRecord.TotalCharge = totalCharge;
		shiftRecord.FromStation = fromStation;
		shiftRecord.IsDelete = isDelete;
		return shiftRecord;
	}
}
