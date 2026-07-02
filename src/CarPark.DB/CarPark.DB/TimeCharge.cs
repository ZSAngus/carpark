using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "TimeCharge")]
public class TimeCharge : EntityObject
{
	private int? _AreaID;

	private int? _ChargeType;

	private int _TimeChargeTypeID;

	private string _TimeChargeNameCn;

	private string _TimeChargeNamePt;

	private decimal _NormalChargeB;

	private int _ParkTypeID;

	private decimal _FineChargeB;

	private DateTime _StartDate;

	private decimal _NormalChargeA;

	private decimal _FineChargeA;

	private int _FirstMinA;

	private decimal _FirstNormalChargeA;

	private int _FirstMinB;

	private decimal _FirstNormalChargeB;

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
	public int? ChargeType
	{
		get
		{
			return _ChargeType;
		}
		set
		{
			ReportPropertyChanging("ChargeType");
			_ChargeType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeType");
		}
	}

	public int FirstMin
	{
		get
		{
			if (!(StartDate > DateTime.Now))
			{
				return FirstMinB;
			}
			return FirstMinA;
		}
	}

	public decimal FirstNormalCharge
	{
		get
		{
			if (!(StartDate > DateTime.Now))
			{
				return FirstNormalChargeB;
			}
			return FirstNormalChargeA;
		}
	}

	public decimal NormalCharge
	{
		get
		{
			if (!(StartDate > DateTime.Now))
			{
				return NormalChargeB;
			}
			return NormalChargeA;
		}
	}

	public decimal FineCharge
	{
		get
		{
			if (!(StartDate > DateTime.Now))
			{
				return FineChargeB;
			}
			return FineChargeA;
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[BindingControlEditStyle(EnumEditStyle.Value)]
	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal NormalChargeB
	{
		get
		{
			return _NormalChargeB;
		}
		set
		{
			ReportPropertyChanging("NormalChargeB");
			_NormalChargeB = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("NormalChargeB");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(EnumParkTypeSource))]
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

	[BindingControlEditStyle(EnumEditStyle.Value)]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal FineChargeB
	{
		get
		{
			return _FineChargeB;
		}
		set
		{
			ReportPropertyChanging("FineChargeB");
			_FineChargeB = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FineChargeB");
		}
	}

	[BindingControlEditStyle(EnumEditStyle.DateTime)]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime StartDate
	{
		get
		{
			return _StartDate;
		}
		set
		{
			ReportPropertyChanging("StartDate");
			_StartDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartDate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	[BindingControlEditStyle(EnumEditStyle.Value)]
	public decimal NormalChargeA
	{
		get
		{
			return _NormalChargeA;
		}
		set
		{
			ReportPropertyChanging("NormalChargeA");
			_NormalChargeA = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("NormalChargeA");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.Value)]
	[DataMember]
	public decimal FineChargeA
	{
		get
		{
			return _FineChargeA;
		}
		set
		{
			ReportPropertyChanging("FineChargeA");
			_FineChargeA = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FineChargeA");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.Value)]
	[DataMember]
	public int FirstMinA
	{
		get
		{
			return _FirstMinA;
		}
		set
		{
			ReportPropertyChanging("FirstMinA");
			_FirstMinA = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FirstMinA");
		}
	}

	[BindingControlEditStyle(EnumEditStyle.Value)]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal FirstNormalChargeA
	{
		get
		{
			return _FirstNormalChargeA;
		}
		set
		{
			ReportPropertyChanging("FirstNormalChargeA");
			_FirstNormalChargeA = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FirstNormalChargeA");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.Value)]
	[DataMember]
	public int FirstMinB
	{
		get
		{
			return _FirstMinB;
		}
		set
		{
			ReportPropertyChanging("FirstMinB");
			_FirstMinB = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FirstMinB");
		}
	}

	[BindingControlEditStyle(EnumEditStyle.Value)]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal FirstNormalChargeB
	{
		get
		{
			return _FirstNormalChargeB;
		}
		set
		{
			ReportPropertyChanging("FirstNormalChargeB");
			_FirstNormalChargeB = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FirstNormalChargeB");
		}
	}

	public static TimeCharge CreateTimeCharge(int timeChargeTypeID, string timeChargeNameCn, string timeChargeNamePt, decimal normalChargeB, int parkTypeID, decimal fineChargeB, DateTime startDate, decimal normalChargeA, decimal fineChargeA, int firstMinA, decimal firstNormalChargeA, int firstMinB, decimal firstNormalChargeB)
	{
		TimeCharge timeCharge = new TimeCharge();
		timeCharge.TimeChargeTypeID = timeChargeTypeID;
		timeCharge.TimeChargeNameCn = timeChargeNameCn;
		timeCharge.TimeChargeNamePt = timeChargeNamePt;
		timeCharge.NormalChargeB = normalChargeB;
		timeCharge.ParkTypeID = parkTypeID;
		timeCharge.FineChargeB = fineChargeB;
		timeCharge.StartDate = startDate;
		timeCharge.NormalChargeA = normalChargeA;
		timeCharge.FineChargeA = fineChargeA;
		timeCharge.FirstMinA = firstMinA;
		timeCharge.FirstNormalChargeA = firstNormalChargeA;
		timeCharge.FirstMinB = firstMinB;
		timeCharge.FirstNormalChargeB = firstNormalChargeB;
		return timeCharge;
	}
}
