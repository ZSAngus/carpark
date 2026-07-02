using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 系統通知
/// </summary>
public class NoticeArgs : ProgramBase
{
	[DataMember]
	public string Content { get; set; }

	[DataMember]
	public NoticeType noticeType { get; set; }

	public NoticeArgs()
	{
	}

	public NoticeArgs(string onlyID)
		: base(onlyID)
	{
	}

	public void BillInOutContent(DateTime passTime, string passCardCode)
	{
	}
}
