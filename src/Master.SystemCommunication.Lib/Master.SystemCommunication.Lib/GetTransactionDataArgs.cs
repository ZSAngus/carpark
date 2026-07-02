using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class GetTransactionDataArgs
{
	[DataMember]
	public string TicketNumber { get; set; }
}
