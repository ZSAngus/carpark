using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class GetInitInfoReturn
{
	[DataMember]
	public bool ISOK { get; set; }

	[DataMember]
	public string ErrCode { get; set; }
}
