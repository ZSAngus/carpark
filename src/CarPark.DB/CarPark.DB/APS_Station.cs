using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_Station")]
public class APS_Station : EntityObject
{
	private int _APSStationID;

	private string _StationNameCN;

	private string _StationNamePT;

	private string _StationCode;

	private string _PCName;

	public string APSName
	{
		get
		{
			string stationNameCN = _StationNameCN;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return stationNameCN;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return _StationNamePT;
			default:
				return stationNameCN;
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int APSStationID
	{
		get
		{
			return _APSStationID;
		}
		set
		{
			if (_APSStationID != value)
			{
				ReportPropertyChanging("APSStationID");
				_APSStationID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("APSStationID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string StationNameCN
	{
		get
		{
			return _StationNameCN;
		}
		set
		{
			ReportPropertyChanging("StationNameCN");
			_StationNameCN = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("StationNameCN");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string StationNamePT
	{
		get
		{
			return _StationNamePT;
		}
		set
		{
			ReportPropertyChanging("StationNamePT");
			_StationNamePT = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("StationNamePT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string StationCode
	{
		get
		{
			return _StationCode;
		}
		set
		{
			ReportPropertyChanging("StationCode");
			_StationCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("StationCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string PCName
	{
		get
		{
			return _PCName;
		}
		set
		{
			ReportPropertyChanging("PCName");
			_PCName = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("PCName");
		}
	}

	public static APS_Station CreateAPS_Station(int aPSStationID, string stationNameCN, string stationNamePT, string stationCode, string pCName)
	{
		APS_Station aPS_Station = new APS_Station();
		aPS_Station.APSStationID = aPSStationID;
		aPS_Station.StationNameCN = stationNameCN;
		aPS_Station.StationNamePT = stationNamePT;
		aPS_Station.StationCode = stationCode;
		aPS_Station.PCName = pCName;
		return aPS_Station;
	}
}
