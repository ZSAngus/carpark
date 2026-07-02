using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class EndShiftArgs
{
	[DataMember]
	public string PayStationName { get; set; }

	[DataMember]
	public string staffCode { get; set; }
}
