using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class PackMPassTransactionDataReturn
{
	[DataMember]
	public bool ISOK { get; set; }
}
