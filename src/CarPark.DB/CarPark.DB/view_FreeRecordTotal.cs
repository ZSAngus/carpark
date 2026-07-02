using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_FreeRecordTotal")]
public class view_FreeRecordTotal : EntityObject
{
	private int _TenatID;

	private string _TenatNo;

	private string _TenatNameCn;

	private string _TenatNamePt;

	private long? _freeCount;

	private decimal? _freeMinutes;

	private decimal? _freeCharge;

	private int? _ShiftID;

	private DateTime? _FreeTime;

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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int TenatID
	{
		get
		{
			return _TenatID;
		}
		set
		{
			if (_TenatID != value)
			{
				ReportPropertyChanging("TenatID");
				_TenatID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TenatID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public string TenatNo
	{
		get
		{
			return _TenatNo;
		}
		set
		{
			if (_TenatNo != value)
			{
				ReportPropertyChanging("TenatNo");
				_TenatNo = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("TenatNo");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public string TenatNameCn
	{
		get
		{
			return _TenatNameCn;
		}
		set
		{
			if (_TenatNameCn != value)
			{
				ReportPropertyChanging("TenatNameCn");
				_TenatNameCn = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("TenatNameCn");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public string TenatNamePt
	{
		get
		{
			return _TenatNamePt;
		}
		set
		{
			if (_TenatNamePt != value)
			{
				ReportPropertyChanging("TenatNamePt");
				_TenatNamePt = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("TenatNamePt");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public long? freeCount
	{
		get
		{
			return _freeCount;
		}
		set
		{
			ReportPropertyChanging("freeCount");
			_freeCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("freeCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? freeMinutes
	{
		get
		{
			return _freeMinutes;
		}
		set
		{
			ReportPropertyChanging("freeMinutes");
			_freeMinutes = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("freeMinutes");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? freeCharge
	{
		get
		{
			return _freeCharge;
		}
		set
		{
			ReportPropertyChanging("freeCharge");
			_freeCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("freeCharge");
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
	public DateTime? FreeTime
	{
		get
		{
			return _FreeTime;
		}
		set
		{
			ReportPropertyChanging("FreeTime");
			_FreeTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FreeTime");
		}
	}

	public static view_FreeRecordTotal Createview_FreeRecordTotal(int tenatID, string tenatNo, string tenatNameCn, string tenatNamePt)
	{
		view_FreeRecordTotal view_FreeRecordTotal2 = new view_FreeRecordTotal();
		view_FreeRecordTotal2.TenatID = tenatID;
		view_FreeRecordTotal2.TenatNo = tenatNo;
		view_FreeRecordTotal2.TenatNameCn = tenatNameCn;
		view_FreeRecordTotal2.TenatNamePt = tenatNamePt;
		return view_FreeRecordTotal2;
	}
}
