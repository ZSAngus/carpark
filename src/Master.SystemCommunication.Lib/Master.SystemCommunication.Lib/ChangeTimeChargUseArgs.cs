using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 改變時租占用參數
/// </summary>
public class ChangeTimeChargUseArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	/// <summary>
	/// true為進場
	/// </summary>
	[DataMember]
	public bool IsIn { get; set; }

	public ChangeTimeChargUseArgs()
	{
	}

	public ChangeTimeChargUseArgs(string onlyID)
		: base(onlyID)
	{
	}
}
