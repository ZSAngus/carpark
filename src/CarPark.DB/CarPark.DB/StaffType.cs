using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "StaffType")]
[DataContract(IsReference = true)]
public class StaffType : EntityObject
{
	private int? _SystemCode;

	private int _StaffTypeID;

	private string _StaffTypeNameCn;

	private string _StaffTypeNamePt;

	private bool _IsDelete;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? SystemCode
	{
		get
		{
			return _SystemCode;
		}
		set
		{
			ReportPropertyChanging("SystemCode");
			_SystemCode = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("SystemCode");
		}
	}

	public string StaffTypeName
	{
		get
		{
			string staffTypeNameCn = StaffTypeNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return staffTypeNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return StaffTypeNamePt;
			default:
				return staffTypeNameCn;
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int StaffTypeID
	{
		get
		{
			return _StaffTypeID;
		}
		set
		{
			if (_StaffTypeID != value)
			{
				ReportPropertyChanging("StaffTypeID");
				_StaffTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("StaffTypeID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string StaffTypeNameCn
	{
		get
		{
			return _StaffTypeNameCn;
		}
		set
		{
			ReportPropertyChanging("StaffTypeNameCn");
			_StaffTypeNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("StaffTypeNameCn");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string StaffTypeNamePt
	{
		get
		{
			return _StaffTypeNamePt;
		}
		set
		{
			ReportPropertyChanging("StaffTypeNamePt");
			_StaffTypeNamePt = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("StaffTypeNamePt");
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

	public static StaffType CreateStaffType(int staffTypeID, string staffTypeNameCn, string staffTypeNamePt, bool isDelete)
	{
		StaffType staffType = new StaffType();
		staffType.StaffTypeID = staffTypeID;
		staffType.StaffTypeNameCn = staffTypeNameCn;
		staffType.StaffTypeNamePt = staffTypeNamePt;
		staffType.IsDelete = isDelete;
		return staffType;
	}
}
