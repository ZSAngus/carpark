using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "MPass_Gate_Transaction_PackData")]
public class MPass_Gate_Transaction_PackData : EntityObject
{
	private int _TransactionID;

	private DateTime _StartTime;

	private DateTime _EndTime;

	private DateTime? _UploadTime;

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

	public static MPass_Gate_Transaction_PackData CreateMPass_Gate_Transaction_PackData(int transactionID, DateTime startTime, DateTime endTime, bool isUploaded)
	{
		MPass_Gate_Transaction_PackData mPass_Gate_Transaction_PackData = new MPass_Gate_Transaction_PackData();
		mPass_Gate_Transaction_PackData.TransactionID = transactionID;
		mPass_Gate_Transaction_PackData.StartTime = startTime;
		mPass_Gate_Transaction_PackData.EndTime = endTime;
		mPass_Gate_Transaction_PackData.IsUploaded = isUploaded;
		return mPass_Gate_Transaction_PackData;
	}
}
