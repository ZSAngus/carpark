using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

[DataContract(Namespace = "Master.SystemCommunication.Lib.ProgramBase")]
public class ProgramBase
{
	[DataMember]
	public string OnlyID { get; internal set; }

	/// <summary>
	/// 消息ID
	/// </summary>
	[DataMember]
	public string GuID { get; internal set; }

	/// <summary>
	/// 消息時間戳
	/// </summary>
	public DateTime MsgTimestamp { get; internal set; }

	[DataMember]
	public string Extend1 { get; set; }

	[DataMember]
	public string Extend2 { get; set; }

	[DataMember]
	public string Extend3 { get; set; }

	public ProgramBase()
	{
		GuID = Guid.NewGuid().ToString();
		MsgTimestamp = DateTime.Now;
	}

	public ProgramBase(string onlyID)
	{
		OnlyID = onlyID;
		GuID = Guid.NewGuid().ToString();
		MsgTimestamp = DateTime.Now;
	}
}
