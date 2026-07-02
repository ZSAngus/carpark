using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 捉拍參數
/// </summary>
public class CameraArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public string PhotoPath { get; set; }

	public CameraArgs()
	{
	}

	public CameraArgs(string onlyID)
		: base(onlyID)
	{
	}
}
