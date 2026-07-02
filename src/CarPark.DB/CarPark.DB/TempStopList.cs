using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "TempStopList")]
public class TempStopList : EntityObject
{
	private int? _ParkTypeID;

	private int _ID;

	private int _SID;

	private string _LicensePlate;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? ParkTypeID
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int SID
	{
		get
		{
			return _SID;
		}
		set
		{
			ReportPropertyChanging("SID");
			_SID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("SID");
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

	public static TempStopList CreateTempStopList(int id, int sID)
	{
		TempStopList tempStopList = new TempStopList();
		tempStopList.ID = id;
		tempStopList.SID = sID;
		return tempStopList;
	}
}
