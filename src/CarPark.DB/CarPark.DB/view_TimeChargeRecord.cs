using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_TimeChargeRecord")]
public class view_TimeChargeRecord : EntityObject
{
	private string _CardCode;

	private int _ParkTypeID;

	private DateTime? _InTime;

	private DateTime _ChargeTime;

	private int _ChargeMin;

	private int _ParkMin;

	private decimal _TotalCharge;

	private int _FreeMin;

	private decimal _FreeCharge;

	private int _BillType;

	private int _ShiftID;

	private string _StaffCode;

	private decimal? _Fine;

	private int _TimeChargeID;

	private int? _AreaID;

	private int? _TenatID;

	private string _TenatNo;

	private string _TenatNameCn;

	private string _TenatNamePt;

	private string _PeriodofTime;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public string CardCode
	{
		get
		{
			return _CardCode;
		}
		set
		{
			if (_CardCode != value)
			{
				ReportPropertyChanging("CardCode");
				_CardCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("CardCode");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ParkTypeID
	{
		get
		{
			return _ParkTypeID;
		}
		set
		{
			if (_ParkTypeID != value)
			{
				ReportPropertyChanging("ParkTypeID");
				_ParkTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ParkTypeID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? InTime
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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public DateTime ChargeTime
	{
		get
		{
			return _ChargeTime;
		}
		set
		{
			if (_ChargeTime != value)
			{
				ReportPropertyChanging("ChargeTime");
				_ChargeTime = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ChargeTime");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ChargeMin
	{
		get
		{
			return _ChargeMin;
		}
		set
		{
			if (_ChargeMin != value)
			{
				ReportPropertyChanging("ChargeMin");
				_ChargeMin = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ChargeMin");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int ParkMin
	{
		get
		{
			return _ParkMin;
		}
		set
		{
			if (_ParkMin != value)
			{
				ReportPropertyChanging("ParkMin");
				_ParkMin = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ParkMin");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public decimal TotalCharge
	{
		get
		{
			return _TotalCharge;
		}
		set
		{
			if (_TotalCharge != value)
			{
				ReportPropertyChanging("TotalCharge");
				_TotalCharge = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TotalCharge");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int FreeMin
	{
		get
		{
			return _FreeMin;
		}
		set
		{
			if (_FreeMin != value)
			{
				ReportPropertyChanging("FreeMin");
				_FreeMin = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("FreeMin");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public decimal FreeCharge
	{
		get
		{
			return _FreeCharge;
		}
		set
		{
			if (_FreeCharge != value)
			{
				ReportPropertyChanging("FreeCharge");
				_FreeCharge = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("FreeCharge");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int BillType
	{
		get
		{
			return _BillType;
		}
		set
		{
			if (_BillType != value)
			{
				ReportPropertyChanging("BillType");
				_BillType = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("BillType");
			}
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public string StaffCode
	{
		get
		{
			return _StaffCode;
		}
		set
		{
			if (_StaffCode != value)
			{
				ReportPropertyChanging("StaffCode");
				_StaffCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("StaffCode");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? Fine
	{
		get
		{
			return _Fine;
		}
		set
		{
			ReportPropertyChanging("Fine");
			_Fine = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Fine");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int TimeChargeID
	{
		get
		{
			return _TimeChargeID;
		}
		set
		{
			if (_TimeChargeID != value)
			{
				ReportPropertyChanging("TimeChargeID");
				_TimeChargeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TimeChargeID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? AreaID
	{
		get
		{
			return _AreaID;
		}
		set
		{
			ReportPropertyChanging("AreaID");
			_AreaID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AreaID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? TenatID
	{
		get
		{
			return _TenatID;
		}
		set
		{
			ReportPropertyChanging("TenatID");
			_TenatID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TenatID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string TenatNo
	{
		get
		{
			return _TenatNo;
		}
		set
		{
			ReportPropertyChanging("TenatNo");
			_TenatNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TenatNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string TenatNameCn
	{
		get
		{
			return _TenatNameCn;
		}
		set
		{
			ReportPropertyChanging("TenatNameCn");
			_TenatNameCn = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TenatNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string TenatNamePt
	{
		get
		{
			return _TenatNamePt;
		}
		set
		{
			ReportPropertyChanging("TenatNamePt");
			_TenatNamePt = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TenatNamePt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PeriodofTime
	{
		get
		{
			return _PeriodofTime;
		}
		set
		{
			ReportPropertyChanging("PeriodofTime");
			_PeriodofTime = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PeriodofTime");
		}
	}

	public static view_TimeChargeRecord Createview_TimeChargeRecord(string cardCode, int parkTypeID, DateTime chargeTime, int chargeMin, int parkMin, decimal totalCharge, int freeMin, decimal freeCharge, int billType, int shiftID, string staffCode, int timeChargeID)
	{
		view_TimeChargeRecord view_TimeChargeRecord2 = new view_TimeChargeRecord();
		view_TimeChargeRecord2.CardCode = cardCode;
		view_TimeChargeRecord2.ParkTypeID = parkTypeID;
		view_TimeChargeRecord2.ChargeTime = chargeTime;
		view_TimeChargeRecord2.ChargeMin = chargeMin;
		view_TimeChargeRecord2.ParkMin = parkMin;
		view_TimeChargeRecord2.TotalCharge = totalCharge;
		view_TimeChargeRecord2.FreeMin = freeMin;
		view_TimeChargeRecord2.FreeCharge = freeCharge;
		view_TimeChargeRecord2.BillType = billType;
		view_TimeChargeRecord2.ShiftID = shiftID;
		view_TimeChargeRecord2.StaffCode = staffCode;
		view_TimeChargeRecord2.TimeChargeID = timeChargeID;
		return view_TimeChargeRecord2;
	}
}
