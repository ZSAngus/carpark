using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "Statistics_UpdateUtil")]
[DataContract(IsReference = true)]
public class Statistics_UpdateUtil : EntityObject
{
	private int _ID;

	private DateTime? _updateTime;

	private int? _lastId;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? updateTime
	{
		get
		{
			return _updateTime;
		}
		set
		{
			ReportPropertyChanging("updateTime");
			_updateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("updateTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? lastId
	{
		get
		{
			return _lastId;
		}
		set
		{
			ReportPropertyChanging("lastId");
			_lastId = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("lastId");
		}
	}

	public static Statistics_UpdateUtil CreateStatistics_UpdateUtil(int id)
	{
		Statistics_UpdateUtil statistics_UpdateUtil = new Statistics_UpdateUtil();
		statistics_UpdateUtil.ID = id;
		return statistics_UpdateUtil;
	}
}
