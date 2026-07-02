using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "SysRole")]
[DataContract(IsReference = true)]
public class SysRole : EntityObject
{
	private int _RoleID;

	private string _RolNameCn;

	private string _RoleNamePt;

	private string _Remark;

	private int? _ParentRoleID;

	private bool _InUse;

	private string _RoleClass;

	private string _FunctionName;

	private string _FunctionUrl;

	public string RoleName
	{
		get
		{
			string rolNameCn = RolNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return rolNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return RoleNamePt;
			default:
				return rolNameCn;
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int RoleID
	{
		get
		{
			return _RoleID;
		}
		set
		{
			if (_RoleID != value)
			{
				ReportPropertyChanging("RoleID");
				_RoleID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("RoleID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string RolNameCn
	{
		get
		{
			return _RolNameCn;
		}
		set
		{
			ReportPropertyChanging("RolNameCn");
			_RolNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("RolNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string RoleNamePt
	{
		get
		{
			return _RoleNamePt;
		}
		set
		{
			ReportPropertyChanging("RoleNamePt");
			_RoleNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("RoleNamePt");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? ParentRoleID
	{
		get
		{
			return _ParentRoleID;
		}
		set
		{
			ReportPropertyChanging("ParentRoleID");
			_ParentRoleID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParentRoleID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool InUse
	{
		get
		{
			return _InUse;
		}
		set
		{
			ReportPropertyChanging("InUse");
			_InUse = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("InUse");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string RoleClass
	{
		get
		{
			return _RoleClass;
		}
		set
		{
			ReportPropertyChanging("RoleClass");
			_RoleClass = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("RoleClass");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string FunctionName
	{
		get
		{
			return _FunctionName;
		}
		set
		{
			ReportPropertyChanging("FunctionName");
			_FunctionName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("FunctionName");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string FunctionUrl
	{
		get
		{
			return _FunctionUrl;
		}
		set
		{
			ReportPropertyChanging("FunctionUrl");
			_FunctionUrl = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("FunctionUrl");
		}
	}

	public static SysRole CreateSysRole(int roleID, string rolNameCn, string roleNamePt, bool inUse)
	{
		SysRole sysRole = new SysRole();
		sysRole.RoleID = roleID;
		sysRole.RolNameCn = rolNameCn;
		sysRole.RoleNamePt = roleNamePt;
		sysRole.InUse = inUse;
		return sysRole;
	}
}
