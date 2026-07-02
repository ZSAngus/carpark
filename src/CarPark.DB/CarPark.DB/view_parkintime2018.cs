using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_parkintime2018")]
[DataContract(IsReference = true)]
public class view_parkintime2018 : EntityObject
{
	private int _ParkID;

	private string _ParkCode;

	private int _ParkLimit;

	private DateTimeOffset _CreateDate;

	private string _CreateStaffCode;

	private decimal? _inUserd;

	private string _CardCode;

	private string _InCardCode;

	private string _LicensePlate;

	private sbyte _IsDelete;

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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public string ParkCode
	{
		get
		{
			return _ParkCode;
		}
		set
		{
			if (_ParkCode != value)
			{
				ReportPropertyChanging("ParkCode");
				_ParkCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("ParkCode");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int ParkLimit
	{
		get
		{
			return _ParkLimit;
		}
		set
		{
			if (_ParkLimit != value)
			{
				ReportPropertyChanging("ParkLimit");
				_ParkLimit = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ParkLimit");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public DateTimeOffset CreateDate
	{
		get
		{
			return _CreateDate;
		}
		set
		{
			if (_CreateDate != value)
			{
				ReportPropertyChanging("CreateDate");
				_CreateDate = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CreateDate");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public string CreateStaffCode
	{
		get
		{
			return _CreateStaffCode;
		}
		set
		{
			if (_CreateStaffCode != value)
			{
				ReportPropertyChanging("CreateStaffCode");
				_CreateStaffCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("CreateStaffCode");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? inUserd
	{
		get
		{
			return _inUserd;
		}
		set
		{
			ReportPropertyChanging("inUserd");
			_inUserd = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("inUserd");
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
	public string InCardCode
	{
		get
		{
			return _InCardCode;
		}
		set
		{
			ReportPropertyChanging("InCardCode");
			_InCardCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("InCardCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string LicensePlate
	{
		get
		{
			return _LicensePlate;
		}
		set
		{
			ReportPropertyChanging("LicensePlate");
			_LicensePlate = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LicensePlate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public sbyte IsDelete
	{
		get
		{
			return _IsDelete;
		}
		set
		{
			if (_IsDelete != value)
			{
				ReportPropertyChanging("IsDelete");
				_IsDelete = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("IsDelete");
			}
		}
	}

	public static view_parkintime2018 Createview_parkintime2018(int parkID, string parkCode, int parkLimit, DateTimeOffset createDate, string createStaffCode, sbyte isDelete)
	{
		view_parkintime2018 view_parkintime2019 = new view_parkintime2018();
		view_parkintime2019.ParkID = parkID;
		view_parkintime2019.ParkCode = parkCode;
		view_parkintime2019.ParkLimit = parkLimit;
		view_parkintime2019.CreateDate = createDate;
		view_parkintime2019.CreateStaffCode = createStaffCode;
		view_parkintime2019.IsDelete = isDelete;
		return view_parkintime2019;
	}
}
