using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>  
/// 人手滿
/// </summary>
public class ManualChangeArgs : ProgramBase
{
	[DataMember]
	public int ParkAreaExtendID { get; set; }

	[DataMember]
	public string OperationPC { get; set; }

	[DataMember]
	public string ShiffCode { get; set; }

	[DataMember]
	public EnumParkType parkType { get; set; }

	/// <summary>
	/// 手工滿字，true為滿
	/// </summary>
	[DataMember]
	public bool ManualFull { get; set; }

	public ManualChangeArgs()
	{
	}

	public ManualChangeArgs(string onlyID)
		: base(onlyID)
	{
	}
}
