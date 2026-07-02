using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "Festival")]
[DataContract(IsReference = true)]
public class Festival : EntityObject
{
	private int _DateTypeID;

	private string _DateNameCn;

	private string _DateNamePt;

	private int _WeekNum;

	private DateTime _Festival1;

	private bool _IsFestival;

	private bool _Enable;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int DateTypeID
	{
		get
		{
			return _DateTypeID;
		}
		set
		{
			if (_DateTypeID != value)
			{
				ReportPropertyChanging("DateTypeID");
				_DateTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("DateTypeID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string DateNameCn
	{
		get
		{
			return _DateNameCn;
		}
		set
		{
			ReportPropertyChanging("DateNameCn");
			_DateNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("DateNameCn");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string DateNamePt
	{
		get
		{
			return _DateNamePt;
		}
		set
		{
			ReportPropertyChanging("DateNamePt");
			_DateNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("DateNamePt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int WeekNum
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTime Festival1
	{
		get
		{
			return _Festival1;
		}
		set
		{
			ReportPropertyChanging("Festival1");
			_Festival1 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Festival1");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsFestival
	{
		get
		{
			return _IsFestival;
		}
		set
		{
			ReportPropertyChanging("IsFestival");
			_IsFestival = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsFestival");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool Enable
	{
		get
		{
			return _Enable;
		}
		set
		{
			ReportPropertyChanging("Enable");
			_Enable = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Enable");
		}
	}

	public static Festival CreateFestival(int dateTypeID, string dateNameCn, string dateNamePt, int weekNum, DateTime festival1, bool isFestival, bool enable)
	{
		Festival festival2 = new Festival();
		festival2.DateTypeID = dateTypeID;
		festival2.DateNameCn = dateNameCn;
		festival2.DateNamePt = dateNamePt;
		festival2.WeekNum = weekNum;
		festival2.Festival1 = festival1;
		festival2.IsFestival = isFestival;
		festival2.Enable = enable;
		return festival2;
	}
}
