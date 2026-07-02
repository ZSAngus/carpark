using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "CloudDiscount")]
public class CloudDiscount : EntityObject
{
	private int _ID;

	private int? _CustomFreeTenatID;

	private int? _CustomFreeTypeID;

	private int? _DiscountID;

	private int? _TransactionDataID;

	private DateTime? _CreateTime;

	private int? _DiscountStatus;

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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? CustomFreeTenatID
	{
		get
		{
			return _CustomFreeTenatID;
		}
		set
		{
			ReportPropertyChanging("CustomFreeTenatID");
			_CustomFreeTenatID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CustomFreeTenatID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? CustomFreeTypeID
	{
		get
		{
			return _CustomFreeTypeID;
		}
		set
		{
			ReportPropertyChanging("CustomFreeTypeID");
			_CustomFreeTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CustomFreeTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? DiscountID
	{
		get
		{
			return _DiscountID;
		}
		set
		{
			ReportPropertyChanging("DiscountID");
			_DiscountID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DiscountID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? TransactionDataID
	{
		get
		{
			return _TransactionDataID;
		}
		set
		{
			ReportPropertyChanging("TransactionDataID");
			_TransactionDataID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TransactionDataID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? CreateTime
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
	public int? DiscountStatus
	{
		get
		{
			return _DiscountStatus;
		}
		set
		{
			ReportPropertyChanging("DiscountStatus");
			_DiscountStatus = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("DiscountStatus");
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

	public static CloudDiscount CreateCloudDiscount(int id)
	{
		CloudDiscount cloudDiscount = new CloudDiscount();
		cloudDiscount.ID = id;
		return cloudDiscount;
	}
}
