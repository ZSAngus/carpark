using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class SaveUpBarrierCountArgs
{
	[DataMember]
	public int GateID { get; set; }

	/// <summary>
	/// 起杆次數
	/// </summary>
	[DataMember]
	public int OpenCount { get; set; }

	/// <summary>
	/// 指定收費電腦名，類似於指定收費處車牌比對
	/// 用於記錄更號
	/// </summary>
	[DataMember]
	public string PayStationName { get; set; }
}
