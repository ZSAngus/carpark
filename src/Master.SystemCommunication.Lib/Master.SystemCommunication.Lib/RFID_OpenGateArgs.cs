using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// RFID起杆參數
/// </summary>
public class RFID_OpenGateArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	public RFID_OpenGateArgs()
	{
	}

	public RFID_OpenGateArgs(string onlyID)
		: base(onlyID)
	{
	}
}
