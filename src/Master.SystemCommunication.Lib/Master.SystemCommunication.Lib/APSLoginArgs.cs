using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class APSLoginArgs
{
	[DataMember]
	public string StaffCode { get; set; }

	[DataMember]
	public string PWD { get; set; }

	[DataMember]
	public string APSStationID { get; set; }
}
