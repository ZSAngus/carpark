using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "PreOffer")]
[DataContract(IsReference = true)]
public class PreOffer : EntityObject
{
	private int _ID;

	private string _ImagePath;

	private string _TransactionDataID;

	private int? _CustomFreeTenatID;

	private int? _CustomFreeTypeID;

	private int? _BarcodeID;

	private DateTime? _CreateTime;

	private string _CarateStaffCode;

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
	public string ImagePath
	{
		get
		{
			return _ImagePath;
		}
		set
		{
			ReportPropertyChanging("ImagePath");
			_ImagePath = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ImagePath");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string TransactionDataID
	{
		get
		{
			return _TransactionDataID;
		}
		set
		{
			ReportPropertyChanging("TransactionDataID");
			_TransactionDataID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TransactionDataID");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? BarcodeID
	{
		get
		{
			return _BarcodeID;
		}
		set
		{
			ReportPropertyChanging("BarcodeID");
			_BarcodeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("BarcodeID");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CarateStaffCode
	{
		get
		{
			return _CarateStaffCode;
		}
		set
		{
			ReportPropertyChanging("CarateStaffCode");
			_CarateStaffCode = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CarateStaffCode");
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

	public static PreOffer CreatePreOffer(int id)
	{
		PreOffer preOffer = new PreOffer();
		preOffer.ID = id;
		return preOffer;
	}
}
