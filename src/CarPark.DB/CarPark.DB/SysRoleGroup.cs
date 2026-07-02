using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "SysRoleGroup")]
public class SysRoleGroup : EntityObject
{
	private int _SystemID;

	private string _DiscriptionCN;

	private string _DiscriptionPt;

	private string _RoleList;

	private bool _IsUseInLogo;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int SystemID
	{
		get
		{
			return _SystemID;
		}
		set
		{
			if (_SystemID != value)
			{
				ReportPropertyChanging("SystemID");
				_SystemID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("SystemID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string DiscriptionCN
	{
		get
		{
			return _DiscriptionCN;
		}
		set
		{
			ReportPropertyChanging("DiscriptionCN");
			_DiscriptionCN = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("DiscriptionCN");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string DiscriptionPt
	{
		get
		{
			return _DiscriptionPt;
		}
		set
		{
			ReportPropertyChanging("DiscriptionPt");
			_DiscriptionPt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("DiscriptionPt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string RoleList
	{
		get
		{
			return _RoleList;
		}
		set
		{
			ReportPropertyChanging("RoleList");
			_RoleList = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("RoleList");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsUseInLogo
	{
		get
		{
			return _IsUseInLogo;
		}
		set
		{
			ReportPropertyChanging("IsUseInLogo");
			_IsUseInLogo = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsUseInLogo");
		}
	}

	public static SysRoleGroup CreateSysRoleGroup(int systemID, string discriptionCN, string discriptionPt, string roleList, bool isUseInLogo)
	{
		SysRoleGroup sysRoleGroup = new SysRoleGroup();
		sysRoleGroup.SystemID = systemID;
		sysRoleGroup.DiscriptionCN = discriptionCN;
		sysRoleGroup.DiscriptionPt = discriptionPt;
		sysRoleGroup.RoleList = roleList;
		sysRoleGroup.IsUseInLogo = isUseInLogo;
		return sysRoleGroup;
	}
}
