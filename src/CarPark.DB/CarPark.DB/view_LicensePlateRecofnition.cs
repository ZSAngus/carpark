using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_LicensePlateRecofnition")]
public class view_LicensePlateRecofnition : EntityObject
{
	private int _TransactionID;

	private string _InCardCode;

	private string _icensePlate;

	private DateTime? _InTime;

	private DateTime? _OutTime;

	private string _InImagePath;

	private string _OutImagePath;

	private string _InAnalysisResult;

	private string _OutAnalysisResult;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int TransactionID
	{
		get
		{
			return _TransactionID;
		}
		set
		{
			if (_TransactionID != value)
			{
				ReportPropertyChanging("TransactionID");
				_TransactionID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TransactionID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public string InCardCode
	{
		get
		{
			return _InCardCode;
		}
		set
		{
			if (_InCardCode != value)
			{
				ReportPropertyChanging("InCardCode");
				_InCardCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("InCardCode");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string icensePlate
	{
		get
		{
			return _icensePlate;
		}
		set
		{
			ReportPropertyChanging("icensePlate");
			_icensePlate = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("icensePlate");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? InTime
	{
		get
		{
			return _InTime;
		}
		set
		{
			ReportPropertyChanging("InTime");
			_InTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("InTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? OutTime
	{
		get
		{
			return _OutTime;
		}
		set
		{
			ReportPropertyChanging("OutTime");
			_OutTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OutTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string InImagePath
	{
		get
		{
			return _InImagePath;
		}
		set
		{
			ReportPropertyChanging("InImagePath");
			_InImagePath = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("InImagePath");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string OutImagePath
	{
		get
		{
			return _OutImagePath;
		}
		set
		{
			ReportPropertyChanging("OutImagePath");
			_OutImagePath = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("OutImagePath");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string InAnalysisResult
	{
		get
		{
			return _InAnalysisResult;
		}
		set
		{
			ReportPropertyChanging("InAnalysisResult");
			_InAnalysisResult = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("InAnalysisResult");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string OutAnalysisResult
	{
		get
		{
			return _OutAnalysisResult;
		}
		set
		{
			ReportPropertyChanging("OutAnalysisResult");
			_OutAnalysisResult = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("OutAnalysisResult");
		}
	}

	public static view_LicensePlateRecofnition Createview_LicensePlateRecofnition(int transactionID, string inCardCode)
	{
		view_LicensePlateRecofnition view_LicensePlateRecofnition2 = new view_LicensePlateRecofnition();
		view_LicensePlateRecofnition2.TransactionID = transactionID;
		view_LicensePlateRecofnition2.InCardCode = inCardCode;
		return view_LicensePlateRecofnition2;
	}
}
