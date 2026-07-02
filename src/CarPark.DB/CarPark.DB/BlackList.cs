using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "BlackList")]
public class BlackList : EntityObject
{
	private int _BID;

	private int? _CustomerID;

	private string _Context;

	private bool? _IsDelete;

	private DateTime? _RecordDate;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int BID
	{
		get
		{
			return _BID;
		}
		set
		{
			if (_BID != value)
			{
				ReportPropertyChanging("BID");
				_BID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("BID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? CustomerID
	{
		get
		{
			return _CustomerID;
		}
		set
		{
			ReportPropertyChanging("CustomerID");
			_CustomerID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CustomerID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Context
	{
		get
		{
			return _Context;
		}
		set
		{
			ReportPropertyChanging("Context");
			_Context = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Context");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public bool? IsDelete
	{
		get
		{
			return _IsDelete;
		}
		set
		{
			ReportPropertyChanging("IsDelete");
			_IsDelete = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDelete");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? RecordDate
	{
		get
		{
			return _RecordDate;
		}
		set
		{
			ReportPropertyChanging("RecordDate");
			_RecordDate = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RecordDate");
		}
	}

	public static BlackList CreateBlackList(int bID)
	{
		BlackList blackList = new BlackList();
		blackList.BID = bID;
		return blackList;
	}
}
