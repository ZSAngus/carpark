using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_Customer")]
[DataContract(IsReference = true)]
public class view_Customer : EntityObject
{
	private int _CustomerID;

	private string _CustomerCode;

	private string _CustomerName;

	private string _PhoneNumber;

	private string _EmailAddress;

	private string _CardCode;

	private string _SmartCardCode;

	private string _CreatorStaff;

	private DateTime _CreateTime;

	private DateTime? _StartDate;

	private DateTime? _ExpireDate;

	private string _LicensePlate;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int CustomerID
	{
		get
		{
			return _CustomerID;
		}
		set
		{
			if (_CustomerID != value)
			{
				ReportPropertyChanging("CustomerID");
				_CustomerID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CustomerID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public string CustomerCode
	{
		get
		{
			return _CustomerCode;
		}
		set
		{
			if (_CustomerCode != value)
			{
				ReportPropertyChanging("CustomerCode");
				_CustomerCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("CustomerCode");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public string CustomerName
	{
		get
		{
			return _CustomerName;
		}
		set
		{
			if (_CustomerName != value)
			{
				ReportPropertyChanging("CustomerName");
				_CustomerName = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("CustomerName");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string PhoneNumber
	{
		get
		{
			return _PhoneNumber;
		}
		set
		{
			ReportPropertyChanging("PhoneNumber");
			_PhoneNumber = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PhoneNumber");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string EmailAddress
	{
		get
		{
			return _EmailAddress;
		}
		set
		{
			ReportPropertyChanging("EmailAddress");
			_EmailAddress = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("EmailAddress");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string SmartCardCode
	{
		get
		{
			return _SmartCardCode;
		}
		set
		{
			ReportPropertyChanging("SmartCardCode");
			_SmartCardCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("SmartCardCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public string CreatorStaff
	{
		get
		{
			return _CreatorStaff;
		}
		set
		{
			if (_CreatorStaff != value)
			{
				ReportPropertyChanging("CreatorStaff");
				_CreatorStaff = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("CreatorStaff");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public DateTime CreateTime
	{
		get
		{
			return _CreateTime;
		}
		set
		{
			if (_CreateTime != value)
			{
				ReportPropertyChanging("CreateTime");
				_CreateTime = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CreateTime");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? StartDate
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? ExpireDate
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	public static view_Customer Createview_Customer(int customerID, string customerCode, string customerName, string creatorStaff, DateTime createTime)
	{
		view_Customer view_Customer2 = new view_Customer();
		view_Customer2.CustomerID = customerID;
		view_Customer2.CustomerCode = customerCode;
		view_Customer2.CustomerName = customerName;
		view_Customer2.CreatorStaff = creatorStaff;
		view_Customer2.CreateTime = createTime;
		return view_Customer2;
	}
}
