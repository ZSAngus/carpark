using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "Statistics_UseTime")]
[DataContract(IsReference = true)]
public class Statistics_UseTime : EntityObject
{
	private int _ID;

	private int? _TransactionID;

	private int? _UseTime;

	private string _Data_Year;

	private string _Data_Month;

	private string _Data_Day;

	private string _Data_Time;

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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? UseTime
	{
		get
		{
			return _UseTime;
		}
		set
		{
			ReportPropertyChanging("UseTime");
			_UseTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("UseTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Data_Year
	{
		get
		{
			return _Data_Year;
		}
		set
		{
			ReportPropertyChanging("Data_Year");
			_Data_Year = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Data_Year");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Data_Month
	{
		get
		{
			return _Data_Month;
		}
		set
		{
			ReportPropertyChanging("Data_Month");
			_Data_Month = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Data_Month");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Data_Day
	{
		get
		{
			return _Data_Day;
		}
		set
		{
			ReportPropertyChanging("Data_Day");
			_Data_Day = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Data_Day");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Data_Time
	{
		get
		{
			return _Data_Time;
		}
		set
		{
			ReportPropertyChanging("Data_Time");
			_Data_Time = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Data_Time");
		}
	}

	public static Statistics_UseTime CreateStatistics_UseTime(int id)
	{
		Statistics_UseTime statistics_UseTime = new Statistics_UseTime();
		statistics_UseTime.ID = id;
		return statistics_UseTime;
	}
}
