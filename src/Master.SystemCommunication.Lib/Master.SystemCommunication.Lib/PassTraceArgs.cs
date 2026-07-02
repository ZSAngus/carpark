using System;
using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 出入記錄
/// </summary>
public class PassTraceArgs : ProgramBase
{
	[DataMember]
	public int PassTraceID { get; set; }

	[DataMember]
	public DateTime PassTime { get; set; }

	[DataMember]
	public string PassCardCode { get; set; }

	[DataMember]
	public EnumParkType ParkTypeID { get; set; }

	[DataMember]
	public EnumPassStatus PassStatus { get; set; }

	[DataMember]
	public EnumPassDirection PassDirection { get; set; }

	[DataMember]
	public EnumCardType PassCardType { get; set; }

	[DataMember]
	public int TransactionID { get; set; }

	[DataMember]
	public string LicensePlate { get; set; }

	[DataMember]
	public short Similarity { get; set; }

	public PassTraceArgs()
	{
	}

	public PassTraceArgs(string onlyID)
		: base(onlyID)
	{
	}

	public override string ToString()
	{
		return string.Format("onlyID{10} PassTraceID{0} PassTime{1} PassCardCode{2} ParkTypeID{3} PassStatus{4} PassDirection{5} PassCardType{6} TransactionID{7} LicensePlate{8} Similarity{9}", PassTraceID, PassTime, PassCardCode, ParkTypeID, PassStatus, PassDirection, PassCardType, TransactionID, LicensePlate, Similarity, base.OnlyID);
	}
}
