using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 所有閘機狀態
/// </summary>
public class GateStatusArgs : ProgramBase
{
	/// <summary>
	/// 所有閘機狀態
	/// </summary>
	[DataMember]
	public Dictionary<int, GateObj> DeviceList { get; set; }

	public GateStatusArgs()
	{
	}

	public GateStatusArgs(string onlyID)
		: base(onlyID)
	{
	}
}
