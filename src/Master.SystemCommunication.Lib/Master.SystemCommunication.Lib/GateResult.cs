using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

[Serializable]
public class GateResult
{
	[DataMember]
	public bool IsPass { get; set; }

	[DataMember]
	public string ErrorCode { get; set; }

	[DataMember]
	public int PassTraceID { get; set; }
}
