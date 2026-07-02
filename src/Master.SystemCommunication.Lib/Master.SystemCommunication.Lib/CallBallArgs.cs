using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class CallBallArgs : ProgramBase
{
	[DataMember]
	public CallBackType callBackType { get; set; }

	public CallBallArgs()
	{
	}

	public CallBallArgs(string onlyID)
		: base(onlyID)
	{
	}
}
