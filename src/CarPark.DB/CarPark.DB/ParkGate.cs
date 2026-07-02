using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.Core;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ParkGate")]
public class ParkGate : EntityObject
{
	private int _GateID;

	private string _GateNameCn;

	private string _GateNamePt;

	private int _GateAreaID;

	private string _GateCode;

	private int _AllowedType;

	private int? _ParentGate;

	private int _GateDirection;

	private string _BarrierCode;

	private int _LPRSDisable;

	private bool _IsDelete;

	public EnumParkType AllowedParkType
	{
		get
		{
			if (AllowedType == 0)
			{
				return EnumParkType.None;
			}
			return (EnumParkType)AllowedType;
		}
	}

	public EnumPassDirection PassDirection
	{
		get
		{
			return (EnumPassDirection)GateDirection;
		}
		set
		{
			GateDirection = (int)value;
		}
	}

	public string GateName
	{
		get
		{
			string gateNameCn = GateNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return gateNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return GateNamePt;
			default:
				return gateNameCn;
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int GateID
	{
		get
		{
			return _GateID;
		}
		set
		{
			if (_GateID != value)
			{
				ReportPropertyChanging("GateID");
				_GateID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("GateID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string GateNameCn
	{
		get
		{
			return _GateNameCn;
		}
		set
		{
			ReportPropertyChanging("GateNameCn");
			_GateNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("GateNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string GateNamePt
	{
		get
		{
			return _GateNamePt;
		}
		set
		{
			ReportPropertyChanging("GateNamePt");
			_GateNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("GateNamePt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int GateAreaID
	{
		get
		{
			return _GateAreaID;
		}
		set
		{
			ReportPropertyChanging("GateAreaID");
			_GateAreaID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("GateAreaID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string GateCode
	{
		get
		{
			return _GateCode;
		}
		set
		{
			ReportPropertyChanging("GateCode");
			_GateCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("GateCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int AllowedType
	{
		get
		{
			return _AllowedType;
		}
		set
		{
			ReportPropertyChanging("AllowedType");
			_AllowedType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AllowedType");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? ParentGate
	{
		get
		{
			return _ParentGate;
		}
		set
		{
			ReportPropertyChanging("ParentGate");
			_ParentGate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParentGate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int GateDirection
	{
		get
		{
			return _GateDirection;
		}
		set
		{
			ReportPropertyChanging("GateDirection");
			_GateDirection = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("GateDirection");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string BarrierCode
	{
		get
		{
			return _BarrierCode;
		}
		set
		{
			ReportPropertyChanging("BarrierCode");
			_BarrierCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("BarrierCode");
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

	public static ParkGate CreateParkGate(int gateID, string gateNameCn, string gateNamePt, int gateAreaID, string gateCode, int allowedType, int gateDirection, string barrierCode, int lPRSDisable, bool isDelete)
	{
		ParkGate parkGate = new ParkGate();
		parkGate.GateID = gateID;
		parkGate.GateNameCn = gateNameCn;
		parkGate.GateNamePt = gateNamePt;
		parkGate.GateAreaID = gateAreaID;
		parkGate.GateCode = gateCode;
		parkGate.AllowedType = allowedType;
		parkGate.GateDirection = gateDirection;
		parkGate.BarrierCode = barrierCode;
		parkGate.LPRSDisable = lPRSDisable;
		parkGate.IsDelete = isDelete;
		return parkGate;
	}
}
