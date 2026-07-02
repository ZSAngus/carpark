using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "TypeDictionary")]
public class TypeDictionary : EntityObject
{
	private int _AutoID;

	private string _TypeNameCN;

	private string _TypeNamePT;

	private string _C_type;

	private string _Ext1;

	private decimal? _Ext2;

	private bool? _IsDelete;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int AutoID
	{
		get
		{
			return _AutoID;
		}
		set
		{
			if (_AutoID != value)
			{
				ReportPropertyChanging("AutoID");
				_AutoID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("AutoID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string TypeNameCN
	{
		get
		{
			return _TypeNameCN;
		}
		set
		{
			ReportPropertyChanging("TypeNameCN");
			_TypeNameCN = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TypeNameCN");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string TypeNamePT
	{
		get
		{
			return _TypeNamePT;
		}
		set
		{
			ReportPropertyChanging("TypeNamePT");
			_TypeNamePT = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TypeNamePT");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string C_type
	{
		get
		{
			return _C_type;
		}
		set
		{
			ReportPropertyChanging("C_type");
			_C_type = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("C_type");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string Ext1
	{
		get
		{
			return _Ext1;
		}
		set
		{
			ReportPropertyChanging("Ext1");
			_Ext1 = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("Ext1");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public decimal? Ext2
	{
		get
		{
			return _Ext2;
		}
		set
		{
			ReportPropertyChanging("Ext2");
			_Ext2 = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("Ext2");
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

	public static TypeDictionary CreateTypeDictionary(int autoID, string typeNameCN, string typeNamePT, string c_type)
	{
		TypeDictionary typeDictionary = new TypeDictionary();
		typeDictionary.AutoID = autoID;
		typeDictionary.TypeNameCN = typeNameCN;
		typeDictionary.TypeNamePT = typeNamePT;
		typeDictionary.C_type = c_type;
		return typeDictionary;
	}
}
