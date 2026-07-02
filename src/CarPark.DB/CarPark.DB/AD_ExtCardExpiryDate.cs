using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "AD_ExtCardExpiryDate")]
[DataContract(IsReference = true)]
public class AD_ExtCardExpiryDate : EntityObject
{
	private int _AutoID;

	private string _Holder_ID;

	private string _Card_no;

	private decimal? _Amount;

	private string _Pay_type;

	private string _Terminal_ID;

	private DateTime? _Txn_date;

	private string _Txn_location;

	private DateTime? _New_acctivation_date;

	private DateTime? _New_validation_date;

	private string _Add_user;

	private DateTime? _Add_date;

	private string _Error_msg;

	private bool? _ReturnValue;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int AutoID
	{
		get
		{
			return _AutoID;
		}
		set
		{
			if (_AutoID != value)
			{
				ReportPropertyChanging("AutoID");
				_AutoID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("AutoID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string Holder_ID
	{
		get
		{
			return _Holder_ID;
		}
		set
		{
			ReportPropertyChanging("Holder_ID");
			_Holder_ID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Holder_ID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string Card_no
	{
		get
		{
			return _Card_no;
		}
		set
		{
			ReportPropertyChanging("Card_no");
			_Card_no = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Card_no");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? Amount
	{
		get
		{
			return _Amount;
		}
		set
		{
			ReportPropertyChanging("Amount");
			_Amount = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Amount");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Pay_type
	{
		get
		{
			return _Pay_type;
		}
		set
		{
			ReportPropertyChanging("Pay_type");
			_Pay_type = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Pay_type");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string Terminal_ID
	{
		get
		{
			return _Terminal_ID;
		}
		set
		{
			ReportPropertyChanging("Terminal_ID");
			_Terminal_ID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Terminal_ID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? Txn_date
	{
		get
		{
			return _Txn_date;
		}
		set
		{
			ReportPropertyChanging("Txn_date");
			_Txn_date = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Txn_date");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Txn_location
	{
		get
		{
			return _Txn_location;
		}
		set
		{
			ReportPropertyChanging("Txn_location");
			_Txn_location = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Txn_location");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? New_acctivation_date
	{
		get
		{
			return _New_acctivation_date;
		}
		set
		{
			ReportPropertyChanging("New_acctivation_date");
			_New_acctivation_date = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("New_acctivation_date");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? New_validation_date
	{
		get
		{
			return _New_validation_date;
		}
		set
		{
			ReportPropertyChanging("New_validation_date");
			_New_validation_date = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("New_validation_date");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string Add_user
	{
		get
		{
			return _Add_user;
		}
		set
		{
			ReportPropertyChanging("Add_user");
			_Add_user = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Add_user");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? Add_date
	{
		get
		{
			return _Add_date;
		}
		set
		{
			ReportPropertyChanging("Add_date");
			_Add_date = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Add_date");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Error_msg
	{
		get
		{
			return _Error_msg;
		}
		set
		{
			ReportPropertyChanging("Error_msg");
			_Error_msg = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Error_msg");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public bool? ReturnValue
	{
		get
		{
			return _ReturnValue;
		}
		set
		{
			ReportPropertyChanging("ReturnValue");
			_ReturnValue = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ReturnValue");
		}
	}

	public static AD_ExtCardExpiryDate CreateAD_ExtCardExpiryDate(int autoID)
	{
		AD_ExtCardExpiryDate aD_ExtCardExpiryDate = new AD_ExtCardExpiryDate();
		aD_ExtCardExpiryDate.AutoID = autoID;
		return aD_ExtCardExpiryDate;
	}
}
