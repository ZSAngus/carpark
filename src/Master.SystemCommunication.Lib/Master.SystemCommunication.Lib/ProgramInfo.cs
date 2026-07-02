using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 程序信息
/// </summary>
[DataContract(Namespace = "Master.SystemCommunication.Lib.ProgramInfo")]
public class ProgramInfo : ProgramBase
{
	private int runState = -1;

	[DataMember]
	public string CompanyName { get; set; }

	[DataMember]
	public SystemType M_SystemType { get; internal set; }

	[DataMember]
	public string SerialNo { get; set; }

	[DataMember]
	public string Version { get; set; }

	[DataMember]
	public string InstalledLocation { get; set; }

	[DataMember]
	public string Description { get; set; }

	[DataMember]
	public string FormatString { get; set; }

	/// <summary>
	/// 運行狀態，-1:表示停止，0表示啟動，1表示運行中
	/// </summary>
	[DataMember]
	public int RunState
	{
		get
		{
			return runState;
		}
		set
		{
			UpdateStateTime = DateTime.Now;
			if (value < 0)
			{
				runState = -1;
				StopTime = UpdateStateTime;
			}
			else if (value == 0)
			{
				runState = 0;
				StartTime = UpdateStateTime;
			}
			else
			{
				runState = 1;
			}
		}
	}

	[DataMember]
	public DateTime UpdateStateTime { get; private set; }

	[DataMember]
	public DateTime StartTime { get; private set; }

	[DataMember]
	public DateTime StopTime { get; private set; }

	public ProgramInfo()
	{
	}

	public ProgramInfo(string onlyID, SystemType systemType)
		: base(onlyID)
	{
		M_SystemType = systemType;
	}

	public override string ToString()
	{
		return string.Format("{0:yyyy-MM-dd HH:mm:ss}\t{1}\t({2})\n", DateTime.Now, CompanyName, string.IsNullOrEmpty(FormatString) ? "" : FormatString);
	}
}
