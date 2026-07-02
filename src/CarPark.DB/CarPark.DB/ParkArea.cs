using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ParkArea")]
[DataContract(IsReference = true)]
public class ParkArea : EntityObject
{
	private int _AreaID;

	private string _AreaNameCn;

	private string _AreaNamePt;

	public string AreaName
	{
		get
		{
			string areaNameCn = AreaNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return areaNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return AreaNamePt;
			default:
				return areaNameCn;
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int AreaID
	{
		get
		{
			return _AreaID;
		}
		set
		{
			if (_AreaID != value)
			{
				ReportPropertyChanging("AreaID");
				_AreaID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("AreaID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string AreaNameCn
	{
		get
		{
			return _AreaNameCn;
		}
		set
		{
			ReportPropertyChanging("AreaNameCn");
			_AreaNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("AreaNameCn");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string AreaNamePt
	{
		get
		{
			return _AreaNamePt;
		}
		set
		{
			ReportPropertyChanging("AreaNamePt");
			_AreaNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("AreaNamePt");
		}
	}

	public static ParkArea CreateParkArea(int areaID, string areaNameCn, string areaNamePt)
	{
		ParkArea parkArea = new ParkArea();
		parkArea.AreaID = areaID;
		parkArea.AreaNameCn = areaNameCn;
		parkArea.AreaNamePt = areaNamePt;
		return parkArea;
	}
}
