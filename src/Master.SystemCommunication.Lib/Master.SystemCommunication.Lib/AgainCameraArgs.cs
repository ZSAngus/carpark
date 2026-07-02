using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 重新捉拍
/// </summary>
public class AgainCameraArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public string ShiffCode { get; set; }

	[DataMember]
	public string OperationPC { get; set; }

	[DataMember]
	public string LicensePlate { get; set; }

	public AgainCameraArgs()
	{
	}

	public AgainCameraArgs(string onlyID)
		: base(onlyID)
	{
	}
}
