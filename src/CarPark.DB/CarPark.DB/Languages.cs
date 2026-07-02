using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "Languages")]
public class Languages : EntityObject
{
	private int _LangresID;

	private string _LangKey;

	private string _LangCHT;

	private string _LangEN;

	private int _LangType;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int LangresID
	{
		get
		{
			return _LangresID;
		}
		set
		{
			if (_LangresID != value)
			{
				ReportPropertyChanging("LangresID");
				_LangresID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("LangresID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string LangKey
	{
		get
		{
			return _LangKey;
		}
		set
		{
			ReportPropertyChanging("LangKey");
			_LangKey = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("LangKey");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string LangCHT
	{
		get
		{
			return _LangCHT;
		}
		set
		{
			ReportPropertyChanging("LangCHT");
			_LangCHT = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("LangCHT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string LangEN
	{
		get
		{
			return _LangEN;
		}
		set
		{
			ReportPropertyChanging("LangEN");
			_LangEN = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("LangEN");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int LangType
	{
		get
		{
			return _LangType;
		}
		set
		{
			ReportPropertyChanging("LangType");
			_LangType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LangType");
		}
	}

	public static Languages CreateLanguages(int langresID, string langKey, string langCHT, string langEN, int langType)
	{
		Languages languages = new Languages();
		languages.LangresID = langresID;
		languages.LangKey = langKey;
		languages.LangCHT = langCHT;
		languages.LangEN = langEN;
		languages.LangType = langType;
		return languages;
	}
}
