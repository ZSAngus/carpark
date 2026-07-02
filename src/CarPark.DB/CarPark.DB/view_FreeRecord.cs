using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.Lang;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_FreeRecord")]
[DataContract(IsReference = true)]
public class view_FreeRecord : EntityObject
{
	private int _ChargeRecordID;

	private int? _TenatID;

	private string _TenatNo;

	private string _TenatNameCn;

	private string _TenatNamePt;

	private int? _CustomFreeTypeID;

	private string _CustomFreeNameCn;

	private string _CustomFreeNamePt;

	private int _FreeMinutes;

	private decimal _FreeCharge;

	private decimal? _TotalCharge;

	private int? _ShiftID;

	private string _StaffCode;

	private DateTime? _ChargeTime;

	private string _CardCode;

	private string _Remark;

	public string TenatName
	{
		get
		{
			string tenatNameCn = TenatNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return tenatNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return TenatNamePt;
			default:
				return tenatNameCn;
			}
		}
	}

	public string CustomFreeName
	{
		get
		{
			string customFreeNameCn = CustomFreeNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return customFreeNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return CustomFreeNamePt;
			default:
				return customFreeNameCn;
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ChargeRecordID
	{
		get
		{
			return _ChargeRecordID;
		}
		set
		{
			if (_ChargeRecordID != value)
			{
				ReportPropertyChanging("ChargeRecordID");
				_ChargeRecordID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ChargeRecordID");
			}
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(DBCustomFreeTypeSource))]
	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? CustomFreeTypeID
	{
		get
		{
			return _CustomFreeTypeID;
		}
		set
		{
			ReportPropertyChanging("CustomFreeTypeID");
			_CustomFreeTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CustomFreeTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CustomFreeNameCn
	{
		get
		{
			return _CustomFreeNameCn;
		}
		set
		{
			ReportPropertyChanging("CustomFreeNameCn");
			_CustomFreeNameCn = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CustomFreeNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CustomFreeNamePt
	{
		get
		{
			return _CustomFreeNamePt;
		}
		set
		{
			ReportPropertyChanging("CustomFreeNamePt");
			_CustomFreeNamePt = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CustomFreeNamePt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int FreeMinutes
	{
		get
		{
			return _FreeMinutes;
		}
		set
		{
			if (_FreeMinutes != value)
			{
				ReportPropertyChanging("FreeMinutes");
				_FreeMinutes = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("FreeMinutes");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? TotalCharge
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? ShiftID
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string StaffCode
	{
		get
		{
			return _StaffCode;
		}
		set
		{
			ReportPropertyChanging("StaffCode");
			_StaffCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("StaffCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? ChargeTime
	{
		get
		{
			return _ChargeTime;
		}
		set
		{
			ReportPropertyChanging("ChargeTime");
			_ChargeTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargeTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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
			_CardCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	public static view_FreeRecord Createview_FreeRecord(int chargeRecordID, int freeMinutes, decimal freeCharge)
	{
		view_FreeRecord view_FreeRecord2 = new view_FreeRecord();
		view_FreeRecord2.ChargeRecordID = chargeRecordID;
		view_FreeRecord2.FreeMinutes = freeMinutes;
		view_FreeRecord2.FreeCharge = freeCharge;
		return view_FreeRecord2;
	}
}
