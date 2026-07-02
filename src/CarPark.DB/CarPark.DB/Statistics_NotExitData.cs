using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "Statistics_NotExitData")]
public class Statistics_NotExitData : EntityObject
{
	private int _ID;

	private int? _TransactionID;

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

	public static Statistics_NotExitData CreateStatistics_NotExitData(int id)
	{
		Statistics_NotExitData statistics_NotExitData = new Statistics_NotExitData();
		statistics_NotExitData.ID = id;
		return statistics_NotExitData;
	}
}
