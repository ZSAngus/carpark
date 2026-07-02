using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 閘機事件通知
/// </summary>
[ServiceContract]
public interface IGateEventCallBack
{
	/// <summary>
	/// 車位改變
	/// </summary>
	/// <param name="parkingSpacesChangeNoticeArgs"></param>
	[OperationContract]
	void ParkingSpacesChangeNotice(ParkingSpacesChangeNoticeArgs parkingSpacesChangeNoticeArgs);

	[OperationContract]
	void SingleGateStatusChangeNotice(DeviceStatus deviceStatus);

	/// <summary>
	/// 系統通知
	/// </summary>
	/// <param name="noticeArgs"></param>
	[OperationContract]
	void SystemNotice(NoticeArgs noticeArgs);
}
