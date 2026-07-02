using System.Runtime.Serialization;

namespace CarPark.Core;

[DataContract]
public class EnterPrintData
{
	[DataMember]
	public bool HasValue { get; set; }

	[DataMember]
	public EnumCardType PrintReceiptType { get; set; }

	[DataMember]
	public string CardNumber { get; set; }

	[DataMember]
	public string CarParkName { get; set; }

	[DataMember]
	public string EnterTime { get; set; }

	[DataMember]
	public string ParkType { get; set; }

	[DataMember]
	public string Remain { get; set; }

	[DataMember]
	public string CardLogicNumber { get; set; }
}
