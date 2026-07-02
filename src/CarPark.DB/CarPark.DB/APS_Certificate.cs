using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_Certificate")]
[DataContract(IsReference = true)]
public class APS_Certificate : EntityObject
{
	private int _CertificateID;

	private string _CertificateCode;

	private int _CertificateType;

	private string _UsageMode;

	private int _StaffType;

	private TimeSpan _ServiceTimeStart;

	private TimeSpan _ServiceTimeEnd;

	private DateTime _CreateTime;

	private string _CreateStaffCode;

	private string _Barcode;

	private DateTime _ExpireDate;

	private DateTime? _LastUpdateTime;

	private DateTime? _LastUsedTime;

	private DateTime? _LastPrintTime;

	private int _PrintCount;

	private bool _IsCancel;

	private DateTime? _CancelTime;

	private string _CancelStaffName;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int CertificateID
	{
		get
		{
			return _CertificateID;
		}
		set
		{
			if (_CertificateID != value)
			{
				ReportPropertyChanging("CertificateID");
				_CertificateID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CertificateID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CertificateCode
	{
		get
		{
			return _CertificateCode;
		}
		set
		{
			ReportPropertyChanging("CertificateCode");
			_CertificateCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CertificateCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int CertificateType
	{
		get
		{
			return _CertificateType;
		}
		set
		{
			ReportPropertyChanging("CertificateType");
			_CertificateType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CertificateType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string UsageMode
	{
		get
		{
			return _UsageMode;
		}
		set
		{
			ReportPropertyChanging("UsageMode");
			_UsageMode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("UsageMode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int StaffType
	{
		get
		{
			return _StaffType;
		}
		set
		{
			ReportPropertyChanging("StaffType");
			_StaffType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StaffType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public TimeSpan ServiceTimeStart
	{
		get
		{
			return _ServiceTimeStart;
		}
		set
		{
			ReportPropertyChanging("ServiceTimeStart");
			_ServiceTimeStart = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ServiceTimeStart");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public TimeSpan ServiceTimeEnd
	{
		get
		{
			return _ServiceTimeEnd;
		}
		set
		{
			ReportPropertyChanging("ServiceTimeEnd");
			_ServiceTimeEnd = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ServiceTimeEnd");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string Barcode
	{
		get
		{
			return _Barcode;
		}
		set
		{
			ReportPropertyChanging("Barcode");
			_Barcode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("Barcode");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? LastUpdateTime
	{
		get
		{
			return _LastUpdateTime;
		}
		set
		{
			ReportPropertyChanging("LastUpdateTime");
			_LastUpdateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LastUpdateTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? LastUsedTime
	{
		get
		{
			return _LastUsedTime;
		}
		set
		{
			ReportPropertyChanging("LastUsedTime");
			_LastUsedTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LastUsedTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? LastPrintTime
	{
		get
		{
			return _LastPrintTime;
		}
		set
		{
			ReportPropertyChanging("LastPrintTime");
			_LastPrintTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LastPrintTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int PrintCount
	{
		get
		{
			return _PrintCount;
		}
		set
		{
			ReportPropertyChanging("PrintCount");
			_PrintCount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PrintCount");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsCancel
	{
		get
		{
			return _IsCancel;
		}
		set
		{
			ReportPropertyChanging("IsCancel");
			_IsCancel = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsCancel");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? CancelTime
	{
		get
		{
			return _CancelTime;
		}
		set
		{
			ReportPropertyChanging("CancelTime");
			_CancelTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CancelTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CancelStaffName
	{
		get
		{
			return _CancelStaffName;
		}
		set
		{
			ReportPropertyChanging("CancelStaffName");
			_CancelStaffName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CancelStaffName");
		}
	}

	public static APS_Certificate CreateAPS_Certificate(int certificateID, string certificateCode, int certificateType, int staffType, TimeSpan serviceTimeStart, TimeSpan serviceTimeEnd, DateTime createTime, string createStaffCode, string barcode, DateTime expireDate, int printCount, bool isCancel)
	{
		APS_Certificate aPS_Certificate = new APS_Certificate();
		aPS_Certificate.CertificateID = certificateID;
		aPS_Certificate.CertificateCode = certificateCode;
		aPS_Certificate.CertificateType = certificateType;
		aPS_Certificate.StaffType = staffType;
		aPS_Certificate.ServiceTimeStart = serviceTimeStart;
		aPS_Certificate.ServiceTimeEnd = serviceTimeEnd;
		aPS_Certificate.CreateTime = createTime;
		aPS_Certificate.CreateStaffCode = createStaffCode;
		aPS_Certificate.Barcode = barcode;
		aPS_Certificate.ExpireDate = expireDate;
		aPS_Certificate.PrintCount = printCount;
		aPS_Certificate.IsCancel = isCancel;
		return aPS_Certificate;
	}
}
