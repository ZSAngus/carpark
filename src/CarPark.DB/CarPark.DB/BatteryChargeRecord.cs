using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "BatteryChargeRecord")]
public class BatteryChargeRecord : EntityObject
{
	private int _ID;

	private string _Licenseplate;

	private DateTime _Intime;

	private DateTime? _Outtime;

	private int _TransactionID;

	private string _ImagePath;

	private int? _ParkID;

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
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string Licenseplate
	{
		get
		{
			return _Licenseplate;
		}
		set
		{
			ReportPropertyChanging("Licenseplate");
			_Licenseplate = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("Licenseplate");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime Intime
	{
		get
		{
			return _Intime;
		}
		set
		{
			ReportPropertyChanging("Intime");
			_Intime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Intime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? Outtime
	{
		get
		{
			return _Outtime;
		}
		set
		{
			ReportPropertyChanging("Outtime");
			_Outtime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Outtime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int TransactionID
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
	public string ImagePath
	{
		get
		{
			return _ImagePath;
		}
		set
		{
			ReportPropertyChanging("ImagePath");
			_ImagePath = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ImagePath");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? ParkID
	{
		get
		{
			return _ParkID;
		}
		set
		{
			ReportPropertyChanging("ParkID");
			_ParkID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ParkID");
		}
	}

	public static BatteryChargeRecord CreateBatteryChargeRecord(int id, string licenseplate, DateTime intime, int transactionID)
	{
		BatteryChargeRecord batteryChargeRecord = new BatteryChargeRecord();
		batteryChargeRecord.ID = id;
		batteryChargeRecord.Licenseplate = licenseplate;
		batteryChargeRecord.Intime = intime;
		batteryChargeRecord.TransactionID = transactionID;
		return batteryChargeRecord;
	}
}
