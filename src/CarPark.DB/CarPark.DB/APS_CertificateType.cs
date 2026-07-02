using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_CertificateType")]
[DataContract(IsReference = true)]
public class APS_CertificateType : EntityObject
{
	private int _CertificateTypeID;

	private string _CertificateNameCn;

	private string _CertificateNamePt;

	private int _Period;

	private TimeSpan _ServiceTimeStart;

	private TimeSpan _ServiceTimeEnd;

	private bool _Enable;

	private bool _IsCancel;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int CertificateTypeID
	{
		get
		{
			return _CertificateTypeID;
		}
		set
		{
			if (_CertificateTypeID != value)
			{
				ReportPropertyChanging("CertificateTypeID");
				_CertificateTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CertificateTypeID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CertificateNameCn
	{
		get
		{
			return _CertificateNameCn;
		}
		set
		{
			ReportPropertyChanging("CertificateNameCn");
			_CertificateNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CertificateNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CertificateNamePt
	{
		get
		{
			return _CertificateNamePt;
		}
		set
		{
			ReportPropertyChanging("CertificateNamePt");
			_CertificateNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CertificateNamePt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int Period
	{
		get
		{
			return _Period;
		}
		set
		{
			ReportPropertyChanging("Period");
			_Period = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Period");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool Enable
	{
		get
		{
			return _Enable;
		}
		set
		{
			ReportPropertyChanging("Enable");
			_Enable = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Enable");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	public static APS_CertificateType CreateAPS_CertificateType(int certificateTypeID, string certificateNameCn, string certificateNamePt, int period, TimeSpan serviceTimeStart, TimeSpan serviceTimeEnd, bool enable, bool isCancel)
	{
		APS_CertificateType aPS_CertificateType = new APS_CertificateType();
		aPS_CertificateType.CertificateTypeID = certificateTypeID;
		aPS_CertificateType.CertificateNameCn = certificateNameCn;
		aPS_CertificateType.CertificateNamePt = certificateNamePt;
		aPS_CertificateType.Period = period;
		aPS_CertificateType.ServiceTimeStart = serviceTimeStart;
		aPS_CertificateType.ServiceTimeEnd = serviceTimeEnd;
		aPS_CertificateType.Enable = enable;
		aPS_CertificateType.IsCancel = isCancel;
		return aPS_CertificateType;
	}
}
