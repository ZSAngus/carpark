using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class GetRentalChargeArgs
{
	/// <summary>
	/// 卡號
	/// </summary>
	[DataMember]
	public string CardNumber { get; set; }
}
