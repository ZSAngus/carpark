using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.Core;
using SkyInno.Lang;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ParkAreaExtend")]
public class ParkAreaExtend : EntityObject
{
	private int? _FloatParkSupply3;

	private int? _FloatParkUse3;

	private int? _FloatParkSupply4;

	private int? _FloatParkUse4;

	private int? _FloatParkSupply5;

	private int? _FloatParkUse5;

	private int? _FloatParkSupply6;

	private int? _FloatParkUse6;

	private int? _FloatParkSupply7;

	private int? _FloatParkUse7;

	private int? _FloatParkSupply8;

	private int? _FloatParkUse8;

	private int? _FloatParkSupply9;

	private int? _FloatParkUse9;

	private int? _FloatParkSupply10;

	private int? _FloatParkUse10;

	private DateTime _StartDate;

	private int _AreaID;

	private int _ParkTypeID;

	private int _TotalSupply;

	private int _CurrentUse;

	private int _ExtendCount;

	private int _FixParkSupply;

	private int _FixParkUse;

	private int _FloatParkSupply;

	private int _FloatParkUse;

	private int _TimeChargeSupply;

	private int _TimeChargeUse;

	private string _ExtendNameCn;

	private string _ExtendNamePt;

	private bool _CustomFunnSigh;

	private int? _QuotaCount;

	private string _SerialNo;

	private bool _IsDelete;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkSupply3
	{
		get
		{
			return _FloatParkSupply3;
		}
		set
		{
			ReportPropertyChanging("FloatParkSupply3");
			_FloatParkSupply3 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkSupply3");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkUse3
	{
		get
		{
			return _FloatParkUse3;
		}
		set
		{
			ReportPropertyChanging("FloatParkUse3");
			_FloatParkUse3 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkUse3");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkSupply4
	{
		get
		{
			return _FloatParkSupply4;
		}
		set
		{
			ReportPropertyChanging("FloatParkSupply4");
			_FloatParkSupply4 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkSupply4");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkUse4
	{
		get
		{
			return _FloatParkUse4;
		}
		set
		{
			ReportPropertyChanging("FloatParkUse4");
			_FloatParkUse4 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkUse4");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? FloatParkSupply5
	{
		get
		{
			return _FloatParkSupply5;
		}
		set
		{
			ReportPropertyChanging("FloatParkSupply5");
			_FloatParkSupply5 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkSupply5");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkUse5
	{
		get
		{
			return _FloatParkUse5;
		}
		set
		{
			ReportPropertyChanging("FloatParkUse5");
			_FloatParkUse5 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkUse5");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? FloatParkSupply6
	{
		get
		{
			return _FloatParkSupply6;
		}
		set
		{
			ReportPropertyChanging("FloatParkSupply6");
			_FloatParkSupply6 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkSupply6");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? FloatParkUse6
	{
		get
		{
			return _FloatParkUse6;
		}
		set
		{
			ReportPropertyChanging("FloatParkUse6");
			_FloatParkUse6 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkUse6");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkSupply7
	{
		get
		{
			return _FloatParkSupply7;
		}
		set
		{
			ReportPropertyChanging("FloatParkSupply7");
			_FloatParkSupply7 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkSupply7");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkUse7
	{
		get
		{
			return _FloatParkUse7;
		}
		set
		{
			ReportPropertyChanging("FloatParkUse7");
			_FloatParkUse7 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkUse7");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkSupply8
	{
		get
		{
			return _FloatParkSupply8;
		}
		set
		{
			ReportPropertyChanging("FloatParkSupply8");
			_FloatParkSupply8 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkSupply8");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? FloatParkUse8
	{
		get
		{
			return _FloatParkUse8;
		}
		set
		{
			ReportPropertyChanging("FloatParkUse8");
			_FloatParkUse8 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkUse8");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? FloatParkSupply9
	{
		get
		{
			return _FloatParkSupply9;
		}
		set
		{
			ReportPropertyChanging("FloatParkSupply9");
			_FloatParkSupply9 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkSupply9");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkUse9
	{
		get
		{
			return _FloatParkUse9;
		}
		set
		{
			ReportPropertyChanging("FloatParkUse9");
			_FloatParkUse9 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkUse9");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? FloatParkSupply10
	{
		get
		{
			return _FloatParkSupply10;
		}
		set
		{
			ReportPropertyChanging("FloatParkSupply10");
			_FloatParkSupply10 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkSupply10");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? FloatParkUse10
	{
		get
		{
			return _FloatParkUse10;
		}
		set
		{
			ReportPropertyChanging("FloatParkUse10");
			_FloatParkUse10 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkUse10");
		}
	}

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

	public string ExtendName
	{
		get
		{
			string extendNameCn = ExtendNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return extendNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return ExtendNamePt;
			default:
				return extendNameCn;
			}
		}
	}

	public EnumParkType ParkType
	{
		get
		{
			return (EnumParkType)ParkTypeID;
		}
		set
		{
			ParkTypeID = (int)value;
		}
	}

	public int TotalRemain
	{
		get
		{
			int num = 0;
			num = TotalSupply - CurrentUse;
			if (CustomFunnSigh || num < 0)
			{
				num = 0;
			}
			return num;
		}
	}

	public int TimeChargRemain
	{
		get
		{
			int num = 0;
			num = TimeChargeSupply + ExtendCount - TimeChargeUse;
			if (CustomFunnSigh || num < 0)
			{
				num = 0;
			}
			else if (num > TimeChargeSupply)
			{
				num = TimeChargeSupply;
			}
			return num;
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int AreaID
	{
		get
		{
			return _AreaID;
		}
		set
		{
			if (_AreaID != value)
			{
				ReportPropertyChanging("AreaID");
				_AreaID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("AreaID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Value)]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int TotalSupply
	{
		get
		{
			return _TotalSupply;
		}
		set
		{
			ReportPropertyChanging("TotalSupply");
			_TotalSupply = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TotalSupply");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EditStyle = EnumEditStyle.Value)]
	[DataMember]
	public int CurrentUse
	{
		get
		{
			return _CurrentUse;
		}
		set
		{
			ReportPropertyChanging("CurrentUse");
			_CurrentUse = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CurrentUse");
		}
	}

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Value)]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ExtendCount
	{
		get
		{
			return _ExtendCount;
		}
		set
		{
			ReportPropertyChanging("ExtendCount");
			_ExtendCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ExtendCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int FixParkSupply
	{
		get
		{
			return _FixParkSupply;
		}
		set
		{
			ReportPropertyChanging("FixParkSupply");
			_FixParkSupply = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FixParkSupply");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FixParkUse
	{
		get
		{
			return _FixParkUse;
		}
		set
		{
			ReportPropertyChanging("FixParkUse");
			_FixParkUse = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FixParkUse");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int FloatParkSupply
	{
		get
		{
			return _FloatParkSupply;
		}
		set
		{
			ReportPropertyChanging("FloatParkSupply");
			_FloatParkSupply = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkSupply");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FloatParkUse
	{
		get
		{
			return _FloatParkUse;
		}
		set
		{
			ReportPropertyChanging("FloatParkUse");
			_FloatParkUse = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FloatParkUse");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int TimeChargeSupply
	{
		get
		{
			return _TimeChargeSupply;
		}
		set
		{
			ReportPropertyChanging("TimeChargeSupply");
			_TimeChargeSupply = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TimeChargeSupply");
		}
	}

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Value)]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int TimeChargeUse
	{
		get
		{
			return _TimeChargeUse;
		}
		set
		{
			ReportPropertyChanging("TimeChargeUse");
			_TimeChargeUse = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TimeChargeUse");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string ExtendNameCn
	{
		get
		{
			return _ExtendNameCn;
		}
		set
		{
			ReportPropertyChanging("ExtendNameCn");
			_ExtendNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("ExtendNameCn");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string ExtendNamePt
	{
		get
		{
			return _ExtendNamePt;
		}
		set
		{
			ReportPropertyChanging("ExtendNamePt");
			_ExtendNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("ExtendNamePt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool CustomFunnSigh
	{
		get
		{
			return _CustomFunnSigh;
		}
		set
		{
			ReportPropertyChanging("CustomFunnSigh");
			_CustomFunnSigh = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CustomFunnSigh");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int? QuotaCount
	{
		get
		{
			return _QuotaCount;
		}
		set
		{
			ReportPropertyChanging("QuotaCount");
			_QuotaCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("QuotaCount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string SerialNo
	{
		get
		{
			return _SerialNo;
		}
		set
		{
			ReportPropertyChanging("SerialNo");
			_SerialNo = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("SerialNo");
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

	public static ParkAreaExtend CreateParkAreaExtend(int areaID, int parkTypeID, int totalSupply, int currentUse, int extendCount, int fixParkSupply, int fixParkUse, int floatParkSupply, int floatParkUse, int timeChargeSupply, int timeChargeUse, string extendNameCn, string extendNamePt, bool customFunnSigh, int quotaCount, string serialNo, bool isDelete, DateTime startDate)
	{
		ParkAreaExtend parkAreaExtend = new ParkAreaExtend();
		parkAreaExtend.AreaID = areaID;
		parkAreaExtend.ParkTypeID = parkTypeID;
		parkAreaExtend.TotalSupply = totalSupply;
		parkAreaExtend.CurrentUse = currentUse;
		parkAreaExtend.ExtendCount = extendCount;
		parkAreaExtend.FixParkSupply = fixParkSupply;
		parkAreaExtend.FixParkUse = fixParkUse;
		parkAreaExtend.FloatParkSupply = floatParkSupply;
		parkAreaExtend.FloatParkUse = floatParkUse;
		parkAreaExtend.TimeChargeSupply = timeChargeSupply;
		parkAreaExtend.TimeChargeUse = timeChargeUse;
		parkAreaExtend.ExtendNameCn = extendNameCn;
		parkAreaExtend.ExtendNamePt = extendNamePt;
		parkAreaExtend.CustomFunnSigh = customFunnSigh;
		parkAreaExtend.QuotaCount = quotaCount;
		parkAreaExtend.SerialNo = serialNo;
		parkAreaExtend.IsDelete = isDelete;
		parkAreaExtend.StartDate = startDate;
		return parkAreaExtend;
	}
}
