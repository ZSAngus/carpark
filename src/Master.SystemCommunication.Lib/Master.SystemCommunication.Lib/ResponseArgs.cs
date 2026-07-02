using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class ResponseArgs : ProgramBase
{
	[DataMember]
	public MessageType msgType { get; set; }

	[DataMember]
	public Type objType { get; set; }

	public ResponseArgs()
	{
	}

	public ResponseArgs(string onlyID)
		: base(onlyID)
	{
	}
}
