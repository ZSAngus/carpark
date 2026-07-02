using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class ShowMessageArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public string Message { get; set; }

	[DataMember]
	public bool IsError { get; set; }

	public ShowMessageArgs()
	{
	}

	public ShowMessageArgs(string onlyID)
		: base(onlyID)
	{
	}
}
