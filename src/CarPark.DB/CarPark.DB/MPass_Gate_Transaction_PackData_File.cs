using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "MPass_Gate_Transaction_PackData_File")]
public class MPass_Gate_Transaction_PackData_File : EntityObject
{
	private int _AutoID;

	private int _TransactionID;

	private byte[] _FileContent;

	private string _FileName;

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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public byte[] FileContent
	{
		get
		{
			return StructuralObject.GetValidValue(_FileContent);
		}
		set
		{
			ReportPropertyChanging("FileContent");
			_FileContent = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("FileContent");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string FileName
	{
		get
		{
			return _FileName;
		}
		set
		{
			ReportPropertyChanging("FileName");
			_FileName = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("FileName");
		}
	}

	public static MPass_Gate_Transaction_PackData_File CreateMPass_Gate_Transaction_PackData_File(int autoID, int transactionID, byte[] fileContent, string fileName)
	{
		MPass_Gate_Transaction_PackData_File mPass_Gate_Transaction_PackData_File = new MPass_Gate_Transaction_PackData_File();
		mPass_Gate_Transaction_PackData_File.AutoID = autoID;
		mPass_Gate_Transaction_PackData_File.TransactionID = transactionID;
		mPass_Gate_Transaction_PackData_File.FileContent = fileContent;
		mPass_Gate_Transaction_PackData_File.FileName = fileName;
		return mPass_Gate_Transaction_PackData_File;
	}
}
