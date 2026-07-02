using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class SaveUpBarrierCountReturn
{
	[DataMember]
	public bool ISOK { get; set; }

	/// <summary>
	/// 不通過原因
	/// </summary>
	[DataMember]
	public string ErrCode { get; set; }
}
