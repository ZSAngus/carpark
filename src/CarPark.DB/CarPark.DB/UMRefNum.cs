using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "UMRefNum")]
public class UMRefNum : EntityObject
{
	private int _ID;

	private string _TypeName;

	private int? _RefNo;

	private DateTime? _CreateTime;

	private string _CreateStaff;

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
	public string TypeName
	{
		get
		{
			return _TypeName;
		}
		set
		{
			ReportPropertyChanging("TypeName");
			_TypeName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("TypeName");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? RefNo
	{
		get
		{
			return _RefNo;
		}
		set
		{
			ReportPropertyChanging("RefNo");
			_RefNo = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RefNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? CreateTime
	{
		get
		{
			return _CreateTime;
		}
		set
		{
			ReportPropertyChanging("CreateTime");
			_CreateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CreateTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string CreateStaff
	{
		get
		{
			return _CreateStaff;
		}
		set
		{
			ReportPropertyChanging("CreateStaff");
			_CreateStaff = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("CreateStaff");
		}
	}

	public static UMRefNum CreateUMRefNum(int id)
	{
		UMRefNum uMRefNum = new UMRefNum();
		uMRefNum.ID = id;
		return uMRefNum;
	}
}
