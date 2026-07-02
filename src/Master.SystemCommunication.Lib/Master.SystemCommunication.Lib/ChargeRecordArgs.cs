using System;
using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 收費記錄
/// </summary>
public class ChargeRecordArgs : ProgramBase
{
	[DataMember]
	public int ChargeRecordID { get; set; }

	[DataMember]
	public DateTime ChargeTime { get; set; }

	[DataMember]
	public decimal TotalCharge { get; set; }

	[DataMember]
	public int ChargeMin { get; set; }

	[DataMember]
	public int FreeMin { get; set; }

	[DataMember]
	public decimal FreeCharge { get; set; }

	[DataMember]
	public int TransactionID { get; set; }

	[DataMember]
	public EnumBillType BillType { get; set; }

	[DataMember]
	public int ParkMin { get; set; }

	[DataMember]
	public string CardCode { get; set; }

	[DataMember]
	public EnumParkType ParkTypeID { get; set; }

	[DataMember]
	public decimal Fin { get; set; }

	[DataMember]
	public int BufferTime { get; set; }

	public ChargeRecordArgs()
	{
	}

	public ChargeRecordArgs(string onlyID)
		: base(onlyID)
	{
	}

	public override string ToString()
	{
		return string.Format("onlyID{13} ChargeRecordID{0} ChargeTime{1} TotalCharge{2} ChargeMin{3} FreeMin{4} FreeCharge{5} TransactionID{6} BillType{7} ParkMin{8} CardCode{9} ParkTypeID{10} Fin{11} BufferTime{12}", ChargeRecordID, ChargeTime, TotalCharge, ChargeMin, FreeMin, FreeCharge, TransactionID, BillType, ParkMin, CardCode, ParkTypeID, Fin, BufferTime, base.OnlyID);
	}
}
