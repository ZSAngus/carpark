using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.Core;
using CarPark.DB.AdditionalDataSource;
using SkyInno.Lang;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "PassTrace")]
[DataContract(IsReference = true)]
public class PassTrace : EntityObject
{
	private int _PassTraceID;

	private int _PassGateID;

	private DateTime _PassTime;

	private string _PassCardCode;

	private int _ParkTypeID;

	private int _PassStatus;

	private int _PassDirection;

	private string _PassRemarkCode;

	private string _PassRemarkCn;

	private string _PassRemarkPt;

	private int? _RentalTypeID;

	private int _PassBillType;

	private int? _TransactionID;

	private bool _IsOpenGate;

	private int _LPRSDisable;

	private int _CardTypeID;

	private bool _IsDelete;

	private bool? _OfflineStatus;

	public EnumPassStatus ExtendEnumPassStatus
	{
		get
		{
			return (EnumPassStatus)PassStatus;
		}
		set
		{
			PassStatus = (int)value;
		}
	}

	public EnumPassDirection ExtendEnumPassDirection
	{
		get
		{
			return (EnumPassDirection)PassDirection;
		}
		set
		{
			PassDirection = (int)value;
		}
	}

	public string PassRemark
	{
		get
		{
			string passRemarkCn = PassRemarkCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return passRemarkCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return PassRemarkPt;
			default:
				return passRemarkCn;
			}
		}
	}

	public string PassTimeHR => PassTime.ToString("HH:mm");

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int PassTraceID
	{
		get
		{
			return _PassTraceID;
		}
		set
		{
			if (_PassTraceID != value)
			{
				ReportPropertyChanging("PassTraceID");
				_PassTraceID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("PassTraceID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int PassGateID
	{
		get
		{
			return _PassGateID;
		}
		set
		{
			ReportPropertyChanging("PassGateID");
			_PassGateID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PassGateID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime PassTime
	{
		get
		{
			return _PassTime;
		}
		set
		{
			ReportPropertyChanging("PassTime");
			_PassTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PassTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string PassCardCode
	{
		get
		{
			return _PassCardCode;
		}
		set
		{
			ReportPropertyChanging("PassCardCode");
			_PassCardCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("PassCardCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumParkTypeSource))]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumPassStatusSource))]
	public int PassStatus
	{
		get
		{
			return _PassStatus;
		}
		set
		{
			ReportPropertyChanging("PassStatus");
			_PassStatus = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PassStatus");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int PassDirection
	{
		get
		{
			return _PassDirection;
		}
		set
		{
			ReportPropertyChanging("PassDirection");
			_PassDirection = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PassDirection");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string PassRemarkCode
	{
		get
		{
			return _PassRemarkCode;
		}
		set
		{
			ReportPropertyChanging("PassRemarkCode");
			_PassRemarkCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("PassRemarkCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string PassRemarkCn
	{
		get
		{
			return _PassRemarkCn;
		}
		set
		{
			ReportPropertyChanging("PassRemarkCn");
			_PassRemarkCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("PassRemarkCn");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string PassRemarkPt
	{
		get
		{
			return _PassRemarkPt;
		}
		set
		{
			ReportPropertyChanging("PassRemarkPt");
			_PassRemarkPt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("PassRemarkPt");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumCardTypeSource))]
	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int PassBillType
	{
		get
		{
			return _PassBillType;
		}
		set
		{
			ReportPropertyChanging("PassBillType");
			_PassBillType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PassBillType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? TransactionID
	{
		get
		{
			return _TransactionID;
		}
		set
		{
			ReportPropertyChanging("TransactionID");
			_TransactionID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TransactionID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsOpenGate
	{
		get
		{
			return _IsOpenGate;
		}
		set
		{
			ReportPropertyChanging("IsOpenGate");
			_IsOpenGate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsOpenGate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int LPRSDisable
	{
		get
		{
			return _LPRSDisable;
		}
		set
		{
			ReportPropertyChanging("LPRSDisable");
			_LPRSDisable = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LPRSDisable");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int CardTypeID
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? OfflineStatus
	{
		get
		{
			return _OfflineStatus;
		}
		set
		{
			ReportPropertyChanging("OfflineStatus");
			_OfflineStatus = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("OfflineStatus");
		}
	}

	public static PassTrace CreatePassTrace(int passTraceID, int passGateID, DateTime passTime, string passCardCode, int parkTypeID, int passStatus, int passDirection, string passRemarkCode, string passRemarkCn, string passRemarkPt, int passBillType, bool isOpenGate, int lPRSDisable, int cardTypeID, bool isDelete)
	{
		PassTrace passTrace = new PassTrace();
		passTrace.PassTraceID = passTraceID;
		passTrace.PassGateID = passGateID;
		passTrace.PassTime = passTime;
		passTrace.PassCardCode = passCardCode;
		passTrace.ParkTypeID = parkTypeID;
		passTrace.PassStatus = passStatus;
		passTrace.PassDirection = passDirection;
		passTrace.PassRemarkCode = passRemarkCode;
		passTrace.PassRemarkCn = passRemarkCn;
		passTrace.PassRemarkPt = passRemarkPt;
		passTrace.PassBillType = passBillType;
		passTrace.IsOpenGate = isOpenGate;
		passTrace.LPRSDisable = lPRSDisable;
		passTrace.CardTypeID = cardTypeID;
		passTrace.IsDelete = isDelete;
		return passTrace;
	}
}
