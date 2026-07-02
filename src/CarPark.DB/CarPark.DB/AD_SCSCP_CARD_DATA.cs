using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "AD_SCSCP_CARD_DATA")]
public class AD_SCSCP_CARD_DATA : EntityObject
{
	private DateTimeOffset? _Last_modify_time;

	private string _HOLDER_ID;

	private string _FULL_HOLDER_ID;

	private string _HOLDER_TYPE;

	private string _HOLDER_NAME;

	private string _HOLDER_NAME_CH;

	private string _HOLDER_STATUS;

	private string _REFERENCE_UNIT;

	private string _CARD_SERIAL_NO;

	private DateTime? _CARD_EFFECTIVE_DATE;

	private string _CARD_STATUS;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTimeOffset? Last_modify_time
	{
		get
		{
			return _Last_modify_time;
		}
		set
		{
			ReportPropertyChanging("Last_modify_time");
			_Last_modify_time = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Last_modify_time");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public string HOLDER_ID
	{
		get
		{
			return _HOLDER_ID;
		}
		set
		{
			if (_HOLDER_ID != value)
			{
				ReportPropertyChanging("HOLDER_ID");
				_HOLDER_ID = StructuralObject.SetValidValue(value, isNullable: false);
				ReportPropertyChanged("HOLDER_ID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string FULL_HOLDER_ID
	{
		get
		{
			return _FULL_HOLDER_ID;
		}
		set
		{
			ReportPropertyChanging("FULL_HOLDER_ID");
			_FULL_HOLDER_ID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("FULL_HOLDER_ID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string HOLDER_TYPE
	{
		get
		{
			return _HOLDER_TYPE;
		}
		set
		{
			ReportPropertyChanging("HOLDER_TYPE");
			_HOLDER_TYPE = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("HOLDER_TYPE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string HOLDER_NAME
	{
		get
		{
			return _HOLDER_NAME;
		}
		set
		{
			ReportPropertyChanging("HOLDER_NAME");
			_HOLDER_NAME = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("HOLDER_NAME");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string HOLDER_NAME_CH
	{
		get
		{
			return _HOLDER_NAME_CH;
		}
		set
		{
			ReportPropertyChanging("HOLDER_NAME_CH");
			_HOLDER_NAME_CH = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("HOLDER_NAME_CH");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string HOLDER_STATUS
	{
		get
		{
			return _HOLDER_STATUS;
		}
		set
		{
			ReportPropertyChanging("HOLDER_STATUS");
			_HOLDER_STATUS = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("HOLDER_STATUS");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string REFERENCE_UNIT
	{
		get
		{
			return _REFERENCE_UNIT;
		}
		set
		{
			ReportPropertyChanging("REFERENCE_UNIT");
			_REFERENCE_UNIT = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("REFERENCE_UNIT");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CARD_SERIAL_NO
	{
		get
		{
			return _CARD_SERIAL_NO;
		}
		set
		{
			ReportPropertyChanging("CARD_SERIAL_NO");
			_CARD_SERIAL_NO = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CARD_SERIAL_NO");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? CARD_EFFECTIVE_DATE
	{
		get
		{
			return _CARD_EFFECTIVE_DATE;
		}
		set
		{
			ReportPropertyChanging("CARD_EFFECTIVE_DATE");
			_CARD_EFFECTIVE_DATE = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CARD_EFFECTIVE_DATE");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CARD_STATUS
	{
		get
		{
			return _CARD_STATUS;
		}
		set
		{
			ReportPropertyChanging("CARD_STATUS");
			_CARD_STATUS = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CARD_STATUS");
		}
	}

	public static AD_SCSCP_CARD_DATA CreateAD_SCSCP_CARD_DATA(string hOLDER_ID)
	{
		AD_SCSCP_CARD_DATA aD_SCSCP_CARD_DATA = new AD_SCSCP_CARD_DATA();
		aD_SCSCP_CARD_DATA.HOLDER_ID = hOLDER_ID;
		return aD_SCSCP_CARD_DATA;
	}
}
