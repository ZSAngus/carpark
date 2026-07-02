using System;
using System.Data.Objects.DataClasses;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "StaffInfo")]
public class StaffInfo : EntityObject
{
	private int _StaffId;

	private string _StaffCode;

	private string _StaffName;

	private int _StaffTypeId;

	private string _PhoneNumber;

	private string _EmailAddress;

	private string _Address;

	private string _IDNo;

	private string _SmartCardCode;

	private DateTime _CreateTime;

	private string _CreateStaffCode;

	private bool _AllowUse;

	private byte[] _StaffImage;

	private string _StaffPwd;

	private bool _IsDelete;

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Image)]
	public Image BindStaffImage
	{
		get
		{
			Image result = null;
			if (StaffImage != null)
			{
				MemoryStream memoryStream = new MemoryStream(StaffImage);
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
				StaffImage = null;
				return;
			}
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				value.Save(memoryStream, ImageFormat.Jpeg);
				StaffImage = memoryStream.ToArray();
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int StaffId
	{
		get
		{
			return _StaffId;
		}
		set
		{
			if (_StaffId != value)
			{
				ReportPropertyChanging("StaffId");
				_StaffId = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("StaffId");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string StaffCode
	{
		get
		{
			return _StaffCode;
		}
		set
		{
			ReportPropertyChanging("StaffCode");
			_StaffCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("StaffCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string StaffName
	{
		get
		{
			return _StaffName;
		}
		set
		{
			ReportPropertyChanging("StaffName");
			_StaffName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("StaffName");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int StaffTypeId
	{
		get
		{
			return _StaffTypeId;
		}
		set
		{
			ReportPropertyChanging("StaffTypeId");
			_StaffTypeId = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StaffTypeId");
		}
	}

	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(DBStaffTypeSource))]
	public int BindStaffTypeID
	{
		get
		{
			return StaffTypeId;
		}
		set
		{
			StaffTypeId = value;
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
	public string Address
	{
		get
		{
			return _Address;
		}
		set
		{
			ReportPropertyChanging("Address");
			_Address = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Address");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string IDNo
	{
		get
		{
			return _IDNo;
		}
		set
		{
			ReportPropertyChanging("IDNo");
			_IDNo = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("IDNo");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool AllowUse
	{
		get
		{
			return _AllowUse;
		}
		set
		{
			ReportPropertyChanging("AllowUse");
			_AllowUse = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AllowUse");
		}
	}

	[BindingControlEditStyle(EditStyle = EnumEditStyle.CheckBox)]
	public bool BindAllowUse
	{
		get
		{
			return AllowUse;
		}
		set
		{
			AllowUse = value;
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public byte[] StaffImage
	{
		get
		{
			return StructuralObject.GetValidValue(_StaffImage);
		}
		set
		{
			ReportPropertyChanging("StaffImage");
			_StaffImage = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("StaffImage");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string StaffPwd
	{
		get
		{
			return _StaffPwd;
		}
		set
		{
			ReportPropertyChanging("StaffPwd");
			_StaffPwd = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("StaffPwd");
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

	public static StaffInfo CreateStaffInfo(int staffId, string staffCode, int staffTypeId, DateTime createTime, string createStaffCode, bool allowUse, string staffPwd, bool isDelete)
	{
		StaffInfo staffInfo = new StaffInfo();
		staffInfo.StaffId = staffId;
		staffInfo.StaffCode = staffCode;
		staffInfo.StaffTypeId = staffTypeId;
		staffInfo.CreateTime = createTime;
		staffInfo.CreateStaffCode = createStaffCode;
		staffInfo.AllowUse = allowUse;
		staffInfo.StaffPwd = staffPwd;
		staffInfo.IsDelete = isDelete;
		return staffInfo;
	}
}
