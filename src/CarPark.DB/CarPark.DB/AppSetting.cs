using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "AppSetting")]
public class AppSetting : EntityObject
{
	private int _ID;

	private int? _AreaID;

	private int? _AppTypeID;

	private string _OnlyID;

	private string _SettingString;

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
	public int? AreaID
	{
		get
		{
			return _AreaID;
		}
		set
		{
			ReportPropertyChanging("AreaID");
			_AreaID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AreaID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? AppTypeID
	{
		get
		{
			return _AppTypeID;
		}
		set
		{
			ReportPropertyChanging("AppTypeID");
			_AppTypeID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AppTypeID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string OnlyID
	{
		get
		{
			return _OnlyID;
		}
		set
		{
			ReportPropertyChanging("OnlyID");
			_OnlyID = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("OnlyID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string SettingString
	{
		get
		{
			return _SettingString;
		}
		set
		{
			ReportPropertyChanging("SettingString");
			_SettingString = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("SettingString");
		}
	}

	public static AppSetting CreateAppSetting(int id)
	{
		AppSetting appSetting = new AppSetting();
		appSetting.ID = id;
		return appSetting;
	}
}
