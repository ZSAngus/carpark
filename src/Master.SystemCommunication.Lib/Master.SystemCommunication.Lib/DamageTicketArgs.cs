using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class DamageTicketArgs
{
	/// <summary>
	/// 壞票號
	/// </summary>
	[DataMember]
	public string DamageNumber { get; set; }
}
