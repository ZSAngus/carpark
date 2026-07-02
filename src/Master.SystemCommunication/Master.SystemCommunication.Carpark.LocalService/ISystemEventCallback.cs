using System.ServiceModel;
using CarPark.DB;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 系統通知
/// </summary>
[ServiceContract]
public interface ISystemEventCallback
{
	/// <summary>
	/// 系統消息通知
	/// </summary>
	/// <param name="noticeArgs"></param>
	[OperationContract]
	void SystemNotice(NoticeArgs noticeArgs);

	/// <summary>
	/// 車輛出入事件
	/// </summary>
	/// <param name="passTrace"></param>
	[OperationContract]
	void PassTraceChange(PassTrace passTrace);

	/// <summary>
	/// 數據庫狀態改變
	/// </summary>
	/// <param name="serverStatusChangeArgs"></param>
	[OperationContract]
	void ServerStatusChange(ServerStatusChangeArgs serverStatusChangeArgs);
}
