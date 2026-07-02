using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class GetParkGateArgs
{
	[DataMember]
	public int GateID { get; set; }
}
