using System;
using System.Data.Objects.DataClasses;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "Customer")]
public class Customer : EntityObject
{
	private string _DrivingLicense;

	private string _CustomerNamePt;

	private int? _CustomerType;

	private string _Department;

	private DateTime? _ContractExpiredDate;

	private string _UniversityDepartment;

	private string _UniversityStaffName;

	private string _Remark;

	private int? _BuckleSalary;

	private DateTime? _StartDate;

	private DateTime? _EndDate;

	private int _CustomerID;

	private string _CustomerCode;

	private string _CustomerName;

	private string _PhoneNumber;

	private string _EmailAddress;

	private DateTime? _BirthDate;

	private byte[] _PicImage;

	private string _SmartCardCode;

	private string _CreatorStaff;

	private DateTime _CreateTime;

	private bool _IsDelete;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string DrivingLicense
	{
		get
		{
			return _DrivingLicense;
		}
		set
		{
			ReportPropertyChanging("DrivingLicense");
			_DrivingLicense = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("DrivingLicense");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CustomerNamePt
	{
		get
		{
			return _CustomerNamePt;
		}
		set
		{
			ReportPropertyChanging("CustomerNamePt");
			_CustomerNamePt = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CustomerNamePt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? CustomerType
	{
		get
		{
			return _CustomerType;
		}
		set
		{
			ReportPropertyChanging("CustomerType");
			_CustomerType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CustomerType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Department
	{
		get
		{
			return _Department;
		}
		set
		{
			ReportPropertyChanging("Department");
			_Department = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Department");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? ContractExpiredDate
	{
		get
		{
			return _ContractExpiredDate;
		}
		set
		{
			ReportPropertyChanging("ContractExpiredDate");
			_ContractExpiredDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ContractExpiredDate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string UniversityDepartment
	{
		get
		{
			return _UniversityDepartment;
		}
		set
		{
			ReportPropertyChanging("UniversityDepartment");
			_UniversityDepartment = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("UniversityDepartment");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string UniversityStaffName
	{
		get
		{
			return _UniversityStaffName;
		}
		set
		{
			ReportPropertyChanging("UniversityStaffName");
			_UniversityStaffName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("UniversityStaffName");
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
	public int? BuckleSalary
	{
		get
		{
			return _BuckleSalary;
		}
		set
		{
			ReportPropertyChanging("BuckleSalary");
			_BuckleSalary = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BuckleSalary");
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
	public DateTime? EndDate
	{
		get
		{
			return _EndDate;
		}
		set
		{
			ReportPropertyChanging("EndDate");
			_EndDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndDate");
		}
	}

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Image)]
	public Image CustomerImage
	{
		get
		{
			Image result = null;
			if (PicImage != null)
			{
				MemoryStream memoryStream = new MemoryStream(PicImage);
				try
				{
					result = Image.FromStream(memoryStream);
				}
				catch
				{
				}
				finally
				{
					memoryStream?.Dispose();
				}
			}
			return result;
		}
		set
		{
			if (value == null)
			{
				PicImage = null;
				return;
			}
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				value.Save(memoryStream, ImageFormat.Jpeg);
				PicImage = memoryStream.ToArray();
			}
			catch (Exception)
			{
			}
			finally
			{
				memoryStream?.Dispose();
			}
		}
	}

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Date)]
	public DateTime? BindBirthDate
	{
		get
		{
			return BirthDate;
		}
		set
		{
			BirthDate = value;
		}
	}

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Date)]
	public DateTime BindCreateTime
	{
		get
		{
			return CreateTime;
		}
		set
		{
			CreateTime = value;
		}
	}

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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CustomerCode
	{
		get
		{
			return _CustomerCode;
		}
		set
		{
			ReportPropertyChanging("CustomerCode");
			_CustomerCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CustomerCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CustomerName
	{
		get
		{
			return _CustomerName;
		}
		set
		{
			ReportPropertyChanging("CustomerName");
			_CustomerName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CustomerName");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? BirthDate
	{
		get
		{
			return _BirthDate;
		}
		set
		{
			ReportPropertyChanging("BirthDate");
			_BirthDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BirthDate");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public byte[] PicImage
	{
		get
		{
			return StructuralObject.GetValidValue(_PicImage);
		}
		set
		{
			ReportPropertyChanging("PicImage");
			_PicImage = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("PicImage");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CreatorStaff
	{
		get
		{
			return _CreatorStaff;
		}
		set
		{
			ReportPropertyChanging("CreatorStaff");
			_CreatorStaff = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CreatorStaff");
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

	public static Customer CreateCustomer(int customerID, string customerCode, string customerName, string creatorStaff, DateTime createTime, bool isDelete)
	{
		Customer customer = new Customer();
		customer.CustomerID = customerID;
		customer.CustomerCode = customerCode;
		customer.CustomerName = customerName;
		customer.CreatorStaff = creatorStaff;
		customer.CreateTime = createTime;
		customer.IsDelete = isDelete;
		return customer;
	}
}
