using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "CompanyInfo")]
public class CompanyInfo : EntityObject
{
	private int _CompanyID;

	private string _CompanyNameCn;

	private string _CompanyNamePt;

	private string _PhoneNumber;

	private string _EmailAddress;

	private string _Address;

	private string _WebSite;

	private string _TaxNo;

	private int _DefaultFreeMin;

	private int _AllowedCardType;

	private int _AllowedParkType;

	private int _ExitTimeOutMin;

	private int? _DefaultBufferTime;

	private bool _LoopDetector;

	private bool _AntiPass;

	private bool _IsDelete;

	public int BufferTime
	{
		get
		{
			if (DefaultBufferTime.HasValue)
			{
				return DefaultBufferTime.Value;
			}
			return 0;
		}
	}

	public string CompanyName
	{
		get
		{
			string companyNameCn = CompanyNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return companyNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return CompanyNamePt;
			default:
				return companyNameCn;
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int CompanyID
	{
		get
		{
			return _CompanyID;
		}
		set
		{
			if (_CompanyID != value)
			{
				ReportPropertyChanging("CompanyID");
				_CompanyID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CompanyID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CompanyNameCn
	{
		get
		{
			return _CompanyNameCn;
		}
		set
		{
			ReportPropertyChanging("CompanyNameCn");
			_CompanyNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CompanyNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CompanyNamePt
	{
		get
		{
			return _CompanyNamePt;
		}
		set
		{
			ReportPropertyChanging("CompanyNamePt");
			_CompanyNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CompanyNamePt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
			_PhoneNumber = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("PhoneNumber");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string EmailAddress
	{
		get
		{
			return _EmailAddress;
		}
		set
		{
			ReportPropertyChanging("EmailAddress");
			_EmailAddress = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("EmailAddress");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string Address
	{
		get
		{
			return _Address;
		}
		set
		{
			ReportPropertyChanging("Address");
			_Address = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("Address");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string WebSite
	{
		get
		{
			return _WebSite;
		}
		set
		{
			ReportPropertyChanging("WebSite");
			_WebSite = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("WebSite");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string TaxNo
	{
		get
		{
			return _TaxNo;
		}
		set
		{
			ReportPropertyChanging("TaxNo");
			_TaxNo = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TaxNo");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int DefaultFreeMin
	{
		get
		{
			return _DefaultFreeMin;
		}
		set
		{
			ReportPropertyChanging("DefaultFreeMin");
			_DefaultFreeMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DefaultFreeMin");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int AllowedCardType
	{
		get
		{
			return _AllowedCardType;
		}
		set
		{
			ReportPropertyChanging("AllowedCardType");
			_AllowedCardType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AllowedCardType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int AllowedParkType
	{
		get
		{
			return _AllowedParkType;
		}
		set
		{
			ReportPropertyChanging("AllowedParkType");
			_AllowedParkType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AllowedParkType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ExitTimeOutMin
	{
		get
		{
			return _ExitTimeOutMin;
		}
		set
		{
			ReportPropertyChanging("ExitTimeOutMin");
			_ExitTimeOutMin = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ExitTimeOutMin");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int? DefaultBufferTime
	{
		get
		{
			return _DefaultBufferTime;
		}
		set
		{
			ReportPropertyChanging("DefaultBufferTime");
			_DefaultBufferTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DefaultBufferTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool LoopDetector
	{
		get
		{
			return _LoopDetector;
		}
		set
		{
			ReportPropertyChanging("LoopDetector");
			_LoopDetector = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LoopDetector");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool AntiPass
	{
		get
		{
			return _AntiPass;
		}
		set
		{
			ReportPropertyChanging("AntiPass");
			_AntiPass = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AntiPass");
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

	public static CompanyInfo CreateCompanyInfo(int companyID, string companyNameCn, string companyNamePt, string phoneNumber, string emailAddress, string address, string webSite, string taxNo, int defaultFreeMin, int allowedCardType, int allowedParkType, int exitTimeOutMin, int defaultBufferTime, bool loopDetector, bool antiPass, bool isDelete)
	{
		CompanyInfo companyInfo = new CompanyInfo();
		companyInfo.CompanyID = companyID;
		companyInfo.CompanyNameCn = companyNameCn;
		companyInfo.CompanyNamePt = companyNamePt;
		companyInfo.PhoneNumber = phoneNumber;
		companyInfo.EmailAddress = emailAddress;
		companyInfo.Address = address;
		companyInfo.WebSite = webSite;
		companyInfo.TaxNo = taxNo;
		companyInfo.DefaultFreeMin = defaultFreeMin;
		companyInfo.AllowedCardType = allowedCardType;
		companyInfo.AllowedParkType = allowedParkType;
		companyInfo.ExitTimeOutMin = exitTimeOutMin;
		companyInfo.DefaultBufferTime = defaultBufferTime;
		companyInfo.LoopDetector = loopDetector;
		companyInfo.AntiPass = antiPass;
		companyInfo.IsDelete = isDelete;
		return companyInfo;
	}
}
