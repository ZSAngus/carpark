using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 傷殘接口
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IDisabilityCallBack))]
public interface IDisability
{
	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void DisabilityPress(DisabilityPressArgs disabilityPressArgs);
}
