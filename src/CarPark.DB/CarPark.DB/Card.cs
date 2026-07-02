using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "Card")]
public class Card : EntityObject
{
	private string _CardOwner;

	private string _ForensicsType;

	private string _ForensicsAddress;

	private string _CardCodeExt;

	private decimal? _Deposit;

	private int? _CustomerID;

	private int? _RuleID;

	private int _CardID;

	private string _CardCode;

	private int _RentalTypeID;

	private DateTime _CreateTime;

	private string _CreateStaffCode;

	private DateTime _StartDate;

	private DateTime? _ExpireDate;

	private int _Status;

	private decimal _Remain;

	private string _Remark;

	private int _AllowedAreaID;

	private string _LicensePlate;

	private int? _CardTypeID;

	private bool _IsDelete;

	private int _ParkID;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CardOwner
	{
		get
		{
			return _CardOwner;
		}
		set
		{
			ReportPropertyChanging("CardOwner");
			_CardOwner = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardOwner");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ForensicsType
	{
		get
		{
			return _ForensicsType;
		}
		set
		{
			ReportPropertyChanging("ForensicsType");
			_ForensicsType = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ForensicsType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ForensicsAddress
	{
		get
		{
			return _ForensicsAddress;
		}
		set
		{
			ReportPropertyChanging("ForensicsAddress");
			_ForensicsAddress = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ForensicsAddress");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string CardCodeExt
	{
		get
		{
			return _CardCodeExt;
		}
		set
		{
			ReportPropertyChanging("CardCodeExt");
			_CardCodeExt = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardCodeExt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? Deposit
	{
		get
		{
			return _Deposit;
		}
		set
		{
			ReportPropertyChanging("Deposit");
			_Deposit = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Deposit");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? RuleID
	{
		get
		{
			return _RuleID;
		}
		set
		{
			ReportPropertyChanging("RuleID");
			_RuleID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RuleID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int CardID
	{
		get
		{
			return _CardID;
		}
		set
		{
			if (_CardID != value)
			{
				ReportPropertyChanging("CardID");
				_CardID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CardID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CardCode
	{
		get
		{
			return _CardCode;
		}
		set
		{
			ReportPropertyChanging("CardCode");
			_CardCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CardCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(DBRentalTypeSource))]
	public int RentalTypeID
	{
		get
		{
			return _RentalTypeID;
		}
		set
		{
			ReportPropertyChanging("RentalTypeID");
			_RentalTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RentalTypeID");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CreateStaffCode
	{
		get
		{
			return _CreateStaffCode;
		}
		set
		{
			ReportPropertyChanging("CreateStaffCode");
			_CreateStaffCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CreateStaffCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime StartDate
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
	public DateTime? ExpireDate
	{
		get
		{
			return _ExpireDate;
		}
		set
		{
			ReportPropertyChanging("ExpireDate");
			_ExpireDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ExpireDate");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumCardStatusSource))]
	public int Status
	{
		get
		{
			return _Status;
		}
		set
		{
			ReportPropertyChanging("Status");
			_Status = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Status");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public decimal Remain
	{
		get
		{
			return _Remain;
		}
		set
		{
			ReportPropertyChanging("Remain");
			_Remain = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Remain");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	[BindingControlEditStyle(EnumEditStyle.MultiCheckBox, typeof(DBParkAreaSource))]
	public int AllowedAreaID
	{
		get
		{
			return _AllowedAreaID;
		}
		set
		{
			ReportPropertyChanging("AllowedAreaID");
			_AllowedAreaID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AllowedAreaID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string LicensePlate
	{
		get
		{
			return _LicensePlate;
		}
		set
		{
			ReportPropertyChanging("LicensePlate");
			_LicensePlate = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("LicensePlate");
		}
	}

	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(DBCardTypeSource))]
	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int? CardTypeID
	{
		get
		{
			return _CardTypeID;
		}
		set
		{
			ReportPropertyChanging("CardTypeID");
			_CardTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CardTypeID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(DBParkSource))]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ParkID
	{
		get
		{
			return _ParkID;
		}
		set
		{
			ReportPropertyChanging("ParkID");
			_ParkID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkID");
		}
	}

	public static Card CreateCard(int cardID, string cardCode, int rentalTypeID, DateTime createTime, string createStaffCode, DateTime startDate, int status, decimal remain, int allowedAreaID, int cardTypeID, bool isDelete)
	{
		Card card = new Card();
		card.CardID = cardID;
		card.CardCode = cardCode;
		card.RentalTypeID = rentalTypeID;
		card.CreateTime = createTime;
		card.CreateStaffCode = createStaffCode;
		card.StartDate = startDate;
		card.Status = status;
		card.Remain = remain;
		card.AllowedAreaID = allowedAreaID;
		card.CardTypeID = cardTypeID;
		card.IsDelete = isDelete;
		return card;
	}
}
