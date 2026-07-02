using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "BOC_Gate_TransactionExtend_PackData_File")]
[DataContract(IsReference = true)]
public class BOC_Gate_TransactionExtend_PackData_File : EntityObject
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
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

	public static BOC_Gate_TransactionExtend_PackData_File CreateBOC_Gate_TransactionExtend_PackData_File(int autoID, DateTime startTime, DateTime endTime, DateTime zIPTime, bool isUploaded, byte[] fileContent, string fileName)
	{
		BOC_Gate_TransactionExtend_PackData_File bOC_Gate_TransactionExtend_PackData_File = new BOC_Gate_TransactionExtend_PackData_File();
		bOC_Gate_TransactionExtend_PackData_File.AutoID = autoID;
		bOC_Gate_TransactionExtend_PackData_File.StartTime = startTime;
		bOC_Gate_TransactionExtend_PackData_File.EndTime = endTime;
		bOC_Gate_TransactionExtend_PackData_File.ZIPTime = zIPTime;
		bOC_Gate_TransactionExtend_PackData_File.IsUploaded = isUploaded;
		bOC_Gate_TransactionExtend_PackData_File.FileContent = fileContent;
		bOC_Gate_TransactionExtend_PackData_File.FileName = fileName;
		return bOC_Gate_TransactionExtend_PackData_File;
	}
}
