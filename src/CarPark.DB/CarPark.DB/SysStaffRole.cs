using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "SysStaffRole")]
public class SysStaffRole : EntityObject
{
	private int _StaffTypeID;

	private int _RoleID;

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

	public static SysStaffRole CreateSysStaffRole(int staffTypeID, int roleID)
	{
		SysStaffRole sysStaffRole = new SysStaffRole();
		sysStaffRole.StaffTypeID = staffTypeID;
		sysStaffRole.RoleID = roleID;
		return sysStaffRole;
	}
}
