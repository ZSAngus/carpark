using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "Park")]
[DataContract(IsReference = true)]
public class Park : EntityObject
{
	private int _ParkID;

	private string _ParkCode;

	private int _ParkLimit;

	private DateTimeOffset _CreateDate;

	private string _CreateStaffCode;

	private DateTime? _DeleteDate;

	private string _DeleteStaffCode;

	private int _Occupied;

	private int _Active;

	private bool _IsDelete;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int ParkID
	{
		get
		{
			return _ParkID;
		}
		set
		{
			if (_ParkID != value)
			{
				ReportPropertyChanging("ParkID");
				_ParkID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ParkID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string ParkCode
	{
		get
		{
			return _ParkCode;
		}
		set
		{
			ReportPropertyChanging("ParkCode");
			_ParkCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("ParkCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ParkLimit
	{
		get
		{
			return _ParkLimit;
		}
		set
		{
			ReportPropertyChanging("ParkLimit");
			_ParkLimit = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkLimit");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTimeOffset CreateDate
	{
		get
		{
			return _CreateDate;
		}
		set
		{
			ReportPropertyChanging("CreateDate");
			_CreateDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CreateDate");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CreateStaffCode
	{
		get
		{
			return _CreateStaffCode;
		}
		set
		{
			ReportPropertyChanging("CreateStaffCode");
			_CreateStaffCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CreateStaffCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? DeleteDate
	{
		get
		{
			return _DeleteDate;
		}
		set
		{
			ReportPropertyChanging("DeleteDate");
			_DeleteDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DeleteDate");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string DeleteStaffCode
	{
		get
		{
			return _DeleteStaffCode;
		}
		set
		{
			ReportPropertyChanging("DeleteStaffCode");
			_DeleteStaffCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("DeleteStaffCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int Occupied
	{
		get
		{
			return _Occupied;
		}
		set
		{
			ReportPropertyChanging("Occupied");
			_Occupied = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Occupied");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int Active
	{
		get
		{
			return _Active;
		}
		set
		{
			ReportPropertyChanging("Active");
			_Active = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Active");
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

	public static Park CreatePark(int parkID, string parkCode, int parkLimit, DateTimeOffset createDate, string createStaffCode, int occupied, int active, bool isDelete)
	{
		Park park = new Park();
		park.ParkID = parkID;
		park.ParkCode = parkCode;
		park.ParkLimit = parkLimit;
		park.CreateDate = createDate;
		park.CreateStaffCode = createStaffCode;
		park.Occupied = occupied;
		park.Active = active;
		park.IsDelete = isDelete;
		return park;
	}
}
