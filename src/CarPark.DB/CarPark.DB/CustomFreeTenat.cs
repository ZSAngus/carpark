using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "CustomFreeTenat")]
public class CustomFreeTenat : EntityObject
{
	private int _TenatID;

	private string _TenatNameCn;

	private string _TenatNamePt;

	private int _AllowedFreeType;

	private string _TenatNo;

	private string _FreeType;

	private int? _InteriorType;

	private int? _TenatTypeID;

	private int? _Status;

	private bool _IsDelete;

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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
			_TenatNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TenatNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
			_TenatNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TenatNamePt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int AllowedFreeType
	{
		get
		{
			return _AllowedFreeType;
		}
		set
		{
			ReportPropertyChanging("AllowedFreeType");
			_AllowedFreeType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AllowedFreeType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
	public string FreeType
	{
		get
		{
			return _FreeType;
		}
		set
		{
			ReportPropertyChanging("FreeType");
			_FreeType = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("FreeType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? InteriorType
	{
		get
		{
			return _InteriorType;
		}
		set
		{
			ReportPropertyChanging("InteriorType");
			_InteriorType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("InteriorType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? TenatTypeID
	{
		get
		{
			return _TenatTypeID;
		}
		set
		{
			ReportPropertyChanging("TenatTypeID");
			_TenatTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TenatTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? Status
	{
		get
		{
			return _Status;
		}
		set
		{
			ReportPropertyChanging("Status");
			_Status = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Status");
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

	public static CustomFreeTenat CreateCustomFreeTenat(int tenatID, string tenatNameCn, string tenatNamePt, int allowedFreeType, string tenatNo, bool isDelete)
	{
		CustomFreeTenat customFreeTenat = new CustomFreeTenat();
		customFreeTenat.TenatID = tenatID;
		customFreeTenat.TenatNameCn = tenatNameCn;
		customFreeTenat.TenatNamePt = tenatNamePt;
		customFreeTenat.AllowedFreeType = allowedFreeType;
		customFreeTenat.TenatNo = tenatNo;
		customFreeTenat.IsDelete = isDelete;
		return customFreeTenat;
	}

	public bool AllowFreeType(CustomFreeType item)
	{
		bool result = false;
		if (item != null && ((1 << item.CustomFreeTypeID) & AllowedFreeType) == 1 << item.CustomFreeTypeID)
		{
			result = true;
		}
		return result;
	}
}
