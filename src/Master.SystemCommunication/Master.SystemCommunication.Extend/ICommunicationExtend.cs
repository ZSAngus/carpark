using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Extend;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallBackExtend))]
public interface ICommunicationExtend
{
	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void ExtendRequestInterface(RequestArgs requestArgs);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	ResponseArgs ExtendRequestResponseInterface(RequestArgs requestArgs);
}
