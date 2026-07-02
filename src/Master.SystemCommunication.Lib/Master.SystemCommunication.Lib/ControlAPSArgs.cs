using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class ControlAPSArgs
{
	[DataMember]
	public APSControlType apsControlType { get; set; }
}
