using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.APSInterface;

/// <summary>
/// aps監測系統接口
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IAPSMXtraServiceCallBack))]
public interface IAPSMXtraService : IService, ILongConnection
{
	[OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
	void UploadAPSRunningStatusArgs(APSRunningStatusArgs apsRunningStatusArgs);
}
