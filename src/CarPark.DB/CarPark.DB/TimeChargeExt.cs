using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using CarPark.DB.AdditionalDataSource;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "TimeChargeExt")]
public class TimeChargeExt : EntityObject
{
	private int? _TimeChargeTypeID;

	private int _ExtID;

	private int _ParkTypeID;

	private int _StartHR;

	private int _EndHR;

	private decimal _Charge;

	private bool _AfterFlag;

	private bool _Enabled;

	private int? _WeekNum;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public int? TimeChargeTypeID
	{
		get
		{
			return _TimeChargeTypeID;
		}
		set
		{
			ReportPropertyChanging("TimeChargeTypeID");
			_TimeChargeTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("TimeChargeTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int ExtID
	{
		get
		{
			return _ExtID;
		}
		set
		{
			if (_ExtID != value)
			{
				ReportPropertyChanging("ExtID");
				_ExtID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ExtID");
			}
		}
	}

	[BindingControlEditStyle(EnumEditStyle.DbComboBox, typeof(EnumParkTypeSource))]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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
	public int StartHR
	{
		get
		{
			return _StartHR;
		}
		set
		{
			ReportPropertyChanging("StartHR");
			_StartHR = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartHR");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int EndHR
	{
		get
		{
			return _EndHR;
		}
		set
		{
			ReportPropertyChanging("EndHR");
			_EndHR = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndHR");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public decimal Charge
	{
		get
		{
			return _Charge;
		}
		set
		{
			ReportPropertyChanging("Charge");
			_Charge = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Charge");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool AfterFlag
	{
		get
		{
			return _AfterFlag;
		}
		set
		{
			ReportPropertyChanging("AfterFlag");
			_AfterFlag = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AfterFlag");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool Enabled
	{
		get
		{
			return _Enabled;
		}
		set
		{
			ReportPropertyChanging("Enabled");
			_Enabled = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Enabled");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? WeekNum
	{
		get
		{
			return _WeekNum;
		}
		set
		{
			ReportPropertyChanging("WeekNum");
			_WeekNum = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("WeekNum");
		}
	}

	public static TimeChargeExt CreateTimeChargeExt(int extID, int parkTypeID, int startHR, int endHR, decimal charge, bool afterFlag, bool enabled)
	{
		TimeChargeExt timeChargeExt = new TimeChargeExt();
		timeChargeExt.ExtID = extID;
		timeChargeExt.ParkTypeID = parkTypeID;
		timeChargeExt.StartHR = startHR;
		timeChargeExt.EndHR = endHR;
		timeChargeExt.Charge = charge;
		timeChargeExt.AfterFlag = afterFlag;
		timeChargeExt.Enabled = enabled;
		return timeChargeExt;
	}
}
