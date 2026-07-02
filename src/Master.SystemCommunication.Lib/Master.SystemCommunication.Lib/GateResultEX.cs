using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

[Serializable]
public class GateResultEX : GateResult
{
	[DataMember]
	public int PassBillType { get; set; }

	[DataMember]
	public string PassCardCode { get; set; }

	[DataMember]
	public int PassGateID { get; set; }

	[DataMember]
	public int PassDirection { get; set; }

	[DataMember]
	public int LPRSDisable { get; set; }

	[DataMember]
	public int TransactionID { get; set; }

	[DataMember]
	public string RegisterLicensePlate { get; set; }

	[DataMember]
	public string EnterResult { get; set; }

	[DataMember]
	public string EnterImagePath { get; set; }

	[DataMember]
	public DateTime Intime { get; set; }

	[DataMember]
	public int? RentalType { get; set; }
}
