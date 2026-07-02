using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>
///             車場出入口參數基類
/// </summary>
public class BaseArgs
{
	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public string PassCardNum { get; set; }

	[DataMember]
	public EnumParkType parkType { get; set; }
}
