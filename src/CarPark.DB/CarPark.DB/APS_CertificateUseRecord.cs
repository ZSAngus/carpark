using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_CertificateUseRecord")]
public class APS_CertificateUseRecord : EntityObject
{
	private int _CertificateUseID;

	private int? _CertificateID;

	private string _CertificateCode;

	private string _CertificateType;

	private DateTime? _StartTime;

	private DateTime? _EndTime;

	private string _StaffCode;

	private DateTime _CreateTime;

	private bool? _IsInsert;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int CertificateUseID
	{
		get
		{
			return _CertificateUseID;
		}
		set
		{
			if (_CertificateUseID != value)
			{
				ReportPropertyChanging("CertificateUseID");
				_CertificateUseID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CertificateUseID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? CertificateID
	{
		get
		{
			return _CertificateID;
		}
		set
		{
			ReportPropertyChanging("CertificateID");
			_CertificateID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CertificateID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string CertificateCode
	{
		get
		{
			return _CertificateCode;
		}
		set
		{
			ReportPropertyChanging("CertificateCode");
			_CertificateCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CertificateCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CertificateType
	{
		get
		{
			return _CertificateType;
		}
		set
		{
			ReportPropertyChanging("CertificateType");
			_CertificateType = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CertificateType");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? StartTime
	{
		get
		{
			return _StartTime;
		}
		set
		{
			ReportPropertyChanging("StartTime");
			_StartTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? EndTime
	{
		get
		{
			return _EndTime;
		}
		set
		{
			ReportPropertyChanging("EndTime");
			_EndTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string StaffCode
	{
		get
		{
			return _StaffCode;
		}
		set
		{
			ReportPropertyChanging("StaffCode");
			_StaffCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("StaffCode");
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? IsInsert
	{
		get
		{
			return _IsInsert;
		}
		set
		{
			ReportPropertyChanging("IsInsert");
			_IsInsert = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsInsert");
		}
	}

	public static APS_CertificateUseRecord CreateAPS_CertificateUseRecord(int certificateUseID, DateTime createTime)
	{
		APS_CertificateUseRecord aPS_CertificateUseRecord = new APS_CertificateUseRecord();
		aPS_CertificateUseRecord.CertificateUseID = certificateUseID;
		aPS_CertificateUseRecord.CreateTime = createTime;
		return aPS_CertificateUseRecord;
	}
}
