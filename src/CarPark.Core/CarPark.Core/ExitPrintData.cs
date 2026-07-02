using System.Runtime.Serialization;

namespace CarPark.Core;

[DataContract]
public class ExitPrintData : EnterPrintData
{
	[DataMember]
	public string Charge { get; set; }

	[DataMember]
	public string ExitTime { get; set; }

	[DataMember]
	public string ParkTime { get; set; }
}
