using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IGateEventCallBack))]
public interface IGateEvent
{
	/// <summary>
	/// 系統通知
	/// </summary>
	/// <param name="noticeArgs"></param>
	[OperationContract]
	void SystemNotice(NoticeArgs noticeArgs);
}
