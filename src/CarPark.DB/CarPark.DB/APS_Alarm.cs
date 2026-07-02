using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_Alarm")]
public class APS_Alarm : EntityObject
{
	private int _AlarmID;

	private int _AlarmCode;

	private string _NameCN;

	private string _NamePT;

	private string _APSReactionCN;

	private string _APSReactionPT;

	private string _Remark;

	public string Name
	{
		get
		{
			string nameCN = _NameCN;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return nameCN;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return NamePT;
			default:
				return nameCN;
			}
		}
	}

	public string APSReaction
	{
		get
		{
			string aPSReactionCN = _APSReactionCN;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return aPSReactionCN;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return _APSReactionPT;
			default:
				return aPSReactionCN;
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int AlarmID
	{
		get
		{
			return _AlarmID;
		}
		set
		{
			if (_AlarmID != value)
			{
				ReportPropertyChanging("AlarmID");
				_AlarmID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("AlarmID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int AlarmCode
	{
		get
		{
			return _AlarmCode;
		}
		set
		{
			ReportPropertyChanging("AlarmCode");
			_AlarmCode = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AlarmCode");
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string APSReactionCN
	{
		get
		{
			return _APSReactionCN;
		}
		set
		{
			ReportPropertyChanging("APSReactionCN");
			_APSReactionCN = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("APSReactionCN");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string APSReactionPT
	{
		get
		{
			return _APSReactionPT;
		}
		set
		{
			ReportPropertyChanging("APSReactionPT");
			_APSReactionPT = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("APSReactionPT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string Remark
	{
		get
		{
			return _Remark;
		}
		set
		{
			ReportPropertyChanging("Remark");
			_Remark = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("Remark");
		}
	}

	public static APS_Alarm CreateAPS_Alarm(int alarmID, int alarmCode, string nameCN, string namePT, string remark)
	{
		APS_Alarm aPS_Alarm = new APS_Alarm();
		aPS_Alarm.AlarmID = alarmID;
		aPS_Alarm.AlarmCode = alarmCode;
		aPS_Alarm.NameCN = nameCN;
		aPS_Alarm.NamePT = namePT;
		aPS_Alarm.Remark = remark;
		return aPS_Alarm;
	}
}
