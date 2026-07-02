using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "LicensePlate_PassTrace")]
public class LicensePlate_PassTrace : EntityObject
{
	private string _UpdateLPRemark;

	private int _LicensePlateID;

	private string _ImagePath;

	private int _PassTraceID;

	private string _AnalysisResult;

	private DateTime _CreateTime;

	private string _StaffCode;

	private int _PassReturn;

	private double _Similarity;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string UpdateLPRemark
	{
		get
		{
			return _UpdateLPRemark;
		}
		set
		{
			ReportPropertyChanging("UpdateLPRemark");
			_UpdateLPRemark = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("UpdateLPRemark");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int LicensePlateID
	{
		get
		{
			return _LicensePlateID;
		}
		set
		{
			if (_LicensePlateID != value)
			{
				ReportPropertyChanging("LicensePlateID");
				_LicensePlateID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("LicensePlateID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string ImagePath
	{
		get
		{
			return _ImagePath;
		}
		set
		{
			ReportPropertyChanging("ImagePath");
			_ImagePath = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("ImagePath");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int PassTraceID
	{
		get
		{
			return _PassTraceID;
		}
		set
		{
			ReportPropertyChanging("PassTraceID");
			_PassTraceID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PassTraceID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string AnalysisResult
	{
		get
		{
			return _AnalysisResult;
		}
		set
		{
			ReportPropertyChanging("AnalysisResult");
			_AnalysisResult = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("AnalysisResult");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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
			_StaffCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("StaffCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int PassReturn
	{
		get
		{
			return _PassReturn;
		}
		set
		{
			ReportPropertyChanging("PassReturn");
			_PassReturn = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PassReturn");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public double Similarity
	{
		get
		{
			return _Similarity;
		}
		set
		{
			ReportPropertyChanging("Similarity");
			_Similarity = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Similarity");
		}
	}

	public static LicensePlate_PassTrace CreateLicensePlate_PassTrace(int licensePlateID, string imagePath, int passTraceID, string analysisResult, DateTime createTime, int passReturn, double similarity)
	{
		LicensePlate_PassTrace licensePlate_PassTrace = new LicensePlate_PassTrace();
		licensePlate_PassTrace.LicensePlateID = licensePlateID;
		licensePlate_PassTrace.ImagePath = imagePath;
		licensePlate_PassTrace.PassTraceID = passTraceID;
		licensePlate_PassTrace.AnalysisResult = analysisResult;
		licensePlate_PassTrace.CreateTime = createTime;
		licensePlate_PassTrace.PassReturn = passReturn;
		licensePlate_PassTrace.Similarity = similarity;
		return licensePlate_PassTrace;
	}
}
