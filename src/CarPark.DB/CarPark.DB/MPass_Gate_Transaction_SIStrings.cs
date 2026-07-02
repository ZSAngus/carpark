using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "MPass_Gate_Transaction_SIStrings")]
public class MPass_Gate_Transaction_SIStrings : EntityObject
{
	private int _TransactionID;

	private DateTime _PackTime;

	private string _ReaderNo;

	private int _FromGateID;

	private string _SIString;

	private bool _IsPacked;

	private string _GenFileName;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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
	public DateTime PackTime
	{
		get
		{
			return _PackTime;
		}
		set
		{
			ReportPropertyChanging("PackTime");
			_PackTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("PackTime");
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

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int FromGateID
	{
		get
		{
			return _FromGateID;
		}
		set
		{
			ReportPropertyChanging("FromGateID");
			_FromGateID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("FromGateID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string SIString
	{
		get
		{
			return _SIString;
		}
		set
		{
			ReportPropertyChanging("SIString");
			_SIString = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("SIString");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsPacked
	{
		get
		{
			return _IsPacked;
		}
		set
		{
			ReportPropertyChanging("IsPacked");
			_IsPacked = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsPacked");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	public string GenFileName
	{
		get
		{
			return _GenFileName;
		}
		set
		{
			ReportPropertyChanging("GenFileName");
			_GenFileName = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("GenFileName");
		}
	}

	public static MPass_Gate_Transaction_SIStrings CreateMPass_Gate_Transaction_SIStrings(int transactionID, DateTime packTime, string readerNo, int fromGateID, string sIString, bool isPacked)
	{
		MPass_Gate_Transaction_SIStrings mPass_Gate_Transaction_SIStrings = new MPass_Gate_Transaction_SIStrings();
		mPass_Gate_Transaction_SIStrings.TransactionID = transactionID;
		mPass_Gate_Transaction_SIStrings.PackTime = packTime;
		mPass_Gate_Transaction_SIStrings.ReaderNo = readerNo;
		mPass_Gate_Transaction_SIStrings.FromGateID = fromGateID;
		mPass_Gate_Transaction_SIStrings.SIString = sIString;
		mPass_Gate_Transaction_SIStrings.IsPacked = isPacked;
		return mPass_Gate_Transaction_SIStrings;
	}
}
