using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "sysdiagrams")]
public class sysdiagrams : EntityObject
{
	private int _principal_id;

	private int _diagram_id;

	private int? _version;

	private byte[] _definition;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int principal_id
	{
		get
		{
			return _principal_id;
		}
		set
		{
			ReportPropertyChanging("principal_id");
			_principal_id = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("principal_id");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int diagram_id
	{
		get
		{
			return _diagram_id;
		}
		set
		{
			if (_diagram_id != value)
			{
				ReportPropertyChanging("diagram_id");
				_diagram_id = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("diagram_id");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public int? version
	{
		get
		{
			return _version;
		}
		set
		{
			ReportPropertyChanging("version");
			_version = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("version");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public byte[] definition
	{
		get
		{
			return StructuralObject.GetValidValue(_definition);
		}
		set
		{
			ReportPropertyChanging("definition");
			_definition = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("definition");
		}
	}

	public static sysdiagrams Createsysdiagrams(int principal_id, int diagram_id)
	{
		sysdiagrams sysdiagrams2 = new sysdiagrams();
		sysdiagrams2.principal_id = principal_id;
		sysdiagrams2.diagram_id = diagram_id;
		return sysdiagrams2;
	}
}
