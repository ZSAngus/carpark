using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "TimeCharge_New")]
[DataContract(IsReference = true)]
public class TimeCharge_New : EntityObject
{
	private int _TimeChargeTypeID;

	private string _TimeChargeNameCn;

	private string _TimeChargeNamePt;

	private int _ParkTypeID;

	private int _DateTypeID;

	private int _StartHR;

	private int _EndHR;

	private decimal _Charge;

	private int _FirstMin;

	private decimal _FirstCharge;

	private int _FirstChargeMode;

	private decimal _FineCharge;

	private decimal _StaffPrice;

	private bool _AfterFlag;

	private bool _IsHalfHR;

	private int _AreaID;

	private bool _IsDelete;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int TimeChargeTypeID
	{
		get
		{
			return _TimeChargeTypeID;
		}
		set
		{
			if (_TimeChargeTypeID != value)
			{
				ReportPropertyChanging("TimeChargeTypeID");
				_TimeChargeTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TimeChargeTypeID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string TimeChargeNameCn
	{
		get
		{
			return _TimeChargeNameCn;
		}
		set
		{
			ReportPropertyChanging("TimeChargeNameCn");
			_TimeChargeNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TimeChargeNameCn");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string TimeChargeNamePt
	{
		get
		{
			return _TimeChargeNamePt;
		}
		set
		{
			ReportPropertyChanging("TimeChargeNamePt");
			_TimeChargeNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TimeChargeNamePt");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int DateTypeID
	{
		get
		{
			return _DateTypeID;
		}
		set
		{
			ReportPropertyChanging("DateTypeID");
			_DateTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DateTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int StartHR
	{
		get
		{
			return _StartHR;
		}
		set
		{
			ReportPropertyChanging("StartHR");
			_StartHR = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartHR");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int EndHR
	{
		get
		{
			return _EndHR;
		}
		set
		{
			ReportPropertyChanging("EndHR");
			_EndHR = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndHR");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal Charge
	{
		get
		{
			return _Charge;
		}
		set
		{
			ReportPropertyChanging("Charge");
			_Charge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Charge");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FirstMin
	{
		get
		{
			return _FirstMin;
		}
		set
		{
			ReportPropertyChanging("FirstMin");
			_FirstMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FirstMin");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal FirstCharge
	{
		get
		{
			return _FirstCharge;
		}
		set
		{
			ReportPropertyChanging("FirstCharge");
			_FirstCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FirstCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int FirstChargeMode
	{
		get
		{
			return _FirstChargeMode;
		}
		set
		{
			ReportPropertyChanging("FirstChargeMode");
			_FirstChargeMode = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FirstChargeMode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal FineCharge
	{
		get
		{
			return _FineCharge;
		}
		set
		{
			ReportPropertyChanging("FineCharge");
			_FineCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FineCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal StaffPrice
	{
		get
		{
			return _StaffPrice;
		}
		set
		{
			ReportPropertyChanging("StaffPrice");
			_StaffPrice = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StaffPrice");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool AfterFlag
	{
		get
		{
			return _AfterFlag;
		}
		set
		{
			ReportPropertyChanging("AfterFlag");
			_AfterFlag = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AfterFlag");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsHalfHR
	{
		get
		{
			return _IsHalfHR;
		}
		set
		{
			ReportPropertyChanging("IsHalfHR");
			_IsHalfHR = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsHalfHR");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int AreaID
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	public static TimeCharge_New CreateTimeCharge_New(int timeChargeTypeID, string timeChargeNameCn, string timeChargeNamePt, int parkTypeID, int dateTypeID, int startHR, int endHR, decimal charge, int firstMin, decimal firstCharge, int firstChargeMode, decimal fineCharge, decimal staffPrice, bool afterFlag, bool isHalfHR, int areaID, bool isDelete)
	{
		TimeCharge_New timeCharge_New = new TimeCharge_New();
		timeCharge_New.TimeChargeTypeID = timeChargeTypeID;
		timeCharge_New.TimeChargeNameCn = timeChargeNameCn;
		timeCharge_New.TimeChargeNamePt = timeChargeNamePt;
		timeCharge_New.ParkTypeID = parkTypeID;
		timeCharge_New.DateTypeID = dateTypeID;
		timeCharge_New.StartHR = startHR;
		timeCharge_New.EndHR = endHR;
		timeCharge_New.Charge = charge;
		timeCharge_New.FirstMin = firstMin;
		timeCharge_New.FirstCharge = firstCharge;
		timeCharge_New.FirstChargeMode = firstChargeMode;
		timeCharge_New.FineCharge = fineCharge;
		timeCharge_New.StaffPrice = staffPrice;
		timeCharge_New.AfterFlag = afterFlag;
		timeCharge_New.IsHalfHR = isHalfHR;
		timeCharge_New.AreaID = areaID;
		timeCharge_New.IsDelete = isDelete;
		return timeCharge_New;
	}
}
