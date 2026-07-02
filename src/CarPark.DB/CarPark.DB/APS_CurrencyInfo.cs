using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_CurrencyInfo")]
[DataContract(IsReference = true)]
public class APS_CurrencyInfo : EntityObject
{
	private int _CurrencyInfoID;

	private string _Symbol;

	private string _NameCN;

	private string _NamePT;

	private bool _IsDeleted;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int CurrencyInfoID
	{
		get
		{
			return _CurrencyInfoID;
		}
		set
		{
			if (_CurrencyInfoID != value)
			{
				ReportPropertyChanging("CurrencyInfoID");
				_CurrencyInfoID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CurrencyInfoID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string Symbol
	{
		get
		{
			return _Symbol;
		}
		set
		{
			ReportPropertyChanging("Symbol");
			_Symbol = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("Symbol");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string NameCN
	{
		get
		{
			return _NameCN;
		}
		set
		{
			ReportPropertyChanging("NameCN");
			_NameCN = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("NameCN");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string NamePT
	{
		get
		{
			return _NamePT;
		}
		set
		{
			ReportPropertyChanging("NamePT");
			_NamePT = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("NamePT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsDeleted
	{
		get
		{
			return _IsDeleted;
		}
		set
		{
			ReportPropertyChanging("IsDeleted");
			_IsDeleted = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDeleted");
		}
	}

	public static APS_CurrencyInfo CreateAPS_CurrencyInfo(int currencyInfoID, string symbol, string nameCN, string namePT, bool isDeleted)
	{
		APS_CurrencyInfo aPS_CurrencyInfo = new APS_CurrencyInfo();
		aPS_CurrencyInfo.CurrencyInfoID = currencyInfoID;
		aPS_CurrencyInfo.Symbol = symbol;
		aPS_CurrencyInfo.NameCN = nameCN;
		aPS_CurrencyInfo.NamePT = namePT;
		aPS_CurrencyInfo.IsDeleted = isDeleted;
		return aPS_CurrencyInfo;
	}
}
