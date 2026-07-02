using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "view_SmartCard")]
public class view_SmartCard : EntityObject
{
	private int _CardID;

	private string _CardCode;

	private int? _RentalTypeID;

	private string _RentalNameCn;

	private string _RentalNamePt;

	private DateTime _CreateTime;

	private string _CreateStaffCode;

	private DateTime _StartDate;

	private DateTime? _ExpireDate;

	private int _Status;

	private string _LicensePlate;

	private int? _CardTypeID;

	private string _CardNameCn;

	private string _CardNamePt;

	private string _Remark;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public string CardCode
	{
		get
		{
			return _CardCode;
		}
		set
		{
			if (_CardCode != value)
			{
				ReportPropertyChanging("CardCode");
				_CardCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("CardCode");
			}
		}
	}

	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(DBRentalTypeSource))]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? RentalTypeID
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string RentalNameCn
	{
		get
		{
			return _RentalNameCn;
		}
		set
		{
			ReportPropertyChanging("RentalNameCn");
			_RentalNameCn = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("RentalNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string RentalNamePt
	{
		get
		{
			return _RentalNamePt;
		}
		set
		{
			ReportPropertyChanging("RentalNamePt");
			_RentalNamePt = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("RentalNamePt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public DateTime CreateTime
	{
		get
		{
			return _CreateTime;
		}
		set
		{
			if (_CreateTime != value)
			{
				ReportPropertyChanging("CreateTime");
				_CreateTime = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CreateTime");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public string CreateStaffCode
	{
		get
		{
			return _CreateStaffCode;
		}
		set
		{
			if (_CreateStaffCode != value)
			{
				ReportPropertyChanging("CreateStaffCode");
				_CreateStaffCode = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("CreateStaffCode");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public DateTime StartDate
	{
		get
		{
			return _StartDate;
		}
		set
		{
			if (_StartDate != value)
			{
				ReportPropertyChanging("StartDate");
				_StartDate = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("StartDate");
			}
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

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumCardStatusSource))]
	[DataMember]
	public int Status
	{
		get
		{
			return _Status;
		}
		set
		{
			if (_Status != value)
			{
				ReportPropertyChanging("Status");
				_Status = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("Status");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(DBCardTypeSource))]
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string CardNameCn
	{
		get
		{
			return _CardNameCn;
		}
		set
		{
			ReportPropertyChanging("CardNameCn");
			_CardNameCn = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CardNamePt
	{
		get
		{
			return _CardNamePt;
		}
		set
		{
			ReportPropertyChanging("CardNamePt");
			_CardNamePt = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CardNamePt");
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

	public static view_SmartCard Createview_SmartCard(int cardID, string cardCode, DateTime createTime, string createStaffCode, DateTime startDate, int status)
	{
		view_SmartCard view_SmartCard2 = new view_SmartCard();
		view_SmartCard2.CardID = cardID;
		view_SmartCard2.CardCode = cardCode;
		view_SmartCard2.CreateTime = createTime;
		view_SmartCard2.CreateStaffCode = createStaffCode;
		view_SmartCard2.StartDate = startDate;
		view_SmartCard2.Status = status;
		return view_SmartCard2;
	}
}
