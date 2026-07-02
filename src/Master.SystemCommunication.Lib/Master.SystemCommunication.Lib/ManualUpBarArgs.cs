using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 人手起杆參數
/// </summary>
public class ManualUpBarArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	/// <summary>
	/// 操作電腦
	/// </summary>
	[DataMember]
	public string OperationPC { get; set; }

	/// <summary>
	/// 操作用戶
	/// </summary>
	[DataMember]
	public string ShiffCode { get; set; }

	public ManualUpBarArgs()
	{
	}

	public ManualUpBarArgs(string onlyID)
		: base(onlyID)
	{
	}
}
