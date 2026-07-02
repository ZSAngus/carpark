using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "MPass_Gate_Transaction_SIFiles")]
[DataContract(IsReference = true)]
public class MPass_Gate_Transaction_SIFiles : EntityObject
{
	private int _TransactionID;

	private byte[] _SIFile;

	private string _FIFileName;

	private string _ReaderNo;

	private bool _IsUploaded;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int TransactionID
	{
		get
		{
			return _TransactionID;
		}
		set
		{
			if (_TransactionID != value)
			{
				ReportPropertyChanging("TransactionID");
				_TransactionID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("TransactionID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public byte[] SIFile
	{
		get
		{
			return StructuralObject.GetValidValue(_SIFile);
		}
		set
		{
			ReportPropertyChanging("SIFile");
			_SIFile = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("SIFile");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string FIFileName
	{
		get
		{
			return _FIFileName;
		}
		set
		{
			ReportPropertyChanging("FIFileName");
			_FIFileName = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("FIFileName");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string ReaderNo
	{
		get
		{
			return _ReaderNo;
		}
		set
		{
			ReportPropertyChanging("ReaderNo");
			_ReaderNo = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("ReaderNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsUploaded
	{
		get
		{
			return _IsUploaded;
		}
		set
		{
			ReportPropertyChanging("IsUploaded");
			_IsUploaded = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsUploaded");
		}
	}

	public static MPass_Gate_Transaction_SIFiles CreateMPass_Gate_Transaction_SIFiles(int transactionID, byte[] sIFile, string fIFileName, string readerNo, bool isUploaded)
	{
		MPass_Gate_Transaction_SIFiles mPass_Gate_Transaction_SIFiles = new MPass_Gate_Transaction_SIFiles();
		mPass_Gate_Transaction_SIFiles.TransactionID = transactionID;
		mPass_Gate_Transaction_SIFiles.SIFile = sIFile;
		mPass_Gate_Transaction_SIFiles.FIFileName = fIFileName;
		mPass_Gate_Transaction_SIFiles.ReaderNo = readerNo;
		mPass_Gate_Transaction_SIFiles.IsUploaded = isUploaded;
		return mPass_Gate_Transaction_SIFiles;
	}
}
