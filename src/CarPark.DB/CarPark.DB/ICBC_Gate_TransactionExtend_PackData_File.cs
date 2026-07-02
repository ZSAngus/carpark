using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "ICBC_Gate_TransactionExtend_PackData_File")]
[DataContract(IsReference = true)]
public class ICBC_Gate_TransactionExtend_PackData_File : EntityObject
{
	private int _AutoID;

	private DateTime _StartTime;

	private DateTime _EndTime;

	private DateTime? _UploadTime;

	private DateTime _ZIPTime;

	private bool _IsUploaded;

	private byte[] _FileContent;

	private string _FileName;

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
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
	public DateTime StartTime
	{
		get
		{
			return _StartTime;
		}
		set
		{
			ReportPropertyChanging("StartTime");
			_StartTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("StartTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime EndTime
	{
		get
		{
			return _EndTime;
		}
		set
		{
			ReportPropertyChanging("EndTime");
			_EndTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("EndTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public DateTime? UploadTime
	{
		get
		{
			return _UploadTime;
		}
		set
		{
			ReportPropertyChanging("UploadTime");
			_UploadTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("UploadTime");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime ZIPTime
	{
		get
		{
			return _ZIPTime;
		}
		set
		{
			ReportPropertyChanging("ZIPTime");
			_ZIPTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ZIPTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	public static ICBC_Gate_TransactionExtend_PackData_File CreateICBC_Gate_TransactionExtend_PackData_File(int autoID, DateTime startTime, DateTime endTime, DateTime zIPTime, bool isUploaded, byte[] fileContent, string fileName)
	{
		ICBC_Gate_TransactionExtend_PackData_File iCBC_Gate_TransactionExtend_PackData_File = new ICBC_Gate_TransactionExtend_PackData_File();
		iCBC_Gate_TransactionExtend_PackData_File.AutoID = autoID;
		iCBC_Gate_TransactionExtend_PackData_File.StartTime = startTime;
		iCBC_Gate_TransactionExtend_PackData_File.EndTime = endTime;
		iCBC_Gate_TransactionExtend_PackData_File.ZIPTime = zIPTime;
		iCBC_Gate_TransactionExtend_PackData_File.IsUploaded = isUploaded;
		iCBC_Gate_TransactionExtend_PackData_File.FileContent = fileContent;
		iCBC_Gate_TransactionExtend_PackData_File.FileName = fileName;
		return iCBC_Gate_TransactionExtend_PackData_File;
	}
}
