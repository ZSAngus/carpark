using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "SalaryBlock")]
[DataContract(IsReference = true)]
public class SalaryBlock : EntityObject
{
	private int _ID;

	private int? _CustomerID;

	private DateTime? _StartDate;

	private DateTime? _Endate;

	private int? _OptType;

	private decimal? _Amount;

	private DateTime? _ChangeDate;

	private int? _ChangeType;

	private string _Remark;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int ID
	{
		get
		{
			return _ID;
		}
		set
		{
			if (_ID != value)
			{
				ReportPropertyChanging("ID");
				_ID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? CustomerID
	{
		get
		{
			return _CustomerID;
		}
		set
		{
			ReportPropertyChanging("CustomerID");
			_CustomerID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CustomerID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? Endate
	{
		get
		{
			return _Endate;
		}
		set
		{
			ReportPropertyChanging("Endate");
			_Endate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Endate");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? OptType
	{
		get
		{
			return _OptType;
		}
		set
		{
			ReportPropertyChanging("OptType");
			_OptType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OptType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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
	public DateTime? ChangeDate
	{
		get
		{
			return _ChangeDate;
		}
		set
		{
			ReportPropertyChanging("ChangeDate");
			_ChangeDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChangeDate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? ChangeType
	{
		get
		{
			return _ChangeType;
		}
		set
		{
			ReportPropertyChanging("ChangeType");
			_ChangeType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChangeType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
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

	public static SalaryBlock CreateSalaryBlock(int id)
	{
		SalaryBlock salaryBlock = new SalaryBlock();
		salaryBlock.ID = id;
		return salaryBlock;
	}
}
