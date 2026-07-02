using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 數據庫狀態改變參數
/// </summary>
public class ServerStatusChangeArgs : ProgramBase
{
	[DataMember]
	public bool ServerA { get; set; }

	[DataMember]
	public bool ServerB { get; set; }

	/// <summary>
	/// 取當前數據庫連接狀態
	/// </summary>
	public bool ServerStatus
	{
		get
		{
			if (ServerA || ServerB)
			{
				return true;
			}
			return false;
		}
	}

	public ServerStatusChangeArgs()
	{
	}

	public ServerStatusChangeArgs(string onlyID)
		: base(onlyID)
	{
	}
}
