using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

public class GetSerialArgs
{
	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public EnumParkType parkType { get; set; }

	[DataMember]
	public string SerialNo { get; set; }
}
