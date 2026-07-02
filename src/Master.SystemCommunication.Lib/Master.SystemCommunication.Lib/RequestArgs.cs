using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class RequestArgs : ProgramBase
{
	[DataMember]
	public MessageType msgType { get; set; }

	[DataMember]
	public Type objType { get; set; }

	public RequestArgs()
	{
	}

	public RequestArgs(string onlyID)
		: base(onlyID)
	{
	}
}
