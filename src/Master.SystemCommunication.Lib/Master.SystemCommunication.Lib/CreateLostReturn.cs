using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class CreateLostReturn
{
	/// <summary>
	/// 生成的失票號
	/// </summary>
	[DataMember]
	public string TicketNumber { get; set; }
}
