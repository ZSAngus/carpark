using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.Lang;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "RentalType")]
[DataContract(IsReference = true)]
public class RentalType : EntityObject
{
	private int? _RuleControlID;

	private string _RealCountStr;

	private int? _ChargingMode;

	private decimal? _LimitCharge;

	private decimal? _ReissueCharge;

	private decimal? _ChargingModeFee;

	private int? _FixParkID;

	private int? _ParkLimit;

	private int _RentalTypeID;

	private string _RentalNameCn;

	private string _RentalNamePt;

	private int _ParkTypeID;

	private decimal _NormalCharge;

	private bool _CheckLoop;

	private int _RealCountType;

	private decimal _Deposit;

	private bool _IsDelete;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? RuleControlID
	{
		get
		{
			return _RuleControlID;
		}
		set
		{
			ReportPropertyChanging("RuleControlID");
			_RuleControlID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RuleControlID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string RealCountStr
	{
		get
		{
			return _RealCountStr;
		}
		set
		{
			ReportPropertyChanging("RealCountStr");
			_RealCountStr = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("RealCountStr");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? ChargingMode
	{
		get
		{
			return _ChargingMode;
		}
		set
		{
			ReportPropertyChanging("ChargingMode");
			_ChargingMode = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargingMode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? LimitCharge
	{
		get
		{
			return _LimitCharge;
		}
		set
		{
			ReportPropertyChanging("LimitCharge");
			_LimitCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LimitCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? ReissueCharge
	{
		get
		{
			return _ReissueCharge;
		}
		set
		{
			ReportPropertyChanging("ReissueCharge");
			_ReissueCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ReissueCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public decimal? ChargingModeFee
	{
		get
		{
			return _ChargingModeFee;
		}
		set
		{
			ReportPropertyChanging("ChargingModeFee");
			_ChargingModeFee = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ChargingModeFee");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? FixParkID
	{
		get
		{
			return _FixParkID;
		}
		set
		{
			ReportPropertyChanging("FixParkID");
			_FixParkID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FixParkID");
		}
	}

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Value)]
	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? ParkLimit
	{
		get
		{
			return _ParkLimit;
		}
		set
		{
			ReportPropertyChanging("ParkLimit");
			_ParkLimit = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkLimit");
		}
	}

	public bool RealCount
	{
		get
		{
			if (RealCountType != 0)
			{
				return true;
			}
			return false;
		}
	}

	public string RentalName
	{
		get
		{
			string rentalNameCn = RentalNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return rentalNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return RentalNamePt;
			default:
				return rentalNameCn;
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int RentalTypeID
	{
		get
		{
			return _RentalTypeID;
		}
		set
		{
			if (_RentalTypeID != value)
			{
				ReportPropertyChanging("RentalTypeID");
				_RentalTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("RentalTypeID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
			_RentalNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("RentalNameCn");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string RentalNamePt
	{
		get
		{
			return _RentalNamePt;
		}
		set
		{
			ReportPropertyChanging("RentalNamePt");
			_RentalNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("RentalNamePt");
		}
	}

	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumParkTypeSource))]
	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int ParkTypeID
	{
		get
		{
			return _ParkTypeID;
		}
		set
		{
			ReportPropertyChanging("ParkTypeID");
			_ParkTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkTypeID");
		}
	}

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Value)]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal NormalCharge
	{
		get
		{
			return _NormalCharge;
		}
		set
		{
			ReportPropertyChanging("NormalCharge");
			_NormalCharge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("NormalCharge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EditStyle = EnumEditStyle.CheckBox)]
	[DataMember]
	public bool CheckLoop
	{
		get
		{
			return _CheckLoop;
		}
		set
		{
			ReportPropertyChanging("CheckLoop");
			_CheckLoop = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CheckLoop");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumRealCountTypeSource))]
	[DataMember]
	public int RealCountType
	{
		get
		{
			return _RealCountType;
		}
		set
		{
			ReportPropertyChanging("RealCountType");
			_RealCountType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RealCountType");
		}
	}

	[BindingControlEditStyle(EditStyle = EnumEditStyle.Value)]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal Deposit
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	public static RentalType CreateRentalType(int rentalTypeID, string rentalNameCn, string rentalNamePt, int parkTypeID, decimal normalCharge, bool checkLoop, int realCountType, decimal deposit, bool isDelete)
	{
		RentalType rentalType = new RentalType();
		rentalType.RentalTypeID = rentalTypeID;
		rentalType.RentalNameCn = rentalNameCn;
		rentalType.RentalNamePt = rentalNamePt;
		rentalType.ParkTypeID = parkTypeID;
		rentalType.NormalCharge = normalCharge;
		rentalType.CheckLoop = checkLoop;
		rentalType.RealCountType = realCountType;
		rentalType.Deposit = deposit;
		rentalType.IsDelete = isDelete;
		return rentalType;
	}
}
