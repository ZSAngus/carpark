using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "FreeRegister")]
[DataContract(IsReference = true)]
public class FreeRegister : EntityObject
{
	private int _ID;

	private string _CarNumber;

	private DateTime _StartDate;

	private DateTime _ExpireDate;

	private DateTime _CreateTime;

	private string _CreateStaff;

	private bool _Status;

	private int _CustomFreeTypeID;

	private int _TenatID;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ID
	{
		get
		{
			return _ID;
		}
		set
		{
			if (_ID != value)
			{
				ReportPropertyChanging("ID");
				_ID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CarNumber
	{
		get
		{
			return _CarNumber;
		}
		set
		{
			ReportPropertyChanging("CarNumber");
			_CarNumber = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CarNumber");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime ExpireDate
	{
		get
		{
			return _ExpireDate;
		}
		set
		{
			ReportPropertyChanging("ExpireDate");
			_ExpireDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ExpireDate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime CreateTime
	{
		get
		{
			return _CreateTime;
		}
		set
		{
			ReportPropertyChanging("CreateTime");
			_CreateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CreateTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CreateStaff
	{
		get
		{
			return _CreateStaff;
		}
		set
		{
			ReportPropertyChanging("CreateStaff");
			_CreateStaff = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CreateStaff");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool Status
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int CustomFreeTypeID
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int TenatID
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

	public static FreeRegister CreateFreeRegister(int id, string carNumber, DateTime startDate, DateTime expireDate, DateTime createTime, string createStaff, bool status, int customFreeTypeID, int tenatID)
	{
		FreeRegister freeRegister = new FreeRegister();
		freeRegister.ID = id;
		freeRegister.CarNumber = carNumber;
		freeRegister.StartDate = startDate;
		freeRegister.ExpireDate = expireDate;
		freeRegister.CreateTime = createTime;
		freeRegister.CreateStaff = createStaff;
		freeRegister.Status = status;
		freeRegister.CustomFreeTypeID = customFreeTypeID;
		freeRegister.TenatID = tenatID;
		return freeRegister;
	}
}
